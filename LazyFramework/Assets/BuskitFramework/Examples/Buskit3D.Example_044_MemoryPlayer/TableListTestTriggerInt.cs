
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
    public class TableListTestTriggerInt : MemoryTableListTestTrigger
    {
        public override void Start()
        {
            base.Start();
        }

        public override void WatchTableListAdd()
        {
            entity.watchTableListInt.Add(1001);
            entity.watchTableListInt.Add(1002);
            entity.watchTableListInt.Add(1003);
        }

        public override void WatchTableListRemove()
        {
            int value = 1001;
            entity.watchTableListInt.Remove(value);
        }

        public override void WatchTableListRemoveAt()
        {
            int index = 0;
            entity.watchTableListInt.RemoveAt(index);
        }

        public override void WatchTableListClear()
        {
            entity.watchTableListInt.Clear();
        }

        public override void WatchTableListInsert()
        {
            int index = 0;
            int value = 10001;
            entity.watchTableListInt.Insert(index, value);
        }

        public override void WatchTableListChangValue()
        {
            int index = 0;
            int value = -1;
            entity.watchTableListInt[index] = value;
        }

        public override void WatchTableListShow()
        {
            if (entity.watchTableListInt.Count == 0)
            {
                Debug.Log("当前集合中无数据");
                return;
            }
            for (int i = 0; i < entity.watchTableListInt.Count; i++)
            {
                Debug.Log(i + "__" + entity.watchTableListInt[i]);
            }
        }
    }
}
