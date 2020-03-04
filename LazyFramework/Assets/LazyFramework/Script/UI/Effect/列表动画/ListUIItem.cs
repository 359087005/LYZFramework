/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ListUIItem
* 创建日期：2019-12-19 15:23:23
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：列表动画
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Buskit3D_CaiLiao
{
    /// <summary>
    /// 
    /// </summary>
	public class ListUIItem : MonoBehaviour, IPointerDownHandler
    {
        private Animator animator;
        private bool open=true;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (open)
            {
                animator.SetTrigger("open");

            }
            else
            {
                animator.SetTrigger("close");

            }
            open = !open;
        }




        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
        }
    }
}

