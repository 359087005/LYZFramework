/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestQiuEntity
* 创建日期：2019-04-17 10:47:01
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace IoCAndTwoCommunication
{
	/// <summary>
    ///
    /// </summary>
	public class TestQiuEntity : BaseDataModelEntity
    {
        public TestQiuEntity bindQiu;
        public TestTaEntity bindingTa;
        /// <summary>
        /// 当前状态：0-触发器意外 1-触发其中 2-建立呼叫
        /// </summary>
        public int currentState = 0;

    }
}

