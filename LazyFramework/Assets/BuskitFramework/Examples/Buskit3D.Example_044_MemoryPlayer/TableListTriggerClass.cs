
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IMemoryTestTrigger
* 创建日期：2019-04-03 09:06:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
namespace Buskit3D.Example_040_MemoryPlayer
{

    public class TableListTriggerClass : MemoryTableListTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void WatchTableListAdd()
        {
            ScoreClass scrore1 = new ScoreClass(100);
            ScoreClass scrore2 = new ScoreClass(100);
            ScoreClass scrore3 = new ScoreClass(100);
            entity.watchTableListClass.Add(scrore1);
            entity.watchTableListClass.Add(scrore2);
            entity.watchTableListClass.Add(scrore3);
        }

        public override void WatchTableListChangValue()
        {
            entity.watchTableListClass[0] = new ScoreClass(111);
        }

        public override void WatchTableListClear()
        {
            entity.watchTableListClass.Clear();
        }

        public override void WatchTableListInsert()
        {
            ScoreClass scrore1 = new ScoreClass(88);
            entity.watchTableListClass.Insert(0, scrore1);
        }

        public override void WatchTableListRemove()
        {
            entity.watchTableListClass.Remove(entity.watchTableListClass[0]);
        }

        public override void WatchTableListRemoveAt()
        {
            entity.watchTableListClass.RemoveAt(0);
        }

        public override void WatchTableListShow()
        {
            if (entity.watchTableListClass.Count == 0)
            {
                Debug.Log("当前集合中无数据");
                return;
            }
            for (int i = 0; i < entity.watchTableListClass.Count; i++)
            {
                Debug.Log(i + "__" + entity.watchTableListClass[i].score);
            }
        }
    }
}
