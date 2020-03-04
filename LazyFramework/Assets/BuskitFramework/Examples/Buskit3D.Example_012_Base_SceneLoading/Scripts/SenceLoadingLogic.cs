/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LoadingDataEntity
* 创建日期：2019-01-09 13:51:26
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    过渡场景业务逻辑
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_012
{
    /// <summary>
    /// 业务逻辑处理类
    /// </summary>
    public class SenceLoadingLogic : LogicBehaviour 
	{   //过渡UI   
        public GameObject LoadingUI;
        //进度条
        public Slider loadingSlider;
        //加载进度值
        public Text loadingText;
        //加载速度
        private float loadingSpeed = 1f;
        //控制切换场景
        private bool startLoading = false;
        //加载进度
        private float targetValue;
        //保存场景异步加载返回值
        private AsyncOperation operation;
        //数据实体对象
        private SenceLoadingDataEntity entity;
        //“SenceManager”对象
        private GameObject SceneManagerObj;

        private void Start()
        {
            SceneManagerObj = GameObject.Find("SenceManager");
            entity = (SenceLoadingDataEntity)GameObject.FindObjectOfType<SenceLoadingDataModel>().DataEntity;
            loadingSlider.value = 0.0f;
        }
        /// <summary>
        /// 接收消息后业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("IsShowLoadingUI"))
            {
                if ((bool)evt.NewValue)
                {
                    //显示LoadingUI，并开始异步加载场景
                    LoadingUI.gameObject.SetActive(true);
                    if (LoadingUI.gameObject.activeInHierarchy)
                        startLoading = true;
                    else
                        startLoading = false;
                    StartCoroutine(AsyncLoading());
                }
                else
                    LoadingUI.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <returns></returns>
        IEnumerator AsyncLoading()
        {
            operation = SceneManager.LoadSceneAsync(entity.TargetSenceName);
            //阻止当加载完成自动切换
            operation.allowSceneActivation = false;
            yield return operation;
        }

        void Update()
        {
            if(startLoading)
            {
                targetValue = operation.progress;
                if (operation.progress >= 0.9f)
                {
                    //operation.progress的值最大为0.9
                    targetValue = 1.0f;
                }
                if (targetValue != loadingSlider.value)
                {
                    //插值运算
                    loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
                    if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
                    {
                        loadingSlider.value = targetValue;
                    }
                }
                loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";
                if ((int)(loadingSlider.value * 100) == 100)
                {
                    operation.allowSceneActivation = true;
                    entity.IsShowLoadingUI = false;
                    startLoading = false;
                    loadingSlider.value = 0.0f;
                }
            }           
        }
    }
}

