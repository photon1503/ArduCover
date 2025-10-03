namespace ArduCoverSetup
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxComPort = new ComboBox();
            btnQueryCoverState = new Button();
            btnOpenCover = new Button();
            btnCloseCover = new Button();
            btnHaltCover = new Button();
            btnQueryVersion = new Button();
            btnQuerySettings = new Button();
            btnEnableAutoDetach = new Button();
            btnDisableAutoDetach = new Button();
            txtMoveTimeMs = new TextBox();
            btnSetMoveTime = new Button();
            txtOpenAngle = new TextBox();
            btnSetOpenAngle = new Button();
            txtCloseAngle = new TextBox();
            btnSetCloseAngle = new Button();
            txtResponse = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // comboBoxComPort
            // 
            comboBoxComPort.FormattingEnabled = true;
            comboBoxComPort.Location = new Point(12, 12);
            comboBoxComPort.Name = "comboBoxComPort";
            comboBoxComPort.Size = new Size(121, 23);
            comboBoxComPort.TabIndex = 0;
            // 
            // btnQueryCoverState
            // 
            btnQueryCoverState.Location = new Point(12, 50);
            btnQueryCoverState.Name = "btnQueryCoverState";
            btnQueryCoverState.Size = new Size(120, 23);
            btnQueryCoverState.TabIndex = 1;
            btnQueryCoverState.Text = "Query Cover State";
            btnQueryCoverState.UseVisualStyleBackColor = true;
            btnQueryCoverState.Click += btnQueryCoverState_Click;
            // 
            // btnOpenCover
            // 
            btnOpenCover.Location = new Point(12, 80);
            btnOpenCover.Name = "btnOpenCover";
            btnOpenCover.Size = new Size(60, 23);
            btnOpenCover.TabIndex = 2;
            btnOpenCover.Text = "Open";
            btnOpenCover.UseVisualStyleBackColor = true;
            btnOpenCover.Click += btnOpenCover_Click;
            // 
            // btnCloseCover
            // 
            btnCloseCover.Location = new Point(72, 80);
            btnCloseCover.Name = "btnCloseCover";
            btnCloseCover.Size = new Size(60, 23);
            btnCloseCover.TabIndex = 3;
            btnCloseCover.Text = "Close";
            btnCloseCover.UseVisualStyleBackColor = true;
            btnCloseCover.Click += btnCloseCover_Click;
            // 
            // btnHaltCover
            // 
            btnHaltCover.Location = new Point(132, 80);
            btnHaltCover.Name = "btnHaltCover";
            btnHaltCover.Size = new Size(60, 23);
            btnHaltCover.TabIndex = 4;
            btnHaltCover.Text = "Halt";
            btnHaltCover.UseVisualStyleBackColor = true;
            btnHaltCover.Click += btnHaltCover_Click;
            // 
            // btnQueryVersion
            // 
            btnQueryVersion.Location = new Point(12, 140);
            btnQueryVersion.Name = "btnQueryVersion";
            btnQueryVersion.Size = new Size(120, 23);
            btnQueryVersion.TabIndex = 6;
            btnQueryVersion.Text = "Query Version";
            btnQueryVersion.UseVisualStyleBackColor = true;
            btnQueryVersion.Click += btnQueryVersion_Click;
            // 
            // btnQuerySettings
            // 
            btnQuerySettings.Location = new Point(12, 170);
            btnQuerySettings.Name = "btnQuerySettings";
            btnQuerySettings.Size = new Size(120, 23);
            btnQuerySettings.TabIndex = 7;
            btnQuerySettings.Text = "Query Settings";
            btnQuerySettings.UseVisualStyleBackColor = true;
            btnQuerySettings.Click += btnQuerySettings_Click;
            // 
            // btnEnableAutoDetach
            // 
            btnEnableAutoDetach.Location = new Point(12, 200);
            btnEnableAutoDetach.Name = "btnEnableAutoDetach";
            btnEnableAutoDetach.Size = new Size(156, 23);
            btnEnableAutoDetach.TabIndex = 8;
            btnEnableAutoDetach.Text = "Enable Auto-Detach";
            btnEnableAutoDetach.UseVisualStyleBackColor = true;
            btnEnableAutoDetach.Click += btnEnableAutoDetach_Click;
            // 
            // btnDisableAutoDetach
            // 
            btnDisableAutoDetach.Location = new Point(12, 230);
            btnDisableAutoDetach.Name = "btnDisableAutoDetach";
            btnDisableAutoDetach.Size = new Size(156, 23);
            btnDisableAutoDetach.TabIndex = 9;
            btnDisableAutoDetach.Text = "Disable Auto-Detach";
            btnDisableAutoDetach.UseVisualStyleBackColor = true;
            btnDisableAutoDetach.Click += btnDisableAutoDetach_Click;
            // 
            // txtMoveTimeMs
            // 
            txtMoveTimeMs.Location = new Point(12, 260);
            txtMoveTimeMs.Name = "txtMoveTimeMs";
            txtMoveTimeMs.Size = new Size(60, 23);
            txtMoveTimeMs.TabIndex = 10;
            // 
            // btnSetMoveTime
            // 
            btnSetMoveTime.Location = new Point(72, 260);
            btnSetMoveTime.Name = "btnSetMoveTime";
            btnSetMoveTime.Size = new Size(120, 23);
            btnSetMoveTime.TabIndex = 11;
            btnSetMoveTime.Text = "Set Move Time (ms)";
            btnSetMoveTime.UseVisualStyleBackColor = true;
            btnSetMoveTime.Click += btnSetMoveTime_Click;
            // 
            // txtOpenAngle
            // 
            txtOpenAngle.Location = new Point(73, 289);
            txtOpenAngle.Name = "txtOpenAngle";
            txtOpenAngle.Size = new Size(60, 23);
            txtOpenAngle.TabIndex = 12;
            // 
            // btnSetOpenAngle
            // 
            btnSetOpenAngle.Location = new Point(195, 290);
            btnSetOpenAngle.Name = "btnSetOpenAngle";
            btnSetOpenAngle.Size = new Size(120, 23);
            btnSetOpenAngle.TabIndex = 13;
            btnSetOpenAngle.Text = "Set Open Angle";
            btnSetOpenAngle.UseVisualStyleBackColor = true;
            btnSetOpenAngle.Click += btnSetOpenAngle_Click;
            // 
            // txtCloseAngle
            // 
            txtCloseAngle.Location = new Point(72, 320);
            txtCloseAngle.Name = "txtCloseAngle";
            txtCloseAngle.Size = new Size(60, 23);
            txtCloseAngle.TabIndex = 14;
            // 
            // btnSetCloseAngle
            // 
            btnSetCloseAngle.Location = new Point(195, 319);
            btnSetCloseAngle.Name = "btnSetCloseAngle";
            btnSetCloseAngle.Size = new Size(120, 23);
            btnSetCloseAngle.TabIndex = 15;
            btnSetCloseAngle.Text = "Set Close Angle";
            btnSetCloseAngle.UseVisualStyleBackColor = true;
            btnSetCloseAngle.Click += btnSetCloseAngle_Click;
            // 
            // txtResponse
            // 
            txtResponse.Location = new Point(321, 12);
            txtResponse.Multiline = true;
            txtResponse.Name = "txtResponse";
            txtResponse.ScrollBars = ScrollBars.Vertical;
            txtResponse.Size = new Size(279, 331);
            txtResponse.TabIndex = 16;
            // 
            // button1
            // 
            button1.Location = new Point(12, 320);
            button1.Name = "button1";
            button1.Size = new Size(54, 23);
            button1.TabIndex = 17;
            button1.Text = "-";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(138, 320);
            button2.Name = "button2";
            button2.Size = new Size(54, 23);
            button2.TabIndex = 18;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(139, 291);
            button3.Name = "button3";
            button3.Size = new Size(54, 23);
            button3.TabIndex = 19;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 289);
            button4.Name = "button4";
            button4.Size = new Size(54, 23);
            button4.TabIndex = 20;
            button4.Text = "-";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 384);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtResponse);
            Controls.Add(btnSetCloseAngle);
            Controls.Add(txtCloseAngle);
            Controls.Add(btnSetOpenAngle);
            Controls.Add(txtOpenAngle);
            Controls.Add(btnSetMoveTime);
            Controls.Add(txtMoveTimeMs);
            Controls.Add(btnDisableAutoDetach);
            Controls.Add(btnEnableAutoDetach);
            Controls.Add(btnQuerySettings);
            Controls.Add(btnQueryVersion);
            Controls.Add(btnHaltCover);
            Controls.Add(btnCloseCover);
            Controls.Add(btnOpenCover);
            Controls.Add(btnQueryCoverState);
            Controls.Add(comboBoxComPort);
            Name = "Form1";
            Text = "ArduCover Setup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Button btnQueryCoverState;
        private System.Windows.Forms.Button btnOpenCover;
        private System.Windows.Forms.Button btnCloseCover;
        private System.Windows.Forms.Button btnHaltCover;
        private System.Windows.Forms.Button btnQueryVersion;
        private System.Windows.Forms.Button btnQuerySettings;
        private System.Windows.Forms.Button btnEnableAutoDetach;
        private System.Windows.Forms.Button btnDisableAutoDetach;
        private System.Windows.Forms.TextBox txtMoveTimeMs;
        private System.Windows.Forms.Button btnSetMoveTime;
        private System.Windows.Forms.TextBox txtOpenAngle;
        private System.Windows.Forms.Button btnSetOpenAngle;
        private System.Windows.Forms.TextBox txtCloseAngle;
        private System.Windows.Forms.Button btnSetCloseAngle;
        private System.Windows.Forms.TextBox txtResponse;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
