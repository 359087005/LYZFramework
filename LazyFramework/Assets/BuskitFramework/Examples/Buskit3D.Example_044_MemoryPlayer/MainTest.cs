
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MainTest
* 创建日期：2019-04-12 11:01:41
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
	public class MainTest : MonoBehaviour 
	{
        /// <summary>
        /// 列表测试
        /// </summary>
        public Transform transList;
        /// <summary>
        /// 字典测试
        /// </summary>
        public Transform transDic;

        public void ListTest() {
            transList.localScale = Vector3.one;
            transDic.localScale = Vector3.zero;
        }

        public void DictionaryTest() {
            transList.localScale = Vector3.zero;
            transDic.localScale = Vector3.one;
        }
	}
}

