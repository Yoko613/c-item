namespace ZTSupermap.UI
{
    partial class RegionUnion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewDt = new System.Windows.Forms.TextBox();
            this.btnUnion = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSelectFilter = new System.Windows.Forms.TextBox();
            this.btnSelectCdt = new System.Windows.Forms.Button();
            this.btnSetCalculateCdt = new System.Windows.Forms.Button();
            this.nudBegin = new System.Windows.Forms.NumericUpDown();
            this.lblDao = new System.Windows.Forms.Label();
            this.lblWei = new System.Windows.Forms.Label();
            this.btnSetCombineCdt = new System.Windows.Forms.Button();
            this.dgvFields = new System.Windows.Forms.DataGridView();
            this.grpCalculate = new System.Windows.Forms.GroupBox();
            this.btnDelCalculateCdt = new System.Windows.Forms.Button();
            this.cmbCalculateCdt = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpCombine = new System.Windows.Forms.GroupBox();
            this.btnDelCombineCdt = new System.Windows.Forms.Button();
            this.cmbCombineCdt = new System.Windows.Forms.ComboBox();
            this.nudEnd = new System.Windows.Forms.NumericUpDown();
            this.cmbAlias = new System.Windows.Forms.ComboBox();
            this.cmbDt = new System.Windows.Forms.ComboBox();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAliasResult = new System.Windows.Forms.ComboBox();
            this.btnAddResult = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbCombineType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.grpCalculate.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpCombine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "待融合数据源";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据集";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "结果数据源";
            // 
            // txtNewDt
            // 
            this.txtNewDt.Location = new System.Drawing.Point(299, 38);
            this.txtNewDt.Name = "txtNewDt";
            this.txtNewDt.Size = new System.Drawing.Size(134, 21);
            this.txtNewDt.TabIndex = 8;
            // 
            // btnUnion
            // 
            this.btnUnion.Location = new System.Drawing.Point(466, 384);
            this.btnUnion.Name = "btnUnion";
            this.btnUnion.Size = new System.Drawing.Size(75, 23);
            this.btnUnion.TabIndex = 15;
            this.btnUnion.Text = "融合(&U)";
            this.btnUnion.UseVisualStyleBackColor = true;
            this.btnUnion.Click += new System.EventHandler(this.btnUnion_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(558, 384);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "关闭(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(222, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(14, 19);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "筛选条件";
            // 
            // txtSelectFilter
            // 
            this.txtSelectFilter.Location = new System.Drawing.Point(80, 66);
            this.txtSelectFilter.Name = "txtSelectFilter";
            this.txtSelectFilter.Size = new System.Drawing.Size(521, 21);
            this.txtSelectFilter.TabIndex = 3;
            // 
            // btnSelectCdt
            // 
            this.btnSelectCdt.Location = new System.Drawing.Point(607, 66);
            this.btnSelectCdt.Name = "btnSelectCdt";
            this.btnSelectCdt.Size = new System.Drawing.Size(14, 19);
            this.btnSelectCdt.TabIndex = 4;
            this.btnSelectCdt.Text = "+";
            this.btnSelectCdt.UseVisualStyleBackColor = true;
            this.btnSelectCdt.Click += new System.EventHandler(this.btnSelectCdt_Click);
            // 
            // btnSetCalculateCdt
            // 
            this.btnSetCalculateCdt.Location = new System.Drawing.Point(146, 25);
            this.btnSetCalculateCdt.Name = "btnSetCalculateCdt";
            this.btnSetCalculateCdt.Size = new System.Drawing.Size(42, 23);
            this.btnSetCalculateCdt.TabIndex = 1;
            this.btnSetCalculateCdt.Text = "设置";
            this.btnSetCalculateCdt.UseVisualStyleBackColor = true;
            this.btnSetCalculateCdt.Click += new System.EventHandler(this.btnSetCalculateCdt_Click);
            // 
            // nudBegin
            // 
            this.nudBegin.Location = new System.Drawing.Point(132, 26);
            this.nudBegin.Name = "nudBegin";
            this.nudBegin.Size = new System.Drawing.Size(40, 21);
            this.nudBegin.TabIndex = 1;
            // 
            // lblDao
            // 
            this.lblDao.AutoSize = true;
            this.lblDao.Location = new System.Drawing.Point(172, 30);
            this.lblDao.Name = "lblDao";
            this.lblDao.Size = new System.Drawing.Size(17, 12);
            this.lblDao.TabIndex = 19;
            this.lblDao.Tag = "";
            this.lblDao.Text = "到";
            // 
            // lblWei
            // 
            this.lblWei.AutoSize = true;
            this.lblWei.Location = new System.Drawing.Point(228, 30);
            this.lblWei.Name = "lblWei";
            this.lblWei.Size = new System.Drawing.Size(17, 12);
            this.lblWei.TabIndex = 18;
            this.lblWei.Tag = "";
            this.lblWei.Text = "位";
            // 
            // btnSetCombineCdt
            // 
            this.btnSetCombineCdt.Location = new System.Drawing.Point(251, 25);
            this.btnSetCombineCdt.Name = "btnSetCombineCdt";
            this.btnSetCombineCdt.Size = new System.Drawing.Size(42, 23);
            this.btnSetCombineCdt.TabIndex = 3;
            this.btnSetCombineCdt.Text = "设置";
            this.btnSetCombineCdt.UseVisualStyleBackColor = true;
            this.btnSetCombineCdt.Click += new System.EventHandler(this.btnSetCombineCdt_Click);
            // 
            // dgvFields
            // 
            this.dgvFields.AllowUserToAddRows = false;
            this.dgvFields.AllowUserToDeleteRows = false;
            this.dgvFields.AllowUserToResizeRows = false;
            this.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFields.Location = new System.Drawing.Point(6, 18);
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.ReadOnly = true;
            this.dgvFields.RowTemplate.Height = 23;
            this.dgvFields.Size = new System.Drawing.Size(609, 183);
            this.dgvFields.TabIndex = 0;
            this.dgvFields.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFields_RowEnter);
            // 
            // grpCalculate
            // 
            this.grpCalculate.Controls.Add(this.btnDelCalculateCdt);
            this.grpCalculate.Controls.Add(this.cmbCalculateCdt);
            this.grpCalculate.Controls.Add(this.btnSetCalculateCdt);
            this.grpCalculate.Location = new System.Drawing.Point(366, 207);
            this.grpCalculate.Name = "grpCalculate";
            this.grpCalculate.Size = new System.Drawing.Size(249, 62);
            this.grpCalculate.TabIndex = 15;
            this.grpCalculate.TabStop = false;
            this.grpCalculate.Text = "运算规则";
            // 
            // btnDelCalculateCdt
            // 
            this.btnDelCalculateCdt.Location = new System.Drawing.Point(194, 25);
            this.btnDelCalculateCdt.Name = "btnDelCalculateCdt";
            this.btnDelCalculateCdt.Size = new System.Drawing.Size(42, 23);
            this.btnDelCalculateCdt.TabIndex = 2;
            this.btnDelCalculateCdt.Text = "清除";
            this.btnDelCalculateCdt.UseVisualStyleBackColor = true;
            this.btnDelCalculateCdt.Click += new System.EventHandler(this.btnDelCalculateCdt_Click);
            // 
            // cmbCalculateCdt
            // 
            this.cmbCalculateCdt.FormattingEnabled = true;
            this.cmbCalculateCdt.Location = new System.Drawing.Point(6, 27);
            this.cmbCalculateCdt.Name = "cmbCalculateCdt";
            this.cmbCalculateCdt.Size = new System.Drawing.Size(122, 20);
            this.cmbCalculateCdt.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grpCalculate);
            this.groupBox2.Controls.Add(this.grpCombine);
            this.groupBox2.Controls.Add(this.dgvFields);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(621, 276);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "融合规则、字段运算规则";
            // 
            // grpCombine
            // 
            this.grpCombine.Controls.Add(this.btnDelCombineCdt);
            this.grpCombine.Controls.Add(this.cmbCombineCdt);
            this.grpCombine.Controls.Add(this.btnSetCombineCdt);
            this.grpCombine.Controls.Add(this.nudEnd);
            this.grpCombine.Controls.Add(this.nudBegin);
            this.grpCombine.Controls.Add(this.lblDao);
            this.grpCombine.Controls.Add(this.lblWei);
            this.grpCombine.Location = new System.Drawing.Point(9, 207);
            this.grpCombine.Name = "grpCombine";
            this.grpCombine.Size = new System.Drawing.Size(351, 62);
            this.grpCombine.TabIndex = 14;
            this.grpCombine.TabStop = false;
            this.grpCombine.Text = "组合规则";
            // 
            // btnDelCombineCdt
            // 
            this.btnDelCombineCdt.Location = new System.Drawing.Point(299, 25);
            this.btnDelCombineCdt.Name = "btnDelCombineCdt";
            this.btnDelCombineCdt.Size = new System.Drawing.Size(42, 23);
            this.btnDelCombineCdt.TabIndex = 20;
            this.btnDelCombineCdt.Text = "清除";
            this.btnDelCombineCdt.UseVisualStyleBackColor = true;
            this.btnDelCombineCdt.Click += new System.EventHandler(this.btnDelCombineCdt_Click);
            // 
            // cmbCombineCdt
            // 
            this.cmbCombineCdt.FormattingEnabled = true;
            this.cmbCombineCdt.Location = new System.Drawing.Point(6, 26);
            this.cmbCombineCdt.Name = "cmbCombineCdt";
            this.cmbCombineCdt.Size = new System.Drawing.Size(121, 20);
            this.cmbCombineCdt.TabIndex = 0;
            this.cmbCombineCdt.SelectedIndexChanged += new System.EventHandler(this.cmbCombineCdt_SelectedIndexChanged);
            // 
            // nudEnd
            // 
            this.nudEnd.Location = new System.Drawing.Point(188, 26);
            this.nudEnd.Name = "nudEnd";
            this.nudEnd.Size = new System.Drawing.Size(40, 21);
            this.nudEnd.TabIndex = 2;
            // 
            // cmbAlias
            // 
            this.cmbAlias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlias.FormattingEnabled = true;
            this.cmbAlias.Location = new System.Drawing.Point(80, 13);
            this.cmbAlias.Name = "cmbAlias";
            this.cmbAlias.Size = new System.Drawing.Size(141, 20);
            this.cmbAlias.TabIndex = 0;
            this.cmbAlias.SelectedIndexChanged += new System.EventHandler(this.cmbAlias_SelectedIndexChanged);
            // 
            // cmbDt
            // 
            this.cmbDt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDt.FormattingEnabled = true;
            this.cmbDt.Location = new System.Drawing.Point(299, 13);
            this.cmbDt.Name = "cmbDt";
            this.cmbDt.Size = new System.Drawing.Size(134, 20);
            this.cmbDt.TabIndex = 2;
            this.cmbDt.SelectedIndexChanged += new System.EventHandler(this.cmbDt_SelectedIndexChanged);
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(494, 40);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(127, 21);
            this.txtTolerance.TabIndex = 14;
            this.txtTolerance.Text = "0";
            this.txtTolerance.TextChanged += new System.EventHandler(this.txtTolerance_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(464, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "容限";
            // 
            // cmbAliasResult
            // 
            this.cmbAliasResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAliasResult.FormattingEnabled = true;
            this.cmbAliasResult.Location = new System.Drawing.Point(80, 39);
            this.cmbAliasResult.Name = "cmbAliasResult";
            this.cmbAliasResult.Size = new System.Drawing.Size(141, 20);
            this.cmbAliasResult.TabIndex = 5;
            // 
            // btnAddResult
            // 
            this.btnAddResult.Location = new System.Drawing.Point(222, 40);
            this.btnAddResult.Name = "btnAddResult";
            this.btnAddResult.Size = new System.Drawing.Size(14, 19);
            this.btnAddResult.TabIndex = 6;
            this.btnAddResult.Text = "+";
            this.btnAddResult.UseVisualStyleBackColor = true;
            this.btnAddResult.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "数据集";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(602, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "米";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(440, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "融合类型";
            // 
            // cmbCombineType
            // 
            this.cmbCombineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCombineType.FormattingEnabled = true;
            this.cmbCombineType.Location = new System.Drawing.Point(494, 14);
            this.cmbCombineType.Name = "cmbCombineType";
            this.cmbCombineType.Size = new System.Drawing.Size(127, 20);
            this.cmbCombineType.TabIndex = 17;
            // 
            // RegionUnion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 419);
            this.Controls.Add(this.cmbCombineType);
            this.Controls.Add(this.cmbDt);
            this.Controls.Add(this.cmbAliasResult);
            this.Controls.Add(this.cmbAlias);
            this.Controls.Add(this.txtSelectFilter);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSelectCdt);
            this.Controls.Add(this.btnAddResult);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnUnion);
            this.Controls.Add(this.txtNewDt);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtTolerance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "RegionUnion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "面融合";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegionUnion_FormClosing);
            this.Load += new System.EventHandler(this.RegionUnion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
            this.grpCalculate.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.grpCombine.ResumeLayout(false);
            this.grpCombine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewDt;
        private System.Windows.Forms.Button btnUnion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSelectFilter;
        private System.Windows.Forms.Button btnSelectCdt;
        private System.Windows.Forms.Button btnSetCalculateCdt;
        private System.Windows.Forms.NumericUpDown nudBegin;
        private System.Windows.Forms.Label lblDao;
        private System.Windows.Forms.Label lblWei;
        private System.Windows.Forms.Button btnSetCombineCdt;
        private System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.GroupBox grpCalculate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpCombine;
        private System.Windows.Forms.NumericUpDown nudEnd;
        private System.Windows.Forms.ComboBox cmbCalculateCdt;
        private System.Windows.Forms.ComboBox cmbCombineCdt;
        private System.Windows.Forms.ComboBox cmbAlias;
        private System.Windows.Forms.ComboBox cmbDt;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbAliasResult;
        private System.Windows.Forms.Button btnAddResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbCombineType;
        private System.Windows.Forms.Button btnDelCalculateCdt;
        private System.Windows.Forms.Button btnDelCombineCdt;
    }
}