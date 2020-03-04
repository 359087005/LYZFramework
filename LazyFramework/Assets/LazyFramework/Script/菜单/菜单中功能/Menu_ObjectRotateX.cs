using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    public class Menu_ObjectRotateX : MenuFunctionBase
    {
        bool canRot;
        [SerializeField] float speed = 1;
        protected override void Init()
        {
            base.Init();
            funcName = "X轴旋转";
        }
        protected override void StartFunc()
        {
            canRot = true;
            StartCoroutine(StartRot());
        }
        private void RotateFunc()
        {
            if (canRot && Input.GetMouseButton(0))
            {
                transform.Rotate(-Input.GetAxis("Mouse X") * speed, 0, 0);
            }
        }
        IEnumerator StartRot()
        {
            while (canRot)
            {
                RotateFunc();
                yield return new WaitForFixedUpdate();
                if (Input.GetMouseButtonUp(0))
                {
                    OnFunctionEnd();
                    canRot = false;
                    break;
                }
            }
        }
    }

}
