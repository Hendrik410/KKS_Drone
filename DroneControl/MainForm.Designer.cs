﻿namespace DroneControl {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ipInfoLabel = new System.Windows.Forms.Label();
            this.pingLabel = new System.Windows.Forms.Label();
            this.statusArmedLabel = new System.Windows.Forms.Label();
            this.armToogleButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.infoPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.logButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.mainViewTabs = new System.Windows.Forms.TabControl();
            this.manualControlPage = new System.Windows.Forms.TabPage();
            this.flightControlPage = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.infoTabPage = new System.Windows.Forms.TabPage();
            this.configTabPage = new System.Windows.Forms.TabPage();
            this.droneConfigPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.sensorControl1 = new DroneControl.SensorControl();
            this.motorControl1 = new DroneControl.MotorControl();
            this.flightControl1 = new DroneControl.FlightControl();
            this.mainViewTabs.SuspendLayout();
            this.manualControlPage.SuspendLayout();
            this.flightControlPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.infoTabPage.SuspendLayout();
            this.configTabPage.SuspendLayout();
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
            this.statusArmedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusArmedLabel.AutoSize = true;
            this.statusArmedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusArmedLabel.Location = new System.Drawing.Point(498, 13);
            this.statusArmedLabel.Name = "statusArmedLabel";
            this.statusArmedLabel.Size = new System.Drawing.Size(57, 13);
            this.statusArmedLabel.TabIndex = 3;
            this.statusArmedLabel.Text = "Status: {0}";
            // 
            // armToogleButton
            // 
            this.armToogleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.armToogleButton.Location = new System.Drawing.Point(633, 8);
            this.armToogleButton.Name = "armToogleButton";
            this.armToogleButton.Size = new System.Drawing.Size(75, 23);
            this.armToogleButton.TabIndex = 4;
            this.armToogleButton.Text = "Arm";
            this.armToogleButton.UseVisualStyleBackColor = true;
            this.armToogleButton.Click += new System.EventHandler(this.armToogleButton_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            // 
            // infoPropertyGrid
            // 
            this.infoPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPropertyGrid.HelpVisible = false;
            this.infoPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.infoPropertyGrid.Name = "infoPropertyGrid";
            this.infoPropertyGrid.Size = new System.Drawing.Size(206, 340);
            this.infoPropertyGrid.TabIndex = 8;
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(321, 8);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(75, 23);
            this.logButton.TabIndex = 9;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.BackColor = System.Drawing.Color.DarkRed;
            this.stopButton.ForeColor = System.Drawing.Color.White;
            this.stopButton.Location = new System.Drawing.Point(633, 38);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 10;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Location = new System.Drawing.Point(492, 38);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // mainViewTabs
            // 
            this.mainViewTabs.Controls.Add(this.manualControlPage);
            this.mainViewTabs.Controls.Add(this.flightControlPage);
            this.mainViewTabs.Location = new System.Drawing.Point(12, 38);
            this.mainViewTabs.Name = "mainViewTabs";
            this.mainViewTabs.SelectedIndex = 0;
            this.mainViewTabs.Size = new System.Drawing.Size(474, 219);
            this.mainViewTabs.TabIndex = 14;
            // 
            // manualControlPage
            // 
            this.manualControlPage.Controls.Add(this.motorControl1);
            this.manualControlPage.Location = new System.Drawing.Point(4, 22);
            this.manualControlPage.Name = "manualControlPage";
            this.manualControlPage.Padding = new System.Windows.Forms.Padding(3);
            this.manualControlPage.Size = new System.Drawing.Size(466, 193);
            this.manualControlPage.TabIndex = 0;
            this.manualControlPage.Text = "Manual Control";
            this.manualControlPage.UseVisualStyleBackColor = true;
            // 
            // flightControlPage
            // 
            this.flightControlPage.Controls.Add(this.flightControl1);
            this.flightControlPage.Location = new System.Drawing.Point(4, 22);
            this.flightControlPage.Name = "flightControlPage";
            this.flightControlPage.Padding = new System.Windows.Forms.Padding(3);
            this.flightControlPage.Size = new System.Drawing.Size(466, 193);
            this.flightControlPage.TabIndex = 1;
            this.flightControlPage.Text = "Flight Control";
            this.flightControlPage.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.infoTabPage);
            this.tabControl1.Controls.Add(this.configTabPage);
            this.tabControl1.Location = new System.Drawing.Point(488, 67);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(220, 372);
            this.tabControl1.TabIndex = 15;
            // 
            // infoTabPage
            // 
            this.infoTabPage.Controls.Add(this.infoPropertyGrid);
            this.infoTabPage.Location = new System.Drawing.Point(4, 22);
            this.infoTabPage.Name = "infoTabPage";
            this.infoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.infoTabPage.Size = new System.Drawing.Size(212, 346);
            this.infoTabPage.TabIndex = 0;
            this.infoTabPage.Text = "Info";
            this.infoTabPage.UseVisualStyleBackColor = true;
            // 
            // configTabPage
            // 
            this.configTabPage.Controls.Add(this.droneConfigPropertyGrid);
            this.configTabPage.Location = new System.Drawing.Point(4, 22);
            this.configTabPage.Name = "configTabPage";
            this.configTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.configTabPage.Size = new System.Drawing.Size(212, 346);
            this.configTabPage.TabIndex = 1;
            this.configTabPage.Text = "Config";
            this.configTabPage.UseVisualStyleBackColor = true;
            // 
            // droneConfigPropertyGrid
            // 
            this.droneConfigPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.droneConfigPropertyGrid.HelpVisible = false;
            this.droneConfigPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.droneConfigPropertyGrid.Name = "droneConfigPropertyGrid";
            this.droneConfigPropertyGrid.Size = new System.Drawing.Size(206, 340);
            this.droneConfigPropertyGrid.TabIndex = 9;
            // 
            // sensorControl1
            // 
            this.sensorControl1.Location = new System.Drawing.Point(12, 264);
            this.sensorControl1.Name = "sensorControl1";
            this.sensorControl1.Size = new System.Drawing.Size(443, 248);
            this.sensorControl1.TabIndex = 16;
            // 
            // motorControl1
            // 
            this.motorControl1.Location = new System.Drawing.Point(6, 3);
            this.motorControl1.Name = "motorControl1";
            this.motorControl1.Size = new System.Drawing.Size(364, 92);
            this.motorControl1.TabIndex = 3;
            // 
            // flightControl1
            // 
            this.flightControl1.Location = new System.Drawing.Point(-3, 0);
            this.flightControl1.Name = "flightControl1";
            this.flightControl1.Size = new System.Drawing.Size(457, 160);
            this.flightControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 530);
            this.Controls.Add(this.sensorControl1);
            this.Controls.Add(this.mainViewTabs);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.armToogleButton);
            this.Controls.Add(this.statusArmedLabel);
            this.Controls.Add(this.pingLabel);
            this.Controls.Add(this.ipInfoLabel);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Drone Control";
            this.mainViewTabs.ResumeLayout(false);
            this.manualControlPage.ResumeLayout(false);
            this.flightControlPage.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.infoTabPage.ResumeLayout(false);
            this.configTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipInfoLabel;
        private System.Windows.Forms.Label pingLabel;
        private System.Windows.Forms.Label statusArmedLabel;
        private System.Windows.Forms.Button armToogleButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PropertyGrid infoPropertyGrid;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TabControl mainViewTabs;
        private System.Windows.Forms.TabPage manualControlPage;
        private MotorControl motorControl1;
        private System.Windows.Forms.TabPage flightControlPage;
        private FlightControl flightControl1;
        private System.Windows.Forms.TabPage configTabPage;
        private System.Windows.Forms.PropertyGrid droneConfigPropertyGrid;
        private System.Windows.Forms.TabPage infoTabPage;
        private System.Windows.Forms.TabControl tabControl1;
        private SensorControl sensorControl1;
    }
}