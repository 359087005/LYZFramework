/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MiniMapItem
* 创建日期：2018-12-19 09:25:22
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：小地图上显示的可交互的Item元素
******************************************************************************/

using UnityEngine;

namespace Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 小地图工具类
    /// </summary>
    public static class MiniMapUtils
    {
        /// <summary>
        /// 坐标映射
        /// </summary>
        /// <param name="viewPoint"></param>
        /// <param name="maxAnchor"></param>
        /// <returns></returns>
        public static Vector3 CalculateMiniMapPosition(Vector3 viewPoint, RectTransform maxAnchor)
        {
            float x = (viewPoint.x * maxAnchor.sizeDelta.x) - (maxAnchor.sizeDelta.x * 0.5f);
            float y = (viewPoint.y * maxAnchor.sizeDelta.y) - (maxAnchor.sizeDelta.y * 0.5f);
            viewPoint = new Vector2(x, y);
            return viewPoint;
        }
    }
}
