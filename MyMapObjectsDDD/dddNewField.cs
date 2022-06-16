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
    public partial class dddNewField : Form
    {

        private MyMapObjects.moMapLayer mapLayer;
        public dddNewField(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            mapLayer = layer;
            LoadValueTypeList();
        }



        #region 私有函数
        private void LoadValueTypeList()
        {
            valueTypeList.Items.Clear();
            valueTypeList.Items.Add("Double");
            valueTypeList.Items.Add("Int16");
            valueTypeList.Items.Add("Int32");
            valueTypeList.Items.Add("Int64");
            valueTypeList.Items.Add("Single");
            valueTypeList.Items.Add("Text");

            /* valueTypeList.Index -- valueType
             * 0 -- Double
             * 1 -- Int16
             * 2 -- Int32
             * 3 -- Int64
             * 4 -- Single
             * 5 -- Text
             * 选择数据类型
             */
            valueTypeList.SelectedIndex = 0;
            //设置默认值

        }
        #endregion

        #region 窗体与按钮事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sFieldName = this.fieldName.Text;
            if(sFieldName == "")
            {
                MessageBox.Show("字段名不能为空！");
                return;
            }    
            // 判断字段名是否为空
            for(int i =0;i<mapLayer.AttributeFields.Count;i++)
            {
                if(sFieldName == mapLayer.AttributeFields.GetItem(i).Name)
                {
                    //如果存在重名的字段
                    MessageBox.Show("已有名字相同的字段，请重新命名。");
                    return;
                }
            }
            //判断是否有重名的字段
            

            int sValueTypeIndex = valueTypeList.SelectedIndex;
            MyMapObjects.moValueTypeConstant sValueType = MyMapObjects.moValueTypeConstant.dDouble;
            //默认值dDouble
            switch(sValueTypeIndex)
            {
                case 0:
                    sValueType = MyMapObjects.moValueTypeConstant.dDouble;
                    break;
                case 1:
                    sValueType = MyMapObjects.moValueTypeConstant.dInt16;
                    break;
                case 2:
                    sValueType = MyMapObjects.moValueTypeConstant.dInt32;
                    break;
                case 3:
                    sValueType = MyMapObjects.moValueTypeConstant.dInt64;
                    break;
                case 4:
                    sValueType = MyMapObjects.moValueTypeConstant.dSingle;
                    break;
                case 5:
                    sValueType = MyMapObjects.moValueTypeConstant.dText;
                    break;
                default:
                    new NullReferenceException();
                    break;
            }

            MyMapObjects.moField sField = new MyMapObjects.moField(sFieldName,sValueType);
            mapLayer.AttributeFields.Append(sField);
            // 添加新字段

            MessageBox.Show("添加新字段成功！");
            this.Close();
            this.Dispose();
        }


        #endregion

        
    }
}
