namespace DroneControl
{
    partial class SettingsForm
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
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label modelLabel;
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.GroupBox firmwareGroupBox;
            System.Windows.Forms.Label firmwareVersionLabel;
            System.Windows.Forms.Label buildDateLabel;
            System.Windows.Forms.TabPage quadrocopterPage;
            System.Windows.Forms.GroupBox hardwareGroupBox;
            System.Windows.Forms.Label magnetometerLabel;
            System.Windows.Forms.Label gyroSensorLabel;
            System.Windows.Forms.GroupBox motorsGroupBox;
            System.Windows.Forms.Label minValueLabel;
            System.Windows.Forms.Label hoverValueLabel;
            System.Windows.Forms.Label maxValueLabel;
            System.Windows.Forms.Label idleValueLabel;
            System.Windows.Forms.GroupBox safetyGroupBox;
            System.Windows.Forms.Label safeMotorValueLabel;
            System.Windows.Forms.Label safeRollLabel;
            System.Windows.Forms.Label safeTemperatureLabel;
            System.Windows.Forms.Label safePitchLabel;
            System.Windows.Forms.GroupBox pidPitchGroupBox;
            System.Windows.Forms.Label pitchKpLabel;
            System.Windows.Forms.Label pitchKiLabel;
            System.Windows.Forms.Label pitchKdLabel;
            System.Windows.Forms.GroupBox pidRollGroupBox;
            System.Windows.Forms.Label rollKpLabel;
            System.Windows.Forms.Label rollKiLabel;
            System.Windows.Forms.Label rollKdLabel;
            System.Windows.Forms.GroupBox pidYawGroupBox;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label thrust;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.updateFirmwareButton = new System.Windows.Forms.Button();
            this.firmwareVersionTextBox = new System.Windows.Forms.TextBox();
            this.buildDateTextBox = new System.Windows.Forms.TextBox();
            this.saveConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.restartButton = new System.Windows.Forms.Button();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.magnetometerTextBox = new System.Windows.Forms.TextBox();
            this.gyroSensorTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.minValueTextBox = new System.Windows.Forms.TextBox();
            this.hoverValueTextBox = new System.Windows.Forms.TextBox();
            this.idleValueTextBox = new System.Windows.Forms.TextBox();
            this.maxValueTextBox = new System.Windows.Forms.TextBox();
            this.safeMotorValueTextBox = new System.Windows.Forms.TextBox();
            this.safeRollTextBox = new System.Windows.Forms.TextBox();
            this.safePitchTextBox = new System.Windows.Forms.TextBox();
            this.safeTemperatureTextBox = new System.Windows.Forms.TextBox();
            this.pitchKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.pitchKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.pitchKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.rollKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.rollKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.rollKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.flyingPage = new System.Windows.Forms.TabPage();
            this.thrustValue = new System.Windows.Forms.NumericUpDown();
            this.applyButton = new System.Windows.Forms.Button();
            this.enableStabilizationCheckBox = new System.Windows.Forms.CheckBox();
            this.negativeMixingCheckBox = new System.Windows.Forms.CheckBox();
            this.keepMotorsOnCheckBox = new System.Windows.Forms.CheckBox();
            nameLabel = new System.Windows.Forms.Label();
            modelLabel = new System.Windows.Forms.Label();
            idLabel = new System.Windows.Forms.Label();
            firmwareGroupBox = new System.Windows.Forms.GroupBox();
            firmwareVersionLabel = new System.Windows.Forms.Label();
            buildDateLabel = new System.Windows.Forms.Label();
            quadrocopterPage = new System.Windows.Forms.TabPage();
            hardwareGroupBox = new System.Windows.Forms.GroupBox();
            magnetometerLabel = new System.Windows.Forms.Label();
            gyroSensorLabel = new System.Windows.Forms.Label();
            motorsGroupBox = new System.Windows.Forms.GroupBox();
            minValueLabel = new System.Windows.Forms.Label();
            hoverValueLabel = new System.Windows.Forms.Label();
            maxValueLabel = new System.Windows.Forms.Label();
            idleValueLabel = new System.Windows.Forms.Label();
            safetyGroupBox = new System.Windows.Forms.GroupBox();
            safeMotorValueLabel = new System.Windows.Forms.Label();
            safeRollLabel = new System.Windows.Forms.Label();
            safeTemperatureLabel = new System.Windows.Forms.Label();
            safePitchLabel = new System.Windows.Forms.Label();
            pidPitchGroupBox = new System.Windows.Forms.GroupBox();
            pitchKpLabel = new System.Windows.Forms.Label();
            pitchKiLabel = new System.Windows.Forms.Label();
            pitchKdLabel = new System.Windows.Forms.Label();
            pidRollGroupBox = new System.Windows.Forms.GroupBox();
            rollKpLabel = new System.Windows.Forms.Label();
            rollKiLabel = new System.Windows.Forms.Label();
            rollKdLabel = new System.Windows.Forms.Label();
            pidYawGroupBox = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            thrust = new System.Windows.Forms.Label();
            firmwareGroupBox.SuspendLayout();
            quadrocopterPage.SuspendLayout();
            hardwareGroupBox.SuspendLayout();
            motorsGroupBox.SuspendLayout();
            safetyGroupBox.SuspendLayout();
            pidPitchGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKpTextBox)).BeginInit();
            pidRollGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKpTextBox)).BeginInit();
            pidYawGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yawKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKpTextBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.flyingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thrustValue)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(20, 10);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(35, 13);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name";
            // 
            // modelLabel
            // 
            modelLabel.AutoSize = true;
            modelLabel.Location = new System.Drawing.Point(8, 22);
            modelLabel.Name = "modelLabel";
            modelLabel.Size = new System.Drawing.Size(36, 13);
            modelLabel.TabIndex = 2;
            modelLabel.Text = "Model";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(8, 48);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(18, 13);
            idLabel.TabIndex = 8;
            idLabel.Text = "ID";
            // 
            // firmwareGroupBox
            // 
            firmwareGroupBox.Controls.Add(this.updateFirmwareButton);
            firmwareGroupBox.Controls.Add(firmwareVersionLabel);
            firmwareGroupBox.Controls.Add(this.firmwareVersionTextBox);
            firmwareGroupBox.Controls.Add(buildDateLabel);
            firmwareGroupBox.Controls.Add(this.buildDateTextBox);
            firmwareGroupBox.Location = new System.Drawing.Point(11, 33);
            firmwareGroupBox.Name = "firmwareGroupBox";
            firmwareGroupBox.Size = new System.Drawing.Size(258, 108);
            firmwareGroupBox.TabIndex = 11;
            firmwareGroupBox.TabStop = false;
            firmwareGroupBox.Text = "Firmware";
            // 
            // updateFirmwareButton
            // 
            this.updateFirmwareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateFirmwareButton.Location = new System.Drawing.Point(144, 82);
            this.updateFirmwareButton.Name = "updateFirmwareButton";
            this.updateFirmwareButton.Size = new System.Drawing.Size(108, 20);
            this.updateFirmwareButton.TabIndex = 10;
            this.updateFirmwareButton.Text = "Update Firmware";
            this.updateFirmwareButton.UseVisualStyleBackColor = true;
            this.updateFirmwareButton.Click += new System.EventHandler(this.updateFirmwareButton_Click);
            // 
            // firmwareVersionLabel
            // 
            firmwareVersionLabel.AutoSize = true;
            firmwareVersionLabel.Location = new System.Drawing.Point(9, 18);
            firmwareVersionLabel.Name = "firmwareVersionLabel";
            firmwareVersionLabel.Size = new System.Drawing.Size(87, 13);
            firmwareVersionLabel.TabIndex = 4;
            firmwareVersionLabel.Text = "Firmware Version";
            // 
            // firmwareVersionTextBox
            // 
            this.firmwareVersionTextBox.Location = new System.Drawing.Point(110, 15);
            this.firmwareVersionTextBox.Name = "firmwareVersionTextBox";
            this.firmwareVersionTextBox.Size = new System.Drawing.Size(142, 20);
            this.firmwareVersionTextBox.TabIndex = 5;
            // 
            // buildDateLabel
            // 
            buildDateLabel.AutoSize = true;
            buildDateLabel.Location = new System.Drawing.Point(9, 44);
            buildDateLabel.Name = "buildDateLabel";
            buildDateLabel.Size = new System.Drawing.Size(56, 13);
            buildDateLabel.TabIndex = 6;
            buildDateLabel.Text = "Build Date";
            // 
            // buildDateTextBox
            // 
            this.buildDateTextBox.Location = new System.Drawing.Point(110, 41);
            this.buildDateTextBox.Name = "buildDateTextBox";
            this.buildDateTextBox.Size = new System.Drawing.Size(142, 20);
            this.buildDateTextBox.TabIndex = 7;
            // 
            // quadrocopterPage
            // 
            quadrocopterPage.Controls.Add(this.saveConfigCheckBox);
            quadrocopterPage.Controls.Add(hardwareGroupBox);
            quadrocopterPage.Controls.Add(firmwareGroupBox);
            quadrocopterPage.Controls.Add(this.nameTextBox);
            quadrocopterPage.Controls.Add(nameLabel);
            quadrocopterPage.Location = new System.Drawing.Point(4, 22);
            quadrocopterPage.Name = "quadrocopterPage";
            quadrocopterPage.Padding = new System.Windows.Forms.Padding(3);
            quadrocopterPage.Size = new System.Drawing.Size(698, 412);
            quadrocopterPage.TabIndex = 0;
            quadrocopterPage.Text = "Quadrocopter";
            // 
            // saveConfigCheckBox
            // 
            this.saveConfigCheckBox.AutoSize = true;
            this.saveConfigCheckBox.Location = new System.Drawing.Point(302, 10);
            this.saveConfigCheckBox.Name = "saveConfigCheckBox";
            this.saveConfigCheckBox.Size = new System.Drawing.Size(84, 17);
            this.saveConfigCheckBox.TabIndex = 17;
            this.saveConfigCheckBox.Text = "Save Config";
            this.saveConfigCheckBox.UseVisualStyleBackColor = true;
            // 
            // hardwareGroupBox
            // 
            hardwareGroupBox.Controls.Add(this.restartButton);
            hardwareGroupBox.Controls.Add(this.modelTextBox);
            hardwareGroupBox.Controls.Add(this.magnetometerTextBox);
            hardwareGroupBox.Controls.Add(modelLabel);
            hardwareGroupBox.Controls.Add(magnetometerLabel);
            hardwareGroupBox.Controls.Add(idLabel);
            hardwareGroupBox.Controls.Add(this.gyroSensorTextBox);
            hardwareGroupBox.Controls.Add(this.idTextBox);
            hardwareGroupBox.Controls.Add(gyroSensorLabel);
            hardwareGroupBox.Location = new System.Drawing.Point(11, 156);
            hardwareGroupBox.Name = "hardwareGroupBox";
            hardwareGroupBox.Size = new System.Drawing.Size(258, 161);
            hardwareGroupBox.TabIndex = 16;
            hardwareGroupBox.TabStop = false;
            hardwareGroupBox.Text = "Hardware";
            // 
            // restartButton
            // 
            this.restartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restartButton.Location = new System.Drawing.Point(144, 135);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(108, 20);
            this.restartButton.TabIndex = 11;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // modelTextBox
            // 
            this.modelTextBox.Location = new System.Drawing.Point(109, 19);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(142, 20);
            this.modelTextBox.TabIndex = 3;
            // 
            // magnetometerTextBox
            // 
            this.magnetometerTextBox.Location = new System.Drawing.Point(109, 98);
            this.magnetometerTextBox.Name = "magnetometerTextBox";
            this.magnetometerTextBox.Size = new System.Drawing.Size(142, 20);
            this.magnetometerTextBox.TabIndex = 15;
            // 
            // magnetometerLabel
            // 
            magnetometerLabel.AutoSize = true;
            magnetometerLabel.Location = new System.Drawing.Point(8, 101);
            magnetometerLabel.Name = "magnetometerLabel";
            magnetometerLabel.Size = new System.Drawing.Size(75, 13);
            magnetometerLabel.TabIndex = 14;
            magnetometerLabel.Text = "Magnetometer";
            // 
            // gyroSensorTextBox
            // 
            this.gyroSensorTextBox.Location = new System.Drawing.Point(109, 71);
            this.gyroSensorTextBox.Name = "gyroSensorTextBox";
            this.gyroSensorTextBox.Size = new System.Drawing.Size(142, 20);
            this.gyroSensorTextBox.TabIndex = 13;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(109, 45);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(142, 20);
            this.idTextBox.TabIndex = 9;
            // 
            // gyroSensorLabel
            // 
            gyroSensorLabel.AutoSize = true;
            gyroSensorLabel.Location = new System.Drawing.Point(8, 74);
            gyroSensorLabel.Name = "gyroSensorLabel";
            gyroSensorLabel.Size = new System.Drawing.Size(65, 13);
            gyroSensorLabel.TabIndex = 12;
            gyroSensorLabel.Text = "Gyro Sensor";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(121, 7);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(142, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // motorsGroupBox
            // 
            motorsGroupBox.Controls.Add(this.calibrateButton);
            motorsGroupBox.Controls.Add(this.minValueTextBox);
            motorsGroupBox.Controls.Add(this.hoverValueTextBox);
            motorsGroupBox.Controls.Add(minValueLabel);
            motorsGroupBox.Controls.Add(hoverValueLabel);
            motorsGroupBox.Controls.Add(maxValueLabel);
            motorsGroupBox.Controls.Add(this.idleValueTextBox);
            motorsGroupBox.Controls.Add(this.maxValueTextBox);
            motorsGroupBox.Controls.Add(idleValueLabel);
            motorsGroupBox.Location = new System.Drawing.Point(8, 6);
            motorsGroupBox.Name = "motorsGroupBox";
            motorsGroupBox.Size = new System.Drawing.Size(258, 162);
            motorsGroupBox.TabIndex = 17;
            motorsGroupBox.TabStop = false;
            motorsGroupBox.Text = "Motors";
            // 
            // calibrateButton
            // 
            this.calibrateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calibrateButton.Location = new System.Drawing.Point(144, 136);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(108, 20);
            this.calibrateButton.TabIndex = 11;
            this.calibrateButton.Text = "Calibrate";
            this.calibrateButton.UseVisualStyleBackColor = true;
            // 
            // minValueTextBox
            // 
            this.minValueTextBox.Location = new System.Drawing.Point(109, 19);
            this.minValueTextBox.Name = "minValueTextBox";
            this.minValueTextBox.Size = new System.Drawing.Size(142, 20);
            this.minValueTextBox.TabIndex = 3;
            // 
            // hoverValueTextBox
            // 
            this.hoverValueTextBox.Location = new System.Drawing.Point(109, 71);
            this.hoverValueTextBox.Name = "hoverValueTextBox";
            this.hoverValueTextBox.Size = new System.Drawing.Size(142, 20);
            this.hoverValueTextBox.TabIndex = 15;
            // 
            // minValueLabel
            // 
            minValueLabel.AutoSize = true;
            minValueLabel.Location = new System.Drawing.Point(8, 22);
            minValueLabel.Name = "minValueLabel";
            minValueLabel.Size = new System.Drawing.Size(54, 13);
            minValueLabel.TabIndex = 2;
            minValueLabel.Text = "Min Value";
            // 
            // hoverValueLabel
            // 
            hoverValueLabel.AutoSize = true;
            hoverValueLabel.Location = new System.Drawing.Point(8, 74);
            hoverValueLabel.Name = "hoverValueLabel";
            hoverValueLabel.Size = new System.Drawing.Size(66, 13);
            hoverValueLabel.TabIndex = 14;
            hoverValueLabel.Text = "Hover Value";
            // 
            // maxValueLabel
            // 
            maxValueLabel.AutoSize = true;
            maxValueLabel.Location = new System.Drawing.Point(9, 100);
            maxValueLabel.Name = "maxValueLabel";
            maxValueLabel.Size = new System.Drawing.Size(57, 13);
            maxValueLabel.TabIndex = 8;
            maxValueLabel.Text = "Max Value";
            // 
            // idleValueTextBox
            // 
            this.idleValueTextBox.Location = new System.Drawing.Point(109, 45);
            this.idleValueTextBox.Name = "idleValueTextBox";
            this.idleValueTextBox.Size = new System.Drawing.Size(143, 20);
            this.idleValueTextBox.TabIndex = 13;
            // 
            // maxValueTextBox
            // 
            this.maxValueTextBox.Location = new System.Drawing.Point(109, 97);
            this.maxValueTextBox.Name = "maxValueTextBox";
            this.maxValueTextBox.Size = new System.Drawing.Size(143, 20);
            this.maxValueTextBox.TabIndex = 9;
            // 
            // idleValueLabel
            // 
            idleValueLabel.AutoSize = true;
            idleValueLabel.Location = new System.Drawing.Point(9, 48);
            idleValueLabel.Name = "idleValueLabel";
            idleValueLabel.Size = new System.Drawing.Size(54, 13);
            idleValueLabel.TabIndex = 12;
            idleValueLabel.Text = "Idle Value";
            // 
            // safetyGroupBox
            // 
            safetyGroupBox.Controls.Add(this.safeMotorValueTextBox);
            safetyGroupBox.Controls.Add(this.safeRollTextBox);
            safetyGroupBox.Controls.Add(safeMotorValueLabel);
            safetyGroupBox.Controls.Add(safeRollLabel);
            safetyGroupBox.Controls.Add(safeTemperatureLabel);
            safetyGroupBox.Controls.Add(this.safePitchTextBox);
            safetyGroupBox.Controls.Add(this.safeTemperatureTextBox);
            safetyGroupBox.Controls.Add(safePitchLabel);
            safetyGroupBox.Location = new System.Drawing.Point(8, 174);
            safetyGroupBox.Name = "safetyGroupBox";
            safetyGroupBox.Size = new System.Drawing.Size(258, 129);
            safetyGroupBox.TabIndex = 18;
            safetyGroupBox.TabStop = false;
            safetyGroupBox.Text = "Safety";
            // 
            // safeMotorValueTextBox
            // 
            this.safeMotorValueTextBox.Location = new System.Drawing.Point(109, 19);
            this.safeMotorValueTextBox.Name = "safeMotorValueTextBox";
            this.safeMotorValueTextBox.Size = new System.Drawing.Size(142, 20);
            this.safeMotorValueTextBox.TabIndex = 3;
            // 
            // safeRollTextBox
            // 
            this.safeRollTextBox.Location = new System.Drawing.Point(109, 98);
            this.safeRollTextBox.Name = "safeRollTextBox";
            this.safeRollTextBox.Size = new System.Drawing.Size(142, 20);
            this.safeRollTextBox.TabIndex = 15;
            // 
            // safeMotorValueLabel
            // 
            safeMotorValueLabel.AutoSize = true;
            safeMotorValueLabel.Location = new System.Drawing.Point(8, 22);
            safeMotorValueLabel.Name = "safeMotorValueLabel";
            safeMotorValueLabel.Size = new System.Drawing.Size(89, 13);
            safeMotorValueLabel.TabIndex = 2;
            safeMotorValueLabel.Text = "Safe Motor Value";
            // 
            // safeRollLabel
            // 
            safeRollLabel.AutoSize = true;
            safeRollLabel.Location = new System.Drawing.Point(8, 101);
            safeRollLabel.Name = "safeRollLabel";
            safeRollLabel.Size = new System.Drawing.Size(50, 13);
            safeRollLabel.TabIndex = 14;
            safeRollLabel.Text = "Safe Roll";
            // 
            // safeTemperatureLabel
            // 
            safeTemperatureLabel.AutoSize = true;
            safeTemperatureLabel.Location = new System.Drawing.Point(8, 48);
            safeTemperatureLabel.Name = "safeTemperatureLabel";
            safeTemperatureLabel.Size = new System.Drawing.Size(92, 13);
            safeTemperatureLabel.TabIndex = 8;
            safeTemperatureLabel.Text = "Safe Temperature";
            // 
            // safePitchTextBox
            // 
            this.safePitchTextBox.Location = new System.Drawing.Point(109, 71);
            this.safePitchTextBox.Name = "safePitchTextBox";
            this.safePitchTextBox.Size = new System.Drawing.Size(142, 20);
            this.safePitchTextBox.TabIndex = 13;
            // 
            // safeTemperatureTextBox
            // 
            this.safeTemperatureTextBox.Location = new System.Drawing.Point(109, 45);
            this.safeTemperatureTextBox.Name = "safeTemperatureTextBox";
            this.safeTemperatureTextBox.Size = new System.Drawing.Size(142, 20);
            this.safeTemperatureTextBox.TabIndex = 9;
            // 
            // safePitchLabel
            // 
            safePitchLabel.AutoSize = true;
            safePitchLabel.Location = new System.Drawing.Point(8, 74);
            safePitchLabel.Name = "safePitchLabel";
            safePitchLabel.Size = new System.Drawing.Size(56, 13);
            safePitchLabel.TabIndex = 12;
            safePitchLabel.Text = "Safe Pitch";
            // 
            // pidPitchGroupBox
            // 
            pidPitchGroupBox.Controls.Add(this.pitchKdTextBox);
            pidPitchGroupBox.Controls.Add(this.pitchKiTextBox);
            pidPitchGroupBox.Controls.Add(this.pitchKpTextBox);
            pidPitchGroupBox.Controls.Add(pitchKpLabel);
            pidPitchGroupBox.Controls.Add(pitchKiLabel);
            pidPitchGroupBox.Controls.Add(pitchKdLabel);
            pidPitchGroupBox.Location = new System.Drawing.Point(272, 6);
            pidPitchGroupBox.Name = "pidPitchGroupBox";
            pidPitchGroupBox.Size = new System.Drawing.Size(117, 101);
            pidPitchGroupBox.TabIndex = 19;
            pidPitchGroupBox.TabStop = false;
            pidPitchGroupBox.Text = "PID Pitch";
            // 
            // pitchKdTextBox
            // 
            this.pitchKdTextBox.DecimalPlaces = 2;
            this.pitchKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.pitchKdTextBox.Name = "pitchKdTextBox";
            this.pitchKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.pitchKdTextBox.TabIndex = 15;
            // 
            // pitchKiTextBox
            // 
            this.pitchKiTextBox.DecimalPlaces = 2;
            this.pitchKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.pitchKiTextBox.Name = "pitchKiTextBox";
            this.pitchKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.pitchKiTextBox.TabIndex = 14;
            // 
            // pitchKpTextBox
            // 
            this.pitchKpTextBox.DecimalPlaces = 2;
            this.pitchKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.pitchKpTextBox.Name = "pitchKpTextBox";
            this.pitchKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.pitchKpTextBox.TabIndex = 13;
            // 
            // pitchKpLabel
            // 
            pitchKpLabel.AutoSize = true;
            pitchKpLabel.Location = new System.Drawing.Point(8, 22);
            pitchKpLabel.Name = "pitchKpLabel";
            pitchKpLabel.Size = new System.Drawing.Size(20, 13);
            pitchKpLabel.TabIndex = 2;
            pitchKpLabel.Text = "Kp";
            // 
            // pitchKiLabel
            // 
            pitchKiLabel.AutoSize = true;
            pitchKiLabel.Location = new System.Drawing.Point(8, 48);
            pitchKiLabel.Name = "pitchKiLabel";
            pitchKiLabel.Size = new System.Drawing.Size(16, 13);
            pitchKiLabel.TabIndex = 8;
            pitchKiLabel.Text = "Ki";
            // 
            // pitchKdLabel
            // 
            pitchKdLabel.AutoSize = true;
            pitchKdLabel.Location = new System.Drawing.Point(8, 74);
            pitchKdLabel.Name = "pitchKdLabel";
            pitchKdLabel.Size = new System.Drawing.Size(20, 13);
            pitchKdLabel.TabIndex = 12;
            pitchKdLabel.Text = "Kd";
            // 
            // pidRollGroupBox
            // 
            pidRollGroupBox.Controls.Add(this.rollKdTextBox);
            pidRollGroupBox.Controls.Add(this.rollKiTextBox);
            pidRollGroupBox.Controls.Add(this.rollKpTextBox);
            pidRollGroupBox.Controls.Add(rollKpLabel);
            pidRollGroupBox.Controls.Add(rollKiLabel);
            pidRollGroupBox.Controls.Add(rollKdLabel);
            pidRollGroupBox.Location = new System.Drawing.Point(395, 6);
            pidRollGroupBox.Name = "pidRollGroupBox";
            pidRollGroupBox.Size = new System.Drawing.Size(117, 101);
            pidRollGroupBox.TabIndex = 20;
            pidRollGroupBox.TabStop = false;
            pidRollGroupBox.Text = "PID Roll";
            // 
            // rollKdTextBox
            // 
            this.rollKdTextBox.DecimalPlaces = 2;
            this.rollKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.rollKdTextBox.Name = "rollKdTextBox";
            this.rollKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.rollKdTextBox.TabIndex = 15;
            // 
            // rollKiTextBox
            // 
            this.rollKiTextBox.DecimalPlaces = 2;
            this.rollKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.rollKiTextBox.Name = "rollKiTextBox";
            this.rollKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.rollKiTextBox.TabIndex = 14;
            // 
            // rollKpTextBox
            // 
            this.rollKpTextBox.DecimalPlaces = 2;
            this.rollKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.rollKpTextBox.Name = "rollKpTextBox";
            this.rollKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.rollKpTextBox.TabIndex = 13;
            // 
            // rollKpLabel
            // 
            rollKpLabel.AutoSize = true;
            rollKpLabel.Location = new System.Drawing.Point(8, 22);
            rollKpLabel.Name = "rollKpLabel";
            rollKpLabel.Size = new System.Drawing.Size(20, 13);
            rollKpLabel.TabIndex = 2;
            rollKpLabel.Text = "Kp";
            // 
            // rollKiLabel
            // 
            rollKiLabel.AutoSize = true;
            rollKiLabel.Location = new System.Drawing.Point(8, 48);
            rollKiLabel.Name = "rollKiLabel";
            rollKiLabel.Size = new System.Drawing.Size(16, 13);
            rollKiLabel.TabIndex = 8;
            rollKiLabel.Text = "Ki";
            // 
            // rollKdLabel
            // 
            rollKdLabel.AutoSize = true;
            rollKdLabel.Location = new System.Drawing.Point(8, 74);
            rollKdLabel.Name = "rollKdLabel";
            rollKdLabel.Size = new System.Drawing.Size(20, 13);
            rollKdLabel.TabIndex = 12;
            rollKdLabel.Text = "Kd";
            // 
            // pidYawGroupBox
            // 
            pidYawGroupBox.Controls.Add(this.yawKdTextBox);
            pidYawGroupBox.Controls.Add(this.yawKiTextBox);
            pidYawGroupBox.Controls.Add(this.yawKpTextBox);
            pidYawGroupBox.Controls.Add(label4);
            pidYawGroupBox.Controls.Add(label5);
            pidYawGroupBox.Controls.Add(label6);
            pidYawGroupBox.Location = new System.Drawing.Point(518, 6);
            pidYawGroupBox.Name = "pidYawGroupBox";
            pidYawGroupBox.Size = new System.Drawing.Size(117, 101);
            pidYawGroupBox.TabIndex = 21;
            pidYawGroupBox.TabStop = false;
            pidYawGroupBox.Text = "PID Yaw";
            // 
            // yawKdTextBox
            // 
            this.yawKdTextBox.DecimalPlaces = 2;
            this.yawKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.yawKdTextBox.Name = "yawKdTextBox";
            this.yawKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.yawKdTextBox.TabIndex = 15;
            // 
            // yawKiTextBox
            // 
            this.yawKiTextBox.DecimalPlaces = 2;
            this.yawKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.yawKiTextBox.Name = "yawKiTextBox";
            this.yawKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.yawKiTextBox.TabIndex = 14;
            // 
            // yawKpTextBox
            // 
            this.yawKpTextBox.DecimalPlaces = 2;
            this.yawKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.yawKpTextBox.Name = "yawKpTextBox";
            this.yawKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.yawKpTextBox.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 22);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(20, 13);
            label4.TabIndex = 2;
            label4.Text = "Kp";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 48);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(16, 13);
            label5.TabIndex = 8;
            label5.Text = "Ki";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(8, 74);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(20, 13);
            label6.TabIndex = 12;
            label6.Text = "Kd";
            // 
            // thrust
            // 
            thrust.AutoSize = true;
            thrust.Location = new System.Drawing.Point(280, 117);
            thrust.Name = "thrust";
            thrust.Size = new System.Drawing.Size(37, 13);
            thrust.TabIndex = 16;
            thrust.Text = "Thrust";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(quadrocopterPage);
            this.tabControl1.Controls.Add(this.flyingPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 438);
            this.tabControl1.TabIndex = 0;
            // 
            // flyingPage
            // 
            this.flyingPage.Controls.Add(this.keepMotorsOnCheckBox);
            this.flyingPage.Controls.Add(this.negativeMixingCheckBox);
            this.flyingPage.Controls.Add(this.enableStabilizationCheckBox);
            this.flyingPage.Controls.Add(this.thrustValue);
            this.flyingPage.Controls.Add(thrust);
            this.flyingPage.Controls.Add(pidYawGroupBox);
            this.flyingPage.Controls.Add(pidRollGroupBox);
            this.flyingPage.Controls.Add(pidPitchGroupBox);
            this.flyingPage.Controls.Add(safetyGroupBox);
            this.flyingPage.Controls.Add(motorsGroupBox);
            this.flyingPage.Location = new System.Drawing.Point(4, 22);
            this.flyingPage.Name = "flyingPage";
            this.flyingPage.Padding = new System.Windows.Forms.Padding(3);
            this.flyingPage.Size = new System.Drawing.Size(698, 412);
            this.flyingPage.TabIndex = 1;
            this.flyingPage.Text = "Flying";
            // 
            // thrustValue
            // 
            this.thrustValue.Location = new System.Drawing.Point(349, 115);
            this.thrustValue.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.thrustValue.Name = "thrustValue";
            this.thrustValue.Size = new System.Drawing.Size(70, 20);
            this.thrustValue.TabIndex = 17;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(645, -1);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(62, 25);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // enableStabilizationCheckBox
            // 
            this.enableStabilizationCheckBox.AutoSize = true;
            this.enableStabilizationCheckBox.Location = new System.Drawing.Point(283, 141);
            this.enableStabilizationCheckBox.Name = "enableStabilizationCheckBox";
            this.enableStabilizationCheckBox.Size = new System.Drawing.Size(116, 17);
            this.enableStabilizationCheckBox.TabIndex = 22;
            this.enableStabilizationCheckBox.Text = "Enable stabilization";
            this.enableStabilizationCheckBox.UseVisualStyleBackColor = true;
            // 
            // negativeMixingCheckBox
            // 
            this.negativeMixingCheckBox.AutoSize = true;
            this.negativeMixingCheckBox.Location = new System.Drawing.Point(283, 164);
            this.negativeMixingCheckBox.Name = "negativeMixingCheckBox";
            this.negativeMixingCheckBox.Size = new System.Drawing.Size(101, 17);
            this.negativeMixingCheckBox.TabIndex = 23;
            this.negativeMixingCheckBox.Text = "Negative mixing";
            this.negativeMixingCheckBox.UseVisualStyleBackColor = true;
            // 
            // keepMotorsOnCheckBox
            // 
            this.keepMotorsOnCheckBox.AutoSize = true;
            this.keepMotorsOnCheckBox.Location = new System.Drawing.Point(283, 187);
            this.keepMotorsOnCheckBox.Name = "keepMotorsOnCheckBox";
            this.keepMotorsOnCheckBox.Size = new System.Drawing.Size(100, 17);
            this.keepMotorsOnCheckBox.TabIndex = 24;
            this.keepMotorsOnCheckBox.Text = "Keep motors on";
            this.keepMotorsOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 438);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            firmwareGroupBox.ResumeLayout(false);
            firmwareGroupBox.PerformLayout();
            quadrocopterPage.ResumeLayout(false);
            quadrocopterPage.PerformLayout();
            hardwareGroupBox.ResumeLayout(false);
            hardwareGroupBox.PerformLayout();
            motorsGroupBox.ResumeLayout(false);
            motorsGroupBox.PerformLayout();
            safetyGroupBox.ResumeLayout(false);
            safetyGroupBox.PerformLayout();
            pidPitchGroupBox.ResumeLayout(false);
            pidPitchGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKpTextBox)).EndInit();
            pidRollGroupBox.ResumeLayout(false);
            pidRollGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKpTextBox)).EndInit();
            pidYawGroupBox.ResumeLayout(false);
            pidYawGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yawKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKpTextBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.flyingPage.ResumeLayout(false);
            this.flyingPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thrustValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage flyingPage;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.TextBox magnetometerTextBox;
        private System.Windows.Forms.TextBox gyroSensorTextBox;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Button updateFirmwareButton;
        private System.Windows.Forms.TextBox firmwareVersionTextBox;
        private System.Windows.Forms.TextBox buildDateTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button calibrateButton;
        private System.Windows.Forms.TextBox minValueTextBox;
        private System.Windows.Forms.TextBox hoverValueTextBox;
        private System.Windows.Forms.TextBox idleValueTextBox;
        private System.Windows.Forms.TextBox maxValueTextBox;
        private System.Windows.Forms.TextBox safeMotorValueTextBox;
        private System.Windows.Forms.TextBox safeRollTextBox;
        private System.Windows.Forms.TextBox safePitchTextBox;
        private System.Windows.Forms.TextBox safeTemperatureTextBox;
        private System.Windows.Forms.NumericUpDown pitchKdTextBox;
        private System.Windows.Forms.NumericUpDown pitchKiTextBox;
        private System.Windows.Forms.NumericUpDown pitchKpTextBox;
        private System.Windows.Forms.NumericUpDown yawKdTextBox;
        private System.Windows.Forms.NumericUpDown yawKiTextBox;
        private System.Windows.Forms.NumericUpDown yawKpTextBox;
        private System.Windows.Forms.NumericUpDown rollKdTextBox;
        private System.Windows.Forms.NumericUpDown rollKiTextBox;
        private System.Windows.Forms.NumericUpDown rollKpTextBox;
        private System.Windows.Forms.NumericUpDown thrustValue;
        private System.Windows.Forms.CheckBox saveConfigCheckBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.CheckBox keepMotorsOnCheckBox;
        private System.Windows.Forms.CheckBox negativeMixingCheckBox;
        private System.Windows.Forms.CheckBox enableStabilizationCheckBox;
    }
}