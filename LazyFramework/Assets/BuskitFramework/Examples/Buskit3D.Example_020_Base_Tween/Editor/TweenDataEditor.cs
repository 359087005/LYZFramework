
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TweenDataEditor
* 创建日期：2019-02-12 10:22:27
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：动画节点编辑器
******************************************************************************/

using UnityEngine;
using UnityEditor;
using System;

namespace Buskit3D.Example_020_Tween
{
    [Flags]
    public enum EditorListOption
    {
        None = 0,
        ListSize = 1,
        ListLabel = 2,
        ElementLabels = 4,
        Buttons = 8,
        Default = ListSize | ListLabel | ElementLabels,
        NoElementLabels = ListSize | ListLabel,
        All = Default | Buttons
    }

    /// <summary>
    /// 节点便捷器Editor
    /// </summary>
    [CustomEditor(typeof(TweenData))]
	public class TweenDataEditor : Editor 
	{

        private static GUIContent
             moveButtonContent = new GUIContent("\u21b4", "move down"),
             duplicateButtonContent = new GUIContent("+", "insert"),
             deleteButtonContent = new GUIContent("-", "delete"),
             addButtonContent = new GUIContent("+", "add element"),
             modifyButtonContent = new GUIContent("m", "modify"),
              selectButtonContent = new GUIContent("s", "select");

        private static GUILayoutOption miniButtonWidth = GUILayout.Width(25f);

        private  static int _Id=0;
        private TweenData tweenData;

        private void OnEnable()
        {
            tweenData = (TweenData)target;
            if (tweenData.Data.Count == 0)
            {
                AllData allData = new AllData();
                tweenData.Data.Add(allData);
              
            }
        }

        public override void OnInspectorGUI()
        {
            tweenData = (TweenData)target;
            GUILayout.BeginHorizontal();
            
            _Id = GUILayout.Toolbar(_Id, Array.ConvertAll(tweenData.Data.ToArray(),p=>"path"+tweenData.Data.IndexOf(p).ToString()), GUILayout.ExpandWidth(true),GUILayout.Height(30));
            if (GUILayout.Button("+",GUILayout.Width(40),GUILayout.Height(30)))
            {
                AllData allData = new AllData();
                tweenData.Data.Add(allData);
            }

            GUI.color = Color.red;
            if (GUILayout.Button("-", GUILayout.Width(40), GUILayout.Height(30)))
            {
                if (tweenData.Data.Count == 1)
                    return;
                tweenData.Data.RemoveAt(_Id);
                _Id = 0;
            }
            GUI.color = Color.white;
            GUILayout.EndHorizontal();
           
            serializedObject.Update();
            tweenData.pointData = tweenData.Data[_Id].data;
            Show(serializedObject.FindProperty("pointData"), EditorListOption.All);
            serializedObject.ApplyModifiedProperties();
     
           //base.OnInspectorGUI();
        }

        public  void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
        {
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            bool
                showListLabel = (options & EditorListOption.ListLabel) != 0,
                showListSize = (options & EditorListOption.ListSize) != 0;

            if (showListLabel)
            {
                EditorGUILayout.PropertyField(list);
                EditorGUI.indentLevel += 1;
            }
            if (!showListLabel || list.isExpanded)
            {
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                if (showListSize)
                {
                    EditorGUILayout.PropertyField(size);
                }
                if (size.hasMultipleDifferentValues)
                {
                    EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
                }
                else
                {
                    ShowElements(list, options);
                }
            }
            if (showListLabel)
            {
                EditorGUI.indentLevel -= 1;
            }
        }

        private  void ShowElements(SerializedProperty list, EditorListOption options)
        {
            bool
                showElementLabels = (options & EditorListOption.ElementLabels) != 0,
                showButtons = (options & EditorListOption.Buttons) != 0;

            for (int i = 0; i < list.arraySize; i++)
            {
                if (showButtons)
                {
                    EditorGUILayout.BeginHorizontal();
                }
                if (showElementLabels)
                {
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), true);
                }
                else
                {
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none, true);
                }
                if (showButtons)
                {
                    ShowButtons(list, i);
                    EditorGUILayout.EndHorizontal();
                }
            }
            //if (showButtons && list.arraySize == 0 && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
            //{
            //    list.arraySize += 1;
            //}
            if (showButtons && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
            {
                //list.arraySize += 1;
                AddElement();
            }
        }

        private  void ShowButtons(SerializedProperty list, int index)
        {
            //if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
            //{
            //     list.MoveArrayElement(index, index + 1);
            //}
           // GUILayout.BeginHorizontal();
           
            if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButton, miniButtonWidth))
            {
                //list.InsertArrayElementAtIndex(index);
                InsertElement(index);
            }
            if (GUILayout.Button(modifyButtonContent, EditorStyles.miniButton, miniButtonWidth))
            {
                ModifyElement(index);
            }
            if(GUILayout.Button(selectButtonContent, EditorStyles.miniButton,miniButtonWidth))
            {
                SelectElement(index);
            }
            if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButton, miniButtonWidth))
            {
                //int oldSize = list.arraySize;
                //list.DeleteArrayElementAtIndex(index);
                //if (list.arraySize == oldSize)
                //{
                //    list.DeleteArrayElementAtIndex(index);
                //}
                DeleteElement(index);
            }
          //  GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        public void AddElement()
        {
            TransData trans = new TransData();
            trans.localPosition = tweenData.transform.localPosition;
            trans.localAngles = tweenData.transform.localEulerAngles;
            trans.localScale = tweenData.transform.localScale;
            tweenData.Data[_Id].data.Add(trans);
        }

        public  void DeleteElement(int index)
        {

            tweenData.Data[_Id].data.RemoveAt(index);
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="index"></param>
        public void InsertElement(int index)
        {
            TransData trans = new TransData();
            trans.localPosition = tweenData.transform.localPosition;
            trans.localAngles = tweenData.transform.localEulerAngles;
            trans.localScale = tweenData.transform.localScale;
            tweenData.Data[_Id].data.Insert(index, tweenData.Data[_Id].data[index]);
        }

        /// <summary>
        /// 修改元素
        /// </summary>
        /// <param name="index"></param>
        public void ModifyElement(int index)
        {
            TransData trans = new TransData();
            trans.localPosition = tweenData.transform.localPosition;
            trans.localAngles = tweenData.transform.localEulerAngles;
            trans.localScale = tweenData.transform.localScale;
            tweenData.Data[_Id].data[index] = trans;
        }

        /// <summary>
        /// 选中元素
        /// </summary>
        /// <param name="index"></param>
        public void SelectElement(int index)
        {
            tweenData.transform.localPosition = tweenData.Data[_Id].data[index].localPosition;
            tweenData.transform.localEulerAngles = tweenData.Data[_Id].data[index].localAngles;
            tweenData.transform.localScale = tweenData.Data[_Id].data[index].localScale;
        }
    }
   


}

