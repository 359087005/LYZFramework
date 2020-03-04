using Lazy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxManager : MonoSingleton<BoxManager>
{
    [SerializeField] private List<MessageBox> boxTypes = new List<MessageBox>();
    private GameObject curBox;
    public GameObject CurBox
    {
        get
        {
            return curBox;
        }
        set
        {
            curBox = value;
        }
    }
    public void Start()
    {
       
        InitFunc();
    }
    private void InitFunc()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<MessageBox>() == null)
               transform.GetChild(i).gameObject.AddComponent<MessageBox>();
            if (transform.GetChild(i).GetComponent<MessageBox>() != null)
            {

                if (!boxTypes.Contains(transform.GetChild(i).GetComponent<MessageBox>()))
                {
                    boxTypes.Add(transform.GetChild(i).GetComponent<MessageBox>());
                    if (boxTypes[i].boxName == "" || boxTypes[i].boxName == null)
                    {
                        boxTypes[i].boxName = boxTypes[i].gameObject.name;
                    }
                }
            }
        }
    }
    public GameObject ShowBox(string boxName, string con ,Action comfirmClick= null)
    {
        bool isFind = false;
        for (int i = 0; i < boxTypes.Count; i++)
        {
            if (boxTypes[i].boxName == boxName)
            {
                CurBox = boxTypes[i].gameObject;
                isFind = true;
                boxTypes[i].gameObject.SetActive(true);
                if (con != null)
                {
                    boxTypes[i].ShowFunc(con);
                }
                if(comfirmClick != null)
                {
                    boxTypes[i].AddConfirmListen(comfirmClick);
                }
                return boxTypes[i].gameObject;
            }
        }
        if (!isFind)
            Debug.Log("没找到弹框" + boxName);
        CurBox = null;
        return null;
    }
    public GameObject ShowBox(string boxName, Action comfirmClick=null)
    {
        bool isFind = false;
        for (int i = 0; i < boxTypes.Count; i++)
        {
            if (boxTypes[i].boxName == boxName)
            {
                CurBox = boxTypes[i].gameObject;
                isFind = true;
                boxTypes[i].gameObject.SetActive(true);
               
                if (comfirmClick != null)
                {
                    boxTypes[i].AddConfirmListen(comfirmClick);
                }
                return boxTypes[i].gameObject;
            }
        }
        if (!isFind)
            Debug.Log("没找到弹框" + boxName);
        CurBox = null;
        return null;
    }
    public void CloseBox(string boxName)
    {
        for (int i = 0; i < boxTypes.Count; i++)
        {
            if (boxTypes[i].boxName == boxName)
                boxTypes[i].gameObject.SetActive(false);
        }
    }
    public void CloseBox(GameObject go)
    {
        for (int i = 0; i < boxTypes.Count; i++)
        {
            if(go==boxTypes[i].gameObject)
            {
                boxTypes[i].gameObject.SetActive(false);
            }
        }
    }
    #region 编辑器
    //public void FindBox()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        if (transform.GetChild(i).GetComponent<MessageBox>()!=null)
    //        {
    //            bool isEx = false;
    //            for (int j= 0; j < boxTypes.Count; j++)
    //            {
    //                if(boxTypes[j].box == transform.GetChild(i).GetComponent<MessageBox>())
    //                {
    //                    isEx = true;
    //                }
    //            }
    //            if(!isEx)
    //            {
    //                boxTypes.Add()
    //            }
               
    //        }
    //    }
    //}
    #endregion
}
