using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buskit3D.Example_040_MemoryPlayer
{
    public class TableListTestTriggerFloat : MemoryTableListTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void WatchTableListAdd()
        {
            entity.watchTableListFloat.Add(1001.5f);
            entity.watchTableListFloat.Add(1002.5f);
            entity.watchTableListFloat.Add(1003.5f);
        }

        public override void WatchTableListChangValue()
        {
            int index = 0;
            float value = -1.5f;
            entity.watchTableListFloat[index] = value;
        }

        public override void WatchTableListClear()
        {
            entity.watchTableListFloat.Clear();
        }

        public override void WatchTableListInsert()
        {
            int index = 0;
            float value = 10001.5f;
            entity.watchTableListFloat.Insert(index, value);
        }

        public override void WatchTableListRemove()
        {
            float value = 1001.5f;
            entity.watchTableListFloat.Remove(value);

        }

        public override void WatchTableListRemoveAt()
        {
            int index = 0;
            entity.watchTableListFloat.RemoveAt(index);
        }

        public override void WatchTableListShow()
        {
            if (entity.watchTableListFloat.Count == 0)
            {
                Debug.Log("当前集合中无数据");
                return;
            }
            for (int i = 0; i < entity.watchTableListFloat.Count; i++)
            {
                Debug.Log(i + "__" + entity.watchTableListFloat[i]);
            }
        }
    }
}