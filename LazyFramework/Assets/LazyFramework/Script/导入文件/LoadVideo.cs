using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
namespace Lazy
{

    public class LoadVideo : MonoBehaviour
    {
        [SerializeField] Button addVideoBtn;
        [SerializeField] GameObject tempBtn;
        [SerializeField] bool isLoop = true;
        [SerializeField] List<VideoInfo> videoInfos = new List<VideoInfo>();
        [SerializeField] string curPath = "";
        private void Start()
        {
            addVideoBtn.onClick.AddListener(() =>
            {
                new ImportFile().OpenLocalVideo(out curPath, () => { AddInfo(curPath); });
            });
        }
        private void AddInfo(string _url)
        {
            GameObject video_Temp = new GameObject();
            video_Temp.AddComponent<VideoPlayer>().url = _url;
            video_Temp.GetComponent<VideoPlayer>().Prepare();
            GameObject btn_temp = Instantiate(tempBtn);
            btn_temp.transform.parent = tempBtn.transform.parent;
            btn_temp.transform.localScale = new Vector3(1, 1, 1);
            btn_temp.SetActive(true);

            if (btn_temp.GetComponent<RawImage>() == null)
            {
                btn_temp.gameObject.AddComponent<RawImage>();
            }
            RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
            video_Temp.GetComponent<VideoPlayer>().targetTexture = renderTexture;
            video_Temp.GetComponent<VideoPlayer>().isLooping = isLoop;
            btn_temp.GetComponent<RawImage>().texture = renderTexture;
            videoInfos.Add(new VideoInfo(btn_temp.GetComponent<Button>(), video_Temp.GetComponent<VideoPlayer>(), renderTexture, _url));
            StartCoroutine(CheckPrepare(video_Temp.GetComponent<VideoPlayer>()));
        }
        IEnumerator CheckPrepare(VideoPlayer videoPlayer)
        {
            yield return new WaitForFixedUpdate();
            while (true)
            {
                if (videoPlayer.isPrepared)
                {
                    videoPlayer.Play();
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
    [System.Serializable]
    public class VideoInfo
    {
        private bool isSelect = false;
        public Button btn;
        public VideoPlayer videoPlayer;
        public static RenderTexture renderTexture;
        public string url;
        public VideoInfo(Button _btn, VideoPlayer _videoPlayer, RenderTexture _renderTexture, string _url)
        {
            btn = _btn;
            videoPlayer = _videoPlayer;
            renderTexture = _renderTexture;
            url = _url;
        }
    }
}