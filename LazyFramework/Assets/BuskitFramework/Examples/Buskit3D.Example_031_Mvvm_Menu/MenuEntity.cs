/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuEntity
* 创建日期：2019-03-19 09:30:20
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;
using System;
using UnityEngine;

namespace Buskit3D.Example_35_Mvvm_Menu
{
    public struct Test2
    {
       public int a;
    }

	/// <summary>
    ///菜单UI数据实体类
    /// </summary>
	public class MenuEntity : BaseDataModelEntity 
	{   
        /// <summary>
        /// 展开灯具认知(Toggle)
        /// </summary>
        [RestoreFireLogic]
        public bool openLight = true;

        //舞台灯具解析按钮
        [RestoreFireLogic]
        public int lightAnalysisBtn = 0;
        //灯具信息浏览按钮
        [RestoreFireLogic]
        public int lightMessageBtn = 0;

        /// <summary>
        /// 展开方案设计(Toggle)
        /// </summary>
        [RestoreFireLogic]
        public bool openSchemeDesign = true;

        //总体设计说明按钮
        [RestoreFireLogic]
        public int allDesignBtn = 0;
        //基本情况分析按钮
        [RestoreFireLogic]
        public int basicBtn = 0;
        //总体设计说明按钮
        [RestoreFireLogic]
        public int spaceDesignBtn = 0;

        /// <summary>
        /// 展开灯光设计(Toggle)
        /// </summary>
        [RestoreFireLogic]
        public bool openLightDesign = true;
        //面光系统设计按钮
        [RestoreFireLogic]
        public int faceDesignBtn = 0;
        //总体设计说明按钮
        [RestoreFireLogic]
        public int earDesignBtn = 0;

        /// <summary>
        /// 显示Text
        /// </summary>
        [RestoreFireLogic]
        public string showText = "";

    }
}

