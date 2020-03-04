using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class RenZhiControl : MonoBehaviour
{
    public static RenZhiControl instance;
    void Awake()
    {
        instance = this;
    }
    public List<GameObject> RenZhiObj;
    // public GameObject[] RenZhiCollider;//认知碰撞
    public List<string> RenZhiNmae;//认知名称
    public RectTransform renzhiPlanel;
    public Image renzhiBG;
    public Text renzhiText;
    private bool CanRenZhi = true;
   private GameObject NowRenZhiObj;
    // Use this for initialization
    void Start()
    {
      
    }

    void Update()
    {
        if (CanRenZhi)
        {
            Renzhi();
        }
        else
        {
            renzhiPlanel.gameObject.SetActive(false);
            if (NowRenZhiObj != null)
            {
                NowRenZhiObj = null;
            }
        }
    }
    void Renzhi()
    {
        renzhiPlanel.position = Input.mousePosition;
        if (RayControl.instance.isHit)
        {
            NowRenZhiObj = isRenZhiObj(RayControl.instance.hitObj);
        }
        else
        {
            Name_ColseAni();
        }
    }
    public void ColseRenZhi()
    {
        CanRenZhi = false;
    }
    GameObject isRenZhiObj(GameObject ga)
    {
        if (ga == NowRenZhiObj)
            return ga;
        for (int i = 0; i < RenZhiObj.Count; i++)
        {
            if (ga == RenZhiObj[i])
            {
                Name_ColseAni();
                renzhiPlanel.gameObject.SetActive(true);
                renzhiText.text = RenZhiNmae[i];

                Name_OpenAni();
                return RenZhiObj[i];
            }
        }
        Name_ColseAni();
        return null;
    }
    public Image Xian;
    void Name_OpenAni()
    {
        SetMouseDrag();
        if (Xian != null)
        {
            DOTween.To(() => Xian.fillAmount, x => Xian.fillAmount = x, 1, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                renzhiBG.rectTransform.sizeDelta = new Vector2(Mathf.Clamp( renzhiText.rectTransform.sizeDelta.x + renzhiText.fontSize * 2f,92,960), renzhiBG.rectTransform.sizeDelta.y);
                renzhiText.transform.DOScaleX(1,0.1f);
                renzhiBG.transform.DOScaleX(1, 0.1f);
            });
        }
        else
        {
            renzhiBG.rectTransform.sizeDelta = new Vector2(renzhiText.rectTransform.sizeDelta.x + renzhiText.fontSize * 2f, renzhiBG.rectTransform.sizeDelta.y); 
            renzhiText.transform.DOScaleX(1, 0.1f);
            renzhiBG.transform.DOScaleX(1, 0.1f);
        }
    }
    void Name_ColseAni()
    {
        SetMouseExit();
        renzhiPlanel.gameObject.SetActive(false);
        if (NowRenZhiObj != null)
        {
            NowRenZhiObj = null;
        }
        renzhiText.transform.localScale = new Vector3(0, 1, 1);
        renzhiBG.transform.localScale = new Vector3(0, 1, 1);
        if (Xian != null)
        {
            Xian.fillAmount = 0;
        }
    } 
    public Texture2D m_T2D_Tu;
    private void SetMouseDrag()
    {
        Cursor.SetCursor(m_T2D_Tu, Vector2.zero, CursorMode.Auto);
    }
    private void SetMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
