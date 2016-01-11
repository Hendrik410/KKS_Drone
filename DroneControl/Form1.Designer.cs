namespace DroneControl {
    partial class MainWindow {
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.txtControllerDump = new System.Windows.Forms.TextBox();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtControllerDump
            // 
            this.txtControllerDump.Enabled = false;
            this.txtControllerDump.Location = new System.Drawing.Point(46, 63);
            this.txtControllerDump.Multiline = true;
            this.txtControllerDump.Name = "txtControllerDump";
            this.txtControllerDump.Size = new System.Drawing.Size(493, 146);
            this.txtControllerDump.TabIndex = 0;
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 309);
            this.Controls.Add(this.txtControllerDump);
            this.Name = "MainWindow";
            this.Text = "Drone Control";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtControllerDump;
        private System.Windows.Forms.Timer RefreshTimer;
    }
}

