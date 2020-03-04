
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Buskit3D.Example_040_MemoryPlayer
{
    public class TableListTestTriggerStruct : MemoryTableListTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void WatchTableListAdd()
        {
            ScoreStruct scrore1 = new ScoreStruct(100);
            ScoreStruct scrore2 = new ScoreStruct(100);
            ScoreStruct scrore3 = new ScoreStruct(100);
            entity.watchTableListStruct.Add(scrore1);
            entity.watchTableListStruct.Add(scrore2);
            entity.watchTableListStruct.Add(scrore3);
        }

        public override void WatchTableListChangValue()
        {
            ScoreStruct score = entity.watchTableListStruct[0];
            score.score = 10;
            entity.watchTableListStruct[0] = score;
        }

        public override void WatchTableListClear()
        {
            entity.watchTableListStruct.Clear();
        }

        public override void WatchTableListInsert()
        {
            ScoreStruct score = new ScoreStruct(99);
            entity.watchTableListStruct.Insert(0, score);
        }

        public override void WatchTableListRemove()
        {
            ScoreStruct score = new ScoreStruct(100);
            entity.watchTableListStruct.Remove(score);
        }

        public override void WatchTableListRemoveAt()
        {
            entity.watchTableListStruct.RemoveAt(0);
        }

        public override void WatchTableListShow()
        {
            if (entity.watchTableListStruct.Count == 0)
            {
                Debug.Log("当前集合中无数据");
                return;
            }
            for (int i = 0; i < entity.watchTableListStruct.Count; i++)
            {
                Debug.Log(i + "__" + entity.watchTableListStruct[i].score);
            }
        }
    }
}
