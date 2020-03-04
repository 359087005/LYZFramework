/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ManYouCanShu
* 创建日期：2018-06-08 13:20:49
* 作者名称：
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;

namespace XuYi
{

	/// <summary></summary>
	public class ManYouCanShu : MonoBehaviour 
	{
        public float Time = 3;
        public float DeTime = 0;
        public string TiShi="";
        Camera cam;
        public void Start() 
        {
            cam = this.GetComponentInChildren<Camera>();
        }
        public void OpenMove(UnityAction action) 
        {

            Debug.Log("OpenMove");
            MoveToDestination(this.transform.position, this.transform.eulerAngles,
                cam.transform.position, cam.transform.eulerAngles, Time, DeTime, cam.fieldOfView).OnComplete(() => { action.Invoke();  });
        }

        public Tweener MoveToDestination(Vector3 playerPos, Vector3 playerRot, Vector3 camPos, Vector3 _camRot, float time, float detime, float fieldvalue)
        {

            Tweener t = default(Tweener);
            FirstViewControl.instance.transform.DOMove(playerPos, time).SetRelative(false).SetDelay(detime).SetEase(Ease.InOutQuad);
            FirstViewControl.instance.transform.DORotate(playerRot, time).SetRelative(false).SetDelay(detime).SetEase(Ease.InOutQuad);
            Camera.main.transform.DOMove(camPos, time).SetRelative(false).SetDelay(detime).SetEase(Ease.InOutQuad);
            t = Camera.main.transform.DORotate(_camRot, time).SetRelative(false).SetDelay(detime).SetEase(Ease.InOutQuad);

            Tweener tt = DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, fieldvalue, time).SetRelative(false).SetDelay(detime).SetEase(Ease.InOutQuad);


            t.OnComplete(() =>
            {
                FirstViewControl.instance.IniQuaternion();

            });


            return t;
          
        }
	}
}

