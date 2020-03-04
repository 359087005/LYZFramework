/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CommonDataModel
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：处理豆苗部分显示业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Training.IoC.E
{
    /// <summary>
    /// 处理豆苗部分显示业务逻辑
    /// </summary>
    public class ShowPanelLogic : CommonLogic
    {
        /// <summary>
        /// 处理豆苗部分显示业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("hideAllDouGan"))
            {
                DouGanDataModel[] dms = dataModels.FindObjects<DouGanDataModel>();
                bool hideAllDouGan = (bool)evt.NewValue;
                foreach(DouGanDataModel dm in dms)
                {
                    DouGanEntity entity = utilsEntity.GetEntity<DouGanEntity>(dm.gameObject);
                    entity.visible = !hideAllDouGan;
                }
                return;
            }

            if (evt.PropertyName.Equals("hideAllDouYa"))
            {
                bool hideAllDouYa = (bool)evt.NewValue;
                DouYaDataModel[] dms = dataModels.FindObjects<DouYaDataModel>();
                foreach (DouYaDataModel dm in dms)
                {
                    DouYaEntity entity = utilsEntity.GetEntity<DouYaEntity>(dm.gameObject);
                    entity.visible = !hideAllDouYa;
                }
                return;
            }

            if (evt.PropertyName.Equals("hideAllDouBai"))
            {
                bool hideAllDouBai = (bool)evt.NewValue;
                DouBaiDataModel[] dms = dataModels.FindObjects<DouBaiDataModel>();
                foreach (DouBaiDataModel dm in dms)
                {
                    DouBaiEntity entity = utilsEntity.GetEntity<DouBaiEntity>(dm.gameObject);
                    entity.visible = !hideAllDouBai;
                }
                return;
            }

            if (evt.PropertyName.Equals("hideAllDouKe"))
            {
                bool hideAllDouKe = (bool)evt.NewValue;
                DouKeDataModel[] dms = dataModels.FindObjects<DouKeDataModel>();
                foreach (DouKeDataModel dm in dms)
                {
                    DouKeEntity entity = utilsEntity.GetEntity<DouKeEntity>(dm.gameObject);
                    entity.visible = !hideAllDouKe;
                }
                return;
            }

            if (evt.PropertyName.Equals("hideAllYeZi"))
            {
                bool hideAllYeZi = (bool)evt.NewValue;
                YeZiDataModel[] dms = dataModels.FindObjects<YeZiDataModel>();
                foreach (YeZiDataModel dm in dms)
                {
                    YeZiEntity entity = utilsEntity.GetEntity<YeZiEntity>(dm.gameObject);
                    entity.visible = !hideAllYeZi;
                }
                return;
            }
        }
    }
}
