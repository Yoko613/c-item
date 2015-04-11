namespace ZTSupermap.UI
{
    partial class ProjectionSet
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetP = new DevComponents.DotNetBar.ButtonX();
            this.combDS = new System.Windows.Forms.ComboBox();
            this.radioSet = new System.Windows.Forms.RadioButton();
            this.radDatasource = new System.Windows.Forms.RadioButton();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetP);
            this.groupBox1.Controls.Add(this.combDS);
            this.groupBox1.Controls.Add(this.radioSet);
            this.groupBox1.Controls.Add(this.radDatasource);
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "投影来源";
            // 
            // btnSetP
            // 
            this.btnSetP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetP.Location = new System.Drawing.Point(129, 49);
            this.btnSetP.Name = "btnSetP";
            this.btnSetP.Size = new System.Drawing.Size(166, 23);
            this.btnSetP.TabIndex = 3;
            this.btnSetP.Text = "指 定 投 影...";
            this.btnSetP.Click += new System.EventHandler(this.btnSetP_Click);
            // 
            // combDS
            // 
            this.combDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDS.FormattingEnabled = true;
            this.combDS.Location = new System.Drawing.Point(129, 19);
            this.combDS.Name = "combDS";
            this.combDS.Size = new System.Drawing.Size(166, 20);
            this.combDS.TabIndex = 2;
            // 
            // radioSet
            // 
            this.radioSet.AutoSize = true;
            this.radioSet.Location = new System.Drawing.Point(6, 53);
            this.radioSet.Name = "radioSet";
            this.radioSet.Size = new System.Drawing.Size(71, 16);
            this.radioSet.TabIndex = 1;
            this.radioSet.TabStop = true;
            this.radioSet.Text = "重新指定";
            this.radioSet.UseVisualStyleBackColor = true;
            this.radioSet.CheckedChanged += new System.EventHandler(this.radioSet_CheckedChanged);
            // 
            // radDatasource
            // 
            this.radDatasource.AutoSize = true;
            this.radDatasource.Location = new System.Drawing.Point(6, 20);
            this.radDatasource.Name = "radDatasource";
            this.radDatasource.Size = new System.Drawing.Size(95, 16);
            this.radDatasource.TabIndex = 0;
            this.radDatasource.TabStop = true;
            this.radDatasource.Text = "从数据源复制";
            this.radDatasource.UseVisualStyleBackColor = true;
            this.radDatasource.CheckedChanged += new System.EventHandler(this.radDatasource_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(141, 92);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确 定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(232, 92);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取 消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // ProjectionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 125);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectionSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "获取投影";            
            this.Load += new System.EventHandler(this.ProjectionSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combDS;
        private System.Windows.Forms.RadioButton radioSet;
        private System.Windows.Forms.RadioButton radDatasource;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnSetP;
    }
}