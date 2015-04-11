/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * ����ͼ������صĳ�������
 * beizhan   2007/08
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace ZTViewMap
{
    /// <summary>
    /// ����ͼ������صĳ�������
    /// </summary>
    public static class ztMapEnum
    {
        /// <summary>
        /// ��ǰ��������.�ղ���
        /// </summary>
        public const int CMD_CLASS_NONE = 0;

        /// <summary>
        /// �����������
        /// </summary>
        public const int CMD_CLASS_PLACEMENT = 1;

        /// <summary>
        /// ��ͼ������
        /// </summary>
        public const int CMD_CLASS_VIEWING = 2; 

        /// <summary>
        /// Ԫ�ز���������,һ��ָ����,�ƶ�,�޸Ľڵ�Ȳ���.
        /// </summary>
        public const int CMD_CLASS_MANIPULATION = 3;

        /// <summary>
        /// ѡ��Ԫ�ز���
        /// </summary>
        public const int CMD_CLASS_SELECT = 4;

        /// <summary>
        /// ��ӡ����.
        /// </summary>
        public const int CMD_CLASS_PLOT = 5;

        /// <summary>
        /// ����.
        /// </summary>
        public const int CMD_CLASS_MEASURE = 6;

        /// <summary>
        /// ����
        /// </summary>
        public const int CMD_CLASS_INPUT = 7; 

        /// <summary>
        /// ��λ����.
        /// </summary>
        public const int CMD_CLASS_LOCATE = 8;

        /// <summary>
        /// ���˲���.
        /// </summary>
        public const int CMD_CLASS_UNDO = 9;

        /// <summary>
        /// ���ٲ���.
        /// </summary>
        public const int CMD_CLASS_TRACK = 10;

        /// <summary>
        /// ȱʡ����,Ҳ�п�����û������Ĳ���.
        /// </summary>
        public const int CMD_CLASS_DEFAULT = 99;

    }
}
