namespace ZTSupermap.UI
{
    partial class MapConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapConnect));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btnSelectAll = new DevComponents.DotNetBar.ButtonX();
            this.btnSelRevers = new DevComponents.DotNetBar.ButtonX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnLarge = new DevComponents.DotNetBar.ButtonX();
            this.btnList = new DevComponents.DotNetBar.ButtonX();
            this.btnTile = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(12, 34);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(328, 167);
            this.listView1.SmallImageList = this.imageList2;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "map");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "map");
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectAll.Location = new System.Drawing.Point(12, 207);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelRevers
            // 
            this.btnSelRevers.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelRevers.Location = new System.Drawing.Point(93, 207);
            this.btnSelRevers.Name = "btnSelRevers";
            this.btnSelRevers.Size = new System.Drawing.Size(75, 23);
            this.btnSelRevers.TabIndex = 2;
            this.btnSelRevers.Text = "反选";
            this.btnSelRevers.Click += new System.EventHandler(this.btnSelRevers_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.Location = new System.Drawing.Point(183, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "提交";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(265, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            // 
            // btnLarge
            // 
            this.btnLarge.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLarge.Image = ((System.Drawing.Image)(resources.GetObject("btnLarge.Image")));
            this.btnLarge.Location = new System.Drawing.Point(252, 5);
            this.btnLarge.Name = "btnLarge";
            this.btnLarge.Size = new System.Drawing.Size(25, 23);
            this.btnLarge.TabIndex = 5;
            this.btnLarge.Tooltip = "大图标显示";
            this.btnLarge.Click += new System.EventHandler(this.btnLarge_Click);
            // 
            // btnList
            // 
            this.btnList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnList.Image = ((System.Drawing.Image)(resources.GetObject("btnList.Image")));
            this.btnList.Location = new System.Drawing.Point(283, 5);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(26, 23);
            this.btnList.TabIndex = 6;
            this.btnList.Tooltip = "列表显示";
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnTile
            // 
            this.btnTile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTile.Image = ((System.Drawing.Image)(resources.GetObject("btnTile.Image")));
            this.btnTile.Location = new System.Drawing.Point(315, 5);
            this.btnTile.Name = "btnTile";
            this.btnTile.Size = new System.Drawing.Size(25, 23);
            this.btnTile.TabIndex = 7;
            this.btnTile.Tooltip = "平铺显示";
            this.btnTile.Click += new System.EventHandler(this.btnTile_Click);
            // 
            // MapConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 239);
            this.Controls.Add(this.btnTile);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnLarge);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSelRevers);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapConnect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择地图";
            this.Load += new System.EventHandler(this.MapConnect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private DevComponents.DotNetBar.ButtonX btnSelectAll;
        private DevComponents.DotNetBar.ButtonX btnSelRevers;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonX btnLarge;
        private DevComponents.DotNetBar.ButtonX btnList;
        private DevComponents.DotNetBar.ButtonX btnTile;
        private System.Windows.Forms.ImageList imageList2;
    }
}