namespace DroneControl
{
    partial class SensorControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox sensorGroupBox;
            this.gyroDataLabel = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.accelerationLabel = new System.Windows.Forms.Label();
            this.calibrateGyroButton = new System.Windows.Forms.Button();
            this.batteryVoltageLabel = new System.Windows.Forms.Label();
            this.headingIndicator = new DroneControl.Avionics.HeadingIndicatorInstrumentControl();
            this.artificialHorizon = new DroneControl.Avionics.AttitudeIndicatorInstrumentControl();
            sensorGroupBox = new System.Windows.Forms.GroupBox();
            sensorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // sensorGroupBox
            // 
            sensorGroupBox.Controls.Add(this.batteryVoltageLabel);
            sensorGroupBox.Controls.Add(this.gyroDataLabel);
            sensorGroupBox.Controls.Add(this.temperatureLabel);
            sensorGroupBox.Controls.Add(this.accelerationLabel);
            sensorGroupBox.Controls.Add(this.headingIndicator);
            sensorGroupBox.Controls.Add(this.calibrateGyroButton);
            sensorGroupBox.Controls.Add(this.artificialHorizon);
            sensorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            sensorGroupBox.Location = new System.Drawing.Point(0, 0);
            sensorGroupBox.Name = "sensorGroupBox";
            sensorGroupBox.Size = new System.Drawing.Size(410, 262);
            sensorGroupBox.TabIndex = 0;
            sensorGroupBox.TabStop = false;
            sensorGroupBox.Text = "Sensors";
            // 
            // gyroDataLabel
            // 
            this.gyroDataLabel.AutoSize = true;
            this.gyroDataLabel.Location = new System.Drawing.Point(6, 201);
            this.gyroDataLabel.Name = "gyroDataLabel";
            this.gyroDataLabel.Size = new System.Drawing.Size(53, 13);
            this.gyroDataLabel.TabIndex = 19;
            this.gyroDataLabel.Text = "Gyro data";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(6, 223);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(67, 13);
            this.temperatureLabel.TabIndex = 18;
            this.temperatureLabel.Text = "Temperature";
            // 
            // accelerationLabel
            // 
            this.accelerationLabel.AutoSize = true;
            this.accelerationLabel.Location = new System.Drawing.Point(212, 201);
            this.accelerationLabel.Name = "accelerationLabel";
            this.accelerationLabel.Size = new System.Drawing.Size(66, 13);
            this.accelerationLabel.TabIndex = 17;
            this.accelerationLabel.Text = "Acceleration";
            // 
            // calibrateGyroButton
            // 
            this.calibrateGyroButton.Location = new System.Drawing.Point(186, 19);
            this.calibrateGyroButton.Name = "calibrateGyroButton";
            this.calibrateGyroButton.Size = new System.Drawing.Size(23, 23);
            this.calibrateGyroButton.TabIndex = 15;
            this.calibrateGyroButton.Text = "0";
            this.calibrateGyroButton.UseVisualStyleBackColor = true;
            this.calibrateGyroButton.Click += new System.EventHandler(this.calibrateGyroButton_Click);
            // 
            // batteryVoltageLabel
            // 
            this.batteryVoltageLabel.AutoSize = true;
            this.batteryVoltageLabel.Location = new System.Drawing.Point(212, 223);
            this.batteryVoltageLabel.Name = "batteryVoltageLabel";
            this.batteryVoltageLabel.Size = new System.Drawing.Size(78, 13);
            this.batteryVoltageLabel.TabIndex = 20;
            this.batteryVoltageLabel.Text = "Battery voltage";
            // 
            // headingIndicator
            // 
            this.headingIndicator.Location = new System.Drawing.Point(215, 18);
            this.headingIndicator.Name = "headingIndicator";
            this.headingIndicator.Size = new System.Drawing.Size(175, 175);
            this.headingIndicator.TabIndex = 16;
            this.headingIndicator.Text = "headingIndicatorInstrumentControl1";
            // 
            // artificialHorizon
            // 
            this.artificialHorizon.Location = new System.Drawing.Point(6, 19);
            this.artificialHorizon.Name = "artificialHorizon";
            this.artificialHorizon.Size = new System.Drawing.Size(175, 175);
            this.artificialHorizon.TabIndex = 14;
            this.artificialHorizon.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // SensorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(sensorGroupBox);
            this.Name = "SensorControl";
            this.Size = new System.Drawing.Size(410, 262);
            sensorGroupBox.ResumeLayout(false);
            sensorGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label accelerationLabel;
        private Avionics.HeadingIndicatorInstrumentControl headingIndicator;
        private System.Windows.Forms.Button calibrateGyroButton;
        private Avionics.AttitudeIndicatorInstrumentControl artificialHorizon;
        private System.Windows.Forms.Label gyroDataLabel;
        private System.Windows.Forms.Label batteryVoltageLabel;
    }
}
