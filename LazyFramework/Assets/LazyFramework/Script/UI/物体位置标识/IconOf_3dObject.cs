/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IconOf_3dObject
* 创建日期：2020-01-07 14:18:54
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：物体位置标识
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Buskit3D_CaiLiao
{
    /// <summary>
    /// 
    /// </summary>
	public class IconOf_3dObject : MonoBehaviour
    {

        public GameObject target;

        [Header("超出视野时是否显示在屏幕侧边")]
        public bool keepInside;
        private string targetName="";
        public Text textComponent;
        private float width;
        private float height;

        public GameObject Target { get => target; set => target = value; }
        public string TargetName
        {
            get => targetName; set
            {
                if (!targetName.Equals(value))
                {
                    if (textComponent)
                    {
                        textComponent.text = value;
                    }
                    targetName = value;
                }
            }
        }

        private void Start()
        {
            width = GetComponent<RectTransform>().rect.width / 2f;
            height = GetComponent<RectTransform>().rect.width / 2f;

        }

        private void Update()
        {
            if (target)
            {
                var pos = Camera.main.WorldToScreenPoint(target.transform.position);

                if (keepInside)
                {
                    pos.y = Mathf.Clamp(pos.y, height, Screen.height-height);
                    //朝向目标
                    if (pos.z > 0)
                    {
                        pos.x = Mathf.Clamp(pos.x, width, Screen.width-width);
                    }
                    else
                    {
                        //不朝向目标
                        if (pos.x > Screen.width / 2f)
                        {
                            pos.x = 0f+width;
                        }
                        else
                        {
                            pos.x = Screen.width-width;
                        }
                        pos.y =  Screen.height -pos.y;

                    }
                }
                else if (pos.z < 0)
                {
                    return;
                }
                transform.position = pos;
            }

        }

    }

}

