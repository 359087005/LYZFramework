/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：TriggerWaterLamp
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：流水灯控制触发器
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Buskit3D.Training.IoC.C
{
    /// <summary>
    /// 流水灯控制触发器
    /// </summary>
    public class TriggerDancer : CommonMonoBehavior
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Start()
        {
            base.Start();
        }

        bool started = false;
        /// <summary>
        /// 首次执行Update时刻全部开始跳舞
        /// </summary>
        private void Update()
        {
            if (started)
            {
                return;
            }

            dancers.Foreach(UpdateDancerStatus);
            started = true;
        }

        /// <summary>
        /// 设置所有灯的关注对象为keyMan
        /// </summary>
        /// <param id="dm"></param>
        void UpdateDancerStatus(int i,DancerDataModel dm)
        {
            DancerEntity entity = utilsEntity.
                GetEntity<DancerEntity>(dm.gameObject);
            entity.isDancing = true;
        }
    }
}


