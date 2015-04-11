namespace ZTSupermap.UI
{
    partial class BatchImgCombine
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
            this.cmbDS = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkDeleteSingleBound = new System.Windows.Forms.CheckBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkDCT = new System.Windows.Forms.CheckBox();
            this.chkPyramid = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDS
            // 
            this.cmbDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDS.FormattingEnabled = true;
            this.cmbDS.Location = new System.Drawing.Point(80, 20);
            this.cmbDS.Name = "cmbDS";
            this.cmbDS.Size = new System.Drawing.Size(181, 20);
            this.cmbDS.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "图像导入后如果生成 *_0,*_1,*_2三个栅格数据集";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "其中*_0 为红色波段；*_1 为绿色波段；";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "*_2为蓝色波段；";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(269, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "处理的结果合并成一个彩色图像，数据集名为原来";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(269, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "数据集_0前的名称，原来单波段数据可以被删除。";
            // 
            // chkDeleteSingleBound
            // 
            this.chkDeleteSingleBound.AutoSize = true;
            this.chkDeleteSingleBound.Checked = true;
            this.chkDeleteSingleBound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteSingleBound.Location = new System.Drawing.Point(23, 56);
            this.chkDeleteSingleBound.Name = "chkDeleteSingleBound";
            this.chkDeleteSingleBound.Size = new System.Drawing.Size(96, 16);
            this.chkDeleteSingleBound.TabIndex = 7;
            this.chkDeleteSingleBound.Text = "删除单色波段";
            this.chkDeleteSingleBound.UseVisualStyleBackColor = true;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(95, 20);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 8;
            this.buttonX1.Text = "确 定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(186, 20);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.TabIndex = 9;
            this.buttonX2.Text = "取　消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPyramid);
            this.groupBox1.Controls.Add(this.chkDCT);
            this.groupBox1.Controls.Add(this.cmbDS);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkDeleteSingleBound);
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 96);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "合成选项";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonX1);
            this.groupBox2.Controls.Add(this.buttonX2);
            this.groupBox2.Location = new System.Drawing.Point(11, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 52);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 281);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(264, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // chkDCT
            // 
            this.chkDCT.AutoSize = true;
            this.chkDCT.Checked = true;
            this.chkDCT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDCT.Location = new System.Drawing.Point(152, 56);
            this.chkDCT.Name = "chkDCT";
            this.chkDCT.Size = new System.Drawing.Size(66, 16);
            this.chkDCT.TabIndex = 8;
            this.chkDCT.Text = "DCT压缩";
            this.chkDCT.UseVisualStyleBackColor = true;
            // 
            // chkPyramid
            // 
            this.chkPyramid.AutoSize = true;
            this.chkPyramid.Checked = true;
            this.chkPyramid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPyramid.Location = new System.Drawing.Point(152, 74);
            this.chkPyramid.Name = "chkPyramid";
            this.chkPyramid.Size = new System.Drawing.Size(84, 16);
            this.chkPyramid.TabIndex = 9;
            this.chkPyramid.Text = "创建金字塔";
            this.chkPyramid.UseVisualStyleBackColor = true;
            // 
            // BatchImgCombine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 310);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchImgCombine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量彩色图像合成";
            this.Load += new System.EventHandler(this.BatchImgCombine_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDeleteSingleBound;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkPyramid;
        private System.Windows.Forms.CheckBox chkDCT;
    }
}