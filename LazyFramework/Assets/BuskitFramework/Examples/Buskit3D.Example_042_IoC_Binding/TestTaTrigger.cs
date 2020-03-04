
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestTaTrigger
* 创建日期：2019-04-17 11:09:12
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace IoCAndTwoCommunication
{
    /// <summary>
    /// 
    /// </summary>
	public class TestTaTrigger : CommonMonoBehavior
    {
        protected override void Start()
        {
            base.Start();
        }

        private void OnTriggerEnter(Collider other)
        {
            BindingTool bt = BindingTool.GetInstance();
            
            #region 手机的绑定
            var testQiumodel = other.GetComponent<TestQiuModel>();
            var testQiuEntity = utilsEntity.GetEntity<TestQiuEntity>(other.gameObject);
            var testTaModel = GetComponent<TestTaModel>();
            var testTaEntity = utilsEntity.GetEntity<TestTaEntity>(gameObject);
            //先解绑
            if (testQiuEntity!=null && testQiuEntity.bindingTa!=null)
            {
                bt.Unbinding(testQiumodel, testQiuEntity.bindingTa);
                bt.Unbinding(testTaModel, testQiuEntity);
                testQiuEntity.bindingTa = null;
                testQiuEntity.bindQiu = null;
            }
            //再绑定
            bt.Binding(testQiumodel, testTaModel);

            testQiuEntity.bindingTa = testTaEntity;
            testTaEntity.testQiuEntityList.Add(testQiuEntity);
            #endregion

            #region 基站的绑定
            //bt.Binding(testTaModel, testQiumodel);
            #endregion

            Debug.Log("OnTriggerEnter");

        }

        private void OnTriggerExit(Collider other)
        {
            var testQiumodel = other.GetComponent<TestQiuModel>();
            var testQiuEntity = utilsEntity.GetEntity<TestQiuEntity>(other.gameObject);
            var testTaModel = GetComponent<TestTaModel>();
            var testTaEntity = utilsEntity.GetEntity<TestTaEntity>(gameObject);

            //testQiumodel.Unwatch(testTaModel, "beihuijiao");

            BindingTool bt = BindingTool.GetInstance();
            bt.Unbinding(gameObject, other.gameObject);
            bt.Unbinding(other.gameObject, gameObject);
            //testTaEntity.callList.RemovePropertyChangeListener(testTaEntity.callList);

            testQiuEntity.bindingTa = null;
            Debug.Log("OnTriggerExit");
        }
    }
}

