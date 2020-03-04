/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：PrefabLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 功能描述： Websocket测试
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using UnityWebSocket;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


namespace Buskit3D.Example_022_WebSocket
{
    /// <summary>
    /// 使用UI测试Websocket
    /// </summary>
    public class TestWebSocket : MonoBehaviour
    {
        public Button newSocketBtn;
        public Button connentBtn;
        public Button closeBtn;
        public Button sendBtn;
        public Text contentText;
        public InputField addressInput;
        public InputField messageInput;
        public Transform entryRoot;
        public Button entryTemplate;
        public Image currentSelectBg;
        public Text currentSelectText;

        public GameObject messageBoxObj;
        public Text messagexBoxText;
        public Button messageBoxCloseBtn;

        private Dictionary<string, WebSocketEntry> webSocketEntityDic = new Dictionary<string, WebSocketEntry>();
        private WebSocketEntry webSocketEntry;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            if (newSocketBtn!=null)
            newSocketBtn.onClick.AddListener(NewSocket);
            connentBtn.onClick.AddListener(Connect);
            closeBtn.onClick.AddListener(Close);
            sendBtn.onClick.AddListener(Send);
            messageBoxCloseBtn.onClick.AddListener(OnClickCloseMessageBox);
            entryTemplate.gameObject.SetActive(false);
            messageBoxObj.gameObject.SetActive(false);
        }

        /// <summary>
        /// 创建新的socket实例
        /// </summary>
        public void NewSocket()
        {
            string addr = addressInput.text;
            if (webSocketEntityDic.ContainsKey(addr))
            {
                MessageBox("Duplicate address " + addr);
                return;
            }

            WebSocketEntry entry = new WebSocketEntry(addr);
            webSocketEntityDic.Add(addr, entry);

            Button entryItem = GameObject.Instantiate(entryTemplate);
            entryItem.GetComponentInChildren<Text>().text = addr;
            entryItem.gameObject.SetActive(true);
            entryItem.transform.SetParent(entryRoot);
            entryItem.transform.localScale = Vector3.one;
            entryItem.transform.localRotation = Quaternion.identity;
            entryItem.onClick.AddListener(() => { OnEntryItemClick(entry); });

            if (webSocketEntry == null)
                webSocketEntry = entry;
        }

        /// <summary>
        /// 确定所选择的websocket实例
        /// </summary>
        /// <param name="entry"></param>
        private void OnEntryItemClick(WebSocketEntry entry)
        {
            webSocketEntry = entry;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect()
        {
            if (webSocketEntry == null)
                return;

            webSocketEntry.Connect();
        }
        
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (webSocketEntry == null)
                return;
            webSocketEntry.Close();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void Send()
        {
            if (webSocketEntry == null)
                return;
            webSocketEntry.Send(messageInput.text);
        }

        /// <summary>
        /// 提示框
        /// </summary>
        /// <param name="log"></param>
        public void MessageBox(string log)
        {
            messageBoxObj.SetActive(true);
            messagexBoxText.text = log;
        }

        /// <summary>
        /// 关闭提示框
        /// </summary>
        private void OnClickCloseMessageBox()
        {
            messageBoxObj.SetActive(false);
        }

        /// <summary>
        /// UnityMethod
        /// </summary>
        void Update()
        {
            var text = "";
            var addr = "请选择服务器地址";
            var state = WebSocketState.Closed;

            if (webSocketEntry != null)
            {
                text = webSocketEntry.content;
                state = webSocketEntry.socket.readyState;
                addr = webSocketEntry.socket.address;
            }
            contentText.text = text;
            currentSelectText.text = addr;
            currentSelectBg.color = GetStateColor(state);
        }


        private Color GetStateColor(WebSocketState state)
        {
            switch (state)
            {
                case WebSocketState.Closed:
                    return Color.grey;
                case WebSocketState.Closing:
                    return Color.cyan;
                case WebSocketState.Connecting:
                    return Color.yellow;
                case WebSocketState.Open:
                    return Color.green;
            }
            return Color.white;
        } 


        /// <summary>
        /// Websocket实例封装
        /// </summary>
        class WebSocketEntry
        {
            //Websocket实例
            public WebSocket socket { get; private set; }
            //传输内容
            public string content { get; private set; }

            public WebSocketEntry(string address)
            {
                socket = new WebSocket(address);
                socket.onOpen += OnOpen;
                socket.onClose += OnClose;
                socket.onMessage += OnReceive;
                socket.onError += OnError;
            }

            /// <summary>
            /// 连接
            /// </summary>
            public void Connect()
            {
                if (socket == null
                    || socket.readyState == WebSocketState.Open
                    || socket.readyState == WebSocketState.Closing)
                    return;

                socket.Connect();
            }

            /// <summary>
            /// 关闭
            /// </summary>
            public void Close()
            {
                if (socket.readyState == WebSocketState.Connecting
                    || socket.readyState == WebSocketState.Open)
                {
                    socket.Close();
                }
            }

            /// <summary>
            /// 发送字符串
            /// </summary>
            /// <param name="text"></param>
            public void Send(string text)
            {
                if (socket.readyState == WebSocketState.Open)
                {
                    socket.Send(text);
                    content += "[SEND] " + text + "\n";
                }
            }

            /// <summary>
            /// 连接成功回调
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnOpen(object sender, EventArgs e)
            {
                content += "[INFO] Connected\n";
            }

            /// <summary>
            /// 关闭回调
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnClose(object sender, CloseEventArgs e)
            {
                content += "[INFO] Closed\n";
            }

            /// <summary>
            /// 接收消息回调
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnReceive(object sender, MessageEventArgs e)
            {
                content += "[RECEIVE] " + e.Data + "\n";
            }

            /// <summary>
            /// 异常回调
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnError(object sender, ErrorEventArgs e)
            {
                content += "[ERROR] " + e.Message + "\n";
            }
        }
    }
}



