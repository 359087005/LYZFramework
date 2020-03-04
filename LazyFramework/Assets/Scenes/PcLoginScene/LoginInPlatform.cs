
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LoginInPlatform
* 创建日期：2019-03-26 16:53:18
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using System.Collections.Generic;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// Pc版登录
    /// </summary>
	public class LoginInPlatform : MonoBehaviour 
	{

        /// <summary>
        /// 登录成功之后要跳转的场景
        /// </summary>
        public string loginSceneName="Buskit3D_Example_005_StorageSystem";

        /// <summary>
        /// 用户名输入框
        /// </summary>
        public InputField userInput;

        /// <summary>
        /// 密码输入框
        /// </summary>
        public InputField passInput;

        /// <summary>
        /// 登录按钮
        /// </summary>
        public Button loginBtn;

        /// <summary>
        /// 用户名
        /// </summary>
        private  string username;

        /// <summary>
        /// 密码
        /// </summary>
        private  string password;

        /// <summary>
        /// 平台接口
        /// </summary>
        [Inject]
        LabInterSystem labInterSystem;

        /// <summary>
        /// 序列化工具
        /// </summary>
        [Inject]
        private IServiceSerializer _serviceSerializer;

        /// <summary>
        /// 实验设置信息
        /// </summary>
        private  ExperimentInfoSettings _ExpInfoSettings;

        private void Awake()
        {
            //登录界面单独初始化
            LabInterSystem.Init();
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start () 
		{
            InjectService.InjectInto(this);
            //赋初值
            username = userInput.text;
            password = passInput.text;

            userInput.onValueChanged.AddListener(p => username = p);
            passInput.onValueChanged.AddListener(p => password = p);

            loginBtn.onClick.AddListener(LoginIn);

            _ExpInfoSettings = (ExperimentInfoSettings)Resources.Load("ExperimentInfoSettings", typeof(ExperimentInfoSettings));
        }
	

        /// <summary>
        /// 登录
        /// </summary>
        public void LoginIn()
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return;
            }

            string esid = _ExpInfoSettings.experimentEsid;
            JObject loginInfo = new JObject();
            //luyud rainier
            loginInfo.Add("account", username);
            loginInfo.Add("password", password);
            loginInfo.Add("esid", esid);

            Debug.Log("当前用户登录信息：[" + loginInfo.ToString() + "]");
 
            labInterSystem.LabRequest(LabInterType.login, loginInfo.ToString());
            //注册回调
            labInterSystem.RequestCallBack = LoginInCallBack;

          
        }

        /// <summary>
        /// 登录回调，选择实验，设置eid
        /// </summary>
        /// <param name="data">状态码000,101,102</param>
        public void LoginInCallBack(string data)
        {

            JObject jObject = _serviceSerializer.DeSerializerObject<JObject>(data);

            string status = jObject["status"].ToString();
            //获取实验列表集合
            IEnumerable<JToken> expList = jObject["expList"];

            Dictionary<string, string> expNameId = new Dictionary<string, string>();


            foreach (var item in expList)
            {
                expNameId.Add(item["name"].ToString(), item["id"].ToString());
            }
            switch (status)
            {
                case "000":
                    Debug.Log("登录成功");
                    //设置实验的eid
                    string eid = "";
                    if (expNameId.TryGetValue(_ExpInfoSettings.experimentName, out eid))
                    {
                        labInterSystem.labInfoParams.eid = eid;
                        //加载场景
                        LoadTargetScene();
                    }
                    else
                    {
                        Debug.Log("实验名称与平台实验名称不一致");
                    }
                    break;
                case "101":
                    Debug.Log("账户不存在或密码错误");
                    break;
                case "102":
                    Debug.Log("用户被禁用");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 登录成功加载对应的场景 
        /// </summary>
        private void LoadTargetScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(loginSceneName);
        }

        /// <summary>
        /// 判断是否是字母和数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsRightFormat(string str)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return regex.IsMatch(str);
        }
	}
}

