/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LandingUILogic
* 创建日期：2019-03-14 11:04:30
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D.Example_012;
using UnityEngine.UI;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{   
	/// <summary>
    /// 
    /// </summary>
	public class LandingUILogic : LogicBehaviour 
	{
        public override void ProcessLogic(PropertyMessage evt)
        {
            LandingUIEntity entity = (LandingUIEntity)GameObject.FindObjectOfType<LandingUiViewModel>().DataEntity;
            //Esc按钮
            if (evt.PropertyName.Equals("escBtn"))
            {
                if((int)evt.NewValue > 0)
                {
                    Debug.Log("点击了退出按钮");
                }
            }
            //Landing按钮
            if (evt.PropertyName.Equals("landingBtn"))
            {
                if ((int)evt.NewValue > 0)
                {                 
                    if(!string.IsNullOrEmpty(entity.inputFieldText))
                    {
                        //答案校验
                        if (int.Parse(entity.inputFieldText).Equals(GetAnswer(entity)))
                        {
                            //开始场景切换
                            SenceLoadingDataEntity sceneEntity = (SenceLoadingDataEntity)GameObject.FindObjectOfType<SenceLoadingDataModel>().DataEntity;
                            sceneEntity.TargetSenceName = "031Main";
                            sceneEntity.IsShowLoadingUI = true;
                        }
                    }                    
                }
            }
        }

        /// <summary>
        /// 计算结果
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetAnswer(LandingUIEntity entity)
        {
            int num1 = int.Parse(entity.questionText.Split('+')[0]);
            int num2 = int.Parse(entity.questionText.Split('+')[1]);
            return num1 + num2;
        }
    }
}

