namespace ZTSystemConfig.UI
{
    partial class frmMapSettings
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnMapColor = new System.Windows.Forms.Button();
            this.chkdbl = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkcoruscate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.chkAutobreak = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkAutoclip = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkAnti = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkMoutwheel = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.textMaxvertex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Location = new System.Drawing.Point(303, 12);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 10;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnMapColor
            // 
            this.btnMapColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMapColor.Location = new System.Drawing.Point(10, 20);
            this.btnMapColor.Name = "btnMapColor";
            this.btnMapColor.Size = new System.Drawing.Size(269, 23);
            this.btnMapColor.TabIndex = 9;
            this.btnMapColor.Text = "地图窗体背景颜色";
            this.btnMapColor.UseVisualStyleBackColor = true;
            this.btnMapColor.Click += new System.EventHandler(this.btnMapColor_Click);
            // 
            // chkdbl
            // 
            this.chkdbl.Location = new System.Drawing.Point(9, 45);
            this.chkdbl.Name = "chkdbl";
            this.chkdbl.Size = new System.Drawing.Size(150, 23);
            this.chkdbl.TabIndex = 8;
            this.chkdbl.Text = "双击元素是否显示属性";
            // 
            // chkcoruscate
            // 
            this.chkcoruscate.Location = new System.Drawing.Point(9, 16);
            this.chkcoruscate.Name = "chkcoruscate";
            this.chkcoruscate.Size = new System.Drawing.Size(150, 23);
            this.chkcoruscate.TabIndex = 7;
            this.chkcoruscate.Text = "选择元素是否闪烁";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Location = new System.Drawing.Point(303, 41);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.TabIndex = 11;
            this.buttonX2.Text = "取消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // chkAutobreak
            // 
            this.chkAutobreak.Location = new System.Drawing.Point(9, 74);
            this.chkAutobreak.Name = "chkAutobreak";
            this.chkAutobreak.Size = new System.Drawing.Size(150, 23);
            this.chkAutobreak.TabIndex = 12;
            this.chkAutobreak.Text = "自动打断线";
            // 
            // chkAutoclip
            // 
            this.chkAutoclip.Location = new System.Drawing.Point(9, 103);
            this.chkAutoclip.Name = "chkAutoclip";
            this.chkAutoclip.Size = new System.Drawing.Size(150, 23);
            this.chkAutoclip.TabIndex = 13;
            this.chkAutoclip.Text = "自动切割面";
            // 
            // chkAnti
            // 
            this.chkAnti.Location = new System.Drawing.Point(10, 133);
            this.chkAnti.Name = "chkAnti";
            this.chkAnti.Size = new System.Drawing.Size(149, 23);
            this.chkAnti.TabIndex = 14;
            this.chkAnti.Text = "反走样显示底图";
            // 
            // chkMoutwheel
            // 
            this.chkMoutwheel.Location = new System.Drawing.Point(10, 162);
            this.chkMoutwheel.Name = "chkMoutwheel";
            this.chkMoutwheel.Size = new System.Drawing.Size(163, 23);
            this.chkMoutwheel.TabIndex = 15;
            this.chkMoutwheel.Text = "鼠标滚轮设置屏幕中心";
            // 
            // textMaxvertex
            // 
            this.textMaxvertex.Location = new System.Drawing.Point(185, 32);
            this.textMaxvertex.Name = "textMaxvertex";
            this.textMaxvertex.Size = new System.Drawing.Size(94, 21);
            this.textMaxvertex.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "可显示几何对象的最大节点数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkdbl);
            this.groupBox1.Controls.Add(this.chkcoruscate);
            this.groupBox1.Controls.Add(this.chkAutobreak);
            this.groupBox1.Controls.Add(this.chkMoutwheel);
            this.groupBox1.Controls.Add(this.chkAutoclip);
            this.groupBox1.Controls.Add(this.chkAnti);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 191);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "开关设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnMapColor);
            this.groupBox2.Location = new System.Drawing.Point(12, 288);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 58);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "风格设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.textMaxvertex);
            this.groupBox3.Location = new System.Drawing.Point(12, 209);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(285, 73);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "容限设置";
            // 
            // frmMapSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 358);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmMapSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图窗口初始设置";
            this.Load += new System.EventHandler(this.frmMapSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.Button btnMapColor;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkdbl;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkcoruscate;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAutobreak;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAutoclip;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAnti;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkMoutwheel;
        private System.Windows.Forms.TextBox textMaxvertex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}