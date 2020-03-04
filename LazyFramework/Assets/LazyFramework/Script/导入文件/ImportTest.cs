using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportTest : MonoBehaviour
{
    [SerializeField]private string path;
    private void OnGUI()
    {
        if (GUILayout.Button("按钮"))
        {
            //path = new ImportFile().OpenLocalVideo();
        }
    }
}
