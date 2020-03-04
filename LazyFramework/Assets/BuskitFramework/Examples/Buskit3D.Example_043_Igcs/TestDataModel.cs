/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：TestEntity
* 创建日期：2019-03-14 11:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_0xx_Igcs
{
    public class TestDataModel : DataModelBehaviour
    {
        private void Awake()
        {
            this.DataEntity = new TestEntity();
            Watch(this);
        }
    }
}

