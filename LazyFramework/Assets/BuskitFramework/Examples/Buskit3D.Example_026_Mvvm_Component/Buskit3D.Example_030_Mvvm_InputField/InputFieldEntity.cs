/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： InputFieldEntity
* 创建日期：2019-03-18 10:58:52
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_30_Mvvm_InputField
{
	/// <summary>
    ///
    /// </summary>
	public class InputFieldEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 输入框测试
        /// </summary>
        [RestoreFireLogic]
        public string inputContent;
	}
}

