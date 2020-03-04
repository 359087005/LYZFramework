using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdaptionRuler : MonoBehaviour
{
    [SerializeField] float scale = 1;
    [SerializeField] float x_min;
    [SerializeField] float x_max;
    [SerializeField] float y_min;
    [SerializeField] float y_max;
    [SerializeField] private float x_width;
    [SerializeField] private float y_hight;
    [SerializeField] private float intervalCount = 100;
    [SerializeField] private GameObject x_temp;
    [SerializeField] private GameObject y_temp;
    List<RulerInfo> x_rulerInfos = new List<RulerInfo>();
    List<RulerInfo> y_rulerInfos = new List<RulerInfo>();
    float x_tempwidth;
    float y_temphight;
    float x_interval;
    float y_interval;
    public float X_Min
    {
        get
        {
            return x_min;
        }
        set
        {
            x_min = value;
        }
    }
    public float X_Max
    {
        get
        {
            return x_max;
        }
        set
        {
            x_max = value;
        }
    }
    public float Y_Min
    {
        get
        {
            return y_min;
        }
        set
        {
            y_min = value;
        }
    }
    private void Update()
    {
        InitRuler(x_min, x_max, y_min, y_max);
        ChangePointScale();
        ScaleFunc(scale);
    }
    //private void FindNearInt
    private void ScaleFunc(float value)
    {
        x_tempwidth = x_width * value;
        y_temphight = y_hight * value;
    }
    public void InitRuler(float x_min, float x_max,float y_min ,float y_max)
    {
        float x_long = 0;
        float y_long = 0;

        x_interval = x_tempwidth / intervalCount;
        y_interval = y_temphight / intervalCount;

        x_long = Distance(x_min, x_max);
        y_long = Distance(y_min, y_max);

        if(x_rulerInfos.Count==0)
        {
            for (int i = 0; i < intervalCount+1; i++)
            {
                GameObject x_go = Instantiate(x_temp);
                x_go.SetActive(true);
                x_go.transform.parent = x_temp.transform.parent;
                x_rulerInfos.Add(new RulerInfo() { value = (x_long / intervalCount)* (i) + x_min, scale = x_go });

                GameObject y_go = Instantiate(y_temp);
                y_go.SetActive(true);
                y_go.transform.parent = y_temp.transform.parent;
                y_rulerInfos.Add(new RulerInfo() { value = (y_long / intervalCount) * (i)+y_min, scale = y_go });
            }
        }
    }
    private float Distance (float min,float max) 
    {
        float temp = 0;
        if (min >= 0 && max >= 0)
        {
            temp = max - min;
        }
        if (min<= 0 && max >= 0)
        {
            temp = Mathf.Abs(min) + max;
        }
        if (min <= 0 && max <= 0)
        {
            temp = Mathf.Abs(min) - Mathf.Abs(max);
        }
       
        return temp;
    }
   
    public void ChangePointScale()
    {
        for (int i = 0; i < x_rulerInfos.Count; i++)
        {
            x_rulerInfos[i].scale.transform.position = new Vector3(x_temp.transform.position.x + i * x_interval, x_temp.transform.position.y, x_temp.transform.position.z);
            y_rulerInfos[i].scale.transform.position = new Vector3(y_temp.transform.position.x, y_temp.transform.position.y + i * y_interval, y_temp.transform.position.z);
           
            if (i%10==0)
            {
                if (x_rulerInfos[i].scale.transform.GetChild(0).GetComponent<Text>() != null)
                {
                    x_rulerInfos[i].scale.transform.GetChild(0).GetComponent<Text>().text = x_rulerInfos[i].value.ToString();
                    y_rulerInfos[i].scale.transform.GetChild(0).GetComponent<Text>().text = y_rulerInfos[i].value.ToString();
                }

                x_rulerInfos[i].scale.GetComponent<RectTransform>().sizeDelta = new Vector2(x_temp.GetComponent<RectTransform>().sizeDelta.x, x_temp.GetComponent<RectTransform>().sizeDelta.y*2);
                x_rulerInfos[i].scale.transform.position = new Vector3(x_rulerInfos[i].scale.transform.position.x,  x_temp.transform.position.y - x_temp.GetComponent<RectTransform>().sizeDelta.y/2, x_rulerInfos[i].scale.transform.position.z);
                y_rulerInfos[i].scale.GetComponent<RectTransform>().sizeDelta = new Vector2(y_temp.GetComponent<RectTransform>().sizeDelta.x*2, y_temp.GetComponent<RectTransform>().sizeDelta.y);
                y_rulerInfos[i].scale.transform.position = new Vector3(y_temp.transform.position.x- y_temp.GetComponent<RectTransform>().sizeDelta.x/2, y_rulerInfos[i].scale.transform.position.y , y_rulerInfos[i].scale.transform.position.z);
            }
        }
    }
}
[System.Serializable]
public class RulerInfo
{
    public float value;
    public GameObject scale;
}
