
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SenceADataModel
* 创建日期：2019-01-09 15:53:26
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    场景切换数据模型
******************************************************************************/
using UnityEngine;
namespace Com.Rainier.Buskit3D.Example_012
{
    /// <summary>
    /// 对象模型类
    /// </summary>
    [RequireComponent(typeof(SenceLoadingLogic))]
    public class SenceLoadingDataModel : DataModelBehaviour
    {
        private void Awake()
        {
            SenceLoadingDataEntity entity = new SenceLoadingDataEntity();
            this.DataEntity = entity;
            Watch(this);
        } 
    } 
}

