/*------------------------------------------------------------------------------------
 * ��ʾ����Դ����
 * beizhan 080326 
 *         ��Ū��һ���򻯰棬���Ըĳɺ� deskpro һ����
 * ---------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap.UI
{
    /// <summary>
    /// ������ʾ����Դ����ϸ��Ϣ���ܹ������趨����Դ��ͶӰ��Ϣ��
    /// ʹ����ֻ��Ҫ�ڹ���ʱָ������Դ���ɡ�
    /// </summary>
    public partial class DataSourceProperty : DevComponents.DotNetBar.Office2007Form
    {
        private soDataSource m_ds;

        public DataSourceProperty(soDataSource ds)
        {
            InitializeComponent();
                        
            m_ds = ds;
        }

        // ��ʾ����Դ���ԡ�
        private void DataSourceProperty_Load(object sender, EventArgs e)
        {
            // ��ʾ����Դ����Ϣ
            if (m_ds != null)
            {
                txtFileName.Text = m_ds.Name;
                txtEngineType.Text = m_ds.EngineType.ToString();
                string strOpenType = string.Empty;
                if (m_ds.Exclusive)
                    strOpenType += "��ռ /";
                if (m_ds.ReadOnly)
                    strOpenType += "ֻ�� /";
                if (m_ds.Transacted)
                    strOpenType += "���� /";
                txtOpentype.Text = strOpenType;

                txtDesc.Text = m_ds.Description;


                soPJCoordSys objPrjSys = m_ds.PJCoordSys;
                txtPrjInfo.Text = ztSuperMap.CoordSystemDescription(objPrjSys);
                Marshal.ReleaseComObject(objPrjSys);
            }
            
        }
                

        // ������ͶӰ
        private void btnSetPrj_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                soPJCoordSys objPrjSys = m_ds.PJCoordSys;
                if (objPrjSys.ShowSettingDialog())
                {
                    m_ds.PJCoordSys = objPrjSys;
                    txtPrjInfo.Text = ztSuperMap.CoordSystemDescription(objPrjSys);
                }
                Marshal.ReleaseComObject(objPrjSys);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}