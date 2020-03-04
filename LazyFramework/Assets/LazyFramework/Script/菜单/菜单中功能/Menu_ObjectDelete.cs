using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    public class Menu_ObjectDelete : MenuFunctionBase
    {
        protected override void Init()
        {
            base.Init();
            funcName = "删除";
        }

        protected override void StartFunc()
        {
            OnFunctionEnd();
            Destroy(gameObject);
        }
    }
}
