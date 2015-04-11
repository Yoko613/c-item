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
    /// 坐标系转换参数设置界面。
    /// 暂时做的比较简单，只支持基于地心坐标系的三参数和七参数参数。
    /// 在实际当中一般填写的单位，角度是秒，尺度是ppm 百万倍，要注意转换
    /// 注意：超图的旋转角度单位是 秒
    /// 我操啊，超图啥时候开始人性化了，可是帮助里面又没说明，整整两天时间阿，老子就猜这个了。
    /// 与此同时，deskpro 里面还有低级错误，老子以它为标准测试，结果标准有错误，晕阿。
    /// </summary>
    public partial class PJParamsSetting : DevComponents.DotNetBar.Office2007Form
    {
        private bool m_SevenParam;              // 是否为七参数界面。

        private soPJParams m_pjParam = null;    //当前投影参数
        private double m_pi = 4.1415926;

        /// <summary>
        /// 设置的投影转换参数,取消时返回　null
        /// </summary>
        public soPJParams ProjectParams
        {
            get { return m_pjParam; }
        }

        /// <summary>
        /// 设置基于地心坐标系的参数
        /// </summary>
        /// <param name="sevenparam">是否为七参数，false 则只接收三参数</param>
        public PJParamsSetting(bool sevenparam)
        {
            InitializeComponent();

            m_SevenParam = sevenparam;
            
            // 旋转和尺度要根据是否七参数
            txtRX.Enabled = m_SevenParam;
            txtRY.Enabled = m_SevenParam;
            txtRZ.Enabled = m_SevenParam;
            txtScale.Enabled = m_SevenParam;            
        }

        /// <summary>
        /// 设置基于地心坐标系的参数
        /// </summary>
        /// <param name="sevenparam">是否为七参数，false 则只接收三参数</param>
        public PJParamsSetting(bool sevenparam,soPJParams initparams)
            :this(sevenparam)
        {
            m_pjParam = initparams;

            if (m_pjParam != null)
            {
                txtX.Text = m_pjParam.TranslateX.ToString();
                txtY.Text = m_pjParam.TranslateY.ToString();
                txtZ.Text = m_pjParam.TranslateZ.ToString(); ;

                if (m_SevenParam)
                {
                    double mmX = m_pjParam.RotateX;
                    double mmY = m_pjParam.RotateY;
                    double mmZ = m_pjParam.RotateZ;
                    double ppmScale = m_pjParam.ScaleFactor * 1000000.0;
                    txtRX.Text = mmX.ToString();
                    txtRY.Text = mmY.ToString();
                    txtRZ.Text = mmZ.ToString();
                    txtScale.Text = ppmScale.ToString();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool VerifyDouble(string str, string prompt)
        {
            try
            {
                double dbl = double.Parse(str);
                return true;
            }
            catch
            {
                string strpm = prompt + "格式不正确！";
                ZTDialog.ztMessageBox.Messagebox(strpm, "输入错误", MessageBoxButtons.OK);
                return false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {            
            if (VerifyDouble(txtX.Text, "X轴平移参数") != true)
                return;

            if (VerifyDouble(txtY.Text, "Y轴平移参数") != true)
                return;

            if (VerifyDouble(txtZ.Text, "Z轴平移参数") != true)
                return;

            if (m_SevenParam)
            {                
                if (VerifyDouble(txtRX.Text, "X轴旋转参数") != true)
                    return;

                if (VerifyDouble(txtRY.Text, "Y轴旋转参数") != true)
                    return;

                if (VerifyDouble(txtRZ.Text, "Z轴旋转参数") != true)
                    return;

                if (VerifyDouble(txtScale.Text, "缩放尺度") != true)
                    return;
            }
                        
            if (m_pjParam == null)
                m_pjParam = new soPJParamsClass();
            m_pjParam.TranslateX = double.Parse(txtX.Text);
            m_pjParam.TranslateY = double.Parse(txtY.Text);
            m_pjParam.TranslateZ = double.Parse(txtZ.Text);

            if (m_SevenParam)
            {
                double mmX = double.Parse(txtRX.Text);
                double mmY = double.Parse(txtRY.Text);
                double mmZ = double.Parse(txtRZ.Text);
                double ppmScale = double.Parse(txtScale.Text) * 0.000001;
                m_pjParam.RotateX = mmX;
                m_pjParam.RotateY = mmY;
                m_pjParam.RotateZ = mmZ;
                m_pjParam.ScaleFactor = ppmScale;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}