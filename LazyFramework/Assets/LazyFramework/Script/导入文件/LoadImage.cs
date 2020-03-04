using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Lazy
{

    public class LoadImage : MonoBehaviour
    {
        [SerializeField] Button addImageBtn;
        [SerializeField] GameObject tempBtn;
        [SerializeField] List<LoadImageInfo> loadImageInfos = new List<LoadImageInfo>();
        [SerializeField] string curPath = "";
        void Start()
        {
            addImageBtn.onClick.AddListener(() =>
            {
                new ImportFile().OpenLocalImage(out curPath, () => { StartCoroutine(WWWLoad(curPath)); });
            });
        }
        IEnumerator WWWLoad(string url)
        {
            Texture2D tex;
            tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            using (WWW www = new WWW(url))
            {
                yield return www;
                GameObject btn_temp = Instantiate(tempBtn);
                btn_temp.transform.parent = tempBtn.transform.parent;
                btn_temp.transform.localScale = new Vector3(1, 1, 1);
                btn_temp.SetActive(true);
                if (btn_temp.transform.GetChild(0).GetComponent<RawImage>() == null)
                {
                    btn_temp.transform.GetChild(0).gameObject.AddComponent<RawImage>();
                }
                btn_temp.transform.GetChild(0).GetComponent<RawImage>().texture = tex;

                www.LoadImageIntoTexture(tex);
                loadImageInfos.Add(new LoadImageInfo(btn_temp.GetComponent<Button>(), tex));
                //GetComponent<Renderer>().material.mainTexture = tex;
                // 或者是下面这样
                // GetComponent<Renderer>().material.mainTexture =www.texture;
            }
        }
    }
    [System.Serializable]
    public class LoadImageInfo
    {
        private bool isSelect = false;
        public Button btn;
        public Texture2D texture2D;
        public LoadImageInfo(Button _btn, Texture2D _texture2D)
        {
            btn = _btn;
            texture2D = _texture2D;
        }
    }
}