using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimlpeExam : MonoBehaviour
{
    [Header("确定")]
    public Button btn_Sure;
    [Header("取消")]
    public Button btn_cancel;
    [Header("解析")]
    public Text txt_analysis;
    [Header("答案")]
     public List<string> result = new List<string>();
    public string analysis;
    public float score = 2;
    [HideInInspector]public float myScore = 0;
    bool isTrue = false;
    [HideInInspector] public int answerCount = 0;
    [SerializeField] public List<Toggle> toggles = new List<Toggle>();
    [SerializeField] public List<string> myAnswer = new List<string>();
   
    public delegate void BtnVoidEventHandler(object sender, bool isTrue);
    public event BtnVoidEventHandler OnSureDownEvt;
    public event BtnVoidEventHandler OnCancelDownEvt;
    public virtual bool OnSureDown()
    {
        bool temp = true;
        if (myAnswer.Count != result.Count) temp = false;
        answerCount++;
        for (int i = 0; i < result.Count; i++)
        {
            if (!myAnswer.Contains(result[i]))
            {
                temp = false;
            }
        }
        if (txt_analysis != null)
        {
            txt_analysis.gameObject.SetActive(true);
            if (temp)
            {
                txt_analysis.text = "回答正确！";
                myScore = score;
            }
            else
                txt_analysis.text = "回答错误！" + analysis;
        }
        btn_cancel.interactable = true;
        btn_Sure.interactable = false;
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].interactable = false;
        }
        if(OnSureDownEvt!=null)
        {
            OnSureDownEvt.Invoke(this, isTrue);
        }
        return temp;
    }
    public void OnCancelDown()
    {
        if (OnCancelDownEvt != null)
        {
            OnCancelDownEvt.Invoke(this, isTrue);
        }
    }
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Toggle>() != null)
            {
                toggles.Add(transform.GetChild(i).GetComponent<Toggle>());
                toggles[i].onValueChanged.AddListener((a) =>
                {
                    OnSelectDown();
                });
            }
        }
        btn_Sure.onClick.AddListener(() => { OnSureDown(); });
        btn_cancel.onClick.AddListener(() =>{OnCancelDown();});
    }
    public void SetSelectActive(bool bo)
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].interactable = bo;
        }
    }
    private void OnSelectDown()
    {
        myAnswer.Clear();

        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
            {
                myAnswer.Add(new System.Text.ASCIIEncoding().GetString(new byte[] { (byte)(i + 65) }));
            }
        }
    }
}
