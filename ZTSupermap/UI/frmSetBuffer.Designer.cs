namespace ZTMapPrint
{
    partial class frmSetBuffer
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
            this.rbtLeftRightBuf = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRightRad = new System.Windows.Forms.TextBox();
            this.txtLeftRad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtRigthBuf = new System.Windows.Forms.RadioButton();
            this.rbtLeftRightNotEquel = new System.Windows.Forms.RadioButton();
            this.rbtLeftRightEquel = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtArc = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtRadBuf = new System.Windows.Forms.RadioButton();
            this.rbtRectBuf = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtLeftBuf = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtLeftRightBuf
            // 
            this.rbtLeftRightBuf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtLeftRightBuf.Location = new System.Drawing.Point(24, 61);
            this.rbtLeftRightBuf.Name = "rbtLeftRightBuf";
            this.rbtLeftRightBuf.Size = new System.Drawing.Size(120, 24);
            this.rbtLeftRightBuf.TabIndex = 2;
            this.rbtLeftRightBuf.Text = "左右缓冲";
            this.rbtLeftRightBuf.Click += new System.EventHandler(this.rbtLeftRightBuf_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRightRad);
            this.groupBox2.Controls.Add(this.txtLeftRad);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(16, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 80);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置缓冲半径:";
            // 
            // txtRightRad
            // 
            this.txtRightRad.Location = new System.Drawing.Point(56, 48);
            this.txtRightRad.Name = "txtRightRad";
            this.txtRightRad.Size = new System.Drawing.Size(88, 21);
            this.txtRightRad.TabIndex = 3;
            // 
            // txtLeftRad
            // 
            this.txtLeftRad.Location = new System.Drawing.Point(56, 21);
            this.txtLeftRad.Name = "txtLeftRad";
            this.txtLeftRad.Size = new System.Drawing.Size(88, 21);
            this.txtLeftRad.TabIndex = 2;
            this.txtLeftRad.Text = "5";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "右半径:";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "左半径:";
            // 
            // rbtRigthBuf
            // 
            this.rbtRigthBuf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtRigthBuf.Location = new System.Drawing.Point(24, 41);
            this.rbtRigthBuf.Name = "rbtRigthBuf";
            this.rbtRigthBuf.Size = new System.Drawing.Size(120, 24);
            this.rbtRigthBuf.TabIndex = 1;
            this.rbtRigthBuf.Text = "右缓冲";
            this.rbtRigthBuf.Click += new System.EventHandler(this.rbtRigthBuf_Click);
            // 
            // rbtLeftRightNotEquel
            // 
            this.rbtLeftRightNotEquel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtLeftRightNotEquel.Location = new System.Drawing.Point(10, 43);
            this.rbtLeftRightNotEquel.Name = "rbtLeftRightNotEquel";
            this.rbtLeftRightNotEquel.Size = new System.Drawing.Size(128, 24);
            this.rbtLeftRightNotEquel.TabIndex = 1;
            this.rbtLeftRightNotEquel.Text = "左右半径不等缓冲";
            this.rbtLeftRightNotEquel.Click += new System.EventHandler(this.rbtLeftRightNotEquel_Click);
            // 
            // rbtLeftRightEquel
            // 
            this.rbtLeftRightEquel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtLeftRightEquel.Location = new System.Drawing.Point(10, 16);
            this.rbtLeftRightEquel.Name = "rbtLeftRightEquel";
            this.rbtLeftRightEquel.Size = new System.Drawing.Size(128, 24);
            this.rbtLeftRightEquel.TabIndex = 0;
            this.rbtLeftRightEquel.Text = "左右半径相等缓冲";
            this.rbtLeftRightEquel.Click += new System.EventHandler(this.rbtLeftRightEquel_Click);
            this.rbtLeftRightEquel.CheckedChanged += new System.EventHandler(this.rbtLeftRightEquel_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(269, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(191, 235);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(72, 25);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "确定";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtArc
            // 
            this.txtArc.Location = new System.Drawing.Point(16, 17);
            this.txtArc.Name = "txtArc";
            this.txtArc.Size = new System.Drawing.Size(128, 21);
            this.txtArc.TabIndex = 0;
            this.txtArc.Text = "12";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtRadBuf);
            this.groupBox5.Controls.Add(this.rbtRectBuf);
            this.groupBox5.Location = new System.Drawing.Point(16, 110);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(160, 43);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "缓冲类型:";
            // 
            // rbtRadBuf
            // 
            this.rbtRadBuf.Location = new System.Drawing.Point(87, 20);
            this.rbtRadBuf.Name = "rbtRadBuf";
            this.rbtRadBuf.Size = new System.Drawing.Size(73, 24);
            this.rbtRadBuf.TabIndex = 1;
            this.rbtRadBuf.Text = "圆头缓冲";
            this.rbtRadBuf.Click += new System.EventHandler(this.rbtRadBuf_Click);
            // 
            // rbtRectBuf
            // 
            this.rbtRectBuf.Checked = true;
            this.rbtRectBuf.Location = new System.Drawing.Point(10, 20);
            this.rbtRectBuf.Name = "rbtRectBuf";
            this.rbtRectBuf.Size = new System.Drawing.Size(73, 24);
            this.rbtRectBuf.TabIndex = 0;
            this.rbtRectBuf.TabStop = true;
            this.rbtRectBuf.Text = "平头缓冲";
            this.rbtRectBuf.Click += new System.EventHandler(this.rbtRectBuf_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtLeftRightBuf);
            this.groupBox4.Controls.Add(this.rbtRigthBuf);
            this.groupBox4.Controls.Add(this.rbtLeftBuf);
            this.groupBox4.Location = new System.Drawing.Point(182, 109);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 96);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "缓冲边设置:";
            // 
            // rbtLeftBuf
            // 
            this.rbtLeftBuf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtLeftBuf.Location = new System.Drawing.Point(24, 20);
            this.rbtLeftBuf.Name = "rbtLeftBuf";
            this.rbtLeftBuf.Size = new System.Drawing.Size(120, 24);
            this.rbtLeftBuf.TabIndex = 0;
            this.rbtLeftBuf.Text = "左缓冲";
            this.rbtLeftBuf.Click += new System.EventHandler(this.rbtLeftBuf_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 217);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置缓冲参数:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtArc);
            this.groupBox6.Location = new System.Drawing.Point(16, 159);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(160, 46);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "边界平滑度(4-50):";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtLeftRightNotEquel);
            this.groupBox3.Controls.Add(this.rbtLeftRightEquel);
            this.groupBox3.Location = new System.Drawing.Point(182, 26);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 78);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "缓冲类型:";
            // 
            // frmSetBuffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmSetBuffer";
            this.Text = "frmSetBuffer";
            this.Load += new System.EventHandler(this.frmSetBuffer_Load_1);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtLeftRightBuf;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRightRad;
        private System.Windows.Forms.TextBox txtLeftRad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtRigthBuf;
        private System.Windows.Forms.RadioButton rbtLeftRightNotEquel;
        private System.Windows.Forms.RadioButton rbtLeftRightEquel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtArc;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbtRadBuf;
        private System.Windows.Forms.RadioButton rbtRectBuf;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtLeftBuf;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}