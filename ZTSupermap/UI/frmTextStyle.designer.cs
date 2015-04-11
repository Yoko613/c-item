namespace ZTSupermap.UI
{
    partial class frmTextStyle
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
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.chkUserDefine = new System.Windows.Forms.CheckBox();
            this.chkGDDX = new System.Windows.Forms.CheckBox();
            this.chkYY = new System.Windows.Forms.CheckBox();
            this.chkBJTM = new System.Windows.Forms.CheckBox();
            this.chxLK = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAlign = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxRotation = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQJ_Color = new System.Windows.Forms.Button();
            this.pbx_QJ_Color = new System.Windows.Forms.PictureBox();
            this.pbx_BJ_Color = new System.Windows.Forms.PictureBox();
            this.btnBJ_Color = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxFontRotation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_QJ_Color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_BJ_Color)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.chkUserDefine);
            this.groupBox1.Controls.Add(this.chkGDDX);
            this.groupBox1.Controls.Add(this.chkYY);
            this.groupBox1.Controls.Add(this.chkBJTM);
            this.groupBox1.Controls.Add(this.chxLK);
            this.groupBox1.Location = new System.Drawing.Point(192, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字体效果";
            // 
            // txtWidth
            // 
            this.txtWidth.BackColor = System.Drawing.Color.Silver;
            this.txtWidth.Enabled = false;
            this.txtWidth.Location = new System.Drawing.Point(121, 85);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(55, 21);
            this.txtWidth.TabIndex = 12;
            this.txtWidth.Text = "0";
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "宽:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "高:";
            // 
            // txtHeight
            // 
            this.txtHeight.BackColor = System.Drawing.Color.Silver;
            this.txtHeight.Enabled = false;
            this.txtHeight.Location = new System.Drawing.Point(38, 85);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(55, 21);
            this.txtHeight.TabIndex = 9;
            this.txtHeight.Text = "0";
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            // 
            // chkUserDefine
            // 
            this.chkUserDefine.AutoSize = true;
            this.chkUserDefine.Location = new System.Drawing.Point(15, 66);
            this.chkUserDefine.Name = "chkUserDefine";
            this.chkUserDefine.Size = new System.Drawing.Size(114, 16);
            this.chkUserDefine.TabIndex = 8;
            this.chkUserDefine.Text = "自定义字体宽高:";
            this.chkUserDefine.UseVisualStyleBackColor = true;
            this.chkUserDefine.CheckedChanged += new System.EventHandler(this.chkUserDefine_CheckedChanged);
            // 
            // chkGDDX
            // 
            this.chkGDDX.AutoSize = true;
            this.chkGDDX.Checked = true;
            this.chkGDDX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGDDX.Location = new System.Drawing.Point(84, 44);
            this.chkGDDX.Name = "chkGDDX";
            this.chkGDDX.Size = new System.Drawing.Size(90, 16);
            this.chkGDDX.TabIndex = 7;
            this.chkGDDX.Text = "固定大小(&G)";
            this.chkGDDX.UseVisualStyleBackColor = true;
            // 
            // chkYY
            // 
            this.chkYY.AutoSize = true;
            this.chkYY.Location = new System.Drawing.Point(15, 44);
            this.chkYY.Name = "chkYY";
            this.chkYY.Size = new System.Drawing.Size(66, 16);
            this.chkYY.TabIndex = 6;
            this.chkYY.Text = "阴影(&D)";
            this.chkYY.UseVisualStyleBackColor = true;
            // 
            // chkBJTM
            // 
            this.chkBJTM.AutoSize = true;
            this.chkBJTM.Checked = true;
            this.chkBJTM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBJTM.Location = new System.Drawing.Point(84, 20);
            this.chkBJTM.Name = "chkBJTM";
            this.chkBJTM.Size = new System.Drawing.Size(90, 16);
            this.chkBJTM.TabIndex = 5;
            this.chkBJTM.Text = "背景透明(&T)";
            this.chkBJTM.UseVisualStyleBackColor = true;
            // 
            // chxLK
            // 
            this.chxLK.AutoSize = true;
            this.chxLK.Location = new System.Drawing.Point(15, 20);
            this.chxLK.Name = "chxLK";
            this.chxLK.Size = new System.Drawing.Size(66, 16);
            this.chxLK.TabIndex = 4;
            this.chxLK.Text = "轮廓(&L)";
            this.chxLK.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "对齐方式(&A)";
            // 
            // cbxAlign
            // 
            this.cbxAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAlign.FormattingEnabled = true;
            this.cbxAlign.Items.AddRange(new object[] {
            "左上角对齐",
            "居中靠上对齐",
            "右上角对齐",
            "基线左对齐",
            "基线居中对齐",
            "基线右对齐",
            "左下角对齐",
            "居中靠下对齐",
            "右下角对齐",
            "居中靠左对齐",
            "中心对齐",
            "居中靠右对齐"});
            this.cbxAlign.Location = new System.Drawing.Point(84, 100);
            this.cbxAlign.Name = "cbxAlign";
            this.cbxAlign.Size = new System.Drawing.Size(102, 20);
            this.cbxAlign.TabIndex = 4;
            this.cbxAlign.SelectedIndexChanged += new System.EventHandler(this.cbxAlign_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "旋转角度(&R):";
            // 
            // cbxRotation
            // 
            this.cbxRotation.FormattingEnabled = true;
            this.cbxRotation.Items.AddRange(new object[] {
            "0",
            "45",
            "90",
            "135",
            "180",
            "225",
            "270"});
            this.cbxRotation.Location = new System.Drawing.Point(95, 124);
            this.cbxRotation.Name = "cbxRotation";
            this.cbxRotation.Size = new System.Drawing.Size(91, 20);
            this.cbxRotation.TabIndex = 12;
            this.cbxRotation.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "前景(&P)";
            // 
            // btnQJ_Color
            // 
            this.btnQJ_Color.Location = new System.Drawing.Point(118, 17);
            this.btnQJ_Color.Name = "btnQJ_Color";
            this.btnQJ_Color.Size = new System.Drawing.Size(33, 21);
            this.btnQJ_Color.TabIndex = 16;
            this.btnQJ_Color.Text = "...";
            this.btnQJ_Color.UseVisualStyleBackColor = true;
            this.btnQJ_Color.Click += new System.EventHandler(this.btnQJ_Color_Click);
            // 
            // pbx_QJ_Color
            // 
            this.pbx_QJ_Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbx_QJ_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbx_QJ_Color.Location = new System.Drawing.Point(66, 17);
            this.pbx_QJ_Color.Name = "pbx_QJ_Color";
            this.pbx_QJ_Color.Size = new System.Drawing.Size(46, 21);
            this.pbx_QJ_Color.TabIndex = 17;
            this.pbx_QJ_Color.TabStop = false;
            // 
            // pbx_BJ_Color
            // 
            this.pbx_BJ_Color.BackColor = System.Drawing.Color.White;
            this.pbx_BJ_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbx_BJ_Color.Location = new System.Drawing.Point(66, 42);
            this.pbx_BJ_Color.Name = "pbx_BJ_Color";
            this.pbx_BJ_Color.Size = new System.Drawing.Size(46, 21);
            this.pbx_BJ_Color.TabIndex = 20;
            this.pbx_BJ_Color.TabStop = false;
            // 
            // btnBJ_Color
            // 
            this.btnBJ_Color.Location = new System.Drawing.Point(118, 42);
            this.btnBJ_Color.Name = "btnBJ_Color";
            this.btnBJ_Color.Size = new System.Drawing.Size(33, 21);
            this.btnBJ_Color.TabIndex = 19;
            this.btnBJ_Color.Text = "...";
            this.btnBJ_Color.UseVisualStyleBackColor = true;
            this.btnBJ_Color.Click += new System.EventHandler(this.btnBJ_Color_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "背景(&O)";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(210, 186);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(89, 22);
            this.btnApply.TabIndex = 21;
            this.btnApply.Text = "确定(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(305, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 22);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxFontRotation);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnFont);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.cbxRotation);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbxAlign);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(8, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 178);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            // 
            // cbxFontRotation
            // 
            this.cbxFontRotation.BackColor = System.Drawing.Color.Silver;
            this.cbxFontRotation.Enabled = false;
            this.cbxFontRotation.FormattingEnabled = true;
            this.cbxFontRotation.Items.AddRange(new object[] {
            "0",
            "45",
            "90",
            "135",
            "180",
            "225",
            "270"});
            this.cbxFontRotation.Location = new System.Drawing.Point(95, 148);
            this.cbxFontRotation.Name = "cbxFontRotation";
            this.cbxFontRotation.Size = new System.Drawing.Size(91, 20);
            this.cbxFontRotation.TabIndex = 25;
            this.cbxFontRotation.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "字体旋转角度:";
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(270, 137);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(79, 23);
            this.btnFont.TabIndex = 23;
            this.btnFont.Text = "字体设置";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "设置字体:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.pbx_QJ_Color);
            this.groupBox3.Controls.Add(this.pbx_BJ_Color);
            this.groupBox3.Controls.Add(this.btnQJ_Color);
            this.groupBox3.Controls.Add(this.btnBJ_Color);
            this.groupBox3.Location = new System.Drawing.Point(6, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 72);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "颜色设置:";
            // 
            // frmTextStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 209);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Name = "frmTextStyle";
            this.Text = "文字信息:";
            this.Load += new System.EventHandler(this.frmTextStyle_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_QJ_Color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_BJ_Color)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAlign;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxRotation;
        private System.Windows.Forms.CheckBox chxLK;
        private System.Windows.Forms.CheckBox chkBJTM;
        private System.Windows.Forms.CheckBox chkYY;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckBox chkGDDX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnQJ_Color;
        private System.Windows.Forms.PictureBox pbx_QJ_Color;
        private System.Windows.Forms.PictureBox pbx_BJ_Color;
        private System.Windows.Forms.Button btnBJ_Color;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.CheckBox chkUserDefine;
        private System.Windows.Forms.ComboBox cbxFontRotation;
        private System.Windows.Forms.Label label5;
    }
}