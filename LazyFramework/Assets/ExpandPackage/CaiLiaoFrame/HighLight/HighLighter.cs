using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using HighlightingSystem;


//[RequireComponent(typeof(HighLightDataModel)), RequireComponent(typeof(HighLightLogic)), DisallowMultipleComponent]
public class HighLighter : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private bool seeThrough = false;
    protected bool _seeThrough = true;

    protected Highlighter h;

    private Color flashColor = new Color(0f / 255f, 255f / 255f, 255f / 255f);//闪烁高亮颜色
    private Color hoverColor = Color.red;//鼠标触碰颜色
    [HideInInspector]
    public bool isFlashIng = false;
    // 
    protected void Awake()
    {
        h = GetComponent<Highlighter>();
        if (h == null) { h = gameObject.AddComponent<Highlighter>(); }
    }
    void OnEnable()
    {
        _seeThrough = seeThrough;

        if (seeThrough) { h.FlashingOn(); }
        else { h.FlashingOff(); }
    }

    public static HighLighter Get(GameObject go)
    {
        HighLighter component = go.GetComponent<HighLighter>();
        if (component == null)
        {
            component = go.AddComponent<HighLighter>();
        }
        return component;
    }
    public static void RemoveHL(GameObject go)
    {
        HighLighter component = go.GetComponent<HighLighter>();
        if (component != null)
        {
            Destroy(component);
            Destroy(go.GetComponent<Highlighter>());
        }
    }

    public void SetHL(bool b)
    {
        if (b)
        {
            h.ConstantOnImmediate(hoverColor);
        }
        else
        {
            h.ConstantOffImmediate();
        }
    }
    public void SetFlash(bool b)
    {
        if (b)
        {
            h.FlashingOn(flashColor, new Color(flashColor.r, flashColor.g, flashColor.b, 0));
        }
        else
        {
            h.FlashingOff();
        }
        isFlashIng = b;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        SetHL(true);
        h.FlashingOff();
    }
    public void OnPointerExit(PointerEventData eventData)
    {

        SetHL(false);
        if (isFlashIng)
            h.FlashingOn(flashColor, new Color(flashColor.r, flashColor.g, flashColor.b, 0));
    }

    // 
    protected virtual void Start() { }

    // 
    protected void Update()
    {
        if (_seeThrough != seeThrough)
        {
            _seeThrough = seeThrough;
            if (_seeThrough) { h.FlashingOn(); }
            else { h.FlashingOff(); }
        }
    }
    /// <summary>
    /// һֱ��
    /// </summary>
    public void ConstantLight()
    {

        h.ConstantOn();
    }
    public void ConstantLightOff()
    {
        h.ConstantOff();
    }
    // 
    public void MouseOver()
    {
        h.On(hoverColor);
    }
    public void LightingOn()
    {
        h.FlashingOn();
    }
    public void LightingOff()
    {
        h.FlashingOff();
    }
    // 
    public void Fire1()
    {
        // Switch flashing
        h.FlashingSwitch();
    }

    // 
    public void Fire2()
    {
        // Stop flashing
        h.FlashingSwitch();
    }

   
}