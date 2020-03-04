using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Lazy;

public class CamPointManager : MonoSingleton<CamPointManager>
{
    [SerializeField]List<CameraPointInfo> cameraPointInfos = new List<CameraPointInfo>();
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CameraTransformMove>() != null)
            {
                cameraPointInfos.Add(new CameraPointInfo(transform.GetChild(i).gameObject, transform.GetChild(i).name));
            }
        }
    }
    public GameObject FindPos(string name)
    {
        bool isEx = false;
        for (int i = 0; i < cameraPointInfos.Count; i++)
        {
            if (cameraPointInfos[i].posName == name)
            {
                isEx = true;
                return cameraPointInfos[i].pos;
            }
        }
        if (!isEx)
        {
            Debug.Log("没有找到摄像机位置" + name);
        }
        return null;
    }
  
}
[System.Serializable]
public class CameraPointInfo
{
    [SerializeField]public GameObject pos;
    [SerializeField]public string posName;
    public CameraPointInfo(GameObject go,string name)
    {
        pos = go;
        posName = name;
    }
}

