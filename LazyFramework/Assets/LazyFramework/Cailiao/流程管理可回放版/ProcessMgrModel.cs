/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ProcessControlPlayBackModel
* 创建日期：2019-06-28 10:20:07
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

public class ProcessMgrModel : DataModelBehaviour
{
    public static ProcessMgrModel ins;
    public void Awake()
    {
        ins = this;
        ProcessMgrEntity processDubegEntity = new ProcessMgrEntity();
        this.DataEntity = processDubegEntity;
        Watch(this);
    }
}

