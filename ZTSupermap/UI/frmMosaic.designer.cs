namespace ZTSupermap.UI
{
    partial class frmMosaic
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
            this.button_AddDatasets = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colIndex = new System.Windows.Forms.ColumnHeader();
            this.colds = new System.Windows.Forms.ColumnHeader();
            this.coldt = new System.Windows.Forms.ColumnHeader();
            this.groupBox_Result = new System.Windows.Forms.GroupBox();
            this.checkPyrimid = new System.Windows.Forms.CheckBox();
            this.label_DesDataset = new System.Windows.Forms.Label();
            this.label_DesDatasource = new System.Windows.Forms.Label();
            this.comboBox_DesDataset = new System.Windows.Forms.ComboBox();
            this.comboBox_DesDatasource = new System.Windows.Forms.ComboBox();
            this.groupBox_params = new System.Windows.Forms.GroupBox();
            this.txtNovalue = new System.Windows.Forms.TextBox();
            this.txtNovalueTol = new System.Windows.Forms.TextBox();
            this.combPixelFormat = new System.Windows.Forms.ComboBox();
            this.combMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.txtBoundyhigh = new System.Windows.Forms.TextBox();
            this.txtBoundylow = new System.Windows.Forms.TextBox();
            this.txtBoundxhigh = new System.Windows.Forms.TextBox();
            this.txtBoundxlow = new System.Windows.Forms.TextBox();
            this.combBound = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_DeleteDataset = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox_Result.SuspendLayout();
            this.groupBox_params.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_AddDatasets
            // 
            this.button_AddDatasets.Location = new System.Drawing.Point(377, 20);
            this.button_AddDatasets.Name = "button_AddDatasets";
            this.button_AddDatasets.Size = new System.Drawing.Size(75, 23);
            this.button_AddDatasets.TabIndex = 2;
            this.button_AddDatasets.Text = "添加";
            this.button_AddDatasets.UseVisualStyleBackColor = true;
            this.button_AddDatasets.Click += new System.EventHandler(this.button_AddDatasets_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colds,
            this.coldt});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(7, 20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(364, 174);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colIndex
            // 
            this.colIndex.Text = "序号";
            // 
            // colds
            // 
            this.colds.Text = "数据源名称";
            this.colds.Width = 140;
            // 
            // coldt
            // 
            this.coldt.Text = "数据集名称";
            this.coldt.Width = 140;
            // 
            // groupBox_Result
            // 
            this.groupBox_Result.Controls.Add(this.checkPyrimid);
            this.groupBox_Result.Controls.Add(this.label_DesDataset);
            this.groupBox_Result.Controls.Add(this.label_DesDatasource);
            this.groupBox_Result.Controls.Add(this.comboBox_DesDataset);
            this.groupBox_Result.Controls.Add(this.comboBox_DesDatasource);
            this.groupBox_Result.Location = new System.Drawing.Point(12, 213);
            this.groupBox_Result.Name = "groupBox_Result";
            this.groupBox_Result.Size = new System.Drawing.Size(230, 109);
            this.groupBox_Result.TabIndex = 4;
            this.groupBox_Result.TabStop = false;
            this.groupBox_Result.Text = "结果数据";
            // 
            // checkPyrimid
            // 
            this.checkPyrimid.AutoSize = true;
            this.checkPyrimid.Location = new System.Drawing.Point(18, 80);
            this.checkPyrimid.Name = "checkPyrimid";
            this.checkPyrimid.Size = new System.Drawing.Size(108, 16);
            this.checkPyrimid.TabIndex = 4;
            this.checkPyrimid.Text = "创建影像金字塔";
            this.checkPyrimid.UseVisualStyleBackColor = true;
            // 
            // label_DesDataset
            // 
            this.label_DesDataset.AutoSize = true;
            this.label_DesDataset.Location = new System.Drawing.Point(16, 53);
            this.label_DesDataset.Name = "label_DesDataset";
            this.label_DesDataset.Size = new System.Drawing.Size(47, 12);
            this.label_DesDataset.TabIndex = 3;
            this.label_DesDataset.Text = "数据集:";
            // 
            // label_DesDatasource
            // 
            this.label_DesDatasource.AutoSize = true;
            this.label_DesDatasource.Location = new System.Drawing.Point(16, 23);
            this.label_DesDatasource.Name = "label_DesDatasource";
            this.label_DesDatasource.Size = new System.Drawing.Size(47, 12);
            this.label_DesDatasource.TabIndex = 2;
            this.label_DesDatasource.Text = "数据源:";
            // 
            // comboBox_DesDataset
            // 
            this.comboBox_DesDataset.FormattingEnabled = true;
            this.comboBox_DesDataset.Location = new System.Drawing.Point(69, 50);
            this.comboBox_DesDataset.Name = "comboBox_DesDataset";
            this.comboBox_DesDataset.Size = new System.Drawing.Size(155, 20);
            this.comboBox_DesDataset.TabIndex = 1;
            // 
            // comboBox_DesDatasource
            // 
            this.comboBox_DesDatasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DesDatasource.FormattingEnabled = true;
            this.comboBox_DesDatasource.Location = new System.Drawing.Point(69, 20);
            this.comboBox_DesDatasource.Name = "comboBox_DesDatasource";
            this.comboBox_DesDatasource.Size = new System.Drawing.Size(155, 20);
            this.comboBox_DesDatasource.TabIndex = 0;
            // 
            // groupBox_params
            // 
            this.groupBox_params.Controls.Add(this.txtNovalue);
            this.groupBox_params.Controls.Add(this.txtNovalueTol);
            this.groupBox_params.Controls.Add(this.combPixelFormat);
            this.groupBox_params.Controls.Add(this.combMethod);
            this.groupBox_params.Controls.Add(this.label4);
            this.groupBox_params.Controls.Add(this.label3);
            this.groupBox_params.Controls.Add(this.label2);
            this.groupBox_params.Controls.Add(this.label1);
            this.groupBox_params.Location = new System.Drawing.Point(12, 328);
            this.groupBox_params.Name = "groupBox_params";
            this.groupBox_params.Size = new System.Drawing.Size(230, 141);
            this.groupBox_params.TabIndex = 5;
            this.groupBox_params.TabStop = false;
            this.groupBox_params.Text = "参数设置";
            // 
            // txtNovalue
            // 
            this.txtNovalue.Location = new System.Drawing.Point(102, 79);
            this.txtNovalue.Name = "txtNovalue";
            this.txtNovalue.ReadOnly = true;
            this.txtNovalue.Size = new System.Drawing.Size(122, 21);
            this.txtNovalue.TabIndex = 17;
            // 
            // txtNovalueTol
            // 
            this.txtNovalueTol.Location = new System.Drawing.Point(102, 111);
            this.txtNovalueTol.Name = "txtNovalueTol";
            this.txtNovalueTol.ReadOnly = true;
            this.txtNovalueTol.Size = new System.Drawing.Size(122, 21);
            this.txtNovalueTol.TabIndex = 16;
            // 
            // combPixelFormat
            // 
            this.combPixelFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combPixelFormat.FormattingEnabled = true;
            this.combPixelFormat.Location = new System.Drawing.Point(102, 48);
            this.combPixelFormat.Name = "combPixelFormat";
            this.combPixelFormat.Size = new System.Drawing.Size(122, 20);
            this.combPixelFormat.TabIndex = 14;
            // 
            // combMethod
            // 
            this.combMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combMethod.FormattingEnabled = true;
            this.combMethod.Location = new System.Drawing.Point(102, 18);
            this.combMethod.Name = "combMethod";
            this.combMethod.Size = new System.Drawing.Size(122, 20);
            this.combMethod.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "无值数据容限：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "无值数据：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "像素类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "重叠区域设置：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtResolution);
            this.groupBox1.Controls.Add(this.txtBoundyhigh);
            this.groupBox1.Controls.Add(this.txtBoundylow);
            this.groupBox1.Controls.Add(this.txtBoundxhigh);
            this.groupBox1.Controls.Add(this.txtBoundxlow);
            this.groupBox1.Controls.Add(this.combBound);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(248, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 183);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "环境设置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "分辨率：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "最大Y值：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "最小Y值：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "最大X值：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "最小X值：";
            // 
            // txtResolution
            // 
            this.txtResolution.Location = new System.Drawing.Point(95, 152);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(121, 21);
            this.txtResolution.TabIndex = 6;
            // 
            // txtBoundyhigh
            // 
            this.txtBoundyhigh.Location = new System.Drawing.Point(95, 125);
            this.txtBoundyhigh.Name = "txtBoundyhigh";
            this.txtBoundyhigh.Size = new System.Drawing.Size(121, 21);
            this.txtBoundyhigh.TabIndex = 5;
            // 
            // txtBoundylow
            // 
            this.txtBoundylow.Location = new System.Drawing.Point(95, 98);
            this.txtBoundylow.Name = "txtBoundylow";
            this.txtBoundylow.Size = new System.Drawing.Size(121, 21);
            this.txtBoundylow.TabIndex = 4;
            // 
            // txtBoundxhigh
            // 
            this.txtBoundxhigh.Location = new System.Drawing.Point(95, 71);
            this.txtBoundxhigh.Name = "txtBoundxhigh";
            this.txtBoundxhigh.Size = new System.Drawing.Size(121, 21);
            this.txtBoundxhigh.TabIndex = 3;
            // 
            // txtBoundxlow
            // 
            this.txtBoundxlow.Location = new System.Drawing.Point(95, 44);
            this.txtBoundxlow.Name = "txtBoundxlow";
            this.txtBoundxlow.Size = new System.Drawing.Size(121, 21);
            this.txtBoundxlow.TabIndex = 2;
            // 
            // combBound
            // 
            this.combBound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBound.FormattingEnabled = true;
            this.combBound.Location = new System.Drawing.Point(95, 18);
            this.combBound.Name = "combBound";
            this.combBound.Size = new System.Drawing.Size(121, 20);
            this.combBound.TabIndex = 1;
            this.combBound.SelectedIndexChanged += new System.EventHandler(this.combBound_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "范围：";
            // 
            // button_DeleteDataset
            // 
            this.button_DeleteDataset.Location = new System.Drawing.Point(377, 57);
            this.button_DeleteDataset.Name = "button_DeleteDataset";
            this.button_DeleteDataset.Size = new System.Drawing.Size(75, 23);
            this.button_DeleteDataset.TabIndex = 7;
            this.button_DeleteDataset.Text = "删除";
            this.button_DeleteDataset.UseVisualStyleBackColor = true;
            this.button_DeleteDataset.Click += new System.EventHandler(this.button_DeleteDataset_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(274, 425);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(89, 29);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "确　定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(379, 425);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 29);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "取　消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.button_AddDatasets);
            this.groupBox2.Controls.Add(this.button_DeleteDataset);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 204);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "待镶嵌数据集";
            // 
            // frmMosaic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 473);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_params);
            this.Controls.Add(this.groupBox_Result);
            this.Name = "frmMosaic";
            this.Text = "栅格数据集镶嵌";
            this.Load += new System.EventHandler(this.frmMosaic_Load);
            this.groupBox_Result.ResumeLayout(false);
            this.groupBox_Result.PerformLayout();
            this.groupBox_params.ResumeLayout(false);
            this.groupBox_params.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_AddDatasets;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox_Result;
        private System.Windows.Forms.Label label_DesDataset;
        private System.Windows.Forms.Label label_DesDatasource;
        private System.Windows.Forms.ComboBox comboBox_DesDataset;
        private System.Windows.Forms.ComboBox comboBox_DesDatasource;
        private System.Windows.Forms.GroupBox groupBox_params;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_DeleteDataset;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.ColumnHeader colds;
        private System.Windows.Forms.ColumnHeader coldt;
        private System.Windows.Forms.TextBox txtNovalue;
        private System.Windows.Forms.TextBox txtNovalueTol;
        private System.Windows.Forms.ComboBox combPixelFormat;
        private System.Windows.Forms.ComboBox combMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combBound;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.TextBox txtBoundyhigh;
        private System.Windows.Forms.TextBox txtBoundylow;
        private System.Windows.Forms.TextBox txtBoundxhigh;
        private System.Windows.Forms.TextBox txtBoundxlow;
        private System.Windows.Forms.CheckBox checkPyrimid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
    }
}

