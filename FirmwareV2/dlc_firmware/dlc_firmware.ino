/*
  DarkLight CoverCalibrator Firmware (float-precision edition)
  Forked from DarkLight_CoverCalibrator by 10thTeeAstronomy
  https://github.com/10thTeeAstronomy/DarkLight_CoverCalibrator/wiki/Firmware-Version-History

  Recent Changes:
  - Stepwise servo movement with microsecond-level PWM smoothing
  - All movement logic uses float precision for angles and timing
  - Multiple easing functions supported (linear, cubic, sine, etc.)
  - Movement speed (timeToMoveCover) is user-settable via serial and saved/restored from EEPROM
  - Servo open/close angles are user-settable via serial (with decimal point) and saved/restored from EEPROM
  - All legacy features (LIGHT_INSTALLED, HEATER_INSTALLED, SECONDARY_SERVO) removed
  - Robust, non-blocking button and serial command handling
  - EEPROM wear-leveling retained for all persistent settings
  - Code cleaned up for maintainability and extension

  Serial Commands:
    P         : Query cover state (returns 0:NotPresent, 1:Closed, 2:Moving, 3:Open, 4:Unknown, 5:Error)
    O         : Open cover
    C         : Close cover
    H         : Halt cover movement
    T<ms>     : Set movement time (timeToMoveCover) in ms (1000-30000), e.g. T5000
    A<angle>  : Set open angle (float, 0.0-180.0), e.g. A135.5
    B<angle>  : Set close angle (float, 0.0-180.0), e.g. B10.0
    L         : Query calibrator state
    V         : Query firmware version

  Button Control (manual):
    Short press   : Open cover
    Double press  : Close cover
    Long press    : Halt cover movement

  All settable values (movement time, open/close angles) are saved to EEPROM and restored on startup.
*/

//----- (UA) USER-ADJUSTABLE OPTIONS ------
#define COVER_INSTALLED //comment out if not utilized
#define ENABLE_SERIAL_CONTROL //comment out if not utilized
#define ENABLE_MANUAL_CONTROL //comment out if not utilized
#define ENABLE_SAVING_TO_MEMORY //comment out if not utilized
//#define SHOW_HEARTBEAT //for debugging purposes only. shows loop is active, uncomment to flash builtin led
const uint32_t serialSpeed = 9600; //values are: (9600, 19200, 38400, 57600, (default 115200), 230400)

//----- (UA) (COVER) -----
uint32_t timeToMoveCover = 20000;  //(ms) time it takes to move between open/close positions (set between 1000(fast)-10000(slow) ms, recommend (5000 ms))
#define SAVED_TIMETOMOVECOVER 10 // EEPROM index for timeToMoveCover

//----- (UA) (COVER) PRIMARY SERVO PARAMETERS -----
const uint16_t primaryServoMinPulseWidth = 500;
const uint16_t primaryServoMaxPulseWidth = 2500;
float primaryServoOpenCoverAngle = 180.0f;
float primaryServoCloseCoverAngle = 0.0f;
#define SAVED_OPEN_ANGLE 11 // EEPROM index for open angle
#define SAVED_CLOSE_ANGLE 12 // EEPROM index for close angle



//----- (UA) (COVER) SELECT A MOVEMENT -----
//----- UNCOMMENT ONLY ONE OPTION, SEE MANUAL FOR DETAILS -----
//#define USE_LINEAR
//#define USE_CIRCULAR
//#define USE_CUBIC
//#define USE_EXPO
//#define USE_QUAD
//#define USE_QUART
//#define USE_QUINT
#define USE_SINE



//----- (UA) (BUTTONS) -----
const uint32_t debounceDelay = 150; //(ms) debounce time for the buttons *may need adjusting, see manual for details

//----- END OF (UA) USER-ADJUSTABLE OPTIONS -----
//-----------------------------------------------
//-------------- DO NOT EDIT BELOW --------------
//-----------------------------------------------
//------------ VARIABLE DECLARATION -------------

//----- VERSIONING CONTROL -----
const char* dlcVersion = "v1.2.0";

//----- MEMORY -----
#ifdef ENABLE_SAVING_TO_MEMORY
  #include <EEPROMWearLevel.h>
  #define EEPROM_LAYOUT_VERSION 1
  #define AMOUNT_OF_INDEXES 4
  #define EEPROM_LENGTH_TOUSE 1023
  #define SAVED_COVER_STATE 0
  #define SAVED_PANEL_VALUE 1
  #define SAVED_BROADBAND_VALUE 2
  #define SAVED_NARROWBAND_VALUE 3
#endif

//----- PIN ASSIGNMENT -----
const uint8_t primeServo = 9;
//PIN 13 RESERVED for LED_BUILTIN
const uint8_t servoButton = A1;
// ...existing code...

//----- COMM FOR ASCOM / INDI -----
uint8_t currentCoverState; //reports # 0:NotPresent, 1:Closed, 2:Moving, 3:Open, 4:Unknown, 5:Error
uint8_t calibratorState = 1; //reports # 0:NotPresent, 1:Off, 2:NotReady, 3:Ready, 4:Unknown, 5:Error
uint8_t heaterState; //reports # 0:NotPresent, 1:Off, 3:On, 4:Unknown, 5:Error, 6:Set (HeatOnClose)

//----- SERIAL -----
#ifdef ENABLE_SERIAL_CONTROL
  const char startMarker = '<'; //signal to process serial command
  const char endMarker = '>'; //signal that serial command is finished
  const uint8_t maxNumReceivedChars = 10; //set max num of characters in array
  const uint8_t maxNumSendChars = 75; //set max num of characters in array
  char receivedChars[maxNumReceivedChars]; //set array
  bool commandComplete = false; //flag to process command when end marker received
  char response[maxNumSendChars];
#endif

//----- COVER -----
#ifdef COVER_INSTALLED
  #include <dlcServo.h>
  uint8_t moveCoverTo; //1:Closed, 3:Open
  uint8_t previousMoveCoverTo;
  uint32_t startServoTimer;
  uint32_t elapsedMoveTime = 0;
  uint32_t proportionalMoveDuration = 0; // duration for current move (ms)
  bool halt = false;
  uint32_t startDetachTimer;
  bool detachServo = false;
  dlcServo primaryServo;
  float primaryServoLastPosition;
  float primaryServoMoveStartPosition;
  


  //validation check: ensure only one option of servo movement is defined
  #if (defined(USE_LINEAR) + defined(USE_CIRCULAR) + defined(USE_CUBIC) + \
      defined(USE_EXPO) + defined(USE_QUAD) + defined(USE_QUART) + \
      defined(USE_QUINT) + defined(USE_SINE)) > 1
  #error "Multiple easing options are defined. Please uncomment only one option."
  #elif !(defined(USE_LINEAR) || defined(USE_CIRCULAR) || defined(USE_CUBIC) || \
        defined(USE_EXPO) || defined(USE_QUAD) || defined(USE_QUART) || \
        defined(USE_QUINT) || defined(USE_SINE))
    #define USE_LINEAR // Default to linear if none are defined
    #pragma message("Warning: No easing option defined. Defaulting to USE_LINEAR.")
  #endif
#endif



//----- MANUAL OPERATION -----
#ifdef ENABLE_MANUAL_CONTROL
  const uint32_t doublePressTime = 400;
  const uint32_t longPressTime = 1000;
  uint32_t lastServoButtonPressTime = 0;

  uint8_t lastServoButtonState = 1;

#endif



//---------------------------------------
//----- END OF VARIABLE DECLARATION -----
//---------------------------------------

void setup(){
  
  pinMode(primeServo, OUTPUT);
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(servoButton, INPUT_PULLUP); //enable internal pull-up resistor
  
  #ifdef ENABLE_SAVING_TO_MEMORY
    EEPROMwl.begin(EEPROM_LAYOUT_VERSION, AMOUNT_OF_INDEXES, EEPROM_LENGTH_TOUSE);
  #endif
  
  initializeVariables();

  #if defined(ENABLE_SERIAL_CONTROL)
    initializeComms();
  #endif
}//end of setup

void loop(){
  
  #ifdef ENABLE_SERIAL_CONTROL
    checkSerial();

    if (commandComplete) {
      processCommand();
    }
  #endif

  #ifdef ENABLE_MANUAL_CONTROL
    checkButtons(); //check if buttons pressed
  #endif

  //monitor and move cover
  #ifdef COVER_INSTALLED
    monitorAndMoveCover();
    
    if (detachServo){
      completeDetach();
    }
  #endif


}//end of loop

void initializeVariables(){  
  //get saved values from EEPROM
  #ifdef ENABLE_SAVING_TO_MEMORY
    #ifdef COVER_INSTALLED
      currentCoverState = EEPROMwl.get(SAVED_COVER_STATE, currentCoverState);
      if (currentCoverState <= 0) currentCoverState = 4; //set cover to 4:Unknown if no recorded state exists
  // Restore timeToMoveCover from EEPROM
  uint32_t savedTime = EEPROMwl.get(SAVED_TIMETOMOVECOVER, timeToMoveCover);
  if (savedTime >= 1000 && savedTime <= 30000) timeToMoveCover = savedTime;
  // Restore open/close angles from EEPROM
  float savedOpen = EEPROMwl.get(SAVED_OPEN_ANGLE, primaryServoOpenCoverAngle);
  float savedClose = EEPROMwl.get(SAVED_CLOSE_ANGLE, primaryServoCloseCoverAngle);
  if (savedOpen >= 0.0f && savedOpen <= 180.0f) primaryServoOpenCoverAngle = savedOpen;
  if (savedClose >= 0.0f && savedClose <= 180.0f) primaryServoCloseCoverAngle = savedClose;
    #endif

  

  #endif
  



    //if panel in open position at start leave in position
    if (currentCoverState == 3) {
      primaryServo.write(primaryServoOpenCoverAngle);
      primaryServoLastPosition = primaryServoOpenCoverAngle;
      currentCoverState = 3;
    } else {
      primaryServo.write(primaryServoCloseCoverAngle);
      primaryServoLastPosition = primaryServoCloseCoverAngle;
      currentCoverState = 1;
    }

    previousMoveCoverTo = currentCoverState;
    attachServo();
    setDetachTimer(); //start timer to detach servo


 
    heaterState = 0; //heater not installed, set reporting to 0:NotPresent
 
}//end of initializeVariables

#ifdef ENABLE_SERIAL_CONTROL
  void initializeComms(){
    Serial.begin(serialSpeed); //start serial
    Serial.flush();
  }

  void checkSerial() {
    static uint8_t index = 0;
    static bool receiveInProgress = false;

    //while serial.available and command isn't complete, read the serial data
    while (Serial.available() > 0 && !commandComplete) {
      char incomingChar = Serial.read();

      if (receiveInProgress) {
        if (incomingChar != endMarker) {
          if(incomingChar == startMarker){
            index = 0;
            memset(receivedChars, 0, sizeof(receivedChars));
          }
          else {
            receivedChars[index] = incomingChar;
            index++;
            if (index >= maxNumReceivedChars) {
              index = maxNumReceivedChars - 1;
            }
          }
        } else {
          receivedChars[index] = '\0';  //terminate string
          receiveInProgress = false;
          commandComplete = true;
          index = 0;
        }
      } else if (incomingChar == startMarker) {
        receiveInProgress = true;
        index = 0;
        memset(receivedChars, 0, sizeof(receivedChars));
      }
    }
  }

  void processCommand() {
    
    char cmd = receivedChars[0];
    char* cmdParameter = &receivedChars[1];

    switch (cmd) {
        // Set open angle

      case 'A':
        {
          float newOpen = atof(cmdParameter);
          if (newOpen >= 0.0f && newOpen <= 180.0f) {
            primaryServoOpenCoverAngle = newOpen;
            #ifdef ENABLE_SAVING_TO_MEMORY
              EEPROMwl.put(SAVED_OPEN_ANGLE, primaryServoOpenCoverAngle);
            #endif
            snprintf(response, maxNumSendChars, "openAngle:%.2f", primaryServoOpenCoverAngle);
          } else {
            snprintf(response, maxNumSendChars, "ERR:Range(0-180)");
          }
          respondToCommand(response);
        }
        break;
      // Set close angle
      case 'B':
        {
          float newClose = atof(cmdParameter);
          if (newClose >= 0.0f && newClose <= 180.0f) {
            primaryServoCloseCoverAngle = newClose;
            #ifdef ENABLE_SAVING_TO_MEMORY
              EEPROMwl.put(SAVED_CLOSE_ANGLE, primaryServoCloseCoverAngle);
            #endif
            snprintf(response, maxNumSendChars, "closeAngle:%.2f", primaryServoCloseCoverAngle);
          } else {
            snprintf(response, maxNumSendChars, "ERR:Range(0-180)");
          }
          respondToCommand(response);
        }
        break;
      //currentCoverState reports # 0:NotPresent, 1:Closed, 2:Moving, 3:Open, 4:Unknown, 5:Error
      case 'P':
        getCoverState();
        respondToCommand(response);
        break;

      //OPEN cover
      #ifdef COVER_INSTALLED
        case 'O':
          openCover();
          respondToCommand(receivedChars);
          break;
    
        //CLOSE cover
        case 'C':
          closeCover();
          respondToCommand(receivedChars);
          break;
    
        //HALT cover moving
        case 'H':
          haltCover();
          respondToCommand(receivedChars);
          break;
      #endif //COVER_INSTALLED

      //CalibratorState (reports # 0:NotPresent, 1:Off, 2:NotReady, 3:Ready, 4:Unknown, 5:Error)
      case 'L':
        getCalibratorState();
        respondToCommand(response);
        break;

      //Set timeToMoveCover value via serial command
      case 'T':
        {
          uint32_t newTime = atol(cmdParameter); //parse as long
          if (newTime >= 1000 && newTime <= 30000) {
            // If cover is paused or moving, recalculate elapsedMoveTime to respect new speed
            if (currentCoverState == 2 || currentCoverState == 4) {
              static uint32_t lastTimeToMoveCover = timeToMoveCover;
              if (lastTimeToMoveCover != newTime && elapsedMoveTime > 0) {
                elapsedMoveTime = (uint32_t)((float)elapsedMoveTime * ((float)newTime / (float)lastTimeToMoveCover));
              }
              lastTimeToMoveCover = newTime;
            }
            timeToMoveCover = newTime;
            #ifdef ENABLE_SAVING_TO_MEMORY
              EEPROMwl.put(SAVED_TIMETOMOVECOVER, timeToMoveCover);
            #endif
            snprintf(response, maxNumSendChars, "timetomovecover:%lu", timeToMoveCover);
          } else {
            snprintf(response, maxNumSendChars, "ERR:Range(1000-10000)");
          }
          respondToCommand(response);
        }
        break;



      //DLC firmware version
      case 'V':
        respondToCommand(dlcVersion);
        break;

      //acknowledge and respond to unknown command received
      default:
        respondToCommand("?");
        break;
    }//end of switch (cmd)
  }//end of processCommand

  void respondToCommand(const char* response) {
    //acknowledge response to command
    char buffer[maxNumSendChars];
    snprintf(buffer, sizeof(buffer), "%c%s%c", startMarker, response, endMarker);
    Serial.print(buffer);

    commandComplete = false; //reset flag to receive next command
  }//end of respondToCommand
#endif //(ENABLE_SERIAL_CONTROL)

#ifdef ENABLE_MANUAL_CONTROL
  void checkButtons() {
    #ifdef COVER_INSTALLED
      // Garage door logic: single short press cycles through open, halt, reverse
      static bool buttonPressed = false;
      uint8_t readingServoButton = digitalRead(servoButton);
      if (readingServoButton == LOW && lastServoButtonState == HIGH && (millis() - lastServoButtonPressTime) > debounceDelay) {
        lastServoButtonPressTime = millis();
        buttonPressed = true;
      }
      if (buttonPressed) {
        buttonPressed = false;
        if (currentCoverState == 2) {
          // If moving, halt
          haltCover();
        } else if (currentCoverState == 1) {
          // If closed, open
          openCover();
        } else if (currentCoverState == 3) {
          // If open, close
          closeCover();
        } else if (currentCoverState == 4 || currentCoverState == 5) {
          // Unknown/Error: reverse last direction
          // Set moveCoverTo before calling open/close to ensure correct direction
          if (previousMoveCoverTo == 1) {
            moveCoverTo = 3;
            openCover();
          } else {
            moveCoverTo = 1;
            closeCover();
          }
        }
      }
      lastServoButtonState = readingServoButton;
    #endif  //COVER_INSTALLED

   
  }
#endif //ENABLE_MANUAL_CONTROL

#ifdef ENABLE_SERIAL_CONTROL
  void getCoverState(){
    itoa(currentCoverState, response, 10); //convert integer to string
  }
#endif

#ifdef COVER_INSTALLED
  void openCover(){
    //if not already moving, OPEN, or set to 0:Not Present
    if (currentCoverState != 2 && currentCoverState != 3 && currentCoverState != 0) {

      moveCoverTo = 3;
      setMovement();
    }
  }//end of openCover

  void closeCover(){
    //if not already moving, CLOSED, or set to 0:Not Present
    if (currentCoverState != 2 && currentCoverState != 1 && currentCoverState != 0) {
      moveCoverTo = 1;
      setMovement();
    }
  }//end of closeCover

  void haltCover(){
    //if moving
    if (currentCoverState == 2) {
      halt = true;
      previousMoveCoverTo = moveCoverTo;
      currentCoverState = 4; //set to Unknown
      elapsedMoveTime += millis() - startServoTimer;
      setDetachTimer();
    }
  }//end of haltCover

  void attachServo(){
    primaryServo.attach(primeServo, primaryServoMinPulseWidth, primaryServoMaxPulseWidth);


  }//end of attachServo
  
  void setDetachTimer(){
    detachServo = true;
    startDetachTimer = millis();
  }//end of setDetachTimer
  
  void completeDetach(){
    uint32_t detachTime = 3000;
    
    //detach to stop sending PWM since servo stopped
    if (millis() - startDetachTimer >= detachTime){
      primaryServo.detach();



      detachServo = false;
    }
  }//end of completeDetach
  
  void setMovement(){
  //sets time left and servo position based on previous and expected direction for calculation in monitorAndMoveCover
  detachServo = false;  //reset in case restart issued right after halt issued
  // Always start from current position for smooth movement
    primaryServoLastPosition = primaryServo.read();
    primaryServoMoveStartPosition = primaryServoLastPosition;
    // Always reset elapsedMoveTime when starting a new move after halt (Unknown state) and direction is reversed
    if ((currentCoverState == 4 && previousMoveCoverTo != moveCoverTo) || (previousMoveCoverTo == 1 && currentCoverState == 1) || (previousMoveCoverTo == 3 && currentCoverState == 3)) {
      elapsedMoveTime = 0;
    }
  // Calculate proportional move duration
  float targetPosition = (moveCoverTo == 3) ? primaryServoOpenCoverAngle : primaryServoCloseCoverAngle;
  float angleDistance = abs(targetPosition - primaryServoLastPosition);
  float totalDistance = abs(primaryServoOpenCoverAngle - primaryServoCloseCoverAngle);
  if (totalDistance < 1.0f) totalDistance = 1.0f; // avoid divide by zero
  proportionalMoveDuration = (uint32_t)(timeToMoveCover * (angleDistance / totalDistance));
  if (proportionalMoveDuration < 100) proportionalMoveDuration = 100; // minimum duration for short moves
  attachServo();
  currentCoverState = 2;
  startServoTimer = millis();
  halt = false; //reset
  }//end of setMovement
  
  void monitorAndMoveCover(){
    uint32_t currentMillis; //holds current milliseconds
    
    //monitor moving and unknown cover
    if (currentCoverState == 2 || currentCoverState == 4){
      //get current time
      currentMillis = millis();

      //report ERROR if timeToMoveCover * 2 reached
      if (currentMillis - startServoTimer >= timeToMoveCover * 2){
        currentCoverState = 5;
        #ifdef ENABLE_SAVING_TO_MEMORY
          saveCurrentCoverState();
        #endif
        return; //exit function since error reached
      }

      //if moving, then move cover
      if (currentCoverState == 2) {
        static uint32_t lastStepTime = 0;
        uint32_t currentServoTimer = millis();
        float progress;
        // For resuming after halt, include elapsedMoveTime
        if (elapsedMoveTime > 0) {
          progress = (float)(currentServoTimer - startServoTimer + elapsedMoveTime) / proportionalMoveDuration;
        } else {
          progress = (float)(currentServoTimer - startServoTimer) / proportionalMoveDuration;
        }
        progress = constrain(progress, 0.0, 1.0); //stay within bounds

        float primaryServoTargetPosition = (moveCoverTo == 3) ? primaryServoOpenCoverAngle : primaryServoCloseCoverAngle;
        // Always apply the selected easing function
  float easedProgress = calculateEasedProgress(progress);
  float newPosition = primaryServoMoveStartPosition + (primaryServoTargetPosition - primaryServoMoveStartPosition) * easedProgress;

        // Stepwise control: update servo every stepInterval ms
        const uint32_t stepInterval = 5; // ms between steps (finer granularity)
        if (currentServoTimer - lastStepTime >= stepInterval) {
          // Calculate eased progress for current time
          float stepProgress;
          if (elapsedMoveTime > 0) {
            stepProgress = (float)(currentServoTimer - startServoTimer + elapsedMoveTime) / proportionalMoveDuration;
          } else {
            stepProgress = (float)(currentServoTimer - startServoTimer) / proportionalMoveDuration;
          }
          stepProgress = constrain(stepProgress, 0.0, 1.0);
          float stepEasedProgress = calculateEasedProgress(stepProgress);
          // Always interpolate from move start position to target
          float nextPosition = primaryServoMoveStartPosition + (primaryServoTargetPosition - primaryServoMoveStartPosition) * stepEasedProgress;
          // Clamp to target
          if ((moveCoverTo == 3 && nextPosition > primaryServoOpenCoverAngle) ||
              (moveCoverTo == 1 && nextPosition < primaryServoCloseCoverAngle)) {
            nextPosition = (moveCoverTo == 3) ? primaryServoOpenCoverAngle : primaryServoCloseCoverAngle;
          }
          // Use writeMicroseconds for smoother movement if supported
          uint16_t pulseWidth = primaryServoMinPulseWidth + (uint16_t)((nextPosition / 180.0f) * (primaryServoMaxPulseWidth - primaryServoMinPulseWidth));
          primaryServo.writeMicroseconds(pulseWidth);
          primaryServoLastPosition = nextPosition;
          lastStepTime = currentServoTimer;
        }

        if (progress == 1.0 || abs(primaryServoLastPosition - primaryServoTargetPosition) < 0.01f){
          //if cover moved to close/open
          elapsedMoveTime = 0;
          primaryServoLastPosition = primaryServoTargetPosition;
          previousMoveCoverTo = currentCoverState = (moveCoverTo == 3) ? 3 : 1;
          #ifdef ENABLE_SAVING_TO_MEMORY
            saveCurrentCoverState();
          #endif
          setDetachTimer();
          lastStepTime = 0; // reset for next move
        }
      }//end of if moving, then move cover
    }//end of monitor moving and unknown cover
  }//end of monitorAndMoveCover

  float calculateServoPosition(unsigned long actualServoTime, unsigned long servoStartTime, float lastPosition, float targetPosition, float progress, float remainingDistance, float openAngle, float closeAngle) {
    #ifdef USE_LINEAR
      return lastPosition + (targetPosition - lastPosition) * progress;
    #endif
  }

  float calculateEasedProgress(float progress){
  #ifdef USE_CIRCULAR
    return (progress < 0.5) ? 0.5 * (1 - sqrt(1 - 4 * pow(progress, 2))) : 0.5 * (sqrt(-((2 * progress) - 3) * ((2 * progress) - 1)) + 1);
  #elif defined(USE_CUBIC)
    return (progress < 0.5) ? 4 * progress * progress * progress : 1 - pow(-2 * progress + 2, 3) / 2;
  #elif defined(USE_EXPO)
    return (progress == 0) ? 0 : (progress == 1) ? 1 : (progress < 0.5) ? pow(2, 20 * progress - 10) / 2 : (2 - pow(2, -20 * progress + 10)) / 2;
  #elif defined(USE_QUAD)
    return (progress < 0.5) ? 2 * progress * progress : 1 - pow(-2 * progress + 2, 2) / 2;
  #elif defined(USE_QUART)
    return (progress < 0.5) ? 8 * progress * progress * progress * progress : 1 - pow(-2 * progress + 2, 4) / 2;
  #elif defined(USE_QUINT)
    return (progress < 0.5) ? 16 * progress * progress * progress * progress * progress : 1 - pow(-2 * progress + 2, 5) / 2;
  #elif defined(USE_SINE)
    return -(cos(M_PI * progress) - 1) / 2;
  #else
    return progress; // fallback to linear if no easing defined
  #endif
  }//end of calculateEasedProgress

  #ifdef ENABLE_SAVING_TO_MEMORY
    void saveCurrentCoverState(){
      EEPROMwl.put(SAVED_COVER_STATE, currentCoverState);
    }//end of saveCurrentCoverState
  #endif
#endif //COVER_INSTALLED

#ifdef ENABLE_SERIAL_CONTROL
  void getCalibratorState(){
    itoa(calibratorState, response, 10); //convert integer to string
  }
#endif