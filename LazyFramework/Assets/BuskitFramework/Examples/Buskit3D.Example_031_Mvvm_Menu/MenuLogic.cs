/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuLogic
* 创建日期：2019-03-19 09:30:02
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_35_Mvvm_Menu
{
    /// <summary>
    /// 菜单UI业务逻辑类
    /// </summary>
    public class MenuLogic : LogicBehaviour
    {
        /// <summary>
        /// 实体数据对象
        /// </summary>
        private MenuEntity entity;
        public GameObject MenuItem1;
        public GameObject MenuItem2;
        public GameObject MenuItem3;

        private void Start()
        {
            entity =(MenuEntity)GetComponent<MenuDataModel>().DataEntity;
        }
        public override void ProcessLogic(PropertyMessage evt)
        {          
            switch (evt.PropertyName)
            {
                //打开/关闭左侧菜单控制
                case ("openLight"):
                    MenuItem1.SetActive((bool)evt.NewValue);
                    break;
                case ("openSchemeDesign"):
                    MenuItem2.SetActive((bool)evt.NewValue);
                    break;
                case ("openLightDesign"):
                    MenuItem3.SetActive((bool)evt.NewValue);
                    break;
                //菜单按钮控制
                case ("lightAnalysisBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "舞台灯具解析菜单";
                    }
                    break;
                case ("lightMessageBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "灯具信息浏览菜单";
                    }
                    break;
                case ("allDesignBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "总体设计说明菜单";
                    }
                    break;
                case ("basicBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "基本情况分析菜单";
                    }
                    break;
                case ("spaceDesignBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "空间设计解析菜单";
                    }
                    break;
                case ("faceDesignBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "面光系统设计菜单";
                    }
                    break;
                case ("earDesignBtn"):
                    if ((int)evt.NewValue > 0)
                    {
                        entity.showText = "耳光系统设计菜单";
                    }
                    break;
            }         
        }
    }
}

