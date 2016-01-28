﻿namespace DroneControl
{
    partial class MotorControl
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
            System.Windows.Forms.Label rightBackLabel;
            System.Windows.Forms.Label rightFrontLabel;
            System.Windows.Forms.Label leftBackLabel;
            System.Windows.Forms.Label leftFrontLabel;
            this.motorsGroupBox = new System.Windows.Forms.GroupBox();
            this.setValuesButton = new System.Windows.Forms.Button();
            this.servoValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rightBackTextBox = new System.Windows.Forms.TextBox();
            this.rightFrontTextBox = new System.Windows.Forms.TextBox();
            this.leftBackTextBox = new System.Windows.Forms.TextBox();
            this.leftFrontTextBox = new System.Windows.Forms.TextBox();
            rightBackLabel = new System.Windows.Forms.Label();
            rightFrontLabel = new System.Windows.Forms.Label();
            leftBackLabel = new System.Windows.Forms.Label();
            leftFrontLabel = new System.Windows.Forms.Label();
            this.motorsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servoValueNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // rightBackLabel
            // 
            rightBackLabel.AutoSize = true;
            rightBackLabel.Location = new System.Drawing.Point(213, 51);
            rightBackLabel.Name = "rightBackLabel";
            rightBackLabel.Size = new System.Drawing.Size(59, 13);
            rightBackLabel.TabIndex = 6;
            rightBackLabel.Text = "Right back";
            // 
            // rightFrontLabel
            // 
            rightFrontLabel.AutoSize = true;
            rightFrontLabel.Location = new System.Drawing.Point(213, 25);
            rightFrontLabel.Name = "rightFrontLabel";
            rightFrontLabel.Size = new System.Drawing.Size(56, 13);
            rightFrontLabel.TabIndex = 4;
            rightFrontLabel.Text = "Right front";
            // 
            // leftBackLabel
            // 
            leftBackLabel.AutoSize = true;
            leftBackLabel.Location = new System.Drawing.Point(13, 51);
            leftBackLabel.Name = "leftBackLabel";
            leftBackLabel.Size = new System.Drawing.Size(52, 13);
            leftBackLabel.TabIndex = 2;
            leftBackLabel.Text = "Left back";
            // 
            // leftFrontLabel
            // 
            leftFrontLabel.AutoSize = true;
            leftFrontLabel.Location = new System.Drawing.Point(13, 25);
            leftFrontLabel.Name = "leftFrontLabel";
            leftFrontLabel.Size = new System.Drawing.Size(49, 13);
            leftFrontLabel.TabIndex = 0;
            leftFrontLabel.Text = "Left front";
            // 
            // motorsGroupBox
            // 
            this.motorsGroupBox.Controls.Add(this.setValuesButton);
            this.motorsGroupBox.Controls.Add(this.servoValueNumericUpDown);
            this.motorsGroupBox.Controls.Add(this.rightBackTextBox);
            this.motorsGroupBox.Controls.Add(rightBackLabel);
            this.motorsGroupBox.Controls.Add(this.rightFrontTextBox);
            this.motorsGroupBox.Controls.Add(rightFrontLabel);
            this.motorsGroupBox.Controls.Add(this.leftBackTextBox);
            this.motorsGroupBox.Controls.Add(leftBackLabel);
            this.motorsGroupBox.Controls.Add(this.leftFrontTextBox);
            this.motorsGroupBox.Controls.Add(leftFrontLabel);
            this.motorsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motorsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.motorsGroupBox.Name = "motorsGroupBox";
            this.motorsGroupBox.Size = new System.Drawing.Size(364, 92);
            this.motorsGroupBox.TabIndex = 4;
            this.motorsGroupBox.TabStop = false;
            this.motorsGroupBox.Text = "Motors";
            // 
            // setValuesButton
            // 
            this.setValuesButton.Location = new System.Drawing.Point(305, 49);
            this.setValuesButton.Name = "setValuesButton";
            this.setValuesButton.Size = new System.Drawing.Size(52, 22);
            this.setValuesButton.TabIndex = 9;
            this.setValuesButton.Text = "Set";
            this.setValuesButton.UseVisualStyleBackColor = true;
            this.setValuesButton.Click += new System.EventHandler(this.setValuesButton_Click);
            // 
            // servoValueNumericUpDown
            // 
            this.servoValueNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.servoValueNumericUpDown.Location = new System.Drawing.Point(305, 23);
            this.servoValueNumericUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.servoValueNumericUpDown.Minimum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.servoValueNumericUpDown.Name = "servoValueNumericUpDown";
            this.servoValueNumericUpDown.Size = new System.Drawing.Size(53, 20);
            this.servoValueNumericUpDown.TabIndex = 8;
            this.servoValueNumericUpDown.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.servoValueNumericUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.servoValueNumericUpDown_KeyUp);
            // 
            // rightBackTextBox
            // 
            this.rightBackTextBox.Location = new System.Drawing.Point(146, 48);
            this.rightBackTextBox.Name = "rightBackTextBox";
            this.rightBackTextBox.Size = new System.Drawing.Size(61, 20);
            this.rightBackTextBox.TabIndex = 7;
            this.rightBackTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnEnter);
            // 
            // rightFrontTextBox
            // 
            this.rightFrontTextBox.Location = new System.Drawing.Point(146, 22);
            this.rightFrontTextBox.Name = "rightFrontTextBox";
            this.rightFrontTextBox.Size = new System.Drawing.Size(61, 20);
            this.rightFrontTextBox.TabIndex = 5;
            this.rightFrontTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnEnter);
            // 
            // leftBackTextBox
            // 
            this.leftBackTextBox.Location = new System.Drawing.Point(79, 48);
            this.leftBackTextBox.Name = "leftBackTextBox";
            this.leftBackTextBox.Size = new System.Drawing.Size(61, 20);
            this.leftBackTextBox.TabIndex = 3;
            this.leftBackTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnEnter);
            // 
            // leftFrontTextBox
            // 
            this.leftFrontTextBox.Location = new System.Drawing.Point(79, 22);
            this.leftFrontTextBox.Name = "leftFrontTextBox";
            this.leftFrontTextBox.Size = new System.Drawing.Size(61, 20);
            this.leftFrontTextBox.TabIndex = 1;
            this.leftFrontTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnEnter);
            // 
            // MotorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.motorsGroupBox);
            this.Name = "MotorControl";
            this.Size = new System.Drawing.Size(364, 92);
            this.motorsGroupBox.ResumeLayout(false);
            this.motorsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servoValueNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox motorsGroupBox;
        private System.Windows.Forms.TextBox rightBackTextBox;
        private System.Windows.Forms.TextBox rightFrontTextBox;
        private System.Windows.Forms.TextBox leftBackTextBox;
        private System.Windows.Forms.TextBox leftFrontTextBox;
        private System.Windows.Forms.NumericUpDown servoValueNumericUpDown;
        private System.Windows.Forms.Button setValuesButton;
    }
}
