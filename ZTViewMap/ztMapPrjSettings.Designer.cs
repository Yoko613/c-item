namespace ZTViewMap
{
    partial class ztMapPrjSettings
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
            this.txtPrjInfo = new System.Windows.Forms.TextBox();
            this.btnPrjReset = new DevComponents.DotNetBar.ButtonX();
            this.btnReply = new DevComponents.DotNetBar.ButtonX();
            this.chkDynamicProjection = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtPrjInfo
            // 
            this.txtPrjInfo.Location = new System.Drawing.Point(12, 12);
            this.txtPrjInfo.Multiline = true;
            this.txtPrjInfo.Name = "txtPrjInfo";
            this.txtPrjInfo.ReadOnly = true;
            this.txtPrjInfo.Size = new System.Drawing.Size(364, 226);
            this.txtPrjInfo.TabIndex = 0;
            // 
            // btnPrjReset
            // 
            this.btnPrjReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrjReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrjReset.Location = new System.Drawing.Point(187, 244);
            this.btnPrjReset.Name = "btnPrjReset";
            this.btnPrjReset.Size = new System.Drawing.Size(97, 23);
            this.btnPrjReset.TabIndex = 1;
            this.btnPrjReset.Text = "重新设定...";
            this.btnPrjReset.Click += new System.EventHandler(this.btnPrjReset_Click);
            // 
            // btnReply
            // 
            this.btnReply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReply.Location = new System.Drawing.Point(301, 244);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(75, 23);
            this.btnReply.TabIndex = 2;
            this.btnReply.Text = "应 用";
            this.btnReply.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkDynamicProjection
            // 
            this.chkDynamicProjection.AutoSize = true;
            this.chkDynamicProjection.Location = new System.Drawing.Point(13, 244);
            this.chkDynamicProjection.Name = "chkDynamicProjection";
            this.chkDynamicProjection.Size = new System.Drawing.Size(72, 16);
            this.chkDynamicProjection.TabIndex = 3;
            this.chkDynamicProjection.Text = "动态投影";
            this.chkDynamicProjection.UseVisualStyleBackColor = true;
            // 
            // ztMapPrjSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 276);
            this.Controls.Add(this.chkDynamicProjection);
            this.Controls.Add(this.btnReply);
            this.Controls.Add(this.btnPrjReset);
            this.Controls.Add(this.txtPrjInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ztMapPrjSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "投影设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrjInfo;
        private DevComponents.DotNetBar.ButtonX btnPrjReset;
        private DevComponents.DotNetBar.ButtonX btnReply;
        private System.Windows.Forms.CheckBox chkDynamicProjection;
    }
}