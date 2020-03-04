/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：容器类功能的触发器类
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

/// <summary>
/// 触发脚本，事件监听方法
/// </summary>
namespace Com.Rainier.Buskit3D.Example_004_PanelClass
{
    /// <summary>
    /// 容器类的触发器类
    /// </summary>
    [RequireComponent(typeof(PanelClassLogic))]
    [RequireComponent(typeof(PanelClassModel))]
    public class PanelClassTrigger : MonoBehaviour
    {
        //是否添加过字典
        private bool _isAddDictionary;
        //是否添加过ArrayList
        private bool _isArrayList;
        //是否添加过List
        private bool _isList;
        //容器实体
        private PanelClassEntity _panelEntity;

        /// <summary>
        /// 朝字典添加一条数据和修改一条数据
        /// </summary>
        public void OnClickDataDicAdd()
        {
            if (_isAddDictionary) return;
            _panelEntity = (PanelClassEntity)GetComponent<DataModelBehaviour>().DataEntity;
            _panelEntity.dataDic.Add(1, "1111");
            _panelEntity.dataDic.Add(2, "4444");
            _panelEntity.dataDic[1] = "222";
            _isAddDictionary = true;
        }

        /// <summary>
        /// 朝字典删除一组数据
        /// </summary>
        public void OnClickDataDicDel()
        {
            if (!_isAddDictionary) return;
            _panelEntity = (PanelClassEntity)GetComponent<DataModelBehaviour>().DataEntity;
            _panelEntity.dataDic.Remove(1);
            _panelEntity.dataDic.Clear();
            _isAddDictionary = false;
        }

        /// <summary>
        /// 朝动态数组添加一组数据和修改一条数据
        /// </summary>
        public void OnClickDataArrayListAdd()
        {
            if (_isArrayList) return;
            _panelEntity = (PanelClassEntity)GetComponent<DataModelBehaviour>().DataEntity;
            _panelEntity.dataArrayList.Add(3);
            _panelEntity.dataArrayList.Add(4);
            _panelEntity.dataArrayList[1]=10;
            _panelEntity.dataArrayList.Insert(2, 10);
            _isArrayList = true;
        }
        
        /// <summary>
        /// 朝动态数组删除一组数据
        /// </summary>
        public void OnClickDataArrayListDel()
        {
            if (!_isArrayList) return;
            _panelEntity = (PanelClassEntity)GetComponent<DataModelBehaviour>().DataEntity;
            _panelEntity.dataArrayList.Remove(3);
            _panelEntity.dataArrayList.Remove(4);
            _panelEntity.dataArrayList.RemoveAt(0);
            _panelEntity.dataArrayList.Clear();
            _isArrayList = false;
        }

        /// <summary>
        /// 朝集合添加一组数据和修改一条数据
        /// </summary>
        public void OnClickdataListAdd()
        {
            if (_isList) return;
            _panelEntity = (PanelClassEntity)GetComponent<DataModelBehaviour>().DataEntity;
            _panelEntity.dataList.Add(5);
            _panelEntity.dataList.Add(6);
            _panelEntity.dataList[0] = 20;
            _panelEntity.dataList.Insert(1, 30);

         
            _isList = true;
        }

        /// <summary>
        /// 朝集合添加一组数据和删除一条数据
        /// </summary>
        public void OnClickdataListDel()
        {
            if (!_isList) return;
            _panelEntity = (PanelClassEntity)GetComponent<DataModelBehaviour>().DataEntity;
            _panelEntity.dataList.Remove(5);
            _panelEntity.dataList.Remove(6);
            _panelEntity.dataList.RemoveAt(1);
            _panelEntity.dataList.Clear();
            _isList = false;
        }
    }
}
