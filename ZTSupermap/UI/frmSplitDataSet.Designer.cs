namespace ZTSupermap.UI
{
    partial class frmSplitDataSet
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chxIsOverOrderDT = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtRegion_Out_Info = new System.Windows.Forms.RadioButton();
            this.rbtRegion_Insert_Info = new System.Windows.Forms.RadioButton();
            this.cboOrderDTName_Info = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboOrderDSName_Info = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboIsReplaceExitDT = new System.Windows.Forms.ComboBox();
            this.cboOrderDSName_Lst = new System.Windows.Forms.ComboBox();
            this.cboCJFS_Lst = new System.Windows.Forms.ComboBox();
            this.cboOrderDTName_Lst = new System.Windows.Forms.ComboBox();
            this.lstSplitLayerInfo = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDel_Region = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboGeoLst = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstZBLst = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 285);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(585, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据裁剪设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chxIsOverOrderDT);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.cboOrderDTName_Info);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboOrderDSName_Info);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(8, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 93);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详细设置:";
            // 
            // chxIsOverOrderDT
            // 
            this.chxIsOverOrderDT.AutoSize = true;
            this.chxIsOverOrderDT.Location = new System.Drawing.Point(261, 64);
            this.chxIsOverOrderDT.Name = "chxIsOverOrderDT";
            this.chxIsOverOrderDT.Size = new System.Drawing.Size(132, 16);
            this.chxIsOverOrderDT.TabIndex = 6;
            this.chxIsOverOrderDT.Text = "是否覆盖目标数据集";
            this.toolTip1.SetToolTip(this.chxIsOverOrderDT, "对于已经存在的目标数据集是否进行覆盖操作");
            this.chxIsOverOrderDT.UseVisualStyleBackColor = true;
            this.chxIsOverOrderDT.CheckedChanged += new System.EventHandler(this.chxIsOverOrderDT_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtRegion_Out_Info);
            this.groupBox4.Controls.Add(this.rbtRegion_Insert_Info);
            this.groupBox4.Location = new System.Drawing.Point(251, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(176, 42);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "裁剪方式:";
            // 
            // rbtRegion_Out_Info
            // 
            this.rbtRegion_Out_Info.AutoSize = true;
            this.rbtRegion_Out_Info.Location = new System.Drawing.Point(93, 16);
            this.rbtRegion_Out_Info.Name = "rbtRegion_Out_Info";
            this.rbtRegion_Out_Info.Size = new System.Drawing.Size(77, 16);
            this.rbtRegion_Out_Info.TabIndex = 1;
            this.rbtRegion_Out_Info.TabStop = true;
            this.rbtRegion_Out_Info.Text = "区域外(&O)";
            this.rbtRegion_Out_Info.UseVisualStyleBackColor = true;
            this.rbtRegion_Out_Info.CheckedChanged += new System.EventHandler(this.rbtRegion_Out_Info_CheckedChanged);
            // 
            // rbtRegion_Insert_Info
            // 
            this.rbtRegion_Insert_Info.AutoSize = true;
            this.rbtRegion_Insert_Info.Checked = true;
            this.rbtRegion_Insert_Info.Location = new System.Drawing.Point(10, 16);
            this.rbtRegion_Insert_Info.Name = "rbtRegion_Insert_Info";
            this.rbtRegion_Insert_Info.Size = new System.Drawing.Size(77, 16);
            this.rbtRegion_Insert_Info.TabIndex = 0;
            this.rbtRegion_Insert_Info.TabStop = true;
            this.rbtRegion_Insert_Info.Text = "区域内(&I)";
            this.rbtRegion_Insert_Info.UseVisualStyleBackColor = true;
            this.rbtRegion_Insert_Info.CheckedChanged += new System.EventHandler(this.rbtRegion_Insert_Info_CheckedChanged);
            // 
            // cboOrderDTName_Info
            // 
            this.cboOrderDTName_Info.FormattingEnabled = true;
            this.cboOrderDTName_Info.Location = new System.Drawing.Point(99, 62);
            this.cboOrderDTName_Info.Name = "cboOrderDTName_Info";
            this.cboOrderDTName_Info.Size = new System.Drawing.Size(140, 20);
            this.cboOrderDTName_Info.TabIndex = 3;
            this.cboOrderDTName_Info.SelectedIndexChanged += new System.EventHandler(this.cboOrderDTName_Info_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "目标数据集:";
            // 
            // cboOrderDSName_Info
            // 
            this.cboOrderDSName_Info.FormattingEnabled = true;
            this.cboOrderDSName_Info.Location = new System.Drawing.Point(99, 27);
            this.cboOrderDSName_Info.Name = "cboOrderDSName_Info";
            this.cboOrderDSName_Info.Size = new System.Drawing.Size(140, 20);
            this.cboOrderDSName_Info.TabIndex = 1;
            this.cboOrderDSName_Info.SelectedIndexChanged += new System.EventHandler(this.cboOrderDSName_Info_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "目标数据源:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboIsReplaceExitDT);
            this.groupBox1.Controls.Add(this.cboOrderDSName_Lst);
            this.groupBox1.Controls.Add(this.cboCJFS_Lst);
            this.groupBox1.Controls.Add(this.cboOrderDTName_Lst);
            this.groupBox1.Controls.Add(this.lstSplitLayerInfo);
            this.groupBox1.Controls.Add(this.menuStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层切割设置:";
            // 
            // cboIsReplaceExitDT
            // 
            this.cboIsReplaceExitDT.FormattingEnabled = true;
            this.cboIsReplaceExitDT.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cboIsReplaceExitDT.Location = new System.Drawing.Point(488, 35);
            this.cboIsReplaceExitDT.Name = "cboIsReplaceExitDT";
            this.cboIsReplaceExitDT.Size = new System.Drawing.Size(74, 20);
            this.cboIsReplaceExitDT.TabIndex = 7;
            this.cboIsReplaceExitDT.SelectedIndexChanged += new System.EventHandler(this.cboIsReplaceExitDT_SelectedIndexChanged);
            // 
            // cboOrderDSName_Lst
            // 
            this.cboOrderDSName_Lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderDSName_Lst.FormattingEnabled = true;
            this.cboOrderDSName_Lst.Location = new System.Drawing.Point(101, 35);
            this.cboOrderDSName_Lst.Name = "cboOrderDSName_Lst";
            this.cboOrderDSName_Lst.Size = new System.Drawing.Size(143, 20);
            this.cboOrderDSName_Lst.TabIndex = 6;
            this.cboOrderDSName_Lst.SelectedIndexChanged += new System.EventHandler(this.cboOrderDSName_Lst_SelectedIndexChanged);
            // 
            // cboCJFS_Lst
            // 
            this.cboCJFS_Lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCJFS_Lst.FormattingEnabled = true;
            this.cboCJFS_Lst.Items.AddRange(new object[] {
            "区域内",
            "区域外"});
            this.cboCJFS_Lst.Location = new System.Drawing.Point(395, 35);
            this.cboCJFS_Lst.Name = "cboCJFS_Lst";
            this.cboCJFS_Lst.Size = new System.Drawing.Size(80, 20);
            this.cboCJFS_Lst.TabIndex = 3;
            this.cboCJFS_Lst.SelectedIndexChanged += new System.EventHandler(this.cboCJFS_Lst_SelectedIndexChanged);
            // 
            // cboOrderDTName_Lst
            // 
            this.cboOrderDTName_Lst.FormattingEnabled = true;
            this.cboOrderDTName_Lst.Location = new System.Drawing.Point(250, 35);
            this.cboOrderDTName_Lst.Name = "cboOrderDTName_Lst";
            this.cboOrderDTName_Lst.Size = new System.Drawing.Size(139, 20);
            this.cboOrderDTName_Lst.TabIndex = 2;
            this.cboOrderDTName_Lst.SelectedIndexChanged += new System.EventHandler(this.cboOrderDTName_Lst_SelectedIndexChanged);
            // 
            // lstSplitLayerInfo
            // 
            this.lstSplitLayerInfo.CheckBoxes = true;
            this.lstSplitLayerInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader7});
            this.lstSplitLayerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSplitLayerInfo.GridLines = true;
            this.lstSplitLayerInfo.Location = new System.Drawing.Point(3, 17);
            this.lstSplitLayerInfo.Name = "lstSplitLayerInfo";
            this.lstSplitLayerInfo.Size = new System.Drawing.Size(573, 111);
            this.lstSplitLayerInfo.TabIndex = 0;
            this.lstSplitLayerInfo.UseCompatibleStateImageBehavior = false;
            this.lstSplitLayerInfo.View = System.Windows.Forms.View.Details;
            this.lstSplitLayerInfo.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lstSplitLayerInfo_ColumnWidthChanged);
            this.lstSplitLayerInfo.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstSplitLayerInfo_ItemChecked);
            this.lstSplitLayerInfo.MouseCaptureChanged += new System.EventHandler(this.lstSplitLayerInfo_MouseCaptureChanged);
            this.lstSplitLayerInfo.Click += new System.EventHandler(this.lstSplitLayerInfo_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "图层名称";
            this.columnHeader4.Width = 92;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "目标数据源";
            this.columnHeader5.Width = 146;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "目标数据集";
            this.columnHeader6.Width = 144;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "裁剪方式";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "是否覆盖";
            this.columnHeader7.Width = 76;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选AToolStripMenuItem,
            this.反选ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 128);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(573, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.全选AToolStripMenuItem.Text = "全选(&L)";
            this.全选AToolStripMenuItem.Click += new System.EventHandler(this.全选AToolStripMenuItem_Click);
            // 
            // 反选ToolStripMenuItem
            // 
            this.反选ToolStripMenuItem.Name = "反选ToolStripMenuItem";
            this.反选ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.反选ToolStripMenuItem.Text = "反选(&R)";
            this.反选ToolStripMenuItem.Click += new System.EventHandler(this.反选ToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDel_Region);
            this.tabPage2.Controls.Add(this.btnRestore);
            this.tabPage2.Controls.Add(this.btnEdit);
            this.tabPage2.Controls.Add(this.txtY);
            this.tabPage2.Controls.Add(this.txtX);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.cboGeoLst);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(585, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "裁剪区域设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDel_Region
            // 
            this.btnDel_Region.Location = new System.Drawing.Point(260, 232);
            this.btnDel_Region.Name = "btnDel_Region";
            this.btnDel_Region.Size = new System.Drawing.Size(111, 23);
            this.btnDel_Region.TabIndex = 12;
            this.btnDel_Region.Text = "删除此多边形";
            this.btnDel_Region.UseVisualStyleBackColor = true;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(260, 201);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(74, 23);
            this.btnRestore.TabIndex = 9;
            this.btnRestore.Text = "恢复(&R)";
            this.btnRestore.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(260, 174);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(74, 23);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "修改(&M)";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(65, 203);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(178, 21);
            this.txtY.TabIndex = 7;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(65, 176);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(178, 21);
            this.txtX.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Y坐标:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "X坐标:";
            // 
            // cboGeoLst
            // 
            this.cboGeoLst.FormattingEnabled = true;
            this.cboGeoLst.Location = new System.Drawing.Point(65, 232);
            this.cboGeoLst.Name = "cboGeoLst";
            this.cboGeoLst.Size = new System.Drawing.Size(178, 20);
            this.cboGeoLst.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "对象列表:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstZBLst);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(579, 167);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "裁剪区域坐标列表:";
            // 
            // lstZBLst
            // 
            this.lstZBLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstZBLst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZBLst.GridLines = true;
            this.lstZBLst.Location = new System.Drawing.Point(3, 17);
            this.lstZBLst.Name = "lstZBLst";
            this.lstZBLst.Size = new System.Drawing.Size(573, 147);
            this.lstZBLst.TabIndex = 0;
            this.lstZBLst.UseCompatibleStateImageBehavior = false;
            this.lstZBLst.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 68;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "X坐标";
            this.columnHeader2.Width = 211;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Y坐标";
            this.columnHeader3.Width = 246;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(369, 290);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(102, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "确定(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(477, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSplitDataSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(593, 319);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmSplitDataSet";
            this.Text = "地图裁剪";
            this.Load += new System.EventHandler(this.frmSplitDataSet_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lstSplitLayerInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lstZBLst;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGeoLst;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOrderDSName_Info;
        private System.Windows.Forms.ComboBox cboOrderDTName_Lst;
        private System.Windows.Forms.ComboBox cboCJFS_Lst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboOrderDTName_Info;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtRegion_Insert_Info;
        private System.Windows.Forms.RadioButton rbtRegion_Out_Info;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnDel_Region;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反选ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboOrderDSName_Lst;
        private System.Windows.Forms.CheckBox chxIsOverOrderDT;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboIsReplaceExitDT;
    }
}