using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    public class ComponentTools : MonoBehaviour
    {
        [SerializeField] Font font;
        static Text[] text_Text;
        static List<string> text_Font = new List<string>();
        /// <summary>
        /// 场景中所有符合条件的物体添加组件
        /// </summary>
        /// <typeparam name="T">该物体拥有的组件（条件）</typeparam>
        /// <typeparam name="Tk">添加的组件</typeparam>
        public static void AddComponentInAll<T, Tk>() where T : Component where Tk : Component
        {
            T[] temp = FindObjectsOfType<T>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].GetComponent<Tk>() == null)
                {
                    temp[i].gameObject.AddComponent<Tk>();
                }
            }
        }
        public void ChangeAllFont()
        {
            text_Text = null;
            text_Text = FindObjectsOfType<Text>();
         
            //for (int i = 0; i < text_Text.Length; i++)
            //{
            //    text_Font.Add(text_Text[i].font.name);
            //}
            for (int i = 0; i < text_Text.Length; i++)
            {
                text_Text[i].font = new Font("SimHei");
            }
        }
        public static void UnDoText()
        {
            for (int i = 0; i < text_Font.Count; i++)
            {
                text_Text[i].font = new Font(text_Font[i]);
            }
            text_Font.Clear();
        }
    }
}