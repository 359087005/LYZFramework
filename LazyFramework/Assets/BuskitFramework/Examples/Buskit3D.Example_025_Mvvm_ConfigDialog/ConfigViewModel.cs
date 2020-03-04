/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ConfigViewModel
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：MVVM例程，以一个简单的配置界面为例，说明MVVM使用方法
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;
using UnityEngine.UI;

namespace Buskit3D.Example_024_Mvvm
{
    /// <summary>
    /// 配置界面的ViewModel
    /// </summary>
    public class ConfigViewModel: ViewModelBehaviour
    {
        /// <summary>
        /// 设置Slider1与Entity中configValue1的映射关系
        /// </summary>
        [Binding(EntityPropertyName= "BackgroundVolume")]
        public SliderView sliderBackground;

        /// <summary>
        /// 设置Slider2与Entity中configValue2的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "ForegroundVolume")]
        public SliderView sliderForeground;

        /// <summary>
        /// 前景音乐显示字符串
        /// </summary>
        [Binding(EntityPropertyName = "BackgroundText")]
        public TextView backgroundValueText;

        /// <summary>
        /// 前景音乐显示字符串
        /// </summary>
        [Binding(EntityPropertyName = "ForegroundText")]
        public TextView foregroundValueText;


        /// <summary>
        /// 设置OK按钮与Entity中clickOk的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "OkClicked")]
        public ButtonView okButton;

        /// <summary>
        /// 设置背景音乐与Entity中EnableBgMusic的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "EnableBgMusic")]
        public ToggleView enableBgMusicToggle;

        /// <summary>
        /// 设置前景音乐与Entity中EnableBgMusic的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "EnableFgMusic")]
        public ToggleView enableFgMusicToggle;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {

            //实例化DataEntity
            this.DataEntity = new ConfigEntity();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }

        /// <summary>
        /// 简单测试：通过设置数据实体的值来更新界面显示
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.P))
            {
                ConfigEntity _entity = (ConfigEntity)this.DataEntity;
                _entity.BackgroundVolume = 0.2f;
                _entity.ForegroundVolume = 0.4f;
                _entity.BackgroundVolume = 0.6f;
                _entity.ForegroundVolume = 0.8f;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                ConfigEntity _entity = (ConfigEntity)this.DataEntity;
                Debug.Log(_entity.BackgroundVolume);
                Debug.Log(_entity.ForegroundVolume);
            }
        }
    }
}
