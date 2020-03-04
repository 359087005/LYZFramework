
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： PresureTest
* 创建日期：2019-03-12 16:05:55
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_000_PresureDemo
{

 

    /// <summary>
    /// 
    /// </summary>
	public class PresureTest : MonoBehaviour 
	{

        public int count = 0;
        public int number=100;
        public GameObject parent;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(CreateCube());

            }
        }

      
        IEnumerator CreateCube()
        {
            for (int i = 0; i < number; i++)
            {
                GameObject go= ReplayManager.GetInstance("Cube", new Vector3(Random.Range(-250,250), Random.Range(-250, 250), Random.Range(-250, 250)));
                go.transform.SetParent(parent.transform);
                ReplayManager.RegisterPrefab(go);
                count++;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

