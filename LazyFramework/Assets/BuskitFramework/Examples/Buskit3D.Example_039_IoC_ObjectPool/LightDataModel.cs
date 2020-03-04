/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：LightDataModel
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：灯数据模型
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Training.IoC.C
{
    /// <summary>
    /// 正方体的数据模型
    /// </summary>
    public class LightDataModel : DataModelBehaviour
    {
        /// <summary>
        /// 初始化实体对象
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new LightEntity();
            this.Watch(this);
        }

        /// <summary>
        /// 注册本对象到对象池中
        /// </summary>
        protected override void Start()
        {
            base.Start();

            //注册本对象到对象池中
            ObjectPool<LightDataModel> pool = 
                InjectService.Get<ObjectPool<LightDataModel>>();
            pool.RegisterObject(this);
        }

        /// <summary>
        /// 在对象池中删除本对象
        /// </summary>
        private void OnDestroy()
        {
            //在对象池中删除本对象
            ObjectPool<LightDataModel> pool =
                InjectService.Get<ObjectPool<LightDataModel>>();
            pool.RemoveObject(this);
        }
    }
}

