namespace DroneControl {
    partial class FlightControl {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox deviceGroupBox;
            System.Windows.Forms.GroupBox dataGroupBox;
            System.Windows.Forms.GroupBox inputConfigGroupBox;
            System.Windows.Forms.Label maxThrustPositiveLabel;
            System.Windows.Forms.Label maxThrustNegativeLabel;
            System.Windows.Forms.Label rotationalOffsetLabel;
            System.Windows.Forms.Label rollOffsetLabel;
            System.Windows.Forms.Label pitchOffsetLabel;
            System.Windows.Forms.Label maxRollLabel;
            System.Windows.Forms.Label maxRotationalSpeedLabel;
            System.Windows.Forms.Label maxPitchLabel;
            this.searchDeviceButton = new System.Windows.Forms.Button();
            this.deviceBatteryLabel = new System.Windows.Forms.Label();
            this.deviceConnectionLabel = new System.Windows.Forms.Label();
            this.inputDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.rollLabel = new System.Windows.Forms.Label();
            this.pitchLabel = new System.Windows.Forms.Label();
            this.rotationalSpeedLabel = new System.Windows.Forms.Label();
            this.thrustLabel = new System.Windows.Forms.Label();
            this.pidDataLabel = new System.Windows.Forms.Label();
            this.thrustNegativeNumeric = new System.Windows.Forms.NumericUpDown();
            this.thrustPositiveNumeric = new System.Windows.Forms.NumericUpDown();
            this.deadZoneCheckBox = new System.Windows.Forms.CheckBox();
            this.pitchOffsetNumeric = new System.Windows.Forms.NumericUpDown();
            this.rollOffsetNumeric = new System.Windows.Forms.NumericUpDown();
            this.rotationalOffsetNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxRollNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxRotationalSpeedNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxPitchNumeric = new System.Windows.Forms.NumericUpDown();
            this.searchTimer = new System.Windows.Forms.Timer(this.components);
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            deviceGroupBox = new System.Windows.Forms.GroupBox();
            dataGroupBox = new System.Windows.Forms.GroupBox();
            inputConfigGroupBox = new System.Windows.Forms.GroupBox();
            maxThrustPositiveLabel = new System.Windows.Forms.Label();
            maxThrustNegativeLabel = new System.Windows.Forms.Label();
            rotationalOffsetLabel = new System.Windows.Forms.Label();
            rollOffsetLabel = new System.Windows.Forms.Label();
            pitchOffsetLabel = new System.Windows.Forms.Label();
            maxRollLabel = new System.Windows.Forms.Label();
            maxRotationalSpeedLabel = new System.Windows.Forms.Label();
            maxPitchLabel = new System.Windows.Forms.Label();
            deviceGroupBox.SuspendLayout();
            dataGroupBox.SuspendLayout();
            inputConfigGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thrustNegativeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thrustPositiveNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchOffsetNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollOffsetNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationalOffsetNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRollNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRotationalSpeedNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPitchNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // deviceGroupBox
            // 
            deviceGroupBox.Controls.Add(this.searchDeviceButton);
            deviceGroupBox.Controls.Add(this.deviceBatteryLabel);
            deviceGroupBox.Controls.Add(this.deviceConnectionLabel);
            deviceGroupBox.Controls.Add(this.inputDeviceComboBox);
            deviceGroupBox.Location = new System.Drawing.Point(10, 14);
            deviceGroupBox.Name = "deviceGroupBox";
            deviceGroupBox.Size = new System.Drawing.Size(215, 154);
            deviceGroupBox.TabIndex = 25;
            deviceGroupBox.TabStop = false;
            deviceGroupBox.Text = "Input Device";
            // 
            // searchDeviceButton
            // 
            this.searchDeviceButton.Location = new System.Drawing.Point(11, 21);
            this.searchDeviceButton.Name = "searchDeviceButton";
            this.searchDeviceButton.Size = new System.Drawing.Size(75, 23);
            this.searchDeviceButton.TabIndex = 12;
            this.searchDeviceButton.Text = "Search";
            this.searchDeviceButton.UseVisualStyleBackColor = true;
            this.searchDeviceButton.Click += new System.EventHandler(this.searchDeviceButton_Click);
            // 
            // deviceBatteryLabel
            // 
            this.deviceBatteryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deviceBatteryLabel.AutoSize = true;
            this.deviceBatteryLabel.Location = new System.Drawing.Point(6, 133);
            this.deviceBatteryLabel.Name = "deviceBatteryLabel";
            this.deviceBatteryLabel.Size = new System.Drawing.Size(76, 13);
            this.deviceBatteryLabel.TabIndex = 11;
            this.deviceBatteryLabel.Text = "Device battery";
            // 
            // deviceConnectionLabel
            // 
            this.deviceConnectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deviceConnectionLabel.AutoSize = true;
            this.deviceConnectionLabel.Location = new System.Drawing.Point(6, 120);
            this.deviceConnectionLabel.Name = "deviceConnectionLabel";
            this.deviceConnectionLabel.Size = new System.Drawing.Size(95, 13);
            this.deviceConnectionLabel.TabIndex = 10;
            this.deviceConnectionLabel.Text = "Device connected";
            // 
            // inputDeviceComboBox
            // 
            this.inputDeviceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputDeviceComboBox.FormattingEnabled = true;
            this.inputDeviceComboBox.Location = new System.Drawing.Point(11, 50);
            this.inputDeviceComboBox.Name = "inputDeviceComboBox";
            this.inputDeviceComboBox.Size = new System.Drawing.Size(198, 21);
            this.inputDeviceComboBox.Sorted = true;
            this.inputDeviceComboBox.TabIndex = 8;
            this.inputDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.inputDeviceComboBox_SelectedIndexChanged);
            // 
            // dataGroupBox
            // 
            dataGroupBox.Controls.Add(this.rollLabel);
            dataGroupBox.Controls.Add(this.pitchLabel);
            dataGroupBox.Controls.Add(this.rotationalSpeedLabel);
            dataGroupBox.Controls.Add(this.thrustLabel);
            dataGroupBox.Controls.Add(this.pidDataLabel);
            dataGroupBox.Location = new System.Drawing.Point(231, 14);
            dataGroupBox.Name = "dataGroupBox";
            dataGroupBox.Size = new System.Drawing.Size(223, 154);
            dataGroupBox.TabIndex = 26;
            dataGroupBox.TabStop = false;
            dataGroupBox.Text = "Data";
            // 
            // rollLabel
            // 
            this.rollLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rollLabel.AutoSize = true;
            this.rollLabel.Font = new System.Drawing.Font("Consolas", 9F);
            this.rollLabel.Location = new System.Drawing.Point(90, 33);
            this.rollLabel.Name = "rollLabel";
            this.rollLabel.Size = new System.Drawing.Size(70, 14);
            this.rollLabel.TabIndex = 18;
            this.rollLabel.Text = "Roll: {0}";
            // 
            // pitchLabel
            // 
            this.pitchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pitchLabel.AutoSize = true;
            this.pitchLabel.Font = new System.Drawing.Font("Consolas", 9F);
            this.pitchLabel.Location = new System.Drawing.Point(83, 21);
            this.pitchLabel.Name = "pitchLabel";
            this.pitchLabel.Size = new System.Drawing.Size(77, 14);
            this.pitchLabel.TabIndex = 17;
            this.pitchLabel.Text = "Pitch: {0}";
            // 
            // rotationalSpeedLabel
            // 
            this.rotationalSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rotationalSpeedLabel.AutoSize = true;
            this.rotationalSpeedLabel.Font = new System.Drawing.Font("Consolas", 9F);
            this.rotationalSpeedLabel.Location = new System.Drawing.Point(6, 47);
            this.rotationalSpeedLabel.Name = "rotationalSpeedLabel";
            this.rotationalSpeedLabel.Size = new System.Drawing.Size(154, 14);
            this.rotationalSpeedLabel.TabIndex = 19;
            this.rotationalSpeedLabel.Text = "Rotational Speed: {0}";
            // 
            // thrustLabel
            // 
            this.thrustLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thrustLabel.AutoSize = true;
            this.thrustLabel.Font = new System.Drawing.Font("Consolas", 9F);
            this.thrustLabel.Location = new System.Drawing.Point(76, 61);
            this.thrustLabel.Name = "thrustLabel";
            this.thrustLabel.Size = new System.Drawing.Size(84, 14);
            this.thrustLabel.TabIndex = 20;
            this.thrustLabel.Text = "Thrust: {0}";
            // 
            // pidDataLabel
            // 
            this.pidDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pidDataLabel.AutoSize = true;
            this.pidDataLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pidDataLabel.Location = new System.Drawing.Point(6, 87);
            this.pidDataLabel.Name = "pidDataLabel";
            this.pidDataLabel.Size = new System.Drawing.Size(55, 13);
            this.pidDataLabel.TabIndex = 21;
            this.pidDataLabel.Text = "PID data";
            // 
            // inputConfigGroupBox
            // 
            inputConfigGroupBox.Controls.Add(maxThrustPositiveLabel);
            inputConfigGroupBox.Controls.Add(maxThrustNegativeLabel);
            inputConfigGroupBox.Controls.Add(this.thrustNegativeNumeric);
            inputConfigGroupBox.Controls.Add(this.thrustPositiveNumeric);
            inputConfigGroupBox.Controls.Add(this.deadZoneCheckBox);
            inputConfigGroupBox.Controls.Add(rotationalOffsetLabel);
            inputConfigGroupBox.Controls.Add(rollOffsetLabel);
            inputConfigGroupBox.Controls.Add(pitchOffsetLabel);
            inputConfigGroupBox.Controls.Add(this.pitchOffsetNumeric);
            inputConfigGroupBox.Controls.Add(this.rollOffsetNumeric);
            inputConfigGroupBox.Controls.Add(this.rotationalOffsetNumeric);
            inputConfigGroupBox.Controls.Add(maxRollLabel);
            inputConfigGroupBox.Controls.Add(this.maxRollNumeric);
            inputConfigGroupBox.Controls.Add(maxRotationalSpeedLabel);
            inputConfigGroupBox.Controls.Add(this.maxRotationalSpeedNumeric);
            inputConfigGroupBox.Controls.Add(maxPitchLabel);
            inputConfigGroupBox.Controls.Add(this.maxPitchNumeric);
            inputConfigGroupBox.Location = new System.Drawing.Point(10, 174);
            inputConfigGroupBox.Name = "inputConfigGroupBox";
            inputConfigGroupBox.Size = new System.Drawing.Size(444, 152);
            inputConfigGroupBox.TabIndex = 27;
            inputConfigGroupBox.TabStop = false;
            inputConfigGroupBox.Text = "Input Config";
            // 
            // maxThrustPositiveLabel
            // 
            maxThrustPositiveLabel.AutoSize = true;
            maxThrustPositiveLabel.Location = new System.Drawing.Point(304, 63);
            maxThrustPositiveLabel.Name = "maxThrustPositiveLabel";
            maxThrustPositiveLabel.Size = new System.Drawing.Size(69, 13);
            maxThrustPositiveLabel.TabIndex = 40;
            maxThrustPositiveLabel.Text = "Max Thrust +";
            // 
            // maxThrustNegativeLabel
            // 
            maxThrustNegativeLabel.AutoSize = true;
            maxThrustNegativeLabel.Location = new System.Drawing.Point(304, 102);
            maxThrustNegativeLabel.Name = "maxThrustNegativeLabel";
            maxThrustNegativeLabel.Size = new System.Drawing.Size(66, 13);
            maxThrustNegativeLabel.TabIndex = 39;
            maxThrustNegativeLabel.Text = "Max Thrust -";
            // 
            // thrustNegativeNumeric
            // 
            this.thrustNegativeNumeric.DecimalPlaces = 2;
            this.thrustNegativeNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.thrustNegativeNumeric.Location = new System.Drawing.Point(307, 118);
            this.thrustNegativeNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thrustNegativeNumeric.Name = "thrustNegativeNumeric";
            this.thrustNegativeNumeric.Size = new System.Drawing.Size(47, 20);
            this.thrustNegativeNumeric.TabIndex = 38;
            this.thrustNegativeNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thrustNegativeNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // thrustPositiveNumeric
            // 
            this.thrustPositiveNumeric.DecimalPlaces = 2;
            this.thrustPositiveNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.thrustPositiveNumeric.Location = new System.Drawing.Point(307, 79);
            this.thrustPositiveNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.thrustPositiveNumeric.Name = "thrustPositiveNumeric";
            this.thrustPositiveNumeric.Size = new System.Drawing.Size(47, 20);
            this.thrustPositiveNumeric.TabIndex = 37;
            this.thrustPositiveNumeric.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.thrustPositiveNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // deadZoneCheckBox
            // 
            this.deadZoneCheckBox.AutoSize = true;
            this.deadZoneCheckBox.Checked = true;
            this.deadZoneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deadZoneCheckBox.Location = new System.Drawing.Point(288, 41);
            this.deadZoneCheckBox.Name = "deadZoneCheckBox";
            this.deadZoneCheckBox.Size = new System.Drawing.Size(80, 17);
            this.deadZoneCheckBox.TabIndex = 36;
            this.deadZoneCheckBox.Text = "Dead Zone";
            this.deadZoneCheckBox.UseVisualStyleBackColor = true;
            this.deadZoneCheckBox.CheckedChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // rotationalOffsetLabel
            // 
            rotationalOffsetLabel.AutoSize = true;
            rotationalOffsetLabel.Location = new System.Drawing.Point(168, 102);
            rotationalOffsetLabel.Name = "rotationalOffsetLabel";
            rotationalOffsetLabel.Size = new System.Drawing.Size(107, 13);
            rotationalOffsetLabel.TabIndex = 35;
            rotationalOffsetLabel.Text = "Rotational offset [°/s]";
            // 
            // rollOffsetLabel
            // 
            rollOffsetLabel.AutoSize = true;
            rollOffsetLabel.Location = new System.Drawing.Point(168, 63);
            rollOffsetLabel.Name = "rollOffsetLabel";
            rollOffsetLabel.Size = new System.Drawing.Size(67, 13);
            rollOffsetLabel.TabIndex = 34;
            rollOffsetLabel.Text = "Roll offset [°]";
            // 
            // pitchOffsetLabel
            // 
            pitchOffsetLabel.AutoSize = true;
            pitchOffsetLabel.Location = new System.Drawing.Point(168, 25);
            pitchOffsetLabel.Name = "pitchOffsetLabel";
            pitchOffsetLabel.Size = new System.Drawing.Size(73, 13);
            pitchOffsetLabel.TabIndex = 33;
            pitchOffsetLabel.Text = "Pitch offset [°]";
            // 
            // pitchOffsetNumeric
            // 
            this.pitchOffsetNumeric.DecimalPlaces = 3;
            this.pitchOffsetNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.pitchOffsetNumeric.Location = new System.Drawing.Point(171, 41);
            this.pitchOffsetNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.pitchOffsetNumeric.Name = "pitchOffsetNumeric";
            this.pitchOffsetNumeric.Size = new System.Drawing.Size(59, 20);
            this.pitchOffsetNumeric.TabIndex = 32;
            this.pitchOffsetNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // rollOffsetNumeric
            // 
            this.rollOffsetNumeric.DecimalPlaces = 3;
            this.rollOffsetNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rollOffsetNumeric.Location = new System.Drawing.Point(171, 79);
            this.rollOffsetNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.rollOffsetNumeric.Name = "rollOffsetNumeric";
            this.rollOffsetNumeric.Size = new System.Drawing.Size(59, 20);
            this.rollOffsetNumeric.TabIndex = 31;
            this.rollOffsetNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // rotationalOffsetNumeric
            // 
            this.rotationalOffsetNumeric.DecimalPlaces = 3;
            this.rotationalOffsetNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rotationalOffsetNumeric.Location = new System.Drawing.Point(171, 118);
            this.rotationalOffsetNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.rotationalOffsetNumeric.Name = "rotationalOffsetNumeric";
            this.rotationalOffsetNumeric.Size = new System.Drawing.Size(59, 20);
            this.rotationalOffsetNumeric.TabIndex = 30;
            this.rotationalOffsetNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // maxRollLabel
            // 
            maxRollLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            maxRollLabel.AutoSize = true;
            maxRollLabel.Location = new System.Drawing.Point(8, 64);
            maxRollLabel.Name = "maxRollLabel";
            maxRollLabel.Size = new System.Drawing.Size(61, 13);
            maxRollLabel.TabIndex = 15;
            maxRollLabel.Text = "Max Roll [°]";
            // 
            // maxRollNumeric
            // 
            this.maxRollNumeric.DecimalPlaces = 1;
            this.maxRollNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxRollNumeric.Location = new System.Drawing.Point(11, 79);
            this.maxRollNumeric.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.maxRollNumeric.Name = "maxRollNumeric";
            this.maxRollNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxRollNumeric.TabIndex = 29;
            this.maxRollNumeric.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxRollNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // maxRotationalSpeedLabel
            // 
            maxRotationalSpeedLabel.AutoSize = true;
            maxRotationalSpeedLabel.Location = new System.Drawing.Point(8, 102);
            maxRotationalSpeedLabel.Name = "maxRotationalSpeedLabel";
            maxRotationalSpeedLabel.Size = new System.Drawing.Size(135, 13);
            maxRotationalSpeedLabel.TabIndex = 28;
            maxRotationalSpeedLabel.Text = "Max Rotational Speed [°/s]";
            // 
            // maxRotationalSpeedNumeric
            // 
            this.maxRotationalSpeedNumeric.DecimalPlaces = 1;
            this.maxRotationalSpeedNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxRotationalSpeedNumeric.Location = new System.Drawing.Point(11, 118);
            this.maxRotationalSpeedNumeric.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.maxRotationalSpeedNumeric.Name = "maxRotationalSpeedNumeric";
            this.maxRotationalSpeedNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxRotationalSpeedNumeric.TabIndex = 27;
            this.maxRotationalSpeedNumeric.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.maxRotationalSpeedNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // maxPitchLabel
            // 
            maxPitchLabel.AutoSize = true;
            maxPitchLabel.Location = new System.Drawing.Point(8, 25);
            maxPitchLabel.Name = "maxPitchLabel";
            maxPitchLabel.Size = new System.Drawing.Size(67, 13);
            maxPitchLabel.TabIndex = 26;
            maxPitchLabel.Text = "Max Pitch [°]";
            // 
            // maxPitchNumeric
            // 
            this.maxPitchNumeric.DecimalPlaces = 1;
            this.maxPitchNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxPitchNumeric.Location = new System.Drawing.Point(11, 41);
            this.maxPitchNumeric.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.maxPitchNumeric.Name = "maxPitchNumeric";
            this.maxPitchNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxPitchNumeric.TabIndex = 25;
            this.maxPitchNumeric.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxPitchNumeric.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // searchTimer
            // 
            this.searchTimer.Enabled = true;
            this.searchTimer.Interval = 1000;
            this.searchTimer.Tick += new System.EventHandler(this.searchTimer_Tick);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 16;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // FlightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(inputConfigGroupBox);
            this.Controls.Add(dataGroupBox);
            this.Controls.Add(deviceGroupBox);
            this.DoubleBuffered = true;
            this.Name = "FlightControl";
            this.Size = new System.Drawing.Size(457, 360);
            deviceGroupBox.ResumeLayout(false);
            deviceGroupBox.PerformLayout();
            dataGroupBox.ResumeLayout(false);
            dataGroupBox.PerformLayout();
            inputConfigGroupBox.ResumeLayout(false);
            inputConfigGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thrustNegativeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thrustPositiveNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchOffsetNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollOffsetNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationalOffsetNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRollNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRotationalSpeedNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPitchNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox inputDeviceComboBox;
        private System.Windows.Forms.Label pitchLabel;
        private System.Windows.Forms.Label rollLabel;
        private System.Windows.Forms.Label rotationalSpeedLabel;
        private System.Windows.Forms.Label thrustLabel;
        private System.Windows.Forms.Label pidDataLabel;
        private System.Windows.Forms.Button searchDeviceButton;
        private System.Windows.Forms.Label deviceBatteryLabel;
        private System.Windows.Forms.Label deviceConnectionLabel;
        private System.Windows.Forms.NumericUpDown maxPitchNumeric;
        private System.Windows.Forms.NumericUpDown maxRotationalSpeedNumeric;
        private System.Windows.Forms.NumericUpDown maxRollNumeric;
        private System.Windows.Forms.NumericUpDown rotationalOffsetNumeric;
        private System.Windows.Forms.NumericUpDown rollOffsetNumeric;
        private System.Windows.Forms.NumericUpDown pitchOffsetNumeric;
        private System.Windows.Forms.Timer searchTimer;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.CheckBox deadZoneCheckBox;
        private System.Windows.Forms.NumericUpDown thrustNegativeNumeric;
        private System.Windows.Forms.NumericUpDown thrustPositiveNumeric;
    }
}
