
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
    public class TabDicTriggerFloat : MemoryTableIDicTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void ShowData()
        {
            if (entity.watchTableDicFloat.Count == 0)
            {
                Debug.Log("当前字典中无数据");
                return;
            }
            foreach (int key in entity.watchTableDicFloat.Keys)
            {
                Debug.Log("key:" + key + "  " + "value:" + entity.watchTableDicFloat[key]);
            }
        }

        public override void WatchTableDicAdd()
        {
            entity.watchTableDicFloat.Add(0, 0.5f);
            entity.watchTableDicFloat.Add(1, 0.5f);
            entity.watchTableDicFloat.Add(2, 0.5f);
        }

        public override void WatchTableDicChangValue()
        {
            entity.watchTableDicFloat[0] = 1.0f;
        }

        public override void WatchTableDicClear()
        {
            entity.watchTableDicFloat.Clear();
        }

        public override void WatchTableDicRemove()
        {
            entity.watchTableDicFloat.Remove(0);
        }

    }
}