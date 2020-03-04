/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：容器类功能的事件分发器类
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_004_PanelClass
{
    /// <summary>
    /// 事件分发器类
    /// </summary>
    public class PanelClassModel : DataModelBehaviour
    {
        /// <summary>
        /// 监听实体
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new PanelClassEntity();
            Watch(this);
        }


        /// <summary>
        /// 从RecoverSystem中读取数据
        /// </summary>
        public override void LoadStorageData()
        {
            base.LoadStorageData();
            //还原位置信息
            var entity = (PanelClassEntity)GetComponent<PanelClassModel>().DataEntity;
            Debug.Log(entity.dataDic.Count);
        }
    }
}
