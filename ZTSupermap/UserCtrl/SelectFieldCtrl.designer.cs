namespace ZTSupermap.UserCtrl
{
    partial class SelectFieldCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkLstField = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.btnSelRev = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkLstField
            // 
            this.chkLstField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkLstField.CheckOnClick = true;
            this.chkLstField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkLstField.FormattingEnabled = true;
            this.chkLstField.Location = new System.Drawing.Point(0, 0);
            this.chkLstField.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.chkLstField.Name = "chkLstField";
            this.chkLstField.Size = new System.Drawing.Size(130, 160);
            this.chkLstField.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelAll);
            this.groupBox1.Controls.Add(this.btnSelRev);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 168);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(130, 40);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnSelAll
            // 
            this.btnSelAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelAll.Location = new System.Drawing.Point(27, 14);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(50, 23);
            this.btnSelAll.TabIndex = 0;
            this.btnSelAll.Text = "全选";
            this.btnSelAll.UseVisualStyleBackColor = true;
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnSelRev
            // 
            this.btnSelRev.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelRev.Location = new System.Drawing.Point(77, 14);
            this.btnSelRev.Name = "btnSelRev";
            this.btnSelRev.Size = new System.Drawing.Size(50, 23);
            this.btnSelRev.TabIndex = 1;
            this.btnSelRev.Text = "反选";
            this.btnSelRev.UseVisualStyleBackColor = true;
            this.btnSelRev.Click += new System.EventHandler(this.btnSelRev_Click);
            // 
            // SelectFieldCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkLstField);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectFieldCtrl";
            this.Size = new System.Drawing.Size(130, 208);
            this.Load += new System.EventHandler(this.SelectFieldCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkLstField;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelAll;
        private System.Windows.Forms.Button btnSelRev;
    }
}
