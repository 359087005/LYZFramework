/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_001(数据实体)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit3D.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example_001
{
    /// <summary>
    /// 数据实体类
    /// </summary>
    public class HelloWorldDataEntity : BaseDataModelEntity
    {
        [RestoreFireLogic]
        public bool IsShow = false;
    }
}
