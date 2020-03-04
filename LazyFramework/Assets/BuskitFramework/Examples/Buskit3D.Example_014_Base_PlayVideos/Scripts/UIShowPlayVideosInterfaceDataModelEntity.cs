/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：UIShowPlayVideosInterfaceDataModelEntity
* 创建日期：2018-1-11 13:36:42
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：数据实体
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

/// <summary>
/// 名称空间定义：Buskit3D.Common.UI
/// </summary>
namespace Com.Rainier.Buskit3D.Example_14_PlayVideos
{
	/// <summary>
    /// 数据实体
    /// </summary>
	public class UIShowPlayVideosInterfaceDataModelEntity : BaseDataModelEntity
	{
        /// <summary>
        /// 是否播放
        /// </summary>
        public bool IsPlaying = false;
        /// <summary>
        /// 是否最大化
        /// </summary>
        public bool IsMax = false;
        /// <summary>
        /// 快进
        /// </summary>
        public bool IsFastForward = false;
        /// <summary>
        /// 快退
        /// </summary>
        public bool IsBackOff = false;
        /// <summary>
        /// 音量
        /// </summary>
        public float VolunmValue = 1.0f;
        /// <summary>
        /// 进度
        /// </summary>
        public float ProgressValue = 1.0f;
        /// <summary>
        /// UI开关
        /// </summary>
        public bool IsClose = false;
	}
    /// <summary>
    /// Video类型
    /// </summary>
    public class VideoType
    {
        public long ID { get; }

        public string VideoClip { get; }

        public string Name { get; }

        public VideoType(long ID, string VideoClip, string Name) {
            this.ID = ID;
            this.VideoClip = VideoClip;
            this.Name = Name;
        }
    }
}

