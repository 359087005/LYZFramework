using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lazy
{
    public abstract class ExamImportBase : MonoBehaviour
    {
        [SerializeField] protected string path;
        [SerializeField] private ExamMgr examMgr;
        protected ExamMgr ExamMgr
        {
            get
            {
                if (examMgr == null)
                {
                    examMgr = FindObjectOfType<ExamMgr>();
                    if (examMgr == null)
                    {
                        examMgr = gameObject.AddComponent<ExamMgr>();
                    }
                }
                if (examMgr == null)
                    Debug.LogError("未找到脚本ExamMgr，请手动挂载");
                return examMgr;
            }
        }
        protected int curImport;
        private void Start()
        {
            if (examMgr == null)
                examMgr = ExamMgr.Instance;

            if (ExamMgr.ExamDatas.Count != 0)
                AddExam();
        }
        public virtual void ClearExam()
        {
            ExamMgr.ExamDatas.Clear();
        }
        public abstract void AddExam();
    }
}


