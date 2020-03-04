/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：LightControlLogic
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：舞者控制业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Buskit3D.Training.IoC.C
{
    /// <summary>
    /// 舞者控制业务逻辑
    /// </summary>
    public class DancerControlLogic : CommonLogic
    {
        private void Update()
        {
            DancerEntity entity = utilsEntity.GetEntity<DancerEntity>(gameObject);
            if (entity.isDancing)
            {
                int speed = Random.Range(1, 3);
                transform.Rotate(Vector3.up * speed * Time.deltaTime, speed);
            }
        }
    }
}


