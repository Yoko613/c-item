/*------------------------------------------------------------------------------------
 * 显示数据源属性
 * beizhan 080326 
 *         先弄了一个简化版，可以改成和 deskpro 一样。
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
    /// 此类显示数据源的详细信息，能够重新设定数据源的投影信息。
    /// 使用是只需要在构造时指定数据源即可。
    /// </summary>
    public partial class DataSourceProperty : DevComponents.DotNetBar.Office2007Form
    {
        private soDataSource m_ds;

        public DataSourceProperty(soDataSource ds)
        {
            InitializeComponent();
                        
            m_ds = ds;
        }

        // 显示数据源属性。
        private void DataSourceProperty_Load(object sender, EventArgs e)
        {
            // 显示数据源的信息
            if (m_ds != null)
            {
                txtFileName.Text = m_ds.Name;
                txtEngineType.Text = m_ds.EngineType.ToString();
                string strOpenType = string.Empty;
                if (m_ds.Exclusive)
                    strOpenType += "独占 /";
                if (m_ds.ReadOnly)
                    strOpenType += "只读 /";
                if (m_ds.Transacted)
                    strOpenType += "事务 /";
                txtOpentype.Text = strOpenType;

                txtDesc.Text = m_ds.Description;


                soPJCoordSys objPrjSys = m_ds.PJCoordSys;
                txtPrjInfo.Text = ztSuperMap.CoordSystemDescription(objPrjSys);
                Marshal.ReleaseComObject(objPrjSys);
            }
            
        }
                

        // 重新设投影
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