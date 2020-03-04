/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CreateCommandLogic
* 创建日期：2019-04-10 11:44:38
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_046_Communication
{   
	/// <summary>
    /// 
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
            if (evt.PropertyName.Equals("CreateTowerMessage"))
            {
                //给参数赋值
                CreateTowerCommandStr str = (CreateTowerCommandStr)evt.NewValue;
                TowerCreateControllCommand cmd = new TowerCreateControllCommand();
                cmd.Position = str.Position;
                _commandService.GetCommandStack().Execute(cmd);
            }
            if (evt.PropertyName.Equals("CreatePhoneMessage"))
            {
                //给参数赋值
                CreatePhoneCommandStr str = (CreatePhoneCommandStr)evt.NewValue;
                PhoneCreateControllCommand cmd = new PhoneCreateControllCommand();
                cmd.Position = str.Position;
                _commandService.GetCommandStack().Execute(cmd);
            }
        }
    }
}

