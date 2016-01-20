namespace DroneControl
{
    partial class LogForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.flushTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.clientPage = new System.Windows.Forms.TabPage();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.logCleanButton = new System.Windows.Forms.Button();
            this.dronePage = new System.Windows.Forms.TabPage();
            this.clearDroneButton = new System.Windows.Forms.Button();
            this.droneLogTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.clientPage.SuspendLayout();
            this.dronePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // flushTimer
            // 
            this.flushTimer.Enabled = true;
            this.flushTimer.Interval = 200;
            this.flushTimer.Tick += new System.EventHandler(this.flushTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.clientPage);
            this.tabControl1.Controls.Add(this.dronePage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(528, 390);
            this.tabControl1.TabIndex = 2;
            // 
            // clientPage
            // 
            this.clientPage.Controls.Add(this.logCleanButton);
            this.clientPage.Controls.Add(this.logTextBox);
            this.clientPage.Location = new System.Drawing.Point(4, 22);
            this.clientPage.Name = "clientPage";
            this.clientPage.Padding = new System.Windows.Forms.Padding(3);
            this.clientPage.Size = new System.Drawing.Size(520, 364);
            this.clientPage.TabIndex = 0;
            this.clientPage.Text = "Client";
            this.clientPage.UseVisualStyleBackColor = true;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTextBox.Location = new System.Drawing.Point(3, 3);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(514, 358);
            this.logTextBox.TabIndex = 2;
            // 
            // logCleanButton
            // 
            this.logCleanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.logCleanButton.Location = new System.Drawing.Point(412, 333);
            this.logCleanButton.Name = "logCleanButton";
            this.logCleanButton.Size = new System.Drawing.Size(75, 23);
            this.logCleanButton.TabIndex = 4;
            this.logCleanButton.Text = "Clear";
            this.logCleanButton.UseVisualStyleBackColor = true;
            this.logCleanButton.Click += new System.EventHandler(this.logCleanButton_Click);
            // 
            // dronePage
            // 
            this.dronePage.Controls.Add(this.clearDroneButton);
            this.dronePage.Controls.Add(this.droneLogTextBox);
            this.dronePage.Location = new System.Drawing.Point(4, 22);
            this.dronePage.Name = "dronePage";
            this.dronePage.Padding = new System.Windows.Forms.Padding(3);
            this.dronePage.Size = new System.Drawing.Size(520, 364);
            this.dronePage.TabIndex = 1;
            this.dronePage.Text = "Drone";
            this.dronePage.UseVisualStyleBackColor = true;
            // 
            // clearDroneButton
            // 
            this.clearDroneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearDroneButton.Location = new System.Drawing.Point(409, 330);
            this.clearDroneButton.Name = "clearDroneButton";
            this.clearDroneButton.Size = new System.Drawing.Size(75, 23);
            this.clearDroneButton.TabIndex = 8;
            this.clearDroneButton.Text = "Clear";
            this.clearDroneButton.UseVisualStyleBackColor = true;
            this.clearDroneButton.Click += new System.EventHandler(this.clearDroneButton_Click);
            // 
            // droneLogTextBox
            // 
            this.droneLogTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.droneLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.droneLogTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.droneLogTextBox.Location = new System.Drawing.Point(3, 3);
            this.droneLogTextBox.Multiline = true;
            this.droneLogTextBox.Name = "droneLogTextBox";
            this.droneLogTextBox.ReadOnly = true;
            this.droneLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.droneLogTextBox.Size = new System.Drawing.Size(514, 358);
            this.droneLogTextBox.TabIndex = 7;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 390);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogForm";
            this.Text = "Log";
            this.tabControl1.ResumeLayout(false);
            this.clientPage.ResumeLayout(false);
            this.clientPage.PerformLayout();
            this.dronePage.ResumeLayout(false);
            this.dronePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer flushTimer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage clientPage;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Button logCleanButton;
        private System.Windows.Forms.TabPage dronePage;
        private System.Windows.Forms.Button clearDroneButton;
        private System.Windows.Forms.TextBox droneLogTextBox;
    }
}