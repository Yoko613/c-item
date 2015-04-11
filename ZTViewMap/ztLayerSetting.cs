using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTViewMap
{
    /// <summary>
    /// 层的一些设置，现在还比较简单，需要改的还很多。
    /// </summary>
    public partial class ztLayerSetting : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperMap pMainSuperMap;                       // supermap

        public ztLayerSetting(AxSuperMap super)
        {
            InitializeComponent();
            
            pMainSuperMap = super;
        }

        private void ztLayerSetting_Load(object sender, EventArgs e)
        {
            chkNOvalueTransparent.Checked = true;
            if(pMainSuperMap != null) 
            {
                soLayers objLayers = pMainSuperMap.Layers;
                if (objLayers != null)
                {
                    for (int m = 1; m <= objLayers.Count; m++)
                    {
                        soLayer solyr = objLayers[m];
                        chkNOvalueTransparent.Checked = solyr.RasterBkTransparent;

                        if(chkNOvalueTransparent.Checked)
                            solyr.RasterBkColor = System.Convert.ToUInt32(ColorTranslator.ToOle(btnRasterBGColor.BackColor));

                        solyr.RasterBKTolerance = 20;
                        Marshal.ReleaseComObject(solyr);
                    }
                    Marshal.ReleaseComObject(objLayers);
                }

                // 刷新
                pMainSuperMap.Refresh();
            }
            btnRasterBGColor.BackColor = Color.White;
        }

        // 设置颜色
        private void btnRasterBGColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.AllowFullOpen = false;
            dlg.ShowHelp = true;
            dlg.Color = btnRasterBGColor.BackColor;

            if (dlg.ShowDialog() == DialogResult.OK)
                btnRasterBGColor.BackColor = dlg.Color;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (pMainSuperMap == null)
                return;

            soLayers objLayers = pMainSuperMap.Layers;
            if (objLayers != null)
            {
                for (int m = 1; m <= objLayers.Count; m++)
                {
                    soLayer solyr = objLayers[m];
                    solyr.RasterBkColor = System.Convert.ToUInt32(ColorTranslator.ToOle(btnRasterBGColor.BackColor));
                    solyr.RasterBkTransparent = chkNOvalueTransparent.Checked;
                    solyr.RasterBKTolerance = 20;
                    Marshal.ReleaseComObject(solyr);
                }
                Marshal.ReleaseComObject(objLayers);
            }

            // 刷新
            pMainSuperMap.Refresh();
            DialogResult = DialogResult.OK;
        }
        
    }
}