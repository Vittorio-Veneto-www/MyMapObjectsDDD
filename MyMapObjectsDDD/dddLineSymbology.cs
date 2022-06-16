using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDDD
{
    public partial class dddLineSymbology : Form
    {
        private MyMapObjects.moMapLayer mapLayer;
        private MyMapObjects.moRendererTypeConstant moRendererType;
        private MyMapObjects.moSimpleLineSymbol bufferSimpleLineSymbol; //缓存简单面符号
        private MyMapObjects.moUniqueValueRenderer bufferUniqueValueRenderer; //缓存唯一值渲染符号
        private MyMapObjects.moClassBreaksRenderer bufferClassBreaksRenderer; //缓存分级渲染符号
        //private bool isTextValueTypeExist = false; //是否存在string类型的字段
        private bool isNumberValueTypeExist = false; //是否存在Text类型的字段
        private bool isLoad = false;

        public dddLineSymbology(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            mapLayer = layer;
            moRendererType = mapLayer.Renderer.RendererType;

        }

        #region 窗口、按钮事件
        private void frmLineSymbology_Load(object sender, EventArgs e)
        {
            LoadSimpleRendererPage();
            LoadUniqueValueRendererPage();
            LoadClassBreaksRendererPage();
            isLoad = true;
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorPictureBox.BackColor = colorDialog1.Color;
                bufferSimpleLineSymbol.Color = colorDialog1.Color;
                RefreshSimpleRendererPage();
            }
        }

        private void colorSizeNumber_ValueChanged(object sender, EventArgs e)
        {
            if (bufferSimpleLineSymbol != null)
            {
                bufferSimpleLineSymbol.Size = Convert.ToDouble(colorSizeNumber.Value);
                RefreshSimpleRendererPage();
            }

        }

        private void symbolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bufferSimpleLineSymbol != null)
            {
                bufferSimpleLineSymbol.Style = IndexToSimpleLineType(symbolComboBox.SelectedIndex);
                RefreshSimpleRendererPage();
            }

        }

        private void uniqueValueFieldsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == false)
                return;
            UpdateBufferUniqueValueRenderer();
        }

        private void uniqueValueSymbolSizeNumber_ValueChanged(object sender, EventArgs e)
        {
            if (isLoad == false)
                return;
            UpdateBufferUniqueValueRenderer();
        }

        private void symbolComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == false)
                return;
            UpdateBufferUniqueValueRenderer();
        }

        private void classBreakFieldComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == false)
                return;
            //修改标签的值


            string sFieldName = classBreakFieldComboBox.Text;
            List<double> sValues = new List<double>();
            int sFieldIndex = mapLayer.AttributeFields.FindField(sFieldName);
            for (int i = 0; i < mapLayer.Features.Count; i++)
            {
                double sValue;
                object sValueObject = mapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                switch (mapLayer.AttributeFields.GetItem(sFieldIndex).ValueType)
                {
                    case MyMapObjects.moValueTypeConstant.dDouble:
                        sValue = (double)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt16:
                        sValue = (Int16)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt32:
                        sValue = (Int32)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt64:
                        sValue = (Int64)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dSingle:
                        sValue = (Single)sValueObject;
                        break;
                    default:
                        throw new Exception("属性类型加载错误");
                }
                sValues.Add(sValue);
            }

            //获取最大、最小值
            double sMaxValue = sValues.Max();
            double sMinValue = sValues.Min();
            maximumLabel.Text = sMaxValue.ToString();
            minimumLabel.Text = sMinValue.ToString();
        }

        private void btnClassBreakSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                classBreakColorPictureBox.BackColor = colorDialog1.Color;
            }
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            AddClassBreaksSymbol();
            LoadClassBreaksBufferSymbol();
        }

        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            int sIndex = classBreakComboBox.SelectedIndex;
            DeleteClassBreaksSymbol(sIndex);
            LoadClassBreaksBufferSymbol();
        }

        private void btnAutoCreateClassBreaks_Click(object sender, EventArgs e)
        {
            int slice = Convert.ToInt32(autoClassBreaksNumber.Value);
            AutoClassBreaks(slice);
            LoadClassBreaksBufferSymbol();
        }
        private void symbolPictureBox_Paint(object sender, PaintEventArgs e)
        {
            
            Point rectangleCenter = new Point(symbolPictureBox.Size.Width / 2, symbolPictureBox.Size.Height / 2);
            Point rectangleTopLeft = new Point(rectangleCenter.X - 2 * (int)bufferSimpleLineSymbol.Size,
                rectangleCenter.Y - 2 * (int)bufferSimpleLineSymbol.Size);
            Size rectangleSize = new Size(4 * (int)bufferSimpleLineSymbol.Size, 4 * (int)bufferSimpleLineSymbol.Size);
            Rectangle drawingArea = new Rectangle(new Point(0,0), symbolPictureBox.Size);
            Graphics g = e.Graphics;
            PaintSymbolPictureBox(g, drawingArea, bufferSimpleLineSymbol);

        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 加载简单渲染界面
        /// </summary>
        private void LoadSimpleRendererPage()
        {

            if (moRendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                bufferSimpleLineSymbol = (MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moSimpleRenderer)mapLayer.Renderer).Symbol);
            }
            else
            {
                bufferSimpleLineSymbol = new MyMapObjects.moSimpleLineSymbol();
                // 默认生成一个简单点符号
            }
            //初始化控件的属性
            colorPictureBox.BackColor = bufferSimpleLineSymbol.Color;
            colorSizeNumber.Value = Convert.ToDecimal(bufferSimpleLineSymbol.Size);

            //初始化ComboBox
            symbolComboBox.Items.Clear();
            symbolComboBox.Items.Add("Solid");
            symbolComboBox.Items.Add("Dash");
            symbolComboBox.Items.Add("DashDot");
            symbolComboBox.Items.Add("DashDotDot");
            symbolComboBox.Items.Add("Dot");
            int sSelectedIndex = SimpleLineTypeToIndex(bufferSimpleLineSymbol.Style);
            symbolComboBox.SelectedIndex = sSelectedIndex;

            //显示点符号样式的画板，这一部分尚未完成...
            symbolPictureBox.BackColor = Color.White;

            symbolPictureBox.Refresh();
            /*
            Bitmap bitmap = new Bitmap(symbolPictureBox.Location.X + symbolPictureBox.Width,
                symbolPictureBox.Location.Y + symbolPictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);

            //Graphics g = symbolPictureBox.CreateGraphics();
            Point rectangleCenter = new Point(symbolPictureBox.Size.Width / 2, symbolPictureBox.Size.Height / 2);
            Point rectangleTopLeft = new Point(rectangleCenter.X + (int)bufferSimpleLineSymbol.Size, 
                rectangleCenter.Y + (int)bufferSimpleLineSymbol.Size);
            Size rectangleSize = new Size(2 * (int)bufferSimpleLineSymbol.Size, 2 * (int)bufferSimpleLineSymbol.Size);
            Rectangle drawingArea = new Rectangle(rectangleTopLeft, rectangleSize);
            
            g.FillRectangle(new SolidBrush(Color.White), drawingArea);

            Rectangle rectangle = new Rectangle(new Point(30, 20), new Size(20, 20));
            g.FillRectangle(new SolidBrush(Color.White), symbolComboBox.Bounds);
            symbolPictureBox.Refresh();

            //PaintSymbolPictureBox(g,drawingArea,bufferSimpleLineSymbol);
            */

        }

        private void RefreshSimpleRendererPage()
        {

            //未完成的部分：绘制符号样品
            symbolPictureBox.Refresh();
        }

        /// <summary>
        /// 加载唯一值渲染界面
        /// </summary>
        private void LoadUniqueValueRendererPage()
        {
            // 加载符号样式


            // 加载字段
            for (int i = 0; i < mapLayer.AttributeFields.Count; i++)
            {
                MyMapObjects.moField sField = mapLayer.AttributeFields.GetItem(i);
                //if(sField.ValueType == MyMapObjects.moValueTypeConstant.dText)
                //{
                //isTextValueTypeExist = true;
                uniqueValueFieldsComboBox.Items.Add(sField.Name);
                //加入字段名
                //}
            }

            /*
            if(isTextValueTypeExist == false)
            {
                // 不存在字符类型的字段名
                uniqueValueFieldsComboBox.Items.Add("无");

                //均设置为不可用
                uniqueValueFieldGroupBox.Enabled = false;
                return;
            }*/

            if (uniqueValueFieldsComboBox.Items.Count > 0)
                uniqueValueFieldsComboBox.SelectedIndex = 0;

            if (moRendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                bufferUniqueValueRenderer = (MyMapObjects.moUniqueValueRenderer)mapLayer.Renderer;
            }
            else
            {
                bufferUniqueValueRenderer = new MyMapObjects.moUniqueValueRenderer();

                //初始化唯一值渲染
                string sFieldName = uniqueValueFieldsComboBox.Text;
                //添加字段
                bufferUniqueValueRenderer.Field = sFieldName;

                int sFieldIndex = mapLayer.AttributeFields.FindField(sFieldName);

                // 统计唯一值
                List<string> sUniqueValueList = new List<string>();
                for (int i = 0; i < mapLayer.Features.Count; i++)
                {
                    MyMapObjects.moFeature sFeature = mapLayer.Features.GetItem(i);
                    string sValue = (string)sFeature.Attributes.GetItem(sFieldIndex).ToString();
                    if (sUniqueValueList.Contains(sValue) == false)
                        sUniqueValueList.Add(sValue);
                    //如果没有相同的就添加进去
                }

                //生成对应的符号
                List<MyMapObjects.moSimpleLineSymbol> sUniqueSymbolList = new List<MyMapObjects.moSimpleLineSymbol>();

                for (int i = 0; i < sUniqueValueList.Count; i++)
                {
                    MyMapObjects.moSimpleLineSymbol Symbol = new MyMapObjects.moSimpleLineSymbol();
                    sUniqueSymbolList.Add(Symbol);
                }



                //添加唯一值
                for (int i = 0; i < sUniqueValueList.Count; i++)
                {
                    bufferUniqueValueRenderer.AddUniqueValue(sUniqueValueList[i], sUniqueSymbolList[i]);
                }
                bufferUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleLineSymbol();
            }
            if (bufferUniqueValueRenderer.Count > 0)
            {
                MyMapObjects.moSimpleLineSymbol sSymbol = ((MyMapObjects.moSimpleLineSymbol)(bufferUniqueValueRenderer.GetSymbol(0)));
                uniqueValueSymbolSizeNumber.Value = Convert.ToDecimal(sSymbol.Size);
                int sSelectIndex = SimpleLineTypeToIndex(sSymbol.Style);
                symbolComboBox2.SelectedIndex = sSelectIndex;
            }
        }

        private void UpdateBufferUniqueValueRenderer()
        {
            bufferUniqueValueRenderer = new MyMapObjects.moUniqueValueRenderer();

            //初始化唯一值渲染
            string sFieldName = uniqueValueFieldsComboBox.Text;
            //添加字段
            bufferUniqueValueRenderer.Field = sFieldName;

            int sFieldIndex = mapLayer.AttributeFields.FindField(sFieldName);

            // 统计唯一值
            List<string> sUniqueValueList = new List<string>();
            for (int i = 0; i < mapLayer.Features.Count; i++)
            {
                MyMapObjects.moFeature sFeature = mapLayer.Features.GetItem(i);
                string sValue = sFeature.Attributes.GetItem(sFieldIndex).ToString();
                if (sUniqueValueList.Contains(sValue) == false)
                    sUniqueValueList.Add(sValue);
                //如果没有相同的就添加进去
            }

            //生成对应的符号
            List<MyMapObjects.moSimpleLineSymbol> sUniqueSymbolList = new List<MyMapObjects.moSimpleLineSymbol>();



            for (int i = 0; i < sUniqueValueList.Count; i++)
            {
                MyMapObjects.moSimpleLineSymbol Symbol = new MyMapObjects.moSimpleLineSymbol();
                //对sSymbol进行修改
                Symbol.Size = Convert.ToDouble(uniqueValueSymbolSizeNumber.Value);
                Symbol.Style = IndexToSimpleLineType(symbolComboBox2.SelectedIndex);

                sUniqueSymbolList.Add(Symbol);
            }



            //添加唯一值
            for (int i = 0; i < sUniqueValueList.Count; i++)
            {
                bufferUniqueValueRenderer.AddUniqueValue(sUniqueValueList[i], sUniqueSymbolList[i]);
            }
            bufferUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleLineSymbol();

            MyMapObjects.moSimpleLineSymbol sSymbol = ((MyMapObjects.moSimpleLineSymbol)(bufferUniqueValueRenderer.DefaultSymbol));
            //uniqueValueSymbolSizeNumber.Value = Convert.ToDecimal(sSymbol.Size);
            //int sSelectIndex = SimpleLineTypeToIndex(sSymbol.Style);
            //symbolComboBox2.SelectedIndex = sSelectIndex;
        }

        /// <summary>
        /// 加载分级渲染界面
        /// </summary>
        private void LoadClassBreaksRendererPage()
        {
            //加入字段
            for (int i = 0; i < mapLayer.AttributeFields.Count; i++)
            {
                MyMapObjects.moField sField = mapLayer.AttributeFields.GetItem(i);
                if (MyMapObjects.moValueTypeConstant.dText != sField.ValueType)
                {
                    //不是文本类型
                    classBreakFieldComboBox.Items.Add(sField.Name);
                    isNumberValueTypeExist = true;
                }
            }


            if (isNumberValueTypeExist == false)
            {
                classBreakFieldComboBox.Items.Add("无");
                classBreakFieldComboBox.SelectedIndex = 0;
                maximumLabel.Text = "";
                minimumLabel.Text = "";
                classBreak.Enabled = false;
                symbolSizeGroupBox.Enabled = false;
                symbolStyleGroupBox.Enabled = false;
                symbolColorGroupBox.Enabled = false;


                return;
            }
            classBreakFieldComboBox.SelectedIndex = 0;

            if (moRendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
                bufferClassBreaksRenderer = (MyMapObjects.moClassBreaksRenderer)mapLayer.Renderer;
            else
            {
                //默认初始化
                bufferClassBreaksRenderer = new MyMapObjects.moClassBreaksRenderer();
                bufferClassBreaksRenderer.Field = classBreakFieldComboBox.Text;
                List<double> sValues = new List<double>();
                int sFieldIndex = mapLayer.AttributeFields.FindField(bufferClassBreaksRenderer.Field);
                for (int i = 0; i < mapLayer.Features.Count; i++)
                {
                    double sValue;
                    object sValueObject = mapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                    switch (mapLayer.AttributeFields.GetItem(sFieldIndex).ValueType)
                    {
                        case MyMapObjects.moValueTypeConstant.dDouble:
                            sValue = (double)sValueObject;
                            break;
                        case MyMapObjects.moValueTypeConstant.dInt16:
                            sValue = (Int16)sValueObject;
                            break;
                        case MyMapObjects.moValueTypeConstant.dInt32:
                            sValue = (Int32)sValueObject;
                            break;
                        case MyMapObjects.moValueTypeConstant.dInt64:
                            sValue = (Int64)sValueObject;
                            break;
                        case MyMapObjects.moValueTypeConstant.dSingle:
                            sValue = (Single)sValueObject;
                            break;
                        default:
                            throw new Exception("属性类型加载错误");
                    }
                    sValues.Add(sValue);
                }

                //获取最大、最小值
                double sMaxValue = sValues.Max();
                double sMinValue = sValues.Min();
                maximumLabel.Text = sMaxValue.ToString();
                minimumLabel.Text = sMinValue.ToString();

                //默认分成两级
                for (Int32 i = 0; i <= 1; i++)
                {
                    double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / 2;
                    MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol();
                    bufferClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                    classBreakComboBox.Items.Add(sValue.ToString());

                }
                Color sStartColor = Color.FromArgb(255, 255, 192, 192);
                Color sEndColor = Color.Maroon;
                bufferClassBreaksRenderer.RampColor(sStartColor, sEndColor);
                bufferClassBreaksRenderer.DefaultSymbol = new MyMapObjects.moSimpleLineSymbol();

            }
            classBreakComboBox.SelectedIndex = 0;


            LoadClassBreaksBufferSymbol();
            LoadClassBreaksDefaultSymbolArgs();
            //已添加的分级



        }

        /// <summary>
        /// 在分级窗口中加载默认的符号参数
        /// </summary>
        private void LoadClassBreaksDefaultSymbolArgs()
        {
            //字段名
            if (classBreakFieldComboBox.SelectedIndex == -1)
            {
                classBreakFieldComboBox.SelectedIndex = 0;
            }
            else
            {
                //不用管
            }

            string sFieldName = classBreakFieldComboBox.Text;
            List<double> sValues = new List<double>();
            int sFieldIndex = mapLayer.AttributeFields.FindField(sFieldName);
            for (int i = 0; i < mapLayer.Features.Count; i++)
            {
                double sValue;
                object sValueObject = mapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                switch (mapLayer.AttributeFields.GetItem(sFieldIndex).ValueType)
                {
                    case MyMapObjects.moValueTypeConstant.dDouble:
                        sValue = (double)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt16:
                        sValue = (Int16)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt32:
                        sValue = (Int32)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt64:
                        sValue = (Int64)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dSingle:
                        sValue = (Single)sValueObject;
                        break;
                    default:
                        throw new Exception("属性类型加载错误");
                }
                sValues.Add(sValue);
            }

            //获取最大、最小值
            double sMaxValue = sValues.Max();
            double sMinValue = sValues.Min();
            maximumLabel.Text = sMaxValue.ToString();
            minimumLabel.Text = sMinValue.ToString();

            //分级
            classBreakComboBox.SelectedIndex = 0;

            double sDefaultNumber = bufferClassBreaksRenderer.GetBreakValue(bufferClassBreaksRenderer.BreakCount - 1);
            //最后一个Break对应的数值
            classBreakNumber.Value = Convert.ToDecimal(sDefaultNumber);

            //符号大小
            classBreakSymbolSizeNumber.Value = 3m;

            //符号样式
            classBreakSymbolSytleComboBox.SelectedIndex = 0;

            //符号颜色
            classBreakColorPictureBox.BackColor = Color.LightGray;
        }

        /// <summary>
        /// 在分级窗口中加载buffer缓存的分级渲染
        /// </summary>
        private void LoadClassBreaksBufferSymbol()
        {
            classBreakComboBox.Items.Clear();
            for (int i = 0; i < bufferClassBreaksRenderer.BreakCount; i++)
            {
                double sBreakNumber = bufferClassBreaksRenderer.GetBreakValue(i);
                classBreakComboBox.Items.Add(sBreakNumber.ToString());
                classBreakFieldComboBox.Enabled = false;
            }
            if (classBreakComboBox.Items.Count > 0)
                classBreakComboBox.SelectedIndex = 0;


        }

        /// <summary>
        /// 分级窗口中添加符号
        /// </summary>
        private void AddClassBreaksSymbol()
        {
            double sBreakNumber = Convert.ToDouble(classBreakNumber.Value);

            //判断能否加入
            double sMaxValue, sMinValue;
            sMaxValue = Convert.ToDouble(maximumLabel.Text);
            sMinValue = Convert.ToDouble(minimumLabel.Text);

            if (sBreakNumber < sMinValue || sBreakNumber > sMaxValue)
            {
                //不在正确的输入范围之内
                MessageBox.Show("请输入最大值和最小值之内的数！");
                return;
            }

            if (bufferClassBreaksRenderer.BreakCount > 0) //如果存在之前的break
            {
                double sMaxBreak = Convert.ToDouble((string)classBreakComboBox.Items[classBreakComboBox.Items.Count - 1]);
                if (sBreakNumber <= sMaxBreak)
                {
                    //输入的break小于最大的break
                    MessageBox.Show("每个分割值都必须比原来所有的分割值大！");
                    return;
                }
                else
                {
                    //接下来才可以加入
                    MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol();
                    sSymbol.Color = classBreakColorPictureBox.BackColor;
                    sSymbol.Size = Convert.ToDouble(classBreakSymbolSizeNumber.Value);
                    sSymbol.Style = IndexToSimpleLineType(classBreakSymbolSytleComboBox.SelectedIndex);
                    bufferClassBreaksRenderer.AddBreakValue(sBreakNumber, sSymbol);
                }
            }
            else
            {
                MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol();
                sSymbol.Color = classBreakColorPictureBox.BackColor;
                sSymbol.Size = Convert.ToDouble(classBreakSymbolSizeNumber.Value);
                sSymbol.Style = IndexToSimpleLineType(classBreakSymbolSytleComboBox.SelectedIndex);
                bufferClassBreaksRenderer.AddBreakValue(sBreakNumber, sSymbol);
            }



            //加入一个break，锁定字段
            if (bufferClassBreaksRenderer.BreakCount == 1)
            {
                classBreakFieldComboBox.Enabled = false;
            }


            //添加分级ComboBox

        }

        /// <summary>
        /// 在分级窗口中删除符号
        /// </summary>
        /// <param name="index"></param>
        private void DeleteClassBreaksSymbol(int index)
        {
            //如果删完了就解除字段名锁定！
            MyMapObjects.moClassBreaksRenderer sSubstitudeRenderer = new MyMapObjects.moClassBreaksRenderer();
            sSubstitudeRenderer.Field = bufferClassBreaksRenderer.Field;
            sSubstitudeRenderer.DefaultSymbol = new MyMapObjects.moSimpleLineSymbol();

            //复制这个原先的缓存分级渲染，然后在需要删除的序号不加
            for (int i = 0; i < bufferClassBreaksRenderer.BreakCount; i++)
            {
                if (i != index)
                {
                    sSubstitudeRenderer.AddBreakValue(bufferClassBreaksRenderer.GetBreakValue(i),
                        bufferClassBreaksRenderer.GetSymbol(i));
                }
            }

            //赋值
            bufferClassBreaksRenderer = sSubstitudeRenderer;

            //没有字段了，字段可以重新选择
            if (bufferClassBreaksRenderer.BreakCount == 0)
                classBreakFieldComboBox.Enabled = true;
        }

        /// <summary>
        /// 自动生成等间隔的分割
        /// </summary>
        private void AutoClassBreaks(int slice)
        {
            bufferClassBreaksRenderer = new MyMapObjects.moClassBreaksRenderer();
            bufferClassBreaksRenderer.Field = classBreakFieldComboBox.Text;
            List<double> sValues = new List<double>();
            int sFieldIndex = mapLayer.AttributeFields.FindField(bufferClassBreaksRenderer.Field);
            for (int i = 0; i < mapLayer.Features.Count; i++)
            {
                double sValue;
                object sValueObject = mapLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                switch (mapLayer.AttributeFields.GetItem(sFieldIndex).ValueType)
                {
                    case MyMapObjects.moValueTypeConstant.dDouble:
                        sValue = (double)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt16:
                        sValue = (Int16)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt32:
                        sValue = (Int32)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dInt64:
                        sValue = (Int64)sValueObject;
                        break;
                    case MyMapObjects.moValueTypeConstant.dSingle:
                        sValue = (Single)sValueObject;
                        break;
                    default:
                        throw new Exception("属性类型加载错误");
                }
                sValues.Add(sValue);
            }

            //获取最大、最小值
            double sMaxValue = sValues.Max();
            double sMinValue = sValues.Min();
            maximumLabel.Text = sMaxValue.ToString();
            minimumLabel.Text = sMinValue.ToString();

            //默认分成两级
            for (Int32 i = 0; i < slice; i++)
            {
                double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / slice;
                MyMapObjects.moSimpleLineSymbol sSymbol = new MyMapObjects.moSimpleLineSymbol();
                bufferClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                classBreakComboBox.Items.Add(sValue.ToString());

            }
            Color sStartColor = Color.FromArgb(255, 255, 192, 192);
            Color sEndColor = Color.Maroon;
            bufferClassBreaksRenderer.RampColor(sStartColor, sEndColor);
            bufferClassBreaksRenderer.DefaultSymbol = new MyMapObjects.moSimpleLineSymbol();
        }

        /// <summary>
        /// 保存该窗口进行的编辑
        /// </summary>
        public void SaveEdit()
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                //简单渲染界面的保存
                MyMapObjects.moSimpleRenderer sSimpleRenderer = new MyMapObjects.moSimpleRenderer();
                sSimpleRenderer.Symbol = bufferSimpleLineSymbol;
                mapLayer.Renderer = sSimpleRenderer;
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                //唯一值渲染的保存
                //UpdateBufferUniqueValueRenderer();
                mapLayer.Renderer = bufferUniqueValueRenderer;
            }
            else
            {
                //分级渲染的保存
                mapLayer.Renderer = bufferClassBreaksRenderer;
            }
        }

        /// <summary>
        /// 根据index值返回对应的简单点符号常数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private MyMapObjects.moSimpleLineSymbolStyleConstant IndexToSimpleLineType(int index)
        {
            MyMapObjects.moSimpleLineSymbolStyleConstant sSimpleLineSymbolStyleConstant;
            switch (index)
            {
                case 0:
                    sSimpleLineSymbolStyleConstant = MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
                    break;
                case 1:
                    sSimpleLineSymbolStyleConstant = MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
                    break;
                case 2:
                    sSimpleLineSymbolStyleConstant = MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot;
                    break;
                case 3:
                    sSimpleLineSymbolStyleConstant = MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot;
                    break;
                case 4:
                    sSimpleLineSymbolStyleConstant = MyMapObjects.moSimpleLineSymbolStyleConstant.Dot;
                    break;
                default:
                    throw new Exception("函数输入值错误！");

            }
            return sSimpleLineSymbolStyleConstant;
        }

        /// <summary>
        /// 根据简单点符号常数返回对应的index值
        /// </summary>
        /// <param name="constant"></param>
        /// <returns></returns>
        private int SimpleLineTypeToIndex(MyMapObjects.moSimpleLineSymbolStyleConstant constant)
        {
            int index = -1;
            switch (constant)
            {
                case MyMapObjects.moSimpleLineSymbolStyleConstant.Solid:
                    index = 0;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.Dash:
                    index = 1;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot:
                    index = 2;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot:
                    index = 3;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.Dot:
                    index = 4;
                    break;
            }
            return index;
        }

        
        /// <summary>
        /// 根据颜色、大小、符号Style在PictureBox绘制一个样例
        /// </summary>
        private void PaintSymbolPictureBox(Graphics g, Rectangle drawingArea, MyMapObjects.moSimpleLineSymbol symbol)
        {
            if (symbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.Solid)
            {
                Pen sPen = new Pen(new SolidBrush(symbol.Color));
                sPen.Width = (float)colorSizeNumber.Value;
                g.DrawLine(sPen, drawingArea.X + drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2, drawingArea.X + 5 * drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2);
                sPen.Dispose();
            }
            else if (symbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.Dash)
            {
                Pen sPen = new Pen(new SolidBrush(symbol.Color));
                sPen.Width = (float)colorSizeNumber.Value;
                sPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawLine(sPen, drawingArea.X + drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2, drawingArea.X + 5 * drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2);
                sPen.Dispose();
            }
            else if (symbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot)
            {
                Pen sPen = new Pen(new SolidBrush(symbol.Color));
                sPen.Width = (float)colorSizeNumber.Value;
                sPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                g.DrawLine(sPen, drawingArea.X + drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2, drawingArea.X + 5 * drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2);
                sPen.Dispose();
            }
            else if (symbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot)
            {
                Pen sPen = new Pen(new SolidBrush(symbol.Color));
                sPen.Width = (float)colorSizeNumber.Value;
                sPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                g.DrawLine(sPen, drawingArea.X + drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2, drawingArea.X + 5 * drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2);
                sPen.Dispose();
            }
            else if (symbol.Style == MyMapObjects.moSimpleLineSymbolStyleConstant.Dot)
            {
                Pen sPen = new Pen(new SolidBrush(symbol.Color));
                sPen.Width = (float)colorSizeNumber.Value;
                sPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                g.DrawLine(sPen, drawingArea.X + drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2, drawingArea.X + 5 * drawingArea.Width / 6, drawingArea.Y + drawingArea.Height / 2);
                sPen.Dispose();
            }
        }
        

        /// <summary>
        /// 从两个RGBColor均等切分，找出对应的那一种颜色
        /// </summary>
        /// <param name="startColor"></param>
        /// <param name="endColor"></param>
        /// <param name="slice"> 切分片数</param>
        /// <param name="index">第几块</param>
        /// <returns></returns>
        private Color ColorSplit(Color startColor, Color endColor, int slice, int index)
        {
            int R_piece = (endColor.R - startColor.R) / slice;
            int G_piece = (endColor.G - startColor.G) / slice;
            int B_piece = (endColor.B - startColor.B) / slice;

            int R_new = startColor.R + index * R_piece;
            int G_new = startColor.G + index * G_piece;
            int B_new = startColor.B + index * B_piece;

            Color color = Color.FromArgb(R_new, G_new, B_new);
            return color;
        }




        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void classBreakSymbolSytleComboBox_TabIndexChanged(object sender, EventArgs e)
        {
            //buffer
        }
    }
}
