
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MemoryTableIDicTestTrigger
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
    /// 字典测试封装实现类
    /// </summary>
	public class MemoryTableIDicTestTrigger : MonoBehaviour, IMemoryDicTrigger
    {
        protected Button addBtn;
        protected Button removeBtn;
        protected Button clearBtn;
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
            clearBtn = transform.Find("Clear").GetComponent<Button>();
            changValueBtn = transform.Find("Remove[]").GetComponent<Button>();
            showData = transform.Find("ShowData").GetComponent<Button>();
            addBtn.onClick.AddListener(WatchTableDicAdd);
            removeBtn.onClick.AddListener(WatchTableDicRemove);
            clearBtn.onClick.AddListener(WatchTableDicClear);
            changValueBtn.onClick.AddListener(WatchTableDicChangValue);
            showData.onClick.AddListener(ShowData);
            entity = (MemoryTestEntity)FindObjectOfType<MemoryTestDataModel>().DataEntity;
        }
        /// <summary>
        /// 添加一个元素
        /// </summary>
        public virtual void WatchTableDicAdd()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 删除一个元素
        /// </summary>
        public virtual void WatchTableDicRemove()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 清楚字典
        /// </summary>
        public virtual void WatchTableDicClear()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 字典值变化
        /// </summary>
        public virtual void WatchTableDicChangValue()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 打印字典信息
        /// </summary>
        public virtual void ShowData()
        {
            throw new System.NotImplementedException();
        }
    }
}

