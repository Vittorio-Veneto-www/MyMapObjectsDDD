using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 字段集合类（即字段名称的集合，表格第一行）
    /// </summary>

    public class moFields
    {
        #region 字段

        private List<moField> _Fields;
        private string _PrimaryField;       // 主字段名称
        private bool _ShowAlias = false;    // 是否显示别名，方便外部程序

        #endregion

        #region 构造函数

        public moFields()
        {
            _Fields = new List<moField>();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取字段数量
        /// </summary>

        public Int32 Count
        {
            get { return _Fields.Count; }
        }

        /// <summary>
        /// 获取或设置主字段
        /// </summary>

        public string PrimaryField
        {
            get { return _PrimaryField; }
            set { _PrimaryField = value; }
        }

        /// <summary>
        /// 指示是否显示别名
        /// </summary>

        public bool ShowAlias
        {
            get { return _ShowAlias; }
            set { _ShowAlias = value; }
        }

        #endregion

        #region  方法

        /// <summary>
        /// 获取指定索引号的字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>

        public moField GetItem(Int32 index)
        {
            return _Fields[index];
        }

        /// <summary>
        /// 获取指定名称的字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public moField GetItem(string name)
        {
            Int32 sIndex = FindField(name);
            if (sIndex >= 0)
                return _Fields[sIndex];
            else
                return null;
        }

        /// <summary>
        /// 根据指定名称查找字段，返回其索引号，若无则返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public Int32 FindField(string name)
        {
            Int32 sFieldCount = _Fields.Count;
            for (Int32 i = 0; i <= sFieldCount - 1; i++)
            {
                if (_Fields[i].Name.ToLower() == name.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 追加一个字段
        /// </summary>
        /// <param name="field"></param>

        public void Append(moField field)
        {
            if (FindField(field.Name) >= 0) // 不允许重名
            {
                throw new Exception("Fields对象中不能存在重名的字段！");
            }
            _Fields.Add(field);
            // 触发事件
            if (FieldAppended != null)
                FieldAppended(this, field);
        }

        /// <summary>
        /// 删除指定索引号的字段
        /// </summary>
        /// <param name="index"></param>

        public void RemoveAt(Int32 index)
        {
            moField sField = _Fields[index];    // 表的维护：要同时删除所有记录中这个字段对应的值
            _Fields.RemoveAt(index);
            // 触发事件
            if (FieldRemoved != null)
                FieldRemoved(this, index, sField);
        }

        #endregion

        #region 事件

        internal delegate void FieldAppendedHandle(object sender, moField fieldAppended);   // 委托，internal意为只有组件集内部（Layer内）才能监听此事件，Layer监听此事件时，可以得知哪一个field被添加，即“fieldAppended”
        /// <summary>
        /// 有字段被加入
        /// </summary>
        internal event FieldAppendedHandle FieldAppended;

        internal delegate void FieldRemovedHandle(object sender, Int32 fieldIndex, moField fieldRemoved);
        /// <summary>
        /// 有字段被删除
        /// </summary>
        internal event FieldRemovedHandle FieldRemoved;

        #endregion
    }
}
