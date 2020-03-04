/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：UIShowPlayVideosInterfaceLogic
* 创建日期：2018-12-19 16:29:25
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：播发器逻辑处理类
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_14_PlayVideos
{
    /// <summary>
    /// 对VideoPlay进行操作
    /// </summary>
    public class UIShowPlayVideosInterfaceLogic : LogicBehaviour
    {
        /// <summary>
        /// 播放器
        /// </summary>
        private VideoPlayer _videoPlayer;
        /// <summary>
        /// 声音控制器
        /// </summary>
        private AudioSource _audioSource;
        /// <summary>
        /// 是否播放
        /// </summary>
        private bool IsPlay = true;
        /// <summary>
        /// 播放百分比
        /// </summary>
        private Slider _videoPercentSlider;
        /// <summary>
        /// 播放时间
        /// </summary>
        private Text _videoTime;
        /// <summary>
        /// 视频的起始背景宽带
        /// </summary>
        private float Width;
        /// <summary>
        /// 视频的起始背景高度
        /// </summary>
        private float Heigtht;
        /// <summary>
        /// 标识为是否首次打开
        /// </summary>
        bool isFirst = true;
        /// <summary>
        /// 总时长
        /// </summary>
        float alltime = 0;
        /// <summary>
        /// 播放总时长
        /// </summary>
        string alltimeSstr;

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void Awake()
        {
            _videoPlayer = GetComponentInChildren<VideoPlayer>();
            _audioSource = GetComponentInChildren<AudioSource>();
            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _videoPlayer.SetTargetAudioSource(0, _audioSource);
            Width = this.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
            Heigtht = this.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
            _videoPercentSlider = transform.Find("BackGround/VideoPercent").GetComponent<Slider>();
            _videoTime = transform.Find("BackGround/VideoTime").GetComponent<Text>();
            //通过URL形式加载视频
            _videoPlayer.url = Application.dataPath + "/StreamingAssets/coffee.ogv";
        }
        
        /// <summary>
        /// 业务逻辑类
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //是否播放
            if (evt.PropertyName.Equals("IsPlaying"))
            {
                IsPlay = (bool)evt.NewValue;
                if (IsPlay)
                {
                    _videoPlayer.Play();
                    _audioSource.Play();
                    transform.Find("BackGround/Play/Text").GetComponent<Text>().text = "暂停";
                }
                else
                {
                    _videoPlayer.Pause();
                    _audioSource.Pause();
                    transform.Find("BackGround/Play/Text").GetComponent<Text>().text = "播放";
                }
            }
            //音量控制
            if (evt.PropertyName.Equals("VolunmValue"))
            {
                _audioSource.volume = (float)evt.NewValue;
                transform.Find("BackGround/VolunmPercent").GetComponent<Slider>().value = (float)evt.NewValue;
            }

            //进度条控制
            if (evt.PropertyName.Equals("ProgressValue"))
            {
                
            }

            //是否最大化
            if (evt.PropertyName.Equals("IsMax"))
            {
                if ((bool)evt.NewValue)
                {
                    this.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 768);
                    this.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1366);
                }
                else
                {
                    this.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Heigtht);
                    this.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
                }
            }

            //是否快进
            if (evt.PropertyName.Equals("IsFastForward"))
            {
                _videoPercentSlider.value += 5f;
                _videoPercentSlider.value = Mathf.Clamp(_videoPercentSlider.value, 0, _videoPercentSlider.maxValue);
                IsPlay = true;
                //OnSliderValueChanged(_videoPercentSlider.Value);
                _videoPlayer.time = _videoPercentSlider.value;
            }

            //是否后退
            if (evt.PropertyName.Equals("IsBackOff"))
            {
                _videoTime.text = "";
                _videoPercentSlider.value -= 5f;
                _videoPercentSlider.value = Mathf.Clamp(_videoPercentSlider.value, 0, _videoPercentSlider.maxValue);
                //OnSliderValueChanged(_videoPercentSlider.Value);
                _videoPlayer.time = _videoPercentSlider.value;
            }
        }

        /// <summary>
        /// 改变播放进度
        /// </summary>
        /// <param name="arg"></param>
        public void OnSliderValueChanged(float arg)
        {
            //_videoPlayer.time = arg;
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        private void Update()
        {
            //计算时间
            if (_videoPlayer.isPlaying)
            {
                GetTimePlay();
                if (Mathf.Abs((int)_videoPlayer.time - (int)_videoPercentSlider.maxValue) <= 0.1f)
                {
                    _videoPlayer.Stop();
                    IsPlay = false;
                }
            }
        }

        /// <summary>
        /// 获取播放时间
        /// </summary>
        private void GetTimePlay()
        {
            _videoPercentSlider.value = (float)_videoPlayer.time;
            if (isFirst)
            {
                isFirst = false;
                alltime = _videoPlayer.frameCount / _videoPlayer.frameRate;
                _videoPercentSlider.maxValue = alltime;
                int allMinute = (int)alltime / 60;
                int allSecond = (int)alltime % 60;
                alltimeSstr = string.Format("{0:D2}:{1:D2}", allMinute.ToString(), allSecond.ToString());
            }
            int Minute = (int)_videoPlayer.time / 60;
            int Second = (int)_videoPlayer.time % 60;
            _videoTime.text = string.Format("{0:D2}:{1:D2}", Minute.ToString(), Second.ToString());
            _videoTime.text += "/" + alltimeSstr;
        }
    }
}