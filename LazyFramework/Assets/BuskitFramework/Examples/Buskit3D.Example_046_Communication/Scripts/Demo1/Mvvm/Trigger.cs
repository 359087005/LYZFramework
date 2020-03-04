
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Call
* 创建日期：2019-04-08 17:34:23
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using System.Collections.Generic;

namespace Buskit3D.Example_045_IoCApplication
{  

    /// <summary>
    /// 
    /// </summary>
	public class Trigger : MonoBehaviour 
	{
        public static Trigger instance;
        public Dictionary<int, TaModel> dicTa = new Dictionary<int, TaModel>();
        public Dictionary<int, IphoneModel> dicIphoneModel = new Dictionary<int, IphoneModel>();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            TaModel A= GameObject.Find("A").GetComponent<TaModel>();
            TaModel B= GameObject.Find("B").GetComponent<TaModel>();
            TaModel C= GameObject.Find("C").GetComponent<TaModel>();
            TaModel D= GameObject.Find("D").GetComponent<TaModel>();
            IphoneModel a = GameObject.Find("ball0").GetComponent<IphoneModel>();
            IphoneModel b = GameObject.Find("ball1").GetComponent<IphoneModel>();
            dicTa.Add(0, A);
            dicTa.Add(1, B);
            dicTa.Add(2, C);
            dicTa.Add(3, D);
            dicIphoneModel.Add(0, a);
            dicIphoneModel.Add(1, b);


        }

        public void CallPhone() {
            var entity = (IphoneEntity)(GetComponent<DataModelBehaviour>().DataEntity);
            entity.currentState = 1;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A)) {
                transform.Translate(Vector3.left * 1 * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * 1 * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * 1 * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * 1 * Time.deltaTime, Space.World);
            }
        }
    }
}

