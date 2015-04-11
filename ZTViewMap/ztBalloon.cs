/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * 
 * XXX   2006/XX
 * --------------------------------------------------------------------- 
 * beizhan 2007-06 �޸ģ������ݵġ�mdichild ȥ��������ص���ʾӦ�ú͵�ͼ����û��ϵ 
 * --------------------------------------------------------------------*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace ZTViewMap
{
	/// <summary>
	/// Summary description for BalloonSearch.
    /// ��Ҫ����tips ����ʾ��
	/// </summary>
	public class ztBalloon : DevComponents.DotNetBar.Balloon
    {
        /// <summary>
        /// ��ʾ�����ı�ǩ�ؼ�
        /// </summary>
        public Label lbl_Field;

        /// <summary>
        /// ��ʾ���ݵĿؼ�
        /// </summary>
        public DataGridView findDataList;
        private DataGridViewTextBoxColumn ֵ;       // �ֶ���ʾ���������óɹ��ã��������ⲿֱ��������ʾ��
        private ztMapTip pMaptip;
  
        /// <summary>
        /// ���캯��,���롡Maptip ����
        /// </summary>        
        public ztBalloon(ztMapTip maptip)
        {            
            pMaptip = maptip;
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{             
            this.Visible = false;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ztBalloon));
            this.lbl_Field = new System.Windows.Forms.Label();
            this.findDataList = new System.Windows.Forms.DataGridView();
            this.ֵ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.findDataList)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Field
            // 
            this.lbl_Field.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Field.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Field.Location = new System.Drawing.Point(4, 20);
            this.lbl_Field.Name = "lbl_Field";
            this.lbl_Field.Size = new System.Drawing.Size(190, 18);
            this.lbl_Field.TabIndex = 2;
            this.lbl_Field.Text = "ͼ�㣺";
            // 
            // findDataList
            // 
            this.findDataList.AllowUserToAddRows = false;
            this.findDataList.AllowUserToDeleteRows = false;
            this.findDataList.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.findDataList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.findDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.findDataList.ColumnHeadersVisible = false;
            this.findDataList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ֵ});
            this.findDataList.Location = new System.Drawing.Point(7, 41);
            this.findDataList.Name = "findDataList";
            this.findDataList.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.findDataList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.findDataList.RowHeadersWidth = 75;
            this.findDataList.RowTemplate.Height = 20;
            this.findDataList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.findDataList.Size = new System.Drawing.Size(204, 47);
            this.findDataList.TabIndex = 4;
            // 
            // ֵ
            // 
            this.ֵ.HeaderText = "ֵ";
            this.ֵ.Name = "ֵ";
            this.ֵ.ReadOnly = true;
            this.ֵ.Width = 125;
            // 
            // ztBalloon
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(218, 96);
            this.Controls.Add(this.findDataList);
            this.Controls.Add(this.lbl_Field);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(200, 96);
            this.Name = "ztBalloon";
            this.Opacity = 0.6;
            this.Click += new System.EventHandler(this.ztBalloon_Click);
            this.CloseButtonClick += new System.EventHandler(this.ztBalloon_CloseButtonClick);
            this.Activated += new System.EventHandler(this.BalloonSearch_Activated);
            this.Deactivate += new System.EventHandler(this.BalloonSearch_Deactivate);
            ((System.ComponentModel.ISupportInitialize)(this.findDataList)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        // �ڻ�Ͳ��ʱ����͸������
		private void BalloonSearch_Deactivate(object sender, System.EventArgs e)
		{
			this.Opacity=.75;
		}

		private void BalloonSearch_Activated(object sender, System.EventArgs e)
		{
			this.Opacity=.6;
		}
                
		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(this.BackColor==SystemColors.Info)
			{
				// Switch to Office 2003 style colors...
				ColorScheme scheme=new ColorScheme(eDotNetBarStyle.Office2003);
				this.BackColor=scheme.ItemCheckedBackground;
                this.BackColor2=scheme.ItemCheckedBackground2;
				this.BackColorGradientAngle=scheme.ItemCheckedBackgroundGradientAngle;
				this.Refresh();
			}
			else
			{
				this.BackColor=SystemColors.Info;
				this.BackColor2=Color.Empty;
				this.Refresh();
			}
		}

        // ��굥�����رա�
        private void ztBalloon_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // �رհ�ť������ֱ�ӹرյ�ͼ���ڵġ�tips ��ʾ��
        private void ztBalloon_CloseButtonClick(object sender, EventArgs e)
        {
            pMaptip.CloseMapTip();
            this.Close();
        }
	}
}
