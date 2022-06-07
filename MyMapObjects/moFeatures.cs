﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 要素集合类
    /// </summary>

    public class moFeatures
    {
        #region 字段

        private List<moFeature> _Features;

        #endregion

        #region 构造函数

        public moFeatures()
        {
            _Features = new List<moFeature>();
        }

        #endregion

        #region 属性

        public Int32 Count
        {
            get{ return _Features.Count(); }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取feature的索引值
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        /// 
        public int GetIndexOf(moFeature feature)
        {
            int sIndex = _Features.IndexOf(feature);
            return sIndex;
        }

        /// <summary>
        /// 获取指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>

        public moFeature GetItem(Int32 index)
        {
            return _Features[index];
        }

        /// <summary>
        /// 设置指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        /// <param name="feature"></param>

        public void SetItem(Int32 index, moFeature feature)
        {
            _Features[index] = feature;
        }

        /// <summary>
        /// 增加一个元素
        /// </summary>
        /// <param name="feature"></param>

        public void Add(moFeature feature)
        {
            _Features.Add(feature);
        }

        /// <summary>
        /// 删除指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        
        public void RemoveAt(Int32 index)
        {
            _Features.RemoveAt(index);
        }

        /// <summary>
        /// 删除指定元素
        /// </summary>
        /// <param name="feature"></param>

        public void Remove(moFeature feature)
        {
            _Features.Remove(feature);
        }

        /// <summary>
        /// 清除所有元素
        /// </summary>

        public void Clear()
        {
            _Features.Clear();
        }


        public moFeatures Clone()
        {
            moFeatures sFeatures = new moFeatures();
            Int32 featureCount = _Features.Count;
            for (Int32 i = 0; i <= featureCount - 1; i++)
            {
                sFeatures.Add(_Features[i].Clone());
            }
            return sFeatures;
        }
        #endregion
    }
}
