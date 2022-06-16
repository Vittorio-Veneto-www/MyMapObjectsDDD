using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MyMapObjectsDDD
{
    /// <summary>
    /// 按属性查询窗体
    /// </summary>
    public partial class dddSelectedByAttributes : Form
    {
        private dddAttributes attributesFrm;
        //调用该窗体的属性表
        //private bool FieldConfirmed = false; //是否确认所查询的字段
        private bool ValueTypeIsText = true; //字段值的类型是否为文本类型
        private string AttributeName;

        public dddSelectedByAttributes(dddAttributes frm)
        {
            InitializeComponent();
            attributesFrm = frm;
            attributesFrm.UpdateFields += AttributesForm_UpdateFields;
            LoadAttributesList();
        }

        #region 按钮和窗体事件
        private void frmSelectedByAttributes_Load(object sender, EventArgs e)
        {
            //LoadAttributesList();
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
                ExecuteQuery();
        }

        private void frmSelectedByAttributes_FormClosed(object sender, FormClosedEventArgs e)
        {
            attributesFrm.UpdateFields -= AttributesForm_UpdateFields;
        }

        private void attributesList_SelectedValueChanged(object sender, EventArgs e)
        {
            // 清除当前的查询内容
            AttributeName = "";
            operatorList.Items.Clear();
            attributeTextbox.Text = "";

            AttributeName = attributesList.Text;
            // 获取选择字段的名字
            int AttributeIndex = attributesList.SelectedIndex;
            //获取选择字段的序号

            MyMapObjects.moMapLayer mapLayer = this.attributesFrm.MapLayer;
            MyMapObjects.moValueTypeConstant sValueType = mapLayer.AttributeFields.GetItem(AttributeIndex).ValueType;
            if (sValueType == MyMapObjects.moValueTypeConstant.dText)
                ValueTypeIsText = true;
            else
                ValueTypeIsText = false;
            //通过选择字段的序号判断其是否为字符串

            attributeTextbox.Text = AttributeName;
            //把选择字段的名字显示到TextBox中

            LoadOperatorList();
            //加载运算符列表
        }

        #endregion


        #region 私有函数

        private void AttributesForm_UpdateFields(Int32 layerIndex)
        {
            LoadAttributesList();
        }

        private void LoadAttributesList()
        {
            attributesList.Items.Clear();
            MyMapObjects.moMapLayer mapLayer = this.attributesFrm.MapLayer;
            int sFieldCount = mapLayer.AttributeFields.Count;
            for(int i =0;i<sFieldCount;i++)
            {
                string sFieldName = mapLayer.AttributeFields.GetItem(i).Name;
                this.attributesList.Items.Add(sFieldName);
            }
            this.attributesList.SelectedIndex = 0; //默认选择为第一个

        }

        private void LoadOperatorList()
        {
            if(ValueTypeIsText == true)
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
            if(ValueTypeIsText == true)
            {
                try
                {
                    string sQueryString = this.queryTextBox.Text;
                    this.attributesFrm.ExecuteTextQuery(sSeletedIndex, sQueryString);
                }
                catch
                {
                    MessageBox.Show("请输入正确的查询语句！");
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
                    switch(sOperator)
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
                    this.attributesFrm.ExecuteNumberQuery(sSeletedIndex, sQueryMode, sQueryNumber);
                }
                catch
                {
                    MessageBox.Show("请输入正确的查询语句！");
                }
            }
        }



        #endregion


    }
}
