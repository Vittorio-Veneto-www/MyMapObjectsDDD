﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 属性字段类（字段名称，一格）
    /// </summary>

    public class moField
    {
        #region  字段

        private string _Name = "";      //字段名称
        private string _AliasName = ""; //字段别名
        private moValueTypeConstant _ValueType = moValueTypeConstant.dInt32;
        private Int32 _Length;          // 长度，针对文本类型，作业一不用DBMS也可以不做_Length
                                        // 其他字段还有“有效位数”等

        #endregion

        #region 构造函数

        public moField(string name)
        {
            _Name = name;
            _AliasName = name;
        }

        public moField(string name,moValueTypeConstant valueType)
        {
            _Name = name;
            _AliasName = name;
            _ValueType = valueType;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取字段名称
        /// </summary>

        public string Name
        {
            get { return _Name; }   // 字段名称一般只允许访问
        }

        /// <summary>
        /// 获取或设置字段别名
        /// </summary>

        public string AliasName
        {
            get { return _AliasName; }
            set { _AliasName = value; }
        }

        /// <summary>
        /// 获取值类型
        /// </summary>

        public moValueTypeConstant ValueType
        {
            get { return _ValueType; }
        }

        /// <summary>
        /// 获取字段长度
        /// </summary>

        public Int32 Length
        {
            get { return _Length; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>

        public moField Clone()
        {
            moField sField = new moField(_Name, _ValueType);
            sField._AliasName = _AliasName;
            sField._Length = _Length;
            return sField;
        }

        #endregion
    }
}
