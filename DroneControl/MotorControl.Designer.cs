namespace DroneControl
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
            this.rightBackTextBox = new System.Windows.Forms.TextBox();
            this.rightFrontTextBox = new System.Windows.Forms.TextBox();
            this.leftBackTextBox = new System.Windows.Forms.TextBox();
            this.leftFrontTextBox = new System.Windows.Forms.TextBox();
            rightBackLabel = new System.Windows.Forms.Label();
            rightFrontLabel = new System.Windows.Forms.Label();
            leftBackLabel = new System.Windows.Forms.Label();
            leftFrontLabel = new System.Windows.Forms.Label();
            this.motorsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // motorsGroupBox
            // 
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
            // rightBackTextBox
            // 
            this.rightBackTextBox.Location = new System.Drawing.Point(146, 48);
            this.rightBackTextBox.Name = "rightBackTextBox";
            this.rightBackTextBox.Size = new System.Drawing.Size(61, 20);
            this.rightBackTextBox.TabIndex = 7;
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
            // rightFrontTextBox
            // 
            this.rightFrontTextBox.Location = new System.Drawing.Point(146, 22);
            this.rightFrontTextBox.Name = "rightFrontTextBox";
            this.rightFrontTextBox.Size = new System.Drawing.Size(61, 20);
            this.rightFrontTextBox.TabIndex = 5;
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
            // leftBackTextBox
            // 
            this.leftBackTextBox.Location = new System.Drawing.Point(79, 48);
            this.leftBackTextBox.Name = "leftBackTextBox";
            this.leftBackTextBox.Size = new System.Drawing.Size(61, 20);
            this.leftBackTextBox.TabIndex = 3;
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
            // leftFrontTextBox
            // 
            this.leftFrontTextBox.Location = new System.Drawing.Point(79, 22);
            this.leftFrontTextBox.Name = "leftFrontTextBox";
            this.leftFrontTextBox.Size = new System.Drawing.Size(61, 20);
            this.leftFrontTextBox.TabIndex = 1;
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
            // MotorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.motorsGroupBox);
            this.Name = "MotorControl";
            this.Size = new System.Drawing.Size(364, 92);
            this.motorsGroupBox.ResumeLayout(false);
            this.motorsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox motorsGroupBox;
        private System.Windows.Forms.TextBox rightBackTextBox;
        private System.Windows.Forms.TextBox rightFrontTextBox;
        private System.Windows.Forms.TextBox leftBackTextBox;
        private System.Windows.Forms.TextBox leftFrontTextBox;
    }
}
