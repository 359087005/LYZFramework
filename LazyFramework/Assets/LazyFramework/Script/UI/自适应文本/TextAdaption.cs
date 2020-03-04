using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    [RequireComponent(typeof(ContentSizeFitter))]
    public class TextAdaption : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] float maxWidth;
        private void Start()
        {
            InitComponent();
            Adaption();
        }
        private void InitComponent()
        {
        
            if(text==null)
            {
                text = GetComponent<Text>();
            }
        }
        private void Adaption()
        {
            maxWidth = text.GetComponent<RectTransform>().sizeDelta.x;
            if (text.preferredWidth > maxWidth)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, text.rectTransform.sizeDelta.y);
                gameObject.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }
    }

}
