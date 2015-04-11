namespace ZTViewMap
{
    partial class ztSQLQuery
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.gbValue = new System.Windows.Forms.GroupBox();
            this.listBoxValue = new System.Windows.Forms.ListBox();
            this.gbExpress = new System.Windows.Forms.GroupBox();
            this.SQLText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbField = new System.Windows.Forms.GroupBox();
            this.listBoxField = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLess = new DevComponents.DotNetBar.ButtonX();
            this.btnLessThan = new DevComponents.DotNetBar.ButtonX();
            this.btnOr = new DevComponents.DotNetBar.ButtonX();
            this.btnAnd = new DevComponents.DotNetBar.ButtonX();
            this.btnLike = new DevComponents.DotNetBar.ButtonX();
            this.btnMore = new DevComponents.DotNetBar.ButtonX();
            this.btnUnequal = new DevComponents.DotNetBar.ButtonX();
            this.btnMoreThan = new DevComponents.DotNetBar.ButtonX();
            this.btnEqual = new DevComponents.DotNetBar.ButtonX();
            this.ribbonTabItemGroup4 = new DevComponents.DotNetBar.RibbonTabItemGroup();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.groupBox6.SuspendLayout();
            this.gbValue.SuspendLayout();
            this.gbExpress.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbField.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.gbValue);
            this.groupBox6.Controls.Add(this.gbExpress);
            this.groupBox6.Controls.Add(this.groupBox1);
            this.groupBox6.Controls.Add(this.gbField);
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(488, 300);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            // 
            // gbValue
            // 
            this.gbValue.Controls.Add(this.listBoxValue);
            this.gbValue.Location = new System.Drawing.Point(347, 59);
            this.gbValue.Name = "gbValue";
            this.gbValue.Size = new System.Drawing.Size(130, 145);
            this.gbValue.TabIndex = 3;
            this.gbValue.TabStop = false;
            this.gbValue.Text = "样值";
            // 
            // listBoxValue
            // 
            this.listBoxValue.FormattingEnabled = true;
            this.listBoxValue.ItemHeight = 12;
            this.listBoxValue.Location = new System.Drawing.Point(10, 14);
            this.listBoxValue.Name = "listBoxValue";
            this.listBoxValue.Size = new System.Drawing.Size(110, 124);
            this.listBoxValue.TabIndex = 0;
            this.listBoxValue.DoubleClick += new System.EventHandler(this.listBoxValue_DoubleClick);
            // 
            // gbExpress
            // 
            this.gbExpress.Controls.Add(this.SQLText);
            this.gbExpress.Location = new System.Drawing.Point(12, 210);
            this.gbExpress.Name = "gbExpress";
            this.gbExpress.Size = new System.Drawing.Size(465, 80);
            this.gbExpress.TabIndex = 4;
            this.gbExpress.TabStop = false;
            this.gbExpress.Text = "SQL表达式";
            // 
            // SQLText
            // 
            this.SQLText.Location = new System.Drawing.Point(6, 20);
            this.SQLText.Multiline = true;
            this.SQLText.Name = "SQLText";
            this.SQLText.Size = new System.Drawing.Size(453, 45);
            this.SQLText.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboLayer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询设置";
            // 
            // comboLayer
            // 
            this.comboLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLayer.FormattingEnabled = true;
            this.comboLayer.Location = new System.Drawing.Point(120, 25);
            this.comboLayer.Name = "comboLayer";
            this.comboLayer.Size = new System.Drawing.Size(282, 20);
            this.comboLayer.TabIndex = 1;
            this.comboLayer.SelectedIndexChanged += new System.EventHandler(this.comboLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择查询图层:";
            // 
            // gbField
            // 
            this.gbField.Controls.Add(this.listBoxField);
            this.gbField.Location = new System.Drawing.Point(12, 59);
            this.gbField.Name = "gbField";
            this.gbField.Size = new System.Drawing.Size(132, 145);
            this.gbField.TabIndex = 1;
            this.gbField.TabStop = false;
            this.gbField.Text = "字段";
            // 
            // listBoxField
            // 
            this.listBoxField.FormattingEnabled = true;
            this.listBoxField.ItemHeight = 12;
            this.listBoxField.Location = new System.Drawing.Point(6, 13);
            this.listBoxField.Name = "listBoxField";
            this.listBoxField.Size = new System.Drawing.Size(117, 124);
            this.listBoxField.TabIndex = 0;
            this.listBoxField.DoubleClick += new System.EventHandler(this.listBoxField_DoubleClick);
            this.listBoxField.SelectedIndexChanged += new System.EventHandler(this.listBoxField_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLess);
            this.groupBox3.Controls.Add(this.btnLessThan);
            this.groupBox3.Controls.Add(this.btnOr);
            this.groupBox3.Controls.Add(this.btnAnd);
            this.groupBox3.Controls.Add(this.btnLike);
            this.groupBox3.Controls.Add(this.btnMore);
            this.groupBox3.Controls.Add(this.btnUnequal);
            this.groupBox3.Controls.Add(this.btnMoreThan);
            this.groupBox3.Controls.Add(this.btnEqual);
            this.groupBox3.Location = new System.Drawing.Point(154, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(187, 145);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作符";
            // 
            // btnLess
            // 
            this.btnLess.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLess.Location = new System.Drawing.Point(134, 64);
            this.btnLess.Name = "btnLess";
            this.btnLess.Size = new System.Drawing.Size(47, 23);
            this.btnLess.TabIndex = 25;
            this.btnLess.Text = "小 于";
            this.btnLess.Click += new System.EventHandler(this.btnLess_Click);
            // 
            // btnLessThan
            // 
            this.btnLessThan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLessThan.Location = new System.Drawing.Point(67, 64);
            this.btnLessThan.Name = "btnLessThan";
            this.btnLessThan.Size = new System.Drawing.Size(60, 23);
            this.btnLessThan.TabIndex = 25;
            this.btnLessThan.Text = "小于等于";
            this.btnLessThan.Click += new System.EventHandler(this.btnLessThan_Click);
            // 
            // btnOr
            // 
            this.btnOr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOr.Location = new System.Drawing.Point(134, 106);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(47, 23);
            this.btnOr.TabIndex = 25;
            this.btnOr.Text = "或 者";
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAnd.Location = new System.Drawing.Point(67, 106);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(60, 23);
            this.btnAnd.TabIndex = 25;
            this.btnAnd.Text = "和";
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnLike
            // 
            this.btnLike.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLike.Location = new System.Drawing.Point(6, 106);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(52, 23);
            this.btnLike.TabIndex = 25;
            this.btnLike.Text = "含　有";
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnMore
            // 
            this.btnMore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMore.Location = new System.Drawing.Point(6, 64);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(52, 23);
            this.btnMore.TabIndex = 25;
            this.btnMore.Text = "大 于";
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // btnUnequal
            // 
            this.btnUnequal.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnequal.Location = new System.Drawing.Point(134, 23);
            this.btnUnequal.Name = "btnUnequal";
            this.btnUnequal.Size = new System.Drawing.Size(47, 23);
            this.btnUnequal.TabIndex = 25;
            this.btnUnequal.Text = "不等于";
            this.btnUnequal.Click += new System.EventHandler(this.btnUnequal_Click);
            // 
            // btnMoreThan
            // 
            this.btnMoreThan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMoreThan.Location = new System.Drawing.Point(67, 23);
            this.btnMoreThan.Name = "btnMoreThan";
            this.btnMoreThan.Size = new System.Drawing.Size(60, 23);
            this.btnMoreThan.TabIndex = 25;
            this.btnMoreThan.Text = "大于等于";
            this.btnMoreThan.Click += new System.EventHandler(this.btnMoreThan_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEqual.Location = new System.Drawing.Point(6, 23);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(52, 23);
            this.btnEqual.TabIndex = 25;
            this.btnEqual.Text = "等 于";
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // ribbonTabItemGroup4
            // 
            this.ribbonTabItemGroup4.Color = DevComponents.DotNetBar.eRibbonTabGroupColor.Orange;
            this.ribbonTabItemGroup4.GroupTitle = "Tab Group";
            this.ribbonTabItemGroup4.Name = "ribbonTabItemGroup4";
            // 
            // 
            // 
            this.ribbonTabItemGroup4.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(158)))), ((int)(((byte)(159)))));
            this.ribbonTabItemGroup4.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.ribbonTabItemGroup4.Style.BackColorGradientAngle = 90;
            this.ribbonTabItemGroup4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup4.Style.BorderBottomWidth = 1;
            this.ribbonTabItemGroup4.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(58)))), ((int)(((byte)(59)))));
            this.ribbonTabItemGroup4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup4.Style.BorderLeftWidth = 1;
            this.ribbonTabItemGroup4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup4.Style.BorderRightWidth = 1;
            this.ribbonTabItemGroup4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup4.Style.BorderTopWidth = 1;
            this.ribbonTabItemGroup4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ribbonTabItemGroup4.Style.TextColor = System.Drawing.Color.Black;
            this.ribbonTabItemGroup4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Location = new System.Drawing.Point(206, 306);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 25;
            this.btnConfirm.Text = "表达式验证";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.Location = new System.Drawing.Point(20, 306);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 25;
            this.btnQuery.Text = "查  询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.Location = new System.Drawing.Point(113, 306);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "清  空(&D)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(392, 306);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "关  闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ztSQLQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 338);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ztSQLQuery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL查询";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ztSQLQuery_FormClosing);
            this.Load += new System.EventHandler(this.SQLQuery_Load);
            this.groupBox6.ResumeLayout(false);
            this.gbValue.ResumeLayout(false);
            this.gbExpress.ResumeLayout(false);
            this.gbExpress.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbField.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox gbExpress;
        private System.Windows.Forms.TextBox SQLText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboLayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbValue;
        private System.Windows.Forms.ListBox listBoxValue;
        private System.Windows.Forms.GroupBox gbField;
        private System.Windows.Forms.ListBox listBoxField;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.RibbonTabItemGroup ribbonTabItemGroup4;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.ButtonX btnEqual;
        private DevComponents.DotNetBar.ButtonX btnMoreThan;
        private DevComponents.DotNetBar.ButtonX btnUnequal;
        private DevComponents.DotNetBar.ButtonX btnMore;
        private DevComponents.DotNetBar.ButtonX btnLessThan;
        private DevComponents.DotNetBar.ButtonX btnLess;
        private DevComponents.DotNetBar.ButtonX btnLike;
        private DevComponents.DotNetBar.ButtonX btnOr;
        private DevComponents.DotNetBar.ButtonX btnAnd;
        private DevComponents.DotNetBar.ButtonX btnClose;

    }
}