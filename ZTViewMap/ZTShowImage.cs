/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 
 * 查看影像图像等
 * 
 * liup   2010/04//28
 * --------------------------------------------------------------------- 
 * 
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZTViewMap
{
    /// <summary>
    /// 查看影像图像等
    /// </summary>
    public partial class ZTShowImage : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 图像操作控件对象
        /// </summary>
        ztImageCtrl.ztImgCtrl m_imgctrl = null;

        public ZTShowImage(string strfrmtext, System.Drawing.Image sourceimage)
        {
            InitializeComponent();
            this.Text = "图像查看-";
            
            if (!string.IsNullOrEmpty(strfrmtext))
                this.Text += strfrmtext;
            else
                this.Text += "未知图像";
            if (sourceimage == null) return;
            this.m_imgctrl = new ztImageCtrl.ztImgCtrl();
            this.Controls.Add(this.m_imgctrl);//实例化并写入窗体控件集合
            this.m_imgctrl.Dock = DockStyle.Fill;
            this.m_imgctrl.OpenImage(sourceimage);//打开
        }

        private void FormShowImage_Load(object sender, EventArgs e)
        {

        }
    }
}
