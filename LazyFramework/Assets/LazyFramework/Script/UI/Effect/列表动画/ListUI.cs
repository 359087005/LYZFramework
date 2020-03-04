/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ListUI
* 创建日期：2019-12-19 15:49:33
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Buskit3D_CaiLiao
{
    /// <summary>
    /// 
    /// </summary>
	public class ListUI : MonoBehaviour
    {
        private Animator animator;
        private bool open=true;

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            animator = GetComponent<Animator>();
            transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
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
            });
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
        }
    }
}

