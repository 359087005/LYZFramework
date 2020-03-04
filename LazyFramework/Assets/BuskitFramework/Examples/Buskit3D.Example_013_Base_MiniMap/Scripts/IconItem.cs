
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IconItem
* 创建日期：2018-12-19 14:14:10
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：小地图中的UI可交互元素
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
namespace Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 小地图上的交互UI属性
    /// </summary>
	public class IconItem : MonoBehaviour 
	{
        //图标尺寸
        public Vector2 sizeData=new Vector2(15,15);

        //显示图标
        [HideInInspector]
        public Sprite image;

        //颜色
        [HideInInspector]
        public Color color;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            GetComponent<Image>().sprite = image;
            GetComponent<Image>().color = color;
        }
    }
}

