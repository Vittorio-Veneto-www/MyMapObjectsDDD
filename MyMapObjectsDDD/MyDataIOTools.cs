using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyMapObjectsDDD
{
    internal static class MyDataIOTools
    {
        #region 程序集方法
        internal static MyMapObjects.moMapLayer MyLoadMapLayer(StreamReader sr)
        {
            string sName = sr.ReadLine();           //名称
            string sDescription = sr.ReadLine();    //描述
            MyMapObjects.moGeometryTypeConstant sGeometryType = (MyMapObjects.moGeometryTypeConstant)Convert.ToInt32(sr.ReadLine());
            MyMapObjects.moFields sFields = MyLoadFields(sr);
            MyMapObjects.moFeatures sFeatures = MyLoadFeatures(sGeometryType, sFields, sr);
            MyMapObjects.moMapLayer sMapLayer = new MyMapObjects.moMapLayer("", sGeometryType, sFields);
            sMapLayer.Features = sFeatures;
            sMapLayer.Name = sName;
            sMapLayer.Description = sDescription;
            return sMapLayer;
        }

        internal static MyMapObjects.moMapLayer ShpFileLoad(FileStream stream)
        {
            //获取名称
            string[] sSplitPath = stream.Name.Split('\\');
            string sName = sSplitPath[sSplitPath.Count() - 1];
            sName = sName.Substring(0, sName.Length - 4);
            //读数据
            BinaryReader sr = new BinaryReader(stream);
            sr.ReadBytes(24);   //跳过头部
            Int32 sFileLengt = sr.ReadInt32();  //长度
            Int32 sVersion = sr.ReadInt32();    //版本
            MyMapObjects.moGeometryTypeConstant sGeometryType = (MyMapObjects.moGeometryTypeConstant)Convert.ToInt32(sr.ReadInt32() / 2);   //类型：1为点，3为线，5为面;转化为GeometryType0,1,2
            /*
            double sXmin = sr.ReadDouble();
            double sYmin = sr.ReadDouble();
            double sXmax = sr.ReadDouble();
            double sYmax = sr.ReadDouble();
            */
            sr.ReadBytes(64);
            MyMapObjects.moFeatures sFeatures = ShpLoadFeatures(sGeometryType, sr);
            MyMapObjects.moMapLayer sMapLayer = new MyMapObjects.moMapLayer("", sGeometryType);
            sMapLayer.Features = sFeatures;
            sMapLayer.Name = sName;
            return sMapLayer;
        }

        //保存图层
        internal static void SaveMapLayer(MyMapObjects.moMapLayer layer, string path)
        {
            if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                SavePointLayer(layer, path);
            }
            if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                SaveMultiLineLayer(layer, path);
            }
            if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                SaveMultiPolygonLayer(layer, path);
            }
        }

        internal static void SaveProject(MyMapObjects.moLayers layers, string path)
        {
            var fsc = new FileStream(path, FileMode.Create, FileAccess.Write);  //无txt就创建
            fsc.Close();
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            TextWriter tw = new StreamWriter(fs, Encoding.UTF8);

            //记录图层数
            Int32 sLayersCount = layers.Count;
            tw.WriteLine(sLayersCount.ToString());
            tw.Flush(); tw.Close(); fs.Close();

            for (Int32 i = 0; i < sLayersCount; i++)
            {
                SaveMapLayer(layers.GetItem(i), path);
            }
        }

        internal static MyMapObjects.moLayers LoadProject(StreamReader sr)
        {
            Int32 sLayersCount = Convert.ToInt32(sr.ReadLine());
            MyMapObjects.moLayers sLayers = new MyMapObjects.moLayers();
            for (Int32 i = 0; i < sLayersCount; i++)
            {
                sLayers.Add(MyLoadMapLayer(sr));
            }
            return sLayers;
        }

        #endregion

        #region 私有函数
        //字段load
        private static MyMapObjects.moFields MyLoadFields(StreamReader sr)
        {
            Int32 sFieldCount = Convert.ToInt32(sr.ReadLine()); //字段数量
            MyMapObjects.moFields sFields = new MyMapObjects.moFields();
            for (Int32 i = 0; i <= sFieldCount - 1; i++)
            {
                string[] sLine = sr.ReadLine().Split('\t');
                string sName = sLine[0];
                MyMapObjects.moValueTypeConstant sValueType = (MyMapObjects.moValueTypeConstant)Convert.ToInt32(sLine[1]);
                //Int32 sTemp = Convert.ToInt32(sLine[1]);   //不需要；
                MyMapObjects.moField sField = new MyMapObjects.moField(sName, sValueType);
                sFields.Append(sField);
            }
            return sFields;
        }

        //要素load
        private static MyMapObjects.moFeatures MyLoadFeatures(MyMapObjects.moGeometryTypeConstant geometryType, MyMapObjects.moFields fields, StreamReader sr)
        {
            MyMapObjects.moFeatures sFeatures = new MyMapObjects.moFeatures();
            Int32 sFeatureCount = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i <= sFeatureCount - 1; i++)
            {
                MyMapObjects.moGeometry sGeometry = MyLoadGeometry(geometryType, sr);
                MyMapObjects.moAttributes sAttributes = MyLoadAttributes(fields, sr);
                MyMapObjects.moFeature sFeature = new MyMapObjects.moFeature(geometryType, sGeometry, sAttributes);
                sFeatures.Add(sFeature);
            }
            return sFeatures;
        }

        //几何load
        private static MyMapObjects.moGeometry MyLoadGeometry(MyMapObjects.moGeometryTypeConstant geometryType, StreamReader sr)
        {
            if (geometryType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                MyMapObjects.moPoint sPoint = MyLoadPoint(sr);
                return sPoint;
            }
            else if (geometryType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline sMultiPolyline = MyLoadMultiPolyline(sr);
                return sMultiPolyline;
            }
            else if (geometryType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon sMultiPolygon = MyLoadMultiPolygon(sr);
                return sMultiPolygon;
            }
            else
                return null;
        }

        //点
        private static MyMapObjects.moPoint MyLoadPoint(StreamReader sr)
        {
            //原数据支持多点，按照多点读取，然后返回多点的第一个点
            string str = sr.ReadLine();
            Int32 sPointCount = Convert.ToInt32(str);
            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
            for (Int32 i = 0; i <= sPointCount - 1; i++)
            {
                string[] sLine = sr.ReadLine().Split('\t');
                double sX = Convert.ToDouble(sLine[0]);
                double sY = Convert.ToDouble(sLine[1]);
                MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(sX, sY);
                sPoints.Add(sPoint);
            }
            return sPoints.GetItem(0);
        }

        //线
        private static MyMapObjects.moMultiPolyline MyLoadMultiPolyline(StreamReader sr)
        {
            MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();
            Int32 sPartCount = Convert.ToInt32(sr.ReadLine());
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                Int32 sPointCount = Convert.ToInt32(sr.ReadLine());
                for (Int32 j = 0; j <= sPointCount - 1; j++)
                {
                    string[] sLine = sr.ReadLine().Split('\t');
                    double sX = Convert.ToDouble(sLine[0]);
                    double sY = Convert.ToDouble(sLine[1]);
                    MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(sX, sY);
                    sPoints.Add(sPoint);
                }
                sMultiPolyline.Parts.Add(sPoints);
            }
            sMultiPolyline.UpdateExtent();
            return sMultiPolyline;
        }

        //面
        private static MyMapObjects.moMultiPolygon MyLoadMultiPolygon(StreamReader sr)
        {
            MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();
            Int32 sPartCount = Convert.ToInt32(sr.ReadLine());
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                Int32 sPointCount = Convert.ToInt32(sr.ReadLine());
                for (Int32 j = 0; j <= sPointCount - 1; j++)
                {
                    string[] sLine = sr.ReadLine().Split('\t');
                    double sX = Convert.ToDouble(sLine[0]);
                    double sY = Convert.ToDouble(sLine[1]);
                    MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(sX, sY);
                    sPoints.Add(sPoint);
                }
                sMultiPolygon.Parts.Add(sPoints);
            }
            sMultiPolygon.UpdateExtent();
            return sMultiPolygon;
        }

        //属性
        private static MyMapObjects.moAttributes MyLoadAttributes(MyMapObjects.moFields fields, StreamReader sr)
        {
            Int32 sFieldCount = fields.Count;
            MyMapObjects.moAttributes sAttributes = new MyMapObjects.moAttributes();
            string[] sLine = sr.ReadLine().Split('\t');
            for (Int32 i = 0; i <= sFieldCount - 1; i++)
            {
                MyMapObjects.moField sField = fields.GetItem(i);
                object sValue = MyLoadValue(sField.ValueType, sLine[i]);
                sAttributes.Append(sValue);
            }
            return sAttributes;
        }

        //值转换
        private static object MyLoadValue(MyMapObjects.moValueTypeConstant valueType, string str)
        {
            if (valueType == MyMapObjects.moValueTypeConstant.dInt16)
            {
                Int16 sValue = Convert.ToInt16(str);
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dInt32)
            {
                Int32 sValue = Convert.ToInt32(str);
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dInt64)
            {
                Int64 sValue = Convert.ToInt64(str);
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dSingle)
            {
                float sValue = Convert.ToSingle(str);
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dDouble)
            {
                double sValue = Convert.ToDouble(str);
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dText)
            {
                string sValue = str.ToString();
                return sValue;
            }
            else
            {
                return null;
            }
        }

        //保存点图层
        internal static void SavePointLayer(MyMapObjects.moMapLayer layer, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            TextWriter tw = new StreamWriter(fs, Encoding.UTF8);
            StringBuilder builder = new StringBuilder();
            string row;
            Int32 i, j;

            builder.AppendLine(layer.Name);         //名称
            builder.AppendLine(layer.Description);  //描述
            tw.Write(builder.ToString());
            builder.Clear();

            SaveFields(layer, tw);

            Int32 sFieldCount = layer.AttributeFields.Count;//字段数
            Int32 sFeatureCount = layer.Features.Count; //要素数
            builder.AppendLine(sFeatureCount.ToString());

            for (i = 0; i < sFeatureCount; i++)
            {
                builder.AppendLine("1");
                //点坐标
                MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry;
                row = sPoint.X.ToString() + "\t" + sPoint.Y.ToString();
                builder.AppendLine(row);
                tw.Write(builder.ToString());
                builder.Clear();

                //属性
                SaveAttributes(layer.Features.GetItem(i), tw, sFieldCount);
            }

            tw.Flush();
            tw.Close();
            fs.Close();
        }

        //保存线图层
        internal static void SaveMultiLineLayer(MyMapObjects.moMapLayer layer, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            TextWriter tw = new StreamWriter(fs, Encoding.UTF8);
            StringBuilder builder = new StringBuilder();
            string row;
            Int32 i, j, k;

            builder.AppendLine(layer.Name);         //名称
            builder.AppendLine(layer.Description);  //描述
            tw.Write(builder.ToString());
            builder.Clear();

            SaveFields(layer, tw);

            Int32 sFieldCount = layer.AttributeFields.Count;//字段数
            Int32 sFeatureCount = layer.Features.Count; //要素数
            builder.AppendLine(sFeatureCount.ToString());

            for (i = 0; i < sFeatureCount; i++)
            {
                MyMapObjects.moMultiPolyline sPolyLine = (MyMapObjects.moMultiPolyline)layer.Features.GetItem(i).Geometry;
                //线数
                Int32 PartsCount = sPolyLine.Parts.Count;
                builder.AppendLine(PartsCount.ToString());

                for (j = 0; j < PartsCount; j++)
                {
                    //一条线点数
                    MyMapObjects.moPoints sPoints = sPolyLine.Parts.GetItem(j);
                    Int32 PointCount = sPoints.Count;
                    builder.AppendLine(PointCount.ToString());
                    for (k = 0; k < PointCount; k++)
                    {
                        row = sPoints.GetItem(k).X.ToString() + "\t" + sPoints.GetItem(k).Y.ToString();
                        builder.AppendLine(row);
                    }
                }
                tw.Write(builder.ToString());
                builder.Clear();

                //属性
                SaveAttributes(layer.Features.GetItem(i), tw, sFieldCount);
            }

            tw.Flush();
            tw.Close();
            fs.Close();
        }

        //保存面图层
        internal static void SaveMultiPolygonLayer(MyMapObjects.moMapLayer layer, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            TextWriter tw = new StreamWriter(fs, Encoding.UTF8);
            StringBuilder builder = new StringBuilder();
            string row;
            Int32 i, j, k;

            builder.AppendLine(layer.Name);         //名称
            builder.AppendLine(layer.Description);  //描述
            tw.Write(builder.ToString());
            builder.Clear();

            SaveFields(layer, tw);

            Int32 sFieldCount = layer.AttributeFields.Count;//字段数
            Int32 sFeatureCount = layer.Features.Count; //要素数
            builder.AppendLine(sFeatureCount.ToString());

            for (i = 0; i < sFeatureCount; i++)
            {
                MyMapObjects.moMultiPolygon sPolygon = (MyMapObjects.moMultiPolygon)layer.Features.GetItem(i).Geometry;
                //面数
                Int32 PartsCount = sPolygon.Parts.Count;
                builder.AppendLine(PartsCount.ToString());

                for (j = 0; j < PartsCount; j++)
                {
                    //一面点数
                    MyMapObjects.moPoints sPoints = sPolygon.Parts.GetItem(j);
                    Int32 PointCount = sPoints.Count;
                    builder.AppendLine(PointCount.ToString());
                    for (k = 0; k < PointCount; k++)
                    {
                        row = sPoints.GetItem(k).X.ToString() + "\t" + sPoints.GetItem(k).Y.ToString();
                        builder.AppendLine(row);
                    }
                }
                tw.Write(builder.ToString());
                builder.Clear();

                //属性
                SaveAttributes(layer.Features.GetItem(i), tw, sFieldCount);
            }

            tw.Flush();
            tw.Close();
            fs.Close();
        }

        //字段保存
        private static void SaveFields(MyMapObjects.moMapLayer layer, TextWriter tw)
        {
            StringBuilder builder = new StringBuilder();
            string row = ((int)layer.ShapeType).ToString();//类型
            builder.AppendLine(row);

            Int32 sFieldCount = layer.AttributeFields.Count;//字段数
            builder.AppendLine(sFieldCount.ToString());

            for (Int32 i = 0; i < sFieldCount; i++)
            {
                string sName = layer.AttributeFields.GetItem(i).Name;
                string sType = ((int)layer.AttributeFields.GetItem(i).ValueType).ToString();
                row = sName + "\t" + sType;
                builder.AppendLine(row);
            }

            tw.Write(builder.ToString());
        }

        //属性保存
        private static void SaveAttributes(MyMapObjects.moFeature feature, TextWriter tw, Int32 fieldCount)
        {
            string row = feature.Attributes.GetItem(0).ToString();
            StringBuilder builder = new StringBuilder();
            for (Int32 j = 1; j < fieldCount; j++)
            {
                row = row + "\t" + feature.Attributes.GetItem(j).ToString();
            }

            builder.AppendLine(row);
            tw.Write(builder.ToString());
        }

        //shpload要素
        private static MyMapObjects.moFeatures ShpLoadFeatures(MyMapObjects.moGeometryTypeConstant geometryType, BinaryReader sr)
        {
            MyMapObjects.moFeatures sFeatures = new MyMapObjects.moFeatures();
            while (sr.PeekChar() != -1)
            {
                MyMapObjects.moGeometry sGeometry = ShpLoadGeometry(geometryType, sr);
                MyMapObjects.moFeature sFeature = new MyMapObjects.moFeature(geometryType, sGeometry, new MyMapObjects.moAttributes());
                sFeatures.Add(sFeature);
            }
            return sFeatures;
        }

        //shp几何
        private static MyMapObjects.moGeometry ShpLoadGeometry(MyMapObjects.moGeometryTypeConstant geometryType, BinaryReader sr)
        {
            if (geometryType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                MyMapObjects.moPoint sPoint = ShpLoadPoint(sr);
                return sPoint;
            }
            else if (geometryType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline sMultiPolyline = ShpLoadMultiPolyline(sr);
                return sMultiPolyline;
            }
            else if (geometryType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon sMultiPolygon = ShpLoadMultiPolygon(sr);
                return sMultiPolygon;
            }
            else
                return null;
        }

        //Shp点
        private static MyMapObjects.moPoint ShpLoadPoint(BinaryReader sr)
        {
            MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint();
            uint RecordNum = sr.ReadUInt32();
            int DataLength = sr.ReadInt32();
            sr.ReadInt32();
            sPoint.X = sr.ReadDouble();
            sPoint.Y = sr.ReadDouble();
            return sPoint;
        }

        //Shp线
        private static MyMapObjects.moMultiPolyline ShpLoadMultiPolyline(BinaryReader sr)
        {
            MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();

            uint RecordNum = sr.ReadUInt32();
            int DataLength = sr.ReadInt32();

            sr.ReadInt32();
            sr.ReadBytes(32);

            Int32 sNumParts = sr.ReadInt32();
            Int32 sNumPoints = sr.ReadInt32();

            List<Int32> sPartIndex = new List<int>();
            for (Int32 i = 0; i < sNumParts; i++)
            {
                int parts = sr.ReadInt32();
                sPartIndex.Add(parts);
            }
            sPartIndex.Add(sNumPoints);

            for (Int32 i = 0; i <= sNumParts - 1; i++)
            {
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();

                for (Int32 j = sPartIndex[i]; j <= sPartIndex[i + 1] - 1; j++)
                {
                    double sX = sr.ReadDouble();
                    double sY = sr.ReadDouble();
                    MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(sX, sY);
                    sPoints.Add(sPoint);
                }
                sMultiPolyline.Parts.Add(sPoints);
            }

            sMultiPolyline.UpdateExtent();
            return sMultiPolyline;
        }

        //shp面
        private static MyMapObjects.moMultiPolygon ShpLoadMultiPolygon(BinaryReader sr)
        {
            MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();

            uint RecordNum = sr.ReadUInt32();   //记录号
            int DataLength = sr.ReadInt32();    //长度
            int Type = sr.ReadInt32();          //类型5
            sr.ReadBytes(32);                   //跳过外包矩形


            Int32 sNumParts = sr.ReadInt32();
            Int32 sNumPoints = sr.ReadInt32();

            List<Int32> sPartIndex = new List<int>();//起始位置
            for (Int32 i = 0; i < sNumParts; i++)
            {
                int parts = sr.ReadInt32();
                sPartIndex.Add(parts);
            }
            sPartIndex.Add(sNumPoints);

            for (Int32 i = 0; i <= sNumParts - 1; i++)
            {
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();

                for (Int32 j = sPartIndex[i]; j <= sPartIndex[i + 1] - 1; j++)
                {
                    double sX = sr.ReadDouble();
                    double sY = sr.ReadDouble();
                    MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(sX, sY);
                    sPoints.Add(sPoint);
                }
                sMultiPolygon.Parts.Add(sPoints);
            }
            sMultiPolygon.UpdateExtent();
            return sMultiPolygon;
        }
        #endregion
    }
}
