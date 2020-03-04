using UnityEngine;

using System.Collections;

using System.Runtime.InteropServices;

public class DialogTest : MonoBehaviour

{

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Open"))
        {

            new OpenFileName().Open("题目", "Excel文件(*.xlsx)\0 *.xlsx");
        }
    }
}