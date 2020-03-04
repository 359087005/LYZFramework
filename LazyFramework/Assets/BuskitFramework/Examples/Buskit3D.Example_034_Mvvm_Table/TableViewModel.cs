/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：RowTitleViewModel
* 创建日期：2019-04-01 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_032_Mvvm_Table
{
    public class TableViewModel : ViewModelBehaviour
    {
        protected override void Awake()
        {
            this.DataEntity = new TableEntity();
            base.Awake();
        }

        public override void LoadStorageData()
        {
            base.LoadStorageData();
            TableEntity entity = (TableEntity)this.DataEntity;
            entity.isLoaded = true;
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.P))
            {
                TableEntity _entity = (TableEntity)this.DataEntity;
                foreach(RowDataEntity en in _entity.rowData)
                {
                    Debug.Log("RowData:[" + "id=" + en.id + " name=" + en.name +"]");
                }
            }
        }
    }
}
