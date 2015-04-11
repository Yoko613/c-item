namespace ZTSupermap.UI
{
    partial class PJTranslator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSourAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSourSet = new System.Windows.Forms.Button();
            this.btnSourView = new System.Windows.Forms.Button();
            this.cmbSourDS = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTarAdd = new System.Windows.Forms.Button();
            this.btnTarView = new System.Windows.Forms.Button();
            this.btnTarSet = new System.Windows.Forms.Button();
            this.cmbTarDS = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTransSet = new System.Windows.Forms.Button();
            this.cmbTransType = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSourDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTarDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelAll = new DevComponents.DotNetBar.ButtonX();
            this.btnSelInvert = new DevComponents.DotNetBar.ButtonX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.contMenuOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contMenuOpen.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSourAdd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSourSet);
            this.groupBox1.Controls.Add(this.btnSourView);
            this.groupBox1.Controls.Add(this.cmbSourDS);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源数据";
            // 
            // btnSourAdd
            // 
            this.btnSourAdd.Location = new System.Drawing.Point(199, 14);
            this.btnSourAdd.Name = "btnSourAdd";
            this.btnSourAdd.Size = new System.Drawing.Size(15, 23);
            this.btnSourAdd.TabIndex = 5;
            this.btnSourAdd.Text = "+";
            this.btnSourAdd.UseVisualStyleBackColor = true;
            this.btnSourAdd.Click += new System.EventHandler(this.btnSourAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "投影信息：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据源：";
            // 
            // btnSourSet
            // 
            this.btnSourSet.Location = new System.Drawing.Point(154, 42);
            this.btnSourSet.Name = "btnSourSet";
            this.btnSourSet.Size = new System.Drawing.Size(61, 23);
            this.btnSourSet.TabIndex = 2;
            this.btnSourSet.Text = "设　置";
            this.btnSourSet.UseVisualStyleBackColor = true;
            this.btnSourSet.Click += new System.EventHandler(this.btnSourSet_Click);
            // 
            // btnSourView
            // 
            this.btnSourView.Location = new System.Drawing.Point(80, 42);
            this.btnSourView.Name = "btnSourView";
            this.btnSourView.Size = new System.Drawing.Size(65, 23);
            this.btnSourView.TabIndex = 1;
            this.btnSourView.Text = "查　看";
            this.btnSourView.UseVisualStyleBackColor = true;
            this.btnSourView.Click += new System.EventHandler(this.btnSourView_Click);
            // 
            // cmbSourDS
            // 
            this.cmbSourDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourDS.FormattingEnabled = true;
            this.cmbSourDS.Location = new System.Drawing.Point(80, 16);
            this.cmbSourDS.Name = "cmbSourDS";
            this.cmbSourDS.Size = new System.Drawing.Size(117, 20);
            this.cmbSourDS.TabIndex = 0;
            this.cmbSourDS.SelectedIndexChanged += new System.EventHandler(this.cmbSourDS_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnTarAdd);
            this.groupBox2.Controls.Add(this.btnTarView);
            this.groupBox2.Controls.Add(this.btnTarSet);
            this.groupBox2.Controls.Add(this.cmbTarDS);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果数据";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "投影信息：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据源：";
            // 
            // btnTarAdd
            // 
            this.btnTarAdd.Location = new System.Drawing.Point(199, 16);
            this.btnTarAdd.Name = "btnTarAdd";
            this.btnTarAdd.Size = new System.Drawing.Size(15, 24);
            this.btnTarAdd.TabIndex = 5;
            this.btnTarAdd.Text = "+";
            this.btnTarAdd.UseVisualStyleBackColor = true;
            this.btnTarAdd.Click += new System.EventHandler(this.btnTarAdd_Click);
            // 
            // btnTarView
            // 
            this.btnTarView.Location = new System.Drawing.Point(80, 46);
            this.btnTarView.Name = "btnTarView";
            this.btnTarView.Size = new System.Drawing.Size(65, 23);
            this.btnTarView.TabIndex = 2;
            this.btnTarView.Text = "查　看";
            this.btnTarView.UseVisualStyleBackColor = true;
            this.btnTarView.Click += new System.EventHandler(this.btnTarView_Click);
            // 
            // btnTarSet
            // 
            this.btnTarSet.Location = new System.Drawing.Point(154, 46);
            this.btnTarSet.Name = "btnTarSet";
            this.btnTarSet.Size = new System.Drawing.Size(60, 23);
            this.btnTarSet.TabIndex = 1;
            this.btnTarSet.Text = "设　置";
            this.btnTarSet.UseVisualStyleBackColor = true;
            this.btnTarSet.Click += new System.EventHandler(this.btnTarSet_Click);
            // 
            // cmbTarDS
            // 
            this.cmbTarDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarDS.FormattingEnabled = true;
            this.cmbTarDS.Location = new System.Drawing.Point(80, 18);
            this.cmbTarDS.Name = "cmbTarDS";
            this.cmbTarDS.Size = new System.Drawing.Size(117, 20);
            this.cmbTarDS.TabIndex = 0;
            this.cmbTarDS.SelectedIndexChanged += new System.EventHandler(this.cmbTarDS_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnTransSet);
            this.groupBox3.Controls.Add(this.cmbTransType);
            this.groupBox3.Location = new System.Drawing.Point(12, 205);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参考系转换设置";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "转换参数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "转换方法：";
            // 
            // btnTransSet
            // 
            this.btnTransSet.Location = new System.Drawing.Point(80, 47);
            this.btnTransSet.Name = "btnTransSet";
            this.btnTransSet.Size = new System.Drawing.Size(134, 23);
            this.btnTransSet.TabIndex = 1;
            this.btnTransSet.Text = "设　　置";
            this.btnTransSet.UseVisualStyleBackColor = true;
            this.btnTransSet.Click += new System.EventHandler(this.btnTransSet_Click);
            // 
            // cmbTransType
            // 
            this.cmbTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransType.FormattingEnabled = true;
            this.cmbTransType.Location = new System.Drawing.Point(80, 20);
            this.cmbTransType.Name = "cmbTransType";
            this.cmbTransType.Size = new System.Drawing.Size(134, 20);
            this.cmbTransType.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChk,
            this.colSourDT,
            this.colTarDT});
            this.dataGridView1.Location = new System.Drawing.Point(243, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(325, 273);
            this.dataGridView1.TabIndex = 3;
            // 
            // colChk
            // 
            this.colChk.FillWeight = 80F;
            this.colChk.HeaderText = "是否转换";
            this.colChk.Name = "colChk";
            this.colChk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colChk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChk.Width = 80;
            // 
            // colSourDT
            // 
            this.colSourDT.FillWeight = 120F;
            this.colSourDT.HeaderText = "源数据集名";
            this.colSourDT.Name = "colSourDT";
            this.colSourDT.ReadOnly = true;
            this.colSourDT.Width = 120;
            // 
            // colTarDT
            // 
            this.colTarDT.FillWeight = 120F;
            this.colTarDT.HeaderText = "目标数据集名";
            this.colTarDT.Name = "colTarDT";
            this.colTarDT.Width = 120;
            // 
            // btnSelAll
            // 
            this.btnSelAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelAll.Location = new System.Drawing.Point(574, 228);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelAll.TabIndex = 5;
            this.btnSelAll.Text = "全　选";
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnSelInvert
            // 
            this.btnSelInvert.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelInvert.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelInvert.Location = new System.Drawing.Point(574, 262);
            this.btnSelInvert.Name = "btnSelInvert";
            this.btnSelInvert.Size = new System.Drawing.Size(75, 23);
            this.btnSelInvert.TabIndex = 6;
            this.btnSelInvert.Text = "反　选";
            this.btnSelInvert.Click += new System.EventHandler(this.btnSelInvert_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(574, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "转　换";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(574, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取　消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // contMenuOpen
            // 
            this.contMenuOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemNew});
            this.contMenuOpen.Name = "contMenuOpen";
            this.contMenuOpen.Size = new System.Drawing.Size(113, 48);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(112, 22);
            this.menuItemOpen.Text = "打开(&O)";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.Size = new System.Drawing.Size(112, 22);
            this.menuItemNew.Text = "新建(&N)";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // PJTranslator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 295);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelInvert);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSelAll);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PJTranslator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "投影转换";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PJTranslator_FormClosing);
            this.Load += new System.EventHandler(this.PJTranslator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contMenuOpen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSourDS;
        private System.Windows.Forms.ComboBox cmbTarDS;
        private System.Windows.Forms.ComboBox cmbTransType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSourSet;
        private System.Windows.Forms.Button btnSourView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTarView;
        private System.Windows.Forms.Button btnTarSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTransSet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevComponents.DotNetBar.ButtonX btnSelAll;
        private DevComponents.DotNetBar.ButtonX btnSelInvert;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarDT;
        private System.Windows.Forms.Button btnSourAdd;
        private System.Windows.Forms.Button btnTarAdd;
        private System.Windows.Forms.ContextMenuStrip contMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
    }
}