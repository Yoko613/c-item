namespace ZTSupermap.UI
{
    partial class ImageDTImport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDTImport));
            this.combDatasourcename = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboxShowProgess = new System.Windows.Forms.CheckBox();
            this.btnRemoveMult = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelMessage = new System.Windows.Forms.Label();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colPy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnOpen = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddFromClipbord = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // combDatasourcename
            // 
            this.combDatasourcename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDatasourcename.FormattingEnabled = true;
            this.combDatasourcename.Location = new System.Drawing.Point(93, 6);
            this.combDatasourcename.Name = "combDatasourcename";
            this.combDatasourcename.Size = new System.Drawing.Size(236, 20);
            this.combDatasourcename.TabIndex = 0;
            this.combDatasourcename.SelectedIndexChanged += new System.EventHandler(this.combDatasourcename_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboxShowProgess);
            this.groupBox1.Controls.Add(this.btnRemoveMult);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.labelMessage);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.btnCancle);
            this.groupBox1.Location = new System.Drawing.Point(12, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 58);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cboxShowProgess
            // 
            this.cboxShowProgess.AutoSize = true;
            this.cboxShowProgess.Location = new System.Drawing.Point(24, 20);
            this.cboxShowProgess.Name = "cboxShowProgess";
            this.cboxShowProgess.Size = new System.Drawing.Size(15, 14);
            this.cboxShowProgess.TabIndex = 6;
            this.cboxShowProgess.UseVisualStyleBackColor = true;
            // 
            // btnRemoveMult
            // 
            this.btnRemoveMult.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemoveMult.Location = new System.Drawing.Point(320, 20);
            this.btnRemoveMult.Name = "btnRemoveMult";
            this.btnRemoveMult.Size = new System.Drawing.Size(64, 23);
            this.btnRemoveMult.TabIndex = 5;
            this.btnRemoveMult.Text = "滤去重复";
            this.btnRemoveMult.Click += new System.EventHandler(this.btnRemoveMult_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(258, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "终 止";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(22, 20);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 12);
            this.labelMessage.TabIndex = 3;
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.Location = new System.Drawing.Point(393, 20);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(51, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "导 入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(453, 20);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(54, 23);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "关 闭";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFilename,
            this.colPath,
            this.colDtName,
            this.colCode,
            this.colType,
            this.colPy});
            this.dataGridView1.Location = new System.Drawing.Point(12, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(524, 206);
            this.dataGridView1.TabIndex = 3;
            // 
            // colFilename
            // 
            this.colFilename.HeaderText = "文件名";
            this.colFilename.Name = "colFilename";
            this.colFilename.ReadOnly = true;
            this.colFilename.Width = 80;
            // 
            // colPath
            // 
            this.colPath.FillWeight = 120F;
            this.colPath.HeaderText = "路径";
            this.colPath.Name = "colPath";
            this.colPath.ReadOnly = true;
            this.colPath.Width = 120;
            // 
            // colDtName
            // 
            this.colDtName.FillWeight = 80F;
            this.colDtName.HeaderText = "数据集名";
            this.colDtName.Name = "colDtName";
            this.colDtName.Width = 80;
            // 
            // colCode
            // 
            this.colCode.FillWeight = 80F;
            this.colCode.HeaderText = "编码方式";
            this.colCode.Items.AddRange(new object[] {
            "DCT",
            "未编码"});
            this.colCode.Name = "colCode";
            this.colCode.Width = 80;
            // 
            // colType
            // 
            this.colType.FillWeight = 80F;
            this.colType.HeaderText = "结果类型";
            this.colType.Items.AddRange(new object[] {
            "IMAGE数据集",
            "GRID数据集",
            "DEM数据集"});
            this.colType.Name = "colType";
            this.colType.Width = 80;
            // 
            // colPy
            // 
            this.colPy.FillWeight = 60F;
            this.colPy.HeaderText = "金字塔";
            this.colPy.Name = "colPy";
            this.colPy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPy.Width = 60;
            // 
            // btnOpen
            // 
            this.btnOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            //this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btnOpen.Location = new System.Drawing.Point(433, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(102, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "添加文件...";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "目标数据源：";
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.Location = new System.Drawing.Point(352, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "全部清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddFile,
            this.menuItemAddFiles,
            this.menuItemAddFromClipbord});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 70);
            // 
            // menuItemAddFile
            // 
            this.menuItemAddFile.Name = "menuItemAddFile";
            this.menuItemAddFile.Size = new System.Drawing.Size(214, 22);
            this.menuItemAddFile.Text = "添加文件";
            this.menuItemAddFile.Click += new System.EventHandler(this.menuItemAddFile_Click);
            // 
            // menuItemAddFiles
            // 
            this.menuItemAddFiles.Name = "menuItemAddFiles";
            this.menuItemAddFiles.Size = new System.Drawing.Size(214, 22);
            this.menuItemAddFiles.Text = "批量添加文件";
            this.menuItemAddFiles.Visible = false;
            this.menuItemAddFiles.Click += new System.EventHandler(this.menuItemAddFiles_Click);
            // 
            // menuItemAddFromClipbord
            // 
            this.menuItemAddFromClipbord.Name = "menuItemAddFromClipbord";
            this.menuItemAddFromClipbord.Size = new System.Drawing.Size(214, 22);
            this.menuItemAddFromClipbord.Text = "导入剪切板中的文件名列表";
            this.menuItemAddFromClipbord.Click += new System.EventHandler(this.menuItemAddFromClipbord_Click);
            // 
            // ImageDTImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 300);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.combDatasourcename);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ImageDTImport";
            this.ShowInTaskbar = false;
            this.Text = "栅格数据导入";
            this.Load += new System.EventHandler(this.ImageDTImport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combDatasourcename;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        //private System.Windows.Forms.CheckBox chkProcessBar;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDtName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPy;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddFiles;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddFromClipbord;
        private System.Windows.Forms.Label labelMessage;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnRemoveMult;
        private System.Windows.Forms.CheckBox cboxShowProgess;
    }
}