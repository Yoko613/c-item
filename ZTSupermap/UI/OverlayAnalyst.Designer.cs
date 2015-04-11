namespace ZTSupermap.UI
{
    partial class OverlayAnalyst
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "裁减"}, "clip", System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "合并"}, "union", System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("擦除", "erase");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("求交", "inter");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("同一", "Identify");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "对称差"}, "sym", System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("更新", "update");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlayAnalyst));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFieldSet = new DevComponents.DotNetBar.ButtonX();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCombineDT = new System.Windows.Forms.TextBox();
            this.cmbCombineDS = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAnanDT = new System.Windows.Forms.ComboBox();
            this.cmbAnaDS = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSourDT = new System.Windows.Forms.ComboBox();
            this.cmbSourDS = new System.Windows.Forms.ComboBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this.listView1.LabelWrap = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(3, 10);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.Size = new System.Drawing.Size(145, 298);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.StateImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(120, 40);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "clip");
            this.imageList1.Images.SetKeyName(1, "union");
            this.imageList1.Images.SetKeyName(2, "erase");
            this.imageList1.Images.SetKeyName(3, "inter");
            this.imageList1.Images.SetKeyName(4, "Identify");
            this.imageList1.Images.SetKeyName(5, "sym");
            this.imageList1.Images.SetKeyName(6, "update");
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnFieldSet);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtCombineDT);
            this.groupBox4.Controls.Add(this.cmbCombineDS);
            this.groupBox4.Location = new System.Drawing.Point(154, 174);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 105);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "结果数据";
            // 
            // btnFieldSet
            // 
            this.btnFieldSet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFieldSet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFieldSet.Location = new System.Drawing.Point(64, 74);
            this.btnFieldSet.Name = "btnFieldSet";
            this.btnFieldSet.Size = new System.Drawing.Size(110, 23);
            this.btnFieldSet.TabIndex = 4;
            this.btnFieldSet.Text = "字段设置...";
            this.btnFieldSet.Click += new System.EventHandler(this.btnFieldSet_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "数据集：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "数据源：";
            // 
            // txtCombineDT
            // 
            this.txtCombineDT.Location = new System.Drawing.Point(64, 47);
            this.txtCombineDT.Name = "txtCombineDT";
            this.txtCombineDT.Size = new System.Drawing.Size(191, 21);
            this.txtCombineDT.TabIndex = 1;
            // 
            // cmbCombineDS
            // 
            this.cmbCombineDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCombineDS.FormattingEnabled = true;
            this.cmbCombineDS.Location = new System.Drawing.Point(64, 20);
            this.cmbCombineDS.Name = "cmbCombineDS";
            this.cmbCombineDS.Size = new System.Drawing.Size(190, 20);
            this.cmbCombineDS.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbAnanDT);
            this.groupBox2.Controls.Add(this.cmbAnaDS);
            this.groupBox2.Location = new System.Drawing.Point(154, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 76);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "叠加数据";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "数据集：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据源：";
            // 
            // cmbAnanDT
            // 
            this.cmbAnanDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnanDT.FormattingEnabled = true;
            this.cmbAnanDT.Location = new System.Drawing.Point(64, 47);
            this.cmbAnanDT.Name = "cmbAnanDT";
            this.cmbAnanDT.Size = new System.Drawing.Size(191, 20);
            this.cmbAnanDT.TabIndex = 1;
            // 
            // cmbAnaDS
            // 
            this.cmbAnaDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnaDS.FormattingEnabled = true;
            this.cmbAnaDS.Location = new System.Drawing.Point(64, 16);
            this.cmbAnaDS.Name = "cmbAnaDS";
            this.cmbAnaDS.Size = new System.Drawing.Size(191, 20);
            this.cmbAnaDS.TabIndex = 0;
            this.cmbAnaDS.SelectedIndexChanged += new System.EventHandler(this.cmbAnaDS_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbSourDT);
            this.groupBox1.Controls.Add(this.cmbSourDS);
            this.groupBox1.Location = new System.Drawing.Point(154, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源数据";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据集：";
            // 
            // cmbSourDT
            // 
            this.cmbSourDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourDT.FormattingEnabled = true;
            this.cmbSourDT.Location = new System.Drawing.Point(64, 43);
            this.cmbSourDT.Name = "cmbSourDT";
            this.cmbSourDT.Size = new System.Drawing.Size(190, 20);
            this.cmbSourDT.TabIndex = 1;
            // 
            // cmbSourDS
            // 
            this.cmbSourDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourDS.FormattingEnabled = true;
            this.cmbSourDS.Location = new System.Drawing.Point(64, 15);
            this.cmbSourDS.Name = "cmbSourDS";
            this.cmbSourDS.Size = new System.Drawing.Size(190, 20);
            this.cmbSourDS.TabIndex = 0;
            this.cmbSourDS.SelectedIndexChanged += new System.EventHandler(this.cmbSourDS_SelectedIndexChanged);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(342, 285);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 7;
            this.buttonX1.Text = "取 消";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(253, 285);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.TabIndex = 8;
            this.buttonX2.Text = "确 定";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // OverlayAnalyst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 318);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OverlayAnalyst";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "叠加分析";
            this.TopLeftCornerSize = 5;
            this.TopRightCornerSize = 5;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OverlayAnalyst_Paint);
            this.Load += new System.EventHandler(this.OverlayAnalyst_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCombineDT;
        private System.Windows.Forms.ComboBox cmbCombineDS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbAnanDT;
        private System.Windows.Forms.ComboBox cmbAnaDS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSourDT;
        private System.Windows.Forms.ComboBox cmbSourDS;
        private DevComponents.DotNetBar.ButtonX btnFieldSet;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}