using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpScore : MonoBehaviour
{
    private static ExpScore _instance;
    public static ExpScore Instance { get { return _instance; } }
    public void Init()
    {
        _instance = this;
    }
    /// <summary>
    ///发送成绩
    /// </summary>
    public void SendScore(int score)
    {
        LaunchScore(ExpInteractive.Instance.platformType, score, UploadScoreCallBack);
    }
    public void LaunchScore(PlatformType platformType,int score,Action<string>m)
    {
        if (platformType == PlatformType.Purchase)
        {
            string data = GetScoreObj_purchase(score);
            ExpInteractive.Instance.UploadScore(data, m);
        }
        else if (platformType == PlatformType.Simple)
        {
#if UNITY_STANDALONE_WIN
            string data = GetScoreObj_simple(score);
            ExpInteractive.Instance.UploadScore(data,m);
#elif UNITY_WEBGL
            SendScoreToWeb(score);
#elif UNITY_WEBPLAYER
            Debug.LogError("简易平台Webplayer暂时没有发送成绩方法");
#endif
        }
    }
    /// <summary>
    /// 发送成绩回调信息
    /// </summary>
    /// <param name="data"></param>
    private void UploadScoreCallBack(string data)
    {
    }
    /// <summary>
    /// 购买平台发送成绩Json格式
    /// </summary>
    /// <param name="score">分数</param>
    /// <returns></returns>
    string GetScoreObj_purchase(int score)
    {
        JObject scoreObj = new JObject();
        //可在此处填充数据	
        scoreObj.Add("eid", ExpInteractive.Instance.eid);
        scoreObj.Add("expScore", score.ToString());
        Debug.Log("当前请求数据头：" + scoreObj.ToString());
        return scoreObj.ToString();
    }
    /// <summary>
    /// 简易平台云渲染发送成绩Json格式
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    string GetScoreObj_simple(int score)
    {       
        JArray body = new JArray();
        JObject jo = new JObject();
        jo.Add("moduleFlag", "实验成绩");
        jo.Add("questionNumber", 1);
        jo.Add("questionStem", "学生操作成绩");
        jo.Add("score", score);//成绩分数
        jo.Add("trueOrFalse", "True");
        body.Add(jo);
        Debug.Log("当前请求数据头：" + JsonConvert.SerializeObject(body));
        return (JsonConvert.SerializeObject(body));
    }
    /// <summary>
    /// 简易平台WebGL 发送成绩
    /// </summary>
    /// <param name="score"></param>
    private void SendScoreToWeb(int score)
    {
        string[] moduleFlag = { "实验成绩" };
        string[] questionNumber = { "1" };
        string[] questionStem = { "学生实验操作成绩" };
        string[] scores = { score.ToString() };
        string[] isTrue = { "True" };
        Application.ExternalCall("ReciveData", moduleFlag, questionNumber, questionStem, scores, isTrue);//过时但是可以用
    }
}
