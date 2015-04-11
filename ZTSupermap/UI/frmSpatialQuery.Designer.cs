namespace ZTSupermap.UI
{
    partial class frmSpatialQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpatialQuery));
            this.lstQueryDB = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportTxt = new System.Windows.Forms.Button();
            this.btnQueryModelSelect = new DevComponents.DotNetBar.ButtonX();
            this.btnUnSelect = new System.Windows.Forms.Button();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.btnSQL_Condation = new DevComponents.DotNetBar.ButtonX();
            this.btnSelectReset = new System.Windows.Forms.Button();
            this.lstQueryModel = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chxOnlySpacial = new System.Windows.Forms.CheckBox();
            this.cbxDT = new System.Windows.Forms.ComboBox();
            this.cbxDS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chxSaveQueryResult = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chxReport = new System.Windows.Forms.CheckBox();
            this.chxAddResultToSuperMap = new System.Windows.Forms.CheckBox();
            this.chxAddDBToSelection = new System.Windows.Forms.CheckBox();
            this.btnExe_Query = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstQueryDB
            // 
            this.lstQueryDB.CheckBoxes = true;
            this.lstQueryDB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstQueryDB.FullRowSelect = true;
            this.lstQueryDB.GridLines = true;
            this.lstQueryDB.Location = new System.Drawing.Point(6, 20);
            this.lstQueryDB.Name = "lstQueryDB";
            this.lstQueryDB.Size = new System.Drawing.Size(434, 139);
            this.lstQueryDB.TabIndex = 0;
            this.lstQueryDB.UseCompatibleStateImageBehavior = false;
            this.lstQueryDB.View = System.Windows.Forms.View.Details;
            this.lstQueryDB.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstQueryDB_ItemChecked);
            this.lstQueryDB.MouseCaptureChanged += new System.EventHandler(this.lstQueryDB_MouseCaptureChanged);
            this.lstQueryDB.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lstQueryDB_ColumnWidthChanging);
            this.lstQueryDB.Click += new System.EventHandler(this.lstQueryDB_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "图层名称";
            this.columnHeader1.Width = 106;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "空间查询条件";
            this.columnHeader2.Width = 138;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "属性查询条件";
            this.columnHeader3.Width = 181;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImportTxt);
            this.groupBox1.Controls.Add(this.btnQueryModelSelect);
            this.groupBox1.Controls.Add(this.btnUnSelect);
            this.groupBox1.Controls.Add(this.btnSelAll);
            this.groupBox1.Controls.Add(this.btnSQL_Condation);
            this.groupBox1.Controls.Add(this.btnSelectReset);
            this.groupBox1.Controls.Add(this.lstQueryDB);
            this.groupBox1.Location = new System.Drawing.Point(0, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询数据设置";
            // 
            // btnImportTxt
            // 
            this.btnImportTxt.Location = new System.Drawing.Point(446, 124);
            this.btnImportTxt.Name = "btnImportTxt";
            this.btnImportTxt.Size = new System.Drawing.Size(116, 23);
            this.btnImportTxt.TabIndex = 7;
            this.btnImportTxt.Text = "自定义对象查询(&O)";
            this.btnImportTxt.UseVisualStyleBackColor = true;
            this.btnImportTxt.Click += new System.EventHandler(this.btnImportTxt_Click);
            // 
            // btnQueryModelSelect
            // 
            this.btnQueryModelSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQueryModelSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQueryModelSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnQueryModelSelect.Image")));
            this.btnQueryModelSelect.Location = new System.Drawing.Point(236, 41);
            this.btnQueryModelSelect.Name = "btnQueryModelSelect";
            this.btnQueryModelSelect.Size = new System.Drawing.Size(17, 19);
            this.btnQueryModelSelect.TabIndex = 6;
            this.btnQueryModelSelect.Visible = false;
            this.btnQueryModelSelect.Click += new System.EventHandler(this.btnQueryModelSelect_Click);
            // 
            // btnUnSelect
            // 
            this.btnUnSelect.Location = new System.Drawing.Point(446, 66);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new System.Drawing.Size(116, 23);
            this.btnUnSelect.TabIndex = 4;
            this.btnUnSelect.Text = "反选(&I)";
            this.btnUnSelect.UseVisualStyleBackColor = true;
            this.btnUnSelect.Click += new System.EventHandler(this.btnUnSelect_Click);
            // 
            // btnSelAll
            // 
            this.btnSelAll.Location = new System.Drawing.Point(446, 36);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(116, 23);
            this.btnSelAll.TabIndex = 3;
            this.btnSelAll.Text = "全选(&A)";
            this.btnSelAll.UseVisualStyleBackColor = true;
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnSQL_Condation
            // 
            this.btnSQL_Condation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSQL_Condation.Location = new System.Drawing.Point(406, 36);
            this.btnSQL_Condation.Name = "btnSQL_Condation";
            this.btnSQL_Condation.Size = new System.Drawing.Size(25, 18);
            this.btnSQL_Condation.TabIndex = 2;
            this.btnSQL_Condation.Text = "...";
            this.btnSQL_Condation.Visible = false;
            this.btnSQL_Condation.Click += new System.EventHandler(this.btnSQL_Condation_Click);
            // 
            // btnSelectReset
            // 
            this.btnSelectReset.Location = new System.Drawing.Point(446, 95);
            this.btnSelectReset.Name = "btnSelectReset";
            this.btnSelectReset.Size = new System.Drawing.Size(116, 23);
            this.btnSelectReset.TabIndex = 1;
            this.btnSelectReset.Text = "重置(&R)";
            this.btnSelectReset.UseVisualStyleBackColor = true;
            this.btnSelectReset.Click += new System.EventHandler(this.btnSelectReset_Click);
            // 
            // lstQueryModel
            // 
            // 
            // 
            // 
            this.lstQueryModel.Border.Class = "ListViewBorder";
            this.lstQueryModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lstQueryModel.Location = new System.Drawing.Point(124, 72);
            this.lstQueryModel.MultiSelect = false;
            this.lstQueryModel.Name = "lstQueryModel";
            this.lstQueryModel.ShowItemToolTips = true;
            this.lstQueryModel.Size = new System.Drawing.Size(139, 204);
            this.lstQueryModel.TabIndex = 5;
            this.lstQueryModel.UseCompatibleStateImageBehavior = false;
            this.lstQueryModel.View = System.Windows.Forms.View.Tile;
            this.lstQueryModel.Visible = false;
            this.lstQueryModel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstQueryModel_MouseMove);
            this.lstQueryModel.Click += new System.EventHandler(this.lstQueryModel_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chxOnlySpacial);
            this.groupBox2.Controls.Add(this.cbxDT);
            this.groupBox2.Controls.Add(this.cbxDS);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chxSaveQueryResult);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(0, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 106);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // chxOnlySpacial
            // 
            this.chxOnlySpacial.AutoSize = true;
            this.chxOnlySpacial.Enabled = false;
            this.chxOnlySpacial.Location = new System.Drawing.Point(25, 84);
            this.chxOnlySpacial.Name = "chxOnlySpacial";
            this.chxOnlySpacial.Size = new System.Drawing.Size(126, 16);
            this.chxOnlySpacial.TabIndex = 4;
            this.chxOnlySpacial.Text = "只保存空间信息(&V)";
            this.chxOnlySpacial.UseVisualStyleBackColor = true;
            this.chxOnlySpacial.CheckedChanged += new System.EventHandler(this.chxOnlySpacial_CheckedChanged);
            // 
            // cbxDT
            // 
            this.cbxDT.Enabled = false;
            this.cbxDT.FormattingEnabled = true;
            this.cbxDT.Location = new System.Drawing.Point(76, 52);
            this.cbxDT.Name = "cbxDT";
            this.cbxDT.Size = new System.Drawing.Size(156, 20);
            this.cbxDT.TabIndex = 3;
            this.cbxDT.TextChanged += new System.EventHandler(this.cbxDT_TextChanged);
            // 
            // cbxDS
            // 
            this.cbxDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDS.Enabled = false;
            this.cbxDS.FormattingEnabled = true;
            this.cbxDS.Location = new System.Drawing.Point(76, 26);
            this.cbxDS.Name = "cbxDS";
            this.cbxDS.Size = new System.Drawing.Size(156, 20);
            this.cbxDS.TabIndex = 2;
            this.cbxDS.SelectedIndexChanged += new System.EventHandler(this.cbxDS_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据集:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源:";
            // 
            // chxSaveQueryResult
            // 
            this.chxSaveQueryResult.AutoSize = true;
            this.chxSaveQueryResult.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chxSaveQueryResult.Location = new System.Drawing.Point(6, 0);
            this.chxSaveQueryResult.Name = "chxSaveQueryResult";
            this.chxSaveQueryResult.Size = new System.Drawing.Size(114, 16);
            this.chxSaveQueryResult.TabIndex = 0;
            this.chxSaveQueryResult.Text = "保存查询结果(&C)";
            this.chxSaveQueryResult.UseVisualStyleBackColor = true;
            this.chxSaveQueryResult.CheckedChanged += new System.EventHandler(this.chxSaveQueryResult_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chxReport);
            this.groupBox3.Controls.Add(this.chxAddResultToSuperMap);
            this.groupBox3.Controls.Add(this.chxAddDBToSelection);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(283, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 106);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结果显示:";
            // 
            // chxReport
            // 
            this.chxReport.AutoSize = true;
            this.chxReport.Location = new System.Drawing.Point(22, 75);
            this.chxReport.Name = "chxReport";
            this.chxReport.Size = new System.Drawing.Size(96, 16);
            this.chxReport.TabIndex = 2;
            this.chxReport.Text = "结果报表显示";
            this.chxReport.UseVisualStyleBackColor = true;
            // 
            // chxAddResultToSuperMap
            // 
            this.chxAddResultToSuperMap.AutoSize = true;
            this.chxAddResultToSuperMap.Checked = true;
            this.chxAddResultToSuperMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxAddResultToSuperMap.Location = new System.Drawing.Point(22, 52);
            this.chxAddResultToSuperMap.Name = "chxAddResultToSuperMap";
            this.chxAddResultToSuperMap.Size = new System.Drawing.Size(234, 16);
            this.chxAddResultToSuperMap.TabIndex = 1;
            this.chxAddResultToSuperMap.Text = "在当前地图窗口中高亮显示查询结果(&H)\r\n";
            this.chxAddResultToSuperMap.UseVisualStyleBackColor = true;
            this.chxAddResultToSuperMap.CheckedChanged += new System.EventHandler(this.chxAddResultToSuperMap_CheckedChanged);
            // 
            // chxAddDBToSelection
            // 
            this.chxAddDBToSelection.AutoSize = true;
            this.chxAddDBToSelection.Location = new System.Drawing.Point(22, 28);
            this.chxAddDBToSelection.Name = "chxAddDBToSelection";
            this.chxAddDBToSelection.Size = new System.Drawing.Size(162, 16);
            this.chxAddDBToSelection.TabIndex = 0;
            this.chxAddDBToSelection.Text = "添加查询结果到选择集(&B)";
            this.chxAddDBToSelection.UseVisualStyleBackColor = true;
            this.chxAddDBToSelection.CheckedChanged += new System.EventHandler(this.chxAddDBToSelection_CheckedChanged);
            // 
            // btnExe_Query
            // 
            this.btnExe_Query.Location = new System.Drawing.Point(365, 311);
            this.btnExe_Query.Name = "btnExe_Query";
            this.btnExe_Query.Size = new System.Drawing.Size(90, 23);
            this.btnExe_Query.TabIndex = 4;
            this.btnExe_Query.Text = "查询(&Q)";
            this.btnExe_Query.UseVisualStyleBackColor = true;
            this.btnExe_Query.Click += new System.EventHandler(this.btnExe_Query_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(472, 311);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(574, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(559, 17);
            this.StatusLabel1.Spring = true;
            this.StatusLabel1.Text = "空间查询...";
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmSpatialQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 365);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstQueryModel);
            this.Controls.Add(this.btnExe_Query);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSpatialQuery";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "空间查询";
            this.TitleText = "空间查询";
            this.Load += new System.EventHandler(this.frmSpacialQuery_Load);
            this.Activated += new System.EventHandler(this.frmSpatialQuery_Activated);
            this.Leave += new System.EventHandler(this.frmSpatialQuery_Leave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstQueryDB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnSelectReset;
        private DevComponents.DotNetBar.ButtonX btnSQL_Condation;
        private System.Windows.Forms.Button btnUnSelect;
        private System.Windows.Forms.Button btnSelAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chxSaveQueryResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDT;
        private System.Windows.Forms.ComboBox cbxDS;
        private System.Windows.Forms.Button btnExe_Query;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chxAddDBToSelection;
        private System.Windows.Forms.CheckBox chxAddResultToSuperMap;
        private System.Windows.Forms.CheckBox chxOnlySpacial;
        private DevComponents.DotNetBar.Controls.ListViewEx lstQueryModel;
        private DevComponents.DotNetBar.ButtonX btnQueryModelSelect;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.Button btnImportTxt;
        private System.Windows.Forms.CheckBox chxReport;
    }
}