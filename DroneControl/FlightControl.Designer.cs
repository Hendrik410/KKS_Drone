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
            this.maxYawLabel = new System.Windows.Forms.Label();
            this.maxYawNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxPitchLabel = new System.Windows.Forms.Label();
            this.maxPitchNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.inputTypeComboBox = new System.Windows.Forms.ComboBox();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.targetPitchLabel = new System.Windows.Forms.Label();
            this.targetRollLabel = new System.Windows.Forms.Label();
            this.targetYawLabel = new System.Windows.Forms.Label();
            this.targetThrustLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maxRollNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxYawNumeric)).BeginInit();
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
            // maxYawLabel
            // 
            this.maxYawLabel.AutoSize = true;
            this.maxYawLabel.Location = new System.Drawing.Point(3, 120);
            this.maxYawLabel.Name = "maxYawLabel";
            this.maxYawLabel.Size = new System.Drawing.Size(74, 13);
            this.maxYawLabel.TabIndex = 13;
            this.maxYawLabel.Text = "Max Yaw [°/s]";
            // 
            // maxYawNumeric
            // 
            this.maxYawNumeric.DecimalPlaces = 1;
            this.maxYawNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxYawNumeric.Location = new System.Drawing.Point(6, 136);
            this.maxYawNumeric.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.maxYawNumeric.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxYawNumeric.Name = "maxYawNumeric";
            this.maxYawNumeric.Size = new System.Drawing.Size(54, 20);
            this.maxYawNumeric.TabIndex = 12;
            this.maxYawNumeric.Value = new decimal(new int[] {
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
            this.targetPitchLabel.AutoSize = true;
            this.targetPitchLabel.Location = new System.Drawing.Point(369, 14);
            this.targetPitchLabel.Name = "targetPitchLabel";
            this.targetPitchLabel.Size = new System.Drawing.Size(85, 13);
            this.targetPitchLabel.TabIndex = 17;
            this.targetPitchLabel.Text = "Target Pitch: {0}";
            // 
            // targetRollLabel
            // 
            this.targetRollLabel.AutoSize = true;
            this.targetRollLabel.Location = new System.Drawing.Point(375, 27);
            this.targetRollLabel.Name = "targetRollLabel";
            this.targetRollLabel.Size = new System.Drawing.Size(79, 13);
            this.targetRollLabel.TabIndex = 18;
            this.targetRollLabel.Text = "Target Roll: {0}";
            // 
            // targetYawLabel
            // 
            this.targetYawLabel.AutoSize = true;
            this.targetYawLabel.Location = new System.Drawing.Point(372, 40);
            this.targetYawLabel.Name = "targetYawLabel";
            this.targetYawLabel.Size = new System.Drawing.Size(82, 13);
            this.targetYawLabel.TabIndex = 19;
            this.targetYawLabel.Text = "Target Yaw: {0}";
            // 
            // targetThrustLabel
            // 
            this.targetThrustLabel.AutoSize = true;
            this.targetThrustLabel.Location = new System.Drawing.Point(363, 53);
            this.targetThrustLabel.Name = "targetThrustLabel";
            this.targetThrustLabel.Size = new System.Drawing.Size(91, 13);
            this.targetThrustLabel.TabIndex = 20;
            this.targetThrustLabel.Text = "Target Thrust: {0}";
            // 
            // FlightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.targetThrustLabel);
            this.Controls.Add(this.targetYawLabel);
            this.Controls.Add(this.targetRollLabel);
            this.Controls.Add(this.targetPitchLabel);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.maxRollLabel);
            this.Controls.Add(this.maxRollNumeric);
            this.Controls.Add(this.maxYawLabel);
            this.Controls.Add(this.maxYawNumeric);
            this.Controls.Add(this.maxPitchLabel);
            this.Controls.Add(this.maxPitchNumeric);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputTypeComboBox);
            this.Name = "FlightControl";
            this.Size = new System.Drawing.Size(457, 160);
            ((System.ComponentModel.ISupportInitialize)(this.maxRollNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxYawNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPitchNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label maxRollLabel;
        private System.Windows.Forms.NumericUpDown maxRollNumeric;
        private System.Windows.Forms.Label maxYawLabel;
        private System.Windows.Forms.NumericUpDown maxYawNumeric;
        private System.Windows.Forms.Label maxPitchLabel;
        private System.Windows.Forms.NumericUpDown maxPitchNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox inputTypeComboBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.Label targetPitchLabel;
        private System.Windows.Forms.Label targetRollLabel;
        private System.Windows.Forms.Label targetYawLabel;
        private System.Windows.Forms.Label targetThrustLabel;
    }
}
