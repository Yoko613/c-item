namespace ZTSupermap.UI
{
    partial class NewDataset
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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("点数据集", 0);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("线数据集", 1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("面数据集", 2);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "文本数据集"}, 3, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, null);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("属性数据集", 4);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("模板创建数据集", 5);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDataset));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageListDT = new System.Windows.Forms.ImageList(this.components);
            this.txtDTName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combChar = new System.Windows.Forms.ComboBox();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.combDatasourcename = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTempDt = new System.Windows.Forms.ComboBox();
            this.comboBoxTempDs = new System.Windows.Forms.ComboBox();
            this.btnSetPrj = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            listViewItem7.ToolTipText = "创建点数据集";
            listViewItem8.ToolTipText = "创建线数据集";
            listViewItem9.ToolTipText = "创建面数据集";
            listViewItem10.ToolTipText = "创建文本数据集";
            listViewItem10.UseItemStyleForSubItems = false;
            listViewItem11.ToolTipText = "创建纯属性表数据集";
            listViewItem12.ToolTipText = "根据现有数据集模板创建数据集";
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.listView1.LargeImageList = this.imageListDT;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.ShowGroups = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(156, 256);
            this.listView1.SmallImageList = this.imageListDT;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageListDT
            // 
            this.imageListDT.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDT.ImageStream")));
            this.imageListDT.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDT.Images.SetKeyName(0, "point");
            this.imageListDT.Images.SetKeyName(1, "line");
            this.imageListDT.Images.SetKeyName(2, "region");
            this.imageListDT.Images.SetKeyName(3, "text");
            this.imageListDT.Images.SetKeyName(4, "table");
            this.imageListDT.Images.SetKeyName(5, "dup");
            // 
            // txtDTName
            // 
            this.txtDTName.Location = new System.Drawing.Point(99, 49);
            this.txtDTName.Name = "txtDTName";
            this.txtDTName.Size = new System.Drawing.Size(191, 21);
            this.txtDTName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据集名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "编码方式：";
            // 
            // combChar
            // 
            this.combChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combChar.FormattingEnabled = true;
            this.combChar.Items.AddRange(new object[] {
            "未编码",
            "SDC",
            "SWC"});
            this.combChar.Location = new System.Drawing.Point(99, 76);
            this.combChar.Name = "combChar";
            this.combChar.Size = new System.Drawing.Size(191, 20);
            this.combChar.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(308, 223);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "确　定";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取　消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // combDatasourcename
            // 
            this.combDatasourcename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDatasourcename.FormattingEnabled = true;
            this.combDatasourcename.Location = new System.Drawing.Point(99, 23);
            this.combDatasourcename.Name = "combDatasourcename";
            this.combDatasourcename.Size = new System.Drawing.Size(191, 20);
            this.combDatasourcename.TabIndex = 8;
            this.combDatasourcename.SelectedIndexChanged += new System.EventHandler(this.combDatasourcename_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "目标数据源：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDTName);
            this.groupBox1.Controls.Add(this.combDatasourcename);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.combChar);
            this.groupBox1.Location = new System.Drawing.Point(173, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 109);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "目标设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBoxTempDt);
            this.groupBox2.Controls.Add(this.comboBoxTempDs);
            this.groupBox2.Location = new System.Drawing.Point(173, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 81);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "模板设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "模板数据集：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "模板数据源：";
            // 
            // comboBoxTempDt
            // 
            this.comboBoxTempDt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTempDt.FormattingEnabled = true;
            this.comboBoxTempDt.Location = new System.Drawing.Point(99, 48);
            this.comboBoxTempDt.Name = "comboBoxTempDt";
            this.comboBoxTempDt.Size = new System.Drawing.Size(191, 20);
            this.comboBoxTempDt.TabIndex = 1;
            // 
            // comboBoxTempDs
            // 
            this.comboBoxTempDs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTempDs.FormattingEnabled = true;
            this.comboBoxTempDs.Location = new System.Drawing.Point(99, 21);
            this.comboBoxTempDs.Name = "comboBoxTempDs";
            this.comboBoxTempDs.Size = new System.Drawing.Size(191, 20);
            this.comboBoxTempDs.TabIndex = 0;
            this.comboBoxTempDs.SelectedIndexChanged += new System.EventHandler(this.comboBoxTempDs_SelectedIndexChanged);
            // 
            // btnSetPrj
            // 
            this.btnSetPrj.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetPrj.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetPrj.Location = new System.Drawing.Point(173, 223);
            this.btnSetPrj.Name = "btnSetPrj";
            this.btnSetPrj.Size = new System.Drawing.Size(75, 23);
            this.btnSetPrj.TabIndex = 12;
            this.btnSetPrj.Text = "设置投影...";
            this.btnSetPrj.Click += new System.EventHandler(this.btnSetPrj_Click);
            // 
            // NewDataset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 256);
            this.Controls.Add(this.btnSetPrj);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewDataset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建数据集";
            this.Load += new System.EventHandler(this.NewDataset_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageListDT;
        private System.Windows.Forms.TextBox txtDTName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combChar;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private System.Windows.Forms.ComboBox combDatasourcename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTempDt;
        private System.Windows.Forms.ComboBox comboBoxTempDs;
        private DevComponents.DotNetBar.ButtonX btnSetPrj;
    }
}