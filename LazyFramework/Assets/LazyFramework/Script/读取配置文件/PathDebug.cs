/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CsvController
* 创建日期：2019-12-12 10:13:27
* 作者名称：尚苗苗
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathDebug : MonoBehaviour
{
    //public GUISkin guiSkin;
    public RawImage rawImage;
    public AudioSource audioSource;
    string s = "";

    void OnGUI()
    {
        Debug.Log("OnGUI");
        //GUI.skin = guiSkin;
        GUI.Label(new Rect(Screen.width/2, 0, Screen.width, Screen.height / 4), s);


        if (GUI.Button(new Rect(0, 0, 100, 50), "LoadCsv"))
            StartCoroutine(StreamingAssetPathConfigReader.TextReader("config.csv", ShowCsv));
        if (GUI.Button(new Rect(0, 50, 100, 50), "LoadTxt"))
            StartCoroutine(StreamingAssetPathConfigReader.TextReader("Test.txt", ShowTxt));
        if (GUI.Button(new Rect(0, 100, 100, 50), "LoadJson"))
            StartCoroutine(StreamingAssetPathConfigReader.TextReader("Test2.json", ShowJson));

        if (GUI.Button(new Rect(0, 150, 100, 50), "ShowImage"))
            StartCoroutine(StreamingAssetPathConfigReader.TextureReader("Picture.png", ShowTexture));

        if (GUI.Button(new Rect(0, 200, 100, 50), "PlayerAudio"))
            StartCoroutine(StreamingAssetPathConfigReader.AudioClipReader("audio.wav", AudioPlay));
    }

 

    /// <summary>
    /// 读取csv文件
    /// </summary>
    /// <param name="s"></param>
    private void ShowCsv(string s)
    {
        this.s = s;
    }


    /// <summary>
    /// 读取txt文件
    /// </summary>
    /// <param name="s"></param>
    private void ShowTxt(string s)
    {
        string[] content = s.Split('\n');
        List<string> line = new List<string>();
        for (int i = 0; i < content.Length; i++)
        {
            line.Add(content[i]);
        }

        List<TxtMessage> txtMessages = new List<TxtMessage>();

        TxtMessage currentLine = new TxtMessage();
        for (int i = 0; i < line.Count; i++)
        {
            string key = line[i].Split(':')[0];
            string value = line[i].Split(':')[1];
            currentLine.key = key;
            currentLine.value = value;
            txtMessages.Add(currentLine);

        }

        Debug.Log(txtMessages[0].value);
        if (txtMessages[0].key.Equals("﻿信息是否保密"))
            if (txtMessages[0].value.Trim().Equals("是"))
            {
                this.s = "该用户信息保密,无法访问";
            }

            else
            {
                this.s = "";
                for (int i = 1; i < content.Length; i++)
                {
                  this.s += content[i] + "\n";
                }
            }
               
    }
    /// <summary>
    /// 读取json文件
    /// </summary>
    /// <param name="s"></param>
    private void ShowJson(string s)
    {
        ParseItemJson(s);
    }
    private void ShowTexture(Texture texture)
    {
        rawImage.texture = texture;
    }

    private void AudioPlay(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void ParseItemJson(string jsonStr)
    {
        // 将Json中的数组用一个list包裹起来，变成一个Wrapper对象
        jsonStr = "{ \"list\": " + jsonStr + "}";
        Response<ExamJson> studentList = JsonUtility.FromJson<Response<ExamJson>>(jsonStr);
        foreach (ExamJson item in studentList.list)
        {
            Debug.Log(item.题目);
        }
    }

    // Json解析为该对象
    public class Response<T>
    {
        public List<T> list;
    }

    [Serializable]
    public class ExamJson
    {
        public string 题号;
        public string 题目;
        public string 选项;
        public string 答案;
        public string 分值;
        public string 解析;
    }
}
/// <summary>
/// Txt信息文档
/// </summary>
public struct TxtMessage
{
    public string key;
    public string value;

}