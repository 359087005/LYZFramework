using UnityEngine;
using System.Collections;

public class FullScreen : MonoBehaviour {

    public GameObject quanPingBtn;
    public GameObject tuiChuQuanPingBtn;
    void Start()
    {
        InitFunc();
    }
    private void InitFunc()
    {
        if (quanPingBtn == null || tuiChuQuanPingBtn == null)
        {
            Debug.LogError("没有注册全屏按钮");
            return;
        }
#if UNITY_WEBGL
        Debug.Log(Screen.fullScreen);
        if(Screen.fullScreen)
        {
            quanPingBtn.SetActive(false);
            tuiChuQuanPingBtn.SetActive(true);
        }
       else
        {
            quanPingBtn.SetActive(true);
            tuiChuQuanPingBtn.SetActive(false);
        }
#endif
#if UNITY_STANDALONE_WIN
        quanPingBtn.SetActive(false);
        tuiChuQuanPingBtn.SetActive(false);
#endif
        if (quanPingBtn != null)
            quanPingBtn.SetClick(QuanPingScreen);
        if (tuiChuQuanPingBtn != null)
            tuiChuQuanPingBtn.SetClick(TuiChuQuanPingScreen);
    }

    public void QuanPingScreen(GameObject go)
    {
        Resolution[] resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        Screen.fullScreen = true;
        quanPingBtn.SetActive(false);
        tuiChuQuanPingBtn.SetActive(true);
    }
    public void TuiChuQuanPingScreen(GameObject go)
    {
        Screen.SetResolution(960, 540, false);
        Screen.fullScreen = false;
        quanPingBtn.SetActive(true);
        tuiChuQuanPingBtn.SetActive(false);
    }

}
