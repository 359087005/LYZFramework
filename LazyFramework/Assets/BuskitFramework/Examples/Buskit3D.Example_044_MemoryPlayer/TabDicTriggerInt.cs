
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

    public class TabDicTriggerInt : MemoryTableIDicTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void ShowData()
        {
            if (entity.watchTableDicInt.Count == 0)
            {
                Debug.Log("当前字典中无数据");
                return;
            }
            foreach (int key in entity.watchTableDicInt.Keys) {
                Debug.Log("key:" + key + "  " + "value:" + entity.watchTableDicInt[key]);
            }
        }

        public override void WatchTableDicAdd()
        {
            entity.watchTableDicInt.Add(0, 0);
            entity.watchTableDicInt.Add(1, 0);
            entity.watchTableDicInt.Add(2, 0);
        }

        public override void WatchTableDicChangValue()
        {
            entity.watchTableDicInt[0] = 1;
        }

        public override void WatchTableDicClear()
        {
            entity.watchTableDicInt.Clear();
        }

        public override void WatchTableDicRemove()
        {
            entity.watchTableDicInt.Remove(0);
        }

    }
}