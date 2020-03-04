/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：RainierSettingTrigger
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_019_Setting
{
    /// <summary>
    /// 触发器类
    /// </summary>
    public class RainierSettingTrigger : MonoBehaviour
    {
        /// <summary>
        /// 滑动条触发器
        /// </summary>
        /// <param name="value"></param>
        public void ChangLightIntensity(float value) {
            ((RainierSettingEntity)GetComponent<RainierSettingModel>().DataEntity).intensity = value;
        }
    }
}