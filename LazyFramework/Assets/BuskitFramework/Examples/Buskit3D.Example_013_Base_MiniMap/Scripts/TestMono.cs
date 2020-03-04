
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestMono
* 创建日期：2018-12-18 13:56:48
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 测试小地图mono脚本
    /// </summary>
	public class TestMono : MonoBehaviour 
	{
        public void Test(GameObject go)
        {
            ((MiniMapEntity)GameObject.Find("Scene").GetComponentInChildren<MinniMapModel>().DataEntity).eventID     = go.transform.GetSiblingIndex();
        }        
    }
}

