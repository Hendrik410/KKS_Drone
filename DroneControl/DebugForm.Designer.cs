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
            this.ratioDataLabel = new System.Windows.Forms.Label();
            this.blinkButton = new System.Windows.Forms.Button();
            this.correctionDataLabel = new System.Windows.Forms.Label();
            this.profilerData = new System.Windows.Forms.Label();
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
            // ratioDataLabel
            // 
            this.ratioDataLabel.AutoSize = true;
            this.ratioDataLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratioDataLabel.Location = new System.Drawing.Point(13, 43);
            this.ratioDataLabel.Name = "ratioDataLabel";
            this.ratioDataLabel.Size = new System.Drawing.Size(67, 13);
            this.ratioDataLabel.TabIndex = 1;
            this.ratioDataLabel.Text = "Ratio data";
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
            // correctionDataLabel
            // 
            this.correctionDataLabel.AutoSize = true;
            this.correctionDataLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correctionDataLabel.Location = new System.Drawing.Point(13, 165);
            this.correctionDataLabel.Name = "correctionDataLabel";
            this.correctionDataLabel.Size = new System.Drawing.Size(97, 13);
            this.correctionDataLabel.TabIndex = 3;
            this.correctionDataLabel.Text = "Correction data";
            // 
            // profilerData
            // 
            this.profilerData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.profilerData.AutoSize = true;
            this.profilerData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profilerData.Location = new System.Drawing.Point(168, 43);
            this.profilerData.Name = "profilerData";
            this.profilerData.Size = new System.Drawing.Size(85, 13);
            this.profilerData.TabIndex = 4;
            this.profilerData.Text = "Profiler data";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 315);
            this.Controls.Add(this.profilerData);
            this.Controls.Add(this.correctionDataLabel);
            this.Controls.Add(this.blinkButton);
            this.Controls.Add(this.ratioDataLabel);
            this.Controls.Add(this.resetButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DebugForm";
            this.Text = "Debug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label ratioDataLabel;
        private System.Windows.Forms.Button blinkButton;
        private System.Windows.Forms.Label correctionDataLabel;
        private System.Windows.Forms.Label profilerData;
    }
}