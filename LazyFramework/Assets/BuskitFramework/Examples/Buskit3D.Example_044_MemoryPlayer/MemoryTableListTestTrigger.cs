
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MemoryTableListTestTrigger 
* 创建日期：2019-04-03 09:06:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Buskit3D.Example_040_MemoryPlayer
{
    /// <summary>
    /// 列表测试封装实现类
    /// </summary>
	public class MemoryTableListTestTrigger : MonoBehaviour , IMemoryListTrigger
    {
        protected Button addBtn;
        protected Button removeBtn;
        protected Button RemoveAtBtn;
        protected Button clearBtn;
        protected Button insertBtn;
        protected Button changValueBtn;
        protected Button showData;
        protected MemoryTestEntity entity;
        /// <summary>
        /// 初始化组件
        /// </summary>
        public virtual void Start()
        {
            addBtn = transform.Find("Add").GetComponent<Button>();
            removeBtn = transform.Find("Remove").GetComponent<Button>();
            RemoveAtBtn = transform.Find("RemoveAt").GetComponent<Button>();
            clearBtn = transform.Find("Clear").GetComponent<Button>();
            insertBtn = transform.Find("Insert").GetComponent<Button>();
            changValueBtn = transform.Find("Remove[]").GetComponent<Button>();
            showData = transform.Find("ShowData").GetComponent<Button>();

            addBtn.onClick.AddListener(WatchTableListAdd);
            removeBtn.onClick.AddListener(WatchTableListRemove);
            RemoveAtBtn.onClick.AddListener(WatchTableListRemoveAt);
            clearBtn.onClick.AddListener(WatchTableListClear);
            insertBtn.onClick.AddListener(WatchTableListInsert);
            changValueBtn.onClick.AddListener(WatchTableListChangValue);
            showData.onClick.AddListener(WatchTableListShow);

            entity = (MemoryTestEntity)FindObjectOfType<MemoryTestDataModel>().DataEntity;
        }
        /// <summary>
        /// 朝列表添加一个数据
        /// </summary>

        public virtual void WatchTableListAdd()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 移除列表中的数据
        /// </summary>
        public virtual void WatchTableListRemove()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 移除列表中的指定数据
        /// </summary>
        public virtual void WatchTableListRemoveAt()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 列表数据清除
        /// </summary>
        public virtual void WatchTableListClear()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 添加一组数据在制定位置
        /// </summary>
        public virtual void WatchTableListInsert()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 修改列表值变化
        /// </summary>
        public virtual void WatchTableListChangValue()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 列表数据显示
        /// </summary>
        public virtual void WatchTableListShow()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 列表数据显示
        /// </summary>
        public void ShowData()
        {
            throw new System.NotImplementedException();
        }
    }
}

