/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：Menu_DropdownMenu
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：下拉框式菜单  UI面板逻辑
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    /// <summary>
    /// 下拉框式菜单
    /// </summary>
    public class Menu_DropdownMenu : MenuPanelBase
    {
        public GameObject panel_Menu;
        public GameObject btn_Temp;
        public float openSpeed=10;
        [HideInInspector] public List<GameObject> curMenu = new List<GameObject>();

        public override void LoadFuncUI(object sender, List<MenuFunctionBase> func)
        {
            InitMenuFunc();
            for (int i = 0; i < func.Count; i++)
            {
                AddFunc(sender, func[i]);
            }
            OpenMenu_Lerp(panel_Menu.transform);
        }
        private void AddFunc(object sender, MenuFunctionBase func)
        {
            curMenu.Add(Instantiate(btn_Temp));
            curMenu[curMenu.Count - 1].GetComponent<Button>().onClick.AddListener(() => 
            {
                panel_Menu.SetActive(false);
                EventManager.SendMessage(EventTopic.FUNC_MENU, sender,"OnFunctionStart", func.funcName);
            });
            curMenu[curMenu.Count - 1].GetComponent<Button>().interactable = func.isActive;
            curMenu[curMenu.Count - 1].transform.parent = panel_Menu.transform;
            curMenu[curMenu.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            curMenu[curMenu.Count - 1].transform.GetChild(0).GetComponent<Text>().text = func.funcName;
            curMenu[curMenu.Count - 1].gameObject.SetActive(true);
        }
        private void OpenMenu_Lerp(Transform trans)
        {
            StartCoroutine(OpenMenu_LerpIE(trans));
        }
        IEnumerator OpenMenu_LerpIE(Transform trans)
        {
            bool isOpen = true;
            trans.localScale = Vector3.zero;
            while (isOpen)
            {
                trans.localScale = Vector3.Lerp(trans.localScale, new Vector3(1, 1, 1), Time.deltaTime* openSpeed);
                if(Vector3.Distance(trans.localScale,new Vector3(1,1,1))<=0.1f)
                {
                    trans.localScale = new Vector3(1, 1, 1);
                    isOpen = false;
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        private void InitMenuFunc()
        {
            isShow = true;
            StartListen();
            panel_Menu.SetActive(true);
            panel_Menu.GetComponent<RectTransform>().position = MenuOffsetPosition();
            btn_Temp.SetActive(false);
            for (int i = 0; i < curMenu.Count; i++)
            {
                Destroy(curMenu[i]);
            }
            curMenu.Clear();
        }
        private void StartListen()
        {
            StartCoroutine(ListenCancel());
        }
        IEnumerator ListenCancel()
        {
            while (isShow)
            {
                yield return new WaitForFixedUpdate();
                if (Input.GetMouseButtonDown(1))
                {
                    isShow = false;
                    panel_Menu.SetActive(false);
                }
            }
        }
        private Vector2 MenuOffsetPosition()
        {
            return new Vector2(Input.mousePosition.x + btn_Temp.GetComponent<RectTransform>().sizeDelta.x / 2, Input.mousePosition.y - btn_Temp.GetComponent<RectTransform>().sizeDelta.y / 2);
        }
    }
}
