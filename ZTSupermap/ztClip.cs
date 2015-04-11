using System;
using System.Collections.Generic;
using System.Text;

using AxSuperMapLib;
using SuperMapLib;
using System.Collections;

namespace ZTSupermap
{
    /// <summary>
    /// 切割数据集方法，实验了很多，效果各有千秋。
    /// </summary>
    public static class ztClip
    {
        /// <summary>
        /// 切割数据集
        /// </summary>
        /// <param name="objClippedDataset">被切割数据集</param>
        /// <param name="objClipRegion">切割面数据集或者单独的面</param>
        /// <param name="objResultDataset">结果数据集</param>
        /// <param name="bShowProgress">是否显示进度条</param>
        /// <returns></returns>
        public static Boolean ClipDataset(soDataset objClippedDataset, Object objClipRegion, soDatasetVector objResultDataset, Boolean bShowProgress)
        {
            bool bClipFlag = false;
            if (objClippedDataset == null || objClipRegion == null || objResultDataset == null)
            {
                return false;
            }
            if (objClippedDataset.Vector)
            {
                soOverlayAnalyst objOverlayAnalyst = new soOverlayAnalyst();
                objOverlayAnalyst.ShowProgress = bShowProgress;
                soDatasetVector objClippedDatasetVector = (soDatasetVector)objClippedDataset;
                try
                {
                    bClipFlag = objOverlayAnalyst.Clip(objClippedDatasetVector, objClipRegion, objResultDataset);
                }
                catch { }                
                
            }
            return bClipFlag;
        }
        
        /// <summary>
        /// 切割数据集
        /// </summary>
        /// <param name="spwsWorkspace"></param>
        /// <param name="strAlias"></param>
        /// <param name="strDatasetName"></param>
        /// <param name="objClipRegion"></param>
        /// <param name="objResultDataset"></param>
        /// <param name="bShowProgress"></param>
        /// <returns></returns>
        public static Boolean ClipDataset(AxSuperWorkspace spwsWorkspace, String strAlias, String strDatasetName,
            soRecordset objClipRegion, soDatasetVector objResultDataset, Boolean bShowProgress)
        {
            if (spwsWorkspace == null || objResultDataset == null || objClipRegion == null)
            {
                return false;
            }
            soDataSources objDss = spwsWorkspace.Datasources;
            soDataSource objDs = objDss[strAlias];
            if (objDs == null)
            {
                ztSuperMap.ReleaseSmObject(objDss);
                objDss = null;

                return false;
            }
            soDatasets objDts = objDs.Datasets;
            soDataset objDt = objDts[strDatasetName];
            if (objDt == null || !objDt.Vector)     //只裁切适量数据集
            {
                ztSuperMap.ReleaseSmObject(objDt);
                objDt = null;
                ztSuperMap.ReleaseSmObject(objDts);
                objDts = null;
                ztSuperMap.ReleaseSmObject(objDs);
                objDs = null;
                ztSuperMap.ReleaseSmObject(objDss);
                objDss = null;
                return false;
            }

            Boolean bClipFlag = false;
            soDatasetVector objDtv = (soDatasetVector)objDt;
            soOverlayAnalyst objOverlayAnalyst = new soOverlayAnalyst();
            objOverlayAnalyst.ShowProgress = bShowProgress;
            try
            {
                bClipFlag = objOverlayAnalyst.Clip(objDtv, objClipRegion, objResultDataset);
            }
            catch
            {
                bClipFlag = false;
            }
            ztSuperMap.ReleaseSmObject(objOverlayAnalyst);
            ztSuperMap.ReleaseSmObject(objDtv);
            //ztSuperMap.ReleaseSmObject(objDt);
            ztSuperMap.ReleaseSmObject(objDts);
            ztSuperMap.ReleaseSmObject(objDs);
            ztSuperMap.ReleaseSmObject(objDss);
            objDss = null;
            objOverlayAnalyst = null;
            objDt = null;
            objDtv = null;
            objDts = null;
            objDs = null;
            return bClipFlag;
        }
               

        /// <summary>
        /// 切割一个数据源内的所有数据集。
        /// </summary>
        /// <param name="_ObjectPrintGeoRegion"></param>
        /// <param name="soDS_From"></param>
        /// <param name="soDS_to"></param>
        /// <param name="ClipDatasetsNames"></param>
        public static void ClipAllDataSet(SuperMapLib.soGeoRegion _ObjectPrintGeoRegion, SuperMapLib.soDataSource soDS_From, SuperMapLib.soDataSource soDS_to, ArrayList ClipDatasetsNames)
        {
            if (soDS_From != null && soDS_to != null)
            {
                SuperMapLib.soDatasets p_soDTs_From = soDS_From.Datasets;
                if (p_soDTs_From != null && p_soDTs_From.Count > 0)
                {
                    SuperMapLib.soDataset soDT_From = null;
                    SuperMapLib.soDatasetVector soDTV_From = null;
                    SuperMapLib.soDataset soDT_To = null;
                    SuperMapLib.soDatasetVector soDTV_To = null;
                    SuperMapLib.soOverlayAnalyst soOverlayAnalyst_ = new SuperMapLib.soOverlayAnalystClass();
                    SuperMapLib.seDatasetType seDatasetType;

                    for (int i = 1; i <= p_soDTs_From.Count; i++)
                    {
                        soDT_From = p_soDTs_From[i];
                        if (soDT_From != null)
                        {
                            if (ClipDatasetsNames.Contains(soDT_From.Name))  //判断该图层是否要裁剪(地图中是否有该图层)
                            //if(soDT_From.Name==clsMapPrintConstValue.clsDataSetNameConst.DLTB ||soDT_From.Name==clsMapPrintConstValue.clsDataSetNameConst.DLJX)
                            {
                                seDatasetType = soDT_From.Type;
                                switch (seDatasetType)
                                {
                                    case SuperMapLib.seDatasetType.scdCAD:
                                    case SuperMapLib.seDatasetType.scdDEM:
                                    case SuperMapLib.seDatasetType.scdECW:
                                    case SuperMapLib.seDatasetType.scdGrid:
                                    case SuperMapLib.seDatasetType.scdImage:
                                    case SuperMapLib.seDatasetType.scdLineM:
                                    case SuperMapLib.seDatasetType.scdLineZ:
                                    case SuperMapLib.seDatasetType.scdMrSID:
                                    case SuperMapLib.seDatasetType.scdNetwork:
                                    case SuperMapLib.seDatasetType.scdParcel:
                                    case SuperMapLib.seDatasetType.scdPointZ:
                                    case SuperMapLib.seDatasetType.scdRegionZ:
                                    case SuperMapLib.seDatasetType.scdTIN:
                                    case SuperMapLib.seDatasetType.scdTabular:
                                    case SuperMapLib.seDatasetType.scdTextZ:
                                    case SuperMapLib.seDatasetType.scdTraverse:
                                        break;
                                    default:
                                        soDTV_From = soDT_From as SuperMapLib.soDatasetVector;
                                        soDT_To = soDS_to.CreateDatasetFrom(soDTV_From.Name, soDTV_From);
                                        soDTV_To = soDT_To as SuperMapLib.soDatasetVector;
                                        if (!soOverlayAnalyst_.Clip(soDTV_From, _ObjectPrintGeoRegion, soDTV_To))
                                        {
                                            //MessageBox.Show(soDTV_From.Name + "裁剪错误");
                                        }
                                        break;
                                }
                            }
                            //ReleaseComObject(seDatasetType);
                            ztSuperMap.ReleaseSmObject(soDT_From);
                            ztSuperMap.ReleaseSmObject(soDT_To);
                        }
                    }
                    ztSuperMap.ReleaseSmObject(soOverlayAnalyst_);
                }
                ztSuperMap.ReleaseSmObject(p_soDTs_From);
            }
        }

        /// <summary>
        /// 切割一个数据源内所有的数据集
        /// </summary>
        /// <param name="spwsWorkspace"></param>
        /// <param name="strClippedAlias"></param>
        /// <param name="strResultAlias"></param>
        /// <param name="objClippRegion"></param>
        /// <param name="strExceptionDataset">例外数据集，在此中的数据集将不被切割而直接拷贝。</param>
        /// <param name="bShowProgress"></param>
        /// <returns></returns>
        public static Boolean ClipAllDataSet(AxSuperWorkspace spwsWorkspace, String strClippedAlias, String strResultAlias, soRecordset objClippRegion, string[] strExceptionDataset, Boolean bShowProgress)
        {
            if (spwsWorkspace == null || objClippRegion == null)
            {
                return false;
            }
            soDataSources objDss = spwsWorkspace.Datasources;
            soDataSource objClippedDatasource = objDss[strClippedAlias];
            soDataSource objResultDatasource = objDss[strResultAlias];
            if (objClippedDatasource == null || objResultDatasource == null)
            {
                ztSuperMap.ReleaseSmObject(objClippedDatasource);
                ztSuperMap.ReleaseSmObject(objResultDatasource);
                ztSuperMap.ReleaseSmObject(objDss); objDss = null;
                return false;
            }
            soDatasets objClippedDatasets = objClippedDatasource.Datasets;
            if (objClippedDatasets == null)
            {
                ztSuperMap.ReleaseSmObject(objClippedDatasource);
                ztSuperMap.ReleaseSmObject(objResultDatasource);
                ztSuperMap.ReleaseSmObject(objDss); objDss = null;
                return false;
            }
            String strDatasetName;
            
            for (int i = 1; i <= objClippedDatasets.Count; i++)
            {
                soDataset objDt = objClippedDatasets[i];
                if (objDt == null || !objDt.Vector) //非矢量数据集直接跳过，结果数据中不要非矢量数据
                {
                    ztSuperMap.ReleaseSmObject(objDt);
                    continue;
                }
                strDatasetName = objDt.Name;
                //ztFileOperate.WriteToTextFile(strLogFile, DateTime.Now.ToString() + " 准备处理数据集：" + strDatasetName);
                

                //这些面不裁切:接图表、市县行政区、乡镇行政区、行政区、村小组行政区，合并时也不合并。空数据集直接拷贝
                soDatasetVector objDtv = (soDatasetVector)objDt;
                switch (objDtv.Type)
                {
                    case SuperMapLib.seDatasetType.scdCAD:
                    case SuperMapLib.seDatasetType.scdDEM:
                    case SuperMapLib.seDatasetType.scdECW:
                    case SuperMapLib.seDatasetType.scdGrid:
                    case SuperMapLib.seDatasetType.scdImage:
                    case SuperMapLib.seDatasetType.scdLineM:
                    case SuperMapLib.seDatasetType.scdLineZ:
                    case SuperMapLib.seDatasetType.scdMrSID:
                    case SuperMapLib.seDatasetType.scdNetwork:
                    case SuperMapLib.seDatasetType.scdParcel:
                    case SuperMapLib.seDatasetType.scdPointZ:
                    case SuperMapLib.seDatasetType.scdRegionZ:
                    case SuperMapLib.seDatasetType.scdTIN:
                    //case SuperMapLib.seDatasetType.scdTabular:
                    case SuperMapLib.seDatasetType.scdTextZ:
                    case SuperMapLib.seDatasetType.scdTraverse:
                        //ztSuperMap.ReleaseSmObject(objDtv);
                        ztSuperMap.ReleaseSmObject(objDt);
                        objDt = null;
                        objDtv = null;
                        //ztFileOperate.WriteToTextFile(strLogFile, strDatasetName+" 为不可裁切数据集"  );
                        continue;
                        break;
                }

                if (strExceptionDataset != null)
                {
                    bool isException = false;
                    for (int j = 0; j<strExceptionDataset.Length; j++)
                    {
                        if (strDatasetName.Equals(strExceptionDataset[j]))
                        {
                            soDataset objDataSet = objResultDatasource.CopyDataset(objDt, strDatasetName, false, seEncodedType.scEncodedNONE);
                            ztSuperMap.ReleaseSmObject(objDataSet);

                            isException= true;
                            break;
                        }
                    }

                    if (isException)
                    {
                        ztSuperMap.ReleaseSmObject(objDt);
                        ztSuperMap.ReleaseSmObject(objDtv);
                        objDt = null;
                        objDtv = null;                     
                        continue;
                    }
                }

                soDataset objDtRetsult = objResultDatasource.CreateDatasetFrom(strDatasetName, objDtv);
                if (objDtRetsult != null)
                {
                    soDatasetVector objDtvResult = (soDatasetVector)objDtRetsult;
                    ClipDataset(spwsWorkspace, strClippedAlias, strDatasetName, objClippRegion, objDtvResult, false);
                    //ztFileOperate.WriteToTextFile(strLogFile, strDatasetName + " 数据集裁切完成，准备处理字段名问题。");
                    #region [农哥新加]
                    //农20071217 裁切后的结果数据集的字段名自动加上了数据集名前缀，在这得处理一下
                    String strDTName = objDtvResult.Name + "_";
                    String strFIName;
                    String strUpdatedFIName;
                    soStrings objStrFields = new soStrings();
                    soFieldInfos objFIsTemp = objDtvResult.GetFieldInfos();
                    Boolean bUpdated = false;
                    for (int nIndexTemp = 1; nIndexTemp <= objFIsTemp.Count; nIndexTemp++)
                    {
                        strFIName = objFIsTemp[nIndexTemp].Name;
                        if (strFIName.Contains(strDTName) && strFIName.Substring(0, strDTName.Length).CompareTo(strDTName) == 0)
                        {
                            strUpdatedFIName = strFIName.Substring(strFIName.IndexOf("_") + 1);
                            bUpdated = objDtvResult.UpdateFieldEx(strUpdatedFIName, strFIName, "");
                            bUpdated = true;
                        }
                        else
                        {
                            if (strFIName.Length > 2 && strFIName.Substring(0, 2).ToLower().CompareTo("sm") != 0)   //字段名只有一个字母吗？
                            {
                                objStrFields.Add(strFIName);
                            }
                        }
                    }
                    if (bUpdated)
                    {
                        soRecordset objRS = objDtvResult.Query("", true, objStrFields, "");   //记录集可以独立于数据集而存在
                        objDtvResult.ClearRecordsets(); //必须清空数据才能删除字段
                        for (int nIndexTemp = 1; nIndexTemp <= objFIsTemp.Count; nIndexTemp++)
                        {
                            strFIName = objFIsTemp[nIndexTemp].Name;
                            if (strFIName.Contains(strDTName) && strFIName.Substring(0, strDTName.Length).CompareTo(strDTName) == 0)
                            {
                                bUpdated = objDtvResult.DeleteField(strFIName);
                            }
                        }
                        objDtvResult.Append(objRS, false);
                        ztSuperMap.ReleaseSmObject(objRS);
                        objRS = null;       //王晟新加
                    }
                    ztSuperMap.ReleaseSmObject(objStrFields);
                    objStrFields = null;        //王晟新加  
                    ztSuperMap.ReleaseSmObject(objFIsTemp);
                    objFIsTemp = null;          //王晟新加

                    #endregion
                    //ztFileOperate.WriteToTextFile(strLogFile, strDatasetName + " 数据集字段名处理完成");
                    //ztSuperMap.ReleaseSmObject(objDtvResult);
                    objDtvResult = null;
                }
                ztSuperMap.ReleaseSmObject(objDtRetsult);
                ztSuperMap.ReleaseSmObject(objDt);
                objDtRetsult = null;
                objDtv = null;
            }
            ztSuperMap.ReleaseSmObject(objClippedDatasets);
            ztSuperMap.ReleaseSmObject(objResultDatasource);
            ztSuperMap.ReleaseSmObject(objClippedDatasource);
            ztSuperMap.ReleaseSmObject(objDss); objDss = null;            
            return true;
            //ztSuperMap.ReleaseSmObject(objClippedDatasets);
            //ztSuperMap.ReleaseSmObject(objClippedDatasource); //20080222:以前裁切总是有问题，注解本行后好象就好了。问题在这里吗？
            //return true; 
        }
    }
}
