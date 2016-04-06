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
            this.motorTabPage = new System.Windows.Forms.TabPage();
            this.servoGraph = new DroneControl.QuadGraphControl();
            this.profilerTabPage = new System.Windows.Forms.TabPage();
            this.orientationTabPage = new System.Windows.Forms.TabPage();
            this.orientationGraphList = new DroneControl.GraphListControl();
            this.rotationTabPage = new System.Windows.Forms.TabPage();
            this.rotationGraphList = new DroneControl.GraphListControl();
            this.accelerationTabPage = new System.Windows.Forms.TabPage();
            this.accelerationGraphList = new DroneControl.GraphListControl();
            this.tabControl1.SuspendLayout();
            this.motorTabPage.SuspendLayout();
            this.orientationTabPage.SuspendLayout();
            this.rotationTabPage.SuspendLayout();
            this.accelerationTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.motorTabPage);
            this.tabControl1.Controls.Add(this.profilerTabPage);
            this.tabControl1.Controls.Add(this.orientationTabPage);
            this.tabControl1.Controls.Add(this.rotationTabPage);
            this.tabControl1.Controls.Add(this.accelerationTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(915, 517);
            this.tabControl1.TabIndex = 0;
            // 
            // motorTabPage
            // 
            this.motorTabPage.Controls.Add(this.servoGraph);
            this.motorTabPage.Location = new System.Drawing.Point(4, 22);
            this.motorTabPage.Name = "motorTabPage";
            this.motorTabPage.Size = new System.Drawing.Size(907, 491);
            this.motorTabPage.TabIndex = 2;
            this.motorTabPage.Text = "Motor";
            this.motorTabPage.UseVisualStyleBackColor = true;
            // 
            // servoGraph
            // 
            this.servoGraph.BaseLine = 0D;
            this.servoGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servoGraph.LeftBottomName = "Left bottom";
            this.servoGraph.LeftTopName = "Left front";
            this.servoGraph.Location = new System.Drawing.Point(0, 0);
            this.servoGraph.Name = "servoGraph";
            this.servoGraph.RightBottomName = "Right bottom";
            this.servoGraph.RightTopName = "Right front";
            this.servoGraph.ShowBaseLine = true;
            this.servoGraph.Size = new System.Drawing.Size(907, 491);
            this.servoGraph.TabIndex = 2;
            this.servoGraph.ValueMax = 0D;
            this.servoGraph.ValueMin = 0D;
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
            // orientationTabPage
            // 
            this.orientationTabPage.Controls.Add(this.orientationGraphList);
            this.orientationTabPage.Location = new System.Drawing.Point(4, 22);
            this.orientationTabPage.Name = "orientationTabPage";
            this.orientationTabPage.Size = new System.Drawing.Size(907, 491);
            this.orientationTabPage.TabIndex = 4;
            this.orientationTabPage.Text = "Orientation";
            this.orientationTabPage.UseVisualStyleBackColor = true;
            // 
            // orientationGraphList
            // 
            this.orientationGraphList.AutoScroll = true;
            this.orientationGraphList.BaseLine = 0D;
            this.orientationGraphList.Count = 3;
            this.orientationGraphList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orientationGraphList.Location = new System.Drawing.Point(0, 0);
            this.orientationGraphList.Name = "orientationGraphList";
            this.orientationGraphList.Names = new string[] {
        "Roll",
        "Pitch",
        "Yaw"};
            this.orientationGraphList.ShowBaseLine = true;
            this.orientationGraphList.Size = new System.Drawing.Size(907, 491);
            this.orientationGraphList.TabIndex = 3;
            this.orientationGraphList.ValueMaximums = new double[] {
        90D,
        90D,
        360D};
            this.orientationGraphList.ValueMinimums = new double[] {
        -90D,
        -90D,
        0D};
            // 
            // rotationTabPage
            // 
            this.rotationTabPage.Controls.Add(this.rotationGraphList);
            this.rotationTabPage.Location = new System.Drawing.Point(4, 22);
            this.rotationTabPage.Name = "rotationTabPage";
            this.rotationTabPage.Size = new System.Drawing.Size(907, 491);
            this.rotationTabPage.TabIndex = 5;
            this.rotationTabPage.Text = "Rotation";
            this.rotationTabPage.UseVisualStyleBackColor = true;
            // 
            // rotationGraphList
            // 
            this.rotationGraphList.AutoScroll = true;
            this.rotationGraphList.BaseLine = 0D;
            this.rotationGraphList.Count = 3;
            this.rotationGraphList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotationGraphList.Location = new System.Drawing.Point(0, 0);
            this.rotationGraphList.Name = "rotationGraphList";
            this.rotationGraphList.Names = new string[] {
        "X",
        "Y",
        "Z"};
            this.rotationGraphList.ShowBaseLine = true;
            this.rotationGraphList.Size = new System.Drawing.Size(907, 491);
            this.rotationGraphList.TabIndex = 2;
            this.rotationGraphList.ValueMaximums = new double[] {
        -1.7976931348623157E+308D,
        -1.7976931348623157E+308D,
        -1.7976931348623157E+308D};
            this.rotationGraphList.ValueMinimums = new double[] {
        1.7976931348623157E+308D,
        1.7976931348623157E+308D,
        1.7976931348623157E+308D};
            // 
            // accelerationTabPage
            // 
            this.accelerationTabPage.Controls.Add(this.accelerationGraphList);
            this.accelerationTabPage.Location = new System.Drawing.Point(4, 22);
            this.accelerationTabPage.Name = "accelerationTabPage";
            this.accelerationTabPage.Size = new System.Drawing.Size(907, 491);
            this.accelerationTabPage.TabIndex = 6;
            this.accelerationTabPage.Text = "Acceleration";
            this.accelerationTabPage.UseVisualStyleBackColor = true;
            // 
            // accelerationGraphList
            // 
            this.accelerationGraphList.AutoScroll = true;
            this.accelerationGraphList.BaseLine = 0D;
            this.accelerationGraphList.Count = 3;
            this.accelerationGraphList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accelerationGraphList.Location = new System.Drawing.Point(0, 0);
            this.accelerationGraphList.Name = "accelerationGraphList";
            this.accelerationGraphList.Names = new string[] {
        "X",
        "Y",
        "Z"};
            this.accelerationGraphList.ShowBaseLine = true;
            this.accelerationGraphList.Size = new System.Drawing.Size(907, 491);
            this.accelerationGraphList.TabIndex = 3;
            this.accelerationGraphList.ValueMaximums = new double[] {
        -1.7976931348623157E+308D,
        -1.7976931348623157E+308D,
        -1.7976931348623157E+308D};
            this.accelerationGraphList.ValueMinimums = new double[] {
        1.7976931348623157E+308D,
        1.7976931348623157E+308D,
        1.7976931348623157E+308D};
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
            this.motorTabPage.ResumeLayout(false);
            this.orientationTabPage.ResumeLayout(false);
            this.rotationTabPage.ResumeLayout(false);
            this.accelerationTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage motorTabPage;
        private System.Windows.Forms.TabPage profilerTabPage;
        private System.Windows.Forms.TabControl tabControl1;
        private QuadGraphControl servoGraph;
        private System.Windows.Forms.TabPage orientationTabPage;
        private System.Windows.Forms.TabPage rotationTabPage;
        private System.Windows.Forms.TabPage accelerationTabPage;
        private GraphListControl accelerationGraphList;
        private GraphListControl rotationGraphList;
        private GraphListControl orientationGraphList;
    }
}