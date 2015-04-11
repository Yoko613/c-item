namespace ZTSupermap.UI
{
    partial class ImgBandCombine
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbRedDT = new System.Windows.Forms.ComboBox();
            this.cmbRedDS = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGreenDT = new System.Windows.Forms.ComboBox();
            this.cmbGreenDS = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBlueDT = new System.Windows.Forms.ComboBox();
            this.cmbBlueDS = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCombineDT = new System.Windows.Forms.TextBox();
            this.cmbCombineDS = new System.Windows.Forms.ComboBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.combEncode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbRedDT);
            this.groupBox1.Controls.Add(this.cmbRedDS);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "红波段";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据集：";
            // 
            // cmbRedDT
            // 
            this.cmbRedDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRedDT.FormattingEnabled = true;
            this.cmbRedDT.Location = new System.Drawing.Point(64, 43);
            this.cmbRedDT.Name = "cmbRedDT";
            this.cmbRedDT.Size = new System.Drawing.Size(190, 20);
            this.cmbRedDT.TabIndex = 1;
            this.cmbRedDT.SelectedIndexChanged += new System.EventHandler(this.cmbRedDT_SelectedIndexChanged);
            // 
            // cmbRedDS
            // 
            this.cmbRedDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRedDS.FormattingEnabled = true;
            this.cmbRedDS.Location = new System.Drawing.Point(64, 15);
            this.cmbRedDS.Name = "cmbRedDS";
            this.cmbRedDS.Size = new System.Drawing.Size(190, 20);
            this.cmbRedDS.TabIndex = 0;
            this.cmbRedDS.SelectedIndexChanged += new System.EventHandler(this.cmbRedDS_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbGreenDT);
            this.groupBox2.Controls.Add(this.cmbGreenDS);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 76);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "绿波段";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "数据集：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据源：";
            // 
            // cmbGreenDT
            // 
            this.cmbGreenDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGreenDT.FormattingEnabled = true;
            this.cmbGreenDT.Location = new System.Drawing.Point(64, 47);
            this.cmbGreenDT.Name = "cmbGreenDT";
            this.cmbGreenDT.Size = new System.Drawing.Size(191, 20);
            this.cmbGreenDT.TabIndex = 1;
            // 
            // cmbGreenDS
            // 
            this.cmbGreenDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGreenDS.FormattingEnabled = true;
            this.cmbGreenDS.Location = new System.Drawing.Point(64, 16);
            this.cmbGreenDS.Name = "cmbGreenDS";
            this.cmbGreenDS.Size = new System.Drawing.Size(191, 20);
            this.cmbGreenDS.TabIndex = 0;
            this.cmbGreenDS.SelectedIndexChanged += new System.EventHandler(this.cmbGreenDS_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbBlueDT);
            this.groupBox3.Controls.Add(this.cmbBlueDS);
            this.groupBox3.Location = new System.Drawing.Point(12, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 76);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "蓝波段";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "数据集：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "数据源：";
            // 
            // cmbBlueDT
            // 
            this.cmbBlueDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBlueDT.FormattingEnabled = true;
            this.cmbBlueDT.Location = new System.Drawing.Point(64, 47);
            this.cmbBlueDT.Name = "cmbBlueDT";
            this.cmbBlueDT.Size = new System.Drawing.Size(191, 20);
            this.cmbBlueDT.TabIndex = 1;
            // 
            // cmbBlueDS
            // 
            this.cmbBlueDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBlueDS.FormattingEnabled = true;
            this.cmbBlueDS.Location = new System.Drawing.Point(64, 20);
            this.cmbBlueDS.Name = "cmbBlueDS";
            this.cmbBlueDS.Size = new System.Drawing.Size(191, 20);
            this.cmbBlueDS.TabIndex = 0;
            this.cmbBlueDS.SelectedIndexChanged += new System.EventHandler(this.cmbBlueDS_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.combEncode);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtCombineDT);
            this.groupBox4.Controls.Add(this.cmbCombineDS);
            this.groupBox4.Location = new System.Drawing.Point(12, 247);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 101);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "合成影像";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "数据集：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "数据源：";
            // 
            // txtCombineDT
            // 
            this.txtCombineDT.Location = new System.Drawing.Point(64, 47);
            this.txtCombineDT.Name = "txtCombineDT";
            this.txtCombineDT.Size = new System.Drawing.Size(191, 21);
            this.txtCombineDT.TabIndex = 1;
            // 
            // cmbCombineDS
            // 
            this.cmbCombineDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCombineDS.FormattingEnabled = true;
            this.cmbCombineDS.Location = new System.Drawing.Point(64, 20);
            this.cmbCombineDS.Name = "cmbCombineDS";
            this.cmbCombineDS.Size = new System.Drawing.Size(190, 20);
            this.cmbCombineDS.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(99, 408);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "确　定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(192, 408);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.TabIndex = 5;
            this.buttonX2.Text = "取　消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 357);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(263, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "说明：红、绿、蓝三种波段数据集都为256色栅格";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 380);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(209, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "数据集，并且长宽像素数必须全部相同";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "编  码：";
            // 
            // combEncode
            // 
            this.combEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combEncode.FormattingEnabled = true;
            this.combEncode.Items.AddRange(new object[] {
            "未编码",
            "DCT",
            "LZW"});
            this.combEncode.Location = new System.Drawing.Point(64, 75);
            this.combEncode.Name = "combEncode";
            this.combEncode.Size = new System.Drawing.Size(190, 20);
            this.combEncode.TabIndex = 5;
            // 
            // ImgBandCombine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 443);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImgBandCombine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单波段合成彩色图像";
            this.Load += new System.EventHandler(this.ImgBandCombine_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbRedDT;
        private System.Windows.Forms.ComboBox cmbRedDS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbGreenDT;
        private System.Windows.Forms.ComboBox cmbGreenDS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbBlueDT;
        private System.Windows.Forms.ComboBox cmbBlueDS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtCombineDT;
        private System.Windows.Forms.ComboBox cmbCombineDS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox combEncode;
        private System.Windows.Forms.Label label11;
    }
}