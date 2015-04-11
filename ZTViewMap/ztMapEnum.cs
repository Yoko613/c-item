/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 跟地图窗口相关的常量定义
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
    /// 跟地图窗口相关的常量定义
    /// </summary>
    public static class ztMapEnum
    {
        /// <summary>
        /// 当前操作类型.空操作
        /// </summary>
        public const int CMD_CLASS_NONE = 0;

        /// <summary>
        /// 放置类操作．
        /// </summary>
        public const int CMD_CLASS_PLACEMENT = 1;

        /// <summary>
        /// 视图操作．
        /// </summary>
        public const int CMD_CLASS_VIEWING = 2; 

        /// <summary>
        /// 元素操作类命令,一般指拷贝,移动,修改节点等操作.
        /// </summary>
        public const int CMD_CLASS_MANIPULATION = 3;

        /// <summary>
        /// 选择元素操作
        /// </summary>
        public const int CMD_CLASS_SELECT = 4;

        /// <summary>
        /// 打印操作.
        /// </summary>
        public const int CMD_CLASS_PLOT = 5;

        /// <summary>
        /// 测量.
        /// </summary>
        public const int CMD_CLASS_MEASURE = 6;

        /// <summary>
        /// 输入
        /// </summary>
        public const int CMD_CLASS_INPUT = 7; 

        /// <summary>
        /// 定位操作.
        /// </summary>
        public const int CMD_CLASS_LOCATE = 8;

        /// <summary>
        /// 回退操作.
        /// </summary>
        public const int CMD_CLASS_UNDO = 9;

        /// <summary>
        /// 跟踪操作.
        /// </summary>
        public const int CMD_CLASS_TRACK = 10;

        /// <summary>
        /// 缺省操作,也有可能是没法归类的操作.
        /// </summary>
        public const int CMD_CLASS_DEFAULT = 99;

    }
}
