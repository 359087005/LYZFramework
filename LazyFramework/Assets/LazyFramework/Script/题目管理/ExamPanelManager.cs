using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    public class ExamPanelManager : MonoSingleton<ExamPanelManager>
    {
        [Header("*文字题模板（子物体需要有1.题目2.选项3.解析")]
        [SerializeField] ExamPanelBase textTemplate;
        [Header("*确认按钮")]
        [SerializeField] Button confirmTemp;
        [Header("取消按钮")]
        [SerializeField] Button cancleTemp;
        [SerializeField] List<ExamPanelBase> customExam = new List<ExamPanelBase>();
        [SerializeField] List<ExamPanelBase> textExam = new List<ExamPanelBase>();
        public List<ExamPanelBase> CurExam
        {
            get
            {
                List<ExamPanelBase> temp = new List<ExamPanelBase>();
                for (int i = 0; i < textExam.Count; i++)
                {
                    temp.Add(textExam[i]);
                }
                for (int i = 0; i < customExam.Count; i++)
                {
                    if (customExam[i].gameObject.activeSelf)
                        temp.Add(customExam[i]);
                }
                return temp;
            }
        }
        bool isAllAnswer = false;
        /// <summary>
        /// 当确认时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="allAns">是否全部作答</param>
        public delegate void ComfirmEventHandler(object sender, bool allAns);
        public static event ComfirmEventHandler OnComfirm;
        public delegate void CancelEventHandler(object sender, EventArgs e);
        public static event CancelEventHandler OnCancel;
        int beginIndex;
        int lastIndex;
        private void InitCustom()
        {
            for (int i = 0; i < textTemplate.transform.parent.childCount; i++)
            {
                if (textTemplate.transform.parent.GetChild(i).GetComponent<ExamPanelBase>()!=null)
                {
                    customExam.Add(textTemplate.transform.parent.GetChild(i).GetComponent<ExamPanelBase>());
                }
            }
            customExam.Remove(textTemplate);
        }
        public void CreatCustomExam(int examId)
        {
            for (int i = 0; i < customExam.Count; i++)
            {
                if (customExam[i].examPanelInfo.id == examId)
                {
                    customExam[i].gameObject.SetActive(true);
                    customExam[i].transform.SetAsLastSibling();
                }
            }
        }
        public void CreatTextExam(int examId)
        {
            ExamPanelBase examTemp = Instantiate(textTemplate);
            textExam.Add(examTemp);
            examTemp.examPanelInfo.id = examId;
            examTemp.transform.parent = textTemplate.transform.parent;
            examTemp.transform.localScale = new Vector3(1, 1, 1);
            List<string> option = ExamMgr.Instance.GetExam(examId).option;

            for (int j = 0; j < examTemp.transform.childCount; j++)
            {
                switch (examTemp.transform.GetChild(j).name)
                {
                    case "题目":
                        if (examTemp.transform.GetChild(j).GetComponent<Text>() != null)
                        {
                            textExam[textExam.Count - 1].examPanelInfo.topic = examTemp.transform.GetChild(j).GetComponent<Text>();
                            textExam[textExam.Count - 1].examPanelInfo.topic.gameObject.SetActive(true);
                            textExam[textExam.Count - 1].examPanelInfo.topic.text = ExamMgr.Instance.GetExam(examId).examContent;
                        }
                        break;
                    case "选项":
                        if (examTemp.transform.GetChild(j).GetComponent<Toggle>() != null)
                        {
                            for (int m = 0; m < option.Count; m++)
                            {
                                Toggle optionTemp = Instantiate(examTemp.transform.GetChild(j)).GetComponent<Toggle>();
                                optionTemp.transform.parent = examTemp.transform;
                                optionTemp.transform.localScale = new Vector3(1, 1, 1);
                                optionTemp.gameObject.SetActive(true);
                                optionTemp.GetComponentInChildren<Text>().text = option[m];
                                textExam[textExam.Count - 1].examPanelInfo.examOptionInfo.Add(new ExamOptionInfo(optionTemp, new System.Text.ASCIIEncoding().GetString(new byte[] { (byte)(m + 65) })));
                            }
                        }
                        break;
                    case "解析":
                        if (examTemp.transform.GetChild(j).GetComponent<Text>() != null)
                        {
                            textExam[textExam.Count - 1].examPanelInfo.analysis = examTemp.transform.GetChild(j).GetComponent<Text>();
                            textExam[textExam.Count - 1].examPanelInfo.analysis.transform.SetAsLastSibling();
                            textExam[textExam.Count - 1].examPanelInfo.analysis.gameObject.SetActive(true);
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 当确认按钮按下时  （如果遇到特殊的判断逻辑，继承这个管理类 并重写这个方法）
        /// </summary>
        public virtual void OnComfirmClick()
        {
            JugeExam();
        }
        /// <summary>
        /// 当取消按钮按下时
        /// </summary>
        public virtual void OnCancleClick()
        {
            this.gameObject.SetActive(false);
        }
        private void JugeExam()
        {
            bool isAllAns = true;
            List<ExamPanelBase> allExam = new List<ExamPanelBase>();
            allExam = CurExam;
            for (int i = 0; i < allExam.Count; i++)
            {
                if(AnalysisResult(allExam[i])=="")
                {
                    allExam[i].examPanelInfo.analysis.text = "未回答！";
                    isAllAns = false;
                    continue;
                }
                if(AnalysisResult(allExam[i])==ExamMgr.Instance.GetExam(allExam[i].examPanelInfo.id).result[0])
                {
                    allExam[i].examPanelInfo.analysis.text = "回答正确！";
                }
                else
                {
                    allExam[i].examPanelInfo.analysis.text = "回答错误！"+ ExamMgr.Instance.GetExam(allExam[i].examPanelInfo.id).analysis;
                }
                ExamMgr.Instance.ChangeExamState(allExam[i].examPanelInfo.id, new List<string> { AnalysisResult(allExam[i])});
            }
            if (OnComfirm != null)
            {
                OnComfirm.Invoke(this, isAllAns);
            }
        }
        protected string AnalysisResult(ExamPanelBase _examPanelBase)
        {
            string temp = "";
            for (int i = 0; i < _examPanelBase.examPanelInfo.examOptionInfo.Count; i++)
            {
                if (_examPanelBase.examPanelInfo.examOptionInfo[i].isTrue)
                {
                    temp += _examPanelBase.examPanelInfo.examOptionInfo[i].tag;
                }
            }
            return temp;
        }
      
        public void EmptyPostion()
        {
            for (int i = 0; i < CurExam.Count; i++)
            {
                if(CurExam[i].emptyObject!=null)
                {
                    CurExam[i].emptyObject.transform.SetAsLastSibling();
                    CurExam[i].emptyObject.SetActive(true);
                }
            }
        }
        /// <summary>
        /// 清空面板中所有题目
        /// </summary>
        public void ClearAll()
        {
            for (int i = 0; i < textExam.Count; i++)
            {
                Destroy(textExam[i].gameObject);
            }
            textExam.Clear();
            for (int i = 0; i < customExam.Count; i++)
            {
                customExam[i].gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 设置单选组
        /// </summary>
        public void SetSingleSelectGroup()
        {
            for (int i = 0; i < CurExam.Count; i++)
            {
                if(ExamMgr.Instance.GetExam(CurExam[i].examPanelInfo.id).examType == ExamType.自定义单选
                    || ExamMgr.Instance.GetExam(CurExam[i].examPanelInfo.id).examType== ExamType.单选)
                {
                    if(CurExam[i].gameObject.GetComponent<ToggleGroup>()==null)
                    {
                        CurExam[i].gameObject.AddComponent<ToggleGroup>();
                        for (int j = 0; j < CurExam[i].examPanelInfo.examOptionInfo.Count; j++)
                        {
                            CurExam[i].examPanelInfo.examOptionInfo[j].toggle.group = CurExam[i].gameObject.GetComponent<ToggleGroup>();
                        }
                    }
                }
               
            }
        }
        /// <summary>
        /// 通过组号，显示一组题目
        /// </summary>
        /// <param name="_group"></param>
        public void ShowExamByGroup(int _group)
        {
            ClearAll();
            List<ExamData> temp = ExamMgr.Instance.GetExamByGroup(_group);
            for (int i = 0; i < temp.Count; i++)
            {
                switch (temp[i].examType)
                {
                    case ExamType.自定义单选:
                        CreatCustomExam(temp[i].id);
                        break;
                    case ExamType.自定义多选:
                        CreatCustomExam(temp[i].id);
                        break;
                    case ExamType.单选:
                        CreatTextExam(temp[i].id);
                        break;
                    case ExamType.多选:
                        CreatTextExam(temp[i].id);
                        break;
                }
            }
        }
        /// <summary>
        /// 展示题目
        /// </summary>
        /// <param name="_beginIndex">展示的第一题的题号</param>
        /// <param name="_lastIndex">展示的最后一题的题号</param>
        public void ShowExam(int _beginIndex,int _lastIndex)
        {
            //清空之前所有题目
            ClearAll();
            beginIndex = _beginIndex;//起始题号
            lastIndex = _lastIndex;//终止题号
            for (int i = 0; i < lastIndex - beginIndex +1 ; i++)
            {
                switch (ExamMgr.Instance.GetExam(_beginIndex + i).examType)
                {
                    case ExamType.自定义单选:
                        CreatCustomExam(_beginIndex + i);
                        break;
                    case ExamType.自定义多选:
                        CreatCustomExam(_beginIndex + i);
                        break;
                    case ExamType.单选:
                        CreatTextExam(_beginIndex + i);
                        break;
                    case ExamType.多选:
                        CreatTextExam(_beginIndex + i);
                        break;
                }
            }
            //设置单选组
            SetSingleSelectGroup();
            //设置空格
            EmptyPostion();
        }
        private void Start()
        {
            //InitCustom();
            confirmTemp.onClick.AddListener(() => { OnComfirmClick(); });
            cancleTemp.onClick.AddListener(() => { OnCancleClick(); });
        }
    }
  
}

