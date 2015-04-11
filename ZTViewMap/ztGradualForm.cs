/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 影像渐变效果。
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
    /// 影像渐变效果，可以依次渐变的显示多张影像，辅助分析影像变化。
    /// 在界面和功能上可能需要简化。
    /// </summary>
    public partial class ztGradualForm : DevComponents.DotNetBar.Office2007Form
    {        
        private ztMdiChild pSubChild;           // 地图对象
        private soLayer objLayer;               
        private bool bChange;
        private int iSwitch=1;
        private bool bStop;
        private int iSwitchStep = 1;            // 快速渐变的次数。

        /// <summary>
        /// 构造方法，需要传入地图窗口
        /// </summary>
        /// <param name="mdichild">地图窗口</param>
        public ztGradualForm(ztMdiChild mdichild)
        {
            pSubChild = mdichild;  
            InitializeComponent();
        }

        // 拖动渐变的滑块时
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            switchtimer.Stop();
            trackBartimer.Stop();
            chkAuto.Checked = false;
            btnSwitch.Text = "开始渐变(&S)";
            bStop = false;

            labelProgress.Text = trackBar.Value.ToString() + "%";
            
            RasterOpaque();
            if (trackBar.Value == 0)
            {
                chkAuto.Checked = false;
            }
            bChange = true;
        }

        // 影像透明，根据滑块的数值设置影像的透明度。
        // 设置找到的第一个图层。
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

        // 是否自动渐变。
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
                    btnSwitch.Text = "开始渐变(&S)";
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
                buttonX1.Text = "暂停(&P)";
            }

        }

        // 自动渐变的时间发生器
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

        // 手动快速渐变的时间发生器
        private void switchtimer_Tick(object sender, EventArgs e)
        {

            soLayers objLayers = pSubChild.SuperMap.Layers;
            if (objLayer == null)
                return;
            if (objLayers.Count < 2)
            {
                MessageBox.Show(this, "影像图层数量小于两个,请重试", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        
        // 自动渐变暂停按钮
        private void buttonX1_Click(object sender, EventArgs e)
        {
            switchtimer.Stop();
            bStop = false;
            if (buttonX1.Text == "暂停(&P)")
            {
                buttonX1.Text = "开始(&S)";
                trackBartimer.Stop();
                
            }
            else
            {
                buttonX1.Text = "暂停(&P)";
                trackBartimer.Start();
            }
        }

        // 快速手工渐变。
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (bStop == false)
            {
                switchtimer.Start();
                trackBartimer.Stop();
                bStop = true;
                btnSwitch.Text = "停止渐变(&S)";
            }
            else
            {
                switchtimer.Stop();
                bStop = false;
                iSwitchStep = 1;
                btnSwitch.Text = "开始渐变(&S)";
            }
        }

        // 窗口关闭时，恢复影像的透明度。
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