using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SuperMapLib;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 设置叠加显示字段
    /// </summary>
    internal partial class OverlayFieldSetting : DevComponents.DotNetBar.Office2007Form
    {
        private soDataset m_sourdt;
        private soDataset m_tardt;
        private string[] m_sourField;
        private string[] m_tarField;
        
        /// <summary>
        /// 返回选择的源数据集字段
        /// </summary>
        public string[] SelectSourField
        {
            get { return m_sourField; }
        }

        /// <summary>
        /// 设置选择的叠加数据集字段
        /// </summary>
        public string[] SelectTarFiled
        {
            get { return m_tarField; }
        }

        /// <summary>
        /// 显示字段
        /// </summary>        
        /// <param name="sourdt">源数据集名</param>
        /// <param name="analystdt">目标数据集名</param>
        public OverlayFieldSetting(soDataset sourdt, soDataset analystdt)
        {
            InitializeComponent();

            m_sourdt = sourdt;
            m_tardt = analystdt;            
        }

        // 显示字段
        private void OverlayFieldSetting_Load(object sender, EventArgs e)
        {
            selFiledSour.Dataset = m_sourdt;
            selFiledSour.ShowSysFields = true;
            selFiledSour.LoadFields();

            selFieldTar.Dataset = m_tardt;
            selFieldTar.ShowSysFields = true;
            selFieldTar.LoadFields();
        }   

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_sourField = selFiledSour.GetSelFieldNames();
            m_tarField = selFieldTar.GetSelFieldNames();
            this.DialogResult = DialogResult.OK;
        }
                     
    }
}