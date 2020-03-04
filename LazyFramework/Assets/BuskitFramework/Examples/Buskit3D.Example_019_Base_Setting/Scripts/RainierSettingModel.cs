/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：RainierSettingModel
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
    /// 事件分发器类
    /// </summary>
    public class RainierSettingModel : DataModelBehaviour
    {
        /// <summary>
        /// 灯光
        /// </summary>
        public Light DirectionalLight;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new RainierSettingEntity();
            Watch(this);
        }

        /// <summary>
        ///还原 
        /// </summary>
        public override void LoadStorageData()
        {
            base.LoadStorageData();
            RainierSettingEntity data = (RainierSettingEntity)DataEntity;
            DirectionalLight.intensity = data.intensity;
        }
    }

    /// <summary>
    /// 设置实体
    /// </summary>
    public class RainierSettingEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 是否播放背景音乐
        /// </summary>
        public long isBackgroundMusic = 0;
        /// <summary>
        /// 是否进行语音播报
        /// </summary>
        public long isVoiceBroadcast = 0;
        /// <summary>
        /// 设置光照强度
        /// </summary>
        public float intensity;
    }
}