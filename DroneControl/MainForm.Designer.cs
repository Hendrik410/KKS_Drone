namespace DroneControl
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ipInfoLabel = new System.Windows.Forms.Label();
            this.pingLabel = new System.Windows.Forms.Label();
            this.statusArmedLabel = new System.Windows.Forms.Label();
            this.armToogleButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.calibrateGyroButton = new System.Windows.Forms.Button();
            this.infoPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.headingIndicator = new DroneControl.Avionics.HeadingIndicatorInstrumentControl();
            this.artificialHorizon = new DroneControl.Avionics.AttitudeIndicatorInstrumentControl();
            this.motorControl1 = new DroneControl.MotorControl();
            this.logButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipInfoLabel
            // 
            this.ipInfoLabel.AutoSize = true;
            this.ipInfoLabel.Location = new System.Drawing.Point(9, 13);
            this.ipInfoLabel.Name = "ipInfoLabel";
            this.ipInfoLabel.Size = new System.Drawing.Size(98, 13);
            this.ipInfoLabel.TabIndex = 0;
            this.ipInfoLabel.Text = "Connected to \"{0}\"";
            // 
            // pingLabel
            // 
            this.pingLabel.AutoSize = true;
            this.pingLabel.ForeColor = System.Drawing.Color.Red;
            this.pingLabel.Location = new System.Drawing.Point(189, 13);
            this.pingLabel.Name = "pingLabel";
            this.pingLabel.Size = new System.Drawing.Size(61, 13);
            this.pingLabel.TabIndex = 1;
            this.pingLabel.Text = "Ping: {0}ms";
            // 
            // statusArmedLabel
            // 
            this.statusArmedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusArmedLabel.AutoSize = true;
            this.statusArmedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusArmedLabel.Location = new System.Drawing.Point(430, 13);
            this.statusArmedLabel.Name = "statusArmedLabel";
            this.statusArmedLabel.Size = new System.Drawing.Size(57, 13);
            this.statusArmedLabel.TabIndex = 3;
            this.statusArmedLabel.Text = "Status: {0}";
            // 
            // armToogleButton
            // 
            this.armToogleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.armToogleButton.Location = new System.Drawing.Point(565, 8);
            this.armToogleButton.Name = "armToogleButton";
            this.armToogleButton.Size = new System.Drawing.Size(75, 23);
            this.armToogleButton.TabIndex = 4;
            this.armToogleButton.Text = "Arm";
            this.armToogleButton.UseVisualStyleBackColor = true;
            this.armToogleButton.Click += new System.EventHandler(this.armToogleButton_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            // 
            // calibrateGyroButton
            // 
            this.calibrateGyroButton.Location = new System.Drawing.Point(191, 140);
            this.calibrateGyroButton.Name = "calibrateGyroButton";
            this.calibrateGyroButton.Size = new System.Drawing.Size(23, 23);
            this.calibrateGyroButton.TabIndex = 6;
            this.calibrateGyroButton.Text = "0";
            this.calibrateGyroButton.UseVisualStyleBackColor = true;
            this.calibrateGyroButton.Click += new System.EventHandler(this.calibrateGyroButton_Click);
            // 
            // infoPropertyGrid
            // 
            this.infoPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoPropertyGrid.HelpVisible = false;
            this.infoPropertyGrid.Location = new System.Drawing.Point(424, 42);
            this.infoPropertyGrid.Name = "infoPropertyGrid";
            this.infoPropertyGrid.Size = new System.Drawing.Size(216, 299);
            this.infoPropertyGrid.TabIndex = 8;
            // 
            // headingIndicator
            // 
            this.headingIndicator.Location = new System.Drawing.Point(221, 139);
            this.headingIndicator.Name = "headingIndicator";
            this.headingIndicator.Size = new System.Drawing.Size(175, 175);
            this.headingIndicator.TabIndex = 7;
            this.headingIndicator.Text = "headingIndicatorInstrumentControl1";
            // 
            // artificialHorizon
            // 
            this.artificialHorizon.Location = new System.Drawing.Point(12, 140);
            this.artificialHorizon.Name = "artificialHorizon";
            this.artificialHorizon.Size = new System.Drawing.Size(175, 175);
            this.artificialHorizon.TabIndex = 5;
            this.artificialHorizon.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // motorControl1
            // 
            this.motorControl1.Location = new System.Drawing.Point(12, 42);
            this.motorControl1.Name = "motorControl1";
            this.motorControl1.Size = new System.Drawing.Size(364, 92);
            this.motorControl1.TabIndex = 2;
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(321, 8);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(75, 23);
            this.logButton.TabIndex = 9;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.DarkRed;
            this.stopButton.ForeColor = System.Drawing.Color.White;
            this.stopButton.Location = new System.Drawing.Point(565, 38);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 10;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 353);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.infoPropertyGrid);
            this.Controls.Add(this.headingIndicator);
            this.Controls.Add(this.calibrateGyroButton);
            this.Controls.Add(this.artificialHorizon);
            this.Controls.Add(this.armToogleButton);
            this.Controls.Add(this.statusArmedLabel);
            this.Controls.Add(this.motorControl1);
            this.Controls.Add(this.pingLabel);
            this.Controls.Add(this.ipInfoLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Drone Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipInfoLabel;
        private System.Windows.Forms.Label pingLabel;
        private MotorControl motorControl1;
        private System.Windows.Forms.Label statusArmedLabel;
        private System.Windows.Forms.Button armToogleButton;
        private Avionics.AttitudeIndicatorInstrumentControl artificialHorizon;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button calibrateGyroButton;
        private Avionics.HeadingIndicatorInstrumentControl headingIndicator;
        private System.Windows.Forms.PropertyGrid infoPropertyGrid;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button stopButton;
    }
}