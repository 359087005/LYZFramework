/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ChangeFontEditor
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：全场景更换字体
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Lazy
{
    
    public class ChangeFontEditor : EditorWindow
    {
        Font toChange;
        static Font toChangeFont;
        FontStyle toFontStyle;
        static FontStyle toChangeFontStyle;
        [SerializeField]
        protected List<Text> ignoreText = new List<Text>();
        protected SerializedObject serializedObject;
        protected SerializedProperty assetLstProperty;
        Vector2 scrollPos;
        [MenuItem("LazyTools/更换场景中所有字体")]
        public static void Open()
        {
            GetWindow(typeof(ChangeFontEditor));
        }
        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            assetLstProperty = serializedObject.FindProperty("ignoreText");
        }
        void OnGUI()
        {
            Window_AllScene();
        }
       
        private void Window_AllScene()
        {
            EditorGUILayout.LabelField("------------------------更换场景中所有Text字体------------------------");
            EditorGUILayout.LabelField("选择字体：");
            toChange = (Font)EditorGUILayout.ObjectField(toChange, typeof(Font), true, GUILayout.MinWidth(400));
            toChangeFont = toChange;
            toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(400));
            EditorGUILayout.LabelField("添加忽略更换的Text:");
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.PropertyField(assetLstProperty, true);
            toChangeFontStyle = toFontStyle;
            serializedObject.ApplyModifiedProperties();
          
            if (GUILayout.Button("确认更换"))
            {
                AllScene();
            }
            EditorGUILayout.EndScrollView();
        }
        /// <summary>
        /// 仅更换指定物体的所有子物体中的Text（还没完成）
        /// </summary>
        public void OnlyChild()
        {
            //Text[] tArray = canvas.GetComponentsInChildren<Transform>();
        }
        /// <summary>
        /// 更换场景中所有
        /// </summary>
        public void AllScene()
        {
            Text[] texts = FindObjectsOfType<Text>();
            for (int i = 0; i < texts.Length; i++)
            {
                if (!CheckContain(texts[i]))
                {
                    //Undo.RecordObject(texts[i], texts[i].gameObject.name);
                    texts[i].font = toChangeFont;
                    texts[i].fontStyle = toChangeFontStyle;
                    EditorUtility.SetDirty(texts[i]);
                }
            }
            Debug.Log("成功");
        }
        public bool CheckContain(Text _text)
        {
            for (int i = 0; i < ignoreText.Count; i++)
            {
                if (ignoreText[i]==_text)
                {
                    return true;
                }
            }
            return false;
        }
        private class TextInfo
        {
            public Text text;
            public Font oldFont;
            public TextInfo(Text _text,Font _oldFont)
            {
                text = _text;
                oldFont = _oldFont;
            }
        }
    }
}
