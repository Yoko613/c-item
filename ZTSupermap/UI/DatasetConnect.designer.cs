namespace ZTSupermap.UI
{
    partial class DatasetConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatasetConnect));
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.plDockBottomForm = new System.Windows.Forms.Panel();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.btnItemSelectAll = new DevComponents.DotNetBar.ButtonX();
            this.btnItemUnSelect = new DevComponents.DotNetBar.ButtonX();
            this.btnItemExit = new DevComponents.DotNetBar.ButtonX();
            this.plDataset = new System.Windows.Forms.Panel();
            this.tvData = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOpenDatasource = new DevComponents.DotNetBar.ButtonX();
            this.cmbDatasetType = new System.Windows.Forms.ComboBox();
            this.Labeldatas = new System.Windows.Forms.Label();
            this.cmbDataSource = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnItemFilter = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.plDockBottomForm.SuspendLayout();
            this.plDataset.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelItem2
            // 
            this.labelItem2.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem2.BorderType = DevComponents.DotNetBar.eBorderType.Etched;
            this.labelItem2.CanCustomize = false;
            this.labelItem2.Name = "labelItem2";
            // 
            // plDockBottomForm
            // 
            this.plDockBottomForm.Controls.Add(this.btnSubmit);
            this.plDockBottomForm.Controls.Add(this.btnItemSelectAll);
            this.plDockBottomForm.Controls.Add(this.btnItemUnSelect);
            this.plDockBottomForm.Controls.Add(this.btnItemExit);
            this.plDockBottomForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plDockBottomForm.Location = new System.Drawing.Point(0, 381);
            this.plDockBottomForm.Name = "plDockBottomForm";
            this.plDockBottomForm.Size = new System.Drawing.Size(292, 34);
            this.plDockBottomForm.TabIndex = 36;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.Location = new System.Drawing.Point(155, 6);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(64, 23);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnItemSelectAll
            // 
            this.btnItemSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemSelectAll.Location = new System.Drawing.Point(10, 6);
            this.btnItemSelectAll.Name = "btnItemSelectAll";
            this.btnItemSelectAll.Size = new System.Drawing.Size(36, 23);
            this.btnItemSelectAll.TabIndex = 13;
            this.btnItemSelectAll.Text = "全选";
            this.btnItemSelectAll.Click += new System.EventHandler(this.btnItemSelectAll_Click);
            // 
            // btnItemUnSelect
            // 
            this.btnItemUnSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemUnSelect.Location = new System.Drawing.Point(50, 6);
            this.btnItemUnSelect.Name = "btnItemUnSelect";
            this.btnItemUnSelect.Size = new System.Drawing.Size(36, 23);
            this.btnItemUnSelect.TabIndex = 12;
            this.btnItemUnSelect.Text = "反选";
            this.btnItemUnSelect.Click += new System.EventHandler(this.btnItemUnSelect_Click);
            // 
            // btnItemExit
            // 
            this.btnItemExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemExit.Location = new System.Drawing.Point(225, 6);
            this.btnItemExit.Name = "btnItemExit";
            this.btnItemExit.Size = new System.Drawing.Size(64, 23);
            this.btnItemExit.TabIndex = 11;
            this.btnItemExit.Text = "退出";
            this.btnItemExit.Click += new System.EventHandler(this.btnItemExit_Click);
            // 
            // plDataset
            // 
            this.plDataset.Controls.Add(this.tvData);
            this.plDataset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plDataset.Location = new System.Drawing.Point(0, 86);
            this.plDataset.Name = "plDataset";
            this.plDataset.Size = new System.Drawing.Size(292, 295);
            this.plDataset.TabIndex = 39;
            // 
            // tvData
            // 
            this.tvData.CheckBoxes = true;
            this.tvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvData.ItemHeight = 16;
            this.tvData.Location = new System.Drawing.Point(0, 0);
            this.tvData.Name = "tvData";
            this.tvData.Size = new System.Drawing.Size(292, 295);
            this.tvData.TabIndex = 6;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOpenDatasource);
            this.panel2.Controls.Add(this.cmbDatasetType);
            this.panel2.Controls.Add(this.Labeldatas);
            this.panel2.Controls.Add(this.cmbDataSource);
            this.panel2.Controls.Add(this.txtFilter);
            this.panel2.Controls.Add(this.btnItemFilter);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbDatabase);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 86);
            this.panel2.TabIndex = 9;
            // 
            // btnOpenDatasource
            // 
            this.btnOpenDatasource.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenDatasource.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOpenDatasource.Location = new System.Drawing.Point(263, 9);
            this.btnOpenDatasource.Name = "btnOpenDatasource";
            this.btnOpenDatasource.Size = new System.Drawing.Size(17, 20);
            this.btnOpenDatasource.TabIndex = 12;
            this.btnOpenDatasource.Text = "+";
            this.btnOpenDatasource.Click += new System.EventHandler(this.btnOpenDatasource_Click);
            // 
            // cmbDatasetType
            // 
            this.cmbDatasetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatasetType.FormattingEnabled = true;
            this.cmbDatasetType.Location = new System.Drawing.Point(51, 34);
            this.cmbDatasetType.Name = "cmbDatasetType";
            this.cmbDatasetType.Size = new System.Drawing.Size(229, 20);
            this.cmbDatasetType.TabIndex = 11;
            this.cmbDatasetType.SelectedIndexChanged += new System.EventHandler(this.cmbDatasetType_SelectedIndexChanged);
            // 
            // Labeldatas
            // 
            this.Labeldatas.AutoSize = true;
            this.Labeldatas.Location = new System.Drawing.Point(4, 14);
            this.Labeldatas.Name = "Labeldatas";
            this.Labeldatas.Size = new System.Drawing.Size(47, 12);
            this.Labeldatas.TabIndex = 10;
            this.Labeldatas.Text = "数据源:";
            // 
            // cmbDataSource
            // 
            this.cmbDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataSource.FormattingEnabled = true;
            this.cmbDataSource.Location = new System.Drawing.Point(51, 9);
            this.cmbDataSource.Name = "cmbDataSource";
            this.cmbDataSource.Size = new System.Drawing.Size(212, 20);
            this.cmbDataSource.TabIndex = 9;
            this.cmbDataSource.SelectedIndexChanged += new System.EventHandler(this.cmbDataSource_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(51, 59);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(168, 21);
            this.txtFilter.TabIndex = 3;
            // 
            // btnItemFilter
            // 
            this.btnItemFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemFilter.Location = new System.Drawing.Point(225, 59);
            this.btnItemFilter.Name = "btnItemFilter";
            this.btnItemFilter.Size = new System.Drawing.Size(55, 23);
            this.btnItemFilter.TabIndex = 7;
            this.btnItemFilter.Text = "筛选(&F)";
            this.btnItemFilter.Click += new System.EventHandler(this.btnItemFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类  型:";
            // 
            // lbDatabase
            // 
            this.lbDatabase.AutoSize = true;
            this.lbDatabase.Location = new System.Drawing.Point(4, 62);
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(47, 12);
            this.lbDatabase.TabIndex = 0;
            this.lbDatabase.Text = "关键字:";
            // 
            // DatasetConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 415);
            this.Controls.Add(this.plDataset);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.plDockBottomForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 277);
            this.Name = "DatasetConnect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据集连接";
            this.Load += new System.EventHandler(this.DatasetConnect_Load);
            this.plDockBottomForm.ResumeLayout(false);
            this.plDataset.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelItem labelItem2;        
        private System.Windows.Forms.Panel plDockBottomForm;
        private DevComponents.DotNetBar.ButtonX btnItemSelectAll;
        private DevComponents.DotNetBar.ButtonX btnItemUnSelect;
        private DevComponents.DotNetBar.ButtonX btnItemExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFilter;
        private DevComponents.DotNetBar.ButtonX btnItemFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbDatabase;
        private System.Windows.Forms.TreeView tvData;
        private System.Windows.Forms.Panel plDataset;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label Labeldatas;
        private System.Windows.Forms.ComboBox cmbDataSource;
        private System.Windows.Forms.ComboBox cmbDatasetType;
        private DevComponents.DotNetBar.ButtonX btnOpenDatasource;
        private DevComponents.DotNetBar.ButtonX btnSubmit;

    }
}