using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZTMapPrint
{
    public partial class frmSetBuffer : DevComponents.DotNetBar.Office2007Form
    {
        //×óÓÒ°ë¾¶
        private double fLeftRadius = 0;
        private double fRightRadius = 0;
        //Æ½»¬¶È
        private int iRadArc = 12;

        public frmSetBuffer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ×óÓÒ°ë¾¶ÏàµÈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtLeftRightEquel_Click(object sender, System.EventArgs e)
        {
            txtLeftRad.Enabled = true;
            txtRightRad.Enabled = false;
            rbtLeftRightBuf.Checked = true;
        }

        /// <summary>
        /// µã»÷×óÓÒ°ë¾¶²»ÏàµÈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtLeftRightNotEquel_Click(object sender, System.EventArgs e)
        {
            txtRightRad.Enabled = true;
            txtLeftRad.Enabled = true;
        }

        /// <summary>
        /// µã»÷Æ½Í·»º³å
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtRectBuf_Click(object sender, System.EventArgs e)
        {
            rbtLeftBuf.Checked = true;
            rbtLeftRightEquel.Enabled = true;
            rbtLeftRightNotEquel.Enabled = true;
            rbtLeftBuf.Enabled = true;
            txtRightRad.Enabled = true;
            rbtRigthBuf.Enabled = true;
            rbtLeftRightBuf.Enabled = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            //txtArc.Text = "0";
            //txtArc.ReadOnly = true;
            rbtLeftBuf_Click(sender, e);

        }

        /// <summary>
        /// µã»÷Ô²Í·»º³å
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtRadBuf_Click(object sender, System.EventArgs e)
        {
            rbtLeftRightNotEquel.Enabled = false;
            rbtRigthBuf.Enabled = false;
            rbtLeftBuf.Enabled = false;
            txtRightRad.Enabled = false;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            //txtArc.ReadOnly = false;
            txtLeftRad.Enabled = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            rbtLeftRightEquel.Enabled = true;
        }

        /// <summary>
        /// µã»÷×ó»º³å
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtLeftBuf_Click(object sender, System.EventArgs e)
        {
            txtRightRad.Enabled = false;
            txtLeftRad.Enabled = true;
            rbtLeftRightEquel.Enabled = false;
            rbtLeftRightNotEquel.Enabled = false;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        /// <summary>
        /// µã»÷ÓÒ»º³å
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtRigthBuf_Click(object sender, System.EventArgs e)
        {
            txtRightRad.Enabled = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            txtLeftRad.Enabled = false;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            rbtLeftRightEquel.Enabled = false;
            rbtLeftRightNotEquel.Enabled = false;

        }

        /// <summary>
        /// µã»÷×óÓÒ»º³å
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtLeftRightBuf_Click(object sender, System.EventArgs e)
        {
            if (rbtRectBuf.Checked == true)
            {
                rbtLeftRightEquel.Enabled = true;
                rbtLeftRightNotEquel.Enabled = true;
                txtLeftRad.Enabled = true;
                txtRightRad.Enabled = true;
                this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
                this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void FrmSetBuffer_Load(object sender, System.EventArgs e)
        {

            rbtRectBuf_Click(sender, e);
            rbtRectBuf.Checked = true;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        /// <summary>
        /// È·¶¨»º³å²ÎÊý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, System.EventArgs e)
        {
            if (IsNumber(txtLeftRad.Text))
            {
                if (rbtRectBuf.Checked == true)
                {
                    if (rbtLeftBuf.Checked == true)
                    {
                        fLeftRadius = Convert.ToDouble(txtLeftRad.Text);
                        fRightRadius = 0.0;
                        if (IsNumber(txtArc.Text))
                        {
                            iRadArc = Convert.ToInt32(txtArc.Text);
                        }
                    }

                    if (rbtRigthBuf.Checked == true)
                    {
                        if (IsNumber(txtRightRad.Text))
                        {
                            fLeftRadius = 0.0;
                            fRightRadius = Convert.ToDouble(txtRightRad.Text);
                            if (IsNumber(txtArc.Text))
                            {
                                iRadArc = Convert.ToInt32(txtArc.Text);
                            }
                        }

                    }

                    if (rbtLeftRightBuf.Checked == true)
                    {
                        if (IsNumber(txtRightRad.Text))
                        {
                            fLeftRadius = Convert.ToDouble(txtLeftRad.Text);
                            fRightRadius = Convert.ToDouble(txtRightRad.Text);
                            if (IsNumber(txtArc.Text))
                            {
                                iRadArc = Convert.ToInt32(txtArc.Text);
                            }
                        }
                    }

                    if (rbtLeftRightEquel.Checked == true)
                    {
                        fLeftRadius = Convert.ToDouble(txtLeftRad.Text);
                        fRightRadius = Convert.ToDouble(txtLeftRad.Text);
                        if (IsNumber(txtArc.Text))
                        {
                            iRadArc = Convert.ToInt32(txtArc.Text);
                        }
                    }
                }

                if (rbtRadBuf.Checked == true)
                {
                    fLeftRadius = Convert.ToDouble(txtLeftRad.Text);
                    //fRightRadius = Convert.ToDouble(txtLeftRad.Text);
                    if (IsNumber(txtArc.Text))
                    {
                        iRadArc = Convert.ToInt32(txtArc.Text);
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("ÇëÊäÈëÊý×Ö!");
                return;
            }
        }

        private void txtLeftRad_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //            if (Convert.ToInt16(e.KeyData) > 9 || Convert.ToInt16(e.KeyData) < 0)
            //            {
            //                txtLeftRad.Text = txtLeftRad.Text.Replace( e.KeyData.ToString(),"");
            //            }
        }

        private void txtLeftRad_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            //            if (Convert.ToInt16(e.KeyData) > 9 || Convert.ToInt16(e.KeyData) < 0)
            //            {
            //                txtLeftRad.Text = txtLeftRad.Text.Replace( e.KeyData.ToString(),"");
            //            }        
        }

        private void txtLeftRad_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //            if (Convert.ToInt16(e.KeyData) > 9 || Convert.ToInt16(e.KeyData) < 0)
            //            {
            //                txtLeftRad.Text = txtLeftRad.Text.Substring(0,txtLeftRad.Text.Length - 1);
            //            }        
        }

        //×ó°ë¾¶
        public double LeftRadius
        {
            get
            {
                return fLeftRadius;
            }
        }

        //ÓÒ°ë¾¶
        public double RightRadius
        {
            get
            {
                return fRightRadius;
            }
        }

        //Æ½»¬¶È
        public int PHArc
        {
            get
            {
                return iRadArc;
            }
        }

        /// <summary>
        /// ÅÐ¶Ï×Ö·û´®ÊÇ·ñÊÇÊý×Ö
        /// </summary>
        /// <param name="p_str"></param>
        /// <returns></returns>
        public static bool IsNumber(string p_str)
        {
            try
            {
                System.Convert.ToDouble(p_str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void rbtLeftRightEquel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmSetBuffer_Load_1(object sender, EventArgs e)
        {
            rbtRadBuf_Click(sender, e);
            rbtLeftRightEquel_Click(sender, e);
            rbtLeftRightEquel.Checked = true;
        }
    }
}