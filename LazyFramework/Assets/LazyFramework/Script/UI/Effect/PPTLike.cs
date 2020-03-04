/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ExtraTriggerEXpand
* 创建日期：2019-12-10 
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PPTLike : MonoBehaviour
{
    private RectTransform target;
    private bool open;
    static PPTLike instance;
    private bool grab;
    private static RectTransform curTarget;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //target = GameObject.Find("GameMain/Canvas").transform.Find("Image").GetComponent<RectTransform>(); //GameObject.Find("GameMain/Canvas").transform.Find("Image (2)").GetComponent<RectTransform>();// 

    }

    /// <summary>
    /// 模仿ppt 的梳理切换效果
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isOpen"></param>
    /// <param name="needDestroy">需要销毁掉复制出来的ui</param>
    public static void UIEffect_shuLi(RectTransform target, bool isOpen = true, Action onComplete = null, bool needDestroy = true)
    {

        Sprite img = null;
        img = GetSprite();

        //生成mask平铺
        //数量
        var count = 8;
        //遮罩模板水平
        GameObject mask_m = CreateMaskTemplate("shuLi", target, img, Image.FillMethod.Horizontal);

        int StartValue = isOpen ? 0 : 1;
        float EndValue = isOpen ? 1 : 0;
        mask_m.GetComponent<Image>().fillAmount = StartValue;

        float y = target.rect.height / count;
        float x = target.rect.width;
        //mask size
        Vector2 ch = new Vector2(x, y);
        //位置不变
        var pos = target.position;
        target.gameObject.SetActive(true);
        var maskArr = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            var mask = Instantiate(mask_m, target.parent);
            mask.name = target.name + "shuli_" + i.ToString();
            var maskRect = mask.GetComponent<RectTransform>();
            maskRect.anchoredPosition = target.anchoredPosition + new Vector2(0, (-target.rect.height / 2) + (y * (i + 0.5f)));
            maskRect.sizeDelta = ch;
            maskRect.localScale = Vector3.one;
            //分布显示
            var clone = Instantiate(target, mask.transform);
            clone.position = pos;
            Image image = mask.GetComponent<Image>();
            //控制方向
            //动画
            image.fillOrigin = i % 2;

            mask.gameObject.SetActive(true);
            maskArr[i] = mask;
            int i1 = i;
            float duration = 1f * ((1 + i1) / (float)count);
            //print(duration);

            mask.GetComponent<Image>().DOFillAmount(EndValue, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (i1 == count - 1)
                {
                    if (isOpen)
                        target.gameObject.SetActive(true);

                    for (int k = 0; k < maskArr.Length; k++)
                    {
                        GameObject gameObject1 = maskArr[k];
                        if (needDestroy)
                        {
                            Destroy(gameObject1);
                        }
                        else
                        {
                            gameObject1.SetActive(false);
                        }
                    }
                    if (onComplete != null) onComplete();
                }
            });

        }
        if (needDestroy)
            Destroy(mask_m);

        target.gameObject.SetActive(false);
    }

    /// <summary>
    /// 模仿ppt 的分割切换效果
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isOpen"></param>
    /// <param name="needDestroy">需要销毁掉复制出来的ui</param>
    public static void UIEffect_FenGe(RectTransform target, bool isOpen = true, Action onComplete = null, bool needDestroy = true)
    {

        Sprite img = null;
        img = GetSprite();

        //生成mask平铺
        //数量
        var count = 2;
        //遮罩模板水平
        var mask_m = CreateMaskTemplate("fenGe", target, img, Image.FillMethod.Horizontal);
        int StartValue = isOpen ? 0 : 1;
        float EndValue = isOpen ? 1 : 0;
        mask_m.GetComponent<Image>().fillAmount = StartValue;

        mask_m.GetComponent<Mask>().showMaskGraphic = false;
        float y = target.rect.height;
        float x = target.rect.width / count;
        //mask size
        Vector2 ch = new Vector2(x, y);
        //位置不变
        var pos = target.position;
        target.gameObject.SetActive(true);
        var maskArr = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            var mask = Instantiate(mask_m, target.parent);
            mask.name = target.name + "fenGe_" + i.ToString();
            var maskRect = mask.GetComponent<RectTransform>();
            maskRect.anchoredPosition = target.anchoredPosition + new Vector2((-target.rect.width / 2) + (x * (i + 0.5f)), 0);
            maskRect.sizeDelta = ch;
            maskRect.localScale = Vector3.one;
            //分布显示
            var clone = Instantiate(target, target.parent);
            clone.position = pos;
            clone.SetParent(mask.transform);
            Image image = mask.GetComponent<Image>();
            //控制方向
            //动画
            image.fillOrigin = 1 + (i % 2);
            //Mask
            mask.gameObject.SetActive(true);
            maskArr[i] = mask;
            float duration = 1;
            //print(duration);
            mask.GetComponent<Image>().DOFillAmount(EndValue, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (isOpen)
                    target.gameObject.SetActive(true);

                if (needDestroy)
                {
                    Destroy(mask);
                }
                else
                {
                    mask.SetActive(false);
                }
                if (onComplete != null) onComplete();

            });

        }
        if (needDestroy)
            Destroy(mask_m);

        target.gameObject.SetActive(false);
    }

    /// <summary>
    ///  多米若
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isOpen"></param>
    /// <param name="needDestroy">需要销毁掉复制出来的ui</param>
    public static void UIEffect_duoMiNuo(RectTransform target, bool isOpen = true, Action onComplete = null, bool needDestroy = true)

    {

        Sprite img = null;
        img = GetSprite();

        //生成mask平铺
        //数量n*n
        int countX = 6;

        //遮罩模板水平
        var mask_m = CreateMaskTemplate("qiPan", target, img, Image.FillMethod.Horizontal);
        int StartValue = isOpen ? 1 : 0;
        float EndValue = isOpen ? 0 : 1;
        //mask_m.transform.localEulerAngles = 90f * StartValue * Vector3.up;

        mask_m.GetComponent<Mask>().showMaskGraphic = false;
        float y = target.rect.height;
        float x = target.rect.width / countX;
        //mask size
        Vector2 ch = new Vector2(x, y);
        //位置不变
        var pos = target.position;
        target.gameObject.SetActive(true);
        var maskArr = new GameObject[countX];

        for (int i = 0; i < countX; i++)
        {
            var mask = Instantiate(mask_m, target.parent);
            mask.name = target.name + "qiPan_" + i.ToString();
            var maskRect = mask.GetComponent<RectTransform>();
            maskRect.anchoredPosition = target.anchoredPosition + new Vector2((-target.rect.width / 2) + (x * ((i % countX) + 0.5f)), 0);
            maskRect.sizeDelta = ch;
            maskRect.localScale = Vector3.one;
            //分布显示
            var clone = Instantiate(target, target.parent);
            clone.position = pos;
        clone.SetParent(mask.transform);
            Image image = mask.GetComponent<Image>();
            //控制方向
            //动画
            //image.fillOrigin = 1 + (i % 2);
            //Mask
            mask.gameObject.SetActive(true);
            mask.transform.localEulerAngles = Vector3.up * 90f * StartValue; // Vector3.up * 45f * (StartValue+((i/countX)%2));
            maskArr[i] = mask;
            float duration = 0.5f;
            //print(duration);
            Vector3 endV3 = Vector3.up * (EndValue * 90f);
            //print(endV3);
            var i1 = i;
            mask.transform.DOLocalRotate(endV3, duration).SetDelay(0 + (i1 % countX) * duration * 0.5f).SetEase(Ease.Linear).OnComplete(() =>
              {
                  if (((i1 % countX) == countX - 1))
                  {
                      if (isOpen)
                          target.gameObject.SetActive(true);
                      for (int j = 0; j < maskArr.Length; j++)
                      {
                          var m = maskArr[j];
                          if (needDestroy)
                          {
                              Destroy(m);
                          }
                          else
                          {
                              m.SetActive(false);
                          }
                      }
                      if (onComplete != null) onComplete();
                  }

              });

        }
        if (needDestroy)
        {
            Destroy(mask_m);
        }

        target.gameObject.SetActive(false);
    }

    /// <summary>
    ///  模仿ppt 的棋盘切换效果
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isOpen"></param>
    /// <param name="needDestroy">需要销毁掉复制出来的ui</param>
    public static void UIEffect_qiPan(RectTransform target, bool isOpen = true, Action onComplete = null, bool needDestroy = true)
    {

        Sprite img = null;
        img = GetSprite();

        //生成mask平铺
        //数量n*n
        int countX = (int)target.rect.width / 100;
        int countY = (int)target.rect.height / 100;
        //遮罩模板水平
        var mask_m = CreateMaskTemplate("qiPan", target, img, Image.FillMethod.Horizontal);
        int StartValue = isOpen ? 1 : 0;
        float EndValue = isOpen ? 0 : 1;
        //mask_m.transform.localEulerAngles = 90f * StartValue * Vector3.up;

        mask_m.GetComponent<Mask>().showMaskGraphic = false;
        float y = target.rect.height / countY;
        float x = target.rect.width / countX;
        //mask size
        Vector2 ch = new Vector2(x, y);
        //位置不变
        var pos = target.position;
        target.gameObject.SetActive(true);
        var maskArr = new GameObject[countX * countY];

        for (int i = 0; i < countX * countY; i++)
        {
            var mask = Instantiate(mask_m, target.parent);
            mask.name = target.name + "qiPan_" + i.ToString();
            var maskRect = mask.GetComponent<RectTransform>();
            maskRect.anchoredPosition = target.anchoredPosition + new Vector2((-target.rect.width / 2) + (x * ((i % countX) + 0.5f)), (-target.rect.height / 2) + y * ((i / countX) + 0.5f));
            maskRect.sizeDelta = ch;
            maskRect.localScale = Vector3.one;
            //分布显示
            var clone = Instantiate(target, target.parent);
            clone.SetParent(mask.transform);
            clone.position = pos;
            Image image = mask.GetComponent<Image>();
            //控制方向
            //动画
            //image.fillOrigin = 1 + (i % 2);
            //Mask
            mask.gameObject.SetActive(true);
            mask.transform.localEulerAngles = Vector3.up * 45f * (StartValue + ((i / countX) % 2));
            maskArr[i] = mask;
            float duration = 0.5f;
            //print(duration);
            Vector3 endV3 = Vector3.up * (EndValue * 90f);
            print(endV3);
            var i1 = i;
            mask.transform.DOLocalRotate(endV3, duration).SetEase(Ease.Linear).OnComplete(() =>
            {

                if (((i1 % countX) == countX - 1) && (i1 % countY) == countY - 1)
                {
                    if (isOpen)
                        target.gameObject.SetActive(true);
                    for (int j = 0; j < maskArr.Length; j++)
                    {
                        var m = maskArr[j];
                        if (needDestroy)
                        {
                            Destroy(m);
                        }
                        else
                        {
                            m.SetActive(false);
                        }
                    }
                    if (onComplete != null) onComplete();
                }

            });

        }
        if (needDestroy)
            Destroy(mask_m);

        target.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (target)
                UIEffect_duoMiNuo(target, open);
            open = !open;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (target)
                UIEffect_FenGe(target, open);
            open = !open;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (target)
                UIEffect_qiPan(target, open);
            open = !open;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (target)
                UIEffect_shuLi(target, open);
            open = !open;
        }
    }

    private static GameObject CreateMaskTemplate(string name, RectTransform target, Sprite img, Image.FillMethod fillMethod, Image.Type type = Image.Type.Filled)
    {
        var mask_m = new GameObject(name+"template", typeof(RectTransform), typeof(Mask), typeof(Image));
        mask_m.transform.SetParent(target.parent);
        mask_m.GetComponent<Image>().type = type;
        mask_m.GetComponent<Image>().fillMethod = fillMethod;
        mask_m.GetComponent<Image>().sprite = img;
        mask_m.GetComponent<Mask>().showMaskGraphic = false;
        return mask_m;
    }

    private static Sprite GetSprite()
    {
        var img = Sprite.Create(Texture2D.whiteTexture, new Rect(Vector2.zero, Vector2.one), Vector2.zero);

        return img;
    }



}
