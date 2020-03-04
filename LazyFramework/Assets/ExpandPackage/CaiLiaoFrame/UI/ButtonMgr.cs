using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using HighlightingSystem;


public class ButtonMgr : MonoBehaviour, IPointerClickHandler {


    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClickLeft;
	void Start () {
	
	}
	

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickLeft.Invoke(gameObject);
    }
    public Transform LightQuad;
    public void OnHight(bool useRecord = false)
    {
        if(LightQuad==null)
        {
            LightQuad = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            LightQuad.transform.parent = this.transform;
            Destroy(LightQuad.GetComponent<BoxCollider>());
            LightQuad.gameObject.AddComponent<Highlighter>();
            LightQuad.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 0f);
            TransparentMat(LightQuad.GetComponent<MeshRenderer>().material);

            HighLightLogic.instance.allHLList.Add(LightQuad.gameObject);
        }
        RectTransform _rect = GetComponent<RectTransform>();
        if (_rect == null)
            Debug.Log("没有RectTransform", gameObject);
        Vector2 quadscale = _rect.sizeDelta;
       
        LightQuad.localScale = new Vector3(quadscale.x, quadscale.y,1);
        LightQuad.localPosition = Vector3.zero;
        LightQuad.localEulerAngles = Vector3.zero;

        LightQuad.gameObject.OnHightligher();
        //HighLighter listener = HighLighter.Get(LightQuad.gameObject);
        //listener.SetFlash(true);
    }
    void TransparentMat(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }
    public void OffHight(bool useRecord = false)
    {
        LightQuad.gameObject.OffHightligher();
        //HighLighter listener = HighLighter.Get(LightQuad.gameObject);
        //listener.SetFlash(false);
    }



}
