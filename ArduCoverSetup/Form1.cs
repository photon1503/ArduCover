using System;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace ArduCoverSetup
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
            LoadComPorts();
        }

        private void LoadComPorts()
        {
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (comboBoxComPort.Items.Count > 0)
                comboBoxComPort.SelectedIndex = 0;
        }

        private bool EnsurePortOpen()
        {
            if (serialPort == null)
            {
                if (comboBoxComPort.SelectedItem == null)
                {
                    MessageBox.Show("Select a COM port.");
                    return false;
                }
                serialPort = new System.IO.Ports.SerialPort(comboBoxComPort.SelectedItem.ToString(), 9600);
                serialPort.ReadTimeout = 1000;
                serialPort.WriteTimeout = 1000;
                try { serialPort.Open(); } catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
            }
            return true;
        }

        private string SendSerialCommand(string command)
        {
            try
            {
                if (!EnsurePortOpen()) return "Port not open";
                string wrapped = $"<{command}>";
                serialPort.DiscardInBuffer();
                serialPort.Write(wrapped);
                // Read until '>' is received
                string response = "";
                while (true)
                {
                    int b = serialPort.ReadByte();
                    if (b == -1) break;
                    char c = (char)b;
                    response += c;
                    if (c == '>') break;
                }
                return response;
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

        private void btnQueryCalState_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("L");
        }

        private void btnQueryVersion_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("V");
        }

        private void btnQuerySettings_Click(object sender, EventArgs e)
        {
            string response = SendSerialCommand("S");
            txtResponse.Text = response;
            // Parse response like <T:12000,A:180.00,B:0.00,M:ON>
            if (response.StartsWith("<") && response.EndsWith(">"))
            {
                string inner = response.Trim('<', '>');
                var parts = inner.Split(',');
                foreach (var part in parts)
                {
                    var kv = part.Split(':');
                    if (kv.Length == 2)
                    {
                        switch (kv[0])
                        {
                            case "T":
                                txtMoveTimeMs.Text = kv[1];
                                break;

                            case "A":
                                txtOpenAngle.Text = kv[1];
                                break;

                            case "B":
                                txtCloseAngle.Text = kv[1];
                                break;

                            case "M":
                                // Optionally, update UI for auto-detach if you add a control
                                break;
                        }
                    }
                }
            }
        }

        private void btnEnableAutoDetach_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("M1");
        }

        private void btnDisableAutoDetach_Click(object sender, EventArgs e)
        {
            txtResponse.Text = SendSerialCommand("M0");
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
                serialPort.Close();
            base.OnFormClosed(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float.TryParse(txtCloseAngle.Text, out float angle);
            angle += 0.5f;
            // Clamp to 180
            if (angle > 180) angle = 180;
            txtCloseAngle.Text = angle.ToString("F2");
            btnSetCloseAngle_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float.TryParse(txtCloseAngle.Text, out float angle);
            angle -= 0.5f;
            // Clamp to 0
            if (angle < 0) angle = 0;
            txtCloseAngle.Text = angle.ToString("F2");
            btnSetCloseAngle_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float.TryParse(txtOpenAngle.Text, out float angle);
            angle -= 0.5f;
            // Clamp to 180
            if (angle < 0) angle = 0;
            txtOpenAngle.Text = angle.ToString("F2");
            btnSetOpenAngle_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float.TryParse(txtOpenAngle.Text, out float angle);
            angle += 0.5f;
            // Clamp to 180
            if (angle > 180) angle = 180;
            txtOpenAngle.Text = angle.ToString("F2");
            btnSetOpenAngle_Click(sender, e);
        }
    }
}