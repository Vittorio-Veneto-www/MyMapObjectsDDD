using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    /// <summary>
    /// 点集合
    /// </summary>

    public class moPoints : moShape
    {
        #region 字段

        private List<moPoint> _Points;
        private double _MinX = double.MaxValue, _MaxX = double.MinValue;    // 用来记录最小值的变量，通常赋给它一个最大的初值；同理...
        private double _MinY = double.MaxValue, _MaxY = double.MinValue;

        #endregion

        #region 构造函数

        public moPoints()
        {
            _Points = new List<moPoint>();
        }

        public moPoints(moPoint[] points)
        {
            _Points = new List<moPoint>();
            _Points.AddRange(points);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取点的数量
        /// </summary>

        public Int32 Count
        {
            get { return _Points.Count; }
        }

        /// <summary>
        /// 获取最小X坐标
        /// </summary>

        public double MinX
        {
            get { return _MinX; }
        }

        /// <summary>
        /// 获取最大X坐标
        /// </summary>

        public double MaxX
        {
            get { return _MaxX; }
        }

        /// <summary>
        /// 获取最小Y坐标
        /// </summary>

        public double MinY
        {
            get { return _MinY; }
        }

        /// <summary>
        /// 获取最大Y坐标
        /// </summary>

        public double MaxY
        {
            get { return _MaxY; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取指定索引号的元素
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns>指定的元素</returns>

        public moPoint GetItem(Int32 index)
        {
            return _Points[index];
        }

        /// <summary>
        /// 增加一个点
        /// </summary>
        /// <param name="point">拟增加的点</param>

        public void Add(moPoint point)
        {
            _Points.Add(point);
        }

        /// <summary>
        /// 将指定数组中的元素添加到末尾
        /// </summary>
        /// <param name="points">点的数组</param>

        public void AddRange(moPoint[] points)
        {
            _Points.AddRange(points);
        }

        /// <summary>
        /// 将指定数组中的元素插入到指定索引处
        /// </summary>
        /// <param name="index">索引号</param>
        /// <param name="points">点的数组</param>

        public void InsertRange(Int32 index, moPoint[] points)
        {
            _Points.InsertRange(index, points);
        }

        /// <summary>
        /// 将指定元素插入到指定索引处
        /// </summary>
        /// <param name="index">索引号</param>
        /// <param name="point">拟插入的点</param>

        public void Insert(Int32 index, moPoint point)
        {
            _Points.Insert(index, point);
        }

        /// <summary>
        /// 删除指定索引号的元素
        /// </summary>
        /// <param name="index">索引号</param>

        public void RemoveAt(Int32 index)
        {
            _Points.RemoveAt(index);
        }

        /// <summary>
        /// 将所有元素复制到一个数组中，并返回该数组
        /// </summary>
        /// <returns></returns>

        public moPoint[] ToArray()
        {
            return _Points.ToArray();
        }

        /// <summary>
        /// 删除所有元素
        /// </summary>

        public void Clear()
        {
            _Points.Clear();
        }

        /// <summary>
        /// 获取外包矩形
        /// </summary>
        /// <returns></returns>
        
        public moRectangle GetEnvelope()
        {
            moRectangle sRec = new moRectangle(_MinX, _MaxX, _MinY, _MaxY);
            return sRec;
        }

        /// <summary>
        /// 重新计算坐标范围
        /// </summary>

        public void UpdateExtent()
        {
            CalExtent();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>

        public moPoints Clone()
        {
            moPoints sPoints = new moPoints();
            Int32 sPointCount = _Points.Count;
            for (Int32 i = 0; i <= sPointCount - 1; i++)
            {
                moPoint sPoint = new moPoint(_Points[i].X, _Points[i].Y);
                sPoints.Add(sPoint);
            }
            sPoints._MinX = _MinX;
            sPoints._MaxX = _MaxX;
            sPoints._MinY = _MinY;
            sPoints._MaxY = _MaxY;
            return sPoints;
        }

        #endregion

        #region 私有函数

        // 计算坐标范围
        private void CalExtent()
        {
            double sMinX = double.MaxValue;
            double sMaxX = double.MinValue;
            double sMinY = double.MaxValue;
            double sMaxY = double.MinValue;
            Int32 sPointCount = _Points.Count;
            for (Int32 i = 0; i <= sPointCount - 1; i++)
            {
                if (_Points[i].X < sMinX)
                    sMinX = _Points[i].X;
                if (_Points[i].X > sMaxX)
                    sMaxX = _Points[i].X;
                if (_Points[i].Y < sMinY)
                    sMinY = _Points[i].Y;
                if (_Points[i].Y > sMaxY)
                    sMaxY = _Points[i].Y;
            }
            _MinX = sMinX;
            _MaxX = sMaxX;
            _MinY = sMinY;
            _MaxY = sMaxY;
        }

        #endregion
    }
}
