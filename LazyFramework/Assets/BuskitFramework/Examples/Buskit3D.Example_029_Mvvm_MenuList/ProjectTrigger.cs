
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ProjectTrigger
* 创建日期：2019-03-18 17:40:02
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Com.Rainier.Buskit3D.Example_012;

namespace Buskit3D.Example_033_MenuList
{
    /// <summary>
    /// 
    /// </summary>
	public class ProjectTrigger : MonoBehaviour 
	{
        public static ProjectTrigger Instance;
        public Transform contentParent;
        public ToggleGroup toggleGroup;
        public Dictionary<int, string> menuItemDic = new Dictionary<int, string>();

        /// <summary>
        /// Unity Method
        /// </summary>
        void Awake () 
		{
            Instance = this;
        }

        /// <summary>
        /// 切换场景按钮点击事件
        /// </summary>
        public void OnClickLoadSence_A()
        {
            SenceLoadingDataEntity entity = (SenceLoadingDataEntity)GameObject.FindObjectOfType<SenceLoadingDataModel>().DataEntity;
            entity.TargetSenceName = "Degsin";
            entity.IsShowLoadingUI = true;
        }
    }
}

