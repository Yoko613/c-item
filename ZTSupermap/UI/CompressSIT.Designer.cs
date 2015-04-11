namespace ZTSupermap.UI
{
    partial class CompressSIT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompressSIT));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colSourFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTarFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuelity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbBlue = new System.Windows.Forms.Label();
            this.lbGreen = new System.Windows.Forms.Label();
            this.lbRed = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarBlue = new System.Windows.Forms.TrackBar();
            this.trackBarGreen = new System.Windows.Forms.TrackBar();
            this.trackBarRed = new System.Windows.Forms.TrackBar();
            this.btnAddFile = new DevComponents.DotNetBar.ButtonX();
            this.btnclear = new DevComponents.DotNetBar.ButtonX();
            this.trackBarQuelity = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.lbQuelity = new System.Windows.Forms.Label();
            this.txtTarFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelTarFile = new System.Windows.Forms.Button();
            this.btnDelfile = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuelity)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSourFile,
            this.colTarFile,
            this.colBand,
            this.colQuelity});
            this.dataGridView1.Location = new System.Drawing.Point(3, 5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(509, 166);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // colSourFile
            // 
            this.colSourFile.FillWeight = 200F;
            this.colSourFile.HeaderText = "源文件";
            this.colSourFile.Name = "colSourFile";
            this.colSourFile.ReadOnly = true;
            this.colSourFile.Width = 200;
            // 
            // colTarFile
            // 
            this.colTarFile.FillWeight = 300F;
            this.colTarFile.HeaderText = "目标文件";
            this.colTarFile.Name = "colTarFile";
            this.colTarFile.ReadOnly = true;
            this.colTarFile.Width = 300;
            // 
            // colBand
            // 
            this.colBand.HeaderText = "波段";
            this.colBand.Name = "colBand";
            this.colBand.ReadOnly = true;
            this.colBand.Visible = false;
            // 
            // colQuelity
            // 
            this.colQuelity.HeaderText = "压缩质量";
            this.colQuelity.Name = "colQuelity";
            this.colQuelity.ReadOnly = true;
            this.colQuelity.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbBlue);
            this.groupBox1.Controls.Add(this.lbGreen);
            this.groupBox1.Controls.Add(this.lbRed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBarBlue);
            this.groupBox1.Controls.Add(this.trackBarGreen);
            this.groupBox1.Controls.Add(this.trackBarRed);
            this.groupBox1.Location = new System.Drawing.Point(3, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "波段";
            // 
            // lbBlue
            // 
            this.lbBlue.AutoSize = true;
            this.lbBlue.Location = new System.Drawing.Point(220, 90);
            this.lbBlue.Name = "lbBlue";
            this.lbBlue.Size = new System.Drawing.Size(11, 12);
            this.lbBlue.TabIndex = 10;
            this.lbBlue.Text = "1";
            // 
            // lbGreen
            // 
            this.lbGreen.AutoSize = true;
            this.lbGreen.Location = new System.Drawing.Point(220, 57);
            this.lbGreen.Name = "lbGreen";
            this.lbGreen.Size = new System.Drawing.Size(11, 12);
            this.lbGreen.TabIndex = 9;
            this.lbGreen.Text = "1";
            // 
            // lbRed
            // 
            this.lbRed.AutoSize = true;
            this.lbRed.Location = new System.Drawing.Point(220, 20);
            this.lbRed.Name = "lbRed";
            this.lbRed.Size = new System.Drawing.Size(11, 12);
            this.lbRed.TabIndex = 8;
            this.lbRed.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "蓝(B):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "绿(G):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "红(R):";
            // 
            // trackBarBlue
            // 
            this.trackBarBlue.Location = new System.Drawing.Point(59, 88);
            this.trackBarBlue.Maximum = 20;
            this.trackBarBlue.Minimum = 1;
            this.trackBarBlue.Name = "trackBarBlue";
            this.trackBarBlue.Size = new System.Drawing.Size(160, 45);
            this.trackBarBlue.TabIndex = 4;
            this.trackBarBlue.Value = 1;
            this.trackBarBlue.ValueChanged += new System.EventHandler(this.trackBarBlue_ValueChanged);
            // 
            // trackBarGreen
            // 
            this.trackBarGreen.Location = new System.Drawing.Point(59, 57);
            this.trackBarGreen.Maximum = 20;
            this.trackBarGreen.Minimum = 1;
            this.trackBarGreen.Name = "trackBarGreen";
            this.trackBarGreen.Size = new System.Drawing.Size(160, 45);
            this.trackBarGreen.TabIndex = 3;
            this.trackBarGreen.Value = 1;
            this.trackBarGreen.ValueChanged += new System.EventHandler(this.trackBarGreen_ValueChanged);
            // 
            // trackBarRed
            // 
            this.trackBarRed.Location = new System.Drawing.Point(59, 20);
            this.trackBarRed.Maximum = 20;
            this.trackBarRed.Minimum = 1;
            this.trackBarRed.Name = "trackBarRed";
            this.trackBarRed.Size = new System.Drawing.Size(160, 45);
            this.trackBarRed.TabIndex = 2;
            this.trackBarRed.Value = 1;
            this.trackBarRed.ValueChanged += new System.EventHandler(this.trackBarRed_ValueChanged);
            // 
            // btnAddFile
            // 
            this.btnAddFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFile.Image")));
            this.btnAddFile.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btnAddFile.Location = new System.Drawing.Point(259, 180);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(99, 23);
            this.btnAddFile.TabIndex = 2;
            this.btnAddFile.Text = "添加文件...";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnclear
            // 
            this.btnclear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnclear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnclear.Location = new System.Drawing.Point(450, 180);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(62, 23);
            this.btnclear.TabIndex = 3;
            this.btnclear.Text = "全部清空";
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // trackBarQuelity
            // 
            this.trackBarQuelity.Enabled = false;
            this.trackBarQuelity.Location = new System.Drawing.Point(257, 269);
            this.trackBarQuelity.Maximum = 100;
            this.trackBarQuelity.Name = "trackBarQuelity";
            this.trackBarQuelity.Size = new System.Drawing.Size(237, 45);
            this.trackBarQuelity.TabIndex = 4;
            this.trackBarQuelity.TickFrequency = 5;
            this.trackBarQuelity.Value = 75;
            this.trackBarQuelity.ValueChanged += new System.EventHandler(this.trackBarQuelity_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "压缩质量：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.btnCancle);
            this.groupBox2.Controls.Add(this.btnOK);
            this.groupBox2.Location = new System.Drawing.Point(3, 319);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 39);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 17);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(234, 16);
            this.progressBar1.TabIndex = 8;
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(416, 11);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "取　消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(318, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbQuelity
            // 
            this.lbQuelity.AutoSize = true;
            this.lbQuelity.Location = new System.Drawing.Point(469, 260);
            this.lbQuelity.Name = "lbQuelity";
            this.lbQuelity.Size = new System.Drawing.Size(17, 12);
            this.lbQuelity.TabIndex = 7;
            this.lbQuelity.Text = "75";
            // 
            // txtTarFile
            // 
            this.txtTarFile.Location = new System.Drawing.Point(259, 230);
            this.txtTarFile.Name = "txtTarFile";
            this.txtTarFile.ReadOnly = true;
            this.txtTarFile.Size = new System.Drawing.Size(235, 21);
            this.txtTarFile.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(257, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "目标文件：";
            // 
            // btnSelTarFile
            // 
            this.btnSelTarFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSelTarFile.Image")));
            this.btnSelTarFile.Location = new System.Drawing.Point(471, 231);
            this.btnSelTarFile.Name = "btnSelTarFile";
            this.btnSelTarFile.Size = new System.Drawing.Size(22, 19);
            this.btnSelTarFile.TabIndex = 10;
            this.btnSelTarFile.UseVisualStyleBackColor = true;
            this.btnSelTarFile.Click += new System.EventHandler(this.btnSelTarFile_Click);
            // 
            // btnDelfile
            // 
            this.btnDelfile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelfile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelfile.Location = new System.Drawing.Point(369, 180);
            this.btnDelfile.Name = "btnDelfile";
            this.btnDelfile.Size = new System.Drawing.Size(75, 23);
            this.btnDelfile.TabIndex = 11;
            this.btnDelfile.Text = "清除文件";
            this.btnDelfile.Click += new System.EventHandler(this.btnDelfile_Click);
            // 
            // CompressSIT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 370);
            this.Controls.Add(this.btnDelfile);
            this.Controls.Add(this.btnSelTarFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTarFile);
            this.Controls.Add(this.lbQuelity);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBarQuelity);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompressSIT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "压缩为SIT影像文件";
            this.Load += new System.EventHandler(this.CompressSIT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuelity)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarBlue;
        private System.Windows.Forms.TrackBar trackBarGreen;
        private System.Windows.Forms.TrackBar trackBarRed;
        private System.Windows.Forms.Label lbBlue;
        private System.Windows.Forms.Label lbGreen;
        private System.Windows.Forms.Label lbRed;
        private DevComponents.DotNetBar.ButtonX btnAddFile;
        private DevComponents.DotNetBar.ButtonX btnclear;
        private System.Windows.Forms.TrackBar trackBarQuelity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbQuelity;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtTarFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelTarFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuelity;
        private DevComponents.DotNetBar.ButtonX btnDelfile;
    }
}