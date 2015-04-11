namespace ZTSupermap.UI
{
    partial class AfferTranslation
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
            this.cboOpenDs = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRevSelect = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.listViewOpenDt = new System.Windows.Forms.ListView();
            this.btnTranslation = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Y = new System.Windows.Forms.TextBox();
            this.txt_X = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddDs = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源：";
            // 
            // cboOpenDs
            // 
            this.cboOpenDs.FormattingEnabled = true;
            this.cboOpenDs.Location = new System.Drawing.Point(75, 17);
            this.cboOpenDs.Name = "cboOpenDs";
            this.cboOpenDs.Size = new System.Drawing.Size(173, 20);
            this.cboOpenDs.TabIndex = 1;
            this.cboOpenDs.SelectedIndexChanged += new System.EventHandler(this.cboOpenDs_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRevSelect);
            this.groupBox1.Controls.Add(this.btnAll);
            this.groupBox1.Controls.Add(this.listViewOpenDt);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 231);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据集：";
            // 
            // btnRevSelect
            // 
            this.btnRevSelect.Location = new System.Drawing.Point(179, 201);
            this.btnRevSelect.Name = "btnRevSelect";
            this.btnRevSelect.Size = new System.Drawing.Size(75, 23);
            this.btnRevSelect.TabIndex = 2;
            this.btnRevSelect.Text = "反选";
            this.btnRevSelect.UseVisualStyleBackColor = true;
            this.btnRevSelect.Click += new System.EventHandler(this.btnRevSelect_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(98, 201);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "全选";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // listViewOpenDt
            // 
            this.listViewOpenDt.CheckBoxes = true;
            this.listViewOpenDt.FullRowSelect = true;
            this.listViewOpenDt.GridLines = true;
            this.listViewOpenDt.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.listViewOpenDt.Location = new System.Drawing.Point(6, 16);
            this.listViewOpenDt.Name = "listViewOpenDt";
            this.listViewOpenDt.Size = new System.Drawing.Size(248, 179);
            this.listViewOpenDt.TabIndex = 0;
            this.listViewOpenDt.UseCompatibleStateImageBehavior = false;
            this.listViewOpenDt.View = System.Windows.Forms.View.Details;
            // 
            // btnTranslation
            // 
            this.btnTranslation.Location = new System.Drawing.Point(110, 360);
            this.btnTranslation.Name = "btnTranslation";
            this.btnTranslation.Size = new System.Drawing.Size(75, 23);
            this.btnTranslation.TabIndex = 3;
            this.btnTranslation.Text = "平移";
            this.btnTranslation.UseVisualStyleBackColor = true;
            this.btnTranslation.Click += new System.EventHandler(this.btnTranslation_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(191, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_Y);
            this.groupBox2.Controls.Add(this.txt_X);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 72);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "偏移参数：";
            // 
            // txt_Y
            // 
            this.txt_Y.Location = new System.Drawing.Point(98, 45);
            this.txt_Y.Name = "txt_Y";
            this.txt_Y.Size = new System.Drawing.Size(145, 21);
            this.txt_Y.TabIndex = 3;
            // 
            // txt_X
            // 
            this.txt_X.Location = new System.Drawing.Point(98, 17);
            this.txt_X.Name = "txt_X";
            this.txt_X.Size = new System.Drawing.Size(145, 21);
            this.txt_X.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Y 偏移量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "X 偏移量：";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 7;
            // 
            // btnAddDs
            // 
            this.btnAddDs.Location = new System.Drawing.Point(254, 14);
            this.btnAddDs.Name = "btnAddDs";
            this.btnAddDs.Size = new System.Drawing.Size(18, 26);
            this.btnAddDs.TabIndex = 8;
            this.btnAddDs.Text = "+";
            this.btnAddDs.UseVisualStyleBackColor = true;
            this.btnAddDs.Click += new System.EventHandler(this.btnAddDs_Click);
            // 
            // AfferTranslation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 391);
            this.Controls.Add(this.btnAddDs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTranslation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboOpenDs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AfferTranslation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "平移";
            this.Load += new System.EventHandler(this.AfferTranslation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOpenDs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRevSelect;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.ListView listViewOpenDt;
        private System.Windows.Forms.Button btnTranslation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_X;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Y;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddDs;
    }
}