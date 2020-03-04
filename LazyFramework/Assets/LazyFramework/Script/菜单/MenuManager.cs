/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：MenuManager
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：菜单管理器
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    public class MenuManager : MonoSingleton<MenuManager>
    {
        public bool isOpen;
        [SerializeField] List<MenuFunctionBase> menuBases = new List<MenuFunctionBase>();
        [SerializeField] MenuPanelBase panelBase;
        [SerializeField] GameObject curGameObject;
        public List<MenuFunctionBase> MenuBases
        {
            get
            {
                return menuBases;
            }
        }
        private void Start()
        {
            Init();
        }
        private void Init()
        {
            EventManager.AddEvent(EventTopic.FUNC_MENU, OnClickFunc);
            if(panelBase==null)
            {
                panelBase = FindObjectOfType<MenuPanelBase>();
                if (panelBase == null)
                    Debug.LogError("未找到菜单面板，或未挂载菜单面板脚本");
            }
        }
        private void OnClickFunc(object sender, object[] message)
        {
            MenuBases.Clear();
            if (sender is Menu_Interactive)
            {
                if(((GameObject)message[0]).GetComponent<MenuFunctionBase>() != null)
                {
                    int count = ((GameObject)message[0]).GetComponents<MenuFunctionBase>().Length;
                    for (int i = 0; i < count; i++)
                    {
                        MenuBases.Add(((GameObject)message[0]).GetComponents<MenuFunctionBase>()[i]);
                    }
                    panelBase.LoadFuncUI(((Menu_Interactive)sender).gameObject , MenuBases);
                }
            }
        }
    }
}
