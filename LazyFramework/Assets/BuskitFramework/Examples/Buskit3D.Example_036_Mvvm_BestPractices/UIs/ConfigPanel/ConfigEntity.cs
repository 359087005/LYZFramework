/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ConfigEntity
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：MVVM例程，以一个简单的配置界面为例，说明MVVM使用方法
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_036_Mvvm_BestPractices
{
    /// <summary>
    /// 配置数据实体
    /// </summary>
    public class ConfigEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 背景音乐音量
        /// </summary>
        [RestoreFireLogic]
        public float BackgroundVolume = 0.2f;

        /// <summary>
        /// 前景音乐音量
        /// </summary>
        [RestoreFireLogic]
        public float ForegroundVolume = 0.3f;

        /// <summary>
        /// 前景音乐音量显示
        /// </summary>
        [RestoreFireLogic]
        public string ForegroundText = "";

        /// <summary>
        /// 背景音乐音量显示
        /// </summary>
        [RestoreFireLogic]
        public string BackgroundText = "";

        /// <summary>
        /// 是否点击了OK按钮
        /// </summary>
        [RestoreFireLogic]
        public int OkClicked = -1;

        /// <summary>
        /// 开启背景音乐
        /// </summary>
        [RestoreFireLogic]
        public bool EnableBgMusic = false;

        /// <summary>
        /// 开启前景音乐
        /// </summary>
        [RestoreFireLogic]
        public bool EnableFgMusic = false;

        /// <summary>
        /// 打开关闭状态
        /// </summary>
        [RestoreFireLogic]
        public bool OpenClose = true;
    }
}
