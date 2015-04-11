/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * 
 * ��ͼ����༭�ࣨ����ʹ�ã�
 * 
 * YaoSH 2010/07
 * ---------------------------------------------------------------------
 * 
 * --------------------------------------------------------------------*/
using System;
using System.Text;
using System.Collections.Generic;

using SuperMapLib;
using AxSuperMapLib;

namespace ZTViewMap
{
    public sealed class EditorManager
    {
        private AxSuperMap supermap = null;
        private static List<int> m_Undo = new List<int>();
        private static List<int> m_Redo = new List<int>();

        public EditorManager(AxSuperMap map)
        {
            supermap = map;
        }

        #region ��ʼ�༭
        private bool StartOperatorFunction()
        {
            m_Undo.Add(0);
            return true;
        }
       
        /// <summary>
        /// ��ʼ��¼�༭����
        /// </summary>
        /// <returns></returns>
        public bool StartEditOperator()
        {            
            return StartOperatorFunction();
        }
        #endregion

        #region ֹͣ�༭
        /// <summary>
        /// ���µı༭������Ӻ󣬳��������������
        /// </summary>
        /// <returns></returns>
        private bool StopOperatorFunction()
        {
            soEditHistory edit = supermap.EditHistory;
            if (edit == null)
            {
                return false;
            }
            int Count = 0;
            for (int i = 0; i < m_Undo.Count; i++)
            {
                Count += m_Undo[i];
            }

            int historyCount = edit.EventCount;
            m_Undo[m_Undo.Count - 1] = historyCount - Count;
            m_Redo.Clear();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
            return true;
        }        
        #endregion

        /// <summary>
        /// �����༭����
        /// </summary>
        /// <returns></returns>
        public bool StopEditOperator()
        {                        
            return StopOperatorFunction();
        }

        #region ȡ���༭
        private void CancelOperatorFunction()
        {
            soEditHistory edit = supermap.EditHistory;
            if (edit == null)
            {
                return;
            }
            int historyCount = edit.EventCount;
            int Count = 0;
            for (int i = 0; i < m_Undo.Count; i++)
            {
                Count += m_Undo[i];
            }

            for (int i = 0; i < historyCount - Count; i++)
            {
                edit.RemoveEventAt(edit.EventCount);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
        }
        

        public void CancelEditOperator()
        {            
            CancelOperatorFunction();
        }
        #endregion

        #region ����������
        /// <summary>
        /// �Ƿ���Գ�������
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            if (supermap == null) return false;

            soEditHistory edit = supermap.EditHistory;
            if (edit == null) return false;
            if (edit.EventCount > 0 && m_Undo.Count > 0)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
                return true;
            }
            else
            {
                m_Undo.Clear();
                m_Redo.Clear();
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
            return false;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Undo()
        {
            soEditHistory edit = supermap.EditHistory;
            if (m_Undo.Count == 0 || edit == null)
            {
                return;
            }

            int count = m_Undo[m_Undo.Count - 1];
            m_Redo.Insert(0, count);            
            for (int i = 0; i < count; i++)
            {
                if (supermap.UndoEnabled)
                {
                    bool success = supermap.Undo();
                }
            }
            m_Undo.RemoveAt(m_Undo.Count - 1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
        }

        /// <summary>
        /// �Ƿ�����ظ�����
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            if (supermap == null) return false;
            soEditHistory edit = supermap.EditHistory;
            if (edit == null) return false;
            if (edit.EventCount > 0 && m_Redo.Count > 0)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
                return true;
            }
            else
            {
                m_Undo.Clear();
                m_Redo.Clear();
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
            return false;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Redo()
        {
            soEditHistory edit = supermap.EditHistory;
            if (m_Redo.Count == 0 || edit == null)
            {
                return;
            }

            int count = m_Redo[0];      
            
            try
            {

                for (int i = 0; i < count; i++)
                {
                    if (supermap.RedoEnabled)
                    {
                        supermap.Redo();
                    }
                }
            }
            catch
            {
            }
            m_Redo.RemoveAt(0);
            m_Undo.Add(count);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(edit);
        }
        #endregion

        /// <summary>
        /// ��ղ���
        /// </summary>
        public void Clear()
        {
            m_Undo.Clear();
            m_Redo.Clear();
        }
        //public IWorkspace GetEditorWorkSpace()
        //{
        //    return m_FeatureWorkspace as IWorkspace;
        //}
    }
}
