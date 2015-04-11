namespace ZTSupermap.UI
{
    partial class PJTranslatorDT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTransSet = new System.Windows.Forms.Button();
            this.cmbTransType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnTarAdd = new System.Windows.Forms.Button();
            this.btnTarSet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTarDT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTarView = new System.Windows.Forms.Button();
            this.cmbTarDS = new System.Windows.Forms.ComboBox();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSourDT = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSourAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSourSet = new System.Windows.Forms.Button();
            this.btnSourView = new System.Windows.Forms.Button();
            this.cmbSourDS = new System.Windows.Forms.ComboBox();
            this.contMenuOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contMenuOpen.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "投影信息：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnTransSet);
            this.groupBox3.Controls.Add(this.cmbTransType);
            this.groupBox3.Location = new System.Drawing.Point(2, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 80);
            this.groupBox3.TabIndex = 11;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据源：";
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(72, 301);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "转　换";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            // btnTarSet
            // 
            this.btnTarSet.Location = new System.Drawing.Point(151, 71);
            this.btnTarSet.Name = "btnTarSet";
            this.btnTarSet.Size = new System.Drawing.Size(60, 23);
            this.btnTarSet.TabIndex = 1;
            this.btnTarSet.Text = "设　置";
            this.btnTarSet.UseVisualStyleBackColor = true;
            this.btnTarSet.Click += new System.EventHandler(this.btnTarSet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTarDT);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnTarAdd);
            this.groupBox2.Controls.Add(this.btnTarView);
            this.groupBox2.Controls.Add(this.btnTarSet);
            this.groupBox2.Controls.Add(this.cmbTarDS);
            this.groupBox2.Location = new System.Drawing.Point(2, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 104);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果数据";
            // 
            // txtTarDT
            // 
            this.txtTarDT.Location = new System.Drawing.Point(80, 44);
            this.txtTarDT.Name = "txtTarDT";
            this.txtTarDT.Size = new System.Drawing.Size(117, 21);
            this.txtTarDT.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "数据集：";
            // 
            // btnTarView
            // 
            this.btnTarView.Location = new System.Drawing.Point(80, 71);
            this.btnTarView.Name = "btnTarView";
            this.btnTarView.Size = new System.Drawing.Size(65, 23);
            this.btnTarView.TabIndex = 2;
            this.btnTarView.Text = "查　看";
            this.btnTarView.UseVisualStyleBackColor = true;
            this.btnTarView.Click += new System.EventHandler(this.btnTarView_Click);
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
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(153, 301);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取　消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSourDT);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnSourAdd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSourSet);
            this.groupBox1.Controls.Add(this.btnSourView);
            this.groupBox1.Controls.Add(this.cmbSourDS);
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 99);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源数据";
            // 
            // cmbSourDT
            // 
            this.cmbSourDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourDT.FormattingEnabled = true;
            this.cmbSourDT.Location = new System.Drawing.Point(80, 42);
            this.cmbSourDT.Name = "cmbSourDT";
            this.cmbSourDT.Size = new System.Drawing.Size(117, 20);
            this.cmbSourDT.TabIndex = 7;
            this.cmbSourDT.SelectedIndexChanged += new System.EventHandler(this.cmbSourDT_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "数据集：";
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
            this.label2.Location = new System.Drawing.Point(10, 73);
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
            this.btnSourSet.Location = new System.Drawing.Point(151, 68);
            this.btnSourSet.Name = "btnSourSet";
            this.btnSourSet.Size = new System.Drawing.Size(61, 23);
            this.btnSourSet.TabIndex = 2;
            this.btnSourSet.Text = "设　置";
            this.btnSourSet.UseVisualStyleBackColor = true;
            this.btnSourSet.Click += new System.EventHandler(this.btnSourSet_Click);
            // 
            // btnSourView
            // 
            this.btnSourView.Location = new System.Drawing.Point(80, 68);
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
            // contMenuOpen
            // 
            this.contMenuOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemNew});
            this.contMenuOpen.Name = "contMenuOpen";
            this.contMenuOpen.Size = new System.Drawing.Size(153, 70);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(152, 22);
            this.menuItemOpen.Text = "打开(&O)";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.Size = new System.Drawing.Size(152, 22);
            this.menuItemNew.Text = "新建(&N)";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // PJTranslatorDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 330);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Name = "PJTranslatorDT";
            this.Text = "数据集投影转换";
            this.Load += new System.EventHandler(this.PJTranslatorDT_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PJTranslatorDT_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contMenuOpen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTransSet;
        private System.Windows.Forms.ComboBox cmbTransType;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private System.Windows.Forms.Button btnTarAdd;
        private System.Windows.Forms.Button btnTarSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTarView;
        private System.Windows.Forms.ComboBox cmbTarDS;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private System.Windows.Forms.TextBox txtTarDT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSourDT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSourAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSourSet;
        private System.Windows.Forms.Button btnSourView;
        private System.Windows.Forms.ComboBox cmbSourDS;
        private System.Windows.Forms.ContextMenuStrip contMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
    }
}