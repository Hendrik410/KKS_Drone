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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ipInfoLabel = new System.Windows.Forms.Label();
            this.pingLabel = new System.Windows.Forms.Label();
            this.statusArmedLabel = new System.Windows.Forms.Label();
            this.armToogleButton = new System.Windows.Forms.Button();
            this.motorControl1 = new DroneControl.MotorControl();
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
            this.statusArmedLabel.AutoSize = true;
            this.statusArmedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusArmedLabel.Location = new System.Drawing.Point(294, 13);
            this.statusArmedLabel.Name = "statusArmedLabel";
            this.statusArmedLabel.Size = new System.Drawing.Size(57, 13);
            this.statusArmedLabel.TabIndex = 3;
            this.statusArmedLabel.Text = "Status: {0}";
            // 
            // armToogleButton
            // 
            this.armToogleButton.Location = new System.Drawing.Point(384, 8);
            this.armToogleButton.Name = "armToogleButton";
            this.armToogleButton.Size = new System.Drawing.Size(75, 23);
            this.armToogleButton.TabIndex = 4;
            this.armToogleButton.Text = "Arm";
            this.armToogleButton.UseVisualStyleBackColor = true;
            this.armToogleButton.Click += new System.EventHandler(this.armToogleButton_Click);
            // 
            // motorControl1
            // 
            this.motorControl1.Location = new System.Drawing.Point(12, 42);
            this.motorControl1.Name = "motorControl1";
            this.motorControl1.Size = new System.Drawing.Size(364, 92);
            this.motorControl1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 261);
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
    }
}