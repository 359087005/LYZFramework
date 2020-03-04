/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_019_Setting
{
    /// <summary>
    /// 设置逻辑处理类
    /// </summary>
    public class RainierSettingLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("intensity"))
            {
                float intensity = (float)evt.NewValue;
                GetComponent<RainierSettingModel>().DirectionalLight.intensity = intensity;
            }
        }
    }
}


