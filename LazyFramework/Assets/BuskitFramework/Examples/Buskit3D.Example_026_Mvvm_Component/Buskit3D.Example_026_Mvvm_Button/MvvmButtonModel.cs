using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit3D.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buskit3D.Example_26_Mvvm_Button
{

    public class MvvmButtonModel : ViewModelBehaviour
    {
                
        [Binding(EntityPropertyName = "isShow")]
        public ButtonView btnShow;

        protected override void Awake()
        {
            //实例化DataEntity
            this.DataEntity = new MvvmButtonEntity();
            base.Awake();
        }
    }
}
