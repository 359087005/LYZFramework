using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Networking;
using UnityEngine.Networking;

/// <summary>
/// 平台类型
/// </summary>
public enum PlatformType
{
    [Header("购买平台")]
    Purchase,
    //#if !UNITY_WEBPLAYER
    [Header("简易平台")]
    Simple,
    //#endif
}
/// <summary>
/// url类型
/// </summary>
public enum UrlType
{
    Login_purchase,
    Login_simple,
    UploadScore_simple_cloudRender,//简易平台 云渲染上传成绩url  
    UploadScore_bought,//购买平台 上传成绩url(包括 云渲染 WebGl WebPlayer)
    UploadReport_bought,//购买平台上传报告
}
public class ExpInteractive : MonoBehaviour
{
    private static ExpInteractive _instance;
    public static ExpInteractive Instance
    {
        get { return _instance; }
    }
    private bool isOnly = false;
    [EnumLabel("平台类型")]
    public PlatformType platformType = PlatformType.Purchase;
    //url 类型
    private UrlType _urlType;
    public string eid
    {
        get { return _loginInfoDic["eid"]; }
    }
    //储存登陆返回信息
    private Dictionary<string, string> _loginInfoDic = new Dictionary<string, string> 
    {
    {"eid",string.Empty},{"numberId",string.Empty},{"name",string.Empty},{"role",string.Empty},
    {"urlDataPost",string.Empty},{"expId",string.Empty}
    };
    
    /// <summary>
    /// 接口地址
    /// </summary>
    private Dictionary<UrlType, string> _urlDic = new Dictionary<UrlType, string>();
    /// <summary>
    /// 回调
    /// </summary>
    public Action<string> CallBack;
    #region Windows属性

#if UNITY_STANDALONE_WIN

    //旧平台（购买平台）
    public string hostUrl = "http://sfx.owvlab.net/virexp/";
    private string suffixTokenUrl = "outer/getMessageByToken";
    private string _token="";

    //新平台（简易平台）（租用平台）
    private string host_simplePlatform = "";
    public string Host = "";
    public string id = "";
    private string _sequenceCode="";
    private string suffixSequenceUrl = "";


    [Tooltip("从网页上获取")]
    [Header("云渲染在编辑器测试用的token值")]
    public string token = "";
    [Tooltip("从网页上获取")]
    [Header("云渲染在编辑器测试用的序列号")]
    public string sequenceCode = "";

#endif
    #endregion
    #region WebGL WebPlayer属性

#if UNITY_WEBGL||UNITY_WEBPLAYER
    public string hostUrl = "http://sfx.owvlab.net/virexp/";
#endif

    #endregion

    #region 回调字符串
    private string str_eid = "";
    private string str_login = "";
    private string str_sendinfo = "";
    private string str_sendScore = "";
    private string str_sendReport = "";
    #endregion
    void Awake()
    {
        #region 单例
        if (_instance == null)
        {
            isOnly = true;
            _instance = this;
            GetComponent<ExpReport>().Init();
            GetComponent<ExpScore>().Init();
            DontDestroyOnLoad(this);
        }
        if (!isOnly) Destroy(gameObject);
        #endregion
    }
    void Start()
    {
        GetSettings();//获取url等信息          
    }
    public void UploadScore(string data,Action<string>m)
    {
        if (platformType == PlatformType.Purchase)
        {            
            str_sendinfo = data;
            _urlType = UrlType.UploadScore_bought;
            Up(data);
            CallBack = m;
        }
        else if (platformType == PlatformType.Simple)
        {
#if UNITY_STANDALONE_WIN        
            str_sendinfo = data;
            _urlType = UrlType.UploadScore_simple_cloudRender;
            Up(data);
            CallBack = m;
#endif
        }
    }
    /// <summary>
    /// 取得token值或sequenceCode
    /// </summary>
#if UNITY_STANDALONE_WIN
    void SetInitValue()
    {
        string[] args = Environment.GetCommandLineArgs();
        foreach (var item in args)
        {
            switch (platformType)
            {
                case PlatformType.Purchase:
                    if (item.StartsWith("token"))
                    {
                        _token = item.Split('=')[1];
                    }
                    break;
                case PlatformType.Simple:
                    if (item.StartsWith("sequenceCode"))
                    {
                        _sequenceCode = item.Split('=')[1];
                    }
                    break;
            }

        }
#if UNITY_EDITOR
        _token = token;
        _sequenceCode = sequenceCode;
#endif
    }
#endif
    /// <summary>
    /// 取得用户信息
    /// </summary>
    private void GetUserInfoOnCloudRender()
    {

#if UNITY_STANDALONE_WIN
        JObject jo = new JObject();
        switch (platformType)
        {
            case PlatformType.Purchase:
                if (!string.IsNullOrEmpty(_token))
                {
                    jo["token"] = _token;

                    byte[] postBtye = System.Text.Encoding.UTF8.GetBytes(jo.ToString());
                    string data = System.Convert.ToBase64String(postBtye);
                    _urlType = UrlType.Login_purchase;
                    Up(data);
                    CallBack = LoginCallBack;
                }
                break;
            case PlatformType.Simple:
                if (!string.IsNullOrEmpty(_sequenceCode))
                {
                    _urlType = UrlType.Login_simple;
                    Up(_sequenceCode);
                    CallBack = LoginCallBack;
                }
                break;
        }
#elif UNITY_WEBPLAYER
        Application.ExternalCall("getUserInfoForWebPlayer");
#elif UNIT_WEBGL
      _GetUserInfo();
#endif
    }
    private void Up(string data)
    {
        StartCoroutine(ExcuteRequest(_urlDic[_urlType], data));
    }

    IEnumerator ExcuteRequest(string url, string data)
    {
        //webgl webplayer 发送成绩
#if UNITY_WEBGL
        if (platformType == PlatformType.Purchase)
        {
            Dictionary<string, string> postdata = new Dictionary<string, string>();
            postdata.Add("param", data);
            UnityWebRequest request = UnityWebRequest.Post(url, postdata);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return request.SendWebRequest();
            TotalCallBack(request.responseCode, request.downloadHandler.text);
        }
#elif UNITY_WEBPLAYER
        if (platformType == PlatformType.Purchase)
        {
            WWWForm pdata = new WWWForm();
            pdata.AddField("param", data);
            if (!pdata.headers.ContainsKey("Content-Type"))
            {
                pdata.headers.Add("Content-Type", "application/x-www-form-urlencoded");
            }
            WWW www = new WWW(url, pdata);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            { Debug.Log(www.error); }              //如这里报错跨域访问，把edit里的 projectsetting 里的host url改成自己实验的主页地址       
            TotalCallBack(1, www.text);
        }

#endif
        //云渲染 登陆 发送成绩
#if UNITY_STANDALONE_WIN&&!UNITY_5        
        if (platformType == PlatformType.Purchase)
        { 
            Dictionary<string, string> postdata = new Dictionary<string, string>();
            postdata.Add("param", data);
            UnityWebRequest request = UnityWebRequest.Post(url, postdata);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return request.SendWebRequest();
            TotalCallBack(request.responseCode, request.downloadHandler.text);
        }
        else if (platformType == PlatformType.Simple)
        {
            switch (_urlType)
            { 
                case UrlType.Login_simple:
                      Dictionary<string, string> postdata = new Dictionary<string, string>();
                      postdata.Add("sequenceCode", data);
                      UnityWebRequest request = UnityWebRequest.Post(url, postdata);
                      request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                      request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                      yield return request.SendWebRequest();
                      TotalCallBack(request.responseCode, request.downloadHandler.text);
                    break;
                case UrlType.UploadScore_simple_cloudRender:
                    byte[] body = Encoding.UTF8.GetBytes(data);
                    UnityWebRequest request2 = UnityWebRequest.Post(url, "POST");
                    request2.uploadHandler = new UploadHandlerRaw(body);
                    request2.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                    request2.SetRequestHeader("Content-Type", "application/json;charset=UTF-8");
                    yield return request2.SendWebRequest();
                    TotalCallBack(request2.responseCode, request2.downloadHandler.text);
                    break;
            }
          
        }
#elif UNITY_STANDALONE_WIN && UNITY_5
        if (platformType == PlatformType.Purchase)
        {
            WWWForm pdata = new WWWForm();
            pdata.AddField("param", data);
            if (!pdata.headers.ContainsKey("Content-Type"))
            {
                pdata.headers.Add("Content-Type", "application/x-www-form-urlencoded");
            }
            WWW www = new WWW(url, pdata);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            { Debug.Log(www.error); }              //如这里报错跨域访问，把edit里的 projectsetting 里的host url改成自己实验的主页地址       
            TotalCallBack(1, www.text);
        }
        else if (platformType == PlatformType.Simple)
        {
            switch (_urlType)
            {
                case UrlType.Login_simple:
                    WWWForm pdata = new WWWForm();
                    pdata.AddField("sequenceCode", data);

                    WWW www = new WWW(url, pdata);
                    yield return www;
                    if (!string.IsNullOrEmpty(www.error))
                    { Debug.Log(www.error); }              //如这里报错跨域访问，把edit里的 projectsetting 里的host url改成自己实验的主页地址                  
                    TotalCallBack(1, www.text);
                    break;
                case UrlType.UploadScore_simple_cloudRender:
                    Dictionary<string, string> JsonDic = new Dictionary<string, string>();
                    JsonDic.Add("Content-Type", "application/json");
                    byte[] body = Encoding.UTF8.GetBytes(data);
                    WWW www2 = new WWW(url, body, JsonDic);
                    yield return www2;
                    if (!string.IsNullOrEmpty(www2.error))
                    { Debug.Log(www2.error); }              //如这里报错跨域访问，把edit里的 projectsetting 里的host url改成自己实验的主页地址       
                    TotalCallBack(1, www2.text);
                    break;
            }

        }
#endif
    }
    /// <summary>
    /// 云渲染登陆回调信息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="paltformType"></param>
    private void LoginCallBack(string data)
    {

        JObject jResult = null;

        if (platformType == PlatformType.Purchase)
        {
            byte[] data64 = System.Convert.FromBase64String(data);
            string result = System.Text.Encoding.UTF8.GetString(data64);
            jResult = JObject.Parse(result);
            str_login = jResult.ToString();
            if (jResult["status"].ToString() == "909")
            {
                Debug.LogError("token值失效，请重新到网页上取得");
                return;
            }
            _loginInfoDic["eid"] = jResult["eId"].ToString();
            _loginInfoDic["numberId"] = jResult["numberId"].ToString();
            _loginInfoDic["name"] = jResult["name"].ToString();
        }
        else if (platformType == PlatformType.Simple)
        {
            jResult = JObject.Parse(data);
            str_login = jResult.ToString();
            if (jResult["error"].ToString() == "验证码错误.")
            {
                Debug.LogError("序列号失效，请重新到网页上取得");
                return;
            }
            IEnumerable<JToken> expList = jResult["expList"];
            _loginInfoDic["urlDataPost"] = jResult["urlDataPost"].ToString();

            if (expList != null)
            {
                List<string> idList = new List<string>();//实验eid记录
                foreach (var item in expList)
                {
                    idList.Add(item["expId"].ToString());

                }
                _loginInfoDic["expId"] = idList[0];
#if UNITY_STANDALONE_WIN
                string url = host_simplePlatform + _loginInfoDic["urlDataPost"] + "/" + id + "/" + _loginInfoDic["expId"];// 简易平台发送成绩Url

                if (!_urlDic.ContainsKey(UrlType.UploadScore_simple_cloudRender))
                {
                    _urlDic.Add(UrlType.UploadScore_simple_cloudRender, url);
                }
#endif
            }
            else
            {
                Debug.Log("error" + jResult["error"]);
            }

        }
        Debug.Log("当前用户登录信息：" + jResult);
    }
    /// <summary>
    /// 回调信息
    /// </summary>
    /// <param name="responseCode"></param>
    /// <param name="data"></param>
    private void TotalCallBack(long responseCode, string data)
    {       
        switch (_urlType)
        { 
            case UrlType.UploadScore_bought:
                JObject jo = JObject.Parse(data);        
                switch (jo["status"].ToString())
                 {
                  case "101":
                      Debug.Log("上传成绩请求返回信息：[" + data + "]");
                      str_sendScore = data;
                   break;
                   case "102":
                       #if(!UNITY_STANDALONE_WIN)&&UNITY_EDITOR
                       string info = "接口可以走通,但是eid没有获取到(在编辑器获取不到eid需打包出来)";
                        Debug.Log(info);
                        str_sendScore = info;
                        #else
                        Debug.Log("上传成绩请求返回信息：[" + data + "]");   
                        str_sendScore = data;
                       #endif
                    break;
                   default:
                    Debug.Log("上传成绩请求返回信息：[" + data + "]");
                    str_sendScore = data;
                    break;
                  } 
                break;
            case UrlType.UploadScore_simple_cloudRender:
                str_sendScore = data;
                Debug.Log("上传成绩请求返回信息：[" + data + "]");
                break;
            case UrlType.UploadReport_bought:
                JObject jo2 = JObject.Parse(data);
                switch (jo2["status"].ToString())
                { 
                    case "102":
                       #if(!UNITY_STANDALONE_WIN)&&UNITY_EDITOR
                       string info = "接口可以走通,但是eid没有获取到(在编辑器获取不到eid需打包出来)";
                        Debug.Log(info);
                        str_sendReport = info;
                        #else                       
                        str_sendReport = data;
                        Debug.Log("上传实验报告请求返回信息：[" + data + "]"); 
                       #endif
                        break;
                    default:
                        str_sendReport = data;
                        Debug.Log("上传实验报告请求返回信息：[" + data + "]"); 
                        break;
                }
                break;
        }
        if (CallBack != null)
        {
            CallBack(data);
        }

    }

    private Action<string> getJsonCallBack;
    JObject data_getJson = null;
    private void GetSettings()
    {
#if !UNITY_WEBPLAYER
        StartCoroutine(TextReader("DataInteractiveSettings.json", (a) => { GetJsonFromFileCallBack(a); }));
#else
        //StartCoroutine(ReadData("file://" + Application.streamingAssetsPath + "/DataInteractiveSettings.json"));
        //getJsonCallBack = GetJsonFromFileCallBack;        
        SetUrl();
#endif

    }
    /// <summary>
    /// 读取StreamingAsset中的配置文件 尚苗苗 
    /// </summary>
    /// <param name="configName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
#if !UNITY_WEBPLAYER
    public static IEnumerator TextReader(string configName, UnityAction<string> action = null)
    {
        string path;
#if UNITY_WIN_STANDALONE || UNITY_IPHONE &&!UNITY_EDITOR
        path ="file://"+ Application.streamingAssetsPath + configName;
#else
        path = Application.streamingAssetsPath + "/" + configName;
#endif
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(path);
#if UNITY_2018
        yield return unityWebRequest.SendWebRequest();
#else
         yield return unityWebRequest.Send();
#endif
        if (unityWebRequest.error != null)
            Debug.Log(unityWebRequest.error);
        else
        {
            string content = unityWebRequest.downloadHandler.text;
            if (action != null)
                action(content);
        }
    }
        public static IEnumerator FileReader(string configName, UnityAction<byte[]> action = null)
    {
        string path;
#if UNITY_WIN_STANDALONE || UNITY_IPHONE &&!UNITY_EDITOR
        path ="file://"+ Application.streamingAssetsPath + configName;
#else
        path = Application.streamingAssetsPath + "/" + configName;
#endif
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(path);
#if UNITY_2018
        yield return unityWebRequest.SendWebRequest();
#else
         yield return unityWebRequest.Send();
#endif
        if (unityWebRequest.error != null)
            Debug.Log(unityWebRequest.error);
        else
        {
            byte[] content = unityWebRequest.downloadHandler.data;
            if (action != null)
                action(content);
        }
    }
#endif

    /// <summary>
    ///  从streamingAssets 读取json
    /// </summary>
    void GetJsonFromFileCallBack(string content)
    {
        Debug.Log(content);
        data_getJson = JObject.Parse(content);
        if (data_getJson["设置"]["isUseMe"].ToString() == "true")
        {
            GetSettingsInfoinFile();
        }
        else
        {
            GetSettingsInfoinUnity();
        }
        SetUrl();

    }
    /// <summary>
    /// 设置url
    /// </summary>
    void SetUrl()
    {
        Debug.Log("平台类型:" + platformType.ToString());
        _urlDic.Add(UrlType.UploadScore_bought, hostUrl + "outer/intelligent/!expScoreSave");
        _urlDic.Add(UrlType.UploadReport_bought, hostUrl + "outer/report/!reportEdit");
#if UNITY_STANDALONE_WIN
        for (int i = 0; i < Host.Length - 1; i++)
        {
            host_simplePlatform += Host[i];
        }
        suffixSequenceUrl = "/openapi/" + id;
        _urlDic.Add(UrlType.Login_purchase, hostUrl + suffixTokenUrl);
        _urlDic.Add(UrlType.Login_simple, host_simplePlatform + suffixSequenceUrl);
        GetUserInfoOnCloudRender();
#elif UNITY_WEBGL
        if (platformType == PlatformType.Purchase)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                 ExpInteractive.GetUserInfo();           
            }            
        }
#elif UNITY_WEBPLAYER
        if (platformType == PlatformType.Purchase)
        {
            if (Application.platform == RuntimePlatform.WindowsWebPlayer)
            {
                Application.ExternalCall("getUserInfoForWebPlayer");                
            }
            
        }

#endif
    }
    /// <summary>
    /// 从unity设置url
    /// </summary>
    void GetSettingsInfoinUnity()
    {
        Debug.Log("inUnity...");
#if UNITY_STANDALONE_WIN
        SetInitValue();
#endif
    }
    /// <summary>
    /// 从json设置url
    /// </summary>
    void GetSettingsInfoinFile()
    {
        Debug.Log("inFile...");
        if (data_getJson["设置"]["平台类型"].ToString() == "购买平台") platformType = PlatformType.Purchase;
        else if (data_getJson["设置"]["平台类型"].ToString() == "简易平台") platformType = PlatformType.Simple;
        hostUrl = data_getJson["购买平台设置"]["url"].ToString();
#if UNITY_STANDALONE_WIN
        Host = data_getJson["简易平台设置"]["host"].ToString();
        id = data_getJson["简易平台设置"]["id"].ToString();

        SetInitValue();
//#if UNITY_EDITOR
//        _token = data_getJson["购买平台设置"]["token"].ToString();
//        _sequenceCode = data_getJson["简易平台设置"]["sequenceCode"].ToString();
//#endif
#endif

    }
    IEnumerator ReadData(string path)
    {
        WWW www = new WWW(path);
        Debug.Log(path);
        yield return www;
        if (www.isDone)
        {
            yield return new WaitForSeconds(0.5f);
            data_getJson = JObject.Parse(www.text);
            getJsonCallBack(data_getJson.ToString());
        }

        yield return new WaitForEndOfFrame();

    }
    /// <summary>
    /// 上传报告
    /// </summary>
    /// <param name="reportJson"></param>
    public void UpLoadReport(JObject reportJson,Action<string>m)
    {
        if (platformType == PlatformType.Purchase)
        {
            _urlType = UrlType.UploadReport_bought;
            Up(reportJson.ToString());
            CallBack = m;
        }
        else if (platformType == PlatformType.Simple)
        {
            Debug.LogError("简易平台暂时不能上传报告");
        }

    }

    /// <summary>
    /// jsLib中的GetUserInfo函数见：Assets\Plugins\LanInterApi.jslib
    /// </summary>
    /// <param name="str"></param>
    [DllImport("__Internal")]
    private static extern void _GetUserInfo();
    /// <summary>
    /// 获取用户信息
    /// </summary>
    public static void GetUserInfo()
    {
        _GetUserInfo();
    }
    /// <summary>
    /// 填充实验室相关数据(webPlayer专用)
    /// </summary>
    /// <param name="infoParams"></param>
    public void getUserInfoForWebPlayer(string infoParams)
    {
        JObject jObject = JsonConvert.DeserializeObject<JObject>(infoParams);
        _loginInfoDic["role"] = jObject["role"].ToString();
        _loginInfoDic["numberId"] = jObject["numberId"].ToString();
        _loginInfoDic["name"] = jObject["name"].ToString();
        _loginInfoDic["eid"] = jObject["eid"].ToString();
        Debug.Log("Webplayer自动获取:" + jObject.ToString());
    }

    /// <summary>
    /// 填充实验室相关数据(获取登陆信息时JS会自动调用此方法传入登陆信息)WebGL
    /// </summary>
    public void SetLabInfoParams(string infoParams)
    {
        //调用jslib返回数据
#if UNITY_WEBGL
        JObject jObject = JsonConvert.DeserializeObject<JObject>(infoParams);
        _loginInfoDic["role"] = jObject["role"].ToString();
        _loginInfoDic["numberId"]=jObject["numberId"].ToString();
        _loginInfoDic["name"]=jObject["name"].ToString();
        _loginInfoDic["eid"]=jObject["eid"].ToString();
        Debug.Log("WebGL自动获取:"+jObject.ToString());
#endif

    }

    public string GetInfo()
    {
      string info = "";
#if UNITY_STANDALONE_WIN
            info="平台类型:"+platformType.ToString()+"\n云渲染登陆回调:\n" + str_login.ToString() + "\n发送成绩Json格式:\n" + str_sendinfo.ToString() + "\n发送成绩回调:\n" + str_sendScore.ToString() + "\n对接实验报告回调:\n" + str_sendReport.ToString() + "\n";
#elif UNITY_WEBGL||UNITY_WEBPLAYER
        if (platformType == PlatformType.Purchase) str_eid = _loginInfoDic["eid"];
        info = "平台类型:\n" + platformType.ToString() + "\neid:\n" + str_eid + "\n发送成绩Json格式:\n" + str_sendinfo + "\n发送成绩回调:\n" + str_sendScore + "\n对接实验报告回调:\n" + str_sendReport + "\n";
#endif
        return info;
    }

}
