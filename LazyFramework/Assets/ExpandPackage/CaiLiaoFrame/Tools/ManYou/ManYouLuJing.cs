/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ManYouLuJing
* 创建日期：2018-06-08 11:13:52
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
using UnityEngine.Events;
using DG.Tweening;
namespace XuYi
{

	/// <summary></summary>
    public class ManYouLuJing : MonoBehaviour
	{
        public static ManYouLuJing intance{set{}get{return _intance;}}
        static ManYouLuJing _intance;
        private List<ManYouCanShu> ManYouCanShus = new List<ManYouCanShu>();

        void Awake() 
        {
            _intance = this;

        }
        //[ContextMenu("Start")]
        private void Start()
        {
            Debug.Log("Start");
            ManYouCanShu[] man = this.GetComponentsInChildren<ManYouCanShu>();
            Camera[] mCam = this.GetComponentsInChildren<Camera>();
            for (int i = 0; i < man.Length; i++)
            {
                mCam[i].enabled = false;
                ManYouCanShus.Add(man[i]);
            }
            //Debug.Log(ManYouCanShus.Count);

        }
        [ContextMenu("开始漫游")]
        public void Open()
        {
            OpenManYou(0);
        }

        public void Open(UnityAction action=null) 
        {
            OpenManYou(0, action);
        }
        /// <summary>        /// 开始漫游        /// </summary>
        public void OpenManYou(int index=0,UnityAction action=null) 
        {
            if (index < ManYouCanShus.Count)
                ManYouCanShus[index].OpenMove(() => { OpenManYou(++index, action); });
            else
                action.Invoke();
        }
        
#if UNITY_EDITOR
        void OnDrawGizmos() 
        {
            Camera[] mCam = this.GetComponentsInChildren<Camera>();
            List<Transform> mCamTrans = new List<Transform>();
            List<Transform> mPlayTrans = new List<Transform>();
            for (int i = 0; i < mCam.Length; i++)
            {
                mCamTrans.Add(mCam[i].transform);
                //mCam[i].enabled = false;
                mPlayTrans.Add(mCam[i].transform.parent);
            }
            DrawGizmos(mCamTrans, Color.red);
            DrawGizmos(mPlayTrans, Color.blue);
        }
        #region 画线
        private void DrawGizmos(List<Transform> mTrans, Color DrawColor)
        {
            if (mTrans.Count > 0)
            {
                Gizmos.color = DrawColor;
                for (int i = 0; i < mTrans.Count - 1; i++)
                {
                    Gizmos.DrawSphere(mTrans[i].position, 0.1f);
                    Gizmos.DrawLine(mTrans[i].position, mTrans[i + 1].position);
                }
                Gizmos.DrawSphere(mTrans[mTrans.Count-1].position, 0.1f);
            }
        }
        #endregion
#endif
    }
}

