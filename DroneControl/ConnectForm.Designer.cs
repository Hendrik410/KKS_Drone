namespace DroneControl
{
    partial class ConnectForm
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
            System.Windows.Forms.Label ipAddressLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
            this.connectButton = new System.Windows.Forms.Button();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.dronenList = new System.Windows.Forms.ListBox();
            this.searchStatus = new System.Windows.Forms.Label();
            ipAddressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipAddressLabel
            // 
            ipAddressLabel.AutoSize = true;
            ipAddressLabel.Location = new System.Drawing.Point(12, 15);
            ipAddressLabel.Name = "ipAddressLabel";
            ipAddressLabel.Size = new System.Drawing.Size(58, 13);
            ipAddressLabel.TabIndex = 2;
            ipAddressLabel.Text = "IP-Address";
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(175, 198);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(76, 12);
            this.ipAddressTextBox.MaxLength = 15;
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(173, 20);
            this.ipAddressTextBox.TabIndex = 3;
            this.ipAddressTextBox.TextChanged += new System.EventHandler(this.ipAddressTextBox_TextChanged);
            // 
            // dronenList
            // 
            this.dronenList.FormattingEnabled = true;
            this.dronenList.Location = new System.Drawing.Point(15, 53);
            this.dronenList.Name = "dronenList";
            this.dronenList.Size = new System.Drawing.Size(234, 134);
            this.dronenList.TabIndex = 4;
            // 
            // searchStatus
            // 
            this.searchStatus.AutoSize = true;
            this.searchStatus.Location = new System.Drawing.Point(12, 35);
            this.searchStatus.Name = "searchStatus";
            this.searchStatus.Size = new System.Drawing.Size(90, 13);
            this.searchStatus.TabIndex = 5;
            this.searchStatus.Text = "Searching drones";
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 233);
            this.Controls.Add(this.searchStatus);
            this.Controls.Add(this.dronenList);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(ipAddressLabel);
            this.Controls.Add(this.connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectForm";
            this.Text = "ConnectForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.ListBox dronenList;
        private System.Windows.Forms.Label searchStatus;
    }
}