
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

    public class TabDicTrigerStruct : MemoryTableIDicTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void ShowData()
        {
            if (entity.watchTableDicStruct.Count == 0)
            {
                Debug.Log("当前字典中无数据");
                return;
            }
            foreach (int key in entity.watchTableDicStruct.Keys)
            {
                Debug.Log("key:" + key + "  " + "value:" + entity.watchTableDicStruct[key].score);
            }
        }

        public override void WatchTableDicAdd()
        {
            ScoreStruct score1 = new ScoreStruct(10);
            ScoreStruct score2 = new ScoreStruct(10);
            ScoreStruct score3 = new ScoreStruct(10);
            entity.watchTableDicStruct.Add(0, score1);
            entity.watchTableDicStruct.Add(1, score2);
            entity.watchTableDicStruct.Add(2, score3);
        }

        public override void WatchTableDicChangValue()
        {
            ScoreStruct score = entity.watchTableDicStruct[0];
            score.score = 88;
            entity.watchTableDicStruct[0] = score;
        }

        public override void WatchTableDicClear()
        {
            entity.watchTableDicStruct.Clear();
        }

        public override void WatchTableDicRemove()
        {
            entity.watchTableDicStruct.Remove(0);
        }

    }
}