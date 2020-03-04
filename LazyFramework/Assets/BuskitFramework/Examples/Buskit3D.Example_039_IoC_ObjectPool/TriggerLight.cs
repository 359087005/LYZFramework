/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：TriggerColor
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：灯光颜色控制触发器
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Buskit3D.Training.IoC.C
{
    /// <summary>
    /// 灯光颜色控制触发器
    /// </summary>
    public class TriggerLight : CommonMonoBehavior
    {
        /// <summary>
        /// 计时器
        /// </summary>
        public float attackTimer;

        /// <summary>
        /// 时间
        /// </summary>
        public float attackTime;

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Start()
        {
            base.Start();
            attackTimer = 0;
            attackTime = 0.1f;
        }

        /// <summary>
        /// 每隔两秒执行一次
        /// </summary>
        void Update()
        {
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
            if (attackTimer == 0)
            {
                Attack();
                attackTimer = attackTime;
            }
        }

        /// <summary>
        /// 执行更新
        /// </summary>
        public void Attack()
        {
            lights.Foreach(OnLight);
        }

        /// <summary>
        /// 遍历时修改灯的颜色
        /// </summary>
        /// <param id="dm"></param>
        void OnLight(int i,LightDataModel dm)
        {
            LightEntity entity = utilsEntity.
                GetEntity<LightEntity>(dm.gameObject);

            entity.localPositon = 
                new Vector3(Random.Range(-5f, 5f), 5, Random.Range(-5f, 5f));
        }
    }
}


