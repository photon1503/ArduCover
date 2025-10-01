# Copilot Instructions for AI Coding Agents

## Overview
This workspace contains two PlatformIO-based firmware projects for microcontroller devices:
- `ArduCoverFW`: Motorized telescope cover/flat panel controller (DarkLight Cover Calibrator)
- `Firmware`: Fan controller for microcontroller-based systems

## Architecture & Major Components
- Each project is self-contained with its own `platformio.ini`, `src/`, `lib/`, and `include/` directories.
- `ArduCoverFW` supports multiple hardware features (cover, light, heater) via compile-time flags in `src/main.cpp`.
- `Firmware` focuses on PWM fan control and RPM measurement using interrupts.

## Build & Test Workflows
- **Build:** Use PlatformIO commands (`pio run`, `pio upload`) in each project folder.
- **Upload:** Board-specific settings are in `platformio.ini` (e.g., `esp32dev` for Firmware, `nanoatmega328` for ArduCoverFW).
- **Serial Monitor:** Use PlatformIO's monitor (`pio device monitor`) with speeds set in `platformio.ini` or `src/main.cpp`.
- **Testing:** If `test/` is populated, use `pio test`.

## Project-Specific Conventions
- **User-Adjustable Options:** Key hardware features are toggled via `#define` flags at the top of `src/main.cpp` in ArduCoverFW. Document changes here.
- **Servo Parameters:** Servo pulse widths and angles are set as constants in ArduCoverFW for easy tuning.
- **Fan Control:** Firmware uses high-resolution PWM and interrupt-driven tachometer readings. Pin assignments and PWM settings are defined at the top of `src/main.cpp`.
- **Versioning:** ArduCoverFW includes version and author info in a header comment. See the GitHub Wiki for change history.

## Integration & External Dependencies
- Both projects use the Arduino framework (see `platformio.ini`).
- ArduCoverFW integrates with INDI and ASCOM platforms for telescope automation.
- Firmware is designed for ESP32 (see board in `platformio.ini`).

## Patterns & Examples
- **Feature toggling:**
  ```cpp
  #define COVER_INSTALLED // Enable cover feature
  //#define LIGHT_INSTALLED // Disable light feature
  ```
- **PWM setup (Firmware):**
  ```cpp
  ledcSetup(PWM_CHANNEL, pwm_freq, PWM_RESOLUTION);
  ledcAttachPin(PWM_PIN, PWM_CHANNEL);
  ```
- **Interrupts (Firmware):**
  ```cpp
  attachInterrupt(digitalPinToInterrupt(TACH_PIN), tachISR, FALLING);
  ```

## Key Files & Directories
- `platformio.ini`: Board, platform, and upload settings
- `src/main.cpp`: Main firmware logic and hardware configuration
- `lib/`, `include/`: For shared libraries and headers (currently empty)
- `test/`: For unit/integration tests (currently empty)

## Tips for AI Agents
- Always check for hardware feature flags in `src/main.cpp` before implementing new features.
- Use PlatformIO CLI for builds, uploads, and monitoring.
- Reference board and framework settings in `platformio.ini` for compatibility.
- Document any new hardware features or changes in the header comment of `src/main.cpp`.

---

If any section is unclear or missing important project-specific details, please provide feedback to improve these instructions.