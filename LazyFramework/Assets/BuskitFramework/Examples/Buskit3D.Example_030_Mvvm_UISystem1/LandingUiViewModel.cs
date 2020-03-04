/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LandingDataModel
* 创建日期：2019-03-14 10:30:08
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{
	/// <summary>
    /// 
    /// </summary>
	public class LandingUiViewModel : ViewModelBehaviour
    {
        [Binding(EntityPropertyName = "questionText")]
        public TextView _questionText;

        [Binding(EntityPropertyName = "inputFieldText")]
        public InputFieldView _inputFieldText;

        [Binding(EntityPropertyName = "landingBtn")]
        public ButtonView _landingBtn;

        [Binding(EntityPropertyName = "escBtn")]
        public ButtonView _escBtn;

        protected override void Awake()
        {
            _questionText = GameObject.Find("UIRoot/LandingUI/QuestionBg/QuestionText").GetComponent<TextView>();

            _inputFieldText = GameObject.Find("UIRoot/LandingUI/InputField").GetComponent<InputFieldView>();

            _landingBtn = GameObject.Find("UIRoot/LandingUI/LandingButton").GetComponent<ButtonView>();

            _escBtn = GameObject.Find("UIRoot/LandingUI/EscButton").GetComponent<ButtonView>();
            
            this.DataEntity = new LandingUIEntity();
            base.Awake();
        }
    }
}

