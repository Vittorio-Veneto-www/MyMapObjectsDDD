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
    public partial class dddFillSymbology : Form
    {
        MyMapObjects.moMapLayer mapLayer;
        private MyMapObjects.moRendererTypeConstant moRendererType;
        private MyMapObjects.moSimpleFillSymbol bufferSimpleFillSymbol; //缓存简单面符号
        private MyMapObjects.moUniqueValueRenderer bufferUniqueValueRenderer; //缓存唯一值渲染符号
        private MyMapObjects.moClassBreaksRenderer bufferClassBreaksRenderer; //缓存分级渲染符号
        private bool isTextValueTypeExist = false; //是否存在string类型的字段
        private bool isNumberValueTypeExist = false; //是否存在Text类型的字段
        private bool isLoad = false;
        public dddFillSymbology(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            mapLayer = layer;
            moRendererType = mapLayer.Renderer.RendererType;
        }

        #region 私有函数
        private void LoadSimpleRendererPage()
        {
            //Load bufferRenderer
            if(moRendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                bufferSimpleFillSymbol = (MyMapObjects.moSimpleFillSymbol)(((MyMapObjects.moSimpleRenderer)mapLayer.Renderer).Symbol).Clone();
            }
            else
            {
                //新建一个简单面符号
                bufferSimpleFillSymbol = new MyMapObjects.moSimpleFillSymbol();
            }

            //初始化控件的属性
            pictureBoxSimpleRendererFillColor.BackColor = bufferSimpleFillSymbol.Color;

            MyMapObjects.moSimpleLineSymbol sOutLine = bufferSimpleFillSymbol.Outline;
            pictureBoxSimpleRendererOutlineColor.BackColor = sOutLine.Color;
            numSimpleRendererOutlineWidth.Value = Convert.ToDecimal(sOutLine.Size);
            cbbSimpleRendererOutlineStyle.SelectedIndex = SimpleLineSymbolStyleToIndex(sOutLine.Style);
        }

        private void UpdateBufferSimpleRenderer()
        {
            bufferSimpleFillSymbol.Color = pictureBoxSimpleRendererFillColor.BackColor;
            MyMapObjects.moSimpleLineSymbol sOutLine = new MyMapObjects.moSimpleLineSymbol();
            sOutLine.Color = pictureBoxSimpleRendererOutlineColor.BackColor;
            sOutLine.Style = IndexToSimpleLineSymbolStyle(cbbSimpleRendererOutlineStyle.SelectedIndex);
            sOutLine.Size = Convert.ToDouble(numSimpleRendererOutlineWidth.Value);
            bufferSimpleFillSymbol.Outline = sOutLine;
        }

        private void LoadUniqueValueRendererPage()
        {
            // 加载字段
            for (int i = 0; i < mapLayer.AttributeFields.Count; i++)
            {
                MyMapObjects.moField sField = mapLayer.AttributeFields.GetItem(i);
                if (sField.ValueType == MyMapObjects.moValueTypeConstant.dText)
                {
                    isTextValueTypeExist = true;
                    //加入字段名
                }
                cbbUniqueValueRendererFieldName.Items.Add(sField.Name);
            }
            if (cbbUniqueValueRendererFieldName.Items.Count > 0)
                cbbUniqueValueRendererFieldName.SelectedIndex = 0;

            //加载控件
            pictureBoxUniqueValueRendererOutlineColor.BackColor = Color.LightGray;
            numUniqueValueRendererOutlineWidth.Value = 0.35m;
            cbbUniqueValueRendererOutlineStyle.SelectedIndex = 0;

            //初始化bufferRenderer
            if (moRendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                bufferUniqueValueRenderer = (MyMapObjects.moUniqueValueRenderer)mapLayer.Renderer.Clone();
            }
            else
            {
                bufferUniqueValueRenderer = new MyMapObjects.moUniqueValueRenderer();

                //初始化唯一值渲染
                string sFieldName = cbbUniqueValueRendererFieldName.Text;
                //添加字段
                bufferUniqueValueRenderer.Field = sFieldName;

                int sFieldIndex = mapLayer.AttributeFields.FindField(sFieldName);

                // 统计唯一值
                List<string> sUniqueValueList = new List<string>();
                for (int i = 0; i < mapLayer.Features.Count; i++)
                {
                    MyMapObjects.moFeature sFeature = mapLayer.Features.GetItem(i);
                    string sValue = (string)sFeature.Attributes.GetItem(sFieldIndex);
                    if (sUniqueValueList.Contains(sValue) == false)
                        sUniqueValueList.Add(sValue);
                    //如果没有相同的就添加进去
                }

                //生成对应的符号
                List<MyMapObjects.moSimpleFillSymbol> sUniqueSymbolList = new List<MyMapObjects.moSimpleFillSymbol>();


                for (int i = 0; i < sUniqueValueList.Count; i++)
                {
                    MyMapObjects.moSimpleFillSymbol Symbol = new MyMapObjects.moSimpleFillSymbol();
                    MyMapObjects.moSimpleLineSymbol Outline = new MyMapObjects.moSimpleLineSymbol();
                    Outline.Color = pictureBoxUniqueValueRendererOutlineColor.BackColor;
                    Outline.Size = Convert.ToDouble(numUniqueValueRendererOutlineWidth.Value);
                    Outline.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
                    Symbol.Outline = Outline;
                    sUniqueSymbolList.Add(Symbol);
                }



                //添加唯一值
                for (int i = 0; i < sUniqueValueList.Count; i++)
                {
                    bufferUniqueValueRenderer.AddUniqueValue(sUniqueValueList[i], sUniqueSymbolList[i]);
                }
                bufferUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
            }
            if (bufferUniqueValueRenderer.Count > 0)
            {
                MyMapObjects.moSimpleFillSymbol sSymbol = ((MyMapObjects.moSimpleFillSymbol)bufferUniqueValueRenderer.GetSymbol(0));
                pictureBoxUniqueValueRendererOutlineColor.BackColor = sSymbol.Outline.Color;
                numUniqueValueRendererOutlineWidth.Value = Convert.ToDecimal(sSymbol.Outline.Size);
                cbbUniqueValueRendererOutlineStyle.SelectedIndex = SimpleLineSymbolStyleToIndex(sSymbol.Outline.Style);
            }
        }
        

        private void UpdateBufferUniqueValueRenderer()
        {
            bufferUniqueValueRenderer = new MyMapObjects.moUniqueValueRenderer();
            string sFieldName = cbbUniqueValueRendererFieldName.Text;
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
            List<MyMapObjects.moSimpleFillSymbol> sUniqueSymbolList = new List<MyMapObjects.moSimpleFillSymbol>();


            for (int i = 0; i < sUniqueValueList.Count; i++)
            {
                MyMapObjects.moSimpleFillSymbol Symbol = new MyMapObjects.moSimpleFillSymbol();
                MyMapObjects.moSimpleLineSymbol Outline = new MyMapObjects.moSimpleLineSymbol();
                Outline.Color = pictureBoxUniqueValueRendererOutlineColor.BackColor;
                Outline.Size = Convert.ToDouble(numUniqueValueRendererOutlineWidth.Value);
                Outline.Style = IndexToSimpleLineSymbolStyle(cbbUniqueValueRendererOutlineStyle.SelectedIndex);
                Symbol.Outline = Outline;
                sUniqueSymbolList.Add(Symbol);
            }



            //添加唯一值
            for (int i = 0; i < sUniqueValueList.Count; i++)
            {
                bufferUniqueValueRenderer.AddUniqueValue(sUniqueValueList[i], sUniqueSymbolList[i]);
            }
            bufferUniqueValueRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();

        }
        

        private void LoadClassBreaksRendererPage()
        {
            //加入字段
            for (int i = 0; i < mapLayer.AttributeFields.Count; i++)
            {
                MyMapObjects.moField sField = mapLayer.AttributeFields.GetItem(i);
                if (MyMapObjects.moValueTypeConstant.dText != sField.ValueType)
                {
                    //不是文本类型
                    cbbClassBreaksFieldName.Items.Add(sField.Name);
                    isNumberValueTypeExist = true;
                }
            }

            // 没有数字字段
            if (isNumberValueTypeExist == false)
            {
                cbbClassBreaksFieldName.Items.Add("无");
                cbbClassBreaksFieldName.SelectedIndex = 0;
                labMaximum.Text = "";
                labMinimum.Text = "";
                classBreak.Enabled = false;
                groupBoxClassBreaksSymbolSize.Enabled = false;
                symbolStyleGroupBox.Enabled = false;
                symbolColorGroupBox.Enabled = false;
                classBreak.Enabled = false;
                groupBox5.Enabled = false;

                return;
            }
            cbbClassBreaksFieldName.SelectedIndex = 0;


            //LoadbufferRenderer
            if (moRendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
                bufferClassBreaksRenderer = (MyMapObjects.moClassBreaksRenderer)mapLayer.Renderer.Clone();
            else
            {
                //默认初始化
                bufferClassBreaksRenderer = new MyMapObjects.moClassBreaksRenderer();
                bufferClassBreaksRenderer.Field = cbbClassBreaksFieldName.Text;
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
                labMaximum.Text = sMaxValue.ToString();
                labMinimum.Text = sMinValue.ToString();

                //默认分成两级
                for (Int32 i = 0; i <= 1; i++)
                {
                    double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / 2;
                    MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                    MyMapObjects.moSimpleLineSymbol sOutLine = new MyMapObjects.moSimpleLineSymbol();
                    sOutLine.Color = Color.LightGray;
                    sOutLine.Size = 0.35;
                    sOutLine.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
                    sSymbol.Outline = sOutLine;

                    bufferClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                    cbbClassBreaksBreaks.Items.Add(sValue.ToString());

                }
                Color sStartColor = Color.FromArgb(255, 255, 192, 192);
                Color sEndColor = Color.Maroon;
                bufferClassBreaksRenderer.RampColor(sStartColor, sEndColor);
                bufferClassBreaksRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();

            }
            cbbClassBreaksFieldName.SelectedIndex = 0;
            cbbClassBreaksBreaks.SelectedIndex = 0;

            //给控件默认属性
            pictureBoxClassBreaksFillColor.BackColor = Color.LightBlue;
            pictureBoxClassBreaksOutlineColor.BackColor = Color.LightGray;
            numClassBreaksOutlineWidth.Value = 0.35m;

            numClassBreaksBreaksNumber.Value = Convert.ToDecimal((string)(cbbClassBreaksBreaks.Items[cbbClassBreaksBreaks.Items.Count - 1]));
            cbbClassBreaksOutlineStyle.SelectedIndex = 0;

            LoadClassBreaksBufferSymbol();
            //LoadClassBreaksDefaultSymbolArgs();
            //已添加的分级



        }
        
        private void LoadClassBreaksBufferSymbol()
        {
            cbbClassBreaksBreaks.Items.Clear();
            for (int i = 0; i < bufferClassBreaksRenderer.BreakCount; i++)
            {
                double sBreakNumber = bufferClassBreaksRenderer.GetBreakValue(i);
                cbbClassBreaksBreaks.Items.Add(sBreakNumber.ToString());
                cbbClassBreaksFieldName.Enabled = false;
            }
            if (cbbClassBreaksBreaks.Items.Count > 0)
                cbbClassBreaksBreaks.SelectedIndex = 0;
        }

        private void RefreshClassBreaksPage()
        { }

        private void AddClassBreaksSymbol()
        {
            double sBreakNumber = Convert.ToDouble(numClassBreaksBreaksNumber.Value);

            //判断能否加入
            double sMaxValue, sMinValue;
            sMaxValue = Convert.ToDouble(labMaximum.Text);
            sMinValue = Convert.ToDouble(labMinimum.Text);

            if (sBreakNumber < sMinValue || sBreakNumber > sMaxValue)
            {
                //不在正确的输入范围之内
                MessageBox.Show("请输入最大值和最小值之内的数！");
                return;
            }

            if (bufferClassBreaksRenderer.BreakCount > 0) //如果存在之前的break
            {
                double sMaxBreak = Convert.ToDouble((string)cbbClassBreaksBreaks.Items[cbbClassBreaksBreaks.Items.Count - 1]);
                if (sBreakNumber <= sMaxBreak)
                {
                    //输入的break小于最大的break
                    MessageBox.Show("每个分割值都必须比原来所有的分割值大！");
                    return;
                }
                else
                {
                    //接下来才可以加入
                    MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                    sSymbol.Color = pictureBoxClassBreaksFillColor.BackColor;
                    MyMapObjects.moSimpleLineSymbol sOutLine = new MyMapObjects.moSimpleLineSymbol();
                    sOutLine.Color = pictureBoxClassBreaksOutlineColor.BackColor;
                    sOutLine.Size = Convert.ToDouble(numClassBreaksOutlineWidth.Value);
                    sOutLine.Style = IndexToSimpleLineSymbolStyle(cbbSimpleRendererOutlineStyle.SelectedIndex);
                    sSymbol.Outline = sOutLine;
                    
                    bufferClassBreaksRenderer.AddBreakValue(sBreakNumber, sSymbol);
                }
            }
            else
            {
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                sSymbol.Color = pictureBoxClassBreaksFillColor.BackColor;
                MyMapObjects.moSimpleLineSymbol sOutLine = new MyMapObjects.moSimpleLineSymbol();
                sOutLine.Color = pictureBoxClassBreaksOutlineColor.BackColor;
                sOutLine.Size = Convert.ToDouble(numClassBreaksOutlineWidth.Value);
                sOutLine.Style = IndexToSimpleLineSymbolStyle(cbbSimpleRendererOutlineStyle.SelectedIndex);
                sSymbol.Outline = sOutLine;
                bufferClassBreaksRenderer.AddBreakValue(sBreakNumber, sSymbol);
            }



            //加入一个break，锁定字段
            if (bufferClassBreaksRenderer.BreakCount == 1)
            {
                cbbClassBreaksFieldName.Enabled = false;
            }


            //添加分级ComboBox
        }

        private void DeleteClassBreaksSymbol(int index)
        {
            //如果删完了就解除字段名锁定！
            MyMapObjects.moClassBreaksRenderer sSubstitudeRenderer = new MyMapObjects.moClassBreaksRenderer();
            sSubstitudeRenderer.Field = bufferClassBreaksRenderer.Field;
            sSubstitudeRenderer.DefaultSymbol = new MyMapObjects.moSimpleMarkerSymbol();

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
                cbbClassBreaksFieldName.Enabled = true;
        }

        private void AutoCreateClassBreaks(int slice)
        {
            bufferClassBreaksRenderer = new MyMapObjects.moClassBreaksRenderer();
            bufferClassBreaksRenderer.Field = cbbClassBreaksFieldName.Text;
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
            labMaximum.Text = sMaxValue.ToString();
            labMinimum.Text = sMinValue.ToString();

            //默认分成两级
            for (Int32 i = 0; i < slice; i++)
            {
                double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / slice;
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                MyMapObjects.moSimpleLineSymbol sOutline = new MyMapObjects.moSimpleLineSymbol();
                sOutline.Color = pictureBoxClassBreaksOutlineColor.BackColor;
                sOutline.Size = Convert.ToDouble(numClassBreaksOutlineWidth.Value);
                sOutline.Style = IndexToSimpleLineSymbolStyle(cbbSimpleRendererOutlineStyle.SelectedIndex);
                sSymbol.Outline = sOutline;

                bufferClassBreaksRenderer.AddBreakValue(sValue, sSymbol);
                cbbClassBreaksBreaks.Items.Add(sValue.ToString());

            }
            Color sStartColor = Color.FromArgb(255, 255, 192, 192);
            Color sEndColor = Color.Maroon;
            bufferClassBreaksRenderer.RampColor(sStartColor, sEndColor);
            bufferClassBreaksRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();

        }

        private MyMapObjects.moSimpleLineSymbolStyleConstant IndexToSimpleLineSymbolStyle(int index)
        {
            MyMapObjects.moSimpleLineSymbolStyleConstant constant = MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
            switch(index)
            {
                case 0:
                    constant = MyMapObjects.moSimpleLineSymbolStyleConstant.Solid;
                    break;
                case 1:
                    constant = MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
                    break;
                case 2:
                    constant = MyMapObjects.moSimpleLineSymbolStyleConstant.Dot;
                    break;
                case 3:
                    constant = MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot;
                    break;
                case 4:
                    constant = MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot;
                    break;
                default:
                    throw new Exception("函数参数输入错误！");

            }
            return constant;
        }
        
        private int SimpleLineSymbolStyleToIndex(MyMapObjects.moSimpleLineSymbolStyleConstant constant)
        {
            int index = -1;
            switch(constant)
            {
                case MyMapObjects.moSimpleLineSymbolStyleConstant.Dash:
                    index = 1;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.DashDot:
                    index = 3;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.DashDotDot:
                    index = 4;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.Dot:
                    index = 2;
                    break;
                case MyMapObjects.moSimpleLineSymbolStyleConstant.Solid:
                    index = 0;
                    break;
            }
            return index;

        }

        #endregion

        #region 窗体、按钮函数


        #endregion

        #region 保存函数
        public void SaveEdit()
        {
            if(tabControl1.SelectedIndex == 0)
            {
                //简单渲染
                UpdateBufferSimpleRenderer();
                MyMapObjects.moSimpleRenderer sSimpleRenderer = new MyMapObjects.moSimpleRenderer();
                sSimpleRenderer.Symbol = bufferSimpleFillSymbol;
                mapLayer.Renderer = sSimpleRenderer;
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                //唯一值渲染
                mapLayer.Renderer = bufferUniqueValueRenderer;
            }
            else
            {
                //分级渲染
                mapLayer.Renderer = bufferClassBreaksRenderer;
            }
        }
        #endregion

        private void frmFillSymbology_Load(object sender, EventArgs e)
        {
            LoadSimpleRendererPage();
            LoadUniqueValueRendererPage();
            LoadClassBreaksRendererPage();
            isLoad = true;
        }

        private void btnSimpleRendererSelectFillColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxSimpleRendererFillColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnSimpleRendererSelectOutLineColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBoxSimpleRendererOutlineColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnUniqueValueRendererSelectOutlineColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBoxUniqueValueRendererOutlineColor.BackColor = colorDialog1.Color;
            }
        }

        private void pictureBoxSimpleRendererFillColor_BackColorChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferSimpleRenderer();
        }

        private void pictureBoxSimpleRendererOutlineColor_BackColorChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferSimpleRenderer();
        }

        private void numSimpleRendererOutlineWidth_ValueChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferSimpleRenderer();
        }

        private void cbbSimpleRendererOutlineStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferSimpleRenderer();
        }

        private void cbbUniqueValueRendererFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferUniqueValueRenderer();
        }

        private void pictureBoxUniqueValueRendererOutlineColor_BackColorChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferUniqueValueRenderer();

        }

        private void numUniqueValueRendererOutlineWidth_ValueChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferUniqueValueRenderer();
        }

        private void cbbUniqueValueRendererOutlineStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == true)
                UpdateBufferUniqueValueRenderer();
        }

        private void btnClassBreakSelectOutlineColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBoxClassBreaksOutlineColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnClassBreaksSelectFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxClassBreaksFillColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnClassBreaksAutoCreateBreaks_Click(object sender, EventArgs e)
        {
            int slice = Convert.ToInt32(numClassBreaksAutoCreateBreaksNumber.Value);
            AutoCreateClassBreaks(slice);
            LoadClassBreaksBufferSymbol();
        }

        private void btnClassBreaksAddBreaks_Click(object sender, EventArgs e)
        {
            AddClassBreaksSymbol();
            LoadClassBreaksBufferSymbol();
        }

        private void btnClassBreaksDeleteBreaks_Click(object sender, EventArgs e)
        {
            int index = cbbClassBreaksBreaks.SelectedIndex;
            DeleteClassBreaksSymbol(index);
            LoadClassBreaksBufferSymbol();
        }

        private void cbbClassBreaksFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoad == false)
                return;
            //修改标签的值


            string sFieldName = cbbClassBreaksFieldName.Text;
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
            labMaximum.Text = sMaxValue.ToString();
            labMinimum.Text = sMinValue.ToString();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
