using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum expType
{
    text,
    picture,
    audio,
    video,
}
public class ExpReport : MonoBehaviour
{
    private static ExpReport _instance;
    public static ExpReport Instance { get { return _instance; } }

    public void Init()
    {
        _instance = this;
    }
    /// <summary>
    /// 对接实验报告中的文本
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="modelName">实验报告中的传递文本处所对应的名字</param>
    public void SendText(string content,string modelName="text1")
    {
         ExpInteractive.Instance.UpLoadReport(GetTextJson(content, modelName), UploadExpCallBack);        
    }
    /// <summary>
    /// 一次传送多个
    /// </summary>
    /// <param name="contentArray"></param>
    /// <param name="modelNameArray"></param>
    public void SendText(string[]contentArray,string[] modelNameArray)
    {
        ExpInteractive.Instance.UpLoadReport(GetTextJson(contentArray, modelNameArray), UploadExpCallBack);      
    }
    /// <summary>
    /// 对接实验报告中的图片
    /// </summary>
    /// <param name="bytes">图片字节</param>
    /// <param name="modelName">实验报告中的传递图片处所对应的名字</param>
    public void SendPicture(byte[]bytes, string modelName = "picture1")
    {
        ExpInteractive.Instance.UpLoadReport(GetPicJson(bytes, modelName), UploadExpCallBack);     
    }
    /// <summary>
    /// 一次传送多个
    /// </summary>
    public void SendPicture(List<byte[]>bytesList, string[] modelNameArray)
    {
        ExpInteractive.Instance.UpLoadReport(GetPicJson(bytesList, modelNameArray), UploadExpCallBack);         
    }
    /// <summary>
    /// 对接实验报告中的音频
    /// </summary>
    /// <param name="bytes">音频字节</param>
    /// <param name="modelName">实验报告中的传递音频处所对应的名字</param>
    public void SendAudio(byte[] bytes, string modelName = "voice1")
    {
        ExpInteractive.Instance.UpLoadReport(GetAudioJson(bytes, modelName), UploadExpCallBack);   
        
    }
    /// <summary>
    /// 一次传送多个
    /// </summary>
    public void SendAudio(List<byte[]> bytesList, string[] modelNameArray)
    {
        ExpInteractive.Instance.UpLoadReport(GetAudioJson(bytesList, modelNameArray), UploadExpCallBack);   
    }
    /// <summary>
    /// 对接实验报告中的视频
    /// </summary>
    /// <param name="bytes">视频字节</param>
    /// <param name="modelName">实验报告中的传递视频处所对应的名字</param>
    public void SendVideo(byte[] bytes, string modelName = "video1")
    {
        ExpInteractive.Instance.UpLoadReport(GetVideoJson(bytes, modelName), UploadExpCallBack);   
    }
    /// <summary>
    /// 一次传送多个
    /// </summary>
    public void SendVideo(List<byte[]> bytesList, string[] modelNameArray)
    {
        ExpInteractive.Instance.UpLoadReport(GetVideoJson(bytesList, modelNameArray), UploadExpCallBack);   
      
    }
    /// <summary>
    /// 上传实验报告回调信息
    /// </summary>
    /// <param name="data"></param>
    private void UploadExpCallBack(string data)
    {
     

    }
    #region 得到实验报告Json
    JObject GetTextJson(string content,string modelName)
    {
        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        JArray body = new JArray();
        JObject bodyJo = new JObject();
        bodyJo["text"] = content;
        bodyJo["color"] = "black";
        body.Add(bodyJo);
        jo[modelName] = body;
        return jo;
    }
    JObject GetTextJson(string[]contentArray,string[]modelNameArray)
    {
        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        for (int i = 0; i < modelNameArray.Length; i++)
        {
            JArray body = new JArray();
            JObject bodyJo = new JObject();
            bodyJo["text"] = contentArray[i];
            bodyJo["color"] = "black";
            body.Add(bodyJo);
            jo[modelNameArray[i]] = body;
        }           
        return jo;
    }
    JObject GetPicJson(byte[]bytes,string modelName)
    {
      
        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        JArray body = new JArray();
        JObject bodyJo = new JObject();
        string data64String = System.Convert.ToBase64String(bytes);    
        bodyJo["src"] = data64String;
        bodyJo["extend"] = "jpg";
        bodyJo["width"] = "300px";
        bodyJo["height"] = "200px";
        body.Add(bodyJo);
        jo[modelName] = body;
        return jo;
    }
    JObject GetPicJson(List<byte[]>bytesList, string[]modelNameArray)
    {
        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        for (int i = 0; i < modelNameArray.Length; i++)
        {
            JArray body = new JArray();
            JObject bodyJo = new JObject();
            string data64String = System.Convert.ToBase64String(bytesList[i]);
            bodyJo["src"] = data64String;
            bodyJo["extend"] = "jpg";
            bodyJo["width"] = "300px";
            bodyJo["height"] = "200px";
            body.Add(bodyJo);
            jo[modelNameArray[i]] = body;
        }
        return jo;
    }
    JObject GetAudioJson(byte[] bytes, string modelName)
    {

        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        JArray body = new JArray();
        JObject bodyJo = new JObject();
        string data64String = System.Convert.ToBase64String(bytes);       
        bodyJo["src"] = data64String;
        bodyJo["extend"] = "mp3";
        body.Add(bodyJo);
        jo[modelName] = body;
        return jo;
    }
    JObject GetAudioJson(List<byte[]> bytesList, string[] modelNameArray)
    {
        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        for (int i = 0; i < modelNameArray.Length; i++)
        {
            JArray body = new JArray();
            JObject bodyJo = new JObject();
            string data64String = System.Convert.ToBase64String(bytesList[i]);
            bodyJo["src"] = data64String;
            bodyJo["extend"] = "mp3";
            body.Add(bodyJo);
            jo[modelNameArray[i]] = body;
        }
        return jo;
    }
    JObject GetVideoJson(byte[] bytes, string modelName)
    {

        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        JArray body = new JArray();
        JObject bodyJo = new JObject();
        string data64String = System.Convert.ToBase64String(bytes);        
        bodyJo["src"] = data64String;
        bodyJo["extend"] = "mp4";
        bodyJo["width"] = "300px";
        bodyJo["height"] = "200px";
        body.Add(bodyJo);
        jo[modelName] = body;
        return jo;
    }
    JObject GetVideoJson(List<byte[]> bytesList, string[] modelNameArray)
    {
        JObject jo = new JObject();
        jo["eid"] = ExpInteractive.Instance.eid;
        for (int i = 0; i < modelNameArray.Length; i++)
        {
            JArray body = new JArray();
            JObject bodyJo = new JObject();
            string data64String = System.Convert.ToBase64String(bytesList[i]);
            bodyJo["src"] = data64String;
            bodyJo["extend"] = "mp4";
            bodyJo["width"] = "300px";
            bodyJo["height"] = "200px";
            body.Add(bodyJo);
            jo[modelNameArray[i]] = body;
        }
        return jo;
    }
    #endregion

}
