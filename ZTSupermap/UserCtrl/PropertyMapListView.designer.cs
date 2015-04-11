namespace ZTSupermap.UserCtrl
{
    partial class PropertyMapListView
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNot = new System.Windows.Forms.Button();
            this.mycombox = new System.Windows.Forms.ComboBox();
            this.myListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(162, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 33;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNot
            // 
            this.btnSelectNot.Location = new System.Drawing.Point(243, 3);
            this.btnSelectNot.Name = "btnSelectNot";
            this.btnSelectNot.Size = new System.Drawing.Size(75, 23);
            this.btnSelectNot.TabIndex = 32;
            this.btnSelectNot.Text = "反选";
            this.btnSelectNot.UseVisualStyleBackColor = true;
            this.btnSelectNot.Click += new System.EventHandler(this.btnSelectNot_Click);
            // 
            // mycombox
            // 
            this.mycombox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mycombox.FormattingEnabled = true;
            this.mycombox.Location = new System.Drawing.Point(185, 111);
            this.mycombox.Name = "mycombox";
            this.mycombox.Size = new System.Drawing.Size(97, 20);
            this.mycombox.TabIndex = 31;
            this.mycombox.Visible = false;
            this.mycombox.SelectedIndexChanged += new System.EventHandler(this.mycombox_SelectedIndexChanged);
            // 
            // myListView
            // 
            this.myListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.myListView.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.myListView.CheckBoxes = true;
            this.myListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.myListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myListView.FullRowSelect = true;
            this.myListView.GridLines = true;
            this.myListView.LabelEdit = true;
            this.myListView.Location = new System.Drawing.Point(0, 0);
            this.myListView.Name = "myListView";
            this.myListView.Size = new System.Drawing.Size(330, 455);
            this.myListView.TabIndex = 0;
            this.myListView.UseCompatibleStateImageBehavior = false;
            this.myListView.View = System.Windows.Forms.View.Details;
            this.myListView.MouseCaptureChanged += new System.EventHandler(this.myListView_MouseCaptureChanged);
            this.myListView.Click += new System.EventHandler(this.myListView_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "结果列名称";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "结果列别名";
            this.columnHeader2.Width = 81;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "结果数据类型";
            this.columnHeader3.Width = 0;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "来源列名称";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "来源列别名";
            this.columnHeader5.Width = 84;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "源数据类型";
            this.columnHeader6.Width = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mycombox);
            this.splitContainer1.Panel1.Controls.Add(this.myListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectNot);
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectAll);
            this.splitContainer1.Size = new System.Drawing.Size(330, 487);
            this.splitContainer1.SplitterDistance = 455;
            this.splitContainer1.TabIndex = 34;
            // 
            // PropertyMapListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PropertyMapListView";
            this.Size = new System.Drawing.Size(330, 487);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNot;
        private System.Windows.Forms.ComboBox mycombox;
        public System.Windows.Forms.ListView myListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
