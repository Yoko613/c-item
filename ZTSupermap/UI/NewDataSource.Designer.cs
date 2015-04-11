namespace ZTSupermap.UI
{
    partial class NewDataSource
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
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("文件数据源", 0);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("SQL+数据源", 1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Oracle数据源", 2);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("SQL数据源", 3);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Oracle Spatial", 4);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDataSource));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageListDS = new System.Windows.Forms.ImageList(this.components);
            this.panelSDB = new System.Windows.Forms.Panel();
            this.btnSDBPath = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSDBName = new System.Windows.Forms.TextBox();
            this.txtSDBPath = new System.Windows.Forms.TextBox();
            this.txtDsPsw = new System.Windows.Forms.TextBox();
            this.panelDatabase = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPsw = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.chkTran = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetPrj = new DevComponents.DotNetBar.ButtonX();
            this.panelSDB.SuspendLayout();
            this.panelDatabase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            listViewItem6.Tag = "0";
            listViewItem6.ToolTipText = "文件数据源";
            listViewItem7.Tag = "1";
            listViewItem7.ToolTipText = "创建 SQLPlus 数据源";
            listViewItem8.Tag = "2";
            listViewItem8.ToolTipText = "创建 Oracle 数据源";
            listViewItem9.Tag = "3";
            listViewItem9.ToolTipText = "创建 SQL 数据源";
            listViewItem10.Tag = "4";
            listViewItem10.ToolTipText = "Oracle Spatial";
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.listView1.LargeImageList = this.imageListDS;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(136, 320);
            this.listView1.SmallImageList = this.imageListDS;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageListDS
            // 
            this.imageListDS.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDS.ImageStream")));
            this.imageListDS.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDS.Images.SetKeyName(0, "sdb.jpg");
            this.imageListDS.Images.SetKeyName(1, "sql+.jpg");
            this.imageListDS.Images.SetKeyName(2, "oracle.jpg");
            this.imageListDS.Images.SetKeyName(3, "sql.jpg");
            this.imageListDS.Images.SetKeyName(4, "orclesp.jpg");
            // 
            // panelSDB
            // 
            this.panelSDB.Controls.Add(this.btnSDBPath);
            this.panelSDB.Controls.Add(this.label2);
            this.panelSDB.Controls.Add(this.label9);
            this.panelSDB.Controls.Add(this.label1);
            this.panelSDB.Controls.Add(this.txtSDBName);
            this.panelSDB.Controls.Add(this.txtSDBPath);
            this.panelSDB.Controls.Add(this.txtDsPsw);
            this.panelSDB.Location = new System.Drawing.Point(456, 10);
            this.panelSDB.Name = "panelSDB";
            this.panelSDB.Size = new System.Drawing.Size(290, 141);
            this.panelSDB.TabIndex = 1;
            // 
            // btnSDBPath
            // 
            this.btnSDBPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSDBPath.Location = new System.Drawing.Point(248, 16);
            this.btnSDBPath.Name = "btnSDBPath";
            this.btnSDBPath.Size = new System.Drawing.Size(32, 21);
            this.btnSDBPath.TabIndex = 4;
            this.btnSDBPath.Text = "...";
            this.btnSDBPath.Click += new System.EventHandler(this.btnSDBPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件名：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "数据源密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件路径：";
            // 
            // txtSDBName
            // 
            this.txtSDBName.Location = new System.Drawing.Point(95, 48);
            this.txtSDBName.Name = "txtSDBName";
            this.txtSDBName.Size = new System.Drawing.Size(185, 21);
            this.txtSDBName.TabIndex = 1;
            this.txtSDBName.TextChanged += new System.EventHandler(this.txtSDBName_TextChanged);
            // 
            // txtSDBPath
            // 
            this.txtSDBPath.BackColor = System.Drawing.Color.White;
            this.txtSDBPath.Location = new System.Drawing.Point(95, 16);
            this.txtSDBPath.Name = "txtSDBPath";
            this.txtSDBPath.ReadOnly = true;
            this.txtSDBPath.Size = new System.Drawing.Size(151, 21);
            this.txtSDBPath.TabIndex = 0;
            // 
            // txtDsPsw
            // 
            this.txtDsPsw.Location = new System.Drawing.Point(95, 77);
            this.txtDsPsw.Name = "txtDsPsw";
            this.txtDsPsw.PasswordChar = '*';
            this.txtDsPsw.Size = new System.Drawing.Size(185, 21);
            this.txtDsPsw.TabIndex = 1;
            // 
            // panelDatabase
            // 
            this.panelDatabase.Controls.Add(this.label3);
            this.panelDatabase.Controls.Add(this.txtPsw);
            this.panelDatabase.Controls.Add(this.label5);
            this.panelDatabase.Controls.Add(this.label4);
            this.panelDatabase.Controls.Add(this.labelServer);
            this.panelDatabase.Controls.Add(this.txtUser);
            this.panelDatabase.Controls.Add(this.txtDatabase);
            this.panelDatabase.Controls.Add(this.txtServer);
            this.panelDatabase.Location = new System.Drawing.Point(142, 10);
            this.panelDatabase.Name = "panelDatabase";
            this.panelDatabase.Size = new System.Drawing.Size(290, 141);
            this.panelDatabase.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "密码：";
            // 
            // txtPsw
            // 
            this.txtPsw.Location = new System.Drawing.Point(98, 105);
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.PasswordChar = '*';
            this.txtPsw.Size = new System.Drawing.Size(180, 21);
            this.txtPsw.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "用户名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "数据源名称：";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(11, 19);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(77, 12);
            this.labelServer.TabIndex = 4;
            this.labelServer.Text = "服务器名称：";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(98, 77);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(180, 21);
            this.txtUser.TabIndex = 2;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(98, 48);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(180, 21);
            this.txtDatabase.TabIndex = 1;
            this.txtDatabase.TextChanged += new System.EventHandler(this.txtDatabase_TextChanged);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(98, 16);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(180, 21);
            this.txtServer.TabIndex = 0;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkReadOnly);
            this.groupBox1.Controls.Add(this.chkTran);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtAlias);
            this.groupBox1.Location = new System.Drawing.Point(142, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打开方式";
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.AutoSize = true;
            this.chkReadOnly.Location = new System.Drawing.Point(206, 55);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(48, 16);
            this.chkReadOnly.TabIndex = 7;
            this.chkReadOnly.Text = "只读";
            this.chkReadOnly.UseVisualStyleBackColor = true;
            // 
            // chkTran
            // 
            this.chkTran.AutoSize = true;
            this.chkTran.Location = new System.Drawing.Point(98, 55);
            this.chkTran.Name = "chkTran";
            this.chkTran.Size = new System.Drawing.Size(48, 16);
            this.chkTran.TabIndex = 6;
            this.chkTran.Text = "事务";
            this.chkTran.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "数据源别名：";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(98, 20);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(180, 21);
            this.txtAlias.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(206, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取 消";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(125, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "确 定";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSetPrj);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(142, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 45);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnSetPrj
            // 
            this.btnSetPrj.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetPrj.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetPrj.Location = new System.Drawing.Point(14, 14);
            this.btnSetPrj.Name = "btnSetPrj";
            this.btnSetPrj.Size = new System.Drawing.Size(75, 23);
            this.btnSetPrj.TabIndex = 6;
            this.btnSetPrj.Text = "设置投影...";
            this.btnSetPrj.Click += new System.EventHandler(this.btnSetPrj_Click);
            // 
            // NewDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 320);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panelSDB);
            this.Controls.Add(this.panelDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewDataSource";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建数据源";
            this.Load += new System.EventHandler(this.NewDataSource_Load);
            this.panelSDB.ResumeLayout(false);
            this.panelSDB.PerformLayout();
            this.panelDatabase.ResumeLayout(false);
            this.panelDatabase.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageListDS;
        private System.Windows.Forms.Panel panelSDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSDBName;
        private System.Windows.Forms.TextBox txtSDBPath;
        private DevComponents.DotNetBar.ButtonX btnSDBPath;
        private System.Windows.Forms.Panel panelDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDsPsw;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkReadOnly;
        private System.Windows.Forms.CheckBox chkTran;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPsw;
        private DevComponents.DotNetBar.ButtonX btnSetPrj;
    }
}