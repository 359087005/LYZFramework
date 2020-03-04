/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.BusKit.Unity.UI
* 类 名 称：ImageEx
* 创建日期：2018-01-24 14:13:58
* 作者名称：王冠南
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// UI系统程序集
/// </summary>
namespace Com.Rainier.BusKit.Unity.UI
{

    /// <summary>按像素点击图片</summary>
    public class ImageEx : Image
    {
        /// <summary>
        /// 过滤类型，透明度/颜色
        /// </summary>
        public FilterType filterType = FilterType.Alpha;
        /// <summary>
        /// 过滤范围
        /// </summary>
        public float filterRange = 0.1f;
        /// <summary>
        /// 过滤颜色数组
        /// </summary>
        public Color[] filterColors;
        private int selectStage = -1;
        /// <summary>
        /// 现在所选状态
        /// </summary>
        public int SelectStage { get { return selectStage; } }
        private Color selectColor;
        /// <summary>
        /// 现在所选颜色
        /// </summary>
        public Color SelectColor { get { return selectColor; } }

        Rect rect;
        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            if (!raycastTarget) return false;
            rect = GetComponent<RectTransform>().rect;
            Sprite sprite = overrideSprite;
            if (sprite == null || sprite.texture == null) return true;

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, canvas.worldCamera, out pos))
            {
                pos += new Vector2(rect.width, rect.height) / 2;
            }

            Color c = sprite.texture.GetPixel((int)(pos.x * sprite.texture.width / rect.width), (int)(pos.y * sprite.texture.height / rect.height));
            selectColor = c;
            if (filterType == FilterType.Alpha)
            {
                bool r = c.a >= filterRange;
                selectStage = r ? 0 : -1;
                return c.a >= filterRange;
            }
            else
            {
                for (int i = 0; i < filterColors.Length; i++)
                {
                    if (ColorOff(c, filterColors[i]) <= filterRange)
                    {
                        selectStage = i;
                        return true;
                    }
                }
                selectStage = -1;
                return false;
            }
        }

        public float ColorOff(Color c1, Color c2)
        {
            return Mathf.Pow(c1.r - c2.r, 2) + Mathf.Pow(c1.g - c2.g, 2) + Mathf.Pow(c1.b - c2.b, 2) / 3;
        }

        public enum FilterType
        {
            Alpha,
            Color
        }

    }
}

