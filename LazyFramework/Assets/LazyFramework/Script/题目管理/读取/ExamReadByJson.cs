using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Lazy
{
    public class ExamReadByJson : ExamConfigFilesBase
    {
        Response<ExamJson> responses = new Response<ExamJson>();
        public override List<ExamData> LoadExam(string str)
        {
            Analysis_Json(str);
            return LoadFunc();
        }

        private List<ExamData> LoadFunc()
        {
            List<ExamData> temp = new List<ExamData>();
            for (int i = 0; i < responses.list.Count; i++)
            {
                temp.Add(new ExamData(
                int.Parse(responses.list[i].题号),
                int.Parse(responses.list[i].组),
                (ExamType)Enum.Parse(typeof(ExamType), responses.list[i].题目类型),
                float.Parse(responses.list[i].分值),
                responses.list[i].题目,
                responses.list[i].解析,
                AnalysisSign_Json(responses.list[i].选项),
                AnalysisSign_Json(responses.list[i].答案)
                ));
            }
            return temp;
        }
        private List<string> AnalysisSign_Json(string str)
        {
            List<string> temp = new List<string>();
            string[] tempstr = Regex.Split(str, "&");
            for (int i = 1; i < tempstr.Length; i++)
            {
                temp.Add(tempstr[i]);
            }
            return temp;
        }
        private void Analysis_Json(string jsonStr)
        {
            // 将Json中的数组用一个list包裹起来，变成一个Wrapper对象
            jsonStr = "{ \"list\": " + jsonStr + "}";
            responses = JsonUtility.FromJson<Response<ExamJson>>(jsonStr);
        }
        public class Response<T>
        {
            public List<T> list;
        }
        [System.Serializable]
        public class ExamJson
        {
            public string 题号;
            public string 组;
            public string 题目类型;
            public string 题目;
            public string 选项;
            public string 答案;
            public string 分值;
            public string 解析;
        }
    }
   
}

