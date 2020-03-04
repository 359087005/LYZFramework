using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Lazy
{
    public class ScreenShotTool : MonoBehaviour
    {
        private static int PaizhaoCount = 0;

        /// <summary>  
        /// 对相机截图。   
        /// </summary>  
        /// <returns>The screenshot2.</returns>  
        /// <param name="camera">Camera.要被截屏的相机</param>  
        /// <param name="rect">Rect.截屏的区域</param>  
        public Texture2D CaptureCamera(Camera camera, Rect rect, string path)
        {
            CreatPath(path);
            RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
            camera.targetTexture = rt;
            camera.Render();
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
            Debug.Log(System.DateTime.Now.Month.ToString());
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = /*Application.dataPath*/ path + "/ " + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + "时" + System.DateTime.Now.Minute.ToString() + "分" + System.DateTime.Now.Second.ToString() + "秒" + ".png";
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("截屏了一张照片: {0}", filename));
            return screenShot;
        }
        public Texture2D CaptureCamera(Camera camera, Rect rect)
        {
            RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
            camera.targetTexture = rt;
            camera.Render();
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
            return screenShot;
        }
        public Texture2D CaptureScreen()
        {
            return ScreenCapture.CaptureScreenshotAsTexture();
        }
        public void CaptureScreen(string path)
        {
            CreatPath(path);
            string filename = "";
            filename = path;
            ScreenCapture.CaptureScreenshot(filename, 0);
        }
        private void CreatPath(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);//不存在就创建目录
            }
        }
    }
}
