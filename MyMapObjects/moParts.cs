﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 部分集合类
    /// </summary>

    public class moParts
    {
        #region 字段

        private List<moPoints> _Parts;

        #endregion

        #region 构造函数

        public moParts()
        {
            _Parts = new List<moPoints>();
        }

        public moParts(moPoints[] parts)
        {
            _Parts = new List<moPoints>();
            _Parts.AddRange(parts);

        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取部分的数量
        /// </summary>

        public Int32 Count
        {
            get { return _Parts.Count; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取指定索引号的元素
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns>指定的元素</returns>

        public moPoints GetItem(Int32 index)
        {
            return _Parts[index];
        }

        /// <summary>
        /// 设置指定索引号的元素
        /// </summary>
        /// <param name="index">索引号</param>
        /// <param name="part">新的元素</param>

        public void SetItem(Int32 index, moPoints part)
        {
            _Parts[index] = part;
        }

        /// <summary>
        /// 增加一个元素
        /// </summary>
        /// <param name="part">拟增加的部分</param>

        public void Add(moPoints part)
        {
            _Parts.Add(part);
        }

        /// <summary>
        /// 将指定数组中的元素添加到末尾
        /// </summary>
        /// <param name="parts">部分的数组</param>

        public void AddRange(moPoints[] parts)
        {
            _Parts.AddRange(parts);
        }

        // 其他关于增加、输入、删除的接口不再编写，小组作业中可根据需要自行增加

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>

        public moParts Clone()
        {
            moParts sParts = new moParts();
            Int32 sPartCount = _Parts.Count;
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                moPoints sPart = _Parts[i].Clone();
                sParts.Add(sPart);
            }
            return sParts;
        }

        #endregion
    }
}
