/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：InputFieldAutoScale
* 创建日期：2020-1-2
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 功能描述：输入栏自动变高
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldAutoScale : MonoBehaviour
{
    private float padding;
    private float height;
    void Start()
    {
        UnityEngine.Events.UnityAction<string, InputField> auto = (str, inputField) =>
        {

            padding = inputField.GetComponent<RectTransform>().rect.height - inputField.textComponent.GetComponent<RectTransform>().rect.height;
#if UNITY_2018_3_0
            height = inputField.preferredHeight;
#endif
#if UNITY_5_3_8

            height = inputField.textComponent.cachedTextGenerator.GetPreferredHeight(inputField.text,
                inputField.textComponent.GetGenerationSettings(inputField.textComponent.rectTransform.rect.size));
#endif

            inputField.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height + padding);

        };

        var t = GetComponent<InputField>();
        if (t)
        {
            t.lineType = InputField.LineType.MultiLineNewline;
            t.onValueChanged.AddListener((str) => auto(str, t));
        }

    }

}
