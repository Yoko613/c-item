namespace ZTSupermap.UI
{
    partial class ExportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportData));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDS = new System.Windows.Forms.ComboBox();
            this.cmbDT = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbDatumnName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvFields = new System.Windows.Forms.CheckedListBox();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.btnSelINs = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据源:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据集:";
            // 
            // cmbDS
            // 
            this.cmbDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDS.FormattingEnabled = true;
            this.cmbDS.Location = new System.Drawing.Point(70, 39);
            this.cmbDS.Name = "cmbDS";
            this.cmbDS.Size = new System.Drawing.Size(252, 20);
            this.cmbDS.TabIndex = 4;
            this.cmbDS.SelectedIndexChanged += new System.EventHandler(this.cmbDS_SelectedIndexChanged);
            // 
            // cmbDT
            // 
            this.cmbDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDT.FormattingEnabled = true;
            this.cmbDT.Location = new System.Drawing.Point(70, 65);
            this.cmbDT.Name = "cmbDT";
            this.cmbDT.Size = new System.Drawing.Size(251, 20);
            this.cmbDT.TabIndex = 5;
            this.cmbDT.SelectedIndexChanged += new System.EventHandler(this.cmbDT_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "字  段:";
            // 
            // lbDatumnName
            // 
            this.lbDatumnName.AutoSize = true;
            this.lbDatumnName.Location = new System.Drawing.Point(12, 15);
            this.lbDatumnName.Name = "lbDatumnName";
            this.lbDatumnName.Size = new System.Drawing.Size(47, 12);
            this.lbDatumnName.TabIndex = 31;
            this.lbDatumnName.Text = "文件名:";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtFileName.Location = new System.Drawing.Point(70, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(252, 21);
            this.txtFileName.TabIndex = 32;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(145, 20);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 34;
            this.btnOK.Text = "导出数据";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(226, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(294, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(28, 21);
            this.btnOpenFile.TabIndex = 38;
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelINs);
            this.groupBox1.Controls.Add(this.btnSelAll);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Location = new System.Drawing.Point(14, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 50);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // tvFields
            // 
            this.tvFields.FormattingEnabled = true;
            this.tvFields.Location = new System.Drawing.Point(70, 91);
            this.tvFields.Name = "tvFields";
            this.tvFields.Size = new System.Drawing.Size(252, 148);
            this.tvFields.TabIndex = 40;
            // 
            // btnSelAll
            // 
            this.btnSelAll.Location = new System.Drawing.Point(7, 20);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(45, 23);
            this.btnSelAll.TabIndex = 36;
            this.btnSelAll.Text = "全选";
            this.btnSelAll.UseVisualStyleBackColor = true;
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnSelINs
            // 
            this.btnSelINs.Location = new System.Drawing.Point(56, 20);
            this.btnSelINs.Name = "btnSelINs";
            this.btnSelINs.Size = new System.Drawing.Size(44, 23);
            this.btnSelINs.TabIndex = 37;
            this.btnSelINs.Text = "反选";
            this.btnSelINs.UseVisualStyleBackColor = true;
            this.btnSelINs.Click += new System.EventHandler(this.btnSelINs_Click);
            // 
            // ExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 295);
            this.Controls.Add(this.tvFields);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.lbDatumnName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDT);
            this.Controls.Add(this.cmbDS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出数据到文本文件";
            this.Load += new System.EventHandler(this.ExportData_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDS;
        private System.Windows.Forms.ComboBox cmbDT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbDatumnName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox tvFields;
        private System.Windows.Forms.Button btnSelINs;
        private System.Windows.Forms.Button btnSelAll;
    }
}