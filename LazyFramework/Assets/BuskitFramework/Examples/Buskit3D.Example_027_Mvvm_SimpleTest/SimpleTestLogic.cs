/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：Binding
* 创建日期：2019-0-07 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：业务逻辑类
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

/// <summary>
/// 业务逻辑处理类
/// </summary>
namespace Buskit3D.Example_25_Mvvm_SimpleTest
{
    public class SimpleTestLogic : LogicBehaviour
    {
        /// <summary>
        /// 错误提示Text
        /// </summary>
        public Text erroyText;

        public bool isRight = false;
        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            SimpleTestEntity entity = (SimpleTestEntity)evt.TargetObject;

            if (evt.PropertyName.Equals("answerInputField"))
            {               
                //输入框不为空
                if (!string.IsNullOrEmpty(entity.answerInputField))
                {
                    int num1 = int.Parse(entity.question.Split('+')[0]);
                    int num2 = int.Parse(entity.question.Split('+')[1]);
                    //未回答题数大于0
                    if (int.Parse(entity.questionNum) > 0)
                    {

                        //回答正确
                        if (int.Parse(entity.answerInputField).Equals(num1 + num2))
                        {

                            erroyText.gameObject.SetActive(false);
                            entity.isRight = true;
                        }
                        //回答错误
                        else
                        {
                            Debug.Log(entity.question + "=" + (num1 + num2));
                            erroyText.gameObject.SetActive(true);
                        }
                    }
                }
            }

            if (evt.PropertyName.Equals("isSure")&&(int)evt.NewValue>=0)
            {
                if (entity.isRight)
                {
                    entity.questionNum = (int.Parse(entity.questionNum) - 1).ToString();
                    RandomNum();
                    entity.isRight = false;
                }
               
            }
        }

        /// <summary>
        /// 随机出题
        /// </summary>
        public void RandomNum()
        {
            SimpleTestEntity entity = (SimpleTestEntity)GetComponent<SimpleTestViewModel>().DataEntity;
            //加分
            entity.grade = (int.Parse(entity.grade) + 20).ToString();
            entity.question = Random.Range(1, 10) + "+" + Random.Range(1, 10);
            if (int.Parse(entity.questionNum) <= 0)
            {
                entity.question = "over";
            }
        }
    }
}

