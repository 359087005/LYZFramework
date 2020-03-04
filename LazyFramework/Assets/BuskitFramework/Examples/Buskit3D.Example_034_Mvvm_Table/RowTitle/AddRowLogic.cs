/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：AddRowLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：添加行业务逻辑
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_032_Mvvm_Table
{
    /// <summary>
    /// 添加行业务逻辑
    /// </summary>
    public class AddRowLogic : LogicBehaviour
    {
        /// <summary>
        /// 处理添加行业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
          
            //忽略初始化事件
            if (evt.OldValue == evt.NewValue)
            {
                return;
            }

            if (evt.PropertyName.Equals("addClicked"))
            {
                //随机创建行数据并添加到数据表
                TableViewModel tvm = gameObject.GetComponentInParent<TableViewModel>();
                if(tvm != null)
                {
                    TableEntity entity = (TableEntity)tvm.DataEntity;
                    RowDataEntity rentity = CreatRandomRowEntity();
                    entity.rowData.Add(rentity);
                }
                return;
            }
        }

        /// <summary>
        /// 创建随机行数据
        /// </summary>
        /// <returns></returns>
        private RowDataEntity CreatRandomRowEntity()
        {
            RowDataEntity rentity = new RowDataEntity();
            rentity.age = UnityEngine.Random.Range(10,100).ToString();
            rentity.id = "BVRP-"+ UnityEngine.Random.Range(10, 100).ToString(); 
            rentity.name = "Student";
            rentity.clazz = "BBRR";
            rentity.status = "Good";
            return rentity;
        }
    }
}
