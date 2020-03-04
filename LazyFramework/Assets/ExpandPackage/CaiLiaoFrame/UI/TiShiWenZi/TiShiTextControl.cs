using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(TypewriterEffectUGUI))]
[RequireComponent(typeof(ContentSizeFitter))]
public class TiShiTextControl : MonoBehaviour, IScrollHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static TiShiTextControl instance;
    public static TiShiTextControl GetInstance()
    {
        return instance;
    }
    RectTransform ThisRect;
    RectTransform ParentRect;
    private GameObject  tipsBg;
    private GameObject  tipsText;
    bool CanScrollWheel = true;
    public bool isTipsSinglerow = false;

    public string[] tiShiWenZi = new string[12]{
        "","",//0,1
        "","",//2,3
        
        "","",//4,5
        "","",//6,7

        "","",//8,9
        "","",//10,11

    };

    void Awake()
    {
        instance = this;
        tipsText = GameObject.Find("TipsText");
        tipsBg = tipsText.transform.parent.gameObject;
        ThisRect = GetComponent<RectTransform>();
        GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        GetComponent<TypewriterEffectUGUI>().isActive = true;
        GetComponent<TypewriterEffectUGUI>().charsPerSecond = 10;
        ThisRect.pivot = new Vector2(0.5f, 0.5f);
        if (isTipsSinglerow ==true )
        {
            ThisRect.pivot = new Vector2(0.5f, 0);

            ThisRect.anchorMin = new Vector2(0.5f, 0);
            ThisRect.anchorMax = new Vector2(0.5f, 0);
            tipsBg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(ThisRect.rect.width + 6, 20 );
            tipsBg.transform.localPosition = new Vector3(0, 0, 0);
            tipsText.transform.parent = tipsBg.transform;
            tipsText.transform.localScale = new Vector3(1, 1, 1);
        }
        ParentRect = transform.parent.GetComponent<RectTransform>();
        ThisRect.localPosition = new Vector3(ThisRect.localPosition.x, 0, ThisRect.localPosition.z);
    }
    // Use this for initialization
    void Start()
    {
        Invoke("Delay", 0.5f);
    }
    void Delay()
    {
      //  ChangeText("开始：我是测试文本！！我是测试文本！！我是测试文本！！我是测试文本！！我是测试文本！！我是测试文本！！我是测试文本！！：结束。...................................");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
           // ChangeText("asfnbhajsfkasjgggggggggggggj");
        }
        if (ThisRect.rect.height > ParentRect.rect.height)
        {
            if (!CanScrollWheel)
            {
                if (isTipsSinglerow == false)
                {
                    this.transform.localPosition -= new Vector3(0, ParentRect.rect.height / 2f, 0);
                }
                ThisRect.pivot = new Vector2(0.5f, 0);
            }
            CanScrollWheel = true; 
        }
        else
        {
            if (CanScrollWheel)
            {
                if (isTipsSinglerow == false)
                {
                    ThisRect.localPosition = new Vector3(ThisRect.localPosition.x, 0, ThisRect.localPosition.z);
                    ThisRect.pivot = new Vector2(0.5f, 0.5f);
                }
                else
                {
                    ThisRect.localPosition = new Vector3(ThisRect.localPosition.x, -8, ThisRect.localPosition.z);
                    //ThisRect.pivot = new Vector2(0.5f, 0.5f);
                }
            }
            CanScrollWheel = false;
        }
    }
    /// <summary>
    /// 滚轮控制文字滚动
    /// </summary>
    /// <param name="eventData"></param>
    #region IScrollHandler 成员

    public void OnScroll(PointerEventData eventData)
    {
        if (CanScrollWheel)
        {
            this.transform.localPosition += new Vector3(0, -ParentRect.rect.height/4f * eventData.scrollDelta.y, 0);
            if (this.transform.localPosition.y > -ParentRect.rect.height / 2f )
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, -ParentRect.rect.height / 2f, this.transform.localPosition.z);
            }
            if (this.transform.localPosition.y < -ThisRect.rect.height+ ParentRect.rect.height / 2f)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, ParentRect.rect.height / 2f - ThisRect.rect.height, this.transform.localPosition.z);
            }
        }

    }

    #endregion
    /// <summary>
    /// 鼠标在文字上，禁止角色控制器滚轮
    /// </summary>
    /// <param name="eventData"></param>
    #region IPointerExitHandler 成员

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CanScrollWheel)
        {
            FirstViewControl.instance.SetIsCanScrollView(save_IsCanScrollView);
        }
    }

    #endregion
    /// <summary>
    /// 鼠标离开文字，开放角色控制器滚轮
    /// </summary>
    /// <param name="eventData"></param>
    #region IPointerEnterHandler 成员

    bool save_IsCanRotate, save_IsCanScrollView;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CanScrollWheel)
        {
            save_IsCanScrollView = FirstViewControl.instance.IsCanScrollView();
            FirstViewControl.instance.SetIsCanScrollView(false);
        }
    }

    #endregion
    /// <summary>
    /// 修改文字提示
    /// </summary>
    /// <param name="str"></param>
    public void ChangeText(string str)
    {
        //GetComponent<TypewriterEffectUGUI>().isActive = true;
        //GetComponent<TypewriterEffectUGUI>().words = str;
        GetComponent<TypewriterEffectUGUI>().ReloadText(str);
    }
    public void ChangeText(int id)
    {
        //GetComponent<TypewriterEffectUGUI>().isActive = true;
        //GetComponent<TypewriterEffectUGUI>().words = tiShiWenZi[id];
        GetComponent<TypewriterEffectUGUI>().ReloadText(tiShiWenZi[id]);
    }
}
