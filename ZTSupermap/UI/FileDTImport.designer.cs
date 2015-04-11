namespace ZTSupermap.UI
{
    partial class FileDTImport
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
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.cmbFtyp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_transfileOpen = new System.Windows.Forms.Button();
            this.chkPointLv = new System.Windows.Forms.CheckBox();
            this.chkLineLv = new System.Windows.Forms.CheckBox();
            this.chkShapeLv = new System.Windows.Forms.CheckBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNewRegion = new System.Windows.Forms.TextBox();
            this.txtNewLine = new System.Windows.Forms.TextBox();
            this.txtNewPoint = new System.Windows.Forms.TextBox();
            this.cmbShapeDupCond = new System.Windows.Forms.ComboBox();
            this.cmbLineDupCond = new System.Windows.Forms.ComboBox();
            this.cmbPointDupCond = new System.Windows.Forms.ComboBox();
            this.cmbBDSShape = new System.Windows.Forms.ComboBox();
            this.cmbBDSLine = new System.Windows.Forms.ComboBox();
            this.cmbBDSPoint = new System.Windows.Forms.ComboBox();
            this.cmbDSShape = new System.Windows.Forms.ComboBox();
            this.cmbDSLine = new System.Windows.Forms.ComboBox();
            this.cmbDSPoint = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImport_tFile = new System.Windows.Forms.Button();
            this.rdiNew = new System.Windows.Forms.RadioButton();
            this.rdiAppend = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDataSourceName = new System.Windows.Forms.ComboBox();
            this.labDSource = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilepath
            // 
            this.txtFilepath.Location = new System.Drawing.Point(83, 59);
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(367, 21);
            this.txtFilepath.TabIndex = 0;
            // 
            // cmbFtyp
            // 
            this.cmbFtyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFtyp.FormattingEnabled = true;
            this.cmbFtyp.Location = new System.Drawing.Point(83, 31);
            this.cmbFtyp.Name = "cmbFtyp";
            this.cmbFtyp.Size = new System.Drawing.Size(209, 20);
            this.cmbFtyp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件名：";
            // 
            // btn_transfileOpen
            // 
            this.btn_transfileOpen.Location = new System.Drawing.Point(449, 59);
            this.btn_transfileOpen.Name = "btn_transfileOpen";
            this.btn_transfileOpen.Size = new System.Drawing.Size(21, 22);
            this.btn_transfileOpen.TabIndex = 4;
            this.btn_transfileOpen.Text = ">";
            this.btn_transfileOpen.UseVisualStyleBackColor = true;
            this.btn_transfileOpen.Click += new System.EventHandler(this.btn_transfileOpen_Click);
            // 
            // chkPointLv
            // 
            this.chkPointLv.AutoSize = true;
            this.chkPointLv.Location = new System.Drawing.Point(6, 25);
            this.chkPointLv.Name = "chkPointLv";
            this.chkPointLv.Size = new System.Drawing.Size(60, 16);
            this.chkPointLv.TabIndex = 5;
            this.chkPointLv.Text = "点数据";
            this.chkPointLv.UseVisualStyleBackColor = true;
            this.chkPointLv.CheckedChanged += new System.EventHandler(this.chkPointLv_CheckedChanged);
            // 
            // chkLineLv
            // 
            this.chkLineLv.AutoSize = true;
            this.chkLineLv.Location = new System.Drawing.Point(6, 47);
            this.chkLineLv.Name = "chkLineLv";
            this.chkLineLv.Size = new System.Drawing.Size(60, 16);
            this.chkLineLv.TabIndex = 6;
            this.chkLineLv.Text = "线数据";
            this.chkLineLv.UseVisualStyleBackColor = true;
            this.chkLineLv.CheckedChanged += new System.EventHandler(this.chkLineLv_CheckedChanged);
            // 
            // chkShapeLv
            // 
            this.chkShapeLv.AutoSize = true;
            this.chkShapeLv.Location = new System.Drawing.Point(6, 69);
            this.chkShapeLv.Name = "chkShapeLv";
            this.chkShapeLv.Size = new System.Drawing.Size(60, 16);
            this.chkShapeLv.TabIndex = 7;
            this.chkShapeLv.Text = "面数据";
            this.chkShapeLv.UseVisualStyleBackColor = true;
            this.chkShapeLv.CheckedChanged += new System.EventHandler(this.chkShapeLv_CheckedChanged);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(369, 31);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(101, 20);
            this.cmbUnit.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNewRegion);
            this.groupBox1.Controls.Add(this.txtNewLine);
            this.groupBox1.Controls.Add(this.txtNewPoint);
            this.groupBox1.Controls.Add(this.cmbShapeDupCond);
            this.groupBox1.Controls.Add(this.cmbLineDupCond);
            this.groupBox1.Controls.Add(this.cmbPointDupCond);
            this.groupBox1.Controls.Add(this.cmbBDSShape);
            this.groupBox1.Controls.Add(this.cmbBDSLine);
            this.groupBox1.Controls.Add(this.cmbBDSPoint);
            this.groupBox1.Controls.Add(this.cmbDSShape);
            this.groupBox1.Controls.Add(this.cmbDSLine);
            this.groupBox1.Controls.Add(this.cmbDSPoint);
            this.groupBox1.Controls.Add(this.chkPointLv);
            this.groupBox1.Controls.Add(this.chkLineLv);
            this.groupBox1.Controls.Add(this.chkShapeLv);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 98);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "　类型 　|　 转入数据集　　 　|　　 备份数据集　　  | 　　  重复条件 　  　";
            // 
            // txtNewRegion
            // 
            this.txtNewRegion.Location = new System.Drawing.Point(64, 67);
            this.txtNewRegion.Name = "txtNewRegion";
            this.txtNewRegion.Size = new System.Drawing.Size(121, 21);
            this.txtNewRegion.TabIndex = 22;
            // 
            // txtNewLine
            // 
            this.txtNewLine.Location = new System.Drawing.Point(64, 43);
            this.txtNewLine.Name = "txtNewLine";
            this.txtNewLine.Size = new System.Drawing.Size(121, 21);
            this.txtNewLine.TabIndex = 22;
            // 
            // txtNewPoint
            // 
            this.txtNewPoint.Location = new System.Drawing.Point(64, 19);
            this.txtNewPoint.Name = "txtNewPoint";
            this.txtNewPoint.Size = new System.Drawing.Size(121, 21);
            this.txtNewPoint.TabIndex = 22;
            // 
            // cmbShapeDupCond
            // 
            this.cmbShapeDupCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShapeDupCond.FormattingEnabled = true;
            this.cmbShapeDupCond.Location = new System.Drawing.Point(326, 67);
            this.cmbShapeDupCond.Name = "cmbShapeDupCond";
            this.cmbShapeDupCond.Size = new System.Drawing.Size(126, 20);
            this.cmbShapeDupCond.TabIndex = 16;
            // 
            // cmbLineDupCond
            // 
            this.cmbLineDupCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineDupCond.FormattingEnabled = true;
            this.cmbLineDupCond.Location = new System.Drawing.Point(326, 43);
            this.cmbLineDupCond.Name = "cmbLineDupCond";
            this.cmbLineDupCond.Size = new System.Drawing.Size(126, 20);
            this.cmbLineDupCond.TabIndex = 15;
            // 
            // cmbPointDupCond
            // 
            this.cmbPointDupCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointDupCond.FormattingEnabled = true;
            this.cmbPointDupCond.Location = new System.Drawing.Point(326, 19);
            this.cmbPointDupCond.Name = "cmbPointDupCond";
            this.cmbPointDupCond.Size = new System.Drawing.Size(126, 20);
            this.cmbPointDupCond.TabIndex = 14;
            // 
            // cmbBDSShape
            // 
            this.cmbBDSShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBDSShape.FormattingEnabled = true;
            this.cmbBDSShape.Location = new System.Drawing.Point(191, 67);
            this.cmbBDSShape.Name = "cmbBDSShape";
            this.cmbBDSShape.Size = new System.Drawing.Size(129, 20);
            this.cmbBDSShape.TabIndex = 13;
            this.cmbBDSShape.SelectedIndexChanged += new System.EventHandler(this.cmbBDSShape_SelectedIndexChanged);
            // 
            // cmbBDSLine
            // 
            this.cmbBDSLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBDSLine.FormattingEnabled = true;
            this.cmbBDSLine.Location = new System.Drawing.Point(191, 43);
            this.cmbBDSLine.Name = "cmbBDSLine";
            this.cmbBDSLine.Size = new System.Drawing.Size(129, 20);
            this.cmbBDSLine.TabIndex = 12;
            this.cmbBDSLine.SelectedIndexChanged += new System.EventHandler(this.cmbBDSLine_SelectedIndexChanged);
            // 
            // cmbBDSPoint
            // 
            this.cmbBDSPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBDSPoint.FormattingEnabled = true;
            this.cmbBDSPoint.Location = new System.Drawing.Point(191, 19);
            this.cmbBDSPoint.Name = "cmbBDSPoint";
            this.cmbBDSPoint.Size = new System.Drawing.Size(129, 20);
            this.cmbBDSPoint.TabIndex = 11;
            this.cmbBDSPoint.SelectedIndexChanged += new System.EventHandler(this.cmbBDSPoint_SelectedIndexChanged);
            // 
            // cmbDSShape
            // 
            this.cmbDSShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDSShape.FormattingEnabled = true;
            this.cmbDSShape.Location = new System.Drawing.Point(64, 67);
            this.cmbDSShape.Name = "cmbDSShape";
            this.cmbDSShape.Size = new System.Drawing.Size(121, 20);
            this.cmbDSShape.TabIndex = 10;
            this.cmbDSShape.SelectedIndexChanged += new System.EventHandler(this.cmbDSShape_SelectedIndexChanged);
            // 
            // cmbDSLine
            // 
            this.cmbDSLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDSLine.FormattingEnabled = true;
            this.cmbDSLine.Location = new System.Drawing.Point(64, 43);
            this.cmbDSLine.Name = "cmbDSLine";
            this.cmbDSLine.Size = new System.Drawing.Size(121, 20);
            this.cmbDSLine.TabIndex = 9;
            this.cmbDSLine.SelectedIndexChanged += new System.EventHandler(this.cmbDSLine_SelectedIndexChanged);
            // 
            // cmbDSPoint
            // 
            this.cmbDSPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDSPoint.FormattingEnabled = true;
            this.cmbDSPoint.Location = new System.Drawing.Point(64, 19);
            this.cmbDSPoint.Name = "cmbDSPoint";
            this.cmbDSPoint.Size = new System.Drawing.Size(121, 20);
            this.cmbDSPoint.TabIndex = 8;
            this.cmbDSPoint.SelectedIndexChanged += new System.EventHandler(this.cmbDSPoint_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "坐标单位：";
            // 
            // btnImport_tFile
            // 
            this.btnImport_tFile.Location = new System.Drawing.Point(369, 205);
            this.btnImport_tFile.Name = "btnImport_tFile";
            this.btnImport_tFile.Size = new System.Drawing.Size(92, 24);
            this.btnImport_tFile.TabIndex = 18;
            this.btnImport_tFile.Text = "导入";
            this.btnImport_tFile.UseVisualStyleBackColor = true;
            this.btnImport_tFile.Click += new System.EventHandler(this.btnImport_tFile_Click);
            // 
            // rdiNew
            // 
            this.rdiNew.AutoSize = true;
            this.rdiNew.Location = new System.Drawing.Point(136, 12);
            this.rdiNew.Name = "rdiNew";
            this.rdiNew.Size = new System.Drawing.Size(47, 16);
            this.rdiNew.TabIndex = 19;
            this.rdiNew.TabStop = true;
            this.rdiNew.Text = "新建";
            this.rdiNew.UseVisualStyleBackColor = true;
            // 
            // rdiAppend
            // 
            this.rdiAppend.AutoSize = true;
            this.rdiAppend.Location = new System.Drawing.Point(4, 12);
            this.rdiAppend.Name = "rdiAppend";
            this.rdiAppend.Size = new System.Drawing.Size(47, 16);
            this.rdiAppend.TabIndex = 20;
            this.rdiAppend.TabStop = true;
            this.rdiAppend.Text = "追加";
            this.rdiAppend.UseVisualStyleBackColor = true;
            this.rdiAppend.CheckedChanged += new System.EventHandler(this.rdiAppend_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdiNew);
            this.groupBox2.Controls.Add(this.rdiAppend);
            this.groupBox2.Location = new System.Drawing.Point(12, 195);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 34);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // cmbDataSourceName
            // 
            this.cmbDataSourceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataSourceName.FormattingEnabled = true;
            this.cmbDataSourceName.Location = new System.Drawing.Point(84, 5);
            this.cmbDataSourceName.Name = "cmbDataSourceName";
            this.cmbDataSourceName.Size = new System.Drawing.Size(386, 20);
            this.cmbDataSourceName.TabIndex = 22;
            this.cmbDataSourceName.SelectedIndexChanged += new System.EventHandler(this.cmbDataSourceName_SelectedIndexChanged);
            // 
            // labDSource
            // 
            this.labDSource.AutoSize = true;
            this.labDSource.Location = new System.Drawing.Point(1, 9);
            this.labDSource.Name = "labDSource";
            this.labDSource.Size = new System.Drawing.Size(77, 12);
            this.labDSource.TabIndex = 23;
            this.labDSource.Text = "目标数据源：";
            // 
            // FileDTImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(482, 239);
            this.Controls.Add(this.labDSource);
            this.Controls.Add(this.cmbDataSourceName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnImport_tFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.btn_transfileOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFtyp);
            this.Controls.Add(this.txtFilepath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileDTImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "文件方式数据导入";
            this.Load += new System.EventHandler(this.FileDTImport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.ComboBox cmbFtyp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_transfileOpen;
        private System.Windows.Forms.CheckBox chkPointLv;
        private System.Windows.Forms.CheckBox chkLineLv;
        private System.Windows.Forms.CheckBox chkShapeLv;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImport_tFile;
        private System.Windows.Forms.ComboBox cmbBDSShape;
        private System.Windows.Forms.ComboBox cmbBDSLine;
        private System.Windows.Forms.ComboBox cmbBDSPoint;
        private System.Windows.Forms.ComboBox cmbDSShape;
        private System.Windows.Forms.ComboBox cmbDSLine;
        private System.Windows.Forms.ComboBox cmbDSPoint;
        private System.Windows.Forms.ComboBox cmbShapeDupCond;
        private System.Windows.Forms.ComboBox cmbLineDupCond;
        private System.Windows.Forms.ComboBox cmbPointDupCond;
        private System.Windows.Forms.RadioButton rdiNew;
        private System.Windows.Forms.RadioButton rdiAppend;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNewRegion;
        private System.Windows.Forms.TextBox txtNewLine;
        private System.Windows.Forms.TextBox txtNewPoint;
        private System.Windows.Forms.ComboBox cmbDataSourceName;
        private System.Windows.Forms.Label labDSource;
    }
}