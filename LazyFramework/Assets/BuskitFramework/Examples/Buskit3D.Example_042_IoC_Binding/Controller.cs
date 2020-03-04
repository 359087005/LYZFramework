
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Controller
* 创建日期：2019-04-17 15:24:51
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace IoCAndTwoCommunication
{
    /// <summary>
    /// 
    /// </summary>
	public class Controller : CommonMonoBehavior 
	{
        private int index = 0;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //射线检测，点击UI和空白处无反应
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name.Contains("qiu"))
                    {
                        var value = GetComponentsInChildren<TestQiuMove>();
                        for (int i = 0; i < value.Length; i++)
                        {
                            value[i].enabled = false;
                        }
                        hit.collider.GetComponent<TestQiuMove>().enabled = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                index++;
                var value = GetComponentsInChildren<TestQiuMove>();
                if (index % 2 == 0)
                {
                    value[0].enabled = true;
                    value[1].enabled = false;
                }
                else
                {
                    value[0].enabled = false;
                    value[1].enabled = true;
                }
                if (index == 100) index = 0;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("按下P键");
                GameObject game = GameObject.Find("Capsule");
                var value = utilsEntity.GetEntity<TestTaEntity>(game);
                value.callList.Add(-11527);
            }
        }
    }
}

