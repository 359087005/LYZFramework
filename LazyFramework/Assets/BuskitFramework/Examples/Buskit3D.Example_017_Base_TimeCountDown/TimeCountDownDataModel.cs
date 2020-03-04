/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-09 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：倒计时(数据模型)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example_017
{
    /// <summary>
    /// 对象模型类
    /// </summary>
    public class TimeCountDownDataModel : DataModelBehaviour
    {
        /// <summary>
        /// 时间显示框
        /// </summary>
        public Text showTimeText;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            TimeCountDownDataEntity entity = new TimeCountDownDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }

        /// <summary>
        /// 重写还原函数
        /// </summary>
        public override void LoadStorageData()
        {
            base.LoadStorageData();
            TimeCountDownDataEntity entity = (TimeCountDownDataEntity)GetComponent<TimeCountDownDataModel>().DataEntity;
            showTimeText.text = entity.Second.ToString("#0.00");
        }
    }
}

