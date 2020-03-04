using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class JiePin : MonoBehaviour {

    public Camera camera;
    public Image image;
	void Start () {
        //CameraJiePinAction();
        StartCoroutine("ScreenshotAction");
    }
	
	
    /// <summary>
    /// 摄像机截图操作
    /// </summary>
    void CameraJiePinAction()
    {
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        // 创建一个RenderTexture对象  
        RenderTexture renderText = new RenderTexture((int)rect.width, (int)rect.height, 24);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = renderText;
        camera.Render();
        RenderTexture.active = renderText;

        Texture2D t2d = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        t2d.ReadPixels(new Rect(0, 0, (int)rect.width, (int)rect.height), 1, 1);
        t2d.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = t2d.EncodeToPNG();

        //字节转字符
        string texStr = System.Convert.ToBase64String(bytes);

        //赋值图片
        image.sprite = Sprite.Create(t2d, rect, Vector3.zero);


#if UNITY_EDITOR
        string filename = Application.dataPath + "/" + "01.jpg";
        System.IO.File.WriteAllBytes(filename, bytes);
#endif

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    /// <summary>
    /// 屏幕截图操作
    /// </summary>
    /// <returns></returns>
    IEnumerator  ScreenshotAction()
    {
        //等待一帧结束后才能截屏
        yield return new WaitForEndOfFrame();

        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        Texture2D t2d = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        // 读取屏幕像素信息并存储为纹理数据，  
        t2d.ReadPixels(rect, 1, 1);
        t2d.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = t2d.EncodeToPNG();

        //字节转字符
        string texStr = System.Convert.ToBase64String(bytes);

        //赋值图片
        image.sprite = Sprite.Create(t2d, rect, Vector3.zero);

#if UNITY_EDITOR
        string filename = Application.dataPath + "/" + "01.jpg";
        System.IO.File.WriteAllBytes(filename, bytes);
#endif

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

    }




}
