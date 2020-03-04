/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestQiuMove
* 创建日期：2019-04-17 13:21:57
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
	public class TestQiuMove : MonoBehaviour
    {
        public float speed;
        Vector3 transformValue = new Vector3();//定义平移向量

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transformValue = Vector3.forward * Time.deltaTime * speed;
                transform.Translate(transformValue, Space.World);//平移角色
            }
            if (Input.GetKey(KeyCode.A))
            {
                transformValue = Vector3.left * Time.deltaTime * speed;
                transform.Translate(transformValue, Space.World);//平移角色
            }
            if (Input.GetKey(KeyCode.S))
            {
                transformValue = Vector3.back * Time.deltaTime * speed;
                transform.Translate(transformValue, Space.World);//平移角色
            }
            if (Input.GetKey(KeyCode.D))
            {
                transformValue = Vector3.right * Time.deltaTime * speed;
                transform.Translate(transformValue, Space.World);//平移角色
            }
            
        }
    }
}

