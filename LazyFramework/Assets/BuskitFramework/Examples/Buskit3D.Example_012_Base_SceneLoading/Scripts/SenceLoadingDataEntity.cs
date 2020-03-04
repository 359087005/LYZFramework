/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LoadingDataEntity
* 创建日期：2019-01-09 13:52:41
* 作者名称： 黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    场景过渡数据实体
******************************************************************************/
namespace Com.Rainier.Buskit3D.Example_012
{
    /// <summary>
    /// 数据实体类
    /// </summary>
    public class SenceLoadingDataEntity : BaseDataModelEntity 
	{
        // 加载目标场景name   
        public string TargetSenceName = "";
        //控制显示LoadingUI
        public bool IsShowLoadingUI = false;
    }
}

