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
            this.motorsGroupBox = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rightTopTextBox = new System.Windows.Forms.TextBox();
            this.rightTopLabel = new System.Windows.Forms.Label();
            this.leftBottomTextBox = new System.Windows.Forms.TextBox();
            this.leftBottomLabel = new System.Windows.Forms.Label();
            this.leftTopTextBox = new System.Windows.Forms.TextBox();
            this.leftTopLabel = new System.Windows.Forms.Label();
            this.motorsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // motorsGroupBox
            // 
            this.motorsGroupBox.Controls.Add(this.textBox1);
            this.motorsGroupBox.Controls.Add(this.label2);
            this.motorsGroupBox.Controls.Add(this.rightTopTextBox);
            this.motorsGroupBox.Controls.Add(this.rightTopLabel);
            this.motorsGroupBox.Controls.Add(this.leftBottomTextBox);
            this.motorsGroupBox.Controls.Add(this.leftBottomLabel);
            this.motorsGroupBox.Controls.Add(this.leftTopTextBox);
            this.motorsGroupBox.Controls.Add(this.leftTopLabel);
            this.motorsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motorsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.motorsGroupBox.Name = "motorsGroupBox";
            this.motorsGroupBox.Size = new System.Drawing.Size(364, 92);
            this.motorsGroupBox.TabIndex = 4;
            this.motorsGroupBox.TabStop = false;
            this.motorsGroupBox.Text = "Motors";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(61, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Left bottom";
            // 
            // rightTopTextBox
            // 
            this.rightTopTextBox.Location = new System.Drawing.Point(146, 22);
            this.rightTopTextBox.Name = "rightTopTextBox";
            this.rightTopTextBox.Size = new System.Drawing.Size(61, 20);
            this.rightTopTextBox.TabIndex = 5;
            // 
            // rightTopLabel
            // 
            this.rightTopLabel.AutoSize = true;
            this.rightTopLabel.Location = new System.Drawing.Point(213, 25);
            this.rightTopLabel.Name = "rightTopLabel";
            this.rightTopLabel.Size = new System.Drawing.Size(67, 13);
            this.rightTopLabel.TabIndex = 4;
            this.rightTopLabel.Text = "Right bottom";
            // 
            // leftBottomTextBox
            // 
            this.leftBottomTextBox.Location = new System.Drawing.Point(79, 48);
            this.leftBottomTextBox.Name = "leftBottomTextBox";
            this.leftBottomTextBox.Size = new System.Drawing.Size(61, 20);
            this.leftBottomTextBox.TabIndex = 3;
            // 
            // leftBottomLabel
            // 
            this.leftBottomLabel.AutoSize = true;
            this.leftBottomLabel.Location = new System.Drawing.Point(13, 51);
            this.leftBottomLabel.Name = "leftBottomLabel";
            this.leftBottomLabel.Size = new System.Drawing.Size(60, 13);
            this.leftBottomLabel.TabIndex = 2;
            this.leftBottomLabel.Text = "Left bottom";
            // 
            // leftTopTextBox
            // 
            this.leftTopTextBox.Location = new System.Drawing.Point(79, 22);
            this.leftTopTextBox.Name = "leftTopTextBox";
            this.leftTopTextBox.Size = new System.Drawing.Size(61, 20);
            this.leftTopTextBox.TabIndex = 1;
            // 
            // leftTopLabel
            // 
            this.leftTopLabel.AutoSize = true;
            this.leftTopLabel.Location = new System.Drawing.Point(13, 25);
            this.leftTopLabel.Name = "leftTopLabel";
            this.leftTopLabel.Size = new System.Drawing.Size(43, 13);
            this.leftTopLabel.TabIndex = 0;
            this.leftTopLabel.Text = "Left top";
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rightTopTextBox;
        private System.Windows.Forms.Label rightTopLabel;
        private System.Windows.Forms.TextBox leftBottomTextBox;
        private System.Windows.Forms.Label leftBottomLabel;
        private System.Windows.Forms.TextBox leftTopTextBox;
        private System.Windows.Forms.Label leftTopLabel;
    }
}
