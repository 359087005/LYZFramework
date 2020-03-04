using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 此脚本是能够将文本字符串随着时间打字或褪色显示。
/// </summary>

[RequireComponent(typeof(Text))]
[AddComponentMenu("Typewriter Effect")]
public class TypewriterEffectUGUI : MonoBehaviour
{
    public UnityEvent myEvent;
    public int charsPerSecond = 0;
    // public AudioClip mAudioClip;             // 打字的声音，不是没打一个字播放一下，开始的时候播放结束就停止播放
    public bool isActive = false;

    [HideInInspector]
    public float timer;
    public string words;
    private Text mText;

    void Start()
    {
        if (myEvent == null)
            myEvent = new UnityEvent();

        words = GetComponent<Text>().text;
        GetComponent<Text>().text = string.Empty;
        timer = 0;
     //   isActive = true;
        charsPerSecond = Mathf.Max(1, charsPerSecond);
        mText = GetComponent<Text>();
    }

    string[] totalTips; 
    string titleTips;
   public  void ReloadText(string _words)
    {
        if (_words.IndexOf('|') > -1)
        {
            totalTips = _words.Split('|');

            titleTips = totalTips[0];
            words = totalTips[1];
        }
        else
        {
            titleTips = "";
            words = _words;
        }
        isActive = true;
        mText = GetComponent<Text>();
    }

    public void OnStart()
    {
        isActive = true;
    }

    void Update()
    {
        if (isActive)
        {
            try
            {
                mText.text = titleTips + words.Substring(0, (int)(charsPerSecond * timer));
                timer += Time.deltaTime;
            }
            catch (Exception)
            {
                OnFinish();
            }
        }
    }

    void OnFinish()
    {
        isActive = false;
        timer = 0;
        GetComponent<Text>().text = titleTips + words;
        try
        {
            myEvent.Invoke();
        }
        catch (Exception)
        {
            Debug.Log("问题");
        }
    }

    
}
