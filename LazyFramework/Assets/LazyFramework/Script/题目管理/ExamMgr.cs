using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    public class ExamMgr : MonoSingleton<ExamMgr>
    {
        [SerializeField] List<ExamData> examDatas = new List<ExamData>();
        private float aggregateScore = 0;
        public float AggregateScore
        {
            get
            {
                float temp = 0;
                for (int i = 0; i < ExamDatas.Count; i++)
                {
                    temp += ExamDatas[i].myScore;
                }
                return temp;
            }
        }
        public List<ExamData> ExamDatas
        {
            get
            {
                return examDatas;
            }
            set
            {
                examDatas = value;
            }
        }
        /// <summary>
        /// 获取内存中全部的题目数量
        /// </summary>
        public int GetExamCount
        {
            get
            {
                return examDatas.Count;
            }
        }
        /// <summary>
        /// 动态增加题目
        /// </summary>
        /// <param name="examData"></param>
        public void AddExam(ExamData examData)
        {
            examDatas.Add(examData);
        }
        /// <summary>
        /// 设置题目
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_isTrue"></param>
        /// <param name="_myAnswer"></param>
        public void SetExam(int _id,bool _isTrue,List<string> _myAnswer)
        {
            ExamDatas[_id].isTrue = _isTrue;
            ExamDatas[_id].myAnswer = _myAnswer;
            examDatas[_id].ansCount++;
            if(_isTrue)
            {
                ExamDatas[_id].myScore = ExamDatas[_id].score;
            }
        }
        /// <summary>
        /// 通过组号获取一组题目
        /// </summary>
        /// <param name="_group">组</param>
        /// <returns></returns>
        public List<ExamData> GetExamByGroup(int _group)
        {
            List<ExamData> temp = new List<ExamData>();
            for (int i = 0; i < ExamDatas.Count; i++)
            {
                if (ExamDatas[i].group == _group)
                {
                    temp.Add(ExamDatas[i]);
                }
            }
            return temp;
        }
        /// <summary>
        /// 获取当前所有错题
        /// </summary>
        /// <returns></returns>
        public List<ExamData> GetErrorExam()
        {
            List<ExamData> temp = new List<ExamData>();
            for (int i = 0; i < ExamDatas.Count; i++)
            {
                if(!ExamDatas[i].isTrue)
                {
                    temp.Add(ExamDatas[i]);
                }
            }
            return temp;
        }
        /// <summary>
        /// 通过题号获取题目
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public ExamData GetExam(int _id)
        {
            for (int i = 0; i < examDatas.Count; i++)
            {
                if (examDatas[i].id == _id)
                {
                    return examDatas[i];
                }
            }
            Debug.LogError("没有找到题目：" + _id);
            return null;
        }
        /// <summary>
        /// 改变题目状态（此方法可以重写，当前预制的逻辑为：只计算第一次回答的成绩）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_myAnswer"></param>
        public virtual void ChangeExamState(int id, List<string> _myAnswer)
        {
            for (int i = 0; i < examDatas.Count; i++)
            {
                if (examDatas[i].id == id)
                {
                    if (++examDatas[i].ansCount <= 1)
                    {
                        examDatas[i].myAnswer = _myAnswer;
                        Judge(i);
                    }
                }
            }
        }
       
        /// <summary>
        /// 判断回答是否正确并作出相应判断
        /// </summary>
        /// <param name="index"></param>
        private void Judge(int index)
        {
            if (examDatas[index].myAnswer[0] == examDatas[index].result[0])
            {
                examDatas[index].myScore = examDatas[index].score;
                examDatas[index].isTrue = true;
            }
            else
            {
                examDatas[index].myScore = 0;
                examDatas[index].isTrue = false;
            }
        }
    }
    /// <summary>
    /// 题目类型
    /// </summary>
    public enum ExamType
    {
        单选,
        多选,
        判断,
        自定义单选,
        自定义多选,
        自定义判断,
    }

    /// <summary>
    /// 题目数据结构
    /// </summary>
    [System.Serializable]
    public class ExamData
    {
        /// <summary>
        /// 题号
        /// </summary>
        public int id;
        /// <summary>
        /// 组
        /// </summary>
        public int group;
        /// <summary>
        /// 题目类型
        /// </summary>
        public ExamType examType;
        /// <summary>
        /// 分数
        /// </summary>
        public float score;
        /// <summary>
        /// 题目内容
        /// </summary>
        [TextArea(5,5)]
        public string examContent;
        /// <summary>
        /// 题目解析
        /// </summary>
        public string analysis;
        /// <summary>
        /// 题目选项
        /// </summary>
        public List<string> option = new List<string>();
        /// <summary>
        /// 题目结果
        /// </summary>
        public List<string> result = new List<string>();
        /// <summary>
        /// 我的答案
        /// </summary>
        public List<string> myAnswer = new List<string>();
        /// <summary>
        /// 该题目是否回答正确
        /// </summary>
        public bool isTrue = false;
        /// <summary>
        /// 题目做过次数
        /// </summary>
        public int ansCount;
        /// <summary>
        /// 获得的分数
        /// </summary>
        public float myScore = 0;
        /// <summary>
        /// 初始化题目数据
        /// </summary>
        /// <param name="_id">题号</param>
        /// <param name="_score">该题目分值</param>
        /// <param name="_examContent">题目内容</param>
        /// <param name="_analysis">题目答案解析</param>
        /// <param name="_option">题目选项</param>
        /// <param name="_result">题目答案</param>
        public ExamData(int _id,int _group,ExamType _examType,float _score,string _examContent,string _analysis,List<string> _option,List<string> _result)
        {
            id = _id;
            group = _group;
            examType = _examType;
            score = _score;
            examContent = _examContent;
            analysis = _analysis;
            option = _option;
            result = _result;
        }
        /// <summary>
        /// 初始化题目数据(无解析)
        /// </summary>
        /// <param name="_id">题号</param>
        /// <param name="_score">该题目分值</param>
        /// <param name="_examContent">题目内容</param>
        /// <param name="_option">题目选项</param>
        /// <param name="_result">题目答案</param>
        public ExamData(int _id, ExamType _examType, float _score, string _examContent, List<string> _option, List<string> _result)
        {
            id = _id;
            examType = _examType;
            score = _score;
            examContent = _examContent;
            option = _option;
            result = _result;
        }
        /// <summary>
        /// 初始化题目数据(无解无分)
        /// </summary>
        /// <param name="_id">题号</param>
        /// <param name="_score">该题目分值</param>
        /// <param name="_examContent">题目内容</param>
        /// <param name="_option">题目选项</param>
        /// <param name="_result">题目答案</param>
        public ExamData(int _id, ExamType _examType, string _examContent, List<string> _option, List<string> _result)
        {
            id = _id;
            examType = _examType;
            examContent = _examContent;
            option = _option;
            result = _result;
        }
        /// <summary>
        /// 初始化题目数据(无解析、答案、分数)（可用于问卷形式）
        /// </summary>
        /// <param name="_id">题号</param>
        /// <param name="_score">该题目分值</param>
        /// <param name="_examContent">题目内容</param>
        /// <param name="_option">题目选项</param>
        /// <param name="_result">题目答案</param>
        public ExamData(int _id, string _examContent, List<string> _option)
        {
            id = _id;
            examContent = _examContent;
            option = _option;
        }
    }
}

