namespace ZTSupermap.UI
{
    partial class FileDTExport
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
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFileFormat = new System.Windows.Forms.ComboBox();
            this.btnOpenPath = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dataVwExport = new System.Windows.Forms.DataGridView();
            this.chkExport = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DtSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbUnitExport = new System.Windows.Forms.ComboBox();
            this.ddddd = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labDSource = new System.Windows.Forms.Label();
            this.cmbDataSourceName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataVwExport)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(80, 62);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(356, 21);
            this.txtFilePath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "导出格式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "存储目录：";
            // 
            // cmbFileFormat
            // 
            this.cmbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileFormat.FormattingEnabled = true;
            this.cmbFileFormat.Location = new System.Drawing.Point(80, 33);
            this.cmbFileFormat.Name = "cmbFileFormat";
            this.cmbFileFormat.Size = new System.Drawing.Size(216, 20);
            this.cmbFileFormat.TabIndex = 4;
            // 
            // btnOpenPath
            // 
            this.btnOpenPath.Location = new System.Drawing.Point(436, 62);
            this.btnOpenPath.Name = "btnOpenPath";
            this.btnOpenPath.Size = new System.Drawing.Size(20, 22);
            this.btnOpenPath.TabIndex = 5;
            this.btnOpenPath.Text = ">>";
            this.btnOpenPath.UseVisualStyleBackColor = true;
            this.btnOpenPath.Click += new System.EventHandler(this.btnOpenPath_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(373, 295);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(77, 24);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dataVwExport
            // 
            this.dataVwExport.AllowUserToAddRows = false;
            this.dataVwExport.AllowUserToDeleteRows = false;
            this.dataVwExport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataVwExport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkExport,
            this.DtSet,
            this.DtFileName});
            this.dataVwExport.Location = new System.Drawing.Point(14, 97);
            this.dataVwExport.MultiSelect = false;
            this.dataVwExport.Name = "dataVwExport";
            this.dataVwExport.RowHeadersVisible = false;
            this.dataVwExport.RowTemplate.Height = 23;
            this.dataVwExport.Size = new System.Drawing.Size(439, 181);
            this.dataVwExport.TabIndex = 8;
            // 
            // chkExport
            // 
            this.chkExport.HeaderText = "选择";
            this.chkExport.Name = "chkExport";
            // 
            // DtSet
            // 
            this.DtSet.HeaderText = "数据集";
            this.DtSet.Name = "DtSet";
            this.DtSet.ReadOnly = true;
            this.DtSet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DtSet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DtFileName
            // 
            this.DtFileName.HeaderText = "转出文件名";
            this.DtFileName.Name = "DtFileName";
            this.DtFileName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DtFileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cmbUnitExport
            // 
            this.cmbUnitExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitExport.FormattingEnabled = true;
            this.cmbUnitExport.Location = new System.Drawing.Point(373, 33);
            this.cmbUnitExport.Name = "cmbUnitExport";
            this.cmbUnitExport.Size = new System.Drawing.Size(80, 20);
            this.cmbUnitExport.TabIndex = 9;
            // 
            // ddddd
            // 
            this.ddddd.AutoSize = true;
            this.ddddd.Location = new System.Drawing.Point(302, 36);
            this.ddddd.Name = "ddddd";
            this.ddddd.Size = new System.Drawing.Size(65, 12);
            this.ddddd.TabIndex = 10;
            this.ddddd.Text = "坐标单位：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 296);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(296, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // labDSource
            // 
            this.labDSource.AutoSize = true;
            this.labDSource.Location = new System.Drawing.Point(0, 9);
            this.labDSource.Name = "labDSource";
            this.labDSource.Size = new System.Drawing.Size(77, 12);
            this.labDSource.TabIndex = 12;
            this.labDSource.Text = "目标数据源：";
            // 
            // cmbDataSourceName
            // 
            this.cmbDataSourceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataSourceName.FormattingEnabled = true;
            this.cmbDataSourceName.Location = new System.Drawing.Point(80, 6);
            this.cmbDataSourceName.Name = "cmbDataSourceName";
            this.cmbDataSourceName.Size = new System.Drawing.Size(376, 20);
            this.cmbDataSourceName.TabIndex = 13;
            this.cmbDataSourceName.SelectedIndexChanged += new System.EventHandler(this.cmbDataSourceName_SelectedIndexChanged);
            // 
            // FileDTExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 331);
            this.Controls.Add(this.cmbDataSourceName);
            this.Controls.Add(this.labDSource);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ddddd);
            this.Controls.Add(this.cmbUnitExport);
            this.Controls.Add(this.dataVwExport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnOpenPath);
            this.Controls.Add(this.cmbFileFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileDTExport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据导出";
            this.Load += new System.EventHandler(this.frmFileDTExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataVwExport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFileFormat;
        private System.Windows.Forms.Button btnOpenPath;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dataVwExport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtFileName;
        private System.Windows.Forms.ComboBox cmbUnitExport;
        private System.Windows.Forms.Label ddddd;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labDSource;
        private System.Windows.Forms.ComboBox cmbDataSourceName;
    }
}