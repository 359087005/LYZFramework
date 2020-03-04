/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SenceADataModel
* 创建日期：2019-01-09 15:53:26
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    场景切换触发器
******************************************************************************/
using UnityEngine;
namespace Com.Rainier.Buskit3D.Example_012
{
    public class SenceLoadingTrigger : MonoBehaviour
    {
        /// <summary>
        /// 不销毁SenceManager
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }

        /// <summary>
        /// 切换场景按钮点击事件
        /// </summary>
        public void OnClickLoadSence_A()
        {
            SenceLoadingDataEntity entity = (SenceLoadingDataEntity)GameObject.FindObjectOfType<SenceLoadingDataModel>().DataEntity;
            entity.TargetSenceName = "SceneB";
            entity.IsShowLoadingUI = true;                     
        }

        public void OnClickLoadSence_B()
        {
            SenceLoadingDataEntity entity = (SenceLoadingDataEntity)GameObject.FindObjectOfType<SenceLoadingDataModel>().DataEntity;
            entity.TargetSenceName = "SceneC";
            entity.IsShowLoadingUI = true;
        }

        public void OnClickLoadSence_C()
        {
            SenceLoadingDataEntity entity = (SenceLoadingDataEntity)GameObject.FindObjectOfType<SenceLoadingDataModel>().DataEntity;
            entity.TargetSenceName = "SceneA";
            entity.IsShowLoadingUI = true;
        }
    }
}

