#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    public class UIEditor
    {
        [MenuItem("GameObject/Rainier/Mvvm/AddView", priority = 0)]
        public static void AddComPontr()
        {
            GameObject objParent = Selection.activeGameObject;
            if (objParent == null) {
                Debug.Log("父节点为空");
                return;
            }
            #region 给Button添加ButtonView            
            var objButton = objParent.transform.GetComponentsInChildren<Button>();
            for (int i = 0; i < objButton.Length; i++)
            {
                if (!objButton[i].gameObject.GetComponent<ButtonView>())
                {
                    objButton[i].gameObject.AddComponent<ButtonView>();
                }
            }
            #endregion

            #region 给文本添加TextView
            var objText = objParent.transform.GetComponentsInChildren<Text>();
            for (int i = 0; i < objText.Length; i++)
            {
                if (!objText[i].gameObject.GetComponent<TextView>())
                {
                    objText[i].gameObject.AddComponent<TextView>();
                }
            }
            #endregion

            #region 给Toogle添加ToggleView
            var objToggle = objParent.transform.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < objToggle.Length; i++)
            {
                if (!objToggle[i].gameObject.GetComponent<ToggleView>())
                {
                    objToggle[i].gameObject.AddComponent<ToggleView>();
                }
            }
            #endregion

            #region 给Slider添加SliderView
            var objSlider = objParent.transform.GetComponentsInChildren<Slider>();
            for (int i = 0; i < objSlider.Length; i++)
            {
                if (!objSlider[i].gameObject.GetComponent<SliderView>())
                {
                    objSlider[i].gameObject.AddComponent<SliderView>();
                }
            }
            #endregion

            #region 给InputField添加InputFieldView
            var objInputField = objParent.transform.GetComponentsInChildren<InputField>();
            for (int i = 0; i < objInputField.Length; i++)
            {
                if (!objInputField[i].gameObject.GetComponent<InputFieldView>())
                {
                    objInputField[i].gameObject.AddComponent<InputFieldView>();
                }
            }
            #endregion

            #region 给Dropdown添加DropdownView
            var objDropdown = objParent.transform.GetComponentsInChildren<Dropdown>();
            for (int i = 0; i < objDropdown.Length; i++)
            {
                if (!objDropdown[i].gameObject.GetComponent<DropdownView>())
                {
                    objDropdown[i].gameObject.AddComponent<DropdownView>();
                }
            }
            #endregion

            #region 给Scrollbar添加ScrollbarView
            var objScrollbar = objParent.transform.GetComponentsInChildren<Scrollbar>();
            for (int i = 0; i < objScrollbar.Length; i++)
            {
                if (!objScrollbar[i].gameObject.GetComponent<ScrollbarView>())
                {
                    objScrollbar[i].gameObject.AddComponent<ScrollbarView>();
                }
            }
            #endregion
        }

    }
}