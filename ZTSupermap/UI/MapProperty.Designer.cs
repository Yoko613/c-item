namespace ZTSupermap.UI
{
    partial class MapProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapProperty));
            this.txtMapname = new System.Windows.Forms.TextBox();
            this.txtMapAlias = new System.Windows.Forms.TextBox();
            this.txtMapdesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.axSuperLegend1 = new AxSuperLegendLib.AxSuperLegend();
            this.axSuperMap1 = new AxSuperMapLib.AxSuperMap();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperLegend1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperMap1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMapname
            // 
            this.txtMapname.Location = new System.Drawing.Point(80, 12);
            this.txtMapname.Name = "txtMapname";
            this.txtMapname.ReadOnly = true;
            this.txtMapname.Size = new System.Drawing.Size(197, 21);
            this.txtMapname.TabIndex = 0;
            // 
            // txtMapAlias
            // 
            this.txtMapAlias.Location = new System.Drawing.Point(80, 39);
            this.txtMapAlias.Name = "txtMapAlias";
            this.txtMapAlias.Size = new System.Drawing.Size(197, 21);
            this.txtMapAlias.TabIndex = 1;
            // 
            // txtMapdesc
            // 
            this.txtMapdesc.Location = new System.Drawing.Point(80, 66);
            this.txtMapdesc.Multiline = true;
            this.txtMapdesc.Name = "txtMapdesc";
            this.txtMapdesc.Size = new System.Drawing.Size(197, 55);
            this.txtMapdesc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "地图名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "地图别名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "描述：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "包含层：";
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(39, 275);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(178, 275);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // axSuperLegend1
            // 
            this.axSuperLegend1.Enabled = true;
            this.axSuperLegend1.Location = new System.Drawing.Point(80, 137);
            this.axSuperLegend1.Name = "axSuperLegend1";
            this.axSuperLegend1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSuperLegend1.OcxState")));
            this.axSuperLegend1.Size = new System.Drawing.Size(195, 119);
            this.axSuperLegend1.TabIndex = 10;
            // 
            // axSuperMap1
            // 
            this.axSuperMap1.Enabled = true;
            this.axSuperMap1.Location = new System.Drawing.Point(0, 152);
            this.axSuperMap1.Name = "axSuperMap1";
            this.axSuperMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSuperMap1.OcxState")));
            this.axSuperMap1.Size = new System.Drawing.Size(78, 88);
            this.axSuperMap1.TabIndex = 11;
            this.axSuperMap1.Visible = false;
            // 
            // MapProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 310);
            this.Controls.Add(this.axSuperMap1);
            this.Controls.Add(this.axSuperLegend1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMapdesc);
            this.Controls.Add(this.txtMapAlias);
            this.Controls.Add(this.txtMapname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapProperty";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图属性";
            this.Load += new System.EventHandler(this.MapProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSuperLegend1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperMap1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMapname;
        private System.Windows.Forms.TextBox txtMapAlias;
        private System.Windows.Forms.TextBox txtMapdesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private AxSuperLegendLib.AxSuperLegend axSuperLegend1;
        private AxSuperMapLib.AxSuperMap axSuperMap1;
    }
}