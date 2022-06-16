using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MyMapObjectsDemo
{
    /// <summary>
    /// 按属性查询窗体
    /// </summary>
    public partial class dddAnyLayerSelectedByAttributes : Form
    {
        private dddMain mMainForm;
        private MyMapObjects.moLayers mLayers;
        private MyMapObjects.moMapLayer mLayer;
        private bool ValueTypeIsText = true; //字段值的类型是否为文本类型

        // 构造函数
        public dddAnyLayerSelectedByAttributes(dddMain mainForm, MyMapObjects.moLayers layers)
        {
            InitializeComponent();
            mMainForm = mainForm;
            mLayers = layers;
            mLayer = null;

            mMainForm.FieldsChanged += MainForm_FieldsChanged;
            mMainForm.LayersChanged += MainForm_LayersChanged;
        }

        #region 按钮和窗体事件

        // 窗体加载事件
        private void frmAnyLayerSelectedByAttributes_Load(object sender, EventArgs e)
        {
            LoadLayerList();            
        }

        // 窗体关闭事件
        private void frmAnyLayerSelectedByAttributes_FormClosed(object sender, FormClosedEventArgs e)
        {
            mMainForm.FieldsChanged -= MainForm_FieldsChanged;
            mMainForm.LayersChanged -= MainForm_LayersChanged;
        }

        // 选择图层更改事件
        private void layerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mLayer = mLayers.GetItem(layerList.SelectedIndex);
            LoadAttributeList();
        }

        // 选择的属性发生更改
        private void attributesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mLayer.AttributeFields.GetItem(attributesList.SelectedIndex).ValueType == MyMapObjects.moValueTypeConstant.dText)
                ValueTypeIsText = true;
            else
                ValueTypeIsText = false;

            attributeTextbox.Text = attributesList.Text;
            LoadOperatorList();
        }

        // 查询按钮点击事件
        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }

        #endregion


        #region 私有函数

        private void MainForm_LayersChanged()
        {
            LoadLayerList();
        }

        private void MainForm_FieldsChanged(Int32 layerIndex)
        {
            if (layerList.SelectedIndex == layerIndex)
                LoadAttributeList();
        }

        private void LoadAttributeList()
        {
            attributesList.Items.Clear();
            if (mLayer == null)
            {
                return;
            }

            Int32 attriCount = mLayer.AttributeFields.Count;
            for(Int32 i = 0; i <= attriCount - 1; i++)
            {
                attributesList.Items.Add(mLayer.AttributeFields.GetItem(i).Name);
            }
            if (attributesList.Items.Count != 0)
                attributesList.SelectedIndex = 0;
        }

        private void LoadLayerList()
        {
            layerList.Items.Clear();
            Int32 layerCount = mLayers.Count;
            for (Int32 i = 0; i <= layerCount - 1; i++)
            {
                layerList.Items.Add(mLayers.GetItem(i).Name);
            }
            if (layerList.Items.Count > 0)
            {
                layerList.SelectedIndex = 0;
            }
        }

        private void LoadOperatorList()
        {
            operatorList.Items.Clear();

            if (ValueTypeIsText == true)
            {
                this.operatorList.Items.Add("=");
                operatorList.SelectedIndex = 0;
            }
            else
            {
                this.operatorList.Items.Add("=");
                this.operatorList.Items.Add(">");
                this.operatorList.Items.Add("<");
                this.operatorList.Items.Add(">=");
                this.operatorList.Items.Add("<=");
                operatorList.SelectedIndex = 0;
            }
            operatorList.Enabled = true;
        }

        private void ExecuteQuery()
        {
            int sSeletedIndex = this.attributesList.SelectedIndex;
            if (ValueTypeIsText == true)
            {
                try
                {
                    string sQueryString = this.queryTextBox.Text;
                    ExecuteTextQuery(mLayer, sSeletedIndex, sQueryString);
                }
                catch(Exception error)
                {
                    //MessageBox.Show("请输入正确的查询语句！");
                }
                //文本类查询

            }
            else
            {
                try
                {
                    string sQueryString = this.queryTextBox.Text;
                    object sQueryNumber = (object)sQueryString;
                    string sOperator = this.operatorList.Text;
                    int sQueryMode = 0;
                    switch (sOperator)
                    {
                        case "=":
                            sQueryMode = 1;
                            break;
                        case ">":
                            sQueryMode = 2;
                            break;
                        case "<":
                            sQueryMode = 3;
                            break;
                        case ">=":
                            sQueryMode = 4;
                            break;
                        case "<=":
                            sQueryMode = 5;
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                    ExecuteNumberQuery(mLayer, sSeletedIndex, sQueryMode, sQueryNumber);
                }
                catch
                {
                    //MessageBox.Show("请输入正确的查询语句！");
                }
            }
        }

        // 执行文本类型查询
        // 提供公共接口给查询窗口使用
        private void ExecuteTextQuery(MyMapObjects.moMapLayer layer, int attributeIndex, string queryString)
        {

            int sFeatureCount = mLayer.Features.Count;
            mLayer.SelectedFeatures.Clear();

            //查询相应的属性
            for (int i = 0; i < sFeatureCount; i++)
            {
                MyMapObjects.moFeature sFeature = mLayer.Features.GetItem(i);
                string sContent = (string)sFeature.Attributes.GetItem(attributeIndex);
                if (sContent == queryString)
                {
                    mLayer.SelectedFeatures.Add(sFeature);
                }
            }
            UpdateMapSelection();
        }

        // 执行数字类查询
        // 提供公共接口给查询窗口使用
        /// queryMode:1=,2>,3<,4>=,5<=
        private void ExecuteNumberQuery(MyMapObjects.moMapLayer layer, int attributeIndex, int queryMode, object queryNumber)
        {
            int sFeatureCount = mLayer.Features.Count;
            mLayer.SelectedFeatures.Clear();

            // 确定字段类型
            MyMapObjects.moValueTypeConstant sValueType = mLayer.AttributeFields.GetItem(attributeIndex).ValueType;

            // 循环判断查询结果
            for (int i = 0; i < sFeatureCount; i++)
            {
                MyMapObjects.moFeature sFeature = mLayer.Features.GetItem(i);

                if (sValueType == MyMapObjects.moValueTypeConstant.dDouble)
                {
                    double sValue = (double)sFeature.Attributes.GetItem(attributeIndex);
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == MyMapObjects.moValueTypeConstant.dInt16)
                {
                    Int16 sValue = (Int16)sFeature.Attributes.GetItem(attributeIndex);
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    // 对于整数类型的比较，还是把输入的数字转换成double比较更加实用，以下整形都相同
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == MyMapObjects.moValueTypeConstant.dInt32)
                {
                    Int32 sValue = (Int32)sFeature.Attributes.GetItem(attributeIndex);
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == MyMapObjects.moValueTypeConstant.dInt64)
                {
                    Int64 sValue = (Int64)sFeature.Attributes.GetItem(attributeIndex);
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == MyMapObjects.moValueTypeConstant.dSingle)
                {
                    Single sValue = (Single)sFeature.Attributes.GetItem(attributeIndex);
                    Single sQueryNumber;
                    sQueryNumber = Convert.ToSingle((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                mLayer.SelectedFeatures.Add(sFeature);
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else
                {
                    new NullReferenceException();
                }
            }

            UpdateMapSelection();
        }

        #endregion

        #region 事件

        public delegate void UpdateMapSelectionHandle();
        public event UpdateMapSelectionHandle UpdateMapSelection;

        #endregion
    }
}

