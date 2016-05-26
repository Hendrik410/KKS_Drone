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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label invertLabel;
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
            this.thrustTextBox = new System.Windows.Forms.NumericUpDown();
            this.invertRotationalCheckBox = new System.Windows.Forms.CheckBox();
            this.invertRollCheckBox = new System.Windows.Forms.CheckBox();
            this.invertPitchCheckBox = new System.Windows.Forms.CheckBox();
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
            label1 = new System.Windows.Forms.Label();
            invertLabel = new System.Windows.Forms.Label();
            rotationalOffsetLabel = new System.Windows.Forms.Label();
            rollOffsetLabel = new System.Windows.Forms.Label();
            pitchOffsetLabel = new System.Windows.Forms.Label();
            maxRollLabel = new System.Windows.Forms.Label();
            maxRotationalSpeedLabel = new System.Windows.Forms.Label();
            maxPitchLabel = new System.Windows.Forms.Label();
            deviceGroupBox.SuspendLayout();
            dataGroupBox.SuspendLayout();
            inputConfigGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thrustTextBox)).BeginInit();
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
            this.rollLabel.Location = new System.Drawing.Point(90, 19);
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
            this.pitchLabel.Location = new System.Drawing.Point(83, 33);
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
            this.pidDataLabel.Font = new System.Drawing.Font("Consolas", 9F);
            this.pidDataLabel.Location = new System.Drawing.Point(6, 87);
            this.pidDataLabel.Name = "pidDataLabel";
            this.pidDataLabel.Size = new System.Drawing.Size(63, 14);
            this.pidDataLabel.TabIndex = 21;
            this.pidDataLabel.Text = "PID data";
            // 
            // inputConfigGroupBox
            // 
            inputConfigGroupBox.Controls.Add(label1);
            inputConfigGroupBox.Controls.Add(this.thrustTextBox);
            inputConfigGroupBox.Controls.Add(this.invertRotationalCheckBox);
            inputConfigGroupBox.Controls.Add(this.invertRollCheckBox);
            inputConfigGroupBox.Controls.Add(this.invertPitchCheckBox);
            inputConfigGroupBox.Controls.Add(invertLabel);
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
            inputConfigGroupBox.Size = new System.Drawing.Size(444, 169);
            inputConfigGroupBox.TabIndex = 27;
            inputConfigGroupBox.TabStop = false;
            inputConfigGroupBox.Text = "Input Config";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(285, 102);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 13);
            label1.TabIndex = 46;
            label1.Text = "Thrust";
            // 
            // thrustTextBox
            // 
            this.thrustTextBox.Location = new System.Drawing.Point(288, 118);
            this.thrustTextBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.thrustTextBox.Name = "thrustTextBox";
            this.thrustTextBox.Size = new System.Drawing.Size(59, 20);
            this.thrustTextBox.TabIndex = 45;
            this.thrustTextBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.thrustTextBox.ValueChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // invertRotationalCheckBox
            // 
            this.invertRotationalCheckBox.AutoSize = true;
            this.invertRotationalCheckBox.Location = new System.Drawing.Point(156, 145);
            this.invertRotationalCheckBox.Name = "invertRotationalCheckBox";
            this.invertRotationalCheckBox.Size = new System.Drawing.Size(74, 17);
            this.invertRotationalCheckBox.TabIndex = 44;
            this.invertRotationalCheckBox.Text = "Rotational";
            this.invertRotationalCheckBox.UseVisualStyleBackColor = true;
            this.invertRotationalCheckBox.CheckedChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // invertRollCheckBox
            // 
            this.invertRollCheckBox.AutoSize = true;
            this.invertRollCheckBox.Location = new System.Drawing.Point(107, 145);
            this.invertRollCheckBox.Name = "invertRollCheckBox";
            this.invertRollCheckBox.Size = new System.Drawing.Size(44, 17);
            this.invertRollCheckBox.TabIndex = 43;
            this.invertRollCheckBox.Text = "Roll";
            this.invertRollCheckBox.UseVisualStyleBackColor = true;
            this.invertRollCheckBox.CheckedChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // invertPitchCheckBox
            // 
            this.invertPitchCheckBox.AutoSize = true;
            this.invertPitchCheckBox.Location = new System.Drawing.Point(51, 145);
            this.invertPitchCheckBox.Name = "invertPitchCheckBox";
            this.invertPitchCheckBox.Size = new System.Drawing.Size(50, 17);
            this.invertPitchCheckBox.TabIndex = 42;
            this.invertPitchCheckBox.Text = "Pitch";
            this.invertPitchCheckBox.UseVisualStyleBackColor = true;
            this.invertPitchCheckBox.CheckedChanged += new System.EventHandler(this.OnInputConfigChange);
            // 
            // invertLabel
            // 
            invertLabel.AutoSize = true;
            invertLabel.Location = new System.Drawing.Point(11, 145);
            invertLabel.Name = "invertLabel";
            invertLabel.Size = new System.Drawing.Size(34, 13);
            invertLabel.TabIndex = 41;
            invertLabel.Text = "Invert";
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
            5,
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
            ((System.ComponentModel.ISupportInitialize)(this.thrustTextBox)).EndInit();
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
        private System.Windows.Forms.CheckBox invertRotationalCheckBox;
        private System.Windows.Forms.CheckBox invertRollCheckBox;
        private System.Windows.Forms.CheckBox invertPitchCheckBox;
        private System.Windows.Forms.NumericUpDown thrustTextBox;
    }
}
