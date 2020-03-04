using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class UISwitch : MonoBehaviour
{
    [SerializeField] int curIndex;
    [SerializeField] List<ButtonInfo> buttonInfos = new List<ButtonInfo>();
    private void Start()
    {
        InitFunc();
    }
    public void InitFunc()
    {
        for (int i = 0; i < buttonInfos.Count; i++)
        {
            buttonInfos[i].button.gameObject.SetActive(false);
        }
        buttonInfos[curIndex].button.gameObject.SetActive(true);
    }
    public void ChangeFunc()
    {
        if (buttonInfos[curIndex].unityEvent != null)
        {
            buttonInfos[curIndex].unityEvent.Invoke();
        }
        for (int i = 0; i < buttonInfos.Count; i++)
        {
            buttonInfos[i].button.gameObject.SetActive(false);
        }
        if(curIndex+1>buttonInfos.Count-1)
        {
            curIndex = 0;
        }
        else
        {
            curIndex++;
        }
        buttonInfos[curIndex].button.gameObject.SetActive(true);
    }
}
[System.Serializable]
public class ButtonInfo
{
    public Button button;
    public UnityEvent unityEvent;
}

