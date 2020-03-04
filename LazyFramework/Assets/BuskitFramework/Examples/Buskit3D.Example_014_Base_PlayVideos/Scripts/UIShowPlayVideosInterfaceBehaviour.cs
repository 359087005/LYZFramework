/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIShowPlayVideosInterfaceBehaviour
* 创建日期：2018-1-11 13:36:42
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：PlayView管理器
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example_14_PlayVideos
{
    /// <summary>
    /// PlayView管理器
    /// </summary>
    public class UIShowPlayVideosInterfaceBehaviour : MonoBehaviour
	{
        /// <summary>
        /// 按钮数组
        /// </summary>
        private Button[] _buttons;

        /// <summary>
        /// 音量控制条
        /// </summary>
        private Slider _volunmPercentslider;

        /// <summary>
        /// 进度控制条
        /// </summary>
        private Slider _ProgressPercentslider;

        /// <summary>
        /// 音量显示器
        /// </summary>
        private Text _volumValuetext;

        /// <summary>
        /// 获取事件分发器
        /// </summary>
        private UIShowPlayVideosInterfaceDataModel _uiShowPlayVideosInterfaceDataModel;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            _buttons = GetComponentsInChildren<Button>(true);
            _volunmPercentslider = transform.Find("BackGround/VolunmPercent").GetComponent<Slider>();
            _ProgressPercentslider = transform.Find("BackGround/VideoPercent").GetComponent<Slider>();
            _ProgressPercentslider.maxValue = 1;
            _volumValuetext = transform.Find("BackGround/VolumValue").GetComponent<Text>();
            _uiShowPlayVideosInterfaceDataModel = GetComponent<UIShowPlayVideosInterfaceDataModel>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                int tmp = i;
                _buttons[i].onClick.AddListener(() => OnBtnClick(tmp));
            }
            _volunmPercentslider.onValueChanged.AddListener(OnVolunmPercentSliderValueChanged);
            _ProgressPercentslider.onValueChanged.AddListener(OnProgressSliderValueChanged);
        }

        /// <summary>
        /// 按键的点击事件
        /// </summary>
        /// <param name="num"></param>
        private void OnBtnClick(int num) {
            UIShowPlayVideosInterfaceDataModelEntity entity = (UIShowPlayVideosInterfaceDataModelEntity)_uiShowPlayVideosInterfaceDataModel.DataEntity;
            switch (num)
            {
                case 0:
                    entity.IsClose = !entity.IsClose;
                    break;
                case 1:
                    entity.IsMax = !entity.IsMax;
                    if (entity.IsMax)
                    {
                        _buttons[num].GetComponentInChildren<Text>().text = "最小化";
                    }
                    else
                    {
                        _buttons[num].GetComponentInChildren<Text>().text = "最大化";
                    }
                    break;
                case 2:
                    entity.IsPlaying = !entity.IsPlaying;
                    break;
                case 3:
                    entity.IsFastForward = !entity.IsFastForward;
                    break;
                case 4:
                    entity.IsBackOff = !entity.IsBackOff;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更改声音
        /// </summary>
        /// <param name="arg"></param>
        private void OnVolunmPercentSliderValueChanged(float arg) {
            UIShowPlayVideosInterfaceDataModelEntity entity = (UIShowPlayVideosInterfaceDataModelEntity)_uiShowPlayVideosInterfaceDataModel.DataEntity;
            entity.VolunmValue = arg;
            _volumValuetext.text = "音量:" + (arg * 100).ToString() + "%";
        }

        /// <summary>
        /// 更改进度条
        /// </summary>
        /// <param name="arg"></param>
        public void OnProgressSliderValueChanged(float arg)
        {
            UIShowPlayVideosInterfaceDataModelEntity entity = (UIShowPlayVideosInterfaceDataModelEntity)_uiShowPlayVideosInterfaceDataModel.DataEntity;
            entity.ProgressValue = arg;
        }
    }
}

