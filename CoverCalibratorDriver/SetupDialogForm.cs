using ASCOM.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.ArduCover1.CoverCalibrator
{
    [ComVisible(false)] // Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        private const string NO_PORTS_MESSAGE = "No COM ports found";
        private TraceLogger tl; // Holder for a reference to the driver's trace logger

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here and update the state variables with results from the dialogue

            tl.Enabled = chkTrace.Checked;

            // Update the COM port variable if one has been selected
            if (comboBoxComPort.SelectedItem is null) // No COM port selected
            {
                tl.LogMessage("Setup OK", $"New configuration values - COM Port: Not selected");
            }
            else if (comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
            {
                tl.LogMessage("Setup OK", $"New configuration values - NO COM ports detected on this PC.");
            }
            else // A valid COM port has been selected
            {
                CoverCalibratorHardware.comPort = (string)comboBoxComPort.SelectedItem;
                tl.LogMessage("Setup OK", $"New configuration values - COM Port: {comboBoxComPort.SelectedItem}");
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            // Set the trace checkbox
            chkTrace.Checked = tl.Enabled;

            // set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear(); // Clear any existing entries
            using (Serial serial = new Serial()) // User the Se5rial component to get an extended list of COM ports
            {
                comboBoxComPort.Items.AddRange(serial.AvailableCOMPorts);
            }

            // If no ports are found include a message to this effect
            if (comboBoxComPort.Items.Count == 0)
            {
                comboBoxComPort.Items.Add(NO_PORTS_MESSAGE);
                comboBoxComPort.SelectedItem = NO_PORTS_MESSAGE;
            }

            // select the current port if possible
            if (comboBoxComPort.Items.Contains(CoverCalibratorHardware.comPort))
            {
                comboBoxComPort.SelectedItem = CoverCalibratorHardware.comPort;
            }

            tl.LogMessage("InitUI", $"Set GUI controls to Trace: {chkTrace.Checked}, COM Port: {comboBoxComPort.SelectedItem}");
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            // Bring the setup dialogue to the front of the screen
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }

        // --- Begin: Serial Command UI and Logic ---
        private string SendSerialCommand(string command)
        {
            try
            {
                string wrapped = $"<{command}>";
                using (var serial = new Serial())
                {
                    serial.PortName = comboBoxComPort.SelectedItem?.ToString();
                    serial.Speed = SerialSpeed.ps9600;
                    serial.Connected = true;
                    serial.Transmit(wrapped);
                    // Use ReceiveTerminated with only the terminator (no timeout argument)
                    string response = serial.ReceiveTerminated(">");
                    serial.Connected = false;
                    return response?.Trim();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private void btnQueryCoverState_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("P");
        }

        private void btnOpenCover_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("O");
        }

        private void btnCloseCover_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("C");
        }

        private void btnHaltCover_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("H");
        }

        private void btnSetMoveTime_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMoveTimeMs.Text, out int ms) && ms >= 1000 && ms <= 30000)
                txtResponse.Text = SendSerialCommand($"T{ms}");
            else
                txtResponse.Text = "Invalid ms (1000-30000)";
        }

        private void btnSetOpenAngle_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtOpenAngle.Text, out float angle) && angle >= 0 && angle <= 180)
                txtResponse.Text = SendSerialCommand($"A{angle}");
            else
                txtResponse.Text = "Invalid angle (0-180)";
        }

        private void btnSetCloseAngle_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtCloseAngle.Text, out float angle) && angle >= 0 && angle <= 180)
                txtResponse.Text = SendSerialCommand($"B{angle}");
            else
                txtResponse.Text = "Invalid angle (0-180)";
        }

        private void btnQueryCalState_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("L");
        }

        private void btnQueryVersion_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("V");
        }

        // --- End: Serial Command UI and Logic ---
    }
}