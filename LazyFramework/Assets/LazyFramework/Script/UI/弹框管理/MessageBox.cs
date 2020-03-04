using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Lazy
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] public string boxName;
        [SerializeField] Text text;
        [SerializeField] Button confirm;
        private void Start()
        {
            if (text == null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    text = transform.GetChild(i).GetComponent<Text>();
                }
            }
        }
        public void ShowFunc(string str)
        {
            text.text = str;

        }
        public void AddConfirmListen(Action onClick)
        {
            if (confirm != null)
            {
                confirm.onClick.AddListener(() =>
                {
                    onClick();
                });
            }
            else
            {
                Debug.Log("没有挂载确认按钮");
            }
        }

        public void OnClose()
        {
            this.gameObject.SetActive(false);
        }
    }
}