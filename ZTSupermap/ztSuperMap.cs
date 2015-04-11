/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * SuperMap 的常用操作
 * beizhan  2008/03
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

using AxSuperMapLib;
using SuperMapLib;
using System.Diagnostics;
using System.IO;
using ZTDialog;
using System.Drawing;
using System.Data;
using SuperTopoLib;
using AxSuperTopoLib;

namespace ZTSupermap
{
    /// <summary>
    /// SuperMap 的常用方法.
    /// </summary>
    public static class ztSuperMap
    {
        private static bool debug = false;

        public static bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="s"></param>
        public static void WriteLogToFile(string s)
        {
            if (!debug) return;
            FileStream fs = null;
            try { fs = new FileStream("c:\\ztmap.log", FileMode.Open); }
            catch { fs = null; }
            try { if (fs == null) fs = new FileStream("c:\\ztmap.log", FileMode.Create); }
            catch { fs = null; }
            if (fs == null) return;
            fs.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(fs);
            System.DateTime currentTime = System.DateTime.Now;
            sw.Write("[");
            sw.Write(currentTime.ToString());
            sw.Write("]");
            sw.Write(s);
            sw.Write("\r\n");
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 根据参数Win32颜色值获取到Uint类型的值
        /// </summary>
        /// <param name="color">Win32颜色值</param>
        /// <returns>返回Uint类型的值</returns>
        public static UInt32 GetOleUintFromColor(Color color) 
        {
            return System.Convert.ToUInt32(ColorTranslator.ToOle(color));
        }

        #region 定位操作

        // 现在很多操作都存在在工作空间中定位数据集，或者在数据源中定位数据集，或者在工作空间中定位数据源的操作，请尽量使用这几个方法。
        // 注意：返回的东西要释放。

        #region 在一个 workspace 中查找指定名称的 soDataset
        /// <summary>
        /// 在一个 workspace 中查找指定名称的 soDataset
        /// 注意多个数据源中可能会有同名的数据集，所以参数 datasourcename == null 的时候执行模糊查询，返回第一个找到的数据集。
        /// </summary>
        /// <param name="datasetname">数据集名</param>
        /// <param name="superworkspaceDTS">工作空间</param>
        /// <param name="datasourcealias">数据源别名，如果指定为 null, 执行模糊查询</param>
        /// <returns>返回找到的数据集，没有找到返回 NULL</returns>
        public static soDataset getDatasetFromWorkspaceByName(string datasetname, AxSuperWorkspace superworkspaceDTS, string datasourcealias)
        {
            // 找出指定层
            soDataset objDtSet = null;

            // 参数判断
            if (superworkspaceDTS == null)
                return objDtSet;

            soDataSources objDatasources = superworkspaceDTS.Datasources;
            if (objDatasources != null)
            {
                soDataSource objDatasr;

                // 定位数据源
                // 如果不提供数据源名就找所有数据源下面第一个能匹配上的。
                if (datasourcealias != null)
                {
                    objDatasr = objDatasources[datasourcealias];
                    if (objDatasr != null)
                    {
                        objDtSet = getDatasetFromDataSourceByName(datasetname, objDatasr);
                        Marshal.ReleaseComObject(objDatasr);
                    }
                }
                else
                {
                    for (int i = 1; i <= objDatasources.Count; i++)
                    {
                        objDatasr = objDatasources[i];
                        if (objDatasr != null)
                        {
                            objDtSet = getDatasetFromDataSourceByName(datasetname, objDatasr);
                            if (objDtSet != null)
                                break;
                            ztSuperMap.ReleaseSmObject(objDtSet);
                            ztSuperMap.ReleaseSmObject(objDatasr);
                        }
                    }
                }
                Marshal.ReleaseComObject(objDatasources);
            }

            return objDtSet;
        }
        #endregion

        #region 在一个 DataSource 中查找指定名称的 soDataset
        /// <summary>
        /// 在一个 DataSource 中查找指定名称的 soDataset
        /// </summary>
        /// <param name="datasetname">数据集名</param>
        /// <param name="dataSource">数据源</param>
        /// <returns>返回找到的数据集，没有找到返回 null</returns>
        public static soDataset getDatasetFromDataSourceByName(string datasetname, soDataSource dataSource)
        {
            // 找出指定层
            soDataset objDtSet = null;
            int i;

            // 参数判断
            if (dataSource == null)
                return objDtSet;

            soDatasets objDatasets = dataSource.Datasets;

            if (objDatasets != null)
            {
                for (i = 1; i <= objDatasets.Count; i++)
                {
                    objDtSet = objDatasets[i];
                    if (objDtSet != null)
                    {
                        if (objDtSet.Name.ToUpper() == datasetname.ToUpper())
                        {
                            break;
                        }
                        // 这里的释放推荐仔细琢磨一下，当循环保持的时候可以保证释放每一个com对象，但是 break 的时候对象还是有值的，呵呵
                        // beizhan 2007-06
                        Marshal.ReleaseComObject(objDtSet);
                    }
                }

                if (i > objDatasets.Count)
                    objDtSet = null;

                Marshal.ReleaseComObject(objDatasets);
            }

            return objDtSet;
        }
        #endregion

        #region 在工作空间中定位数据源

        /// <summary>
        /// 根据地图中已加载图层获取数据源别名
        /// </summary>
        /// <param name="supermap">地图控件对象</param>
        /// <returns>失败返回Null</returns>
        public static List<string> getDataSourceFormSuperMap(AxSuperMap supermap)
        {
            if (supermap == null) return null;

            soLayers layers = null;
            List<string> lst_strsourcenames = new List<string>();

            try
            {
                layers = supermap.Layers;
                for (int i = 1; i <= layers.Count; i++)
                {
                    soLayer layer = layers[i];
                    if (layer == null) continue;
                    soDataset dset = layer.Dataset;
                    if (dset == null)
                    {
                        ReleaseSmObject(layer);
                        layer = null;
                        continue;
                    }
                    string strdatasourcealias = string.Empty;
                    strdatasourcealias = dset.DataSourceAlias;
                    ReleaseSmObject(dset);
                    dset = null;
                    ReleaseSmObject(layer);
                    layer = null;
                    if (string.IsNullOrEmpty(strdatasourcealias)) continue;
                    if (lst_strsourcenames.Contains(strdatasourcealias)) continue;
                    lst_strsourcenames.Add(strdatasourcealias);
                }
                return lst_strsourcenames;
            }
            catch
            {
                return null;
            }
            finally
            {
                ReleaseSmObject(layers);
                layers = null;
            }
        }

        /// <summary>
        /// 在工作空间中定位数据源
        /// </summary>
        /// <param name="superwksp"></param>
        /// <param name="datasourcealias"></param>
        /// <returns>如果没有找到，则返回 null</returns>
        public static soDataSource getDataSourceFromWorkspaceByName(AxSuperWorkspace superwksp, string datasourcealias)
        {
            //soDataSource objDatasr = null;

            soDataSources objDatasources = null;

            try
            {
                // 参数判断
                if (superwksp == null)
                    return null;

                objDatasources = superwksp.Datasources;

                if (objDatasources != null)
                {
                    for (int i = 1; i <= objDatasources.Count; i++)
                    {
                        soDataSource objDS = null;

                        objDS = objDatasources[i];

                        if (objDS.Alias.ToUpper() == datasourcealias.ToUpper())
                        {
                            return objDS;
                        }

                        ReleaseSmObject(objDS);
                    }

                    return null;
                }
                else
                { return null; }
            }
            catch
            { return null; }
            finally
            {
                ReleaseSmObject(objDatasources);
            }
        }

        #endregion

        #region 在一个 Supermap 中查找指定名称的 soDataset
        /// <summary>
        /// 在一个 Supermap 中查找指定名称的 soDataset
        /// </summary>
        /// <param name="datasetname">数据集名称</param>
        /// <param name="objsupermap">supermap</param>
        /// <returns>返回找到的数据集，没有找到返回 null</returns>
        public static soDataset getDatasetFromSuperMap(string datasetname, AxSuperMap objsupermap)
        {
            // 找出指定层
            soDataset objDtSet = null;

            if (objsupermap == null)
                return objDtSet;

            soLayers objLayers = objsupermap.Layers;
            if (objLayers != null)
            {
                for (int m = 1; m <= objLayers.Count; m++)
                {
                    soLayer solyr = objLayers[m];
                    soDataset lyrdataset = solyr.Dataset;

                    if (lyrdataset.Name == datasetname)
                    {
                        objDtSet = lyrdataset;
                        break;
                    }
                    Marshal.ReleaseComObject(lyrdataset);
                    Marshal.ReleaseComObject(solyr);
                }
                Marshal.ReleaseComObject(objLayers);
            }

            return objDtSet;
        }
        #endregion 

        #endregion

        #region 判断操作

        #region 判断一个数据集在指定的 Supermap 控件中是否已经打开
        /// <summary>
        /// 判断一个数据集在指定的 Supermap 控件中是否已经打开
        /// </summary>
        /// <param name="datasetname">指定要查找的数据集名,注意这里要指定全名</param>
        /// <param name="objSuperMap">指定要查找的SuperMap控件</param>
        /// <returns>返回是否已经打开</returns>
        public static bool DatasetIsOpenInSuperMap(string datasetname, AxSuperMapLib.AxSuperMap objSuperMap)
        {
            bool bHasDatasetName = false;

            if (objSuperMap == null)
                return bHasDatasetName;

            soLayers objLayers = objSuperMap.Layers;
            if (objLayers != null)
            {
                for (int m = 1; m <= objLayers.Count; m++)
                {
                    soLayer solyr = objLayers[m];
                    if (solyr.Name == datasetname)
                    {
                        Marshal.ReleaseComObject(solyr);
                        bHasDatasetName = true;
                        break;
                    }
                    Marshal.ReleaseComObject(solyr);
                    solyr = null;
                }
                Marshal.ReleaseComObject(objLayers);
                objLayers = null;
            }

            return bHasDatasetName;
        }
        #endregion

        #region 判断工作空间内是否存在指定的地图
        /// <summary>
        /// 判断工作空间内是否存在指定的地图
        /// </summary>
        /// <param name="mapname">地图名</param>
        /// <param name="superworkspace">指定工作空间</param>
        /// <returns>是否存在该名称的地图</returns>
        public static bool MapIsIncludedInWorkspace(string mapname, AxSuperWorkspace superworkspace)
        {
            if (superworkspace == null)
                return false;

            bool bHasMap = false;
            soMaps objMaps = superworkspace.Maps;
            if (objMaps != null)
            {
                for (int i = 1; i <= objMaps.Count; i++)
                {
                    if (objMaps[i].ToString() == mapname)
                    {
                        bHasMap = true;
                        break;
                    }
                }
                Marshal.ReleaseComObject(objMaps);
                objMaps = null;
            }

            return bHasMap;
        }
        #endregion

        #region 判断当前Supermap是否存在该图层
        /// <summary>
        /// 判断当前Supermap是否存在该图层
        /// </summary>
        /// <param name="LayerName">图层名称</param>
        /// <param name="AxobjSuperMap">SuperMap地图</param>
        /// <returns>判断是否存在此图层</returns>
        public static bool LayerIsIncludedInSuperMap(string LayerName, AxSuperMap AxobjSuperMap)
        {
            soLayers objLayers = AxobjSuperMap.Layers;
            bool bExit = false;
            if (objLayers == null)
                return false;

            for (int i = 1; i <= objLayers.Count; i++)
            {
                soLayer objLayer = objLayers[i];

                if (objLayer.Name == LayerName)
                {
                    ReleaseSmObject(objLayer);
                    bExit = true;
                    break;
                }

                ReleaseSmObject(objLayer);
            }

            ReleaseSmObject(objLayers);
            return bExit;
        }
        #endregion 

        #endregion

        #region 控制操作

        #region 在 SuperMap 中追加打开数据集。

        public static bool RasterDatasetOpenWithPyramidPrompt(soDataset sodst)
        {
            bool isOpen = true;

            // 如果是影像，判断是否有金字塔，创建金字塔。
            if (sodst.Type == seDatasetType.scdImage)
            {
                soDatasetRaster sorst = (soDatasetRaster)sodst;
                if (sorst != null)
                {
                    int lwd = sorst.PixelWidth;
                    int lht = sorst.PixelHeight;

                    // 先判断影像的大小，小于 3000 就不创建金字塔
                    if ((lwd > 3000) || (lht > 3000))
                    {
                        soDatasetInfo inf = sorst.GetDatasetInfo();
                        if (inf != null)
                        {
                            bool pyrmd = inf.get_Options(seDatasetOption.scoPyramid);
                            if (pyrmd != true)
                            {
                                string sPropmt = "数据集 " + sodst.Name + " 太大，为了加快显示速度是否创建影像金字塔？";
                                DialogResult rest = ZTDialog.ztMessageBox.Messagebox(sPropmt, "提示", MessageBoxButtons.YesNoCancel);
                                if (rest == DialogResult.Yes)
                                {
                                    if (sorst.BuildPyramid(true) != true)
                                        isOpen = false;
                                }
                                else if (rest == DialogResult.Cancel)
                                {
                                    isOpen = false;
                                }
                            }
                            ReleaseSmObject(inf);
                        }
                    }

                    sorst = null;
                }
            }

            // 是否需要打开。
            return isOpen;
        }

        /// <summary>
        /// 在 SuperMap 中追加打开数据集。
        /// 注意，在打开数据集之前，需要先判断该数据集是否已经在地图中打开。
        /// </summary>
        /// <param name="sodst">要打开的数据集</param>
        /// <param name="objSuperMap">指定 supermap</param>
        /// <param name="isrefresh">是否刷新地图，刷新地图包括指定选择状态和全屏</param>
        /// <returns>返回打开的层</returns>
        public static soLayer OpenDataInSuperMap(soDataset sodst, AxSuperMapLib.AxSuperMap objSuperMap, bool isrefresh)
        {
            if (objSuperMap == null) return null;
            if (sodst == null) return null;

            // 提示创建金字塔
            if (RasterDatasetOpenWithPyramidPrompt(sodst) != true)
                return null;

            soLayers soAddLayers = null;
            soLayer objLyr = null;

            try
            {
                soAddLayers = objSuperMap.Layers;
                if (soAddLayers == null) return null;
                objLyr = soAddLayers.AddDataset(sodst, true);
                if (isrefresh)
                {
                    objSuperMap.Action = seAction.scaSelectEx;
                    objSuperMap.Refresh();
                }
                return objLyr;    
            }
            catch
            {
                return null;
            }
            finally 
            {
                ReleaseSmObject(soAddLayers);
            }
        }

        /// <summary>
        /// 在 SuperMap 中追加打开数据集。
        /// 注意，在打开数据集之前，需要先判断该数据集是否已经在地图中打开。
        /// </summary>
        /// <param name="sodst">要打开的数据集</param>
        /// <param name="objSuperMap">指定 supermap</param>
        /// <param name="isrefresh">是否刷新地图，刷新地图包括指定选择状态和全屏</param>
        /// <returns>成功返回True</returns>
        public static bool OpenDataInSuperMap(soDataset sodst, AxSuperMapLib.AxSuperMap objSuperMap, bool isrefresh, bool bflag)
        {
            if (objSuperMap == null) return false;
            if (sodst == null) return false;

            // 提示创建金字塔
            // 提示创建金字塔
            if (RasterDatasetOpenWithPyramidPrompt(sodst) != true)
                return false;

            soLayers soAddLayers = null;
            soLayer objLyr = null;

            try
            {
                soAddLayers = objSuperMap.Layers;
                if (soAddLayers == null) return false;
                objLyr = soAddLayers.AddDataset(sodst, true);
                if (isrefresh)
                {
                    objSuperMap.Action = seAction.scaSelectEx;
                    objSuperMap.Refresh();
                }
                if (objLyr == null) return false;
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objLyr);
                ReleaseSmObject(soAddLayers);
            }
        }
        #endregion 

        #region  在 Supermap 中打开地图 (通过地图名称)
        /// <summary>
        /// 在 Supermap 中打开地图。
        /// </summary>
        /// <param name="mapname">地图名</param>
        /// <param name="objsupermap">supermap</param>
        /// <param name="isEntire">是否刷新，刷新包括全屏和设置选择状态</param>
        /// <returns>是否打开地图</returns>
        public static bool OpenMapInSupermap(string mapname, AxSuperMap objsupermap, bool isEntire)
        {
            if (objsupermap == null)
                return false;
            //if (!System.Runtime.InteropServices.Marshal.IsComObject(objsupermap)) return false;

            // supermap 这里挺奇怪的，如果是打开地图，窗口中原来的图层都要清掉
            // 地图上可以再添加数据集。
            soLayers objLayers = objsupermap.Layers;
            if (objLayers == null) return false;
            if (objLayers.Count != 0)
            {
                objLayers.RemoveAll();
            }
            Marshal.ReleaseComObject(objLayers);

            bool bMap = objsupermap.OpenMap(mapname);

            if (bMap)
            {
                if (isEntire)
                {
                    objsupermap.ViewEntire();
                }

                objsupermap.Refresh();
                objsupermap.Action = seAction.scaSelectEx;
            }

            return bMap;
        }
        #endregion 

        #region 在 Supermap 中打开地图 (通过SuperMap导出的XML字符串)
        /// <summary>
        /// 在 Supermap 中打开地图。
        /// </summary>
        /// <param name="xmlstring">地图名</param>
        /// <param name="objsupermap">supermap</param>
        /// <param name="isrefresh">是否刷新，刷新包括全屏和设置选择状态</param>
        /// <returns>是否打开地图</returns>
        public static bool OpenXmlMapInSupermap(string xmlstring, AxSuperMap objsupermap, bool isEntire)
        {
            if (objsupermap == null)
                return false;
            //if (!System.Runtime.InteropServices.Marshal.IsComObject(objsupermap)) return false;

            // supermap 这里挺奇怪的，如果是打开地图，窗口中原来的图层都要清掉
            // 地图上可以再添加数据集。
            soLayers objLayers = objsupermap.Layers;
            if (objLayers == null) return false;
            if (objLayers.Count != 0)
            {
                objLayers.RemoveAll();
            }
            Marshal.ReleaseComObject(objLayers);
            objsupermap.Refresh();

            
            bool bMap = objsupermap.FromXML(xmlstring,false);

            if (bMap)
            {
                if (isEntire)
                {
                    objsupermap.ViewEntire();
                }

                objsupermap.Refresh();
                objsupermap.Action = seAction.scaSelectEx;
            }

            return bMap;
        }
        #endregion 

        #region 删除跟踪层上某种指定名称的临时对象
        /// <summary>
        /// 删除跟踪层上某种指定名称的临时对象。
        /// 关于跟踪层：
        /// 跟踪图层位于地图窗口最顶层，是单独显示的，刷新时不需要整个地图刷新，所以速度快。
        /// 可以加入临时对象，临时对象可以有命名，如果有多个重名的对象，在remove 的时候先删除最先加入的。
        /// </summary>
        /// <param name="objsupermap">要删除临时对象的地图</param>
        /// <param name="evtname">要删除的临时对象名称。</param>
        public static void RemoveTrackEventByName(AxSuperMap objsupermap, string evtname)
        {
            if (objsupermap == null) return;
            try
            {
                soTrackingLayer objTrackingLayer = objsupermap.TrackingLayer;
                //删除的时候只要还有这一类临时对象就返回 true,当这类对象删除完后返回 false
                bool bRemoved = true;
                while (bRemoved)
                {
                    bRemoved = objTrackingLayer.RemoveEvent(evtname);
                }

                objTrackingLayer.Refresh();
                Marshal.ReleaseComObject(objTrackingLayer);
            }
            catch { }
        }
        #endregion

        #region 根据指定的显示风格和指定的事件对象名称在跟踪层上符号化显示一个元素集中的元素
        /// <summary>
        /// 根据指定的显示风格和指定的事件对象名称在跟踪层上符号化显示一个元素集中的元素
        /// </summary>
        /// <param name="objsupermap">supermap 对象</param>
        /// <param name="recordst">选择集</param>
        /// <param name="objstyle">显示的风格</param>
        /// <param name="evtname">事件对象的名称</param>
        public static void TrackEventSymbol(AxSuperMap objsupermap, soRecordset recordst, soStyle objstyle, string evtname)
        {
            if (objsupermap == null) return;
            if (recordst == null) return;
            if (recordst.RecordCount < 1) return;

            try
            {
                recordst.MoveFirst();
                while (!recordst.IsEOF())
                {
                    soGeometry objGeometry;
                    objGeometry = recordst.GetGeometry();
                    if (objGeometry != null)
                    {
                        soTrackingLayer objTrackingLayer = objsupermap.TrackingLayer;
                        if (objTrackingLayer != null)
                        {
                            objTrackingLayer.AddEvent(objGeometry, objstyle, evtname);
                            Marshal.ReleaseComObject(objTrackingLayer);
                        }
                        Marshal.ReleaseComObject(objGeometry);
                        objGeometry = null;
                    }
                    recordst.MoveNext();
                }
            }
            catch { }
        }
        #endregion

        #region  创建SDB到制定的工作空间
        /// <summary>
        /// 功能描述:创建SDB数据到工作空间
        /// </summary>
        /// <param name="m_AxSMWorkSpace">工作空间</param>
        /// <param name="strFileName">保存文件名称</param>
        /// <param name="strAlias">导出SDB数据别名</param>
        /// <returns>返回创建的数据源</returns>
        public static soDataSource CreataSDBDataSource(AxSuperMapLib.AxSuperWorkspace m_AxSMWorkSpace, string strFileName, string strAlias)
        {
            try
            {
                soErrorClass objErr = new soErrorClass();
                System.Diagnostics.Debug.Assert(m_AxSMWorkSpace != null, "操作的工作空间不能为空");
                System.Diagnostics.Debug.Assert(strFileName != "", "导出的文件名称不能为空");
                if (File.Exists(strFileName + ".sdb"))
                {
                    File.Delete(strFileName);
                }
                soDataSource objExpDataSource = m_AxSMWorkSpace.CreateDataSource(strFileName, strAlias, seEngineType.sceSDBPlus, false, false, false, "");
                if (objExpDataSource == null)
                {
                    return null;
                }
                else
                {
                    return objExpDataSource;
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message);
                return null;
            }
        }

        #region 将数据记录集转换为数据集到制定的数据源中
        /// <summary>
        /// 功能描述:将数据记录集转换为数据集到制定的数据源中
        /// </summary>
        /// <param name="objSrcRecordSet">被转换数据记录集</param>
        /// <param name="objConvertDataSource">转换目标数据源</param>
        /// <param name="strDataSetName">转换目标数据集名称</param>
        /// <param name="bShowPrograss">是否显示转换进度条</param>
        /// <returns>返回转换后的矢量数据集</returns>
        public static soDatasetVector ConvertRescordSetToDataSet(soRecordset objSrcRecordSet,soDataSource objConvertDataSource,string strDataSetName,bool bShowPrograss)
        {
            try
            {
                if (objSrcRecordSet == null) return null;

                soDatasetVector objConvertRes;

                objConvertRes = objConvertDataSource.RecordsetToDataset(objSrcRecordSet, strDataSetName, bShowPrograss);

                return objConvertRes;
            }
            catch (Exception Err)
            { 
                return null;
            }
        }
        #endregion

        #endregion

        #region 将一个制定文件导入数据源

        /// <summary>
        /// 将一个制定文件导入数据源
        /// </summary>
        /// <param name="objImportDataSource">导入目标数据源</param>
        /// <param name="strImpFileName">导入文件名称</param>
        /// <param name="ImpResDataSetName">导入结果数据集名称</param>
        /// <param name="objImpFileType">导入文件类型</param>
        /// <returns></returns>
        public static bool ImportFile(soDataSource objImportDataSource, string strImpFileName, string ImpResDataSetName, seFileType objImpFileType, seDatasetType objDataSetType, bool bShowProgress)
        {
            soDataPump objDataPump = null;
            soImportParams objImpParams = null;
            soErrorClass objErr = new soErrorClass();

            try
            {
                objDataPump = objImportDataSource.DataPump;
                objImpParams = objDataPump.DataImportParams;

                objImpParams.FileName = strImpFileName;
                objImpParams.FileType = objImpFileType;
                objImpParams.ShowProgress = bShowProgress;

                //根据要导入的数据集类型进行数据集名称赋值
                switch (objDataSetType)
                {
                    case seDatasetType.scdCAD:
                        objImpParams.ImportAsCADDataset = true;
                        objImpParams.DatasetCAD = ImpResDataSetName;
                        break;
                    case seDatasetType.scdLine:
                        objImpParams.ImportAsCADDataset = false;
                        objImpParams.DatasetLine = ImpResDataSetName;
                        break;
                    case seDatasetType.scdPoint:
                        objImpParams.ImportAsCADDataset = false;
                        objImpParams.DatasetPoint = ImpResDataSetName;
                        break;
                    case seDatasetType.scdRegion:
                        objImpParams.ImportAsCADDataset = false;
                        objImpParams.DatasetRegion = ImpResDataSetName;
                        break;
                    case seDatasetType.scdTabular:
                        objImpParams.ImportAsCADDataset = false;
                        objImpParams.DatasetNoneGeometry = ImpResDataSetName;
                        break;
                    case seDatasetType.scdText:
                        objImpParams.ImportAsCADDataset = false;
                        objImpParams.DatasetText = ImpResDataSetName;
                        break;
                    case seDatasetType.scdImage:
                        objImpParams.BuildPyramid = false;
                        objImpParams.DatasetImage = ImpResDataSetName;
                        break;
                    default:
                        objImpParams.ImportAsCADDataset = false;
                        objImpParams.DatasetLine = ImpResDataSetName;
                        objImpParams.DatasetPoint = ImpResDataSetName;
                        objImpParams.DatasetRegion = ImpResDataSetName;
                        objImpParams.DatasetText = ImpResDataSetName;
                        objImpParams.DatasetNoneGeometry = ImpResDataSetName;
                        objImpParams.DatasetDBF = ImpResDataSetName;

                        break;
                }

                //导入投影信息
                objImportDataSource.PJCoordSys = objDataPump.RetrievePJCoordSys(strImpFileName, objImpFileType);

                if (objDataPump.Import())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                if (objDataPump != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDataPump);
                }

                if (objImpParams != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objImpParams);
                }
            }
        }

        #endregion

        #region 切割一个制定的数据集并将切割结果放入一个新的数据集

        /// <summary>
        /// 功能描述:切割一个制定的数据集的数据到另外一个制定的数据集
        /// </summary>
        /// <param name="SmDsVectorByOverLay">被切割的数据集</param>
        /// <param name="objOverLayRegion">切割多边形</param>
        /// <param name="strDsVectorStone">切割数据存放的数据集名称</param>
        /// <param name="objSmDataSourceStone">切割数据存放的数据源</param>
        /// <returns>返回切割数据存放的数据集</returns>
        public static soDatasetVector GetOverlayAnalystClipDataVector(soDatasetVector objSmDsVectorByOverLay, object objSmOverLay
                        , string strDsVectorStone, soDataSource objSmDataSourceStone)
        {
            SuperMapLib.soOverlayAnalyst m_soOverlayAnalyst = new SuperMapLib.soOverlayAnalystClass();

            try
            {
                soDatasetVector objDataStoneVector = (soDatasetVector)objSmDataSourceStone.CreateDatasetFrom(strDsVectorStone, objSmDsVectorByOverLay);

                if (objDataStoneVector == null)
                {
                    return null;
                }

                bool bclip1 = m_soOverlayAnalyst.Clip(objSmDsVectorByOverLay, objSmOverLay, objDataStoneVector);

                if (bclip1 == true)
                {
                    return objDataStoneVector;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            {
                if (m_soOverlayAnalyst != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_soOverlayAnalyst);
                    m_soOverlayAnalyst = null;
                }
            }
        }

        #endregion 

        #region 擦除一个制定的数据集并将切割结果放入一个新的数据集

        /// <summary>
        /// 功能描述:擦除一个制定的数据集的数据到另外一个制定的数据集
        /// </summary>
        /// <param name="SmDsVectorByOverLay">被擦除的数据集</param>
        /// <param name="objOverLayRegion">擦除多边形</param>
        /// <param name="strDsVectorStone">擦除数据存放的数据集名称</param>
        /// <param name="objSmDataSourceStone">擦除数据存放的数据源</param>
        /// <returns>返回擦除数据存放的数据集</returns>
        public static soDatasetVector GetOverlayAnalystEraseDataVector(soDatasetVector objSmDsVectorByOverLay, object objSmOverLay
                        , string strDsVectorStone, soDataSource objSmDataSourceStone)
        {
            SuperMapLib.soOverlayAnalyst m_soOverlayAnalyst = new SuperMapLib.soOverlayAnalystClass();

            try
            {
                soDatasetVector objDataStoneVector = (soDatasetVector)objSmDataSourceStone.CreateDatasetFrom(strDsVectorStone, objSmDsVectorByOverLay);

                if (objDataStoneVector == null)
                {
                    return null;
                }

                bool bclip1 = m_soOverlayAnalyst.Erase(objSmDsVectorByOverLay, objSmOverLay, objDataStoneVector);

                if (bclip1 == true)
                {
                    return objDataStoneVector;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            {
                if (m_soOverlayAnalyst != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_soOverlayAnalyst);
                    m_soOverlayAnalyst = null;
                }
            }
        }

        #endregion 

        #region 对两个数据集做求交叠加分析(不指定附加字段)
        /// <summary>
        /// 功能描述:对两个数据集做求交叠加分析(不指定附加字段)
        /// </summary>
        /// <param name="objAxSuperWorkSpace">工作空间</param>
        /// <param name="sttInDataset_Name">被叠加分析的对象</param>
        /// <param name="strIntersectDataset_Name">用于叠加分析的对象 类型是面数据集</param>
        /// <param name="strOutDataset_Name">存放结果的数据集</param>
        /// <param name="strAlias">数据源别名</param>
        /// <param name="bJoinAttribute">结果数据集的属性表是否连接参与运算的两个数据集</param>
        /// <param name="bShowProgress">是否显示进度</param>
        /// <param name="dTolerance">叠加分析的容限值</param>
        /// <returns>返回叠加分析是否成功</returns>
        public static bool GetOverlayAnalyst_Intersect(AxSuperWorkspace objAxSuperWorkSpace,string sttInDataset_Name, string strIntersectDataset_Name
                , string strOutDataset_Name, string strAlias, bool bJoinAttribute, bool bShowProgress, double dTolerance)
        {
            soDataset objInDataset = null;
            soDataset objIntersectDataset = null;
            soDataSource objDs = null;
            soDataset objOutDataset = null;
            soOverlayAnalyst objOverlayAnalyst = null;

            try
            {
                objInDataset = getDatasetFromWorkspaceByName(sttInDataset_Name, objAxSuperWorkSpace, strAlias);
                objIntersectDataset = getDatasetFromWorkspaceByName(strIntersectDataset_Name, objAxSuperWorkSpace, strAlias);

                #region 判断是否可以进行求交叠加分析
                if (objInDataset == null) return false;
                if (objIntersectDataset == null) return false;

                if (objInDataset.Type != seDatasetType.scdPoint && objInDataset.Type != seDatasetType.scdLine
                    && objInDataset.Type != seDatasetType.scdRegion && objInDataset.Type != seDatasetType.scdLineM
                    && objInDataset.Type != seDatasetType.scdCAD)
                {
                    return false;
                }

                if (objIntersectDataset.Type != seDatasetType.scdRegion)
                {
                    return false;
                }
                #endregion

                objDs = GetDataSource(objAxSuperWorkSpace, strAlias);

                objOutDataset = getDatasetFromWorkspaceByName(strOutDataset_Name, objAxSuperWorkSpace, strAlias);

                #region 判断是否已经存在结果数据集
                if (objOutDataset != null)
                {
                    objDs.DeleteDataset(strOutDataset_Name);
                }
                #endregion

                objOverlayAnalyst = new SuperMapLib.soOverlayAnalystClass();

                objOutDataset = objDs.CreateDataset(strOutDataset_Name, objInDataset.Type, seDatasetOption.scoDefault, null);

                if (objOutDataset == null) return false;

                objOverlayAnalyst.ShowProgress = bShowProgress;
                objOverlayAnalyst.Tolerance = dTolerance;
                bool bSuccess = objOverlayAnalyst.Intersect(objInDataset as soDatasetVector, objIntersectDataset as soDatasetVector, objOutDataset as soDatasetVector, bJoinAttribute);

                return bSuccess;
            }
            catch
            { return false; }
            finally
            {
                ReleaseSmObject(objInDataset);
                ReleaseSmObject(objIntersectDataset);
                ReleaseSmObject(objDs);
                ReleaseSmObject(objOutDataset);
                ReleaseSmObject(objOverlayAnalyst);
            }
        }
        #endregion 

        #region 对两个数据集做求交叠加分析(指定附加字段)
        /// <summary>
        /// 功能描述:对两个数据集做求交叠加分析(指定附加字段)
        /// </summary>
        /// <param name="objAxSuperWorkSpace">工作空间</param>
        /// <param name="sttInDataset_Name">被叠加分析的对象</param>
        /// <param name="strIntersectDataset_Name">用于叠加分析的对象 类型是面数据集</param>
        /// <param name="strOutDataset_Name">存放结果的数据集</param>
        /// <param name="strAlias">数据源别名</param>
        /// <param name="objInDataset_Fields">被叠加分析的对象附加字段列表</param>
        /// <param name="objOutDataset_Fields">用于叠加分析的对象附加字段列表</param>
        /// <param name="bShowProgress"是否显示进度>是否显示进度</param>
        /// <param name="dTolerance">叠加分析的容限值</param>
        /// <returns>返回叠加分析是否成功</returns>
        public static bool GetOverlayAnalyst_Intersect(AxSuperWorkspace objAxSuperWorkSpace, string sttInDataset_Name, string strIntersectDataset_Name
            , string strOutDataset_Name, string strAlias, ArrayList objInDataset_Fields, ArrayList objIntersectDataset_Fields, bool bShowProgress, double dTolerance)
        {
            soDataset objInDataset = null;
            soDataset objIntersectDataset = null;
            soDataSource objDs = null;
            soDataset objOutDataset = null;
            soOverlayAnalyst objOverlayAnalyst = null;
            soStrings objInFieldNames = null;
            soStrings objOpFieldNames = null;

            try
            {
                objInDataset = getDatasetFromWorkspaceByName(sttInDataset_Name, objAxSuperWorkSpace, strAlias);
                objIntersectDataset = getDatasetFromWorkspaceByName(strIntersectDataset_Name, objAxSuperWorkSpace, strAlias);

                #region 判断是否可以进行求交叠加分析
                if (objInDataset == null) return false;
                if (objIntersectDataset == null) return false;

                if (objInDataset.Type != seDatasetType.scdPoint && objInDataset.Type != seDatasetType.scdLine
                    && objInDataset.Type != seDatasetType.scdRegion && objInDataset.Type != seDatasetType.scdLineM
                    && objInDataset.Type != seDatasetType.scdCAD)
                {
                    return false;
                }

                if (objIntersectDataset.Type != seDatasetType.scdRegion)
                {
                    return false;
                }
                #endregion

                objDs = GetDataSource(objAxSuperWorkSpace, strAlias);

                objOutDataset = getDatasetFromWorkspaceByName(strOutDataset_Name, objAxSuperWorkSpace, strAlias);

                #region 判断是否已经存在结果数据集
                if (objOutDataset != null)
                {
                    objDs.DeleteDataset(strOutDataset_Name);
                }
                #endregion

                objOverlayAnalyst = new SuperMapLib.soOverlayAnalystClass();

                objOutDataset = objDs.CreateDataset(strOutDataset_Name, objInDataset.Type, seDatasetOption.scoDefault, null);

                if (objOutDataset == null) return false;

                objOverlayAnalyst.ShowProgress = bShowProgress;
                objOverlayAnalyst.Tolerance = dTolerance;
                objInFieldNames = new soStringsClass();
                
                for (int i = 0; i < objInDataset_Fields.Count; i++)
                {
                    objInFieldNames.Add(objInDataset_Fields[i].ToString());
                }

                objOpFieldNames = new soStringsClass();

                for (int i = 0; i < objIntersectDataset_Fields.Count; i++)
                {
                    objOpFieldNames.Add(objIntersectDataset_Fields[i].ToString());
                }

                objOverlayAnalyst.OpFieldNames = objOpFieldNames;
                objOverlayAnalyst.InFieldNames = objInFieldNames;

                
                objOverlayAnalyst.Tolerance = dTolerance;
                bool bSuccess = objOverlayAnalyst.Intersect(objInDataset as soDatasetVector, objIntersectDataset as soDatasetVector, objOutDataset as soDatasetVector, false);

                return bSuccess;
            }
            catch
            { return false; }
            finally
            {
                ReleaseSmObject(objInDataset);
                ReleaseSmObject(objIntersectDataset);
                ReleaseSmObject(objDs);
                ReleaseSmObject(objOutDataset);
                ReleaseSmObject(objOverlayAnalyst);
                ReleaseSmObject(objInFieldNames);
                ReleaseSmObject(objOpFieldNames);
            }
        }
        #endregion

        #region 将一个制定文件导入数据源

        /// <summary>
        /// 功能描述:将一个制定文件导入数据源
        /// </summary>
        /// <param name="objImportDataSource">导入目标数据源</param>
        /// <param name="strImpFileName">导入文件名称</param>
        /// <param name="ImpResDataSetName">导入结果数据集名称</param>
        /// <param name="objImpFileType">导入文件类型</param>
        /// <returns></returns>
        public static bool ImportFile(AxSuperWorkspace AxSmWorkSpace, string strAlias, string strImpFileName, string ImpResDataSetName, seFileType objImpFileType)
        {
            return ImportFile(AxSmWorkSpace, strAlias, strImpFileName, ImpResDataSetName, objImpFileType, "", "IMAGE数据集", false, true);
        }
        public static bool ImportFile(AxSuperWorkspace AxSmWorkSpace, string strAlias, string strImpFileName, string ImpResDataSetName, seFileType objImpFileType, string icode, string iType, bool bPyramid, bool bShowProgess)
        {
            soDataSource objImportDataSource = null;
            soDataSources objDSS = AxSmWorkSpace.Datasources;
            objImportDataSource = objDSS[strAlias];
            bool bRet = ImportFile(objImportDataSource, strImpFileName, ImpResDataSetName, objImpFileType, icode, iType, bPyramid, bShowProgess);
            if (objDSS != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objDSS);
                objDSS = null;
            }
            if (objImportDataSource != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objImportDataSource);
                objImportDataSource = null;
            }
            return bRet;
        }
        public static bool ImportFile(soDataSource objImportDataSource, string strImpFileName, string ImpResDataSetName, seFileType objImpFileType, string icode, string iType, bool bPyramid, bool bShowProgess)
        {
            if (objImportDataSource == null) return false;
            soDataPump objDataPump = null;
            soImportParams objImpParams = null;
            soErrorClass objErr = new soErrorClass();
            try
            {
                objDataPump = objImportDataSource.DataPump;
                objImpParams = objDataPump.DataImportParams;

                objImpParams.FileName = strImpFileName;
                objImpParams.FileType = objImpFileType;
                objImpParams.ShowProgress = bShowProgess;
                objImpParams.ImportAsCADDataset = false;

                objImpParams.DatasetLine = ImpResDataSetName;
                objImpParams.DatasetPoint = ImpResDataSetName;
                objImpParams.DatasetRegion = ImpResDataSetName;
                objImpParams.DatasetText = ImpResDataSetName;
                objImpParams.DatasetImage = ImpResDataSetName;
                objImpParams.BuildPyramid = bPyramid;
                
               // 查找对应的坐标文件
                string strTFWFile = Path.ChangeExtension(strImpFileName, ".tfw");
                string strSMCFile = Path.ChangeExtension(strImpFileName, ".smc");
                string strDOMFile = Path.ChangeExtension(strImpFileName, ".dom");
                if (File.Exists(strTFWFile))
                {
                    objImpParams.ImageGeoRefFileType = seGeoRefFileType.scdTFW;
                    objImpParams.ImageGeoRefFileName = strTFWFile;
                }
                else if (File.Exists(strSMCFile))
                {
                    objImpParams.ImageGeoRefFileType = seGeoRefFileType.scdSMC;
                    objImpParams.ImageGeoRefFileName = strSMCFile;
                }
                else if (File.Exists(strDOMFile))
                {
                    objImpParams.ImageGeoRefFileType = seGeoRefFileType.scdDOM;
                    objImpParams.ImageGeoRefFileName = strDOMFile;
                }
                else
                {
                    ;
                }

                if (icode == "DCT")
                    objImpParams.EncodedType = seEncodedType.scEncodedDCT;
                else
                    objImpParams.EncodedType = seEncodedType.scEncodedNONE;

                if (iType == "IMAGE数据集")
                    objImpParams.DatasetRasterType = seDatasetType.scdImage;
                else if (iType == "GRID数据集")
                    objImpParams.DatasetRasterType = seDatasetType.scdGrid;
                else
                    objImpParams.DatasetRasterType = seDatasetType.scdDEM;

                if (objImpFileType == seFileType.scfE00 || objImpFileType == seFileType.scfMIF)
                {
                    //导入投影信息
                    objImportDataSource.PJCoordSys = objDataPump.RetrievePJCoordSys(strImpFileName, objImpFileType);
                }

                if (objDataPump.Import())
                {
                    WriteLogToFile("ImportOK:" + strImpFileName + ";Pyramid:" + bPyramid.ToString());
                    //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Natural, clsOperateType.OperDataImport, clsDataType.OperDataSource, "数据转入成功！");
                    return true;
                }
                else
                {
                    WriteLogToFile("ImportError:" + strImpFileName + ";Pyramid:" + bPyramid.ToString());
                    //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Error, clsOperateType.OperDataImport, clsDataType.OperDataSource, objErr.LastErrorMsg);
                    return false;
                }
            }
            catch (Exception Err)
            {
                WriteLogToFile("ImportError:" + strImpFileName + ";error:" + Err.Message + ";Pyramid:" + bPyramid.ToString() + "\n" + Err.StackTrace);
                //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Error, clsOperateType.OperDataImport, clsDataType.OperDataSource, Err.Message);
                return false;
            }
            finally
            {
                //if (objDSS != null)
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDSS);
                //    objDSS = null;
                //}

                //if (objImportDataSource != null)
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(objImportDataSource);
                //    objImportDataSource = null;
                //}

                if (objDataPump != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDataPump);
                    objDataPump = null;
                }

                if (objImpParams != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objImpParams);
                    objImpParams = null;
                }
            }
        }

        #endregion

        #region 将一个数据集导出为一个文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AxSmWorkSpace"></param>
        /// <param name="strAlias"></param>
        /// <param name="strImpFileName"></param>
        /// <param name="ImpResDataSetName"></param>
        /// <param name="objImpFileType"></param>
        /// <returns></returns>
        public static bool ExportFile(AxSuperWorkspace AxSmWorkSpace, string strAlias, string strExtFileName, string ExtDataSetName, seFileType objExpFileType)
        {
            soDataPump objDataPump = null;
            soExportParams objExpParams = null;
            soErrorClass objErr = new soErrorClass();
            soDataSource objExportDataSource = null;
            soDataSources objDSS = null;

            try
            {
                objDSS = AxSmWorkSpace.Datasources;

                objExportDataSource = objDSS[strAlias];

                objDataPump = objExportDataSource.DataPump;
                objExpParams = objDataPump.DataExportParams;

                objExpParams.DatasetToBeExported = ExtDataSetName;
                objExpParams.FileName = strExtFileName;
                objExpParams.FileType = objExpFileType;
                objExpParams.ShowProgress = true;

                if (objDataPump.Export())
                {
                    //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Natural, clsOperateType.OperDataImport, clsDataType.OperDataSource, "数据转入成功！");
                    return true;
                }
                else
                {
                    //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Error, clsOperateType.OperDataImport, clsDataType.OperDataSource, objErr.LastErrorMsg);
                    return false;
                }
            }
            catch (Exception Err)
            {
                //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Error, clsOperateType.OperDataImport, clsDataType.OperDataSource, Err.Message);
                return false;
            }
            finally
            {
                if (objDSS != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDSS);
                    objDSS = null;
                }

                if (objExportDataSource != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExportDataSource);
                    objExportDataSource = null;
                }

                if (objDataPump != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDataPump);
                    objDataPump = null;
                }

                if (objExpParams != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExpParams);
                    objExpParams = null;
                }
            }
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="AxSmWorkSpace"></param>
        /// <param name="strAlias"></param>
        /// <param name="strExtFileName"></param>
        /// <param name="ExtDataSetName"></param>
        /// <param name="objExpFileType"></param>
        /// <param name="bShowProgress"></param>
        /// <returns></returns>
        public static bool ExportFile(AxSuperWorkspace AxSmWorkSpace, string strAlias, string strExtFileName, string ExtDataSetName, seFileType objExpFileType,bool bShowProgress)
        {
            soDataPump objDataPump = null;
            soExportParams objExpParams = null;
            soErrorClass objErr = new soErrorClass();
            soDataSource objExportDataSource = null;
            soDataSources objDSS = null;

            try
            {
                objDSS = AxSmWorkSpace.Datasources;

                objExportDataSource = objDSS[strAlias];

                objDataPump = objExportDataSource.DataPump;
                objExpParams = objDataPump.DataExportParams;

                objExpParams.DatasetToBeExported = ExtDataSetName;
                objExpParams.FileName = strExtFileName;
                objExpParams.FileType = objExpFileType;
                objExpParams.ShowProgress = bShowProgress;

                if (objDataPump.Export())
                {
                    //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Natural, clsOperateType.OperDataImport, clsDataType.OperDataSource, "数据转入成功！");
                    return true;
                }
                else
                {
                    //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Error, clsOperateType.OperDataImport, clsDataType.OperDataSource, objErr.LastErrorMsg);
                    return false;
                }
            }
            catch (Exception Err)
            {
                //clsMessage.WriteSystemLogShowMessage(clsStatus.Status_Error, clsOperateType.OperDataImport, clsDataType.OperDataSource, Err.Message);
                return false;
            }
            finally
            {
                if (objDSS != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDSS);
                    objDSS = null;
                }

                if (objExportDataSource != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExportDataSource);
                    objExportDataSource = null;
                }

                if (objDataPump != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objDataPump);
                    objDataPump = null;
                }

                if (objExpParams != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExpParams);
                    objExpParams = null;
                }
            }
        }
        #endregion 

        #region 从原数据源中查询一个数据集并将查询的结果拷贝到目标数据集当中
        /// <summary>
        /// 功能描述:从原数据源中查询一个数据集并将查询的结果拷贝到目标数据集当中
        /// </summary>
        /// <param name="objDataSrc">原数据源</param>
        /// <param name="objQueryDef">自定义查询条件</param>
        /// <param name="objDataSrcOrder">目标数据集</param>
        /// <param name="strDTName">查询的数据集名称</param>
        /// <returns>返回拷贝后目标数据集的名称</returns>
        public static string CopyDataToAnother(soDataSource objDataSrc,soQueryDef objQueryDef,soDataSource objDataSrcOrder,string strDTName)
        {
            soErrorClass objErr = new soErrorClass();

            soDatasetVector objDTVector_Src = null;    //原数据集
            soDatasetVector objDTVector_Order = null;  //目标数据集

            soRecordset objRec_Src = null; //原数据中的查询结果

            string strDTOrder_Name = "";

            try
            {
                //判断参数是否有效
                if (objDataSrc == null || objQueryDef == null || objDataSrcOrder == null || strDTName == "" || strDTName == null)
                {
                    strDTOrder_Name = "";
                    return "";
                }

                objDTVector_Src = objDataSrc.Datasets[strDTName] as soDatasetVector;

                if (objDTVector_Src == null) return "";

                objRec_Src = objDTVector_Src.Query2(objQueryDef);

                #region 判断是否已经存在数据在临时数据源当中
                soDatasetVector objOrderDT = objDataSrcOrder.Datasets[strDTName] as soDatasetVector;

                if (objOrderDT != null)
                {
                    //将临时数据源中的数据集删除
                    bool bDel = objDataSrcOrder.DeleteDataset(strDTName);
                    
                    ReleaseSmObject(objOrderDT);
                }
                #endregion 

                //objDTVector_Order = objDataSrcOrder.CreateDatasetFrom(strDTName, objDTVector_Src) as soDatasetVector;

                objDTVector_Order = objDataSrcOrder.RecordsetToDataset(objRec_Src, strDTName, false);

                if (objDTVector_Order == null)
                {
                    return "";
                }
                else
                {
                    return objDTVector_Order.Name;
                }
            }
            catch (Exception Err)
            {
                strDTOrder_Name = "";
                return "";
            }
            finally
            {
                ReleaseSmObject(objQueryDef);
                ReleaseSmObject(objErr);
                ReleaseSmObject(objDTVector_Src);
                ReleaseSmObject(objDTVector_Order);
                ReleaseSmObject(objRec_Src);
            }
        }
        #endregion

        #region 拷贝记录集到数据源
        /// <summary>
        /// 拷贝记录集到制定数据源当中
        /// </summary>
        /// <param name="objSrc">目标数据源</param>
        /// <param name="objRec">记录集</param>
        /// <param name="strDTName">数据集名称</param>
        /// <param name="bShow">是否显示拷贝过程</param>
        /// <returns></returns>
        public static bool CopyRecordSetToDataSource(soDataSource objSrc, soRecordset objRec,string strDTName,bool bShow)
        {
            soDatasetVector objDTVector = null;

            try
            {
                objDTVector = objSrc.RecordsetToDataset(objRec, strDTName, bShow);

                if (objDTVector != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objRec);
            }
        }

        #endregion

        #region 附加记录集到指定数据集
        
        /// <summary>
        /// 附加记录集到目标数据集
        /// </summary>
        /// <param name="AxSuperWKS">工作空间</param>
        /// <param name="objRecSet">附加记录集</param>
        /// <param name="strOrderDS_Alias">目标数据源别名</param>
        /// <param name="strOrderDTName">目标数据集别名</param>
        /// <param name="bShowProgress">是否显示进度条</param>
        /// <returns>是否附加成功</returns>
        public static  bool Append_RecToDataSetVector (AxSuperWorkspace AxSuperWKS,soRecordset objRecSet
            ,string strOrderDS_Alias,string strOrderDTName,bool bShowProgress)
        {
            soDataSource objOrderDS = null;
            soDatasetVector objOrderDT = null;

            try
            {
                if (objRecSet == null) return false;

                if (objRecSet.RecordCount == 0) return false;

                objOrderDS = GetDataSource(AxSuperWKS, strOrderDS_Alias);
                objOrderDT = getDatasetFromWorkspaceByName(strOrderDTName, AxSuperWKS, strOrderDS_Alias) as soDatasetVector;

                if (objOrderDS == null) return false;
                if (objOrderDT == null) return false;

                bool bApp = objOrderDT.Append(objRecSet, bShowProgress);

                return bApp;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objOrderDS);
                ReleaseSmObject(objOrderDT);
            }
        }
        #endregion              



        #region 模板创建数据集
        /// <summary>
        /// 模板创建数据集
        /// </summary>
        /// <param name="objAxSuperWKS">工作空间</param>
        /// <param name="strOrderDS">目标数据源</param>
        /// <param name="strOrderDTName">目标数据集名称</param>
        /// <param name="objFrmDTVector">模板数据集</param>
        /// <returns>是否创建成功</returns>
        public static bool CreateDataSetByFrmDT(AxSuperWorkspace objAxSuperWKS,string strOrderDS
            ,string strOrderDTName,soDatasetVector objFrmDTVector)
        {
            soDataSource objOrderDS = null;
            soDataset objOrderDT = null;
            
            try
            {
                objOrderDS = GetDataSource(objAxSuperWKS, strOrderDS);
                objOrderDT =  objOrderDS.CreateDatasetFrom(strOrderDTName, objFrmDTVector);

                if (objOrderDT != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objOrderDS);
                ReleaseSmObject(objOrderDT);
            }
        }

        /// <summary>
        /// 模板创建属性结构相同的不同类型的数据集
        /// </summary>
        /// <param name="objAxSuperWKS">工作空间</param>
        /// <param name="strOrderDS">模板数据源别名</param>
        /// <param name="strOrderDTName">目标数据集名称</param>
        /// <param name="objFrmDTVector">模板数据集</param>
        /// <param name="seOrdreDTType">要创建的数据集类型</param>
        /// <returns></returns>
        public static bool CreateDataSetByFrmDT(AxSuperWorkspace objAxSuperWKS, string strOrderDS
            , string strOrderDTName, soDatasetVector objFrmDTVector,seDatasetType seOrdreDTType)
        {
            soDataSource objDS = null;
            soDatasetVector objOrderDT = null;
            soFieldInfos objFields = null;
            soDatasetVector objCreateOrderDT = null;

            try
            {
                if (objFrmDTVector == null) return false;

                objDS = GetDataSource(objAxSuperWKS, strOrderDS);

                objOrderDT = getDatasetFromWorkspaceByName(strOrderDTName, objAxSuperWKS, strOrderDS) as soDatasetVector ;

                if (objOrderDT != null)
                {
                    objDS.DeleteDataset(strOrderDTName);
                    ReleaseSmObject(objOrderDT);
                }

                if (objDS == null) return false;

                objCreateOrderDT = objDS.CreateDataset(strOrderDTName, seOrdreDTType, seDatasetOption.scoDefault, null) as soDatasetVector;

                objFields = objFrmDTVector.GetFieldInfos();

                for (int i = 1; i <= objFields.Count; i++)
                {
                    soFieldInfo objField = objFields[i];

                    if (objField.Name.Substring(0,2).ToUpper() != "SM")
                    {
                        objCreateOrderDT.CreateField(objField);
                    }

                    ReleaseSmObject(objField);
                }

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objDS);
                ReleaseSmObject(objOrderDT);
                ReleaseSmObject(objFields);
                ReleaseSmObject(objCreateOrderDT);
            }
        }
        #endregion 

        #region 从模板数据集添加用户自定义字段到目标数据集
        /// <summary>
        /// 从模板数据集添加用户自定义字段到目标数据集
        /// </summary>
        /// <param name="objFromDT">模板数据集</param>
        /// <param name="objOrderDT">目标数据集</param>
        /// <returns>是否添加成功</returns>
        public static bool AddUserDefineField_To_DataSet(soDatasetVector objFromDT,soDatasetVector objOrderDT)
        {
            soFieldInfos objFields = null;

            try
            {
                objFields = objFromDT.GetFieldInfos();

                for (int i = 1; i <= objFields.Count; i++)
                {
                    soFieldInfo objField = objFields[i];

                    if (objOrderDT.IsAvailableFieldName(objField.Name))
                    {
                        objOrderDT.CreateField(objField);
                    }

                    ReleaseSmObject(objField);
                }

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objFields);
            }
        }
        #endregion

        #region 根据一个主控字段将数据从一个数据集拷贝到目标数据集

        /// <summary>
        /// 用户自定义条件复制
        /// </summary>
        /// <param name="objAxSuperWorkSpace">工作空间控件</param>
        /// <param name="strAlias">数据源别名</param>
        /// <param name="strSrcDT">原数据集名称</param>
        /// <param name="strOrderDT">目标数据集名称</param>
        /// <param name="objFields">更新的字段列表</param>
        /// <param name="strFilter">过滤条件</param>
        /// <returns>是否拷贝成功</returns>
        public static bool CopyDataToOrderData(AxSuperWorkspace objAxSuperWorkSpace,string strAlias
            ,string strSrcDT,string strOrderDT,ArrayList objFields,string strFilter)
        {
            soDataSource objDS = null;
            soDatasetVector objSrcDTVector = null;
            soDatasetVector objOrderDTVector = null;
            soRecordset objSrcRec = null;
            soStrings objstrFields = new soStringsClass();
            bool bApp = false;

            try
            {
                objDS = ztSuperMap.GetDataSource(objAxSuperWorkSpace, strAlias);

                if (objDS == null) return false;

                objSrcDTVector = ztSuperMap.getDatasetFromWorkspaceByName(strSrcDT, objAxSuperWorkSpace, strAlias) as soDatasetVector;
                objOrderDTVector = ztSuperMap.getDatasetFromWorkspaceByName(strOrderDT, objAxSuperWorkSpace, strAlias) as soDatasetVector;

                if (objSrcDTVector == null || objOrderDTVector == null)
                {
                    return false;
                }

                for (int i = 0; i < objFields.Count; i++)
                {
                    objstrFields.Add(objFields[i].ToString());
                }

                objSrcRec = objSrcDTVector.Query(strFilter, true, objstrFields, "");

                if (objSrcRec != null)
                {
                    bApp = objOrderDTVector.Append(objSrcRec, false);
                }

                return bApp;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objSrcDTVector);
                ztSuperMap.ReleaseSmObject(objOrderDTVector);
                ztSuperMap.ReleaseSmObject(objSrcRec);
                ztSuperMap.ReleaseSmObject(objstrFields);
            }
        }

        /// <summary>
        /// 前置条件:这两个数据集必须都要在此数据源当中
        /// 根据主控字段对数据进行属性继承
        /// </summary>
        /// <param name="objOrderDS">目标数据源</param>
        /// <param name="strSrcDTName">原始数据集名称</param>
        /// <param name="strOrderDTName">目标数据集名称</param>
        /// <param name="strSrcKeyField">原始数据集主控字段</param>
        /// <param name="strOrderKeyField">目标数据集主控字段</param>
        /// <param name="strSrcFields">拷贝的原始字段列表</param>
        /// <param name="strOrderFields">拷贝的目标字段列表</param>
        /// <returns>是否拷贝成功</returns>
        public static bool CopyDataToOrderDataByKeyField(soDataSource objOrderDS, string strSrcDTName
            , string strOrderDTName, string strSrcKeyField, string strOrderKeyField, string[] strSrcFields, string[] strOrderFields)
        {
            if (objOrderDS == null) return false;

            if (strSrcFields.Length != strOrderFields.Length) return false;

            string UpdateSQL = "Update ( SELECT * from " + strOrderDTName
                + " inner join " + strSrcDTName + " on " + strOrderDTName + "." + strOrderKeyField + " = " + strSrcDTName + "." + strSrcKeyField + ")  Set ";

            for (int i = 0; i < strOrderFields.Length; i++)
            {
                UpdateSQL += strOrderDTName + "." + strOrderFields[i] + "=" + strSrcDTName + "." + strSrcFields[i] + ",";
            }

            UpdateSQL = UpdateSQL.Substring(0, UpdateSQL.LastIndexOf(","));

            bool bExe = objOrderDS.ExecuteSQL(UpdateSQL);

            return bExe;
        }

        public static bool CopyDataToOrderDataByKeyField(soDatasetVector objDT_Src,soDatasetVector objOrderDT,string strSrcKeyField,string strOrderKeyField,string[] strCopyFields)
        {
            soRecordset objSrcRec = null;
            soFieldInfo objKeyField = null;

            try
            {
                if (objDT_Src == null) return false;
                if (objOrderDT == null) return false;
                if (strSrcKeyField == "" || strSrcKeyField == null) return false;
                if (strCopyFields == null) return false;
                if (strCopyFields.Length == 0) return false;

                objKeyField = objDT_Src.GetFieldInfo(strSrcKeyField);

                if (objKeyField == null) return false;

                objSrcRec = objDT_Src.Query("", false, null, "");

                if (objSrcRec == null) return false;

                //循环原始记录集
                for (int i = 1; i <= objSrcRec.RecordCount; i++)
                {
                    string strKeyValue = objSrcRec.GetFieldValueText(strSrcKeyField);

                    string strFilter = "";

                    //构造过滤条件
                    if (objKeyField.Type == seFieldType.scfDate || objKeyField.Type == seFieldType.scfText
                        || objKeyField.Type == seFieldType.scfMemo || objKeyField.Type == seFieldType.scfChar
                        || objKeyField.Type == seFieldType.scfTime || objKeyField.Type == seFieldType.scfNchar)
                    {
                        strFilter = strOrderKeyField + "= " + strKeyValue + "'";
                    }
                    else
                    {
                        strFilter = strOrderKeyField + "= " + strKeyValue;
                    }

                    //给每个字段赋值
                    for (int j = 0; j < strCopyFields.Length; j++)
                    {
                        bool bUpdate = objOrderDT.UpdateFieldEx(strCopyFields[j], objSrcRec.GetFieldValueText(strCopyFields[j]), strFilter);
                    }

                    objSrcRec.MoveNext();
                }

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objSrcRec);
                ReleaseSmObject(objKeyField);
            }
        }
        #endregion

        #endregion

        #region 查找操作

        #region 查询记录集
        /// <summary>
        /// 查询记录集
        /// </summary>
        /// <param name="objDS"></param>
        /// <param name="strDTName"></param>
        /// <param name="strCondation"></param>
        /// <param name="bHasGeometry"></param>
        /// <param name="objString"></param>
        /// <param name="strOption"></param>
        /// <returns></returns>
        public static soRecordset GetRecordByCondation(soDataSource objDS,string strDTName,string strCondation,bool bHasGeometry,soStrings objString,string strOption)
        {
            soDatasetVector objDtVector = null;

            try
            {
                objDtVector = objDS.Datasets[strDTName] as soDatasetVector;

                if (objDtVector == null) return null;

                soRecordset objRec = objDtVector.Query(strCondation, bHasGeometry, objString, strOption);

                return objRec;
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            {
                ReleaseSmObject(objDtVector);
            }
        }
        #endregion 

       
        #region 获取图层名
        public static string[] GetLayerName(AxSuperMap m_objAxSuperMap)
        {
            List<string> m_objLayers = new List<string>();
            soLayers objLayers = m_objAxSuperMap.Layers;
            if (objLayers == null)
                return null;
            
            for (int i = 1; i <= objLayers.Count; i++)
            {
                soLayer objLayer = objLayers[i];
                m_objLayers.Add(objLayer.Name);
                ReleaseSmObject(objLayer);
            }

            ReleaseSmObject(objLayers);

            return m_objLayers.ToArray();
        }

        /// <summary>
        /// 获取图层名，图层可能有如下规则：
        /// 如果图层名后面有 "_",返回最后一个_前面的所有字符
        /// 如果图层名后面有 "@",返回最后一个@前面的所有字符
        /// </summary>
        /// <param name="strLayerName">输入的层名</param>
        /// <returns>返回去掉标签后的层名</returns>
        public static string GetLayerName(string strLayerName)
        {
            string strTemp = string.Empty;
            int strIndex = strLayerName.LastIndexOf("_");
            if (strIndex == -1)
            {
                int strLyrIndex = strLayerName.LastIndexOf("@");
                if (strLyrIndex == -1)
                {
                    return strLayerName;
                }
                else
                {
                    strTemp = strLayerName.Remove(strLyrIndex);
                    return strTemp;
                }

            }
            else
            {
                strTemp = strLayerName.Remove(strIndex);
                return strTemp;
            }
        }

        /// <summary>
        /// 根据参数数据集名称字符串获得图层名称
        /// </summary>
        /// <param name="AxSuperMap">地图对象</param>
        /// <param name="strDatasetName">数据集名称</param>
        /// <returns>失败返回Empty</returns>
        public static string SelectedLayerNameFormDataSetName(AxSuperMap AxSuperMap, string strDatasetName) 
        {
            if (AxSuperMap == null) return string.Empty;
            if (string.IsNullOrEmpty(strDatasetName)) return string.Empty;
            soLayers objlayers = null;

            try 
            {
                objlayers = AxSuperMap.Layers;
                if (objlayers == null) return string.Empty;
                for (int i = 1; i <= objlayers.Count; i++) 
                {
                    soLayer objlayer = objlayers[i];
                    if (objlayer == null) continue;
                    string strlayername = objlayer.Name;
                    ReleaseSmObject(objlayer);
                    string strlayernametemp = strlayername.Substring(0, strlayername.IndexOf("@"));
                    //if (strlayernametemp.Equals(strDatasetName)) return strlayername;
                    if (strlayernametemp.Contains(strDatasetName)) return strlayername;
                }
                return string.Empty;
            }
            catch 
            { 
                return string.Empty;
            }
            finally 
            {
                ReleaseSmObject(objlayers);
            }
        }

        public static string GetEditLayerName(AxSuperMap AxSuperMap)
        {
            soLayers objLayers = null;
            soLayer objLayer = null;

            try
            {
                objLayers = AxSuperMap.Layers;
                if (objLayers == null)
                    return "";

                for (int i = 1; i <= objLayers.Count; i++)
                {
                    objLayer = objLayers[i];

                    if (objLayer == null)
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            if (objLayer.Editable)
                            {
                                return objLayer.Name;
                            }
                        }
                        catch
                        {
                            ReleaseSmObject(objLayer);
                        }
                    }

                    ReleaseSmObject(objLayer);
                }

                return "";
            }
            catch (Exception Err)
            {
                return "";
            }
            finally
            {
                ReleaseSmObject(objLayer);
                ReleaseSmObject(objLayers);
            }
        }

        /// <summary>
        /// 根据图层名获取数据集名称
        /// </summary>
        /// <param name="strLayerName">图层名</param>
        /// <returns></returns>
        public static String GetDatasetNameFromLayerName(String strLayerName)
        {
            if (strLayerName.Length == 0)
            {
                return "";
            }
            strLayerName = strLayerName.Trim();
            if (strLayerName.IndexOf("@") >= 0)     //如果有@,就从此位置开始删除掉
            {
                strLayerName = strLayerName.Remove(strLayerName.IndexOf("@"));
            }
            return strLayerName;
        }

        /// <summary>
        /// 根据图层名获取数据源名称
        /// </summary>
        /// <param name="strLayerName">图层名</param>
        /// <returns></returns>
        public static String GetDataSourceNameFromLayerName(String strLayerName)
        {
            if (strLayerName.Length == 0)
            {
                return "";
            }
            strLayerName = strLayerName.Trim();
            if (strLayerName.IndexOf("@") >= 0)     //如果有@,就从此位置开始删除掉
            {
                strLayerName = strLayerName.Remove(0,strLayerName.IndexOf("@") + 1);
            }
            return strLayerName;
        }

        public static string[] GetDataSetNameFromDataSource(AxSuperWorkspace objAxSuperWorkSpace, object objDSAlias)
        {
            soDataSource objDS = GetDataSource(objAxSuperWorkSpace, objDSAlias);

            if (objDS == null) return null;

            soDatasets objDTS = objDS.Datasets;
            string[] strDTS_Name = new string[objDTS.Count];

            for (int i = 1; i <= objDTS.Count; i++)
            {
                soDataset objDT = objDTS[i];
                strDTS_Name[i - 1] = objDT.Name;
                ReleaseSmObject(objDT);
            }

            ReleaseSmObject(objDTS);
            ReleaseSmObject(objDS);

            return strDTS_Name;
        }

        #endregion 

        #region 获取指定数据源的数据引擎类型
        /// <summary>
        /// 获取指定数据源的数据引擎类型．
        /// </summary>
        /// <param name="enginetype">返回，数据源引擎类型。，当返回值为 false 时无效</param>
        /// <param name="strDatasourceName">数据源名</param>
        /// <param name="superworkspace">工作空间</param>
        /// <returns>返回是否提取成功</returns>
        public static bool getDatasourceEnginType(seEngineType enginetype, string strDatasourceName, AxSuperWorkspace superworkspace)
        {
            bool bResult = false;

            soDataSources objdss = superworkspace.Datasources;
            if (objdss != null)
            {
                soDataSource objds = objdss[strDatasourceName];
                if (objds != null)
                {
                    enginetype = objds.EngineType;
                    bResult = true;
                    Marshal.ReleaseComObject(objds);
                    objds = null;
                }
                Marshal.ReleaseComObject(objdss);
                objdss = null;
            }

            // 返回是否成功
            return bResult;
        }
        #endregion 

        #region 获取指定数据集的数据类型
        /// <summary>
        /// 获取指定数据集的数据类型。
        /// </summary>
        /// <param name="datatype">数据集类型，当返回值为 false 时无效</param>
        /// <param name="datasetname">数据集名</param>
        /// <param name="datasourcename">数据源名</param>
        /// <returns>返回数据集的类型，发生错误返回 null</returns>
        public static bool getDatasetType(seDatasetType datatype, string datasetname, soDataSource datasourcename)
        {
            bool bResult = false;

            soDataset objDataset;
            objDataset = getDatasetFromDataSourceByName(datasetname, datasourcename);
            if (objDataset != null)
            {
                datatype = objDataset.Type;
                bResult = true;
                Marshal.ReleaseComObject(objDataset);
                objDataset = null;
            }
            // 返回是否成功。
            return bResult;
        }
        #endregion

        #region 提取某一数据层的所有字段名称
        /// <summary>
        /// 提取某一数据层的所有字段名称。
        /// </summary>
        /// <param name="maplayer">层</param>
        /// <param name="containsm">是否包含系统字段</param>
        /// <returns>字段名列表</returns>
        public static string[] GetFieldName(soLayer maplayer, bool containsm)
        {
            string[] lstFilename = null;

            if (maplayer == null) return null;
            try
            {
                soDataset objDt = maplayer.Dataset;                
                if (objDt != null && objDt.Vector)
                {
                    soDatasetVector objdtVect= objDt as soDatasetVector;
                    lstFilename = GetFieldName(objdtVect, containsm);
                    Marshal.ReleaseComObject(objDt);
                    objDt = null;
                }
            }
            catch{ }
            return lstFilename;
        }
        #endregion 

        #region 获得数据集字段名称
        /// <summary>
        /// 获得数据集字段名称
        /// </summary>
        /// <param name="objDTVector">矢量数据集</param>
        /// <returns></returns>
        public static string[] GetFieldName(SuperMapLib.soDatasetVector objDTVector, bool containsm)
        {
            if (objDTVector != null && objDTVector.FieldCount > 0)
            {
                ArrayList alFieldNames = new ArrayList();
                soFieldInfo objFieldInfo = null; // 源矢量数据集字段信息
                for (int i = 1; i <= objDTVector.FieldCount; i++)
                {
                    objFieldInfo = objDTVector.GetFieldInfo(i);
                    if (!containsm)
                    {
                        if (objFieldInfo.Name.Length < 2 || objFieldInfo.Name.ToUpper().Substring(0, 2) == "SM")
                        {
                            ReleaseSmObject(objFieldInfo);
                            continue;                            
                        }
                    }
                    alFieldNames.Add(objFieldInfo.Name);
                    ReleaseSmObject(objFieldInfo);
                }
                
                return (string[])alFieldNames.ToArray(typeof(string));
            }
            else
            {
                return null;
            }

        }
        #endregion 

        #region 获得非SM字段别名caption数组

        /// <summary>
        /// 提取某一数据层的所有字段名称。
        /// </summary>
        /// <param name="maplayer">层</param>
        /// <param name="containsm">是否包含系统字段</param>
        /// <returns>字段名列表</returns>
        public static string[] GetFieldCaption(soLayer maplayer, bool containsm)
        {
            string[] lstFilename = null;

            if (maplayer == null) return null;
            try
            {
                soDataset objDt = maplayer.Dataset;
                if (objDt != null && objDt.Vector)
                {
                    soDatasetVector objdtVect = objDt as soDatasetVector;
                    lstFilename = GetFieldCaption(objdtVect, containsm);
                    Marshal.ReleaseComObject(objDt);
                    objDt = null;
                }
            }
            catch { }
            return lstFilename;
        }

        /// <summary>
        /// 获得非SM字段别名caption数组
        /// </summary>
        /// <param name="objDTVector">矢量数据集</param>
        /// <param name="containsm">是否包含系统字段</param>
        /// <returns></returns>
        public static string[] GetFieldCaption(SuperMapLib.soDatasetVector objDTVector,bool containsm)
        {
            if (objDTVector != null && objDTVector.FieldCount > 0)
            {
                ArrayList alFieldNames = new ArrayList();
                soFieldInfo objFieldInfo = null; // 源矢量数据集字段信息
                for (int i = 1; i <= objDTVector.FieldCount; i++)
                {
                    objFieldInfo = objDTVector.GetFieldInfo(i);
                    if (!containsm)
                    {
                        if (objFieldInfo.Name.Length < 2 || objFieldInfo.Name.ToUpper().Substring(0, 2) == "SM")
                        {
                            ReleaseSmObject(objFieldInfo);
                            continue;
                        }
                    }
                    
                    alFieldNames.Add(objFieldInfo.Caption);
                    ReleaseSmObject(objFieldInfo);
                }
                
                return (string[])alFieldNames.ToArray(typeof(string));
            }
            else
            {
                return null;
            }

        }
        #endregion 

        #region 获得记录集所有字段名称
        /// <summary>
        /// 获得记录集所有字段名称。
        /// </summary>
        /// <param name="objRs">记录集</param>
        /// <param name="containsm">是否包含系统字段</param>
        /// <returns></returns>
        public static string[] GetFieldName(SuperMapLib.soRecordset objRs,bool containsm)
        {
            if (objRs != null && objRs.FieldCount > 0)
            {
                ArrayList alFieldNames = new ArrayList();
                soFieldInfo objFieldInfo = null; // 源矢量数据集字段信息
                for (int i = 1; i <= objRs.FieldCount; i++)
                {
                    objFieldInfo = objRs.GetFieldInfo(i);

                    if (!containsm)
                    {
                        if (objFieldInfo.Name.Length < 2 || objFieldInfo.Name.ToUpper().Substring(0, 2) == "SM")
                        {
                            continue;
                            ReleaseSmObject(objFieldInfo);
                        }
                    }                   
                    alFieldNames.Add(objFieldInfo.Name);
                    ReleaseSmObject(objFieldInfo);
                }                
                return (string[])alFieldNames.ToArray(typeof(string));
            }
            else
            {
                return null;
            }

        }
        #endregion


        #region 获取按照影像1：1显示时地图的比例尺

        /// <summary>
        /// 根据传入的图层名设置影像原始分辨率
        /// </summary>
        /// <param name="layername"></param>
        /// <param name="supermap"></param>
        /// <param name="wksp"></param>
        /// <returns></returns>
        public static double GetImgOneRateViewRate(string layername, AxSuperMap supermap)
        {
            // 我们看到，其实影像原始分辨率麻烦是怎么定位到真实的栅格影像图层。
            // 那么我们就从外部想办法把确定的图层名传进来。
            soDatasetRaster objRaster = null;

            soLayers objLayers = supermap.Layers;
            if (objLayers != null)
            {
                for (int i = 1; i <= objLayers.Count; i++)
                {
                    // 没有显示的不算
                    if (objLayers[i].Visible != true)
                        continue;

                    if (objLayers[i].Name != layername)
                        continue;

                    soDataset objDt = objLayers[i].Dataset;
                    if (objDt != null)
                    {
                        if (!objDt.Vector)
                        {
                            objRaster = (soDatasetRaster)objDt;
                            break;
                        }

                        Marshal.ReleaseComObject(objDt);
                        objDt = null;
                    }
                }
                Marshal.ReleaseComObject(objLayers);
                objLayers = null;
            }

            if (objRaster == null)
                return 0.0;

            double dNewRatio = GetImgOneRatebyDatasetRaster(objRaster, supermap);
            Marshal.ReleaseComObject(objRaster);

            // 窗口当前的比例尺．
            return dNewRatio;
        }

        /// <summary>
        /// 获取影像数据1：1显示时，窗口应该设置的比例尺
        /// </summary>
        /// <param name="objRaster">栅格数据</param>
        /// <param name="supermap"></param>
        /// <param name="wksp"></param>
        /// <returns></returns>
        public static double GetImgOneRatebyDatasetRaster(soDatasetRaster objRaster, AxSuperMap supermap)
        {
            double dRes;                    // 像元大小
            if (supermap == null)
                return 0.0;

            if (objRaster != null)
            {
                dRes = objRaster.ResolutionX;           // 像元大小．                                
            }
            else
                return 0.0;

            // 屏幕的像素宽
            int nViewWidth = supermap.MapToPixelX(supermap.ViewBounds.Right) - supermap.MapToPixelX(supermap.ViewBounds.Left);

            // 如果按照图像1：1显示时当前supermap应该具有的地图宽度。            
            double dOneRatioWidth = nViewWidth * dRes;

            // 如果按照 1:1 显示，窗口应该放大的倍数．
            double dRatio = supermap.ViewScale;
            double dViewWidth = supermap.ViewBounds.Width();

            // 如果动态投影，需要知道屏幕的宽度是栅格宽度单位的多少。
            if (supermap.EnableDynamicProjection)
            {
                soPoint pntbr = supermap.ViewBounds.BottomRight();
                soPoint pntbl = supermap.ViewBounds.TopLeft();
                soPJCoordSys s = supermap.PJCoordSys;
                soPJCoordSys t = objRaster.PJCoordSys;
                CoordTranslator(pntbl, s, t);
                CoordTranslator(pntbr, s, t);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);

                dViewWidth = pntbr.x - pntbl.x;

                Marshal.ReleaseComObject(pntbr);
                Marshal.ReleaseComObject(pntbl);
            }

            double dNewRatio = dRatio / (dOneRatioWidth / dViewWidth);

            // 窗口当前的比例尺．
            return dNewRatio;
        }



        /// <summary>
        /// 获取按照影像1：1显示时地图的比例尺
        /// </summary>
        /// <param name="supermap"></param>
        /// <returns>如果没有栅格数据集，返回0.0;</returns>
        public static double GetImgOneRateViewRate(AxSuperMap supermap)
        {
            if (supermap == null)
                return 0.0;

            soDatasetRaster objRaster = null;
            soLayers objLayers = supermap.Layers;

            if (objLayers != null)
            {
                for (int i = 1; i <= objLayers.Count; i++)
                {
                    // 没有显示的不算
                    if (objLayers[i].Visible != true)
                        continue;

                    soDataset objDt = objLayers[i].Dataset;
                    if (objDt != null)
                    {
                        if (!objDt.Vector)
                        {
                            // 找在当前窗口显示的第一个栅格
                            soGeoRect geoR = new soGeoRectClass();
                            geoR.Bottom = objDt.Bounds.Bottom;
                            geoR.Top = objDt.Bounds.Top;
                            geoR.Left = objDt.Bounds.Left;
                            geoR.Right = objDt.Bounds.Right;

                            if (supermap.EnableDynamicProjection)
                            {
                                soPJCoordSys s = objDt.PJCoordSys;
                                soPJCoordSys t = supermap.PJCoordSys;
                                CoordTranslator((soGeometry)geoR, s, t);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                            }

                            // 判断该栅格是否在当前supermap 中显示
                            // 这样也还是有漏洞.  暂时先这样吧.   beizhan 20090106
                            soGeoRect geoMapR = new soGeoRectClass();
                            geoMapR.Bottom = supermap.ViewBounds.Bottom;
                            geoMapR.Top = supermap.ViewBounds.Top;
                            geoMapR.Left = supermap.ViewBounds.Left;
                            geoMapR.Right = supermap.ViewBounds.Right;

                            bool bresult = geoR.SpatialRelation.Disjoint((soGeometry)geoMapR);

                            Marshal.ReleaseComObject(geoR);
                            Marshal.ReleaseComObject(geoMapR);

                            // 如果没在当前窗口显示
                            if (bresult)
                                continue;

                            objRaster = (soDatasetRaster)objDt;
                            break;
                        }

                        Marshal.ReleaseComObject(objDt);
                        objDt = null;
                    }
                }

                Marshal.ReleaseComObject(objLayers);
                objLayers = null;
            }

            if (objRaster == null)
                return 0.0;

            double dNewRatio = GetImgOneRatebyDatasetRaster(objRaster, supermap);
            Marshal.ReleaseComObject(objRaster);

            // 窗口当前的比例尺．
            return dNewRatio;
        }
        #endregion 

        #region 根据条件查询
        /// <summary>
        /// 功能描述:根据条件查询
        /// </summary>
        /// <param name="objDTSrc">数据源</param>
        /// <param name="strDTName">数据集名称</param>
        /// <param name="objQueryDef">查询条件对象</param>
        /// <returns>查询结果</returns>
        public static soRecordset GetQueryResult(soDataSource objDTSrc, string strDTName, soQueryDef objQueryDef)
        {
            soDatasetVector objQueryDT = null;

            try
            {
                objQueryDT = objDTSrc.Datasets[strDTName] as soDatasetVector;

                soRecordset objQueryRec = objQueryDT.Query2(objQueryDef);

                if (objQueryRec != null)
                {
                    return objQueryRec;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            {
                ReleaseSmObject(objQueryDT);
                ReleaseSmObject(objQueryDef);
            }
        }
        #endregion

        #endregion

        #region 坐标系投影操作公共方法

        #region 根据传入的坐标系统类型返回坐标系统名称，暂时支持54，80，wgs84 坐标系
        /// <summary>
        /// 根据传入的坐标系统类型返回坐标系统名称，暂时支持54，80，wgs84 坐标系。
        /// </summary>
        /// <param name="spjgs">传入的坐标系类型</param>
        /// <returns>返回坐标系名称</returns>
        public static string GetPJGeoCoordSysTypeName(sePJGeoCoordSysType spjgs)
        {
            string strUnitName;
            try
            {
                switch (spjgs)
                {
                    case SuperMapLib.sePJGeoCoordSysType.scGCS_USER_DEFINED:
                        strUnitName = "用户自定义坐标系";
                        break;
                    case SuperMapLib.sePJGeoCoordSysType.scGCS_BEIJING_1954:
                        strUnitName = "北京1954地理坐标系";
                        break;
                    case SuperMapLib.sePJGeoCoordSysType.scGCS_WGS_1984:
                        strUnitName = "WGS 1984 坐标系";
                        break;
                    case SuperMapLib.sePJGeoCoordSysType.scGCS_XIAN_1980:
                        strUnitName = "西安1980地理坐标系";
                        break;
                    default:
                        strUnitName = "未知坐标系";
                        break;
                }
                return strUnitName;
            }
            catch
            {
                strUnitName = "未知坐标系";
                return strUnitName;
            }
        }
        #endregion

        #region 读取一个SuperMap控件中坐标系统字符串
        /// <summary>
        /// 读取一个SuperMap控件中坐标系统字符串。
        /// </summary>
        /// <param name="objsupermap">SuperMap 控件</param>        
        /// <returns>坐标系描述</returns>
        public static string GetGeoCoordsysTypeFromSupermap(AxSuperMap objsupermap)
        {
            string strSPJName = "未知坐标系";

            if (objsupermap == null) return strSPJName;

            soPJCoordSys pjCoordsys = objsupermap.PJCoordSys;
            if (pjCoordsys != null)
            {
                strSPJName = pjCoordsys.Name;
                Marshal.ReleaseComObject(pjCoordsys);
            }

            // 返回坐标系描述
            return strSPJName;
        }
        #endregion

        #region 设置投影
        /// <summary>
        /// 设置投影
        /// 于喜江 2007-7-2
        /// </summary>
        /// <param name="spjbt"></param>
        /// <returns></returns>
        public static string strsePJObjectType(sePJObjectType spjbt)
        {
            try
            {
                string strUnitName = "";
                switch (spjbt)
                {
                    case sePJObjectType.scPRJ_PLATE_CARREE:
                        strUnitName = "Plate Carree";
                        break;
                    case sePJObjectType.scPRJ_MERCATOR:
                        strUnitName = "Mercator";
                        break;
                    case sePJObjectType.scPRJ_GAUSS_KRUGER:
                        strUnitName = "Gauss-Kruger";
                        break;
                    case sePJObjectType.scPRJ_ALBERS:
                        strUnitName = "Albers";
                        break;
                    case sePJObjectType.scPRJ_ORTHO_GRAPHIC:
                        strUnitName = "ortho_graphic 正射";
                        break;
                    case sePJObjectType.scPRJ_CHINA_AZIMUTHAL:
                        strUnitName = "中国全图方位投影";
                        break;
                }
                if (strUnitName == "") return "其他未知投影类型";
                return strUnitName;
            }
            catch
            {
                return "";
            }
        }
        #endregion 

        #region 得到某个supermap 下的投影坐标描述
        /// <summary>
        /// 得到某个supermap 下的投影坐标描述。
        /// </summary>
        /// <param name="pnt">原始坐标</param>
        /// <param name="objsupermap">超图控件</param>
        /// <returns>不成功返回 Empty</returns>
        public static string GetProjectionCoord(soPoint pnt, AxSuperMap objsupermap)
        {
            string strCoord = string.Empty;
            soPoint temppnt = new soPoint();
            temppnt.x = pnt.x;
            temppnt.y = pnt.y;

            soPJCoordSys pjCoordsys = objsupermap.PJCoordSys;
            if (pjCoordsys != null)
            {
                if (pjCoordsys.IsProjected)
                {
                    // 如果是地理坐标，要转换
                    if (pjCoordsys.CoordUnits == seUnits.scuDegree)
                    {
                        if (pjCoordsys.Type == sePJCoordSysType.scPCS_LONGITUDE_LATITUDE)//如果是就返回Empty
                        {
                            Marshal.ReleaseComObject(pjCoordsys);
                            pjCoordsys = null;
                            Marshal.ReleaseComObject(temppnt);
                            temppnt = null;
                            return string.Empty;
                        }
                        else
                            pjCoordsys.Forward(temppnt);
                    }
                }
                Marshal.ReleaseComObject(pjCoordsys);
            }

            strCoord = temppnt.x.ToString(".###") + "," + temppnt.y.ToString(".###");
            Marshal.ReleaseComObject(temppnt);
            return strCoord;
        }

        /// <summary>
        /// 将点的X/Y值转换为源投影坐标描述值
        /// </summary>
        /// <param name="pnt">点对象</param>
        /// <param name="pjCoordsys">目标投影</param>
        /// <returns>失败返回Empty</returns>
        public static string GetProjectionCoord(soPoint pnt, soPJCoordSys pjCoordsys)
        {
            if (pnt == null || pjCoordsys == null) return string.Empty;

            string strCoord = string.Empty;
            soPoint temppnt = new soPoint();
            temppnt.x = pnt.x;
            temppnt.y = pnt.y;

            if (pjCoordsys != null)
            {
                if (pjCoordsys.IsProjected)
                {
                    // 如果是地理坐标，要转换
                    if (pjCoordsys.CoordUnits != seUnits.scuDegree)
                    {
                        if (pjCoordsys.Type == sePJCoordSysType.scPCS_LONGITUDE_LATITUDE)//如果是就返回Empty
                        {
                            Marshal.ReleaseComObject(temppnt);
                            temppnt = null;
                            return string.Empty;
                        }
                        else
                            pjCoordsys.Forward(temppnt);
                    }
                }
            }

            strCoord = temppnt.x.ToString(".###") + "," + temppnt.y.ToString(".###");
            Marshal.ReleaseComObject(temppnt);
            temppnt = null;
            return strCoord;
        }
        #endregion 

        #region 得到某个supermap 下的投影坐标描述
        /// <summary>
        /// 得到某个supermap 下的投影坐标描述。
        /// </summary>
        /// <param name="pnt">原始坐标</param>
        /// <param name="objsupermap">超图控件</param>
        /// <returns>不成功返回 Empty</returns>
        public static string GetGeoCoord(soPoint pnt, AxSuperMap objsupermap)
        {
            string strCoord = string.Empty;

            soPJCoordSys pjCoordsys = objsupermap.PJCoordSys;
            if (pjCoordsys != null)
            {
                // 这里一定要清楚,投影坐标系和经纬度坐标系,这里都为 true. 所以下面的判断没有那么复杂.
                if (pjCoordsys.IsProjected)
                {
                    soPoint temppnt = new soPoint();
                    temppnt.x = pnt.x;
                    temppnt.y = pnt.y;

                    // 如果是地理坐标，要转换
                    if (pjCoordsys.CoordUnits != seUnits.scuDegree)
                    {
                        pjCoordsys.Inverse(temppnt);
                    }

                    strCoord = GetDegressDescription(temppnt.x) + "," + GetDegressDescription(temppnt.y);
                    Marshal.ReleaseComObject(temppnt);
                }
                Marshal.ReleaseComObject(pjCoordsys);
            }

            return strCoord;
        }
        #endregion 

        #region 解析坐标系的详细描述
        /// <summary>
        /// 解析坐标系的详细描述。        
        /// </summary>
        /// <param name="pjsys">坐标系对象</param>
        /// <returns>返回字符串，每一个单项用回车换行</returns>
        public static string CoordSystemDescription(soPJCoordSys pjsys)
        {
            string strPrj = "坐标系信息:" + Environment.NewLine;
            strPrj += "名称:" + pjsys.Name + Environment.NewLine;
            strPrj += "坐标系类型:" + pjsys.Type.ToString() + Environment.NewLine;
            strPrj += "坐标单位:" + pjsys.DistUnits.ToString() + Environment.NewLine;
            if (pjsys.Type == sePJCoordSysType.scPCS_NON_EARTH)
            {
                strPrj += Environment.NewLine + "**平面坐标**，无投影参数。" + Environment.NewLine;
            }
            else if (pjsys.Type == sePJCoordSysType.scPCS_LONGITUDE_LATITUDE)
            {
                // 经纬度坐标系                
                strPrj += "大地参考系:" + pjsys.GeoCoordSys.PJDatum.Name + Environment.NewLine;
                strPrj += "参考椭球体:" + pjsys.GeoCoordSys.PJDatum.PJSpheroid.Name + Environment.NewLine;
                strPrj += "椭球长轴:" + pjsys.GeoCoordSys.PJDatum.PJSpheroid.Axis.ToString() + Environment.NewLine;
                strPrj += "椭球扁率:" + pjsys.GeoCoordSys.PJDatum.PJSpheroid.Flatten.ToString() + Environment.NewLine;
                strPrj += "本初子午线:" + pjsys.GeoCoordSys.PJPrimeMeridian.LongitudeValue.ToString() + Environment.NewLine;
            }
            else
            {
                // 投影坐标系
                strPrj += "投影方式:" + pjsys.Projection.ToString() + Environment.NewLine;
                strPrj += "中央子午线:" + pjsys.PJParams.CentralMeridian.ToString() + Environment.NewLine;
                strPrj += "东偏移:" + pjsys.PJParams.FalseEasting.ToString() + Environment.NewLine;
                strPrj += "北偏移:" + pjsys.PJParams.FalseNorthing.ToString() + Environment.NewLine;
                strPrj += "比例因子:" + pjsys.PJParams.ScaleFactor.ToString() + Environment.NewLine;
                strPrj += "地理坐标系:" + pjsys.GeoCoordSys.Name + Environment.NewLine;
                strPrj += "大地参考系:" + pjsys.GeoCoordSys.PJDatum.Name + Environment.NewLine;
                strPrj += "参考椭球体:" + pjsys.GeoCoordSys.PJDatum.PJSpheroid.Name + Environment.NewLine;
            }
            
            return strPrj;
        }
        #endregion 

        #region 坐标投影
        /// <summary>
        /// 对一个 geometry 进行投影转换
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="pjs"></param>
        /// <param name="pjt"></param>
        /// <returns></returns>
        public static bool CoordTranslator(soGeometry geo,soPJCoordSys pjs,soPJCoordSys pjt)
        {
            // 构造转换器
            soPJTranslator oTransl = new soPJTranslatorClass();
            oTransl.PJCoordSysSrc = pjs;
            oTransl.PJCoordSysDes = pjt;            
            oTransl.TransMethod = sePJTransMethodType.scMethod_GEOCENTRIC_TRANSLATION;
            oTransl.PJParams = new soPJParamsClass();
            if (oTransl.Create() != true)
            {                
                return false;
            }

            bool bresult = oTransl.Convert(geo);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oTransl);
            return bresult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dssrc"></param>
        /// <param name="dstag">必须是Null</param>
        /// <param name="source"></param>
        /// <param name="pjs"></param>
        /// <param name="pjt"></param>
        /// <param name="strresultdsname"></param>
        /// <returns>失败返回False</returns>
        public static bool CoordTranslator(ref soDataset dssrc, ref soDataset dstag, ref soDataSource sourcetag, soPJCoordSys pjs, soPJCoordSys pjt,string strresultdsname)
        {
            soPJTranslator oTransl = new soPJTranslatorClass();
            oTransl.PJCoordSysSrc = pjs;
            oTransl.PJCoordSysDes = pjt;
            oTransl.TransMethod = sePJTransMethodType.scMethod_GEOCENTRIC_TRANSLATION;
            oTransl.PJParams = new soPJParamsClass();
            if (!oTransl.Create()) return false;

            dstag = oTransl.Convert2(dssrc, sourcetag, strresultdsname);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oTransl);
            oTransl = null;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dssrc"></param>
        /// <param name="dstag">必须是Null</param>
        /// <param name="sourcetag"></param>
        /// <param name="pjs"></param>
        /// <param name="pjt"></param>
        /// <param name="strresultdsname"></param>
        /// <returns></returns>
        public static bool CoordTranslator(ref soDatasetVector dssrc, ref soDataset dstag, ref soDataSource sourcetag, soPJCoordSys pjs, soPJCoordSys pjt, string strresultdsname)
        {
            soDataset ds = dssrc as soDataset;
            if (ds == null) return false;
            if (dstag != null) dstag = null;
            return CoordTranslator(ref ds, ref dstag, ref sourcetag, pjs, pjt, strresultdsname);
        }

        /// <summary>
        /// 对一个 点 进行投影转换
        /// </summary>
        /// <param name="geopnt">要转换的点</param>
        /// <param name="pjs">原始坐标系</param>
        /// <param name="pjt">目标坐标系</param>
        /// <returns></returns>
        public static bool CoordTranslator(soPoint geopnt, soPJCoordSys pjs, soPJCoordSys pjt)
        {
            // 构造转换器
            soPJTranslator oTransl = new soPJTranslatorClass();
            oTransl.PJCoordSysSrc = pjs;
            oTransl.PJCoordSysDes = pjt;
            oTransl.TransMethod = sePJTransMethodType.scMethod_GEOCENTRIC_TRANSLATION;
            oTransl.PJParams = new soPJParamsClass();
            if (oTransl.Create() != true)
            {
                return false;
            }

            bool bresult = oTransl.Convert(geopnt);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oTransl);
            return bresult;
        }

        /// <summary>
        /// 对一个 点 进行投影转换
        /// </summary>
        /// <param name="geopnt">要转换的点</param>
        /// <param name="pjs">原始坐标系</param>
        /// <param name="pjt">目标坐标系</param>
        /// <returns></returns>
        public static bool CoordTranslator(soRecordset rcd, soPJCoordSys pjs, soPJCoordSys pjt)
        {
            // 构造转换器
            soPJTranslator oTransl = new soPJTranslatorClass();
            oTransl.PJCoordSysSrc = pjs;
            oTransl.PJCoordSysDes = pjt;
            oTransl.TransMethod = sePJTransMethodType.scMethod_GEOCENTRIC_TRANSLATION;
            oTransl.PJParams = new soPJParamsClass();
            if (oTransl.Create() != true)
            {
                return false;
            }

            bool bresult = oTransl.Convert(rcd);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oTransl);
            return bresult;
        }
        #endregion

        #region 获取投影

        /// <summary>
        /// 获取投影定义
        /// </summary>
        /// <param name="superwksp"></param>
        /// <param name="datasourcealias"></param>
        /// <returns></returns>
        public static soPJCoordSys getPJSys(AxSuperWorkspace superwksp, string datasourcealias)
        {
            soPJCoordSys objsys = null;
            soDataSource objds = getDataSourceFromWorkspaceByName(superwksp, datasourcealias);
            if (objds != null)
                objsys = objds.PJCoordSys;

            if (objds != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objds);
                objds = null;
            }
            return objsys;
        }

        /// <summary>
        /// 获取投影定义
        /// </summary>
        /// <param name="superwksp"></param>
        /// <param name="datasourcealias"></param>
        /// <param name="dtname"></param>
        /// <returns></returns>
        public static soPJCoordSys getPJSys(AxSuperWorkspace superwksp, string datasourcealias, string dtname)
        {
            soPJCoordSys objsys = null;
            soDataset objdt = getDatasetFromWorkspaceByName(dtname, superwksp, datasourcealias);
            if (objdt != null)
                objsys = objdt.PJCoordSys;

            if (objdt != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objdt);
                objdt = null;
            }
            return objsys;
        }


        /// <summary>
        /// 因为超图的投影创建还是比较复杂的，我直接根据字符串构造。
        /// </summary>
        /// <returns></returns>
        public static soPJCoordSys getWGS84PJSystem()
        {
            
            soPJGeoCoordSys geosys = new soPJGeoCoordSysClass();
            geosys.Type = sePJGeoCoordSysType.scGCS_WGS_1984;

            soPJCoordSys pjsys = new soPJCoordSysClass();
            pjsys.Type = sePJCoordSysType.scPCS_LONGITUDE_LATITUDE;
            pjsys.GeoCoordSys = geosys;
            
            ztSuperMap.ReleaseSmObject(geosys);

            return pjsys;
        }

        #endregion

        #region 把数据集从一个数据源转换到另一个数据源
        /// <summary>
        /// 将数据集从一个数据源的投影坐标系转换到另一个数据源到投影坐标系
        /// 转换方法：地心三参法
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strOriDsAlias">源数据源</param>
        /// <param name="strTarDsAlias">目标数据源</param>
        /// <param name="strDtNames">数据集名称集合：如果没有传入数据集的名称集合 就把这个数据源里面到全部数据集都做投影转换</param>
        /// <param name="strTarDtTailMark">目标数据集后缀标示：如果没有就会先删掉目标数据源中对应到数据集然后再做转换</param>
        public static void TranslatorOriDsToTarDs(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strOriDsAlias, string strTarDsAlias, string[] strDtNames, string strTarDtTailMark)
        {
            soDataSources objDss = null;
            soDataSource objDs_Ori = null;
            soDataSource objDs_Tar = null;

            soDatasets objDts = null;

            soPJCoordSys objPJCoordSys_Ori = new soPJCoordSysClass();
            soPJCoordSys objPJCoordSys_Tar = new soPJCoordSysClass();
            soPJTranslator objPJTranslator = new soPJTranslatorClass();
            soPJParams objParams = new soPJParamsClass();

            try
            {
                objDss = objAxSuperWspace.Datasources;
                if (objDss != null)
                {
                    objDs_Ori = objDss[strOriDsAlias];
                    objDs_Tar = objDss[strTarDsAlias];

                    if (objDs_Ori != null && objDs_Tar != null)
                    {
                        //数据集 集合
                        objDts = objDs_Ori.Datasets;

                        if (objDts != null && objDts.Count > 0)
                        {
                            objPJCoordSys_Ori = objDs_Ori.PJCoordSys;
                            objPJCoordSys_Tar = objDs_Tar.PJCoordSys;

                            objPJTranslator.PJCoordSysSrc = objPJCoordSys_Ori;
                            objPJTranslator.PJCoordSysDes = objPJCoordSys_Tar;

                            //设置投影参数
                            objPJTranslator.PJParams = objParams;
                            //设置转换方法：地心三参法
                            objPJTranslator.TransMethod = sePJTransMethodType.scMethod_GEOCENTRIC_TRANSLATION;
                            //构造转换器
                            bool bCreatPJTranslator = objPJTranslator.Create();

                            if (bCreatPJTranslator)
                            {
                                if (strDtNames != null && strDtNames.Length > 0)
                                {
                                    foreach (string strDtName in strDtNames)
                                    {
                                        soDataset objDt = objDts[strDtName];

                                        if (objDt != null)
                                        {
                                            //如果没有给定 后缀标示 那就删掉目标数据源中对应到数据集
                                            if (string.IsNullOrEmpty(strTarDtTailMark))
                                            {
                                                bool bDel = objDs_Tar.DeleteDataset(strDtName);
                                            }

                                            //执行投影转换
                                            soDataset objDtTar = objPJTranslator.Convert2(objDt, objDs_Tar, strDtName + "_" + strTarDtTailMark);

                                            if (objDtTar != null)
                                            {
                                                ztSuperMap.ReleaseSmObject(objDtTar);
                                            }

                                            ztSuperMap.ReleaseSmObject(objDt);
                                        }
                                    }
                                }
                                else
                                {
                                    //如果没有传入数据集的名称集合 就把这个数据源里面到全部数据集都做投影转换
                                    if (objDts != null && objDts.Count > 0)
                                    {
                                        for (int iDt = 1; iDt <= objDts.Count; iDt++)
                                        {
                                            soDataset objDt = objDts[iDt];

                                            if (objDt != null)
                                            {
                                                string strDtName = objDt.Name;

                                                //如果没有给定 后缀标示 那就删掉目标数据源中对应到数据集
                                                if (string.IsNullOrEmpty(strTarDsAlias))
                                                {
                                                    bool bDel = objDs_Tar.DeleteDataset(strDtName);
                                                }

                                                //执行投影转换
                                                soDataset objDtTar = objPJTranslator.Convert2(objDt, objDs_Tar, strDtName + "_" + strTarDsAlias);

                                                if (objDtTar != null)
                                                {
                                                    ztSuperMap.ReleaseSmObject(objDtTar);
                                                }

                                                ztSuperMap.ReleaseSmObject(objDt);

                                            }
                                        }

                                    }

                                }
                            }                            
                        }
                    }
                }
            }
            catch { }
            finally
            {
                ztSuperMap.ReleaseSmObject(objParams);
                ztSuperMap.ReleaseSmObject(objPJTranslator);
                ztSuperMap.ReleaseSmObject(objPJCoordSys_Tar);
                ztSuperMap.ReleaseSmObject(objPJCoordSys_Ori);

                ztSuperMap.ReleaseSmObject(objDts);

                ztSuperMap.ReleaseSmObject(objDs_Tar);
                ztSuperMap.ReleaseSmObject(objDs_Ori);
                ztSuperMap.ReleaseSmObject(objDss);
            }

        }

        #endregion



        #endregion

        #region 公共数学方法

        #region 把小数点表示的角度转换成 度 分 秒 描述的字符串
        /// <summary>
        /// 把小数点表示的角度转换成 度 分 秒 描述的字符串
        /// </summary>
        /// <param name="dbldegress"></param>
        /// <returns></returns>
        public static string GetDegressDescription(double dbldegress)
        {
            string strDegress;
            double dblLow;
            int iHigh;
            double dblHigh;

            iHigh = (int)dbldegress;
            strDegress = iHigh.ToString() + "°";
            dblLow = Math.Abs(dbldegress) - Math.Abs(iHigh);
            dblHigh = dblLow * 60;
            iHigh = (int)dblHigh;
            strDegress = strDegress + iHigh.ToString() + "\'";
            dblLow = dblHigh - iHigh;
            dblHigh = dblLow * 60;
            strDegress = strDegress + dblHigh.ToString(".##") + "\"";

            return strDegress;
        }
                #endregion 

        #region HSB色系 转换为 RGB色系
        /// <summary>
        /// HSB色系 转换为 RGB色系
        /// </summary>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="L"></param>
        /// <returns></returns>
        public static Color HSBToColor(float H, float S, float L)
        {
            double HN, SN, LN, RD, GD, BD, V, M, SV, Fract, VSF, Mid1, Mid2;
            int R, G, B;
            int Sextant;
            HN = H / 239;
            SN = S / 240;
            LN = L / 240;

            if (LN < 0.5)
                V = LN * (1.0 + SN);
            else
                V = LN + SN - LN * SN;

            if (V <= 0)
            {
                RD = 0.0;
                GD = 0.0;
                BD = 0.0;
            }
            else
            {
                M = LN + LN - V;
                SV = (V - M) / V;
                HN = HN * 6.0;
                Sextant = (int)HN;
                Fract = HN - Sextant;
                VSF = V * SV * Fract;
                Mid1 = M + VSF;
                Mid2 = V - VSF;
                switch (Sextant)
                {
                    case 0:
                        {
                            RD = V;
                            GD = Mid1;
                            BD = M;
                            break;

                        }
                    case 1:
                        {
                            RD = Mid2;
                            GD = V;
                            BD = M;
                            break;
                        }

                    case 2:
                        {
                            RD = M;
                            GD = V;
                            BD = Mid1;
                            break;
                        }

                    case 3:
                        {
                            RD = M;
                            GD = Mid2;
                            BD = V;
                            break;
                        }

                    case 4:
                        {
                            RD = Mid1;
                            GD = M;
                            BD = V;
                            break;
                        }

                    case 5:
                        {
                            RD = V;
                            GD = M;
                            BD = Mid2;
                            break;
                        }

                    default:
                        {
                            RD = V;
                            GD = Mid1;
                            BD = M;
                            break;
                        }
                }
            }

            if (RD > 1.0) RD = 1.0;
            if (GD > 1.0) GD = 1.0;
            if (BD > 1.0) BD = 1.0;

            R = (int)Math.Round(RD * 255);
            G = (int)Math.Round(GD * 255);
            B = (int)Math.Round(BD * 255);

            return Color.FromArgb(255, R, G, B);

        }
        #endregion 

        #endregion 

        #region 连接oracle
        /// <summary>
        /// 连接orcle数据库，王晟，西元2007年7月27日
        /// </summary>
        /// <param name="objworkspace">工作空间</param>
        /// <param name="strSupermapName">全局数据服务名</param>
        /// <param name="strUserName">用户名</param>
        /// <param name="strGetPassWord">密码</param>
        /// <param name="strAlias">别名</param>
        /// <returns>结果</returns>
        public static soDataSource AddOracleData(AxSuperWorkspace objworkspace, string strSupermapName, string strUserName, string strGetPassWord, string strAlias)
        {
            try
            {
                //string strDataSourceName = "Provider" + "=" + "SQLOLEDB" + ";" + "Driver" + "=" + "SQL Server" + ";" + "SERVER" + "=" + strServerName + ";" + "Database" + "=" + strDatabaseName + ";" + "Caption" + "=" + "testSave1";
                //string strPassword = "UID" + "=" + strUserName + ";" + "PWD" + "=" + strgetPassword;

                string strDataSourceName = "Provider=MSDAORA;server=" + strSupermapName + ";";
                string strPassWord = "UID=" + strUserName + ";pwd=" + strGetPassWord + ";";

                //先判断一下是否打开
                soDataSource objDSFirst = null;
                soError objError = new soErrorClass();
                objDSFirst = objworkspace.OpenDataSourceEx(strDataSourceName, strAlias, seEngineType.sceOraclePlus, false, true, false, true, strPassWord);
                if (objError.LastErrorCode == -13157)
                {
                    ztMessageBox.Messagebox("连接数据库超时", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (objDSFirst != null)
                    {
                        Marshal.ReleaseComObject(objDSFirst);
                        objDSFirst = null;
                    }
                    if (objError != null)
                    {
                        Marshal.ReleaseComObject(objError);
                        objError = null;
                    }
                    return null;
                }
                soDataSource objDS = null;
                if (objDSFirst == null)
                {
                    soDataSources objDSs = objworkspace.Datasources;
                    objDS = objDSs[strAlias];
                    if (objDS == null)
                    {
                        soError objError1 = new soErrorClass();
                        objDS = objworkspace.OpenDataSourceEx(strDataSourceName, strAlias, seEngineType.sceOraclePlus, false, true, false, true, strPassWord);
                        if (objError1.LastErrorCode == -13157)
                        {
                            ztMessageBox.Messagebox("连接数据库超时", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (objDSs != null)
                            {
                                Marshal.ReleaseComObject(objDSs);
                                objDSs = null;
                            }
                            return null;
                        }
                        if (objDS == null)
                        {
                            ztMessageBox.Messagebox("连接数据库失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (objDSs != null)
                            {
                                Marshal.ReleaseComObject(objDSs);
                                objDSs = null;
                            }
                            return null;
                        }
                        if (objError1 != null)
                        {
                            Marshal.ReleaseComObject(objError1);
                            objError1 = null;
                        }
                    }
                    if (objDSs != null)
                    {
                        Marshal.ReleaseComObject(objDSs);
                        objDSs = null;
                    }
                }
                else
                {
                    objDS = objDSFirst;

                }
                if (objDS != null)
                {
                    if (objDSFirst != null)
                    {
                        Marshal.ReleaseComObject(objDSFirst);
                        objDSFirst = null;
                    }
                    return objDS;
                }
            }
            catch
            {
                ztMessageBox.Messagebox("连接数据库失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            return null;
        }



        #endregion

        #region 连接SQL数据
        /// <summary>
        /// 连接SQL数据库,bConnSQL=true表示连接SQL,false表示连接Oracle数据库
        /// </summary>
        /// <param name="objworkspace">工作空间</param>
        /// <param name="strServerName">服务器名</param>
        /// <param name="strDatabaseName">数据库名</param>
        /// <param name="strUserName">用户名</param>
        /// <param name="strgetPassword">密码</param>
        /// <param name="strAlias">数据源名</param>
        /// <returns>连接后的数据源</returns>
        public static soDataSource AddSQLData(AxSuperWorkspace objworkspace, string strServerName, string strDatabaseName, string strUserName, string strgetPassword, string strAlias)
        {
            try
            {
                string strDataSourceName = "Provider" + "=" + "SQLOLEDB" + ";" + "Driver" + "=" + "SQL Server" + ";" + "SERVER" + "=" + strServerName + ";" + "Database" + "=" + strDatabaseName + ";" + "Caption" + "=" + "testSave1";
                string strPassword = "UID" + "=" + strUserName + ";" + "PWD" + "=" + strgetPassword;
                //先判断一下是否打开
                soDataSource objDSFirst = null;
                soError objError = new soErrorClass();
                objDSFirst = objworkspace.OpenDataSourceEx(strDataSourceName, strAlias, seEngineType.sceSQLPlus, false, true, false, true, strPassword);
                if (objError.LastErrorCode == -13157)
                {
                    ztMessageBox.Messagebox("连接数据库超时", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (objDSFirst != null)
                    {
                        Marshal.ReleaseComObject(objDSFirst);
                        objDSFirst = null;
                    }
                    if (objError != null)
                    {
                        Marshal.ReleaseComObject(objError);
                        objError = null;
                    }
                    return null;
                }
                soDataSource objDS = null;
                if (objDSFirst == null)
                {
                    soDataSources objDSs = objworkspace.Datasources;
                    objDS = objDSs[strAlias];
                    if (objDS == null)
                    {
                        soError objError1 = new soErrorClass();
                        objDS = objworkspace.OpenDataSourceEx(strDataSourceName, strAlias, seEngineType.sceSQLPlus, false, true, false, true, strPassword);
                        if (objError1.LastErrorCode == -13157)
                        {
                            ztMessageBox.Messagebox("连接数据库超时", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (objDSs != null)
                            {
                                Marshal.ReleaseComObject(objDSs);
                                objDSs = null;
                            }
                            return null;
                        }
                        if (objDS == null)
                        {
                            ztMessageBox.Messagebox("连接数据库失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (objDSs != null)
                            {
                                Marshal.ReleaseComObject(objDSs);
                                objDSs = null;
                            }
                            return null;
                        }
                        if (objError1 != null)
                        {
                            Marshal.ReleaseComObject(objError1);
                            objError1 = null;
                        }
                    }
                    if (objDSs != null)
                    {
                        Marshal.ReleaseComObject(objDSs);
                        objDSs = null;
                    }
                }
                else
                {
                    objDS = objDSFirst;

                }
                if (objDS != null)
                {
                    if (objDSFirst != null)
                    {
                        Marshal.ReleaseComObject(objDSFirst);
                        objDSFirst = null;
                    }
                    return objDS;
                }
            }
            catch
            {
                ztMessageBox.Messagebox("连接数据库失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            return null;
        }
        #endregion

        #region 常用比例尺类型
        /// <summary>
        /// 中国国家标准比例尺地形图常用比例尺类型
        /// 设置当前工作空间内的地图的比例尺类型
        /// 于喜江增加2007-7-2
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static seStandardScaleType seGetPJObjectType(int i)
        {
            seStandardScaleType sst = 0;
            try
            {
                switch (i)
                {
                    case 500:
                        sst = seStandardScaleType.scsScale500;
                        break;
                    case 1000:
                        sst = seStandardScaleType.scsScale1000;
                        break;
                    case 2000:
                        sst = seStandardScaleType.scsScale2000;
                        break;
                    case 5000:
                        sst = seStandardScaleType.scsScale5000;
                        break;
                    case 10000:
                        sst = seStandardScaleType.scsScale10000;
                        break;
                    case 25000:
                        sst = seStandardScaleType.scsScale25000;
                        break;
                    case 50000:
                        sst = seStandardScaleType.scsScale50000;
                        break;
                    case 100000:
                        sst = seStandardScaleType.scsScale100000;
                        break;
                    case 250000:
                        sst = seStandardScaleType.scsScale250000;
                        break;
                    case 500000:
                        sst = seStandardScaleType.scsScale500000;
                        break;
                    case 1000000:
                        sst = seStandardScaleType.scsScale1000000;
                        break;
                }
                return sst;
            }
            catch
            {
                return sst;
            }
        }
        #endregion

        #region 设置比例尺
        /// <summary>
        /// 设置比例尺
        /// 返回当前地图的比例尺
        /// 于喜江 2007-7-2
        /// </summary>
        /// <param name="sst"></param>
        /// <returns></returns>
        public static string strsePJObjectType(seStandardScaleType sst)
        {
            try
            {
                string strUnitName = "";
                switch (sst)
                {
                    case seStandardScaleType.scsScaleNull:
                        strUnitName = "无效的比例尺";
                        break;
                    case seStandardScaleType.scsScale500:
                        strUnitName = "1:500";
                        break;
                    case seStandardScaleType.scsScale1000:
                        strUnitName = "1:1000";
                        break;
                    case seStandardScaleType.scsScale2000:
                        strUnitName = "1:2000";
                        break;
                    case seStandardScaleType.scsScale5000:
                        strUnitName = "1:5000";
                        break;
                    case seStandardScaleType.scsScale10000:
                        strUnitName = "1:10000";
                        break;
                    case seStandardScaleType.scsScale25000:
                        strUnitName = "1:25000";
                        break;
                    case seStandardScaleType.scsScale50000:
                        strUnitName = "1:50000";
                        break;
                    case seStandardScaleType.scsScale100000:
                        strUnitName = "1:100000";
                        break;
                    case seStandardScaleType.scsScale250000:
                        strUnitName = "1:250000";
                        break;
                    case seStandardScaleType.scsScale500000:
                        strUnitName = "1:500000";
                        break;
                    case seStandardScaleType.scsScale1000000:
                        strUnitName = "1:1000000";
                        break;
                }
                return strUnitName;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 设置单位
        /// <summary>
        /// 设置当前地图显示的单位,返回字符串
        /// 于喜江 2007-7-2
        /// </summary>
        /// <param name="seu"></param>
        /// <returns></returns>
        public static string strGetUnits(seUnits seu)
        {
            try
            {
                string strUnitName = "";
                switch (seu)
                {
                    case seUnits.scuDegree:
                        strUnitName = "度";
                        break;
                    case seUnits.scuMillimeter:
                        strUnitName = "毫米";
                        break;
                    case seUnits.scuCentimeter:
                        strUnitName = "厘米";
                        break;
                    case seUnits.scuInch:
                        strUnitName = "英寸";
                        break;
                    case seUnits.scuDecimeter:
                        strUnitName = "分米";
                        break;
                    case seUnits.scuFoot:
                        strUnitName = "英尺";
                        break;
                    case seUnits.scuYard:
                        strUnitName = "码";
                        break;
                    case seUnits.scuMeter:
                        strUnitName = "米";
                        break;
                    case seUnits.scuKilometer:
                        strUnitName = "千米";
                        break;
                    case seUnits.scuMile:
                        strUnitName = "英里";
                        break;
                }
                return strUnitName;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region  关闭数据源
        /// <summary>
        /// 关闭数据源
        /// 需要传过来一个工作空间和需要关闭数据源的名称
        /// 于喜江 2007-7-2
        /// </summary>
        /// <param name="objworkspace"></param>
        /// <param name="strDataSourceAlias"></param>
        /// <returns></returns>
        public static bool CloseDataSource(AxSuperWorkspace objworkspace, string strDataSourceAlias)
        {
            soDataSources sodDatasources = null;
            soDataSource sodDatasource = null;
            try
            {
                bool bClose = false;
                //关闭数据源的名称
                sodDatasources = objworkspace.Datasources;
                sodDatasource = sodDatasources[strDataSourceAlias];
                if (sodDatasource != null)
                {
                    sodDatasource.Disconnect();
                    bool bRemoveDsource = objworkspace.Datasources.Remove(strDataSourceAlias);
                    bClose = bRemoveDsource;
                    if (bRemoveDsource)
                        objworkspace.Save();
                }
                else
                {
                    bClose = false;
                }

                return bClose;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(sodDatasources);
                ReleaseSmObject(sodDatasource);
            }
        }

        /// <summary>
        /// 关闭数据源
        /// </summary>
        /// <param name="o"></param>
        /// <param name="strAlias"></param>
        public static void CloseDataSource(SuperMapLib.soDataSources o, string strAlias)
        {
            if (strAlias != "")
            {
                SuperMapLib.soDataSource OpenDataSource = o[strAlias];
                OpenDataSource.Disconnect();
                ztSuperMap.ReleaseSmObject(OpenDataSource);
                o.Remove(strAlias);
            }
        }

        /// <summary>
        /// 选择打开的sdb
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string OpenDataSource(AxSuperMapLib.AxSuperWorkspace objAxSuperWorkSpace)
        {
            string strReturn = "";
            OpenFileDialog ddd = new OpenFileDialog();
            ddd.FileName = "";
            ddd.Filter = "SDB+文件(*.sdb)|*.sdb";
            if (ddd.ShowDialog() == DialogResult.OK)
            {
                if (ddd.FileName != "")
                {
                    if (System.IO.File.Exists(ddd.FileName))
                    {
                        string openDataSourceAlias = System.IO.Path.GetFileNameWithoutExtension(ddd.FileName);

                        soDataSource objExitDS = GetDataSource(objAxSuperWorkSpace, openDataSourceAlias);

                        if (objExitDS != null)
                        {
                            MessageBox.Show("存在相同名称的数据源！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ReleaseSmObject(objExitDS);
                            return "";
                        }

                        SuperMapLib.soDataSource dd = objAxSuperWorkSpace.OpenDataSource(ddd.FileName, openDataSourceAlias, seEngineType.sceSDBPlus, false);
                        
                        if (dd != null)
                        {
                            strReturn = openDataSourceAlias;
                            ReleaseSmObject(dd);
                        }
                        else
                        {
                            MessageBox.Show("打开SDB文件出错", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("选择的SDB文件不存在", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("选择的SDB文件名为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ddd = null;
            return strReturn;
        }
                

        #endregion

        #region 获取所有地图名称
        /// <summary>
        /// 从工作空间内获取所有的数据集的名字
        /// </summary>
        /// <param name="objworkspace">传入当前的超图的工作空间</param>
        /// <returns>地图名的数组</returns>
        public static string[] GetMapsName(AxSuperWorkspace objworkspace)
        {
            string[] alMapsName = null;
            soMaps soms = null;
            soms = objworkspace.Maps;
            if (soms != null)
            {
                alMapsName = new string[soms.Count];
                for (int i = 1; i <= soms.Count; i++)
                {
                    if (soms[i].ToString() != null)
                    {
                        alMapsName.SetValue(soms[i].ToString(),i-1);
                    }
                }
                Marshal.ReleaseComObject(soms);
                soms = null;
            }
            return alMapsName;
        }
        #endregion

        #region 获得有效的数据集名称
        public static string GetaAvailableDataSetName(AxSuperWorkspace AxSuperWorkSpace, object strAlias, string strDTName, string strDatasetNamePrefix)
        {   
            soDataSource objDS = null;
            soDataSources objDSS = null;

            try
            {
                if (AxSuperWorkSpace == null) return "";

                objDSS = AxSuperWorkSpace.Datasources;
                objDS = objDSS[strAlias];

                if (objDS == null) return "";

                if (objDS.IsAvailableDatasetName(strDTName))
                {
                    return strDTName;
                }
                else
                {
                    return objDS.GetUnoccupiedDatasetName(strDatasetNamePrefix);
                }
            }
            catch
            {
                return "";
            }
            finally
            {
                ReleaseSmObject(objDS);
                ReleaseSmObject(objDSS);
            }
        }
        #endregion

        #region 删除数据集
        /// <summary>
        /// 删除数据集
        /// </summary>
        /// <param name="objworkspace">工作空间</param>
        /// <param name="strDatasetName">数据集名</param>
        /// <param name="strDatasource">数据源名</param>
        /// <returns>是否删除成功</returns>
        public static bool deleteDataset(AxSuperWorkspace objworkspace, string strDatasetName, string strDatasource)
        {
            bool bDeleteDataset = false;
            try
            {
                soDataSources sodss = objworkspace.Datasources;
                if (sodss != null)
                {
                    soDataSource sods = sodss[strDatasource];
                    if (sods != null)
                    {
                        soDatasets sodts = sods.Datasets;
                        if (sodts != null)
                        {
                            soDataset sodt = sodts[strDatasetName];
                            if (sodt != null)
                            {
                                bDeleteDataset = sods.DeleteDataset(strDatasetName);
                                Marshal.ReleaseComObject(sodt);
                                sodt = null;
                            }
                            Marshal.ReleaseComObject(sodts);
                            sodts = null;
                        }
                        Marshal.ReleaseComObject(sods);
                        sods = null;
                    }
                    Marshal.ReleaseComObject(sodss);
                    sodss = null;
                }
                return bDeleteDataset;
            }
            catch
            {
                return bDeleteDataset;
            }
        }


        /// <summary>
        /// 删除数据源中的数据集
        /// </summary>
        /// <param name="objworkspace">工作空间</param>
        /// <param name="strDatasource">数据源名</param>
        /// <returns>是否删除成功</returns>
        public static bool deleteDataset(AxSuperWorkspace objworkspace, string strDatasource)
        {
            bool bDeleteDataset = false;
            try
            {
                soDataSources sodss = objworkspace.Datasources;
                if (sodss != null)
                {
                    soDataSource sods = sodss[strDatasource];
                    if (sods != null)
                    {
                        soDatasets sodts = sods.Datasets;
                        if (sodts != null)
                        {
                            for (int i = 1; i <= sodts.Count; i++)
                            {
                                soDataset sodt = sodts[i];
                                if (sodt != null)
                                {
                                    string strDatasetName = sodt.Name;

                                    bDeleteDataset = sods.DeleteDataset(strDatasetName);
                                    
                                    Marshal.ReleaseComObject(sodt);
                                    sodt = null;
                                }
                            }
                            Marshal.ReleaseComObject(sodts);
                            sodts = null;
                        }
                        Marshal.ReleaseComObject(sods);
                        sods = null;
                    }
                    Marshal.ReleaseComObject(sodss);
                    sodss = null;
                }
                return bDeleteDataset;
            }
            catch
            {
                return bDeleteDataset;
            }
        }

        #endregion

        #region 删除地图
        /// <summary>
        /// 删除地图
        /// </summary>
        /// <param name="objworkspace">工作空间</param>
        /// <param name="strMapName">地图名称</param>
        /// <returns>是否删除成功</returns>
        public static bool deleteMap(AxSuperWorkspace objworkspace, string strMapName)
        {
            bool bDeleteMap = false;
            try
            {
                soMaps som = objworkspace.Maps;
                for (int i = 1; i < som.Count; i++)
                {
                    if (som[i].ToString() == strMapName)
                    {
                        bDeleteMap = som.Remove(strMapName);
                    }
                }
                return bDeleteMap;
            }
            catch
            {
                return bDeleteMap;
            }
        }
        #endregion

        #region <<从海舰那边转过来的代码>>

        #region 释放对象
        /// <summary>
        ///释放对象
        /// </summary>
        /// <param name="soObject">supermap的com对象</param>
        public static void ReleaseSmObject(object soObject)
        {
            if (soObject != null && System.Runtime.InteropServices.Marshal.IsComObject(soObject))
            {
                int dd = System.Runtime.InteropServices.Marshal.ReleaseComObject(soObject);
                soObject = null;
            }
        }
        #endregion


        #region 由工作空间返回数据源别名数组
        /// <summary>
        /// 由工作空间返回数据源别名数组
        /// </summary>
        /// <param name="objSuperWorkspace">工作空间名称</param>
        /// <returns>数据源别名数组</returns>
        public static string[] GetDataSourcesAlias(AxSuperMapLib.AxSuperWorkspace objSuperWorkspace)
        {
            if (objSuperWorkspace != null)
            {
                SuperMapLib.soDataSources objDataSources = objSuperWorkspace.Datasources;
                if (objDataSources != null && objDataSources.Count > 0)
                {
                    ArrayList alDataSources = new ArrayList();
                    
                    for (int i = 1; i <= objDataSources.Count; i++)
                    {                        
                        //if (objDataSources[i] != null && objDataSources[i].Opened)
                        //    alDataSources.Add(objDataSources[i].Alias);

                        SuperMapLib.soDataSource objDs = objDataSources[i];
                        if (objDs != null)
                        {
                            if (objDs.Opened)
                            {
                                string strDsAlias = objDs.Alias;
                                alDataSources.Add(strDsAlias);
                            }
                            ReleaseSmObject(objDs);
                        }
                    }

                    ReleaseSmObject(objDataSources);
                    return (string[])alDataSources.ToArray(typeof(string));
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 由工作空间、数据源别名及数据集类型得到相应的数据集名称数组
        /// <summary>
        /// 由工作空间、数据源别名及数据集类型得到相应的数据集名称数组
        /// </summary>
        /// <param name="objSuperWorkspace">工作空间</param>
        /// <param name="strDataSourceAlias">数据源别名</param>
        /// <param name="seDSType">数据集类型</param>
        /// <returns>数据集名称数组</returns>
        public static string[] GetDataSetName(AxSuperMapLib.AxSuperWorkspace objSuperWorkspace, string strDataSourceAlias, SuperMapLib.seDatasetType seDSType)
        {
            if (objSuperWorkspace == null || strDataSourceAlias == null || strDataSourceAlias == "")
            {
                return null;
            }
            else
            {
                SuperMapLib.soDataSource objDataSource = objSuperWorkspace.Datasources[strDataSourceAlias];
                if (objDataSource != null)
                {
                    SuperMapLib.soDatasets objDataSets = objDataSource.Datasets;
                    if (objDataSets != null && objDataSets.Count > 0)
                    {
                        ArrayList alDataSetName = new ArrayList(); ;
                        for (int i = 1; i <= objDataSets.Count; i++)
                        {
                            if (objDataSets[i].Type == seDSType)
                            {
                                alDataSetName.Add(objDataSets[i].Name);
                            }

                        }
                        ReleaseSmObject(objDataSets);
                        ReleaseSmObject(objDataSource);
                        return (string[])alDataSetName.ToArray(typeof(string));
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
        }
        #endregion 

        #region 由工作空间、数据源别名得到相应的数据集名称数组
        /// <summary>
        /// 由工作空间、数据源别名得到相应的数据集名称数组
        /// </summary>
        /// <param name="objSuperWorkspace">工作空间</param>
        /// <param name="strDataSourceAlias">数据源别名</param>
        /// <returns>数据集名称数组</returns>
        public static string[] GetDataSetName(AxSuperMapLib.AxSuperWorkspace objSuperWorkspace, string strDataSourceAlias)
        {
            if (objSuperWorkspace == null || strDataSourceAlias == null || strDataSourceAlias == "")
            {
                return null;
            }
            else
            {
                SuperMapLib.soDataSource objDataSource = objSuperWorkspace.Datasources[strDataSourceAlias];
                if (objDataSource != null)
                {
                    SuperMapLib.soDatasets objDataSets = objDataSource.Datasets;
                    if (objDataSets != null && objDataSets.Count > 0)
                    {
                        ArrayList alDataSetName = new ArrayList(); ;
                        
                        for (int i = 1; i <= objDataSets.Count; i++)
                        {
                            alDataSetName.Add(objDataSets[i].Name);
                        }

                        ReleaseSmObject(objDataSets);
                        ReleaseSmObject(objDataSource);
                        return (string[])alDataSetName.ToArray(typeof(string));
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 由工作空间、数据源别名及数据集类型得到相应的数据集名称数组 2007_10_22 新增
        /// <summary>
        /// 由工作空间、数据源别名及数据集类型得到相应的数据集名称数组 2007_10_22 新增
        /// </summary>
        /// <param name="objSuperWorkspace">工作空间</param>
        /// <param name="strDataSourceAlias">数据源别名</param>
        /// <param name="seDSTypes">数据集类型数组</param>
        /// <returns>数据集名称数组</returns>
        public static string[] GetDataSetName(AxSuperMapLib.AxSuperWorkspace objSuperWorkspace, string strDataSourceAlias, SuperMapLib.seDatasetType[] seDSTypes)
        {
            if (objSuperWorkspace == null || strDataSourceAlias == null || strDataSourceAlias == "")
            {
                return null;
            }
            else
            {
                SuperMapLib.soDataSource objDataSource = objSuperWorkspace.Datasources[strDataSourceAlias];
                if (objDataSource != null)
                {
                    SuperMapLib.soDatasets objDataSets = objDataSource.Datasets;
                    if (objDataSets != null && objDataSets.Count > 0)
                    {
                        ArrayList alDataSetName = new ArrayList(); ;
                        for (int i = 1; i <= objDataSets.Count; i++)
                        {
                            for (int j = 0; j < seDSTypes.Length; j++)
                            {
                                if (objDataSets[i].Type == seDSTypes[j])
                                {
                                    alDataSetName.Add(objDataSets[i].Name);
                                }
                            }
                        }
                        ReleaseSmObject(objDataSets);
                        ReleaseSmObject(objDataSource);
                        return (string[])alDataSetName.ToArray(typeof(string));
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion 

        #region 构造 sql 查询
        /// <summary>
        /// 构造 sql 查询
        /// </summary>
        /// <param name="objDtV"></param>
        /// <param name="frmBase"></param>
        /// <returns></returns>
        public static string GetSQLExpression(soDatasetVector objDtV, IWin32Window frmBase)
        {
            string strSQLExpression = "";
            UI.SQLDialog frmSQLDialog = new UI.SQLDialog(objDtV);
            DialogResult dr = frmSQLDialog.ShowDialog(frmBase);
            if (dr == DialogResult.OK)
            {
                strSQLExpression = frmSQLDialog.StrExpression;
                frmSQLDialog.Dispose();
            }
            if (dr == DialogResult.Cancel)
            {
                frmSQLDialog.Dispose();
            }
            return strSQLExpression;
        }
        #endregion 

        #region 导入
        public static bool PM2SDBInput(string strSDBFileName, string strProjectXMLName,
            AxSuperMapLib.AxSuperWorkspace obj_SuperWorkspace)
        {
            try
            {
                //外业数据的导入-----------------------------------------------------------------
                //前期要求：配图软件已经把PM2转换为SDB；（可考虑改进）---------------------------
                //传入参数：一个SDB文件全名，一个工程全名----------------------------------------
                //处理---------------------------------------------------------------------------
                //输入判
                if (!CheckInputPram(strSDBFileName, strProjectXMLName, obj_SuperWorkspace))
                {
                    MessageBox.Show("输入的参数为空或者输入的工作空间为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //运算准备
                SuperMapLib.soDataSources objDataSources = obj_SuperWorkspace.Datasources;
                SuperMapLib.soDataSource PM2SDB = InputSDBtoWorkspace(strSDBFileName, obj_SuperWorkspace, "PM2SDB");
                if (PM2SDB == null)
                {
                    MessageBox.Show("外业SDB打开失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReleaseSmObject(objDataSources); return false;
                }

                //检查该SDB文件是不是外业数据文件（通过图层名和各图层属性字段检查）
                if (CheckPM2SDB(PM2SDB))
                {

                    if (ifProjectExisted(strProjectXMLName))
                    {
                    }
                    else
                    {
                        MessageBox.Show("请先建立工程", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReleaseSmObject(PM2SDB);
                        ReleaseSmObject(objDataSources);
                        return false;
                    }
                    string strProjectName = System.IO.Path.GetFileName(strProjectXMLName);
                    strProjectName = strProjectName.Substring(0, strProjectName.Length - 4);
                    if (strProjectName == "") return false;
                    string strProjectSDBName = System.IO.Path.GetDirectoryName(strProjectXMLName) + "\\" + strProjectName + ".sdb";
                    if (!ifProjectExisted(strProjectSDBName)) return false;
                    SuperMapLib.soDataSource ProjectSDB = InputSDBtoWorkspace(strProjectSDBName, obj_SuperWorkspace, "ProjectSDB");
                    if (ProjectSDB == null)
                    {
                        MessageBox.Show("工程中的SDB打开失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReleaseSmObject(objDataSources);
                        ReleaseSmObject(PM2SDB);
                        return false;
                    }
                    //导入外业数据各个图层的数据到内业SDB文件对应的图层中。更新对应图层数据；
                    if (InputPM2SDB(PM2SDB, ProjectSDB))
                    {
                        //关闭sdb，手工，走人
                        PM2SDB.Disconnect();
                        obj_SuperWorkspace.Datasources.Remove("PM2SDB");
                        ProjectSDB.Disconnect();
                        obj_SuperWorkspace.Datasources.Remove("ProjectSDB");
                        ReleaseSmObject(PM2SDB);
                        ReleaseSmObject(ProjectSDB);
                    }
                    else
                    {
                        MessageBox.Show("导入失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReleaseSmObject(PM2SDB);
                        ReleaseSmObject(ProjectSDB);
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("该SDB文件不是标准的外业数据文件", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReleaseSmObject(PM2SDB);
                    ReleaseSmObject(obj_SuperWorkspace);
                    return false;
                }

                ReleaseSmObject(objDataSources);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool InputPM2SDB(SuperMapLib.soDataSource PM2SDB, SuperMapLib.soDataSource ProjectSDB)
        {
            try
            {
                //导入外业数据各个图层的数据到内业SDB文件对应的图层中。更新对应图层数据；
                //复制外业SDB中的各层到内业SDB
                bool bolReturn = false;
                SuperMapLib.soDatasets PM2Datasets = PM2SDB.Datasets;
                SuperMapLib.soDatasets ProjectDatasets = ProjectSDB.Datasets;

                SuperMapLib.soDatasetVector PM2DatasetVector = null;
                SuperMapLib.soDatasetVector ProjectDatasetVector = null;
                SuperMapLib.soRecordset PM2Recordset = null;
                for (int i = 1; i <= PM2Datasets.Count; i++)
                {
                    PM2DatasetVector = PM2Datasets[i] as SuperMapLib.soDatasetVector;
                    string strProjectDataset = GetProjectDatasetVectorName(PM2DatasetVector.Name);
                    if (strProjectDataset == "") continue;
                    ProjectDatasetVector = ProjectDatasets[strProjectDataset] as SuperMapLib.soDatasetVector;
                    if (ProjectDatasetVector != null)
                    {
                        PM2Recordset = PM2DatasetVector.Query("smid>0", true, null, "");
                        if (PM2Recordset != null && PM2Recordset.RecordCount > 0)
                        {
                            if (ProjectDatasetVector.Append(PM2Recordset, true))
                            {

                            }
                            else
                            {
                                MessageBox.Show(PM2DatasetVector.Name + "数据集添加错误", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    ReleaseSmObject(PM2Recordset);
                    ReleaseSmObject(PM2DatasetVector);
                    ReleaseSmObject(ProjectDatasetVector);
                }
                ReleaseSmObject(PM2Datasets);
                ReleaseSmObject(ProjectDatasets);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetProjectDatasetVectorName(String strFieldDatasetName)
        {
            //从元数据中得到对应的数据集的名称
            ZTCore.ztDataBase ddd = new ZTCore.ztDataBase();
            return ddd.SelectOnlyOneRowOneField("select 内业数据集名 from 内外业数据集对应表 where 外业数据集名 ='" + strFieldDatasetName + "'");
        }

        private static bool ifProjectExisted(string strProjectXMLName)
        {
            //判断工程是否存在
            return System.IO.File.Exists(strProjectXMLName);
        }

        private static bool CheckPM2SDB(SuperMapLib.soDataSource PM2SDB)
        {
            try
            {
                //检查外业准备进来的SDB中的,
                bool bReturn = false;
                ZTCore.ztDataBase dddw = new ZTCore.ztDataBase();
                //检查规则：根据“内外业数据集对应表”中的“外业数据集名”来判断，必须在这里面
                SuperMapLib.soDatasets PM2Datasets = null;
                PM2Datasets = PM2SDB.Datasets;
                if (PM2Datasets != null && PM2Datasets.Count > 0)
                {
                    SuperMapLib.soDataset PM2Dataset = null;
                    for (int i = 1; i <= PM2Datasets.Count; i++)
                    {
                        PM2Dataset = PM2Datasets[i];
                        string PM2DatasetName = PM2Dataset.Name;
                        ReleaseSmObject(PM2Dataset);
                        string strTemp = dddw.SelectOnlyOneRowOneField("select 外业数据集名 from 内外业数据集对应表 where 外业数据集名 ='" + PM2DatasetName + "'");
                        if (strTemp != PM2DatasetName)
                        {
                            bReturn = false;

                            break;
                        }
                        else
                        {
                            bReturn = true;
                        }
                    }
                }
                ReleaseSmObject(PM2Datasets);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 导出
        //把内业工程中对相应图层及相关资料导出为外业数据；
        //传入参数：一个工程xml全名,一个导出SDB名称，传入工作空间
        //建立外业工程文件
        //把工程中对应的图层拷贝到一个SDB中，并把数据集名改外业数据所需的名字，保证满足外业数据所需的数据集名称及字段属性的要求
        //把外业所需的资料文件、影像文件拷贝到外业数据指定的图层/文件夹中；
        public static bool PM2SDBOutput(string strFM2SDBFileName, string strProjectXMLName,
            AxSuperMapLib.AxSuperWorkspace obj_SuperWorkspace, bool ifProjectOpened)
        {
            try
            {
                bool Breturn = false;
                //输入判断
                if (!CheckInputPram(strFM2SDBFileName, strProjectXMLName, obj_SuperWorkspace))
                {
                    MessageBox.Show("输入的参数为空或者输入的工作空间为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //判断工程是不是存在
                if (!ifProjectExisted(strProjectXMLName))
                {
                    MessageBox.Show("请先建立工程", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                SuperMapLib.soDataSource ProjectSDB = null;
                string strXMLName = System.IO.Path.GetFileName(strProjectXMLName);
                string strProjectName = strXMLName.Substring(0, strXMLName.Length - 4);
                //得到工程SDB
                if (ifProjectOpened)
                {
                    //工程已经打开了，从工作空间中得到数据源
                    ProjectSDB = obj_SuperWorkspace.Datasources[strProjectName];
                }
                else
                {
                    //工程没有打开，用工作空间直接打开数据源
                    string strProjectSDBPath = System.IO.Path.GetDirectoryName(strProjectXMLName) + "\\" + strProjectName + ".sdb";
                    ProjectSDB = InputSDBtoWorkspace(strProjectSDBPath, obj_SuperWorkspace, "ProjectSDB");
                }
                if (ProjectSDB == null)
                {
                    MessageBox.Show("打开工程SDB失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //创建导出的SDB
                SuperMapLib.soDataSource PM2SDB = null;
                PM2SDB = CreatePM2SDBbyName(strFM2SDBFileName, obj_SuperWorkspace, "PM2SDB");
                if (PM2SDB == null)
                {
                    MessageBox.Show("创建导出SDB失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ReleaseSmObject(ProjectSDB);
                    return false;
                }
                //对应数据集，对应字段创建数据集及copy数据
                Breturn = CreateDatasetAndCopyDataByMeta(PM2SDB, ProjectSDB);

                //释放变量
                if (PM2SDB.Disconnect())
                {
                    obj_SuperWorkspace.Datasources.Remove("PM2SDB");
                    ReleaseSmObject(PM2SDB);
                }
                if (!ifProjectOpened)
                {
                    if (ProjectSDB.Disconnect())
                    {
                        obj_SuperWorkspace.Datasources.Remove("ProjectSDB");
                        ReleaseSmObject(ProjectSDB);
                    }
                }
                return Breturn;
            }
            catch
            {
                return false;
            }
        }

        private static SuperMapLib.soDataSource CreatePM2SDBbyName(string strFM2SDBFileName, AxSuperMapLib.AxSuperWorkspace obj_SuperWorkspace, string p)
        {
            //soErrorClass objErr = new soErrorClass();
            bool Del_Exitsted_SDB = true;
            try
            {
                if (System.IO.File.Exists(strFM2SDBFileName))
                {
                    System.IO.File.Delete(strFM2SDBFileName);
                }
                if (System.IO.File.Exists(strFM2SDBFileName.Substring(0, strFM2SDBFileName.Length - 3) + "sdd"))
                {
                    System.IO.File.Delete(strFM2SDBFileName.Substring(0, strFM2SDBFileName.Length - 3) + "sdd");
                }
            }
            catch
            {
                Del_Exitsted_SDB = false;
            }
            if (Del_Exitsted_SDB)
            {
                obj_SuperWorkspace.Datasources.Remove(p);
                return obj_SuperWorkspace.CreateDataSource(strFM2SDBFileName, p, SuperMapLib.seEngineType.sceSDBPlus, false, false, false, "");
            }
            else
            {
                return null;
            }
        }

        private static bool CreateDatasetAndCopyDataByMeta(SuperMapLib.soDataSource PM2SDB, SuperMapLib.soDataSource ProjectSDB)
        {
            try
            {
                //传入两个数据源
                //CreateDatasets
                bool bolReturn = false;
                SuperMapLib.soDatasets PM2Datasets = PM2SDB.Datasets;
                SuperMapLib.soDatasets ProjectDatasets = ProjectSDB.Datasets;

                SuperMapLib.soDatasetVector PM2DatasetVector = null;
                SuperMapLib.soDatasetVector ProjectDatasetVector = null;

                for (int i = 1; i <= ProjectDatasets.Count; i++)
                {
                    ProjectDatasetVector = ProjectDatasets[i] as SuperMapLib.soDatasetVector;

                    if (ProjectDatasetVector != null)//&& ProjectDatasetVector.RecordCount > 0)
                    {
                        string strPM2DatasetName = GetPM2DatasetVectorName(ProjectDatasetVector.Name);
                        if (strPM2DatasetName == "") { ReleaseSmObject(ProjectDatasetVector); continue; }
                        string strProjectDatasetAlias = IfProjectDatasetWillOutPut(ProjectDatasetVector.Name);
                        if (strProjectDatasetAlias == "") { ReleaseSmObject(ProjectDatasetVector); continue; }
                        string strMetaTableName = GetProjectDatasetNameByAlias(strProjectDatasetAlias);
                        if (strMetaTableName == "") { ReleaseSmObject(ProjectDatasetVector); continue; }

                        PM2DatasetVector = PM2SDB.CreateDatasetFrom(strPM2DatasetName, ProjectDatasetVector) as SuperMapLib.soDatasetVector;

                        SuperMapLib.soStrings ProjiectsoStrings = new SuperMapLib.soStringsClass();
                        SuperMapLib.soFieldInfos ProjectFielsinfos = ProjectDatasetVector.GetFieldInfos();
                        SuperMapLib.soFieldInfo ProjectFieldInfo = null;
                        if (ProjectFielsinfos != null && ProjectFielsinfos.Count > 0)
                        {
                            for (int iField = 1; iField <= ProjectFielsinfos.Count; iField++)
                            {
                                ProjectFieldInfo = ProjectFielsinfos[iField];
                                ProjiectsoStrings.Add(ProjectFieldInfo.Name);
                                ReleaseSmObject(ProjectFieldInfo);
                            }
                            ReleaseSmObject(ProjectFielsinfos);
                        }
                        if (PM2DatasetVector != null)
                        {
                            if (!DeletePM2DatasetVector(strMetaTableName, PM2DatasetVector, ProjiectsoStrings))
                            {
                                bolReturn = false;
                                ReleaseSmObject(PM2DatasetVector);
                                ReleaseSmObject(ProjectDatasetVector);
                                break;
                            }

                            SuperMapLib.soRecordset ProjectRecordset = null;

                            ProjectRecordset = ProjectDatasetVector.Query("smid>0", true, null, "");
                            if (ProjectRecordset != null && ProjectRecordset.RecordCount > 0)
                            {
                                if (PM2DatasetVector.Append(ProjectRecordset, true))
                                {
                                }
                                else
                                {
                                }
                                ReleaseSmObject(ProjiectsoStrings);
                                ReleaseSmObject(ProjectRecordset);
                            }
                            bolReturn = true;
                        }
                    }
                    ReleaseSmObject(PM2DatasetVector);
                    ReleaseSmObject(ProjectDatasetVector);
                }
                ReleaseSmObject(PM2Datasets);
                ReleaseSmObject(ProjectDatasets);
                return bolReturn;
            }
            catch
            {
                return false;
            }
        }

        private static string GetPM2DatasetVectorName(string p)
        {
            try
            {
                ZTCore.ztDataBase ddd = new ZTCore.ztDataBase();
                return ddd.SelectOnlyOneRowOneField("select 外业数据集名 from 内外业数据集对应表 where 内业数据集名 ='" + p + "'");
            }
            catch
            {
                return "";
            }
        }

        private static bool DeletePM2DatasetVector(string strProjectDatasetName, SuperMapLib.soDatasetVector PM2DatasetVector,
            SuperMapLib.soStrings ProjiectsoStrings)
        {
            ZTCore.ztDataBase ddd = new ZTCore.ztDataBase();
            DataTable t = null;
            try
            {
                //删除PM2DatasetVector中不需要的字段
                string DeleteFieldName = "";
                t = ddd.SelectDataTable("select 字段代码 from " + strProjectDatasetName + " where 是否导出外业 ='0'");
                if (t.Rows.Count > 0)
                {
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        DeleteFieldName = t.Rows[i][0].ToString();
                        PM2DatasetVector.DeleteField(DeleteFieldName);
                        for (int j = 1; j <= ProjiectsoStrings.Count; j++)
                        {
                            if (DeleteFieldName.ToLower() == ProjiectsoStrings[j].ToLower())
                            {
                                ProjiectsoStrings.Remove(j, 1);
                                break;
                            }
                        }
                    }
                }
                t = null;
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "select 字段代码 from " + strProjectDatasetName + " where 是否导出外业 ='0'", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                t = null;
                return false;
            }
        }
        private static void CopyDataByDatasetVector(SuperMapLib.soDatasetVector ToDatasetVector,
            SuperMapLib.soDatasetVector FromDatasetVector, string TableName)
        {
            try
            {
                SuperMapLib.soRecordset FromRecordset = FromDatasetVector.Query("smid>0", true, null, "");
                if (FromRecordset != null && FromRecordset.RecordCount > 0)
                {
                    SuperMapLib.soGeometry FromGeometry = null;

                    SuperMapLib.soRecordset ToRecordset = ToDatasetVector.Query("smid<0", true, null, "");
                    if (ToRecordset == null) return;

                    for (int i = 1; i <= FromRecordset.RecordCount; i++)
                    {
                        FromGeometry = FromRecordset.GetGeometry();
                        if (FromGeometry != null)
                        {
                            ToRecordset.AddNew(FromGeometry, new object());
                            ToRecordset.Update();

                            CopyFieldValues(ToRecordset, FromRecordset, TableName);
                        }
                        ReleaseSmObject(FromGeometry);
                        FromRecordset.MoveNext();

                    }
                    ToRecordset.Close();
                    ReleaseSmObject(ToRecordset);
                }
                FromRecordset.Close();
                ReleaseSmObject(FromRecordset);
            }
            catch
            { 
                
            }
        }



        private static void CopyFieldValues(SuperMapLib.soRecordset soRsFrom, SuperMapLib.soRecordset soRsTo, string TableName)
        {
            try
            {
                ZTCore.ztDataBase ddd = new ZTCore.ztDataBase();
                DataTable t = ddd.SelectDataTable("select 字段代码 from " + TableName + " where 是否导出外业 ='1'");
                if (t.Rows.Count > 0)
                {
                    soRsTo.Edit();

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        try
                        {
                            soRsTo.SetFieldValue(t.Rows[i][0].ToString(), soRsFrom.GetFieldValue(t.Rows[i][0].ToString()));
                        }
                        catch (InvalidCastException ex)
                        {
                            MessageBox.Show("属性复制过程中,对应的字段类型不能转换!异常信息:" + ex.Message);
                            continue;
                        }
                    }

                }
                soRsTo.Update();
                t = null;
            }
            catch
            { }
        }
        private static string GetProjectDatasetNameByAlias(string strProjectDatasetAlias)
        {
            //从元数据中得到对应的数据集的名称
            ZTCore.ztDataBase ddd = new ZTCore.ztDataBase();
            return ddd.SelectOnlyOneRowOneField("select 属性表名 from 要素表 where 层名称 ='" + strProjectDatasetAlias + "'");
        }
        private static string IfProjectDatasetWillOutPut(string p)
        {
            //从元数据中得到对应的数据集的名称
            try
            {
                ZTCore.ztDataBase ddd = new ZTCore.ztDataBase();
                return ddd.SelectOnlyOneRowOneField("select 内业数据集名 from 内外业数据集对应表 where 内业数据集名 ='" + p + "'");
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 公用方法
        private static bool CheckInputPram(string one, string two, AxSuperMapLib.AxSuperWorkspace o)
        {
            if (one == string.Empty) return false;
            if (two == string.Empty) return false;
            if (o == null) return false;
            return true;
        }
        private static SuperMapLib.soDataSource InputSDBtoWorkspace(string strSDBName, AxSuperMapLib.AxSuperWorkspace objSuperWorkspace, string strSDBalias)
        {
            //加载SDB到工作空间中
            objSuperWorkspace.Datasources.Remove(strSDBalias);
            return objSuperWorkspace.OpenDataSource(strSDBName, strSDBalias, SuperMapLib.seEngineType.sceSDBPlus, false);
        }
        #endregion

        #endregion

        #region << 字段类型转换 >>
        /*-----------------------------------------------------------------------------------
         * 可以用在 字段类型显示为下拉 框内容，下拉框中添加 Item ：
            布尔型
            字节型
            整型
            长整型
            货币型
            单精度型
            双精度型
            日期型
            二进制类型
            文本型
            长二进制型
            备注型
            字符型
            数值型
            时间型
            宽字符型
            几何对象型
            Dgn图形连接类型
        *----------------------------------------------------------------------------------*/

        /// <summary>
        ///根据数据类型获取字符串 
        /// </summary>
        /// <param name="fieldtype"></param>
        /// <returns></returns>
        public static string getFiledTypeString(seFieldType fieldtype)
        {
            string strType = "非法类型";
            switch (fieldtype)
            {
                case seFieldType.scfBoolean:
                    strType = "布尔型";
                    break;
                case seFieldType.scfByte:
                    strType = "字节型";
                    break;
                case seFieldType.scfInteger:
                    strType = "整型";
                    break;
                case seFieldType.scfLong:
                    strType = "长整型";
                    break;
                case seFieldType.scfCurrency:
                    strType = "货币型";
                    break;
                case seFieldType.scfSingle:
                    strType = "单精度型";
                    break;
                case seFieldType.scfDouble:
                    strType = "双精度型";
                    break;
                case seFieldType.scfDate:
                    strType = "日期型";
                    break;
                case seFieldType.scfBinary:
                    strType = "二进制类型";
                    break;
                case seFieldType.scfText:
                    strType = "文本型";
                    break;
                case seFieldType.scfLongBinary:
                    strType = "长二进制型";
                    break;
                case seFieldType.scfMemo:
                    strType = "备注型";
                    break;
                case seFieldType.scfChar:
                    strType = "字符型";
                    break;
                case seFieldType.scfNumeric:
                    strType = "数值型";
                    break;
                case seFieldType.scfTime:
                    strType = "时间型";
                    break;
                case seFieldType.scfNchar:
                    strType = "宽字符型";
                    break;
                case seFieldType.scfGeometry:
                    strType = "几何对象型";
                    break;
                case seFieldType.scfDgnLink:
                    strType = "Dgn图形连接类型";
                    break;
                default:
                    strType = "非法类型";
                    break;
            }

            return strType;
        }


        /// <summary>
        /// 根据字符串返回字段类型
        /// </summary>
        /// <param name="fiedtypename"></param>
        /// <returns></returns>
        public static seFieldType getFiledTypeFromString(string fiedtypename)
        {
            if (fiedtypename == "布尔型")
                return seFieldType.scfBoolean;
            else if (fiedtypename == "字节型")
                return seFieldType.scfByte;
            else if (fiedtypename == "整型")
                return seFieldType.scfInteger;
            else if (fiedtypename == "长整型")
                return seFieldType.scfLong;
            else if (fiedtypename == "货币型")
                return seFieldType.scfCurrency;
            else if (fiedtypename == "单精度型")
                return seFieldType.scfSingle;
            else if (fiedtypename == "双精度型")
                return seFieldType.scfDouble;
            else if (fiedtypename == "日期型")
                return seFieldType.scfDate;    
            else if (fiedtypename == "二进制类型")
                return seFieldType.scfBinary;                    
            else if (fiedtypename == "长二进制型")
                return seFieldType.scfLongBinary;        
            else if (fiedtypename == "备注型")
                return seFieldType.scfMemo;        
            else if (fiedtypename == "字符型")
                return seFieldType.scfChar;        
            else if (fiedtypename == "数值型")
                return seFieldType.scfNumeric;        
            else if (fiedtypename == "时间型")
                return seFieldType.scfTime;        
            else if (fiedtypename == "宽字符型")
                return seFieldType.scfNchar;        
            else if (fiedtypename == "几何对象型")
                return seFieldType.scfGeometry;        
            else if (fiedtypename == "Dgn图形连接类型")
                return seFieldType.scfDgnLink;       
            else
                // 缺省为文本类型
                return seFieldType.scfText;
        }

        #endregion

        #region << 从二调 SDBOperate 代码中来>>

        /// <summary>
        /// 拷贝整个数据源的数据
        /// </summary>
        /// <param name="objSourceDS"></param>
        /// <param name="objDestDs"></param>
        /// <returns></returns>
        public static Boolean CopyDataSource(soDataSource objSourceDS, soDataSource objDestDs)
        {
            int nRecCount = 0;
            soDatasets objDts = objSourceDS.Datasets;
            Boolean bResult = true;
            for (int i = 1; i <= objDts.Count; i++)
            {
                soDataset objdt = objDts[i];
                if (objdt.Vector)
                {
                    soDatasetVector objdtv = (soDatasetVector)objdt;
                    if (nRecCount < objdtv.RecordCount)
                    {
                        nRecCount = objdtv.RecordCount;
                    }
                    objdtv = null;
                }
                try
                {
                    objDestDs.CopyDataset(objdt, objdt.Name, true, seEncodedType.scEncodedNONE);
                }
                catch
                {
                    bResult = false;
                    break;
                }
                finally
                {
                    Marshal.ReleaseComObject(objdt);
                    objdt = null;
                }
            }
            Marshal.ReleaseComObject(objDts);
            objDts = null;
            return bResult;
        }

        /// <summary>
        /// 对数据集重新建立索引
        /// </summary>
        /// <param name="spwsWorkspace"></param>
        /// <param name="strDatasourceAlias"></param>
        /// <param name="strDatasetName"></param>
        /// <param name="bShowProgress"></param>
        /// <returns></returns>
        public static bool ReIndexDataset(AxSuperWorkspace spwsWorkspace, string strDatasourceAlias, string strDatasetName, Boolean bShowProgress)
        {
            soDataset objDataset = getDatasetFromWorkspaceByName(strDatasetName,spwsWorkspace,strDatasourceAlias);
            if (objDataset == null || !objDataset.Vector)
            {
                ReleaseSmObject(objDataset); objDataset = null;                
                return false;
            }

            soDatasetVector objDatasetVector = (soDatasetVector)objDataset;
            Boolean bSuccess = true;
            try
            {
                objDatasetVector.BuildSpatialIndex(bShowProgress);
            }
            catch
            {
                bSuccess = false;
            }

            ReleaseSmObject(objDataset); objDataset = null;            
            objDatasetVector = null;
            
            return bSuccess;
        }

        /// <summary>
        /// 重新计算数据集范围
        /// </summary>
        /// <param name="spwsWorkspace"></param>
        /// <param name="strDatasourceAlias"></param>
        /// <param name="strDatasetName"></param>
        /// <returns></returns>
        public static Boolean ReComputeDatasetBound(AxSuperWorkspace spwsWorkspace, String strDatasourceAlias, String strDatasetName)
        {
            soDataset objDataset = getDatasetFromWorkspaceByName(strDatasetName, spwsWorkspace, strDatasourceAlias);
            if (objDataset == null || !objDataset.Vector)
            {
                ReleaseSmObject(objDataset); objDataset = null;                
                return false;
            }
            soDatasetVector objDatasetVector = (soDatasetVector)objDataset;
            Boolean bSuccess = true;
            try
            {
                objDatasetVector.ComputeBounds();
            }
            catch
            {
                bSuccess = false;
            }
            ReleaseSmObject(objDataset); objDataset = null;            
            objDatasetVector = null;

            return bSuccess;
        }

        #endregion

        #region 判断当前的数据源是否是影像插件驱动的数据源
        /// <summary>
        /// 功能描述:判断当前的数据源是否是影像插件驱动的数据源
        /// </summary>
        /// <param name="AxobjSMWorkSpace">工作空间</param>
        /// <param name="strAlias">数据源别名</param>
        /// <returns>是否是影像数据源</returns>
        public static bool IsImageDataSource(AxSuperMapLib.AxSuperWorkspace AxobjSMWorkSpace, string strAlias)
        {
            soDataSource objCurDS = null;
            soDataSources objDSS = null;

            try
            {
                objDSS = AxobjSMWorkSpace.Datasources;
                objCurDS = objDSS[strAlias];

                if (objCurDS.EngineType == seEngineType.sceImagePlugins)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objCurDS);
                ReleaseSmObject(objDSS);
            }
        }
        #endregion 

        #region 判断当前的数据源是否是影像插件驱动的数据源
        /// <summary>
        /// 功能描述:判断当前的数据源是否是影像插件驱动的数据源
        /// </summary>
        /// <param name="AxobjSMWorkSpace">工作空间</param>
        /// <param name="strAlias">数据源别名</param>
        /// <param name="strDTName">数据集名称</param>
        /// <returns>是否是影像数据源</returns>
        public static bool IsImageDataSet(AxSuperMapLib.AxSuperWorkspace AxobjSMWorkSpace, string strAlias,string strDTName)
        {
            soDataSource objCurDS = null;
            soDataSources objDSS = null;
            soDatasets objDTS = null;
            soDataset objDT = null;

            try
            {
                objDSS = AxobjSMWorkSpace.Datasources;
                objCurDS = objDSS[strAlias];
                objDTS = objCurDS.Datasets;
                objDT = objDTS[strDTName];

                if (objDT.Type == seDatasetType.scdImage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objCurDS);
                ReleaseSmObject(objDSS);
                ReleaseSmObject(objDTS);
                ReleaseSmObject(objDT);
            }
        }
        #endregion 

        #region  根据数据源标示得到数据源
        /// <summary>
        /// 功能描述:根据数据源标示得到数据源
        /// </summary>
        /// <param name="AxSuperWorkSpace">工作空间控件</param>
        /// <param name="objDSIndex">数据源标示</param>
        /// <returns></returns>
        public static soDataSource GetDataSource(AxSuperWorkspace AxSuperWorkSpace,object objDSIndex)
        {
            soDataSources objDSS = null;
            soDataSource objDS = null;

            try
            {
                objDSS = AxSuperWorkSpace.Datasources;
                objDS = objDSS[objDSIndex];

                return objDS;
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDSS);
            }
        }
        #endregion 

        #region 根据用户给出的字符串列表验证是否有用户需要的字段名称存在
        /// <summary>
        /// 验证用户字段
        /// </summary>
        /// <param name="AxSuperWorkSpace">工作空间</param>
        /// <param name="objAlias">数据源别名</param>
        /// <param name="objDataSet">数据集别名</param>
        /// <param name="objFieldList">字段列表</param>
        /// <returns>是否验证成功</returns>
        public static bool IsValidateUserFields(AxSuperWorkspace AxSuperWorkSpace
            ,object objAlias,object objDataSet,ArrayList objFieldList)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDatasetVector objDT = null;
            soFieldInfo objField = null;
            soFieldInfos objFields = null;

            try
            {
                objDS = ztSuperMap.GetDataSource(AxSuperWorkSpace, objAlias);
                objDTS = objDS.Datasets;
                objDT = objDTS[objDataSet] as soDatasetVector;

                objFields = objDT.GetFieldInfos();

                for (int i = 0; i < objFieldList.Count; i++)
                {
                    objField = objFields[objFieldList[i]];

                    if (objField == null) 
                        return false;

                    ztSuperMap.ReleaseSmObject(objField);
                }

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objDT);
                ztSuperMap.ReleaseSmObject(objField);
                ztSuperMap.ReleaseSmObject(objFields);
            }
        }
        #endregion 

        #region 陈明伟加的一些方法,稍后归类

        /*金字塔模块
         * 功能：计算数据集的最小外接矩形
         *       建立金字塔
         *       删除金字塔
         *       判断是否建立了金字塔
         * 
         *  对于 建立金字塔 删除金字塔 判断是否建立了金字塔
         *  传进 ObjDt 时 应先判断 是否是 Image 影像
         */

        public static Boolean CalculateDtBounds(soDataset objDt) //计算数据集的最小外接矩形
        {
            if (objDt != null)
            {
                Boolean a = objDt.ComputeBounds();
                return a;
            }
            else { return false; }
        }
        public static Boolean BuildPyramid(soDataset objDt) //建立金字塔
        {
            if (objDt.Type == seDatasetType.scdImage)
            {
                soDatasetRaster objDtRaster = (soDatasetRaster)objDt;
                Boolean BPyramid = objDtRaster.BuildPyramid(true);

                Marshal.ReleaseComObject(objDtRaster);


                return BPyramid;
            }
            else { return false; }

        }
        public static Boolean DeletePyramid(soDataset objDt) //删除金字塔
        {
            if (objDt.Type == seDatasetType.scdImage)
            {
                soDatasetRaster objDtRaster = (soDatasetRaster)objDt;
                Boolean DPyramid = objDtRaster.RemovePyramid();
                Marshal.ReleaseComObject(objDtRaster);
                return DPyramid;
            }
            else { return false; }
        }
        public static Boolean TFPyramid(soDataset objDt) //判断是否建立了金字塔
        {
            if (objDt.Type == seDatasetType.scdImage)
            {
                soDatasetRaster objDtRaster = (soDatasetRaster)objDt;
                soDatasetInfo objDtInfo = objDtRaster.GetDatasetInfo();
                Boolean tfbPyramid = objDtInfo.get_Options(seDatasetOption.scoPyramid);

                Marshal.ReleaseComObject(objDtRaster);

                Marshal.ReleaseComObject(objDtInfo);


                return tfbPyramid;
            }
            else { return false; }

        }

        #endregion

        #region 拓扑处理方法

        #region 拓扑处理线
        /// <summary>
        /// 前置条件：原线数据集必须存在
        /// 功能描述:拓扑处理线
        /// 后置条件:拓扑处理后的线数据集
        /// </summary>
        /// <param name="objSuperTopo">Topo控件</param>
        /// <param name="objSuperWorkSpace">工作空间</param>
        /// <param name="strSrcAlias">原数据源名称</param>
        /// <param name="strSrcLineDTName">原线数据集名称</param>
        /// <param name="strOrderAlias">目标数据源名称</param>
        /// <param name="strOrderLineDTName">目标数据集名称</param>
        /// <returns>是否拓扑线成功</returns>
        public static bool CreateTopoLine(AxSuperTopoLib.AxSuperTopo objSuperTopo,AxSuperWorkspace objSuperWorkSpace,string strSrcAlias
            , string strSrcLineDTName, string strOrderAlias, string strOrderLineDTName, double dTolerance)
        {
            soDatasetVector objSrcDtVector = null;
            soDatasetVector objOrderDTVector = null;
            soDataSource objSrcDS = GetDataSource(objSuperWorkSpace, strSrcAlias);
            soDataSource objOrderDS = GetDataSource(objSuperWorkSpace, strSrcAlias);
            soRecordset objSrcRec = null;
            SuperTopoLib.soTopoCheck objTopoCheck = new SuperTopoLib.soTopoCheckClass();
            soDatasetVector objResultDtVector = null;
            soDatasetVector objErrorDtVector = null;

            try
            {
                //如果数据源为空则返回
                if (objSrcDS == null) return false;

                #region 得到拓扑处理目标数据集
                /*如果原始数据集和目标数据集名称不同就创建目标数据集 */
                /* 如果相同就使用原始数据集*/
                if (strSrcLineDTName != strOrderLineDTName)
                {
                    if (objOrderDS.IsAvailableDatasetName(strOrderLineDTName))
                    {
                        objSrcDtVector = getDatasetFromWorkspaceByName(strSrcLineDTName, objSuperWorkSpace, strSrcAlias) as soDatasetVector;

                        if (objSrcDtVector == null) return false;

                        objOrderDS.DeleteDataset(strOrderLineDTName);

                        objOrderDTVector = objOrderDS.CreateDatasetFrom(strOrderLineDTName, objSrcDtVector) as soDatasetVector;

                        if (objOrderDTVector == null) return false;

                        objSrcRec = objSrcDtVector.Query("", true, null, "");

                        objOrderDTVector.Append(objSrcRec, false);
                    }
                    else
                    { return false; }
                }
                //如果相同就直接使用原始数据集
                else
                {
                    objOrderDTVector = getDatasetFromWorkspaceByName(strOrderLineDTName, objSuperWorkSpace, strOrderAlias) as soDatasetVector;
                }

                if (objOrderDTVector == null) return false;
                #endregion

                #region 清除重复线
                
                if (objSuperTopo.CleanRepeatedLines == true)
                {
                    if (dTolerance != 0)
                    {
                        objSuperTopo.CleanRepeatedLines = false;

                        objOrderDS.DeleteDataset("CheckLineError");

                        objTopoCheck.PreprocessData = true;
                        objTopoCheck.Tolerance = dTolerance;
                        objResultDtVector = objTopoCheck.CheckTopoErrorEx(objOrderDTVector, objOrderDTVector, (int)seTopoRule.sctLineNoOverlap, objOrderDS, "CheckLineError");
                        //ReleaseSmObject(objResultDtVector);
                        //objErrorDtVector = getDatasetFromWorkspaceByName("CheckLineError", objSuperWorkSpace, strOrderAlias) as soDatasetVector;

                        bool bClean = objTopoCheck.FixTopoError(objOrderDTVector, objResultDtVector);

                        objOrderDTVector.Close();
                        bool bDel = objOrderDS.DeleteDataset("CheckLineError");
                    }
                }
                #endregion 

                objOrderDTVector.Open();

                //进行拓扑处理
                bool bTopo = objSuperTopo.Clean(objOrderDTVector);

                return bTopo;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objSrcDtVector);
                ReleaseSmObject(objOrderDTVector);
                ReleaseSmObject(objSrcDS);
                ReleaseSmObject(objResultDtVector);
                ReleaseSmObject(objErrorDtVector);
                ReleaseSmObject(objSrcRec);
                ReleaseSmObject(objTopoCheck);
                ReleaseSmObject(objOrderDS);
            }

        }
        #endregion 

        #region 拓扑构面
        /// <summary>
        /// 前置条件：拓扑处理后的线数据集，或者是确定符合拓扑规则的线数据集
        /// 后置条件：拓扑处理后的面数据集
        /// 功能描述: 对拓扑处理后的线进行构面
        /// </summary>
        /// <param name="objSuperTopo">拓扑控件</param>
        /// <param name="objSuperWorkSpace">工作空间</param>
        /// <param name="strAlias">数据源的别名</param>
        /// <param name="strSrcLineDTName">原始线数据集名称</param>
        /// <param name="strOrderRegionDTName">目标面数据集名称</param>
        /// <returns>构面是否成功</returns>
        public static bool CreateTopoRegion(AxSuperTopoLib.AxSuperTopo objSuperTopo, AxSuperWorkspace objSuperWorkSpace, string strSrcAlias
            ,string strOrderAlias, string strSrcLineDTName, string strOrderRegionDTName)
        {
            soDatasetVector objDTSrcLine = null;
            soDatasetVector objDTRegion = null;
            soDataSource objSrcDS = null;
            soDataSource objOrderDS = null;

            try
            {
                objSrcDS = GetDataSource(objSuperWorkSpace, strSrcAlias);
                objOrderDS = GetDataSource(objSuperWorkSpace, strOrderAlias);

                objDTSrcLine = getDatasetFromWorkspaceByName(strSrcLineDTName, objSuperWorkSpace, strSrcAlias) as soDatasetVector;

                if (objDTSrcLine == null) return false;

                objDTRegion = getDatasetFromWorkspaceByName(strOrderRegionDTName, objSuperWorkSpace, strOrderAlias) as soDatasetVector;

                if (objDTRegion != null)
                {
                    objSrcDS.DeleteDataset(strOrderRegionDTName);
                }

                if (!objSrcDS.IsAvailableDatasetName(strOrderRegionDTName))
                {
                    return false;
                }

                bool bBuild = objSuperTopo.BuildPolygons(objDTSrcLine, objOrderDS, strOrderRegionDTName);

                return bBuild;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objDTRegion);
                ReleaseSmObject(objDTSrcLine);
                ReleaseSmObject(objSrcDS);
                ReleaseSmObject(objOrderDS);
            }
        }
        #endregion 

        #region 拓扑线,得到线的左右多边形
        /// <summary>
        /// 功能描述: 拓扑线,得到线的左右多边形
        /// </summary>
        /// <param name="objAxSuperWorkSpace">工作空间</param>
        /// <param name="AxobjSuperTopo">拓扑控件</param>
        /// <param name="strAlias">数据源别名</param>
        /// <param name="strLineDTName">线数据集名称</param>
        /// <param name="strRegionDTName">多边形数据集名称</param>
        /// <param name="bPreProcessdata">是否拓扑预处理</param>
        /// <param name="fTolerance">拓扑容限</param>
        /// <returns></returns>
        public static bool GetLine_LR_Region(AxSuperWorkspace objAxSuperWorkSpace,AxSuperTopo AxobjSuperTopo
            , string strAlias, string strLineDTName, string strRegionDTName, bool bPreProcessdata, double fTolerance)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDatasetVector objLine = null;
            soDatasetVector objRegion = null;

            try
            {
                objDS = ztSuperMap.GetDataSource(objAxSuperWorkSpace, strAlias);

                if (objDS == null) return false;

                objDTS = objDS.Datasets;

                objLine = objDTS[strLineDTName] as soDatasetVector;
                objRegion = objDTS[strRegionDTName] as soDatasetVector;

                if (objLine == null || objRegion == null) return false;

                if (fTolerance > 0)
                {
                    objLine.ToleranceFuzzy = fTolerance;
                    objRegion.ToleranceFuzzy = fTolerance;
                }

                Boolean bResult = AxobjSuperTopo.GetLRPolygon(objRegion, objLine, bPreProcessdata);
                
                return bResult;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objDS);
                ReleaseSmObject(objDTS);
                ReleaseSmObject(objLine);
                ReleaseSmObject(objRegion);
            }
        }
        #endregion

        #region 拓扑检查
        /// <summary>
        /// 执行拓扑错误检查
        /// </summary>
        /// <param name="axSuperWKS">工作空间</param>
        /// <param name="strErrDS_Alias">错误数据源别名</param>
        /// <param name="strErrDTName">错误数据集名称</param>
        /// <param name="objBeCheck">被检查数据集 或者 记录集</param>
        /// <param name="objCompare">参考数据集 或者 记录集</param>
        /// <param name="CheckRule">检查规则</param>
        /// <param name="bTolerance">拓扑检查容限</param>
        /// <param name="bProcess">是否预处理</param>
        /// <param name="bDelExitDT">是否清除已经存在的数据集</param>
        /// <returns>是执行成功</returns>
        public static bool ExecuteTopoCheck(AxSuperWorkspace axSuperWKS, string strErrDS_Alias,string strErrDTName
            , object objBeCheck, object objCompare, seTopoRule CheckRule, double bTolerance, bool bProcess, bool bDelExitDT)
        {
            soTopoCheck objTopoCheck = new soTopoCheckClass();
            soDatasetVector objResultDtVector = null;
            soDataSource objOrderDS = null;

            try
            {
                objOrderDS = GetDataSource(axSuperWKS, strErrDS_Alias);
                
                if (bDelExitDT)
                    objOrderDS.DeleteDataset(strErrDTName);

                if (bTolerance > 0)
                    objTopoCheck.Tolerance = bTolerance;
                
                objTopoCheck.PreprocessData = bProcess;
                objResultDtVector = objTopoCheck.CheckTopoErrorEx(objBeCheck, objCompare, (int)CheckRule, objOrderDS, strErrDTName);

                if (objResultDtVector != null)
                { return true; }
                else
                { return false; }
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objTopoCheck);
                ReleaseSmObject(objResultDtVector);
                ReleaseSmObject(objOrderDS);
            }
        }
        #endregion

        #region 清除重复线

        /// <summary>
        /// 执行清除重复线
        /// </summary>
        /// <param name="axSuperWKS">工作空间</param>
        /// <param name="strDS_Alias">数据源别名</param>
        /// <param name="strErrDTName">错误数据集</param>
        /// <param name="objBeCheckDT">被检查数据集</param>
        /// <param name="objCompareDT">参考数据集</param>
        /// <param name="bTolerance">检查容限</param>
        /// <param name="isSameDTName">是否是在同一个数据集中处理</param>
        /// <returns>是否检查成功</returns>
        public static bool ExecuteRepeatLine(AxSuperWorkspace axSuperWKS, string strDS_Alias, string strErrDTName
            , soDatasetVector objBeCheckDT, soDatasetVector objCompareDT, double bTolerance,bool isSameDTName)
        {
            soTopoCheck objTopoCheck = new soTopoCheckClass();
            soDatasetVector objErrCheckDT = null;

            try
            {
                objTopoCheck.PreprocessData = true;

                seTopoRule objTopoRule = seTopoRule.sctLineNoOverlap;

                if (!isSameDTName)
                { objTopoRule = seTopoRule.sctLineNoOverlapWith; }

                //执行Topo检查
                bool bExeRepeatCheck = ExecuteTopoCheck(axSuperWKS, strDS_Alias, strErrDTName, objBeCheckDT, objCompareDT, objTopoRule, bTolerance, true, true);

                //return bExeRepeatCheck;     // debug beizhan

                //得到检查结果数据集
                if (bExeRepeatCheck)
                {
                    objErrCheckDT = getDatasetFromWorkspaceByName(strErrDTName, axSuperWKS, strDS_Alias) as soDatasetVector;
                }
                else
                { return false; }
                
                //执行清除

                bool bClean = objTopoCheck.FixTopoError(objBeCheckDT, objErrCheckDT);                

                return bClean;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objTopoCheck);
                ReleaseSmObject(objErrCheckDT);
            }
        }

        /// <summary>
        /// 执行清除重复线
        /// </summary>
        /// <param name="objErrDTVector">错误数据集</param>
        /// <param name="objBeCleanDTVector">被清除数据集</param>
        /// <returns>是否清除成功</returns>
        public static bool ExecuteRepeatLine(soDatasetVector objErrDTVector,soDatasetVector objBeCleanDTVector)
        {
            soTopoCheck objTopoCheck = new soTopoCheckClass();

            try
            {
                objTopoCheck.PreprocessData = true;
                //执行清除
                bool bClean = objTopoCheck.FixTopoError(objBeCleanDTVector, objErrDTVector);                

                return bClean;

            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objTopoCheck);
            }
        }


        #endregion

        #endregion

        #region 将 SDB数据集 转换到 Access 中

        /// <summary>
        ///  记录集集 转换到 Access 中
        /// </summary>
        /// <param name="objRs">记录集</param>
        /// <param name="strDBName">MDB Access 全路径名</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="bIncludeSystemField">是否复制 SMID 到 SMUSERID 中</param>
        /// <returns></returns>
        public static String SaveRecordsetToDB(soRecordset objRs, String strDBName, String strTableName, Boolean bIncludeSystemField)
        {
            if (objRs == null || !System.IO.File.Exists(strDBName))
            {
                return "";
            }

            String strInsertFieldList = CreateDatasetTable(objRs, strDBName, strDBName, bIncludeSystemField);
            if (strInsertFieldList == "")
            {
                return "";
            }

            String[] strFieldName = strInsertFieldList.Split(',');
            //此处的DBName必须是绝对路径表示
            System.Data.OleDb.OleDbConnection cnJetDB = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDBName);
            cnJetDB.Open();            // Open Connection 
            System.Data.OleDb.OleDbCommand cmdJetDB = new System.Data.OleDb.OleDbCommand();
            cmdJetDB.Connection = cnJetDB;

            String strSQL = "";
            String strFieldValue = "";
            String strValueStr = "";
            soFieldInfo objFi = null;

            objRs.MoveFirst();
            for (int i = 1; i <= objRs.RecordCount; i++)
            {
                strValueStr = " values(";
                for (int j = 0; j < strFieldName.Length; j++)
                {
                    if (strFieldName[j] == "" || strFieldName[j].Trim() == "")
                    {
                        continue;
                    }
                    strFieldValue = objRs.GetFieldValueText(strFieldName[j].Trim());
                    objFi = objRs.GetFieldInfo(strFieldName[j].Trim());
                    if (objFi == null)
                    {
                        continue;
                    }
                    switch (objFi.Type)
                    {
                        case seFieldType.scfByte:     //数值型
                        case seFieldType.scfCurrency:
                        case seFieldType.scfDouble:
                        case seFieldType.scfInteger:
                        case seFieldType.scfLong:
                        case seFieldType.scfNumeric:
                        case seFieldType.scfSingle:
                        case seFieldType.scfBoolean:     //布尔型
                            strValueStr += strFieldValue.Length == 0 ? "0," : strFieldValue + ",";
                            break;
                        case seFieldType.scfText:     //字符型
                        case seFieldType.scfNchar:
                        case seFieldType.scfMemo:
                        case seFieldType.scfChar:
                            strValueStr += "'" + strFieldValue + "',";
                            break;
                        case seFieldType.scfDate:     //日期型
                        case seFieldType.scfTime:
                            strValueStr += "#" + (strFieldValue == "" ? "1000-01-01" : strFieldValue) + "#,";
                            break;
                    }
                    ztSuperMap.ReleaseSmObject(objFi); objFi = null;
                }
                strValueStr = strValueStr.Substring(0, strValueStr.Length - 1) + ")";
                strSQL = "INSERT INTO " + strTableName + "(" + strInsertFieldList + ")" + strValueStr;
                cmdJetDB.CommandText = strSQL;
                try
                {
                    cmdJetDB.ExecuteNonQuery();
                }
                catch
                {
                }
                objRs.MoveNext();
            }
            cnJetDB.Close();
            return strTableName;
        }

        /// <summary>
        /// 将 数据源中SDB 数据导入到 Access 中
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strMDBName">Access 全路径名</param>
        /// <param name="strDatasetNames">数据集名</param>
        /// <returns></returns>
        public static Boolean SaveRecordsetToDB(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strDsAlias, String strMDBName, String[] strDatasetNames)
        {
            return SaveRecordsetToDB(objAxSuperWspace, strDsAlias, strMDBName, strDatasetNames, false);
        }

        /// <summary>
        /// 将记录集转到 Access 中
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strMDBName">Access全路径名</param>
        /// <param name="strDatasetNames">数据集名</param>
        /// <param name="bCopySmidToSmUserid">是否复制 SMID 到 SMUSERID 中</param>
        /// <returns></returns>
        public static Boolean SaveRecordsetToDB(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace,string strDsAlias, String strMDBName, String[] strDatasetNames, Boolean bCopySmidToSmUserid)
        {
            soDataSources objDss = objAxSuperWspace.Datasources;
            soDataSource objDs = objDss[strDsAlias];

            if (objDs == null)
            {
                return false;
            }

            String strPath = "";
            String strMDB = "";
            Boolean bFirsMDB = true;
            Boolean bResult = true;
            try
            {
                strPath = System.IO.Path.GetDirectoryName(strMDBName);
                if (!Directory.Exists(strPath)) Directory.CreateDirectory(strPath);
            }
            catch { };

            String strConn1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strMDBName;

            soDatasets objDts = objDs.Datasets;

            String strDtName = "";

            int nDtCount = strDatasetNames == null ? objDts.Count : strDatasetNames.Length;

            System.Data.OleDb.OleDbConnection connDB1 = new System.Data.OleDb.OleDbConnection();
            System.Data.OleDb.OleDbCommand cmdDB1 = new System.Data.OleDb.OleDbCommand();

            //此处的DBName必须是绝对路径表示
            for (int i = 0; i < nDtCount; i++)
            {
                soDataset objDt = null;

                if (strDatasetNames == null)
                {
                    objDt = objDts[i + 1];
                    strDtName = objDt.Name;
                }
                else
                {
                    strDtName = strDatasetNames[i];
                    objDt = objDts[strDtName];
                }
                if (objDt == null || !objDt.Vector)
                {
                    ztSuperMap.ReleaseSmObject(objDt); objDt = null;
                    continue;
                }

                soDatasetVector objDtv = (soDatasetVector)objDt;

                if (bCopySmidToSmUserid)
                {
                    objDtv.CopyField("smid", "smuserid");
                }

                soRecordset objRs = objDtv.Query("", false, null, "");
                soDataPump objDP = objDs.DataPump;
                
                soExportParams objExpParams = objDP.DataExportParams;

                objExpParams.FileType = seFileType.scfMDB;

                if (bFirsMDB)
                {
                    strMDB = strMDBName;
                }
                else
                {
                    strMDB = strPath == "" ? strDtName : strPath + "\\" + strDtName + ".mdb";
                }

                //if (ztFileOperate.IsExistedFile(strMDB))
                if(File.Exists(strMDB))
                {
                    try
                    {
                        System.IO.File.Delete(strMDB);
                    }
                    catch { }
                }
                try
                {
                    if (bFirsMDB)   //第一个则直接导出
                    {
                        ztSuperMap.ReleaseSmObject(objRs); objRs = null;
                        ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;

                        objExpParams.FileName = strMDB;
                        objExpParams.DatasetToBeExported = strDtName;
                        objDP.DataExportParams = objExpParams;

                        bFirsMDB = !objDP.Export();

                    }
                    else if (objRs.RecordCount > 0)
                    {
                        ztSuperMap.ReleaseSmObject(objRs); objRs = null;
                        ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;

                        objExpParams.FileName = strMDB;
                        objExpParams.DatasetToBeExported = strDtName;
                        objDP.DataExportParams = objExpParams;

                        if (objDP.Export())
                        {
                            String strSQL = "select * into " + strDtName +
                                 " from " + strDtName + " in \"" + strMDB + "\"";
                            connDB1.ConnectionString = strConn1;
                            connDB1.Open();
                            System.Data.OleDb.OleDbCommand cmdDB = new System.Data.OleDb.OleDbCommand();
                            cmdDB.Connection = connDB1;
                            cmdDB.CommandText = strSQL;
                            cmdDB.ExecuteNonQuery();
                            connDB1.Close();
                            try
                            {
                                System.IO.File.Delete(strMDB);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        CreateDatasetTable(objRs, strMDBName, strDtName, true);
                        ztSuperMap.ReleaseSmObject(objRs); objRs = null;
                        ztSuperMap.ReleaseSmObject(objDt); objDt = null;
                        ztSuperMap.ReleaseSmObject(objDtv); objDtv = null;
                    }
                }
                catch
                {
                    bResult = false;
                }
                finally
                {
                    ztSuperMap.ReleaseSmObject(objRs); objRs = null;
                    ztSuperMap.ReleaseSmObject(objDt); objDt = null;
                    ztSuperMap.ReleaseSmObject(objDtv); objDtv = null;
                    ztSuperMap.ReleaseSmObject(objExpParams); objExpParams = null;
                    ztSuperMap.ReleaseSmObject(objDP); objDP = null;
                }
            }
            ztSuperMap.ReleaseSmObject(objDts); objDts = null;
            ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            ztSuperMap.ReleaseSmObject(objDss); objDss = null;
            return bResult;
        }

        /// <summary>
        /// 根据 soRecordset 创建 DataTable
        /// </summary>
        /// <param name="objRs">记录集</param>
        /// <param name="strDBName">Access 全路径名</param>
        /// <param name="strTableName">表名</param>
        /// <param name="bIncludeSystemField">是否包含系统字段</param>
        /// <returns></returns>
        public static String CreateDatasetTable(soRecordset objRs, String strDBName, String strTableName, Boolean bIncludeSystemField)
        {
            if (objRs == null || !System.IO.File.Exists(strDBName))
            {
                return "";
            }

            soFieldInfos objFields = objRs.GetFieldInfos();
            DataTable dtResult = new DataTable();
            String[] strFieldName = new String[objFields.Count];
            int[] nFieldType = new int[objFields.Count];
            String strDBFFieldList = "(";
            String strInsertFieldList = "";
            soFieldInfo objFieldInfo;
            int nFieldSize = 0;

            for (int i = 1; i <= objFields.Count; i++)
            {
                objFieldInfo = objFields[i];
                strFieldName[i - 1] = "";
                nFieldType[i - 1] = 0;    //数值型
                if (!bIncludeSystemField && objFieldInfo.Name.Length >= 2 && objFieldInfo.Name.Substring(0, 2).ToLower() == "sm")   //跳过字段
                {
                    ztSuperMap.ReleaseSmObject(objFieldInfo); objFieldInfo = null;
                    continue;
                }
                nFieldSize = objFieldInfo.Size;
                strFieldName[i - 1] = "";
                switch (objFieldInfo.Type)
                {
                    //Access字段类型号 Byte 数字[字节],Long 数字[长整型],Short 数字[整型],Single 数字[单精度,Double 数字[双精度],Currency 货币
                    //Char 文本,Text(n) 文本，其中n表示字段大小,Binary 二进制,Counter 自动编号,Memo 备注,Time 日期/时间
                    case seFieldType.scfDouble:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Double,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        break;
                    case seFieldType.scfCurrency:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Currency,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        break;
                    case seFieldType.scfSingle:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Single,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        break;
                    case seFieldType.scfInteger:
                    case seFieldType.scfNumeric:        //是对应小数吗?或是对应整数?看起来是,过后要验证一下
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Short,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        break;
                    case seFieldType.scfLong:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Long,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        break;
                    case seFieldType.scfText:
                    case seFieldType.scfChar:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " text";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        nFieldType[i - 1] = 1;    //字符串型
                        if (nFieldSize > 0)
                        {
                            strDBFFieldList += "(" + nFieldSize + "),";
                        }
                        break;
                    case seFieldType.scfMemo:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Memo";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        nFieldType[i - 1] = 1;
                        break;
                    case seFieldType.scfByte:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " Byte,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        break;
                    case seFieldType.scfTime:
                    case seFieldType.scfDate:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " time,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        nFieldType[i - 1] = 2;     //日期时间型
                        break;
                    case seFieldType.scfBoolean:
                        strFieldName[i - 1] = objFieldInfo.Name;
                        strDBFFieldList += objFieldInfo.Name + " yesno,";
                        strInsertFieldList += objFieldInfo.Name + ",";
                        nFieldType[i - 1] = 3;     //布尔型
                        break;

                }
                ztSuperMap.ReleaseSmObject(objFieldInfo);
                objFieldInfo = null;
            }

            if (objFields != null)
            {
                ztSuperMap.ReleaseSmObject(objFields);
                objFields = null;
            }
            if (strDBFFieldList.Length <= "(".Length)  //没有字段
            {
                return "";
            }
            if (strDBFFieldList.EndsWith(","))
            {
                strDBFFieldList = strDBFFieldList.Substring(0, strDBFFieldList.Length - 1);
            }
            if (strInsertFieldList.EndsWith(","))
            {
                strInsertFieldList = strInsertFieldList.Substring(0, strInsertFieldList.Length - 1);
            }
            strDBFFieldList += ")";
            //此处的DBName必须是绝对路径表示
            System.Data.OleDb.OleDbConnection cnJetDB = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDBName);
            cnJetDB.Open();            // Open Connection 
            System.Data.OleDb.OleDbCommand cmdJetDB = new System.Data.OleDb.OleDbCommand();
            cmdJetDB.Connection = cnJetDB;

            //如果要创建新表,则试图创建
            try
            {
                cmdJetDB.CommandText = "drop table " + strTableName;
                cmdJetDB.ExecuteNonQuery();
            }
            catch
            {
            }

            try
            {
                cmdJetDB.CommandText = "CREATE TABLE  " + strTableName + strDBFFieldList;
                cmdJetDB.ExecuteNonQuery();
            }
            catch
            {
                strTableName = "";
            }
            cnJetDB.Close();
            if (strTableName.Length == 0)
            {
                return "";
            }
            return strInsertFieldList.Trim();
        }
        

        /// <summary>
        /// 把 Amdb 中的 表 复制到 Bmdb 中
        /// </summary>
        /// <param name="strAMDBName">Amdb文件的绝对路径</param>
        /// <param name="strBMDBName">Bmdb文件的绝对路径</param>
        /// <param name="strDataTableNames">表名集合</param>
        /// <returns></returns>
        public static bool CopyADataTableFromAMDBToBMDB(string strAMDBName, string strBMDBName, string[] strDataTableNames)
        {
            try
            {                
                String strConn1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strAMDBName;

                System.Data.OleDb.OleDbConnection objOLEConn1 = new System.Data.OleDb.OleDbConnection();               
                System.Data.OleDb.OleDbCommand objOLECmd = new System.Data.OleDb.OleDbCommand();

                objOLEConn1.ConnectionString = strConn1;                

                objOLEConn1.Open();                

                foreach (string strTableName in strDataTableNames)
                { 
                    objOLECmd.Connection = objOLEConn1;

                    try
                    {   

                        String strSQLinto = "select * into " + strTableName + " in '" + strBMDBName + "' from " + strTableName;

                        objOLECmd.CommandText = strSQLinto;

                        int iInto = objOLECmd.ExecuteNonQuery();
                    }
                    catch { }

                }

                objOLEConn1.Close();

                return true;
            }
            catch { return false; }


        }



        #endregion

        #region 复制数据集容限
        /// <summary>
        /// 复制数据集容限
        /// </summary>
        /// <param name="objSuperWKS">工作空间</param>
        /// <param name="objSrcAlias">源数据源别名</param>
        /// <param name="objSrcDTName">源数据集名称</param>
        /// <param name="objOrderDSAlias">目标数据源的别名</param>
        /// <param name="objOrderDTName">目标数据集的名称</param>
        /// <returns></returns>
        public static bool CopyDataSetTolerance(AxSuperWorkspace objSuperWKS, string strSrcAlias
            , string strSrcDTName, string strOrderDSAlias, string strOrderDTName)
        {
            soDatasetVector objSrcDT = null;
            soDatasetVector objOrderDT = null;

            try
            {
                objSrcDT = getDatasetFromWorkspaceByName(strSrcDTName, objSuperWKS, strSrcAlias) as soDatasetVector;
                objOrderDT = getDatasetFromWorkspaceByName(strOrderDTName, objSuperWKS, strOrderDSAlias) as soDatasetVector;

                if (objSrcDT == null || objOrderDT == null)
                { return false; }

                objOrderDT.ToleranceDangle = objSrcDT.ToleranceDangle;
                objOrderDT.ToleranceFuzzy = objSrcDT.ToleranceFuzzy;
                objOrderDT.ToleranceGrain = objSrcDT.ToleranceGrain;
                objOrderDT.ToleranceNodeSnap = objSrcDT.ToleranceNodeSnap;
                objOrderDT.ToleranceSmallPolygon = objSrcDT.ToleranceSmallPolygon;

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objSrcDT);
                ReleaseSmObject(objOrderDT);
            }
        }
        #endregion

        #region 添加数据集到Supermap图层

        /// <summary>
        /// 添加数据集到图层
        /// </summary>
        /// <param name="objSuperWKS">工作空间</param>
        /// <param name="strAlias">数据源别名</param>
        /// <param name="strDTName">数据集名称</param>
        /// <param name="objSuperMap">SuperMap控件</param>
        /// <param name="bAddTop">是否添加到SuperMap的顶部</param>
        /// <returns>是否添加图层成功</returns>
        public static bool AddLayerToSuperMap(AxSuperWorkspace objSuperWKS
            , string strAlias, string strDTName, AxSuperMap objSuperMap, bool bAddTop)
        {
            soDataset objDT = null;
            soLayers objLayers = null;
            soLayer objAddLayer = null;

            try
            {
                objDT = getDatasetFromWorkspaceByName(strDTName, objSuperWKS, strAlias);

                if (objDT == null) return false;

                objLayers = objSuperMap.Layers;

                objAddLayer = objLayers.AddDataset(objDT, bAddTop);

                if (objAddLayer != null)
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objDT);
                ReleaseSmObject(objLayers);
                ReleaseSmObject(objAddLayer);
            }
        }

        /// <summary>
        /// 对图层进行过滤
        /// </summary>
        /// <param name="ParamLayer">需要过滤的图层对象</param>
        /// <param name="strCondition">过滤条件字符串</param>
        public static void DisplayFilterLayer(ref soLayer ParamLayer, ref soQueryDef querf, string strCondition)
        {
            if (ParamLayer != null && querf != null && !string.IsNullOrEmpty(strCondition))
            {
                querf.Filter = strCondition;
                ParamLayer.DisplayFilterEx = querf;
            }
        }
        #endregion 

        #region （二调）图层显示不同颜色 不推荐其他软件使用

        /// <summary>
        /// 得到颜色系列 (二调)
        /// </summary>
        /// <param name="objStyle">SoStyle</param>
        /// <param name="i">次数</param>
        /// <returns></returns>
        public static soLayer SetLayerStyle(soLayer objLayer, int i)
        {
            soStyle objStyle = new soStyle();

            Color objColor = new Color();

            float L = 167;   //亮度
            float S = 200;   //饱和度
            float H = 206;   //色调  

            soDataset objDt = objLayer.Dataset;

            //面图层的线颜色 默认为黑色
            if (objDt.Type == seDatasetType.scdRegion)
            {
                L = 200;   //亮度
                S = 200;   //饱和度                 

            }
            else if (objDt.Type == seDatasetType.scdLine || objDt.Type == seDatasetType.scdPoint)
            {
                L = 120;   //亮度
                S = 240;   //饱和度  
            }

            int n = (int)(i / 15);
            if (n > 0) i = i - 15 * n;

            switch (i)
            {
                case 1: //默认颜色                       
                    H = 100;   //色调  
                    break;
                case 2:
                    H = 170;
                    break;
                case 3:
                    H = 210;
                    break;
                case 4:
                    H = 30;
                    break;
                case 5:
                    H = 130;
                    break;
                case 6:
                    H = 70;
                    break;
                case 7:
                    H = 190;
                    break;
                case 8:
                    H = 20;
                    break;
                case 9:
                    H = 150;
                    break;
                case 10:
                    H = 50;
                    break;
                case 11:
                    H = 150;
                    break;
                case 12:
                    H = 10;
                    break;
                case 13:
                    H = 160;
                    break;
                case 14:
                    H = 40;
                    break;
                case 15:
                    H = 180;
                    break;
            }


            objColor = HSBToColor(H, S, L);
            //面的填充颜色
            objStyle.BrushColor = (uint)System.Drawing.ColorTranslator.ToOle(objColor);

            //面图层的线颜色 默认为黑色
            if (objDt.Type == seDatasetType.scdRegion)
            {
                L = 60;
                S = 240;
                objColor = HSBToColor(H, S, L);
                //面图层的线颜色                 
                objStyle.PenColor = (uint)System.Drawing.ColorTranslator.ToOle(objColor);

            }
            else
            {
                //线颜色
                objStyle.PenColor = (uint)System.Drawing.ColorTranslator.ToOle(objColor);
            }


            objLayer.Style = objStyle;

            return objLayer;

        }


        #endregion

        #region 导入投影信息到当前地图
        public static bool ImportPJSys_To_SuperMap(AxSuperMap objAxSuperMap, string strPjSysConfigFile)
        {
            soPJCoordSys objPjCoorSys = null;

            try
            {
                if (objAxSuperMap == null) return false;

                objPjCoorSys = objAxSuperMap.PJCoordSys;

                if (!File.Exists(strPjSysConfigFile)) return false;

                StreamReader objReader = new StreamReader(strPjSysConfigFile);
                string strPjSys = objReader.ReadToEnd();

                objPjCoorSys.FromXML(strPjSys);

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objPjCoorSys);
            }
        }
        #endregion 


        public static bool ImportSysmbolFile(AxSuperWorkspace objWKS,string strSymbolFile)
        {
            soResources objResources = objWKS.Resources;
            soFillStyleLib objFillSyb = objResources.FillStyleLib ;
            soLineStyleLib objLineSyb = objResources.LineStyleLib;
            soSymbolLib objSymlb = objResources.SymbolLib;
 
            try
            {
 
        
                if (strSymbolFile.Contains(".bru"))
                {
                    objFillSyb.Import(strSymbolFile, true);
                }
 
                if (strSymbolFile.Contains(".lsl"))
                {
                    objLineSyb.Import(strSymbolFile, true);
                }
 
                if (strSymbolFile.Contains(".sym"))
                {
                    objSymlb.Import(strSymbolFile, true);
                }
 

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(objResources);
                ReleaseSmObject(objFillSyb);
                ReleaseSmObject(objLineSyb);
                ReleaseSmObject(objSymlb);
            }
        }

        /// <summary>
        /// 计算获得当前点击位置是否在容限范围内
        /// </summary>
        /// <param name="point">线的所属某一点的位置</param>
        /// <param name="currpoint">鼠标点击位置</param>
        /// <returns>容限值</returns>
        public static double SelectionTolerance(SuperMapLib.soPoint point, SuperMapLib.soPoint currpoint, AxSuperMapLib.AxSuperMap map)
        {
            if (point == null) return 0;
            if (currpoint == null) return 0;
            if (map == null) return 0;

            double interglerance = 0;

            double x1 = Math.Abs(map.MapToPixelX(point.x));//绝对值
            double x2 = Math.Abs(map.MapToPixelX(currpoint.x));
            double y1 = Math.Abs(map.MapToPixelY(point.y));
            double y2 = Math.Abs(map.MapToPixelY(currpoint.y));
            double x = x1 - x2;
            x = Math.Abs(x);
            x = Math.Pow(x, 2);
            double y = y1 - y2;
            y = Math.Abs(y);
            y = Math.Pow(y, 2);
            interglerance = Math.Sqrt(x + y);//开平方根
            return interglerance;
        }

        /// <summary>
        /// 从选择集的对象获得其记录集
        /// </summary>
        /// <param name="selection">选择集对象</param>
        /// <param name="bGeometryOnly">是否建立几何对象</param>
        /// <param name="rset">返回的记录集对象</param>
        public static void getRecordsetFormSelection(soSelection selection, bool bGeometryOnly, out soRecordset rset)
        {
            if (selection == null) rset = null;
            if (selection.Count < 1)//
            {
                ReleaseSmObject(selection);
                rset = null;
            }
            if (selection.Count > 1)//
            {
                ReleaseSmObject(selection);
                rset = null;
            }
            rset = selection.ToRecordset(bGeometryOnly);
        }

        #region 判断数据集在数据源中是否可用

        /// <summary>
        /// 判断数据集名称在数据源中是否可用
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strDtName">数据集名称</param>
        /// <returns></returns>
        public static bool IsAvailableDatasetName(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strDsAlias, string strDtName)
        {
            soDataSources objDss = null;
            soDataSource objDs = null;

            try
            {
                objDss = objAxSuperWspace.Datasources;
                objDs = objDss[strDsAlias];

                bool bAvail = objDs.IsAvailableDatasetName(strDtName);

                return bAvail;
            }
            catch { return false; }
            finally 
            {
                ReleaseSmObject(objDs);
                ReleaseSmObject(objDss);
            }
        }

        #endregion

        #region 复制数据集

        /// <summary>
        /// 复制数据集 未编码
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strSrcDsAlias">源数据源别名</param>
        /// <param name="strSrcDtName">源数据集名称</param>
        /// <param name="strTarDsAlias">目标数据源别名</param>
        /// <param name="strTarDtName">目标数据集名称</param>
        /// <returns></returns>
        public static bool CopyDataset(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strSrcDsAlias, string strSrcDtName, string strTarDsAlias, string strTarDtName)
        {         
            
            soDataSource objDs_Tar = null; 
            soDataset objDt_Src=null;
            soDataset objDt_Tar = null;

            try
            {   //目标数据源
                objDs_Tar = getDataSourceFromWorkspaceByName(objAxSuperWspace, strTarDsAlias);

                //源数据集
                objDt_Src = getDatasetFromWorkspaceByName(strSrcDtName, objAxSuperWspace, strSrcDsAlias);

                if (objDs_Tar != null && objDt_Src != null)
                {
                    objDt_Tar = objDs_Tar.CopyDataset(objDt_Src, strTarDtName, true, seEncodedType.scEncodedNONE);
                                        
                }

                if (objDt_Tar != null) return true;
                else return false;

            }
            catch { return false; }
            finally 
            {
                ReleaseSmObject(objDs_Tar);
                ReleaseSmObject(objDt_Src);
                ReleaseSmObject(objDt_Tar);
            }
        }

        /// <summary>
        /// 复制数据集
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strSrcDsAlias">源数据源别名</param>
        /// <param name="strSrcDtName">源数据集名称</param>
        /// <param name="strTarDsAlias">目标数据源别名</param>
        /// <param name="strTarDtName">目标数据集名称</param>
        /// <param name="objEncodedType">目标数据集的编码格式</param>
        /// <param name="bShowProgress">是否显示进度条</param>
        /// <returns></returns>
        public static bool CopyDataset(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strSrcDsAlias, string strSrcDtName, string strTarDsAlias, string strTarDtName,seEncodedType objEncodedType,bool bShowProgress)
        {

            soDataSource objDs_Tar = null;
            soDataset objDt_Src = null;
            soDataset objDt_Tar = null;

            try
            {   //目标数据源
                objDs_Tar = getDataSourceFromWorkspaceByName(objAxSuperWspace, strTarDsAlias);

                //源数据集
                objDt_Src = getDatasetFromWorkspaceByName(strSrcDtName, objAxSuperWspace, strSrcDsAlias);

                if (objDs_Tar != null && objDt_Src != null)
                {
                    objDt_Tar = objDs_Tar.CopyDataset(objDt_Src, strTarDtName, bShowProgress, objEncodedType); 

                }

                if (objDt_Tar != null) return true;
                else return false;

            }
            catch { return false; }
            finally
            {
                ReleaseSmObject(objDs_Tar);
                ReleaseSmObject(objDt_Src);
                ReleaseSmObject(objDt_Tar);
            }
        }


        /// <summary>
        /// 复制数据集
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <param name="strSrcDsAlias"></param>
        /// <param name="strSrcDtName"></param>
        /// <param name="strTarDsAlias"></param>
        /// <param name="strTarDtName"></param>
        /// <param name="objEncodedType"></param>
        /// <param name="bShowProgress"></param>
        /// <param name="bTruncate">如果目标数据集存在是否清除原有数据</param>
        /// <returns></returns>
        public static bool CopyDataset(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strSrcDsAlias, string strSrcDtName, string strTarDsAlias, string strTarDtName, seEncodedType objEncodedType, bool bShowProgress,bool bTruncate)
        {

            soDataSource objDs_Tar = null;
            soDataset objDt_Src = null;
            soDatasetVector objDtv_Src = null;
            soRecordset objRt_Src = null;
            
            soDataset objDt_Tar = null;
            soDatasetVector objDtv_Tar = null;

            try
            {   //目标数据源
                objDs_Tar = getDataSourceFromWorkspaceByName(objAxSuperWspace, strTarDsAlias);
                //目标数据集存在
                objDt_Tar = getDatasetFromWorkspaceByName(strTarDtName, objAxSuperWspace, strTarDsAlias);
                //源数据集
                objDt_Src = getDatasetFromWorkspaceByName(strSrcDtName, objAxSuperWspace, strSrcDsAlias);

                if (objDs_Tar != null && objDt_Src != null)
                {
                    //目标数据集不存在就复制
                    if (objDs_Tar == null)
                    {
                        objDt_Tar = objDs_Tar.CopyDataset(objDt_Src, strTarDtName, bShowProgress, objEncodedType);                        
                    }
                    else
                    {
                        objDtv_Src = objDt_Src as soDatasetVector;
                        objRt_Src = objDtv_Src.Query("", true, null, "");

                        if (bTruncate)
                        {
                            objDtv_Tar = objDt_Tar as soDatasetVector;
                            objDtv_Tar.ClearRecordsets();
                            objDtv_Tar.Truncate();

                            objDtv_Tar.Append(objRt_Src, bShowProgress);
                        }
                        else
                        {
                            objDtv_Tar.Append(objRt_Src, bShowProgress);
                        }
                    }
                }

                if (objDt_Tar != null) return true;
                else return false;

            }
            catch { return false; }
            finally
            {
                ReleaseSmObject(objRt_Src);
                ReleaseSmObject(objDtv_Tar);
                ReleaseSmObject(objDtv_Src);
                ReleaseSmObject(objDs_Tar);
                ReleaseSmObject(objDt_Src);
                ReleaseSmObject(objDt_Tar);
            }
        }


        #endregion

        #region 复制数据源

        /// <summary>
        /// 拷贝一个数据源数据到一个新数据源
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <param name="strSorDsAlias"></param>
        /// <param name="strTarDsAlias"></param>
        /// <param name="bShowProgress">是否显示进度条</param>
        /// <param name="objType">数据集压缩方式</param>
        /// <returns></returns>
        public static Boolean CopyDataSource(AxSuperWorkspace objAxSuperWspace,string strSorDsAlias,string strTarDsAlias,bool bShowProgress,seEncodedType objType)
        {
            soDataSource objDsSor = null;
            soDataSource objDsTar = null;
            soDatasets objDtsSor = null;

            try
            {
                objDsSor = getDataSourceFromWorkspaceByName(objAxSuperWspace, strSorDsAlias);
                objDsTar = getDataSourceFromWorkspaceByName(objAxSuperWspace, strTarDsAlias);

                if (objDsSor == null || objDsTar == null) return false;

                objDtsSor = objDsSor.Datasets;

                if (objDtsSor != null)
                {
                    for (int i = 1; i <= objDtsSor.Count; i++)
                    {
                        soDataset objDt = objDtsSor[i];

                        if (objDt == null) continue;

                        string strDtName = objDt.Name;
                        
                        soDataset objDtTemp = objDsTar.CopyDataset(objDt, strDtName, bShowProgress, objType);

                        ReleaseSmObject(objDtTemp); objDtTemp = null;
                        

                        ReleaseSmObject(objDt); objDt = null;
                    }
                }
                return true;
            }
            catch { return false; }
            finally 
            {
                ReleaseSmObject(objDtsSor); objDtsSor = null;
                ReleaseSmObject(objDsTar); objDsTar = null;
                ReleaseSmObject(objDsSor); objDsSor = null;
            }

        }


        /// <summary>
        /// 拷贝一个数据源数据到一个新数据源
        /// 
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <param name="strSorDsAlias"></param>
        /// <param name="strTarDsAlias"></param>
        /// <param name="bShowProgress">是否显示进度条</param>
        /// <param name="objType">数据集压缩方式</param>
        /// <param name="strCondition">如果 strCondition 前带@表示 拷贝数据集名称中具有strCondition的数据集 如果没带@表示不拷贝数据集名称中带strCondition的数据集 </param>
        /// <returns></returns>
        public static Boolean CopyDataSource(AxSuperWorkspace objAxSuperWspace, string strSorDsAlias, string strTarDsAlias, bool bShowProgress, seEncodedType objType,string strCondition)
        {
            soDataSource objDsSor = null;
            soDataSource objDsTar = null;
            soDatasets objDtsSor = null;

            try
            {
                objDsSor = getDataSourceFromWorkspaceByName(objAxSuperWspace, strSorDsAlias);
                objDsTar = getDataSourceFromWorkspaceByName(objAxSuperWspace, strTarDsAlias);

                if (objDsSor == null || objDsTar == null) return false;

                objDtsSor = objDsSor.Datasets;

                if (objDtsSor != null)
                {
                    bool bCopy = false;

                    if (strCondition.Contains("@"))
                    {
                        bCopy = true;
                        strCondition = strCondition.Substring(1, strCondition.Length - 1);
                    }

                    for (int i = 1; i <= objDtsSor.Count; i++)
                    {
                        soDataset objDt = objDtsSor[i];

                        if (objDt == null) continue;

                        string strDtName = objDt.Name;

                        soDataset objDtTemp = null;

                        if (bCopy)
                        {
                            if (strDtName.ToLower().Contains(strCondition.ToLower()))
                            {
                                objDtTemp = objDsTar.CopyDataset(objDt, strDtName, bShowProgress, objType);
                            }
                        }
                        else
                        {
                            if (!strDtName.ToLower().Contains(strCondition.ToLower()))
                            {
                                objDtTemp = objDsTar.CopyDataset(objDt, strDtName, bShowProgress, objType);                                
                            }
                        }
                        ReleaseSmObject(objDtTemp); objDtTemp = null;
                        ReleaseSmObject(objDt); objDt = null;
                    }
                }
                return true;
            }
            catch { return false; }
            finally
            {
                ReleaseSmObject(objDtsSor); objDtsSor = null;
                ReleaseSmObject(objDsTar); objDsTar = null;
                ReleaseSmObject(objDsSor); objDsSor = null;
            }

        }


        #endregion

        #region 直接创建数据集

        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strDtName">数据集名称</param>
        /// <param name="objDatasetType">数据集类型</param>
        /// <returns>如果数据集名称不可用，则直接返回已存在的数据集</returns>
        public static soDataset CreateDataset(AxSuperWorkspace objAxSuperWspace, string strDsAlias, string strDtName, seDatasetType objDatasetType)
        {
            soDataSources objDss = null;
            soDataSource objDs = null;
            soDataset objDt = null;

            try
            {
                objDss = objAxSuperWspace.Datasources;
                objDs = objDss[strDsAlias];

                if (objDs != null)
                {
                    bool bIsAvailable = objDs.IsAvailableDatasetName(strDtName);

                    if (!bIsAvailable)
                    {  
                        objDt = getDatasetFromDataSourceByName(strDtName, objDs);
                    }
                    else
                    {
                        objDt = objDs.CreateDataset(strDtName, objDatasetType, seDatasetOption.scoDefault, null);
                    }
                    
                }
            }
            catch { }
            finally 
            {
                ReleaseSmObject(objDs);
                ReleaseSmObject(objDss);
            }

            return objDt;
        }


        #endregion

        #region 获得数据集某字段的值

        /// <summary>
        /// 获得数据集某字段的值(所有值)
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strDtName">数据集名称(矢量数据集)</param>
        /// <param name="strFieldName">字段名称</param>
        /// <returns></returns>
        public static List<string> GetFieldValueFromDataset(AxSuperWorkspace objAxSuperWspace, string strDsAlias, string strDtName, string strFieldName)
        {
            List<string> strFields = new List<string>();

            soDataset objDt = null;
            soDatasetVector objDtv = null;
            soRecordset objRt = null;

            try
            {
                objDt = getDatasetFromWorkspaceByName(strDtName, objAxSuperWspace, strDsAlias);

                if (objDt != null && objDt.Vector)
                {
                    objDtv = objDt as soDatasetVector;

                    objRt = objDtv.Query("", false, null, "");

                    if (objRt == null) return null;

                    objRt.MoveFirst();
                    for (int i = 1; i <= objRt.RecordCount; i++)
                    {
                        string strValue = objRt.GetFieldValueText(strFieldName);

                        if (!string.IsNullOrEmpty(strValue) && !strFields.Contains(strValue))
                            strFields.Add(strValue);

                        objRt.MoveNext();
                    }
                }
            }
            catch { }
            finally
            {
                ReleaseSmObject(objRt);
                ReleaseSmObject(objDtv);
                ReleaseSmObject(objDt);
            }

            return strFields;

        }

        /// <summary>
        /// 获得数据集某字段的值(按条件)
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strDtName">数据集名称(矢量数据集)</param>
        /// <param name="strFieldName">字段名称</param>
        /// <param name="strCondition">条件</param>
        /// <returns></returns>
        public static List<string> GetFieldValueFromDataset(AxSuperWorkspace objAxSuperWspace, string strDsAlias, string strDtName, string strFieldName,string strCondition)
        {
            List<string> strFields = new List<string>();

            soDataset objDt = null;
            soDatasetVector objDtv = null;
            soRecordset objRt = null;

            try
            {
                objDt = getDatasetFromWorkspaceByName(strDtName, objAxSuperWspace, strDsAlias);

                if (objDt != null && objDt.Vector)
                {
                    objDtv = objDt as soDatasetVector;

                    objRt = objDtv.Query(strCondition, false, null, "");

                    if (objRt == null) return null;

                    objRt.MoveFirst();
                    for (int i = 1; i <= objRt.RecordCount; i++)
                    {
                        string strValue = objRt.GetFieldValueText(strFieldName);

                        if (!strFields.Contains(strValue))
                            strFields.Add(strValue);

                        objRt.MoveNext();
                    }
                }
            }
            catch { }
            finally
            {
                ReleaseSmObject(objRt);
                ReleaseSmObject(objDtv);
                ReleaseSmObject(objDt);
            }

            return strFields;

        }

        #endregion

        #region 移除SuperMap上的图层

        /// <summary>
        /// 移除指定数据源的数据集
        /// </summary>
        /// <param name="objAxSuperMap"></param>
        /// <param name="strDsAlias"></param>
        /// <param name="strDtName"></param>
        /// <returns></returns>
        public static bool RemoveSuperMapLayer(AxSuperMap objAxSuperMap,string strDsAlias,string strDtName)
        {
            bool bRemove=false;
            
            if (objAxSuperMap != null)
            {
                soLayers objLys = objAxSuperMap.Layers;

                if (objLys != null)
                {
                    for (int i = 1; i <= objLys.Count; i++)
                    {
                        soLayer objLy = objLys[i];

                        if (objLy == null) continue;

                        string strLyName = objLy.Name;

                        if (strLyName == strDtName + "@" + strDsAlias)
                        {
                            int iIndex = objLys.FindLayer(objLy);

                            ztSuperMap.ReleaseSmObject(objLy); objLy = null;

                            bRemove = objLys.RemoveAt(iIndex); i--;
                        }
                        else
                        {
                            ztSuperMap.ReleaseSmObject(objLy); objLy = null;
                        }
                    }
                    ztSuperMap.ReleaseSmObject(objLys); objLys = null;
                }
            }
            return bRemove;
        }

        /// <summary>
        /// 移除指定数据集
        /// 如：移除地图中所有 地类图斑 层
        /// </summary>
        /// <param name="objAxSuperMap"></param>
        /// <param name="strDtName"></param>
        /// <returns></returns>
        public static bool RemoveSuperMapLayer(AxSuperMap objAxSuperMap, string strDtName)
        {
            bool bRemove = false;

            if (objAxSuperMap != null)
            {
                soLayers objLys = objAxSuperMap.Layers;

                if (objLys != null)
                {
                    for (int i = 1; i <= objLys.Count; i++)
                    {
                        soLayer objLy = objLys[i];

                        if (objLy == null) continue;

                        string strLyName = objLy.Name;

                        string strLyDtName = strLyName.Split('@')[0].ToString();

                        if (strLyDtName == strDtName)
                        {
                            int iIndex = objLys.FindLayer(objLy);

                            ztSuperMap.ReleaseSmObject(objLy); objLy = null;

                            bRemove = objLys.RemoveAt(iIndex); i--;
                        }
                        else
                        {
                            ztSuperMap.ReleaseSmObject(objLy); objLy = null;
                        }
                    }
                    ztSuperMap.ReleaseSmObject(objLys); objLys = null;
                }
            }
            return bRemove;
        }

        /// <summary>
        /// 移除所有图层
        /// </summary>
        /// <param name="objAxSuperMap"></param>
        /// <returns></returns>
        public static bool RemoveSuperMapLayer(AxSuperMap objAxSuperMap)
        {
            bool bRemove = false;

            if (objAxSuperMap != null)
            {
                soLayers objLys = objAxSuperMap.Layers;

                if (objLys != null)
                {
                    if (objLys.Count > 0)
                    {
                        objLys.RemoveAll();

                        bRemove = true;
                    }
                    ztSuperMap.ReleaseSmObject(objLys); objLys = null;
                }
            }
            return bRemove;
        }

        #endregion

        #region 移除SuperworkSpace上的数据源

        /// <summary>
        /// 移除数据源
        /// 注：在移除数据源之前，请先移除图层
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <returns></returns>
        public static bool RemoveDataSource(AxSuperWorkspace objAxSuperWspace, string strDsAlias)
        {
            bool bRemove = false;

            if (objAxSuperWspace != null)
            {
                soDataSources objDss = objAxSuperWspace.Datasources;

                if (objDss != null)
                {
                    for (int i = 1; i <= objDss.Count; i++)
                    {
                        soDataSource objDs = objDss[i];

                        if (objDs == null) continue;

                        string strDatasourceAlias = objDs.Alias;

                        if (strDsAlias == strDatasourceAlias)
                        {
                            objDs.Disconnect();
                            ZTSupermap.ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                            bRemove = objDss.Remove(strDsAlias); i--;
                        }
                        else
                        {
                            ZTSupermap.ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                        }
                    }
                    ZTSupermap.ztSuperMap.ReleaseSmObject(objDss); objDss = null;
                }
            }
            return bRemove;
        }

        /// <summary>
        /// 移除所有数据源
        /// 注：在移除数据源之前，请先移除图层
        /// </summary>
        /// <param name="objAxSuperWspace"></param>
        /// <returns></returns>
        public static bool RemoveDataSource(AxSuperWorkspace objAxSuperWspace)
        {
            bool bRemove = false;

            if (objAxSuperWspace != null)
            {
                soDataSources objDss = objAxSuperWspace.Datasources;

                if (objDss != null)
                {
                    for (int i = 1; i <= objDss.Count; i++)
                    {
                        soDataSource objDs = objDss[i];

                        if (objDs == null) continue;

                        string strDatasourceAlias = objDs.Alias;

                        objDs.Disconnect();
                        ZTSupermap.ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                        bRemove = objDss.Remove(strDatasourceAlias); i--;                        
                    }

                    bRemove = true;

                    ZTSupermap.ztSuperMap.ReleaseSmObject(objDss); objDss = null;
                }

            }
            return bRemove;
        }

        #endregion

        #region 跟踪层的对象操作

        /// <summary>
        /// 向跟踪层添加对象的方法
        /// </summary>
        /// <param name="geometry">向跟踪层添加的对象</param>
        /// <param name="map"></param>
        /// <param name="style"></param>
        ///<param name="strtag"></param>AxSuperMapLib.AxSuperMap map
        public static void TrackLayerAddGeometry(ref SuperMapLib.soTrackingLayer tracklayer, SuperMapLib.soGeometry geometry, SuperMapLib.soStyle style, string strtag)
        {
            if (geometry == null || tracklayer == null) return;
            tracklayer.AddEvent(geometry, style, strtag);//写入对象
            tracklayer.Refresh();
        }

        /// <summary>
        /// 向跟踪层添加对象的方法
        /// </summary>
        /// <param name="geometry">向跟踪层添加的对象</param>
        /// <param name="map"></param>
        /// <param name="style"></param>
        ///<param name="strtag"></param>
        public static void TrackLayerAddGeometry(AxSuperMapLib.AxSuperMap map, SuperMapLib.soGeometry geometry, SuperMapLib.soStyle style, string strtag)
        {
            if (geometry == null || map == null) return;
            SuperMapLib.soTrackingLayer tracklayer = map.TrackingLayer;
            if (tracklayer != null)
            {
                tracklayer.AddEvent(geometry, style, strtag);//写入对象
                ReleaseSmObject(tracklayer);
                tracklayer = null;
            }
            map.Refresh();
        }

        /// <summary>
        /// 根据向跟踪层写入的Geomtry的Tags查找对象
        /// </summary>
        /// <param name="strtag">Geomtry的Tags</param>
        /// <param name="geometry">查找到的对象如果没有就是Null</param>
        public static void getTrackLayerOfGeometryFromGeometryTag(AxSuperMapLib.AxSuperMap map, string strtag, out SuperMapLib.soGeometry geometry)
        {
            if (map == null) 
            {
                geometry = null;
                return; 
            }
            strtag = strtag.ToLower();
            SuperMapLib.soTrackingLayer tracklayer = map.TrackingLayer;
            if (tracklayer != null)
            {
                for (int i = 1; i <= tracklayer.EventCount; i++)
                {
                    SuperMapLib.soGeoEvent geoevent = tracklayer.get_Event(i);
                    if (geoevent == null) continue;
                    string streventtag = geoevent.Tag;//获得标识字符串
                    streventtag = streventtag.ToLower();
                    if (streventtag.Equals(strtag))//有对象
                    {
                        geometry = geoevent.geometry;
                        ztSuperMap.ReleaseSmObject(geoevent);
                        return;
                    }
                    else
                    {
                        ztSuperMap.ReleaseSmObject(geoevent);
                        continue;
                    }
                }
                ztSuperMap.ReleaseSmObject(tracklayer);
            }
            geometry = null;
        }

        /// <summary>
        /// 根据参数对象获得其点集合
        /// </summary>
        /// <param name="geometry">参数对象：将其转化为点集合</param>
        /// <returns>没有返回Null反之是点集合对象</returns> 
        public static SuperMapLib.soPoints getPointsFormGeometry(SuperMapLib.soGeometry geometry)
        {
            if (geometry == null) return null;

            SuperMapLib.soPoints points = new SuperMapLib.soPoints();

            switch (geometry.Type)//转换成点集合 
            {
                case SuperMapLib.seGeometryType.scgPoint://点
                    SuperMapLib.soGeoPoint point = geometry as SuperMapLib.soGeoPoint;
                    double x = point.x, y = point.y;
                    if (point != null) ztSuperMap.ReleaseSmObject(point);
                    points.Add2(x, y);
                    return points;
                case SuperMapLib.seGeometryType.scgLine://直线
                    SuperMapLib.soGeoLine line = geometry as SuperMapLib.soGeoLine;
                    for (int i = 1; i <= line.PartCount; i++)
                        points = line.GetPartAt(i);
                    if (line != null) ztSuperMap.ReleaseSmObject(line);
                    return points;
                case SuperMapLib.seGeometryType.scgRegion://面
                    SuperMapLib.soGeoRegion georegion = geometry as SuperMapLib.soGeoRegion;
                    if (georegion != null)
                    {
                        SuperMapLib.soGeoLine regiontempline = georegion.ConvertToLine();
                        if (regiontempline != null)
                        {
                            points = getPointsFormGeometry(regiontempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(regiontempline);
                        }
                        ztSuperMap.ReleaseSmObject(georegion);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgArc://三点弧有问题暂时不用
                    SuperMapLib.soGeoArc arc = geometry as SuperMapLib.soGeoArc;
                    SuperMapLib.soGeoArc arctemp = arc.Clone();
                    if (arctemp != null)
                    {
                        SuperMapLib.soGeoLine linetemp = arctemp.ConvertToLine(72);
                        if (linetemp != null)
                        {
                            points = getPointsFormGeometry(linetemp as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(linetemp);
                        }
                        ztSuperMap.ReleaseSmObject(arctemp);
                    }
                    if (arc != null) ztSuperMap.ReleaseSmObject(arc);
                    return points;
                case SuperMapLib.seGeometryType.scgEllipseOblique://椭圆
                    SuperMapLib.soGeoEllipseOblique geoellipseoblique = geometry as SuperMapLib.soGeoEllipseOblique;
                    if (geoellipseoblique != null)
                    {
                        SuperMapLib.soGeoLine ellipseobliquetempline = geoellipseoblique.ConvertToLine(72);
                        if (ellipseobliquetempline != null)
                        {
                            points = getPointsFormGeometry(ellipseobliquetempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(ellipseobliquetempline);
                        }
                        ztSuperMap.ReleaseSmObject(geoellipseoblique);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgEllipticArc://圆弧
                    SuperMapLib.soGeoEllipticArc geoellipticarc = geometry as SuperMapLib.soGeoEllipticArc;
                    if (geoellipticarc != null)
                    {
                        SuperMapLib.soGeoLine ellipticarctempline = geoellipticarc.ConvertToLine(72);
                        if (ellipticarctempline != null)
                        {
                            points = getPointsFormGeometry(ellipticarctempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(ellipticarctempline);
                        }
                        ztSuperMap.ReleaseSmObject(geoellipticarc);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgRect://矩形
                    SuperMapLib.soGeoRect georect = geometry as SuperMapLib.soGeoRect;
                    if (georect != null)
                    {
                        SuperMapLib.soGeoLine recttempline = georect.ConvertToLine();
                        if (recttempline != null)
                        {
                            points = getPointsFormGeometry(recttempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(recttempline);
                        }
                        ztSuperMap.ReleaseSmObject(georect);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgCardinal://曲线
                    SuperMapLib.soGeoCardinal geocardinal = geometry as SuperMapLib.soGeoCardinal;
                    if (geocardinal != null)
                    {
                        SuperMapLib.soGeoLine cardinaltempline = geocardinal.ConvertToLine(72);
                        if (cardinaltempline != null)
                        {
                            points = getPointsFormGeometry(cardinaltempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(cardinaltempline);
                        }
                        ztSuperMap.ReleaseSmObject(geocardinal);
                    }
                    return points;

                case SuperMapLib.seGeometryType.scgCircle://圆
                    SuperMapLib.soGeoCircle geocircle = geometry as SuperMapLib.soGeoCircle;
                    if (geocircle != null)
                    {
                        SuperMapLib.soGeoLine circletempline = geocircle.ConvertToLine(72);
                        if (circletempline != null)
                        {
                            points = getPointsFormGeometry(circletempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(circletempline);
                        }
                        ztSuperMap.ReleaseSmObject(geocircle);
                    }
                    return points;
                #region
                //case SuperMapLib.seGeometryType.scgCurve:
                //    return points;
                //case SuperMapLib.seGeometryType.scgEllipse:
                //    return points;
                #endregion
            }
            return null;
        }

        /// <summary>
        /// 删除跟踪层的对象
        /// </summary>
        /// <param name="map">地图控件对象</param>
        /// <param name="strtag">跟踪层要删除的对象的标识字符串（如果参数是Empty将删除跟踪层所有的对象）</param>
        /// <returns>成功返回True反之是False</returns>
        public static bool TrackLayerDeleteGeometry(AxSuperMapLib.AxSuperMap map, string strtag)
        {
            if (map == null) return false;
            SuperMapLib.soTrackingLayer tracklayer = null;
            
            try
            {
                tracklayer = map.TrackingLayer;
                if (tracklayer == null) return false;

                switch (string.IsNullOrEmpty(strtag))
                {
                    case true:
                        tracklayer.ClearEvents();
                        return true;
                    case false:
                        bool bflag = false;
                        SuperMapLib.soGeoEvent geoevent = tracklayer.get_Event(strtag);
                        if (geoevent != null) bflag = tracklayer.RemoveEvent(strtag);
                        ReleaseSmObject(geoevent);
                        return bflag;
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseSmObject(tracklayer);
            }
        }

        #endregion
    }
}
