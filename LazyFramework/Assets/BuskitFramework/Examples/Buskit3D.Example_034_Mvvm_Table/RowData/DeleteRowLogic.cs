/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：DeleteRowLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：删除数据行逻辑
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_032_Mvvm_Table
{
    /// <summary>
    /// 删除数据行逻辑
    /// </summary>
    public class DeleteRowLogic : LogicBehaviour
    {
        /// <summary>
        /// 处理删除数据行事件
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //MVVM初始化事件特点是OldValue==NewValue，
            //这里表示不处理初始化事件
            if (evt.OldValue == evt.NewValue)
            {
                return;
            }

            if (evt.PropertyName.Equals("removeClicked"))
            {
                //获取表实体并在表中删除行数据
                RowDataEntity rentity = (RowDataEntity)GetComponent<RowDataViewModel>().DataEntity;
                TableViewModel tvm = gameObject.GetComponentInParent<TableViewModel>();
                if (tvm != null)
                {
                    TableEntity entity = (TableEntity)tvm.DataEntity;
                    entity.rowData.Remove(rentity);
                }

                return;
            }
        }
    }
}
