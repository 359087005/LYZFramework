/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： DebugGUI
* 创建日期：2019-06-27 15:01:45
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：1.用于发布后调试。
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Lazy
{
    public class DebugGUI : MonoSingleton<DebugGUI>
    {
        #region 日志功能
        private bool showDebugGUI = false;
        private bool ShowDebugGUI
        {
            get
            {
                return showDebugGUI;
            }
            set
            {
                showDebugGUI = value;
                if(value)
                {
                    ResetSize();
                }
            }
        }
        private bool canReceiveMsg = true;
        private double lastInterval = 0.0;
        private int frames = 0;
        private float frameInterval = 0.5f;
        private string fps;
        private string curTime;
        private string CurTime
        {
            get
            {
                return "   "+System.DateTime.Now.ToString();
            }
        }
        private string runTime;
        private string RunTime
        {
            get
            {
                return curTime = "已运行：" + Time.time.ToString("f0")+"s";
            }
        }
        private string strBaseInfo;
        private string StrBaseInfo
        {
            get
            {
                return fps + "   "+ RunTime + "   "+ CurTime;
            }
        }
        private List<LogInfo> logs = new List<LogInfo>();
        readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            { LogType.Log, Color.white },
            { LogType.Assert, new Color(0.9f,0.9f,0.9f,1)},
            { LogType.Warning, Color.yellow },
            { LogType.Error, Color.red },
            { LogType.Exception, new Color(0.7f,0.13f,0.13f,1) },
           
        };
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            lastInterval = Time.realtimeSinceStartup;
            ResetSize();
        }
        void Update()
        {
            ShowFPS();
            CallGUI();
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("log");
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.LogAssertion("assert");
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.LogWarning("warning");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.LogError("error");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.LogException(new Exception());
            }
        }
        private void ReceiveMsg(string msg, string stacktrace, LogType type)
        {
            if (canReceiveMsg)
            {
                logs.Add(new LogInfo(msg, stacktrace, type));
                scrollPosition.y = float.MaxValue;
            }
        }
        void OnEnable()
        {
            Application.logMessageReceived += ReceiveMsg;
        }
        void OnDisable()
        {
            Application.logMessageReceived -= ReceiveMsg;
        }
        #endregion
        #region GUI
        private float windowScalingX = 1;
        private float windowScalingY = 1;
        private int fontSize = 15;
        private CurPanel curPanel;
        private Rect window ;
        private bool[] logFilter = new bool[5] { true, true, true,true,true };
        private Vector2 windowSize;
        private Vector2 scrollPosition = Vector2.zero;
        void OnGUI()
        {
            MyWindow();
        }
        private void ResetSize()
        {
            windowSize = new Vector2(Screen.width / 2*windowScalingX, Screen.height / 2* windowScalingY)   ;
            window = new Rect(Vector2.zero, windowSize);
        }
      
        private void CallGUI()
        {
            if(Input.GetKeyDown(KeyCode.LeftAlt))
            {
                StartCoroutine(WaitListen());
            }
        }
        IEnumerator WaitListen()
        {
            float time = 0;
            while (true)
            {
                time += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.B))
                {
                    ShowDebugGUI = !ShowDebugGUI;
                    break;
                }
                if (time > 1)
                {
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        private void ButtonFunc()
        {
            GUILayout.BeginHorizontal();
            switch(curPanel)
            {
                case CurPanel.Debug:
                    if (canReceiveMsg)
                    {
                        if (GUILayout.Button("接收", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                        {
                            canReceiveMsg = false;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("拒收", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                        {
                            canReceiveMsg = true;
                        }
                    }
                    if (GUILayout.Button("清空", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        logs.Clear();
                    }
                    logFilter[0] = GUI.Toggle(new Rect(20 + (windowSize.x / 8) * 0, windowSize.y / 10 + 20, windowSize.x / 8, windowSize.y / 12), logFilter[0], "Debug");
                    logFilter[1] = GUI.Toggle(new Rect(20 + (windowSize.x / 8) * 1, windowSize.y / 10 + 20, windowSize.x / 8, windowSize.y / 12), logFilter[1], "Assert");
                    logFilter[2] = GUI.Toggle(new Rect(20 + (windowSize.x / 8) * 2, windowSize.y / 10 + 20, windowSize.x / 8, windowSize.y / 12), logFilter[2], "Warning");
                    logFilter[3] = GUI.Toggle(new Rect(20 + (windowSize.x / 8) * 3, windowSize.y / 10 + 20, windowSize.x / 8, windowSize.y / 12), logFilter[3], "Error");
                    logFilter[4] = GUI.Toggle(new Rect(20 + (windowSize.x / 8) * 4, windowSize.y / 10 + 20, windowSize.x / 8, windowSize.y / 12), logFilter[4], "Exception");
                    if (GUILayout.Button("命令", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        curPanel = CurPanel.Command;
                    }
                    if (GUILayout.Button("设置", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        curPanel = CurPanel.Set;
                    }
                    break;
                case CurPanel.Command:
                    if (GUILayout.Button("控制台", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        curPanel = CurPanel.Debug;
                    }
                    if (GUILayout.Button("设置", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        curPanel = CurPanel.Set;
                    }
                    break;
                case CurPanel.Set:
                    if (GUILayout.Button("控制台", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        curPanel = CurPanel.Debug;
                    }
                    if (GUILayout.Button("命令", GUILayout.Width(windowSize.x / 8), GUILayout.Height(windowSize.y / 12)))
                    {
                        curPanel = CurPanel.Command;
                    }
                    break;
                   
            }
            GUILayout.EndHorizontal();
        }
        private void WindowFunction(int id)
        {
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.button.fontSize = fontSize;
            GUI.skin.toggle.fontSize = fontSize;
            GUI.skin.button.margin = new RectOffset(20, 5, 5, 5);
            GUI.skin.toggle.padding = new RectOffset(20, 0, 0, 0);
            ButtonFunc();

            switch (curPanel)
            {
                case CurPanel.Debug:
                    Window_Debug();
                    break;
                case CurPanel.Command:
                    Window_Commond();
                    break;
                case CurPanel.Set:
                    Window_Set();
                    break;
                default:
                    break;
            }
            GUI.DragWindow(new Rect(0, 0, windowSize.x, 30));
        }
        private void Window_Set()
        {
            GUILayout.Label("窗口宽度");
            windowScalingX = GUILayout.VerticalSlider(windowScalingX, 1, 2);
            GUILayout.Label("窗口高度");
            windowScalingY = GUILayout.HorizontalSlider(windowScalingY, 1, 2);
            GUILayout.Label("字号");
            fontSize = (int)GUILayout.HorizontalSlider(fontSize, 15, 25);
            ResetSize();
        }
        private void Window_Commond()
        {
            GUILayout.Label(StrBaseInfo);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(window.width - 20), GUILayout.Height(window.height * 0.8f - 100));
            LogFilter();
            GUILayout.EndScrollView();
            commandText = GUILayout.TextField(commandText);
            if (GUILayout.Button("确认"))
            {
                if (evtDebugCommand != null)
                    evtDebugCommand.Invoke(commandText);
            }
            GUI.contentColor = Color.white;
        }
        private void Window_Debug()
        {
            GUILayout.Label("");
            GUILayout.Label(StrBaseInfo);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(window.width - 20), GUILayout.Height(window.height*0.8f-50));
            LogFilter();
            GUILayout.EndScrollView();

            GUI.contentColor = Color.white;
        }
        private void LogFilter()
        {
            foreach (LogInfo log in logs)
            {
                GUI.contentColor = logTypeColors[log.type];

                if (logFilter[0] && log.type == LogType.Log)
                {
                    GUILayout.Label("消息:" + log.message + "\n" + log.stacktrace);
                    continue;
                }
                if (logFilter[1] && log.type == LogType.Assert)
                {
                    GUILayout.Label("消息:" + log.message + "\n" + log.stacktrace);
                    continue;
                }
                if (logFilter[2] && log.type == LogType.Warning)
                {
                    GUILayout.Label("消息:" + log.message + "\n" + log.stacktrace);
                    continue;
                }
                if (logFilter[3] && log.type == LogType.Error)
                {
                    GUILayout.Label("消息:" + log.message + "\n" + log.stacktrace);
                    continue;
                }
                if (logFilter[4] && log.type == LogType.Exception)
                {
                    GUILayout.Label("消息:" + log.message + "\n" + log.stacktrace);
                    continue;
                }
            }
        }
        private void MyWindow()
        {
            if (showDebugGUI)
                window = GUI.Window(0, new Rect(window), WindowFunction, "Debug");
        }
        #endregion
        #region 命令面板
        public delegate void DebugCommandEventHandler(string strCommand);
        public event DebugCommandEventHandler evtDebugCommand;
        string commandText = "";
        #endregion
        #region 内部方法
        private void ShowFPS()
        {
            ++frames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow - lastInterval > frameInterval)
            {
                float curFps = frames / (float)(timeNow - lastInterval);
                float ms = 1000.0f / Mathf.Max(curFps, 0.1f);
                fps = string.Format("帧率:" + curFps.ToString("f0"));
                frames = 0;
                lastInterval = timeNow;
            }
        }
        #endregion
        #region 数据结构
        public class LogInfo
        {
            public string message;
            public string stacktrace;
            public LogType type;
            public LogInfo(string msg, string stacktrace, LogType type)
            {
                this.message = msg;
                this.stacktrace = stacktrace;
                this.type = type;
            }
        }
        enum CurPanel
        {
            Debug,
            Command,
            Set
        }
        #endregion
    }
}
