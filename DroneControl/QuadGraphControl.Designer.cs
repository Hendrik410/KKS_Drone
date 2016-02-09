namespace DroneControl
{
    partial class QuadGraphControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.leftTop = new DroneControl.Graph();
            this.rightTop = new DroneControl.Graph();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.leftBottom = new DroneControl.Graph();
            this.rightBottom = new DroneControl.Graph();
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
            this.splitContainer1.Size = new System.Drawing.Size(901, 485);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.leftTop);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rightTop);
            this.splitContainer2.Size = new System.Drawing.Size(901, 227);
            this.splitContainer2.SplitterDistance = 422;
            this.splitContainer2.TabIndex = 0;
            // 
            // leftTop
            // 
            this.leftTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTop.Location = new System.Drawing.Point(0, 0);
            this.leftTop.Name = "leftTop";
            this.leftTop.Size = new System.Drawing.Size(422, 227);
            this.leftTop.TabIndex = 0;
            this.leftTop.Titel = "Left top";
            // 
            // rightTop
            // 
            this.rightTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightTop.Location = new System.Drawing.Point(0, 0);
            this.rightTop.Name = "rightTop";
            this.rightTop.Size = new System.Drawing.Size(475, 227);
            this.rightTop.TabIndex = 0;
            this.rightTop.Titel = "Right top";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.leftBottom);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.rightBottom);
            this.splitContainer3.Size = new System.Drawing.Size(901, 254);
            this.splitContainer3.SplitterDistance = 422;
            this.splitContainer3.TabIndex = 0;
            // 
            // leftBottom
            // 
            this.leftBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftBottom.Location = new System.Drawing.Point(0, 0);
            this.leftBottom.Name = "leftBottom";
            this.leftBottom.Size = new System.Drawing.Size(422, 254);
            this.leftBottom.TabIndex = 0;
            this.leftBottom.Titel = "Left bottom";
            // 
            // rightBottom
            // 
            this.rightBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightBottom.Location = new System.Drawing.Point(0, 0);
            this.rightBottom.Name = "rightBottom";
            this.rightBottom.Size = new System.Drawing.Size(475, 254);
            this.rightBottom.TabIndex = 0;
            this.rightBottom.Titel = "Right bottom";
            // 
            // QuadGraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "QuadGraphControl";
            this.Size = new System.Drawing.Size(901, 485);
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

        private Graph rightBottom;
        private Graph leftBottom;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Graph rightTop;
        private Graph leftTop;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
