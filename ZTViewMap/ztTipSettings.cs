/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * tip 设置，选择数据层和显示的字段。
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
using System.Runtime.InteropServices;
using DevComponents.DotNetBar;
using SuperMapLib;

using ZTDialog;

namespace ZTViewMap
{
    /// <summary>
    /// tip 设置
    /// </summary>
    public partial class ztTipSettings : Office2007Form
    {
        private ztMapClass pMapClass;    // 此设置对应的地图窗口．
        private soLayers objLyrs;        // 地图窗口当前的层
        
        public ztTipSettings(ztMapClass mdichild)
        {
            pMapClass = mdichild;
            InitializeComponent();
        }

        /// <summary>
        /// 窗口装载的时候添加图层信息．
        /// </summary>        
        private void TipSettings_Load(object sender, EventArgs e)
        {
            if (layerList_fillMapLayers() != true)
            {
                ztMessageBox.Messagebox("提取地图窗口图层失败．", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();                
                return;
            }
        }
                
        
        /// <summary>
        /// 根据传入的子窗口添加图层列表． 
        /// </summary>
        /// <returns>返回是否添加成功．</returns>
        private bool layerList_fillMapLayers()
        {
            string strItemObject;
            try
            {
                objLyrs = pMapClass.SuperMap.Layers;
                if (objLyrs != null)
                {
                    for (int iLyr = 0; iLyr < objLyrs.Count; iLyr++)
                    {
                        soLayer ly = objLyrs[iLyr + 1];
                        if (ly != null)
                        {
                            strItemObject = ly.Name;
                            soDataset dt = ly.Dataset;
                            if (dt != null)
                            {
                                if (dt.Type == seDatasetType.scdPoint || dt.Type == seDatasetType.scdLine || dt.Type == seDatasetType.scdRegion)
                                {
                                    layerList.Items.Add(strItemObject);
                                }

                                Marshal.ReleaseComObject(dt);
                            }

                            // 加入第一层的时候，同时更新字段列表，
                            if (iLyr == 0)
                                fildlist_fillLayerFild(strItemObject);

                            Marshal.ReleaseComObject(ly);
                        }                    
                    }
                }
                if (layerList.Items.Count > 0)
                {
                    layerList.SelectedIndex = 0;
                    return true;
                }
                else
                {                    
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void fildlist_fillLayerFild(string layername)
        {
            soLayer objL;
            soDataset objDt;
            soDatasetVector objDv;

            try
            {
                fieldList.Items.Clear();

                // 提取指定层的字段，加入列表．
                objL = objLyrs[layername];
                objDt = objL.Dataset;

                if (objDt.Type == seDatasetType.scdImage)
                    return;
                objDv = (soDatasetVector)objDt;
                                
                if (objDv != null)
                {
                    for (int i = 1; i <= objDv.FieldCount; i++)
                    {
                        fieldList.Items.Add(objDv.GetFieldInfo(i).Name);
                    }
                    fieldList.SelectedIndex = 0;
                    Marshal.ReleaseComObject(objDv);
                }
                Marshal.ReleaseComObject(objDt);
                Marshal.ReleaseComObject(objL);
            }
            catch { }
        }

        // 选择了图层后，将图层字段列出来。
        private void layerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCurrentLayer;

            strCurrentLayer = layerList.SelectedItem.ToString();
            fildlist_fillLayerFild(strCurrentLayer);
        }

        // 设置字段的列表。
        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] fieldListArray = new string[fieldList.CheckedItems.Count];

                for (int iItem = 0; iItem < fieldList.CheckedItems.Count; iItem++)
                {
                    fieldListArray[iItem] = fieldList.CheckedItems[iItem].ToString();
                }

                pMapClass.MapTip_Setlayer(layerList.Text, fieldListArray);
                this.Close();
            }
            catch
            {
                ztMessageBox.Messagebox("还没有进行设置","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Marshal.ReleaseComObject(objLyrs);
            }
            catch
            {
            }
            finally
            {
                this.Close();
            }           
        }
    }
}