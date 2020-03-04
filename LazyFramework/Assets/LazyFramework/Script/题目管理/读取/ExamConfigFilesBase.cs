using Lazy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ExamConfigFilesBase : MonoBehaviour
{
    public abstract List<ExamData> LoadExam(string str);
}
