namespace DroneControl
{
    partial class DebugForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            this.resetButton = new System.Windows.Forms.Button();
            this.blinkButton = new System.Windows.Forms.Button();
            this.profilerData = new System.Windows.Forms.Label();
            this.recorderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(13, 13);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // blinkButton
            // 
            this.blinkButton.Location = new System.Drawing.Point(94, 13);
            this.blinkButton.Name = "blinkButton";
            this.blinkButton.Size = new System.Drawing.Size(75, 23);
            this.blinkButton.TabIndex = 2;
            this.blinkButton.Text = "Blink";
            this.blinkButton.UseVisualStyleBackColor = true;
            this.blinkButton.Click += new System.EventHandler(this.blinkButton_Click);
            // 
            // profilerData
            // 
            this.profilerData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.profilerData.AutoSize = true;
            this.profilerData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profilerData.Location = new System.Drawing.Point(12, 39);
            this.profilerData.Name = "profilerData";
            this.profilerData.Size = new System.Drawing.Size(85, 13);
            this.profilerData.TabIndex = 4;
            this.profilerData.Text = "Profiler data";
            // 
            // recorderButton
            // 
            this.recorderButton.Location = new System.Drawing.Point(176, 13);
            this.recorderButton.Name = "recorderButton";
            this.recorderButton.Size = new System.Drawing.Size(75, 23);
            this.recorderButton.TabIndex = 5;
            this.recorderButton.Text = "Recorder";
            this.recorderButton.UseVisualStyleBackColor = true;
            this.recorderButton.Click += new System.EventHandler(this.recorderButton_Click);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 315);
            this.Controls.Add(this.recorderButton);
            this.Controls.Add(this.profilerData);
            this.Controls.Add(this.blinkButton);
            this.Controls.Add(this.resetButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DebugForm";
            this.Text = "Debug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button blinkButton;
        private System.Windows.Forms.Label profilerData;
        private System.Windows.Forms.Button recorderButton;
    }
}