/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * Ӱ�񽥱�Ч����
 * XXX   2006/XX
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;
using ZTDialog;


namespace ZTViewMap
{
    /// <summary>
    /// Ӱ�񽥱�Ч�����������ν������ʾ����Ӱ�񣬸�������Ӱ��仯��
    /// �ڽ���͹����Ͽ�����Ҫ�򻯡�
    /// </summary>
    public partial class ztGradualForm : DevComponents.DotNetBar.Office2007Form
    {        
        private ztMdiChild pSubChild;           // ��ͼ����
        private soLayer objLayer;               
        private bool bChange;
        private int iSwitch=1;
        private bool bStop;
        private int iSwitchStep = 1;            // ���ٽ���Ĵ�����

        /// <summary>
        /// ���췽������Ҫ�����ͼ����
        /// </summary>
        /// <param name="mdichild">��ͼ����</param>
        public ztGradualForm(ztMdiChild mdichild)
        {
            pSubChild = mdichild;  
            InitializeComponent();
        }

        // �϶�����Ļ���ʱ
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            switchtimer.Stop();
            trackBartimer.Stop();
            chkAuto.Checked = false;
            btnSwitch.Text = "��ʼ����(&S)";
            bStop = false;

            labelProgress.Text = trackBar.Value.ToString() + "%";
            
            RasterOpaque();
            if (trackBar.Value == 0)
            {
                chkAuto.Checked = false;
            }
            bChange = true;
        }

        // Ӱ��͸�������ݻ������ֵ����Ӱ���͸���ȡ�
        // �����ҵ��ĵ�һ��ͼ�㡣
        private void RasterOpaque()
        {
            string i = trackBar.Value.ToString();
            soLayers objLayers = pSubChild.SuperMap.Layers;
            if (objLayers != null)
            {
                objLayer = objLayers[1];
                if (objLayer != null)
                {
                    objLayer.RasterOpaqueRate = System.Int16.Parse(i);
                    soRect objRect = pSubChild.SuperMap.ViewBounds;
                    pSubChild.SuperMap.RefreshEx(objRect);
                    Marshal.ReleaseComObject(objRect);
                    objRect = null;
                }
                Marshal.ReleaseComObject(objLayers);
                objLayers = null;
            }
        }

        // �Ƿ��Զ����䡣
        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (numericUpDown.Value == 0)
            {
                chkAuto.Checked = false;
                return;
            }
            else
            {
                if (chkAuto.Checked)
                {
                    switchtimer.Stop();
                    btnSwitch.Text = "��ʼ����(&S)";
                    bStop = false;

                    trackBartimer.Start();
                    trackBar.Value = int.Parse(numericUpDown1.Value.ToString());
                    trackBar.Enabled = false;
                    numericUpDown.Enabled = false;
                    numericUpDown1.Enabled = false;
                    buttonX1.Enabled = true;
                }
                else
                {
                    trackBartimer.Stop();
                    trackBar.Enabled = true;
                    numericUpDown.Enabled = true;
                    numericUpDown1.Enabled = true;
                    buttonX1.Enabled = false;
                }
                buttonX1.Text = "��ͣ(&P)";
            }

        }

        // �Զ������ʱ�䷢����
        private void trackBartimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (trackBar.Value == trackBar.Minimum)
                {
                    trackBartimer.Stop();
                    trackBar.Enabled = true;
                    numericUpDown.Enabled = true;
                    numericUpDown1.Enabled = true;
                }
                else
                {
                    int i = int.Parse(numericUpDown.Value.ToString()) + trackBar.Minimum;
                    if (trackBar.Value > i)
                    {
                        trackBar.Value -= i;
                        labelProgress.Text = trackBar.Value.ToString() + "%";
                        RasterOpaque();
                    }
                    else
                    {
                        trackBar.Value = 0;
                        labelProgress.Text = "0" + "%";
                        RasterOpaque();
                    }

                }
            }
            catch
            {
                return;
            }
        }

        // �ֶ����ٽ����ʱ�䷢����
        private void switchtimer_Tick(object sender, EventArgs e)
        {

            soLayers objLayers = pSubChild.SuperMap.Layers;
            if (objLayer == null)
                return;
            if (objLayers.Count < 2)
            {
                MessageBox.Show(this, "Ӱ��ͼ������С������,������", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (objLayers != null)
            {
                if (iSwitchStep % numericUpDown.Value == 0)
                {
                    objLayers.MoveBottom(iSwitch);
                    soRect objRect = pSubChild.SuperMap.ViewBounds;
                    pSubChild.SuperMap.RefreshEx(objRect);
                    Marshal.ReleaseComObject(objRect);
                    objRect = null;
                    Marshal.ReleaseComObject(objLayers);
                    objLayers = null;
                }
                iSwitchStep++;
            }
        }
        
        // �Զ�������ͣ��ť
        private void buttonX1_Click(object sender, EventArgs e)
        {
            switchtimer.Stop();
            bStop = false;
            if (buttonX1.Text == "��ͣ(&P)")
            {
                buttonX1.Text = "��ʼ(&S)";
                trackBartimer.Stop();
                
            }
            else
            {
                buttonX1.Text = "��ͣ(&P)";
                trackBartimer.Start();
            }
        }

        // �����ֹ����䡣
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (bStop == false)
            {
                switchtimer.Start();
                trackBartimer.Stop();
                bStop = true;
                btnSwitch.Text = "ֹͣ����(&S)";
            }
            else
            {
                switchtimer.Stop();
                bStop = false;
                iSwitchStep = 1;
                btnSwitch.Text = "��ʼ����(&S)";
            }
        }

        // ���ڹر�ʱ���ָ�Ӱ���͸���ȡ�
        private void ztGradualForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (bChange == true)
                {
                    if (objLayer != null)
                    {
                        objLayer.RasterOpaqueRate = 100;
                        pSubChild.SuperMap.RefreshEx(pSubChild.SuperMap.ViewBounds);
                        Marshal.ReleaseComObject(objLayer);
                        objLayer = null;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void ztGradualForm_Load(object sender, EventArgs e)
        {

        }        
    }
}