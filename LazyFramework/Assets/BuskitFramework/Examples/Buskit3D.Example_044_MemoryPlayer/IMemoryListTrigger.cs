
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

namespace Buskit3D.Example_040_MemoryPlayer
{
    /// <summary>
    /// list测试接口
    /// </summary>
    public interface IMemoryListTrigger : IMemoryTest
    {
        void WatchTableListAdd();
        void WatchTableListRemove();
        void WatchTableListRemoveAt();
        void WatchTableListClear();
        void WatchTableListInsert();
        void WatchTableListChangValue();
        void WatchTableListShow();
    }
}
