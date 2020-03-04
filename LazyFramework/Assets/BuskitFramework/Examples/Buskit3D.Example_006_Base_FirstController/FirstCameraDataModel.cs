/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： FirstCameraDataModel
* 创建日期：2019-01-14 11:58:55
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：第一人称相机实体模型
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D_Example_006_FirstController
{
    /// <summary>
    /// 第一人称相机实体模型
    /// </summary>
    [RequireComponent(typeof(FirstCameraLogic))]
	public class FirstCameraDataModel : DataModelBehaviour
	{
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            FirstCameraEntity data = new FirstCameraEntity();
            DataEntity = data;
            Watch(this);
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        protected override void  Start()
        {
            base.Start();
            GameObject player = transform.parent.gameObject;
            BindingTool bindingTool = new BindingTool();
            bindingTool.Binding(gameObject, player);
        }
    }
}

