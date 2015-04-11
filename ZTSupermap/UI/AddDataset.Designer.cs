namespace ZTSupermap.UI
{
    partial class AddDataset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDataset));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnItemSelectAll = new DevComponents.DotNetBar.ButtonX();
            this.btnItemUnSelect = new DevComponents.DotNetBar.ButtonX();
            this.btnItemOK = new DevComponents.DotNetBar.ButtonX();
            this.btnItemCancel = new DevComponents.DotNetBar.ButtonX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbDatasource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnItemFilter = new DevComponents.DotNetBar.ButtonX();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.tvDataset = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnItemSelectAll);
            this.panel1.Controls.Add(this.btnItemUnSelect);
            this.panel1.Controls.Add(this.btnItemOK);
            this.panel1.Controls.Add(this.btnItemCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 357);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 42);
            this.panel1.TabIndex = 8;
            // 
            // btnItemSelectAll
            // 
            this.btnItemSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemSelectAll.Location = new System.Drawing.Point(3, 6);
            this.btnItemSelectAll.Name = "btnItemSelectAll";
            this.btnItemSelectAll.Size = new System.Drawing.Size(80, 23);
            this.btnItemSelectAll.TabIndex = 17;
            this.btnItemSelectAll.Text = "全选(&A)";
            this.btnItemSelectAll.Click += new System.EventHandler(this.btnItemSelectAll_Click);
            // 
            // btnItemUnSelect
            // 
            this.btnItemUnSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemUnSelect.Location = new System.Drawing.Point(89, 6);
            this.btnItemUnSelect.Name = "btnItemUnSelect";
            this.btnItemUnSelect.Size = new System.Drawing.Size(70, 23);
            this.btnItemUnSelect.TabIndex = 16;
            this.btnItemUnSelect.Text = "反选(&R)";
            this.btnItemUnSelect.Click += new System.EventHandler(this.btnItemUnSelect_Click);
            // 
            // btnItemOK
            // 
            this.btnItemOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemOK.Location = new System.Drawing.Point(165, 6);
            this.btnItemOK.Name = "btnItemOK";
            this.btnItemOK.Size = new System.Drawing.Size(72, 23);
            this.btnItemOK.TabIndex = 15;
            this.btnItemOK.Text = "提交(&O)";
            this.btnItemOK.Click += new System.EventHandler(this.btnItemOK_Click);
            // 
            // btnItemCancel
            // 
            this.btnItemCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnItemCancel.Location = new System.Drawing.Point(243, 6);
            this.btnItemCancel.Name = "btnItemCancel";
            this.btnItemCancel.Size = new System.Drawing.Size(63, 23);
            this.btnItemCancel.TabIndex = 14;
            this.btnItemCancel.Text = "取消(&C)";
            this.btnItemCancel.Click += new System.EventHandler(this.btnItemCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbDatasource);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtFilter);
            this.panel2.Controls.Add(this.btnItemFilter);
            this.panel2.Controls.Add(this.lbDatabase);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(319, 75);
            this.panel2.TabIndex = 10;
            // 
            // cmbDatasource
            // 
            this.cmbDatasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatasource.FormattingEnabled = true;
            this.cmbDatasource.Location = new System.Drawing.Point(64, 9);
            this.cmbDatasource.Name = "cmbDatasource";
            this.cmbDatasource.Size = new System.Drawing.Size(245, 20);
            this.cmbDatasource.TabIndex = 12;
            this.cmbDatasource.SelectedIndexChanged += new System.EventHandler(this.cmbDatasource_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据源:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(64, 36);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(184, 21);
            this.txtFilter.TabIndex = 9;
            // 
            // btnItemFilter
            // 
            this.btnItemFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemFilter.Location = new System.Drawing.Point(254, 36);
            this.btnItemFilter.Name = "btnItemFilter";
            this.btnItemFilter.Size = new System.Drawing.Size(55, 23);
            this.btnItemFilter.TabIndex = 10;
            this.btnItemFilter.Text = "筛选(&F)";
            this.btnItemFilter.Click += new System.EventHandler(this.btnItemFilter_Click);
            // 
            // lbDatabase
            // 
            this.lbDatabase.AutoSize = true;
            this.lbDatabase.Location = new System.Drawing.Point(17, 41);
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(47, 12);
            this.lbDatabase.TabIndex = 8;
            this.lbDatabase.Text = "关键字:";
            // 
            // tvDataset
            // 
            this.tvDataset.CheckBoxes = true;
            this.tvDataset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDataset.ImageIndex = 0;
            this.tvDataset.ImageList = this.imageList1;
            this.tvDataset.Location = new System.Drawing.Point(0, 75);
            this.tvDataset.Name = "tvDataset";
            this.tvDataset.SelectedImageIndex = 0;
            this.tvDataset.Size = new System.Drawing.Size(319, 282);
            this.tvDataset.TabIndex = 11;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0工作空间.ico");
            this.imageList1.Images.SetKeyName(1, "1工程.ico");
            this.imageList1.Images.SetKeyName(2, "2组.ico");
            this.imageList1.Images.SetKeyName(3, "3数据集.ico");
            this.imageList1.Images.SetKeyName(4, "4图片.ico");
            this.imageList1.Images.SetKeyName(5, "5地图.ico");
            this.imageList1.Images.SetKeyName(6, "6媒体.ico");
            this.imageList1.Images.SetKeyName(7, "7报表.ico");
            this.imageList1.Images.SetKeyName(8, "8文本.ico");
            this.imageList1.Images.SetKeyName(9, "9网页.JPG");
            this.imageList1.Images.SetKeyName(10, "10文档.jpg");
            this.imageList1.Images.SetKeyName(11, "11其他.jpg");
            this.imageList1.Images.SetKeyName(12, "12属性.ico");
            this.imageList1.Images.SetKeyName(13, "13点.ico");
            this.imageList1.Images.SetKeyName(14, "14线.ico");
            this.imageList1.Images.SetKeyName(15, "15网络数据.ico");
            this.imageList1.Images.SetKeyName(16, "16面.ico");
            this.imageList1.Images.SetKeyName(17, "17宗地.jpg");
            this.imageList1.Images.SetKeyName(18, "18文字.ico");
            this.imageList1.Images.SetKeyName(19, "19曲线.ico");
            this.imageList1.Images.SetKeyName(20, "20影像.ico");
            this.imageList1.Images.SetKeyName(21, "21MrSID 数据集.jpg");
            this.imageList1.Images.SetKeyName(22, "22Grid.ico");
            this.imageList1.Images.SetKeyName(23, "23DEM.ico");
            this.imageList1.Images.SetKeyName(24, "24ECW.jpg");
            this.imageList1.Images.SetKeyName(25, "25WMS.ico");
            this.imageList1.Images.SetKeyName(26, "26WCS.ico");
            this.imageList1.Images.SetKeyName(27, "27三维点.jpg");
            this.imageList1.Images.SetKeyName(28, "28TIN.JPG");
            this.imageList1.Images.SetKeyName(29, "29CAD.jpg");
            this.imageList1.Images.SetKeyName(30, "30路由数据集.jpg");
            // 
            // AddDataset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 399);
            this.Controls.Add(this.tvDataset);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDataset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加数据集";
            this.Load += new System.EventHandler(this.AddDataset_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnItemSelectAll;
        private DevComponents.DotNetBar.ButtonX btnItemUnSelect;
        private DevComponents.DotNetBar.ButtonX btnItemOK;
        private DevComponents.DotNetBar.ButtonX btnItemCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFilter;
        private DevComponents.DotNetBar.ButtonX btnItemFilter;
        private System.Windows.Forms.Label lbDatabase;
        private System.Windows.Forms.TreeView tvDataset;
        private System.Windows.Forms.ComboBox cmbDatasource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;

    }
}