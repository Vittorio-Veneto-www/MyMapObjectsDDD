using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 点
    /// </summary>
    public class moPoint:moGeometry
    {
        #region 字段

        private double _X, _Y;  // x、y坐标；需要通过接口调用的参数，微软建议前加下划线“_”


        #endregion

        #region 构造函数

        public moPoint()
        { }

        public moPoint(double x,double y)   // 形参，微软建议第一个字母小写
        {
            _X = x;
            _Y = y;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置X坐标
        /// </summary>
        public double X
        {
            get { return _X; }      // moPoint A   X = A.X (get)   A.X = X (set)
            set { _X = value; }
        }

        /// <summary>
        /// 获取或设置Y坐标
        /// </summary>
        public double Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>        
        public moPoint Clone()  // 克隆，从基本数据类型开始的复制才是克隆，直接令 moPoint B = A 不是克隆，而是让B和A指向了同一个位置
        {
            moPoint sPoint = new moPoint(_X, _Y);   // "s" means "sub"
            return sPoint;
        }

        #endregion
    }
}
