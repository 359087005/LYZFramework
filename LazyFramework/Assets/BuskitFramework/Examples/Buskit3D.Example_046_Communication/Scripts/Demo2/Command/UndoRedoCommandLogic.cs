/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UndoRedoCommandLogic
* 创建日期：2019-04-10 11:31:10
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
	public class UndoRedoCommandLogic : LogicBehaviour 
	{
        /// <summary>
        /// 注入命令系统
        /// </summary>
        [Inject]
        IServiceCommand _CommandService;

        /// <summary>
        /// 执行注入
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
            // 执行Undo操作
            if (evt.PropertyName.Equals("UndoMessage"))
            {
                _CommandService.GetCommandStack().Undo();
                return;
            }
            //执行Redo操作
            if (evt.PropertyName.Equals("RedoMessage"))
            {
                _CommandService.GetCommandStack().Redo();
                return;
            }
        }
    }
}

