namespace ZTViewMap
{
    partial class ztLayerSetting
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
            this.btnRasterBGColor = new System.Windows.Forms.Button();
            this.chkNOvalueTransparent = new System.Windows.Forms.CheckBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // btnRasterBGColor
            // 
            this.btnRasterBGColor.BackColor = System.Drawing.Color.White;
            this.btnRasterBGColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRasterBGColor.Location = new System.Drawing.Point(23, 33);
            this.btnRasterBGColor.Name = "btnRasterBGColor";
            this.btnRasterBGColor.Size = new System.Drawing.Size(133, 23);
            this.btnRasterBGColor.TabIndex = 1;
            this.btnRasterBGColor.Text = "栅格图层背景颜色";
            this.btnRasterBGColor.UseVisualStyleBackColor = false;
            this.btnRasterBGColor.Click += new System.EventHandler(this.btnRasterBGColor_Click);
            // 
            // chkNOvalueTransparent
            // 
            this.chkNOvalueTransparent.AutoSize = true;
            this.chkNOvalueTransparent.Location = new System.Drawing.Point(23, 11);
            this.chkNOvalueTransparent.Name = "chkNOvalueTransparent";
            this.chkNOvalueTransparent.Size = new System.Drawing.Size(120, 16);
            this.chkNOvalueTransparent.TabIndex = 2;
            this.chkNOvalueTransparent.Text = "栅格图层背景透明";
            this.chkNOvalueTransparent.UseVisualStyleBackColor = true;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(219, 12);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 3;
            this.buttonX1.Text = "确 定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // ztLayerSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 70);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.chkNOvalueTransparent);
            this.Controls.Add(this.btnRasterBGColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ztLayerSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层显示设置";
            this.Load += new System.EventHandler(this.ztLayerSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRasterBGColor;
        private System.Windows.Forms.CheckBox chkNOvalueTransparent;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}