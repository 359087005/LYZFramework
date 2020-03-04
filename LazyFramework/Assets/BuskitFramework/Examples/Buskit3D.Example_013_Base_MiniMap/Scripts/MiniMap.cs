
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MiniMap
* 创建日期：2018-12-19 09:24:34
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：生成小地图
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
namespace Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 生成小地图
    /// </summary>
	public class MiniMap : MonoBehaviour 
	{

        // 显示纹理
        public RenderTexture renderTexture;

        //目标物体
        public Transform target;

        //默认相机高度 
        public float defaultHeight=20;

        //默认最大高度
        public float maxHeight=80;

        //默认最小高度
        public float minHeight = 10;

        //玩家图标
        public Image playerIcon;

        //小地图相机
        public  UnityEngine.Camera miniCamera;

        //小地图UI根节点
        public  RectTransform mapUIRoot;

        /// <summary>
        /// 跟新player的UI位置
        /// </summary>
        public void UpdatePosition()
        {
            Vector3 pp = miniCamera.WorldToViewportPoint(target.position);
            playerIcon.rectTransform.anchoredPosition = MiniMapUtils.CalculateMiniMapPosition(pp, mapUIRoot);
            miniCamera.transform.parent.localEulerAngles = new Vector3(90, 0, -target.localEulerAngles.y);
            miniCamera.transform.position = target.position + Vector3.up * defaultHeight;
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Update()
        {
            if (miniCamera == null)
                return;
            if (target == null)
                return;
            UpdatePosition();
        }

    }
}

