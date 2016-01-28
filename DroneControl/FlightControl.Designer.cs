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
            this.maxRollLabel = new System.Windows.Forms.Label();
            this.maxRollNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxRotationalSpeedLabel = new System.Windows.Forms.Label();
            this.maxRotationSpeedNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxPitchLabel = new System.Windows.Forms.Label();
            this.maxPitchNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.inputTypeComboBox = new System.Windows.Forms.ComboBox();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.targetPitchLabel = new System.Windows.Forms.Label();
            this.targetRollLabel = new System.Windows.Forms.Label();
            this.rotationalSpeedLabel = new System.Windows.Forms.Label();
            this.targetThrustLabel = new System.Windows.Forms.Label();
            this.ratioDataLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maxRollNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRotationSpeedNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPitchNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // maxRollLabel
            // 
            this.maxRollLabel.AutoSize = true;
            this.maxRollLabel.Location = new System.Drawing.Point(3, 78);
            this.maxRollLabel.Name = "maxRollLabel";
            this.maxRollLabel.Size = new System.Drawing.Size(61, 13);
            this.maxRollLabel.TabIndex = 15;
            this.maxRollLabel.Text = "Max Roll [°]";
            // 
            // maxRollNumeric
            // 
            this.maxRollNumeric.DecimalPlaces = 1;
            this.maxRollNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxRollNumeric.Location = new System.Drawing.Point(6, 94);
            this.maxRollNumeric.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.maxRollNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.maxRollNumeric.Name = "maxRollNumeric";
            this.maxRollNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxRollNumeric.TabIndex = 14;
            this.maxRollNumeric.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // maxRotationalSpeedLabel
            // 
            this.maxRotationalSpeedLabel.AutoSize = true;
            this.maxRotationalSpeedLabel.Location = new System.Drawing.Point(3, 120);
            this.maxRotationalSpeedLabel.Name = "maxRotationalSpeedLabel";
            this.maxRotationalSpeedLabel.Size = new System.Drawing.Size(135, 13);
            this.maxRotationalSpeedLabel.TabIndex = 13;
            this.maxRotationalSpeedLabel.Text = "Max Rotational Speed [°/s]";
            // 
            // maxRotationSpeedNumeric
            // 
            this.maxRotationSpeedNumeric.DecimalPlaces = 1;
            this.maxRotationSpeedNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxRotationSpeedNumeric.Location = new System.Drawing.Point(6, 136);
            this.maxRotationSpeedNumeric.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.maxRotationSpeedNumeric.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxRotationSpeedNumeric.Name = "maxRotationSpeedNumeric";
            this.maxRotationSpeedNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxRotationSpeedNumeric.TabIndex = 12;
            this.maxRotationSpeedNumeric.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // maxPitchLabel
            // 
            this.maxPitchLabel.AutoSize = true;
            this.maxPitchLabel.Location = new System.Drawing.Point(3, 40);
            this.maxPitchLabel.Name = "maxPitchLabel";
            this.maxPitchLabel.Size = new System.Drawing.Size(67, 13);
            this.maxPitchLabel.TabIndex = 11;
            this.maxPitchLabel.Text = "Max Pitch [°]";
            // 
            // maxPitchNumeric
            // 
            this.maxPitchNumeric.DecimalPlaces = 1;
            this.maxPitchNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxPitchNumeric.Location = new System.Drawing.Point(6, 56);
            this.maxPitchNumeric.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.maxPitchNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.maxPitchNumeric.Name = "maxPitchNumeric";
            this.maxPitchNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxPitchNumeric.TabIndex = 10;
            this.maxPitchNumeric.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Input Device Type";
            // 
            // inputTypeComboBox
            // 
            this.inputTypeComboBox.FormattingEnabled = true;
            this.inputTypeComboBox.Location = new System.Drawing.Point(104, 8);
            this.inputTypeComboBox.Name = "inputTypeComboBox";
            this.inputTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.inputTypeComboBox.TabIndex = 8;
            // 
            // activeCheckBox
            // 
            this.activeCheckBox.AutoSize = true;
            this.activeCheckBox.Location = new System.Drawing.Point(231, 10);
            this.activeCheckBox.Name = "activeCheckBox";
            this.activeCheckBox.Size = new System.Drawing.Size(55, 17);
            this.activeCheckBox.TabIndex = 16;
            this.activeCheckBox.Text = "active";
            this.activeCheckBox.UseVisualStyleBackColor = true;
            this.activeCheckBox.CheckedChanged += new System.EventHandler(this.activeCheckBox_CheckedChanged);
            // 
            // targetPitchLabel
            // 
            this.targetPitchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPitchLabel.AutoSize = true;
            this.targetPitchLabel.Location = new System.Drawing.Point(302, 14);
            this.targetPitchLabel.Name = "targetPitchLabel";
            this.targetPitchLabel.Size = new System.Drawing.Size(85, 13);
            this.targetPitchLabel.TabIndex = 17;
            this.targetPitchLabel.Text = "Target Pitch: {0}";
            // 
            // targetRollLabel
            // 
            this.targetRollLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetRollLabel.AutoSize = true;
            this.targetRollLabel.Location = new System.Drawing.Point(308, 27);
            this.targetRollLabel.Name = "targetRollLabel";
            this.targetRollLabel.Size = new System.Drawing.Size(79, 13);
            this.targetRollLabel.TabIndex = 18;
            this.targetRollLabel.Text = "Target Roll: {0}";
            // 
            // rotationalSpeedLabel
            // 
            this.rotationalSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rotationalSpeedLabel.AutoSize = true;
            this.rotationalSpeedLabel.Location = new System.Drawing.Point(278, 40);
            this.rotationalSpeedLabel.Name = "rotationalSpeedLabel";
            this.rotationalSpeedLabel.Size = new System.Drawing.Size(109, 13);
            this.rotationalSpeedLabel.TabIndex = 19;
            this.rotationalSpeedLabel.Text = "Rotational Speed: {0}";
            // 
            // targetThrustLabel
            // 
            this.targetThrustLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetThrustLabel.AutoSize = true;
            this.targetThrustLabel.Location = new System.Drawing.Point(296, 53);
            this.targetThrustLabel.Name = "targetThrustLabel";
            this.targetThrustLabel.Size = new System.Drawing.Size(91, 13);
            this.targetThrustLabel.TabIndex = 20;
            this.targetThrustLabel.Text = "Target Thrust: {0}";
            // 
            // ratioDataLabel
            // 
            this.ratioDataLabel.AutoSize = true;
            this.ratioDataLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratioDataLabel.Location = new System.Drawing.Point(296, 79);
            this.ratioDataLabel.Name = "ratioDataLabel";
            this.ratioDataLabel.Size = new System.Drawing.Size(67, 13);
            this.ratioDataLabel.TabIndex = 21;
            this.ratioDataLabel.Text = "Ratio data";
            // 
            // FlightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ratioDataLabel);
            this.Controls.Add(this.targetThrustLabel);
            this.Controls.Add(this.rotationalSpeedLabel);
            this.Controls.Add(this.targetRollLabel);
            this.Controls.Add(this.targetPitchLabel);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.maxRollLabel);
            this.Controls.Add(this.maxRollNumeric);
            this.Controls.Add(this.maxRotationalSpeedLabel);
            this.Controls.Add(this.maxRotationSpeedNumeric);
            this.Controls.Add(this.maxPitchLabel);
            this.Controls.Add(this.maxPitchNumeric);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputTypeComboBox);
            this.Name = "FlightControl";
            this.Size = new System.Drawing.Size(457, 160);
            ((System.ComponentModel.ISupportInitialize)(this.maxRollNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRotationSpeedNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPitchNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label maxRollLabel;
        private System.Windows.Forms.NumericUpDown maxRollNumeric;
        private System.Windows.Forms.Label maxRotationalSpeedLabel;
        private System.Windows.Forms.NumericUpDown maxRotationSpeedNumeric;
        private System.Windows.Forms.Label maxPitchLabel;
        private System.Windows.Forms.NumericUpDown maxPitchNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox inputTypeComboBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.Label targetPitchLabel;
        private System.Windows.Forms.Label targetRollLabel;
        private System.Windows.Forms.Label rotationalSpeedLabel;
        private System.Windows.Forms.Label targetThrustLabel;
        private System.Windows.Forms.Label ratioDataLabel;
    }
}
