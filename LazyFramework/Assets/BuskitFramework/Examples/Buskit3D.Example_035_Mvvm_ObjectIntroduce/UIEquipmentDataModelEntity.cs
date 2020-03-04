/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：UIEquipmentDataModelEntity
* 创建日期：2019-04-1 09:36:53
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

//includes for Unity

//includes for System
using System.Collections;
using Com.Rainier.Buskit3D;

/// <summary>
/// 名称空间定义：SHHY.Otoliths
/// </summary>
namespace Buskit3D.Example_039_Mvvm_ObjectIntroduce
{
    /// <summary>
    /// 类 名 称：Buskit3D.Example_039_Mvvm_ObjectIntroduce.UIEquipmentDataModelEntity
    /// 类 功 能：
    /// 主要接口：
    /// </summary>
    public class UIEquipmentDataModelEntity : BaseDataModelEntity
	{
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClose = false;
        /// <summary>
        /// mvvm按钮控制
        /// </summary>
        public int IsChangeToLast = 0;
        /// <summary>
        /// mvvm按钮控制
        /// </summary>
        public int IsChangeToNext = 0;
        /// <summary>
        /// 当前的设备ID
        /// </summary>
        public int currentEquipID = 0;
        /// <summary>
        /// 标题
        /// </summary>
        public string title = "设备介绍";
        /// <summary>
        /// 内容
        /// </summary>
        public string content = "研磨机是用涂上或嵌入磨料的研具对工件表面进行研磨的磨床。主要用于研磨工件中的高精度平面、内外圆柱面、圆锥面、球面、螺纹面和其他型面。研磨机的主要类型有圆盘式研磨机、转轴式研磨机和各种专用研磨机。研磨机控制系统以PLC为控制核心，文本显示器为人机对话界面的控制方式。人机对话界面可以就设备维护、运行、故障等信息与人对话；操作界面直观方便、程序控制、操作简单。全方位安全考虑，非正常状态的误操作无效。实时监控，故障、错误报警，维护方便。";
    }
}

