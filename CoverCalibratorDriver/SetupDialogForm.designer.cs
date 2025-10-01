namespace ASCOM.ArduCover1.CoverCalibrator
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.btnQueryCoverState = new System.Windows.Forms.Button();
            this.btnOpenCover = new System.Windows.Forms.Button();
            this.btnCloseCover = new System.Windows.Forms.Button();
            this.btnHaltCover = new System.Windows.Forms.Button();
            this.txtMoveTimeMs = new System.Windows.Forms.TextBox();
            this.btnSetMoveTime = new System.Windows.Forms.Button();
            this.txtOpenAngle = new System.Windows.Forms.TextBox();
            this.btnSetOpenAngle = new System.Windows.Forms.Button();
            this.txtCloseAngle = new System.Windows.Forms.TextBox();
            this.btnSetCloseAngle = new System.Windows.Forms.Button();
            this.btnQueryCalState = new System.Windows.Forms.Button();
            this.btnQueryVersion = new System.Windows.Forms.Button();
            this.txtResponse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(281, 112);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(281, 142);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = ((System.Drawing.Image)(resources.GetObject("picASCOM.Image")));
            this.picASCOM.Location = new System.Drawing.Point(292, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(77, 118);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(77, 87);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(133, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // btnQueryCoverState
            // 
            this.btnQueryCoverState.Location = new System.Drawing.Point(16, 170);
            this.btnQueryCoverState.Name = "btnQueryCoverState";
            this.btnQueryCoverState.Size = new System.Drawing.Size(110, 23);
            this.btnQueryCoverState.TabIndex = 8;
            this.btnQueryCoverState.Text = "Query Cover";
            this.btnQueryCoverState.UseVisualStyleBackColor = true;
            this.btnQueryCoverState.Click += new System.EventHandler(this.btnQueryCoverState_Click);
            // 
            // btnOpenCover
            // 
            this.btnOpenCover.Location = new System.Drawing.Point(16, 198);
            this.btnOpenCover.Name = "btnOpenCover";
            this.btnOpenCover.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCover.TabIndex = 9;
            this.btnOpenCover.Text = "Open";
            this.btnOpenCover.UseVisualStyleBackColor = true;
            this.btnOpenCover.Click += new System.EventHandler(this.btnOpenCover_Click);
            // 
            // btnCloseCover
            // 
            this.btnCloseCover.Location = new System.Drawing.Point(100, 198);
            this.btnCloseCover.Name = "btnCloseCover";
            this.btnCloseCover.Size = new System.Drawing.Size(75, 23);
            this.btnCloseCover.TabIndex = 10;
            this.btnCloseCover.Text = "Close";
            this.btnCloseCover.UseVisualStyleBackColor = true;
            this.btnCloseCover.Click += new System.EventHandler(this.btnCloseCover_Click);
            // 
            // btnHaltCover
            // 
            this.btnHaltCover.Location = new System.Drawing.Point(185, 198);
            this.btnHaltCover.Name = "btnHaltCover";
            this.btnHaltCover.Size = new System.Drawing.Size(75, 23);
            this.btnHaltCover.TabIndex = 11;
            this.btnHaltCover.Text = "Halt";
            this.btnHaltCover.UseVisualStyleBackColor = true;
            this.btnHaltCover.Click += new System.EventHandler(this.btnHaltCover_Click);
            // 
            // txtMoveTimeMs
            // 
            this.txtMoveTimeMs.Location = new System.Drawing.Point(16, 226);
            this.txtMoveTimeMs.Name = "txtMoveTimeMs";
            this.txtMoveTimeMs.Size = new System.Drawing.Size(60, 20);
            this.txtMoveTimeMs.TabIndex = 12;
            // 
            // btnSetMoveTime
            // 
            this.btnSetMoveTime.Location = new System.Drawing.Point(80, 226);
            this.btnSetMoveTime.Name = "btnSetMoveTime";
            this.btnSetMoveTime.Size = new System.Drawing.Size(90, 23);
            this.btnSetMoveTime.TabIndex = 13;
            this.btnSetMoveTime.Text = "Set Move ms";
            this.btnSetMoveTime.UseVisualStyleBackColor = true;
            this.btnSetMoveTime.Click += new System.EventHandler(this.btnSetMoveTime_Click);
            // 
            // txtOpenAngle
            // 
            this.txtOpenAngle.Location = new System.Drawing.Point(16, 254);
            this.txtOpenAngle.Name = "txtOpenAngle";
            this.txtOpenAngle.Size = new System.Drawing.Size(40, 20);
            this.txtOpenAngle.TabIndex = 14;
            // 
            // btnSetOpenAngle
            // 
            this.btnSetOpenAngle.Location = new System.Drawing.Point(60, 254);
            this.btnSetOpenAngle.Name = "btnSetOpenAngle";
            this.btnSetOpenAngle.Size = new System.Drawing.Size(75, 23);
            this.btnSetOpenAngle.TabIndex = 15;
            this.btnSetOpenAngle.Text = "Set Open";
            this.btnSetOpenAngle.UseVisualStyleBackColor = true;
            this.btnSetOpenAngle.Click += new System.EventHandler(this.btnSetOpenAngle_Click);
            // 
            // txtCloseAngle
            // 
            this.txtCloseAngle.Location = new System.Drawing.Point(140, 254);
            this.txtCloseAngle.Name = "txtCloseAngle";
            this.txtCloseAngle.Size = new System.Drawing.Size(40, 20);
            this.txtCloseAngle.TabIndex = 16;
            // 
            // btnSetCloseAngle
            // 
            this.btnSetCloseAngle.Location = new System.Drawing.Point(185, 254);
            this.btnSetCloseAngle.Name = "btnSetCloseAngle";
            this.btnSetCloseAngle.Size = new System.Drawing.Size(75, 23);
            this.btnSetCloseAngle.TabIndex = 17;
            this.btnSetCloseAngle.Text = "Set Close";
            this.btnSetCloseAngle.UseVisualStyleBackColor = true;
            this.btnSetCloseAngle.Click += new System.EventHandler(this.btnSetCloseAngle_Click);
            // 
            // btnQueryCalState
            // 
            this.btnQueryCalState.Location = new System.Drawing.Point(16, 282);
            this.btnQueryCalState.Name = "btnQueryCalState";
            this.btnQueryCalState.Size = new System.Drawing.Size(120, 23);
            this.btnQueryCalState.TabIndex = 18;
            this.btnQueryCalState.Text = "Query Cal State";
            this.btnQueryCalState.UseVisualStyleBackColor = true;
            this.btnQueryCalState.Click += new System.EventHandler(this.btnQueryCalState_Click);
            // 
            // btnQueryVersion
            // 
            this.btnQueryVersion.Location = new System.Drawing.Point(140, 282);
            this.btnQueryVersion.Name = "btnQueryVersion";
            this.btnQueryVersion.Size = new System.Drawing.Size(120, 23);
            this.btnQueryVersion.TabIndex = 19;
            this.btnQueryVersion.Text = "Query Version";
            this.btnQueryVersion.UseVisualStyleBackColor = true;
            this.btnQueryVersion.Click += new System.EventHandler(this.btnQueryVersion_Click);
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(16, 310);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(320, 60);
            this.txtResponse.TabIndex = 20;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 379);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.btnQueryVersion);
            this.Controls.Add(this.btnQueryCalState);
            this.Controls.Add(this.btnSetCloseAngle);
            this.Controls.Add(this.txtCloseAngle);
            this.Controls.Add(this.btnSetOpenAngle);
            this.Controls.Add(this.txtOpenAngle);
            this.Controls.Add(this.btnSetMoveTime);
            this.Controls.Add(this.txtMoveTimeMs);
            this.Controls.Add(this.btnHaltCover);
            this.Controls.Add(this.btnCloseCover);
            this.Controls.Add(this.btnOpenCover);
            this.Controls.Add(this.btnQueryCoverState);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ArduCover1 Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Button btnQueryCoverState;
        private System.Windows.Forms.Button btnOpenCover;
        private System.Windows.Forms.Button btnCloseCover;
        private System.Windows.Forms.Button btnHaltCover;
        private System.Windows.Forms.TextBox txtMoveTimeMs;
        private System.Windows.Forms.Button btnSetMoveTime;
        private System.Windows.Forms.TextBox txtOpenAngle;
        private System.Windows.Forms.Button btnSetOpenAngle;
        private System.Windows.Forms.TextBox txtCloseAngle;
        private System.Windows.Forms.Button btnSetCloseAngle;
        private System.Windows.Forms.Button btnQueryCalState;
        private System.Windows.Forms.Button btnQueryVersion;
        private System.Windows.Forms.TextBox txtResponse;
    }
}