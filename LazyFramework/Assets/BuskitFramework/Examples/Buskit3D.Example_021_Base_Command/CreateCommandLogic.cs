/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（Cube创建命令业务逻辑）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// 创建命令业务逻辑处理类
    /// </summary>
    public class CreateCommandLogic : LogicBehaviour
    {
        /// <summary>
        /// 命令系统服务
        /// </summary>
        [Inject]
        IServiceCommand _commandService;

        /// <summary>
        /// 注入命令系统
        /// </summary>
        private void Start()
        {
            InjectService.InjectInto(this);
        }

        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if(evt.PropertyName.Equals("CreatePartMessage"))
            {
                //给参数赋值
                CreateCommandStr str = (CreateCommandStr)evt.NewValue;
                CubeCreateControllCommand cmd = new CubeCreateControllCommand();
                cmd.Position = str.Position;
               _commandService.GetCommandStack().Execute(cmd);
            }
        }
    }
}

