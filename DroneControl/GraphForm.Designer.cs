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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ratioTabPage = new System.Windows.Forms.TabPage();
            this.ratiosGraph = new DroneControl.QuadGraphControl();
            this.correctionTabPage = new System.Windows.Forms.TabPage();
            this.correctionGraph = new DroneControl.QuadGraphControl();
            this.motorTabPage = new System.Windows.Forms.TabPage();
            this.profilerTabPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.ratioTabPage.SuspendLayout();
            this.correctionTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ratioTabPage);
            this.tabControl1.Controls.Add(this.correctionTabPage);
            this.tabControl1.Controls.Add(this.motorTabPage);
            this.tabControl1.Controls.Add(this.profilerTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(915, 517);
            this.tabControl1.TabIndex = 0;
            // 
            // ratioTabPage
            // 
            this.ratioTabPage.Controls.Add(this.ratiosGraph);
            this.ratioTabPage.Location = new System.Drawing.Point(4, 22);
            this.ratioTabPage.Name = "ratioTabPage";
            this.ratioTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ratioTabPage.Size = new System.Drawing.Size(907, 491);
            this.ratioTabPage.TabIndex = 0;
            this.ratioTabPage.Text = "Ratios";
            this.ratioTabPage.UseVisualStyleBackColor = true;
            // 
            // ratiosGraph
            // 
            this.ratiosGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ratiosGraph.LeftBottomName = "Left bottom";
            this.ratiosGraph.LeftTopName = "Left front";
            this.ratiosGraph.Location = new System.Drawing.Point(3, 3);
            this.ratiosGraph.Name = "ratiosGraph";
            this.ratiosGraph.RightBottomName = "Right bottom";
            this.ratiosGraph.RightTopName = "Right front";
            this.ratiosGraph.Size = new System.Drawing.Size(901, 485);
            this.ratiosGraph.TabIndex = 1;
            // 
            // correctionTabPage
            // 
            this.correctionTabPage.Controls.Add(this.correctionGraph);
            this.correctionTabPage.Location = new System.Drawing.Point(4, 22);
            this.correctionTabPage.Name = "correctionTabPage";
            this.correctionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.correctionTabPage.Size = new System.Drawing.Size(907, 491);
            this.correctionTabPage.TabIndex = 1;
            this.correctionTabPage.Text = "Correction";
            this.correctionTabPage.UseVisualStyleBackColor = true;
            // 
            // correctionGraph
            // 
            this.correctionGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.correctionGraph.LeftBottomName = "Left bottom";
            this.correctionGraph.LeftTopName = "Left front";
            this.correctionGraph.Location = new System.Drawing.Point(3, 3);
            this.correctionGraph.Name = "correctionGraph";
            this.correctionGraph.RightBottomName = "Right bottom";
            this.correctionGraph.RightTopName = "Right front";
            this.correctionGraph.Size = new System.Drawing.Size(901, 485);
            this.correctionGraph.TabIndex = 1;
            // 
            // motorTabPage
            // 
            this.motorTabPage.Location = new System.Drawing.Point(4, 22);
            this.motorTabPage.Name = "motorTabPage";
            this.motorTabPage.Size = new System.Drawing.Size(907, 491);
            this.motorTabPage.TabIndex = 2;
            this.motorTabPage.Text = "Motor";
            this.motorTabPage.UseVisualStyleBackColor = true;
            this.motorTabPage.Click += new System.EventHandler(this.motorTabPage_Click);
            // 
            // profilerTabPage
            // 
            this.profilerTabPage.Location = new System.Drawing.Point(4, 22);
            this.profilerTabPage.Name = "profilerTabPage";
            this.profilerTabPage.Size = new System.Drawing.Size(907, 491);
            this.profilerTabPage.TabIndex = 3;
            this.profilerTabPage.Text = "Profiler";
            this.profilerTabPage.UseVisualStyleBackColor = true;
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 517);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GraphForm";
            this.Text = "Debug - Graphs";
            this.tabControl1.ResumeLayout(false);
            this.ratioTabPage.ResumeLayout(false);
            this.correctionTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage ratioTabPage;
        private System.Windows.Forms.TabPage correctionTabPage;
        private System.Windows.Forms.TabPage motorTabPage;
        private System.Windows.Forms.TabPage profilerTabPage;
        private System.Windows.Forms.TabControl tabControl1;
        private QuadGraphControl correctionGraph;
        private QuadGraphControl ratiosGraph;
    }
}