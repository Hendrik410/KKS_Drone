namespace DroneControl
{
    partial class GraphForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.frontLeftRatio = new DroneControl.Graph();
            this.frontRightRatio = new DroneControl.Graph();
            this.backLeftRatio = new DroneControl.Graph();
            this.backRightRatio = new DroneControl.Graph();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(915, 517);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.frontLeftRatio);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.frontRightRatio);
            this.splitContainer2.Size = new System.Drawing.Size(915, 242);
            this.splitContainer2.SplitterDistance = 429;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.backLeftRatio);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.backRightRatio);
            this.splitContainer3.Size = new System.Drawing.Size(915, 271);
            this.splitContainer3.SplitterDistance = 429;
            this.splitContainer3.TabIndex = 0;
            // 
            // frontLeftRatio
            // 
            this.frontLeftRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frontLeftRatio.Location = new System.Drawing.Point(0, 0);
            this.frontLeftRatio.Name = "frontLeftRatio";
            this.frontLeftRatio.Size = new System.Drawing.Size(429, 242);
            this.frontLeftRatio.TabIndex = 0;
            // 
            // frontRightRatio
            // 
            this.frontRightRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frontRightRatio.Location = new System.Drawing.Point(0, 0);
            this.frontRightRatio.Name = "frontRightRatio";
            this.frontRightRatio.Size = new System.Drawing.Size(482, 242);
            this.frontRightRatio.TabIndex = 0;
            // 
            // backLeftRatio
            // 
            this.backLeftRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backLeftRatio.Location = new System.Drawing.Point(0, 0);
            this.backLeftRatio.Name = "backLeftRatio";
            this.backLeftRatio.Size = new System.Drawing.Size(429, 271);
            this.backLeftRatio.TabIndex = 0;
            // 
            // backRightRatio
            // 
            this.backRightRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backRightRatio.Location = new System.Drawing.Point(0, 0);
            this.backRightRatio.Name = "backRightRatio";
            this.backRightRatio.Size = new System.Drawing.Size(482, 271);
            this.backRightRatio.TabIndex = 0;
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 517);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Graph frontLeftRatio;
        private Graph frontRightRatio;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Graph backLeftRatio;
        private Graph backRightRatio;
    }
}