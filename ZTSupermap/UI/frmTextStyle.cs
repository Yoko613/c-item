using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMLibrary.Geometry;

namespace ZTSupermap.UI
{
    public partial class frmTextStyle : DevComponents.DotNetBar.Office2007Form
    {
        #region ����
        /// <summary>
        /// Supermap�������
        /// </summary>
        public soNet_TextStyle TextStyle
        {
            get { return m_objTextStyle; }
            set { m_objTextStyle = value; }
        }

        /// <summary>
        /// DonNet�������
        /// </summary>
        public Font NetFont
        {
            get { return m_objFont; }
            set { m_objFont = value; }
        }
        #endregion

        #region ˽�б���
        soNet_TextStyle m_objTextStyle = null;
        
        Font m_objFont = null;

        System.Windows.Forms.FontDialog m_objFontDialog = new FontDialog();
        SuperMapLib.seTextAlign objTextAlign = SuperMapLib.seTextAlign.sctMiddleCenter;
        #endregion

        #region ���캯��
        public frmTextStyle()
        {
            InitializeComponent();
        }

        public frmTextStyle(soNet_TextStyle objTextStyle)
        {
            m_objTextStyle = objTextStyle;
            InitializeComponent();
        }

        public frmTextStyle(soNet_TextStyle objTextStyle, Font objFont)
        {
            m_objTextStyle = objTextStyle;
            m_objFont = objFont;
            InitializeComponent();

            //��ʼ��Supermap����
            InitSuperMapFont();
        }
        #endregion

        #region �û���Ӧ�¼�
        private void btnQJ_Color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color objQJ_Color = colorDialog1.Color;
                pbx_QJ_Color.BackColor = objQJ_Color;
            }
        }

        private void btnBJ_Color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color objQJ_Color = colorDialog1.Color;
                pbx_BJ_Color.BackColor = objQJ_Color;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.ConvertFontToSoStyle())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void frmTextStyle_Load(object sender, EventArgs e)
        {
            cbxAlign.SelectedIndex = 10;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (m_objFont != null)
            {
                m_objFontDialog.Font = m_objFont;
            }

            if (m_objFontDialog.ShowDialog() == DialogResult.OK)
            {
                m_objFont = m_objFontDialog.Font;

                //if (m_objFont.Italic)
                //{
                //    cbxFontRotation.Enabled = true;
                //    cbxFontRotation.BackColor = System.Drawing.Color.White;
                //}
                //else
                //{
                //    cbxFontRotation.Enabled = false;
                //    cbxFontRotation.BackColor = System.Drawing.Color.Silver;
                //}

                txtHeight.Text = Convert.ToString(m_objFont.Size / 0.283);
                txtWidth.Text = Convert.ToString(m_objFont.Size / 0.283 / 2);
            }
        }

        private void cbxAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxAlign.Text)
            {
                case "���ϽǶ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctTopLeft;
                    break;
                case "���п��϶���":
                    objTextAlign = SuperMapLib.seTextAlign.sctTopCenter;
                    break;
                case "���ϽǶ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctTopRight;
                    break;
                case "���������":
                    objTextAlign = SuperMapLib.seTextAlign.sctBaslineLeft;
                    break;
                case "���߾��ж���":
                    objTextAlign = SuperMapLib.seTextAlign.sctBaslineCenter;
                    break;
                case "�����Ҷ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctBaslineRight;
                    break;
                case "���½Ƕ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctBottomLeft;
                    break;
                case "���п��¶���":
                    objTextAlign = SuperMapLib.seTextAlign.sctBottomCenter;
                    break;
                case "���½Ƕ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctBottomRight;
                    break;
                case "���п������":
                    objTextAlign = SuperMapLib.seTextAlign.sctMiddleLeft;
                    break;
                case "���Ķ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctMiddleCenter;
                    break;
                case "���п��Ҷ���":
                    objTextAlign = SuperMapLib.seTextAlign.sctMiddleRight;
                    break;
            }
        }

        private void chkUserDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUserDefine.Checked)
            {
                txtHeight.BackColor = System.Drawing.Color.White;
                txtWidth.BackColor = System.Drawing.Color.White;
                txtWidth.Enabled = true;
                txtHeight.Enabled = true;
            }
            else
            {
                txtHeight.BackColor = System.Drawing.Color.Silver;
                txtWidth.BackColor = System.Drawing.Color.Silver;
                txtHeight.Enabled = false;
                txtWidth.Enabled = false;
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double dTest = Convert.ToDouble(txtHeight.Text);
            }
            catch (Exception Err)
            {
                txtHeight.Text = "0";
            }
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double dTest = Convert.ToDouble(txtWidth.Text);
            }
            catch (Exception Err)
            {
                txtWidth.Text = "0";
            }
        }
        #endregion 
        
        #region ˽�к���

        /// <summary>
        /// ת��Font��soStyle
        /// </summary>
        /// <returns>�Ƿ�ת���ɹ�</returns>
        private bool ConvertFontToSoStyle()
        {
            try
            {
                if (m_objFont == null) return false;
                m_objTextStyle = new soNet_TextStyle();
                m_objTextStyle.Align = this.objTextAlign;
                m_objTextStyle.BgColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(pbx_BJ_Color.BackColor));
                m_objTextStyle.Color = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(pbx_QJ_Color.BackColor));
                m_objTextStyle.Bold = m_objFont.Bold;
                m_objTextStyle.FixedSize = chkGDDX.Checked;
                m_objTextStyle.FixedTextSize = Convert.ToInt16(m_objFont.Size);
                m_objTextStyle.FontName = m_objFont.Name;
                
                try
                {
                    m_objTextStyle.FontHeight = Convert.ToDouble(txtHeight.Text);
                    m_objTextStyle.FontWidth = Convert.ToDouble(txtWidth.Text);
                }
                catch
                {
                    MessageBox.Show("���������֣�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                m_objTextStyle.Italic = m_objFont.Italic;

                if (m_objFont.Italic)
                    m_objTextStyle.ItalicAngle = Convert.ToDouble(cbxFontRotation.Text);

                m_objTextStyle.Outline = chxLK.Checked;
                m_objTextStyle.Rotation = Convert.ToDouble(cbxRotation.Text);
                m_objTextStyle.Shadow = chkYY.Checked;
                m_objTextStyle.Stroke = (m_objFont.Style.ToString().IndexOf("Strikeout")>0?true:false);
                m_objTextStyle.Transparent = chkBJTM.Checked;
                m_objTextStyle.Underline = m_objFont.Underline;
                
                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            { 
                
            }
        }

        /// <summary>
        /// ��������:��ʼ����ͼ����
        /// </summary>
        private void InitSuperMapFont()
        {
            try
            {
                if (TextStyle != null)
                {
                    pbx_QJ_Color.BackColor = System.Drawing.ColorTranslator.FromOle((int)TextStyle.Color);
                    pbx_BJ_Color.BackColor = System.Drawing.ColorTranslator.FromOle((int)TextStyle.BgColor);

                    switch (TextStyle.Align)
                    {
                        case SuperMapLib.seTextAlign.sctBaslineCenter:
                            cbxAlign.Text = "���߾��ж���";
                            break;
                        case SuperMapLib.seTextAlign.sctBaslineLeft:
                            cbxAlign.Text = "���������";
                            break;
                        case SuperMapLib.seTextAlign.sctBaslineRight:
                            cbxAlign.Text = "�����Ҷ���";
                            break;
                        case SuperMapLib.seTextAlign.sctBottomCenter:
                            cbxAlign.Text = "���п��¶���";
                            break;
                        case SuperMapLib.seTextAlign.sctBottomLeft:
                            cbxAlign.Text = "���½Ƕ���";
                            break;
                        case SuperMapLib.seTextAlign.sctBottomRight:
                            cbxAlign.Text = "���½Ƕ���";
                            break;
                        case SuperMapLib.seTextAlign.sctMiddleCenter:
                            cbxAlign.Text = "���Ķ���";
                            break;
                        case SuperMapLib.seTextAlign.sctMiddleLeft:
                            cbxAlign.Text = "���п������";
                            break;
                        case SuperMapLib.seTextAlign.sctMiddleRight:
                            cbxAlign.Text = "���п��Ҷ���";
                            break;
                        case SuperMapLib.seTextAlign.sctTopCenter:
                            cbxAlign.Text = "���п��϶���";
                            break;
                        case SuperMapLib.seTextAlign.sctTopLeft:
                            cbxAlign.Text = "���ϽǶ���";
                            break;
                        case SuperMapLib.seTextAlign.sctTopRight:
                            cbxAlign.Text = "���ϽǶ���";
                            break;
                    }

                    cbxRotation.Text = TextStyle.Rotation.ToString();

                    if (TextStyle.Italic)
                    {
                        cbxFontRotation.Enabled = true;
                        cbxFontRotation.BackColor = System.Drawing.Color.White;
                        cbxFontRotation.Text = TextStyle.ItalicAngle.ToString();
                    }
                    chxLK.Checked = TextStyle.Outline;
                    chkBJTM.Checked = TextStyle.Transparent;
                    chkYY.Checked = TextStyle.Shadow;
                    chkGDDX.Checked = TextStyle.FixedSize;

                    chkUserDefine.Checked = true;

                    txtHeight.Text = TextStyle.FontHeight.ToString();
                    txtWidth.Text = TextStyle.FontHeight.ToString();
                }
            }
            catch (Exception Err)
            {
                
            }
        }

        #endregion 

    }
}