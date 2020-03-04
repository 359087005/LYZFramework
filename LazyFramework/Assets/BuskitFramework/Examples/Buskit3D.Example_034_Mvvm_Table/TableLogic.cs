/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ShowValueLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using System.Reflection;

namespace Buskit3D.Example_032_Mvvm_Table
{
    public class TableLogic : LogicBehaviour
    {
        //实体对象编号器
        public static int counter = 0;

        /// <summary>
        /// 处理数据表格业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //当数据恢复后触发更新界面显示
            if (evt.PropertyName.Equals("isLoaded"))
            {
                //获取Entity
                TableEntity entity = (TableEntity)(gameObject.
                    GetComponent<TableViewModel>().DataEntity);

                //生成数据行UI并重新绑定实体对象
                foreach(RowDataEntity row in entity.rowData)
                {
                    //加载行预制体
                    GameObject newRow = (GameObject)Resources.Load("Prefabs/ImageRowData");
                    newRow = Instantiate(newRow);
                    GameObject goContent = GameObject.Find("ScrollViewTableData/Viewport/Content");
                    if (goContent != null)
                    {
                        newRow.transform.parent = goContent.transform;
                    }

                    //重新绑定数据实体
                    RowDataViewModel rdvm = newRow.GetComponent<RowDataViewModel>();
                    rdvm.Rebinding(row);
                }
                return;
            }

            //当有行实体对象添加到表格实体的rowData中，
            //加载行预制体并重新绑定实体对象
            if (evt.PropertyName.Equals("rowData#Add"))
            {
                var info = evt.TargetObject.GetType().GetField("rowData").GetCustomAttribute<NoStorage>(); 
                if (info != null) {
                    Debug.Log("就是我！！！");
                }
                //给新创建的实体对象编号
                RowDataEntity entity = (RowDataEntity)evt.NewValue;

                //加载行预制体
                GameObject newRow = (GameObject)Resources.Load("Prefabs/ImageRowData");
                newRow = Instantiate(newRow);
                GameObject goContent = GameObject.Find("ScrollViewTableData/Viewport/Content");
                if(goContent != null)
                {
                    newRow.transform.parent = goContent.transform;
                }

                //重新绑定数据实体
                RowDataViewModel rdvm =  newRow.GetComponent<RowDataViewModel>();
                rdvm.Rebinding(entity);

                return;
            }

            //当表格实体的rowData数据发生删除事件时,执行预制体销毁
            if (evt.PropertyName.Equals("rowData#Remove"))
            {
                //销毁预制体
                GameObject goContent = GameObject.Find("ScrollViewTableData/Viewport/Content");
                RowDataViewModel[] rvms = goContent.GetComponentsInChildren<RowDataViewModel>();
                foreach(RowDataViewModel rvm in rvms)
                {
                    RowDataEntity rentity = (RowDataEntity)rvm.DataEntity;
                    if (ReferenceEquals(rentity, evt.OldValue))
                    {
                        Destroy(rvm.gameObject, 0.1f);
                    }
                }
            }
        }
    }
}
