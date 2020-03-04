using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePlayer : MonoBehaviour
{
    [SerializeField] public GameObject tempImage;
    [SerializeField] float playerSpeed = 1;
    [SerializeField] int jumpCount = 1;//正为顺序 负为逆序
    [SerializeField] int curIndex;
    [SerializeField] List<GameObject> images = new List<GameObject>();
    [SerializeField] private PlayerType _playerType;
    public PlayerType _PlayerType
    {
        get
        {
            return _playerType;
        }
        set
        {
            _playerType = value; 
        }
    }
    public enum PlayerType
    {
        loop,
        once,
    }

    public float PlayerSpeed
    {
        get
        {
            return playerSpeed;
        }
        set
        {
            playerSpeed = value;
        }
    }
    public int JumpCount
    {
        get
        {
            return jumpCount;
        }
        set
        {
            jumpCount = value;
        }
    }
    public bool playing=false;
    public int ImagesCount
    {
        get
        {
            return images.Count;
        }
    }
    public int CurIndex
    {
        get
        {
            return curIndex;
        }
        set
        {
            curIndex = value;
            if (curIndex >= ImagesCount)
            {
                curIndex = ImagesCount-1;
                Debug.Log("最后一张了");
            }
            if(curIndex<0)
            {
                curIndex = 0;
                Debug.Log("没有前一张了");
            }
        }
    }
    public void Play()
    {
        playing = true;
        if (_playerType == PlayerType.loop)
            StartCoroutine(PlayIE());
    }
    public void Pause()
    {
        playing = false;
        if (_playerType == PlayerType.loop)
            StopCoroutine(PlayIE());
    }
    public void Next()
    {
        SetActiveFunc(CurIndex, false);
        SetActiveFunc(++CurIndex, false);
    }
    public void Last()
    {
        SetActiveFunc(CurIndex, false);
        SetActiveFunc(--CurIndex, false);
    }
    private void ClearFunc()
    {
        for (int i = 0; i < images.Count; i++)
        {
            Destroy(images[i]);
            images[i].SetActive(false);
        }
        images.Clear();
    }
    private void SetActiveFunc(int index,bool bo)
    {
        images[index].SetActive(bo);
    }
    private void SetActiveAll(bool bo)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetActive(bo);
        }
    }
    public void LoadFunc(List<GameObject> gameObjects)
    {
        CurIndex = 0;
        tempImage.SetActive(false);
        ClearFunc();
        images = gameObjects;
        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.parent = tempImage.transform.parent;
            images[i].transform.localScale = new Vector3(1, 1, 1);
            images[i].GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            images[i].GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            images[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 600);
            images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(600, -300, 0);
        }
        
        SetActiveAll(false);
        SetActiveFunc(CurIndex, true);
    }
    public void LoadFunc(List<Texture2D> textures)
    {
        CurIndex = 0;
        tempImage.SetActive(false);
        ClearFunc();
        for (int i = 0; i < textures.Count; i++)
        {
            Texture2D tx = new Texture2D(100, 100);
            GameObject temp = GameObject.Instantiate(tempImage, tempImage.transform.position, Quaternion.identity);
     
            temp.GetComponent<Transform>().SetParent(tempImage.transform.parent);
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            temp.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            temp.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 600);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector3(600, -300,0);
            Sprite sprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(0.5f, 0.5f));
        
            temp.GetComponent<Image>().sprite = sprite;
            temp.gameObject.SetActive(true);
            images.Add(temp);
        }
        SetActiveAll(false);
        SetActiveFunc(CurIndex, true);
    }
    private IEnumerator PlayIE()
    {
        while (playing)
        {
            SetActiveFunc(CurIndex, false);
            if ((CurIndex + jumpCount >= ImagesCount && jumpCount > 0))
            {
                CurIndex = 0;
            }
            if ((CurIndex <= 0 && jumpCount < 0))
            {
                CurIndex = ImagesCount - 1;
            }
            SetActiveFunc(CurIndex += jumpCount, true);
           
            yield return new WaitForSeconds(PlayerSpeed);
        }
    }
}
