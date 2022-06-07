using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjectsDemo
{
    /// <summary>
    /// 坐标转换类
    /// 实现：兰伯特投影和北京54坐标系的转换
    /// </summary>
    public class moCoordinateTrans
    {
        #region 字段

        private double _a = 6378245; //长半轴
        private double _b = 6356863.01877305; // 短半轴
        private double _f; //扁率
        private double _e1; //第一偏心率
        private double _e2; //第二偏心率
        private double _B1; //第一标准纬线
        private double _B2; //第二标准纬线
        private double _L0; //原点经度
        private double _B0; //原点纬度


        /* test data
        private double _a = 6377397.155; //长半轴
        private double _b; // 短半轴
        private double _f = 0.00334277315366; //扁率
        private double _e1; //第一偏心率
        private double _e2; //第二偏心率
        private double _B1; //第一标准纬线
        private double _B2; //第二标准纬线
        private double _L0; //原点经度
        private double _B0; //远点纬度
        */

        /* 北京54坐标系 克拉索夫斯基椭球体
         * 兰伯特等角圆锥割投影
         * 标准纬线1：30°
         * 标准纬线2：62°
         * 原点经度：105°
         * 原点纬度：0°
         * 经纬度单位：均为弧度制
         */

        #endregion

        #region 构造函数
        public moCoordinateTrans()
        {

            _f = (_a - _b) / _a;
            _e1 = Math.Sqrt(1 - (_b / _a) * (_b / _a));
            _e2 = Math.Sqrt((_a - _b) * (_a - _b) - 1);
            _B1 = 30.0 / 180 * Math.PI;
            _B2 = 62.0 / 180 * Math.PI;
            _B0 = 0;
            _L0 = 105.0 / 180 * Math.PI;
            /*
            _b = Math.Sqrt(a * a * (1 - f * f));
            _e1 = Math.Sqrt(1 - (_b / _a) * (_b / _a));
            _e2 = Math.Sqrt((_a - _b) * (_a - _b) - 1);
            _B1 = 46.0 / 180 * Math.PI;
            _B2 = 49.0 / 180 * Math.PI;
            _B0 = 48.0 / 180 * Math.PI;
            _L0 = 13.333333 / 180 * Math.PI;
            */
        }
        #endregion

        #region 属性
        public double a
        {
            get { return _a; }
        }

        public double b
        {
            get { return _b; }
        }

        public double f
        {
            get { return _f; }
        }

        public double e1
        {
            get { return _e1; }
        }

        public double e2
        {
            get { return _e2; }
        }

        public double B1
        {
            get { return _B1; }
        }

        public double B2
        {
            get { return _B2; }
        }

        public double B0
        {
            get { return _B0; }
        }

        public double L0
        {
            get { return _L0; }
        }
        #endregion

        #region 方法
        public MyMapObjects.moPoint ToProjectedCoordinate(double B, double L)
        {
            // B -- 纬度 角度制
            // L -- 经度 角度制
            B = B / 180 * Math.PI;
            L = L / 180 * Math.PI;
            double X, Y; // 最后转换得到的投影坐标
            // double m = Math.Cos(B) / Math.Sqrt(1 - Math.Pow(e1 * Math.Sin(B), 2));
            // double t = Math.Tan(Math.PI / 4 - B / 2) / Math.Pow((1 - e1 * Math.Sin(B) / (1 + e1 * Math.Sin(B))), e1 / 2);
            // lat 纬度
            // lon 经度

            double m1 = CalculateM(B1);
            double m2 = CalculateM(B2);

            double t = CalculateT(B);
            double t1 = CalculateT(B1);
            double t2 = CalculateT(B2);
            double tF = CalculateT(B0);

            double n = Math.Log(m1 / m2) / Math.Log(t1 / t2);
            double F = m1 / (n * Math.Pow(t1, n));
            double r = a * F * Math.Pow(Math.Abs(t), n) * Math.Sign(t);
            double r0 = a * F * Math.Pow(Math.Abs(tF), n) * Math.Sign(tF);

            double θ = n * (L - L0);

            Y = r0 - r * Math.Cos(θ);
            X = r * Math.Sin(θ);

            MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(X, Y);
            return sPoint;
        }

        public MyMapObjects.moPoint FromProjectedCoordinate(double X, double Y)
        {
            // X,Y -- 投影坐标,单位Meter
            double B, L; //最终返回的地理坐标，单位Degree

            //double i = CalculateM(B1);
            double m1 = CalculateM(B1);
            double m2 = CalculateM(B2);

            double t1 = CalculateT(B1);
            double t2 = CalculateT(B2);
            double tF = CalculateT(B0);

            double n = Math.Log(m1 / m2) / Math.Log(t1 / t2);
            double F = m1 / (n * Math.Pow(Math.Abs(t1), n) * Math.Sign(t1));
            //double inter1 = Math.Pow(Math.Abs(t1), n);
            double r0 = a * F * Math.Pow(Math.Abs(tF), n) * Math.Sign(tF);
            //double inter1 = Math.Pow(Math.Abs(tF), n);

            //double r_ = Math.Sign(n) * Math.Sqrt(Y * Y + (r0 - X) * (r0 - X));
            double r_ = Math.Sign(n) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow((r0 - Y), 2));

            double t_ = Math.Sign(r_ / (a * F)) * Math.Pow(Math.Abs(r_ / (a * F)), 1.0 / n);
            //double inter1 = r_ / (a * F);
            double θ_ = Math.Atan(X / (r0 - Y)); //θ_代表θ' 以上都用‘_’来代替" ' " 

            B = IterateB(0, t_);
            L = θ_ / n + L0;
            B = B / Math.PI * 180;
            L = L / Math.PI * 180;
            //返回的值是角度值

            MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(B, L);
            return sPoint;
        }

        #endregion

        #region 私有函数
        private double CalculateM(double B)
        {
            double m = Math.Cos(B) / Math.Sqrt(1 - Math.Pow(e1, 2) * Math.Pow(Math.Sin(B), 2));
            // double cosB = Math.Cos(B);
            // double fenmu = Math.Sqrt(1 - Math.Pow(e1 * Math.Sin(B), 2));
            return m;
            //No Problem.
        }

        private double CalculateT(double B)
        {
            double t = Math.Tan(Math.PI / 4 - B / 2) / Math.Pow(((1 - e1 * Math.Sin(B)) / (1 + e1 * Math.Sin(B))), e1 / 2);
            //double inter1, inter2, inter3;
            //inter1 = Math.Tan(Math.PI / 4 - B / 2);
            //inter2 = 1 - e1 * Math.Sin(B);
            //inter3 = Math.Pow(((1 - e1 * Math.Sin(B)) / (1 + e1 * Math.Sin(B))), e1 / 2);
            return t;
            //No problem
        }

        private double IterateB(double B, double t_)
        {
            double B_ = Math.PI / 2 - 2 * Math.Atan(t_ * (Math.Pow((1 - e1 * Math.Sin(B)) / (1 + e1 * Math.Sin(B)), e1 / 2)));
            // 
            while (Math.Abs(B - B_) > 0.000001)
            {
                B = B_;
                B_ = Math.PI / 2 - 2 * Math.Atan(t_ * Math.Pow((1 - e1 * Math.Sin(B)) / (1 + e1 * Math.Sin(B)), e1 / 2));
                //迭代结束条件: 改变小于0.0001 
            }
            return B_;
        }
        #endregion
    }
}

