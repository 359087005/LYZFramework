using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    public class ContinuousCapture : MonoSingleton<ContinuousCapture>
    {
        [SerializeField] float frequency = 0.5f;
        [SerializeField] bool isBegin = false;
        [SerializeField] Rect rect = new Rect(0, 0, 1920, 1080);
        public static List<Texture2D> imageInfos = new List<Texture2D>();

        public void BeginRec()
        {
            isBegin = true;
            StartCoroutine(Rec());
        }
        public void StopRec()
        {
            isBegin = false;
        }
        IEnumerator Rec()
        {
            while (isBegin)
            {
                yield return new WaitForSeconds(frequency);
                imageInfos.Add(new ScreenShotTool().CaptureCamera(Camera.main, rect));
            }
        }
      
    }
    [System.Serializable]
    public class ImageInfo
    {
        public Texture2D texture2D;
        public ImageInfo(Texture2D _texture2D)
        {
            texture2D = _texture2D;
        }
    }
}
