
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TransDataDraw
* 创建日期：2019-02-12 15:15:50
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEditor;
using UnityEngine;
namespace Buskit3D.Example_020_Tween
{

    public class TransDataDraw : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.indentLevel = 0;
            Rect contentPosition = EditorGUI.PrefixLabel(position,label);
          
            if (position.height > 16f)
            {
                position.height = 16f;
                EditorGUI.indentLevel += 1;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += 18f;
            }
            contentPosition.width *= 0.75f;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("localPosition"),GUIContent.none);
            contentPosition.y += 16;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("localAngles"), GUIContent.none);
            contentPosition.y += 16;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("localScale"), GUIContent.none);

            EditorGUI.EndProperty();
           
        }
    }
    
}

