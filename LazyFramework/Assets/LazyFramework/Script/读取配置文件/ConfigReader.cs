/*******************************************************************************
* 版本声明：v1.0.0
* 类 名 称： CsvController
* 创建日期：2019-12-12 10:13:27
* 作者名称：尚苗苗
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.IO;
using System;

public static class StreamingAssetPathConfigReader
{
    public delegate void ReadFinishEvtHandler(EventArgs e);
    public static event ReadFinishEvtHandler OnReadFinishEvtHandler;
    /// <summary>
    /// 读取StreamingAsset中的配置文件
    /// </summary>
    /// <param name="configName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator TextReader(string configName, UnityAction<string> action = null)
    {
        string path;
#if UNITY_WIN_STANDALONE || UNITY_IPHONE &&!UNITY_EDITOR
        path ="file://"+ Application.streamingAssetsPath + configName;
#else 
        path = Application.streamingAssetsPath + "/" + configName;
#endif
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(path);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.error != null)
            Debug.Log(unityWebRequest.error);
        else
        {
            string content = unityWebRequest.downloadHandler.text;
            if (action != null)
                action(content);
            if(OnReadFinishEvtHandler!=null)
            {
                OnReadFinishEvtHandler.Invoke(EventArgs.Empty);
            }
        }
    }


    /// <summary>
    /// 读取streamingAsset中的图片
    /// </summary>
    /// <param name="mediaName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator TextureReader(string  mediaName,UnityAction<Texture> action)
    {
        string path;
#if UNITY_WIN_STANDALONE || UNITY_IPHONE &&!UNITY_EDITOR
        path ="file://"+ Application.streamingAssetsPath + configName;
#else 
        path = Application.streamingAssetsPath + "/" + mediaName;
#endif

        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(path);
        yield return unityWebRequest.SendWebRequest();
        
        if (unityWebRequest.error != null)
            Debug.Log(unityWebRequest.error);
        else
        {
            byte[] bts = unityWebRequest.downloadHandler.data;
            if (action != null)
            {
                action(DownloadHandlerTexture.GetContent(unityWebRequest));
            }
        }
    }



    /// <summary>
    /// 读取streamingAsset文件夹中的多媒体（音频）
    /// </summary>
    /// <param name="mediaName"></param>
    /// <param name="action"></param>
    /// <returns></returns>

    public static IEnumerator AudioClipReader(string mediaName, UnityAction<AudioClip> action=null)
    {
        string path;
#if UNITY_WIN_STANDALONE || UNITY_IPHONE &&!UNITY_EDITOR
        path ="file://"+ Application.streamingAssetsPath + configName;
#else 
        path = Application.streamingAssetsPath + "/" + mediaName;
#endif
        FileInfo fileInfo = new FileInfo(path);
        string fileExtension = fileInfo.Extension;
        AudioType audioType;

        switch (fileExtension)
        {
            case ".mp3":
                audioType = AudioType.MPEG;
                break;
            case ".ogg":
                audioType = AudioType.OGGVORBIS;
                break;
            case ".wav":
                audioType = AudioType.WAV;
                break;
            case ".aiff":
                audioType = AudioType.AIFF;
                break;
            default:
                audioType = AudioType.MPEG;
                break;
        }

        UnityWebRequest unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(path, audioType);
        yield return unityWebRequest.SendWebRequest();

        
        if (unityWebRequest.error != null)
            Debug.Log(unityWebRequest.error);
        else
        {
            if (action != null)
                action(DownloadHandlerAudioClip.GetContent(unityWebRequest));
        }
    }
}


