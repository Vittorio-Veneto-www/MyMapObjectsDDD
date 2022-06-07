using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 属性集合类（某个要素的属性值的集合，表格中除第一行外的任一行）
    /// </summary>

    public class moAttributes
    {
        #region 字段

        private List<Object> _Attributes;   // 最佳方案是，专门设计一个AttributeValue类来替代Object

        #endregion

        #region 构造函数

        public moAttributes()
        {
            _Attributes = new List<object>();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取指定索引号的值
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns></returns>

        public object GetItem(Int32 index)
        {
            return _Attributes[index];
        }

        /// <summary>
        /// 设置指定索引号的值
        /// </summary>
        /// <param name="index">索引号</param>
        /// <param name="value">新的值</param>

        public void SetItem(Int32 index, object value)
        {
            _Attributes[index] = value;
        }

        /// <summary>
        /// 将所有值复制到一个新的数组
        /// </summary>
        /// <returns></returns>

        public object[] ToArray()
        {
            return _Attributes.ToArray();
        }

        /// <summary>
        /// 从值数组中获取所有元素
        /// </summary>
        /// <param name="values">值数组</param>

        public void FromArray(object[] values)
        {
            _Attributes.Clear();
            _Attributes.AddRange(values);
        }

        /// <summary>
        /// 追加一个元素
        /// </summary>
        /// <param name="value">拟添加元素的值</param>

        public void Append(object value)    // 一般，使用Add时意味着同时有Insert，而Apppend意味着只能在末尾追加
        {
            _Attributes.Add(value);
        }

        /// <summary>
        /// 删除指定索引号处的元素
        /// </summary>
        /// <param name="index">索引号</param>

        public void RemoveAt(Int32 index)
        {
            _Attributes.RemoveAt(index);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>

        public moAttributes Clone()
        {
            moAttributes sAttributes = new moAttributes();
            sAttributes._Attributes.AddRange(_Attributes);
            return sAttributes;
        }

        #endregion
    }
}
