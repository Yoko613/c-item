namespace ZTViewMap
{
    partial class ztTipSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ztTipSettings));
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.layerList = new System.Windows.Forms.ComboBox();
            this.lbLayer = new System.Windows.Forms.Label();
            this.gbLabel = new System.Windows.Forms.GroupBox();
            this.fieldList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSet = new DevComponents.DotNetBar.ButtonX();
            this.gbLabel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "3";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "4";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "5";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "6";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "7";
            // 
            // layerList
            // 
            this.layerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerList.FormattingEnabled = true;
            this.layerList.Location = new System.Drawing.Point(122, 6);
            this.layerList.Name = "layerList";
            this.layerList.Size = new System.Drawing.Size(193, 20);
            this.layerList.TabIndex = 3;
            this.layerList.SelectedIndexChanged += new System.EventHandler(this.layerList_SelectedIndexChanged);
            // 
            // lbLayer
            // 
            this.lbLayer.AutoSize = true;
            this.lbLayer.Location = new System.Drawing.Point(4, 9);
            this.lbLayer.Name = "lbLayer";
            this.lbLayer.Size = new System.Drawing.Size(113, 12);
            this.lbLayer.TabIndex = 1;
            this.lbLayer.Text = "请选择需提示的图层";
            // 
            // gbLabel
            // 
            this.gbLabel.Controls.Add(this.fieldList);
            this.gbLabel.Location = new System.Drawing.Point(3, 32);
            this.gbLabel.Name = "gbLabel";
            this.gbLabel.Size = new System.Drawing.Size(312, 169);
            this.gbLabel.TabIndex = 1;
            this.gbLabel.TabStop = false;
            this.gbLabel.Text = "标注字段";
            // 
            // fieldList
            // 
            this.fieldList.CheckOnClick = true;
            this.fieldList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldList.FormattingEnabled = true;
            this.fieldList.Location = new System.Drawing.Point(3, 17);
            this.fieldList.Name = "fieldList";
            this.fieldList.Size = new System.Drawing.Size(306, 148);
            this.fieldList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSet);
            this.groupBox1.Location = new System.Drawing.Point(6, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(222, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSet
            // 
            this.btnSet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSet.Location = new System.Drawing.Point(142, 14);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(78, 24);
            this.btnSet.TabIndex = 4;
            this.btnSet.Text = "确  定";
            this.btnSet.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // ztTipSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 252);
            this.Controls.Add(this.layerList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbLabel);
            this.Controls.Add(this.lbLayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 404);
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "ztTipSettings";
            this.Text = "提示信息设置";
            this.Load += new System.EventHandler(this.TipSettings_Load);
            this.gbLabel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private System.Windows.Forms.Label lbLayer;
        private System.Windows.Forms.ComboBox layerList;
        private System.Windows.Forms.GroupBox gbLabel;
        private System.Windows.Forms.CheckedListBox fieldList;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSet;
    }
}