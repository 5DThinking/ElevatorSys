namespace ElevatorSys.UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtOverCount = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtUnderCount = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtElevatorCount = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCreate = new System.Windows.Forms.ToolStripButton();
            this.btnDispose = new System.Windows.Forms.ToolStripButton();
            this.btnCreatePerson = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 34);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(845, 334);
            this.panel1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(481, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(359, 324);
            this.panel3.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 324);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运行刻录";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLog.Location = new System.Drawing.Point(3, 24);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(353, 297);
            this.txtLog.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(274, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 324);
            this.panel2.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 324);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "楼层控制器";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "elevator_128px_1196031_easyicon.net.ico");
            this.imageList1.Images.SetKeyName(1, "elevator_32px_1209769_easyicon.net.ico");
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 36);
            this.toolStripLabel1.Text = "地上楼层：";
            // 
            // txtOverCount
            // 
            this.txtOverCount.AutoSize = false;
            this.txtOverCount.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.txtOverCount.Name = "txtOverCount";
            this.txtOverCount.Size = new System.Drawing.Size(55, 32);
            this.txtOverCount.Text = "10";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(100, 36);
            this.toolStripLabel3.Text = "地下楼层：";
            // 
            // txtUnderCount
            // 
            this.txtUnderCount.AutoSize = false;
            this.txtUnderCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.txtUnderCount.Name = "txtUnderCount";
            this.txtUnderCount.Size = new System.Drawing.Size(50, 32);
            this.txtUnderCount.Text = "3";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(100, 36);
            this.toolStripLabel2.Text = "电梯数量：";
            // 
            // txtElevatorCount
            // 
            this.txtElevatorCount.AutoSize = false;
            this.txtElevatorCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.txtElevatorCount.Name = "txtElevatorCount";
            this.txtElevatorCount.Size = new System.Drawing.Size(50, 32);
            this.txtElevatorCount.Text = "2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtOverCount,
            this.toolStripLabel3,
            this.txtUnderCount,
            this.toolStripLabel2,
            this.txtElevatorCount,
            this.toolStripSeparator1,
            this.btnCreate,
            this.btnDispose,
            this.btnCreatePerson});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(845, 32);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCreate
            // 
            this.btnCreate.Image = global::ElevatorSys.UI.Properties.Resources.elevator_32px_1209769_easyicon_net;
            this.btnCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(82, 36);
            this.btnCreate.Text = "生成";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnDispose
            // 
            this.btnDispose.Enabled = false;
            this.btnDispose.Image = global::ElevatorSys.UI.Properties.Resources.delete_32px_1231124_easyicon_net;
            this.btnDispose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDispose.Name = "btnDispose";
            this.btnDispose.Size = new System.Drawing.Size(74, 29);
            this.btnDispose.Text = "销毁";
            this.btnDispose.Click += new System.EventHandler(this.btnDispose_Click);
            // 
            // btnCreatePerson
            // 
            this.btnCreatePerson.Enabled = false;
            this.btnCreatePerson.Image = global::ElevatorSys.UI.Properties.Resources.elevator_32px_1152200_easyicon_net;
            this.btnCreatePerson.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreatePerson.Name = "btnCreatePerson";
            this.btnCreatePerson.Size = new System.Drawing.Size(110, 29);
            this.btnCreatePerson.Text = "生成乘客";
            this.btnCreatePerson.Click += new System.EventHandler(this.btnCreatePerson_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 370);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "电梯模拟程序 [QQ:3150379五维思考]";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox txtOverCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox txtUnderCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox txtElevatorCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCreate;
        private System.Windows.Forms.ToolStripButton btnDispose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ToolStripButton btnCreatePerson;
    }
}

