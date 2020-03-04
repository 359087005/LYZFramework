


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.ExternalCall("setScriptFromPlugin", "");//向平台传值
	}

    void getStrFromPlatform(string arg)				    //用于从平台获取数据，
    {
        
    }

    IEnumerator SendData(string dataStr)
    {
        string url = "http://hangtian.nwpu.owvlab.net/virexp/extra/dataParse";

        //byte[] _byte = System.Text.Encoding.UTF8.GetBytes(str);
        //string _str = System.Text.Encoding.UTF8.GetString(_byte);

        WWWForm form = new WWWForm();
        form.AddField("uuid", dataStr);
        WWW www = new WWW(url, form);

        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            print(www.error);

    }
	
}





























