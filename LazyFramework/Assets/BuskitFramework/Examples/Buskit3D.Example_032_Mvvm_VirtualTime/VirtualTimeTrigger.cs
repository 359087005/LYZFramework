
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： VirtualTimeTrigger
* 创建日期：2019-03-18 15:47:04
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System;

namespace Buskit3D.Example_34_Mvvm_VirtualTime
{
    /// <summary>
    /// 
    /// </summary>
	public class VirtualTimeTrigger : MonoBehaviour 
	{
        /// <summary>
        /// 上面按钮
        /// </summary>
        public Button topButton;
        /// <summary>
        /// DateTime对象
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            topButton.onClick.AddListener(OnTopBtnClick);
        }

        /// <summary>
        /// 上面按钮点击事件
        /// </summary>
        private void OnTopBtnClick()
        {
            VirtualTimeEntity entity = (VirtualTimeEntity)GetComponent<VirtualTimeDataModel>().DataEntity;
            if(!string.IsNullOrEmpty(entity.inputFildHour) && !string.IsNullOrEmpty(entity.inputFildMinute))
            {
                dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(entity.inputFildHour), int.Parse(entity.inputFildMinute), 0);
                entity.dataTime = dateTime;
            }
        }
    }
}

