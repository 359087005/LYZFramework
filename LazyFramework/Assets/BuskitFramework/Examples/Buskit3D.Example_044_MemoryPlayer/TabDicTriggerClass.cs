
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

    public class TabDicTriggerClass : MemoryTableIDicTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void ShowData()
        {
            if (entity.watchTableDicClass.Count == 0)
            {
                Debug.Log("当前字典中无数据");
                return;
            }
            foreach (int key in entity.watchTableDicClass.Keys)
            {
                Debug.Log("key:" + key + "  " + "value:" + entity.watchTableDicClass[key].score);
            }
        }

        public override void WatchTableDicAdd()
        {
            ScoreClass score1 = new ScoreClass(10);
            ScoreClass score2 = new ScoreClass(10);
            ScoreClass score3 = new ScoreClass(10);
            entity.watchTableDicClass.Add(0, score1);
            entity.watchTableDicClass.Add(1, score2);
            entity.watchTableDicClass.Add(2, score3);
        }

        public override void WatchTableDicChangValue()
        {
            ScoreClass score = new ScoreClass(77);   
            entity.watchTableDicClass[0] = score;            
        }

        public override void WatchTableDicClear()
        {
            entity.watchTableDicClass.Clear();
        }

        public override void WatchTableDicRemove()
        {
            entity.watchTableDicClass.Remove(0);
        }

    }
}