/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommandUIViewEntity
* 创建日期：2019-04-16 17:15:02
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
	public class CommandUIViewEntity : BaseDataModelEntity 
	{
       public int btnCreate;
       public int btnColor;
       public int btnDelete;
       public int btnUndo;
       public int btnRedo;
       public float rotationSlider;
	}
}

