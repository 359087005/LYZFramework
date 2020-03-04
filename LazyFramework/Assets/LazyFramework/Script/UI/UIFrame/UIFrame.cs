/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MDUIFrame
* 创建日期：2019-12-12 14:43:39
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：{林奕州:2020/3/3 1.打开栈顶已关闭的面板 2.全屏窗口模式切换、全屏按钮的平台处理}
* 描述：ui框架,可以打开新面板,关闭之前面板,返回上一面板,返回主页,全屏的功能
*
******************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Lazy
{
    public class UIFrame : MonoSingleton<UIFrame>, IProcessEvent
    {
        [SerializeField] UIPanelBase[] test;
        [Header("测试模式（不展示起始UI面板）")]
        public bool testMode = false;
        [HideInInspector]public bool canClose = false;
        private bool goBackIsClose = false;
        public GameObject UIPanels;
        public bool GoBackIsClose
        {
            set
            {
                goBackIsClose = value;
                goBack_btn.GetComponent<Button>().onClick.RemoveAllListeners();
                if (value)
                {
                    goBack_btn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        HideOtherPanels();
                    });
                }
                else
                {
                    goBack_btn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        MessageCenter.SendEvent(new Message((ushort)CommonEvent.goBack));
                    });
                }
            }
        }

        public GameObject goBack_btn, fullScreen_btn, windowScreen_btn, homePage_btn , close_Btn;
        /// <summary>
        /// 控制面板层级的堆栈
        /// </summary>
        public Stack<UIPanelBase> uiStack = new Stack<UIPanelBase>();
        private static UIFrame instance;
        public UIPanelBase firstPanel;
        #region unity life
        private void Awake()
        {
            instance = this;
            UIPanels = transform.Find("UIPanels").gameObject;
        }
        private void Update()
        {
            test = uiStack.ToArray();
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            this.Register((ushort)CommonEvent.goBack);
            this.Register((ushort)CommonEvent.homePage);
            this.Register((ushort)CommonEvent.fullScreen);
            ScreenFunc();
            if(close_Btn!=null)
            {
                close_Btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    goBack_btn.SetActive(false);
                    close_Btn.SetActive(false);
                    UIPanels.gameObject.SetActive(false);
                    HideOtherPanels();
                });
            }
            if (goBack_btn!=null)
            {
                goBack_btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    MessageCenter.SendEvent(new Message((ushort)CommonEvent.goBack));
                });
                //GoBackIsClose = false;
            }
            if(homePage_btn!=null)
            {
                homePage_btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    goBack_btn.SetActive(true);
                    if(canClose)
                    {
                        close_Btn.SetActive(true);
                    }
                    MessageCenter.SendEvent(new Message((ushort)CommonEvent.homePage, "主菜单"));
                });
            }
        }
#endregion

#region 行为

        public void ClearAll()
        {
            uiStack.Clear();
            for (int i = 0; i < UIPanels.transform.childCount; i++)
            {
                UIPanels.transform.GetChild(i).GetComponent<UIPanelBase>().OnExit();
                UIPanels.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 加载面板
        /// </summary>
        /// <param name="panel"></param>
        public void Open(UIPanelBase panel)
        {
            UIPanels.SetActive(true);
            //如果已经打开,就跳回此面板
            if (uiStack.Contains(panel))
            {
                GoPanel(panel);
                return;
            }
            //暂停上一层面板,停止其交互
            if (uiStack.Count > 0)
            {
                UIPanelBase uIPanelBase1 = uiStack.Peek();
                uIPanelBase1.OnPause();
                //var cg1 = GetCanvasGroup(uIPanelBase1);
                //cg1.interactable = false;
            }
            //如果隐藏就激活
            if (!panel.gameObject.activeSelf)
            {
                panel.gameObject.SetActive(true);
            }
            //进入目标面板
            panel.OnEnter();
            //CanvasGroup cg = GetCanvasGroup(panel);

            switch (panel.showType)
            {
                case UIPanelBase.ShowType.normal:
                    break;
                case UIPanelBase.ShowType.hideOtherPanels:

                    HideOtherPanels();
                    break;
                default:
                    break;

            }
            //入栈
            if (uiStack.Count == 0 || !uiStack.Peek().Equals(panel))
            {
                print("入栈" + panel.name);
                uiStack.Push(panel);

            }
            //显示并启用当前面板的交互
            //cg.blocksRaycasts = true;
            OpenByEffect(panel);
            //放在屏幕最前端
            panel.gameObject.transform.SetAsLastSibling();
        }
        public void ClosePanel(UIPanelBase uIPanelBase)
        {
            if (uIPanelBase.IsActive)
            {
                CloseByEffect(uIPanelBase);
            }
        }
        /// <summary>
        /// 隐藏所有之前的面板
        /// </summary>
        public void HideOtherPanels()
        {
            UIPanelBase[] uIPanelBase = uiStack.ToArray();
            var exitedPanels = new List<UIPanelBase>();
            for (int i = 0; i < uIPanelBase.Length; i++)
            {
                uIPanelBase[i].OnExit();
                if (uIPanelBase[i].IsActive)
                {
                    CloseByEffect(uIPanelBase[i]);
                }
                if (!exitedPanels.Contains(uIPanelBase[i]))
                {
                    exitedPanels.Add(uIPanelBase[i]);
                }
            }
        }


        /// <summary>
        /// 返回上一面板
        /// </summary>
        public void GoBack()
        {
            //关闭最顶层面板
            if (uiStack.Count > 1)
            {
              
                UIPanelBase uIPanelBase = uiStack.Pop();
               //林奕州2020/3/3修改：限制返回到起始页
                if (uiStack.Peek().gameObject.name == firstPanel.name + "(Clone)")
                {
                    uiStack.Push(uIPanelBase);
                    Debug.Log("已经返回到底部");
                    return;
                }
                uIPanelBase.OnExit();
                CloseByEffect(uIPanelBase);
                //恢复显示下一层面板
                if (uiStack.Count > 0)
                {
                    UIPanelBase uIPanelBase1 = uiStack.Peek();
                    uIPanelBase1.OnResume();
                    uIPanelBase1.transform.SetAsLastSibling();
                    OpenByEffect(uIPanelBase1);
                    //var cg = GetCanvasGroup(uIPanelBase1);
                    //cg.interactable = true;
                }
            }

        }

        private void OpenByEffect(UIPanelBase uIPanelBase)
        {
            switch (uIPanelBase.effectType)
            {
                case UIPanelBase.EffectType.byCanvaGroup:
                    var cg = GetCanvasGroup(uIPanelBase);
                    cg.alpha = 1f;
                    cg.interactable = true;
                    break;
                case UIPanelBase.EffectType.active:
                    uIPanelBase.gameObject.SetActive(true);
                    break;
                case UIPanelBase.EffectType.scale:
                    uIPanelBase.transform.localScale = Vector3.zero;
                    uIPanelBase.transform.DOScale(1f, 1f);
                    break;

                default:
                    break;
            }
        }

        private void CloseByEffect(UIPanelBase uIPanelBase)
        {
            switch (uIPanelBase.effectType)
            {
                case UIPanelBase.EffectType.byCanvaGroup:
                    var cg = GetCanvasGroup(uIPanelBase);
                    cg.alpha = 0f;
                    break;
                case UIPanelBase.EffectType.active:
                    uIPanelBase.gameObject.SetActive(false);
                    break;
                case UIPanelBase.EffectType.scale:
                    uIPanelBase.transform.localScale = Vector3.one;
                    uIPanelBase.transform.DOScale(0f, 1f);

                    break;

                default:
                    break;
            }
        }



        /// <summary>
        /// 跳至面板,比如跳回主页
        /// </summary>
        /// <param name="targetPanel"></param>
        public void GoPanel(UIPanelBase targetPanel)
        {
            //林奕州增加：如果当前面板处于关闭状态则打开栈顶面板
            if(uiStack.Peek()==targetPanel&&(targetPanel.transform.localScale==Vector3.zero || !targetPanel.gameObject.activeSelf))
            {
                Debug.Log("打开栈顶面板");
                OpenByEffect(targetPanel);
            }

            while ((uiStack.Count > 1 && !uiStack.Peek().Equals(targetPanel)))
            {
                GoBack();
            }
        }


        /// <summary>
        /// 得到CanvasGroup
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private static CanvasGroup GetCanvasGroup(UIPanelBase panel)
        {
            var cg = panel.GetComponent<CanvasGroup>();
            if (!cg)
            {
                cg = panel.gameObject.AddComponent<CanvasGroup>();
            }
            return cg;
        }

#endregion
        public void ProcessEvent(Message msg)
        {
            if (msg.id == (ushort)CommonEvent.goBack)
            {
                GoBack();
            }
            if (msg.id == (ushort)CommonEvent.homePage)
            {
                Open("主菜单");
                //GoPanel((UIPanelBase)msg.arguments[0]);//mainMenu
                /* GoPanel("主菜单");*///mainMenul);

                //Open(mainPanel);
            }
            if (msg.id == (ushort)CommonEvent.fullScreen)
            {
                Screen.fullScreen = (bool)msg.arguments[0];
            };
        }

        /// <summary>
        /// 返回和全屏消息
        /// </summary>
        public enum CommonEvent
        {
            goBack = 1001,
            fullScreen = 1002,
            homePage = 1003,
            putDownItem = 1004,
        }

#if true
        //Resources.Load方式加载面板
        
        /// <summary>
        /// 面板预制件在resources下的路径
        /// </summary>
        public Dictionary<string, string> panelDic;

        /// <summary>
        /// 已经加载的面板
        /// </summary>
        Dictionary<string, UIPanelBase> alreadyLoadedPanelDic = new Dictionary<string, UIPanelBase>();

        /// <summary>
        /// 从Resources文件夹下加载面板
        /// </summary>
        /// <param name="panelName"></param>
        public void Open(string panelName)
        {
            //加载面板
            UIPanelBase panel;
            panel = GetPanel(panelName);
            Open(panel);
        }
        /// <summary>
        /// 跳至面板,比如跳回主页
        /// </summary>
        /// <param name="targetName"></param>
        public void GoPanel(string targetName)
        {
            //是否存在目标
            if (alreadyLoadedPanelDic.ContainsKey(targetName))
            {
             
                var targetPanel = alreadyLoadedPanelDic[targetName];
                GoPanel(targetPanel);
            }
        }
        /// <summary>
        /// 得到已经缓存的面板或加载面板
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        private UIPanelBase GetPanel(string panelName)
        {
            UIPanelBase panel;
            if (alreadyLoadedPanelDic.ContainsKey(panelName))
            {
                panel = alreadyLoadedPanelDic[panelName].GetComponent<UIPanelBase>();
            }
            else
            {
                //加载面板
                GameObject original = Resources.Load<GameObject>(panelDic[panelName]);
                if (original == null)
                {
                    throw new Exception("没找到" + panelName + "面板");
                }
                panel = Instantiate(original, UIPanels.transform).GetComponent<UIPanelBase>();
                alreadyLoadedPanelDic.Add(panelName, panel);
            }

            return panel;
        }
        private void ScreenFunc()
        {
#if UNITY_STANDALONE_WIN
            if (fullScreen_btn != null)
            {
                fullScreen_btn.SetActive(false);
                windowScreen_btn.SetActive(false);
                windowScreen_btn.transform.parent.gameObject.SetActive(false);
            }
#endif
#if UNITY_WEBGL
            if (fullScreen_btn != null)
                fullScreen_btn.SetClick(QuanPingScreen);
            if (windowScreen_btn != null)
                windowScreen_btn.SetClick(TuiChuQuanPingScreen);
            if (Screen.fullScreen)
            {
                fullScreen_btn.SetActive(false);
                windowScreen_btn.SetActive(true);
            }
            else
            {
                fullScreen_btn.SetActive(true);
                windowScreen_btn.SetActive(false);
            }
#endif
        }
        private void QuanPingScreen(GameObject go)
        {
            Resolution[] resolutions = Screen.resolutions;
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
            Screen.fullScreen = true;
            fullScreen_btn.SetActive(false);
            windowScreen_btn.SetActive(true);
        }
        private void TuiChuQuanPingScreen(GameObject go)
        {
            Screen.SetResolution(960, 540, false);
            Screen.fullScreen = false;
            fullScreen_btn.SetActive(true);
            windowScreen_btn.SetActive(false);
        }
#endif
    }

    public interface IProcessEvent
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="msg"></param>
        void ProcessEvent(Message msg);
    }

}

