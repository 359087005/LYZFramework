using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    public class Menu_ObjectScale : MenuFunctionBase
    {
        public float speed = 1;
        bool isScale = false;
        private float curScale = 0.1f;
        public Transform bottom;
        private Transform tempTrans;
        protected override void Init()
        {
            base.Init();
            if (bottom == null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name == "Bottom" || transform.GetChild(i).name == "bottom")
                    {
                        bottom = transform.GetChild(i);
                    }
                }
            }
            funcName = "缩放";
        }
        protected override void StartFunc()
        {
            isScale = true;
            tempTrans = transform.parent;
            bottom.transform.parent = tempTrans;
            transform.parent = bottom.transform;
            StartCoroutine(StartScale());
        }
        private void ScaleFunc()
        {
            if (isScale && Input.GetMouseButton(0))
            {
                bottom.localScale =
                    new Vector3(bottom.localScale.x + Input.GetAxis("Mouse X") * speed, bottom.localScale.y + Input.GetAxis("Mouse X") * speed, bottom.localScale.z + Input.GetAxis("Mouse X") * speed);
            }
        }
        IEnumerator StartScale()
        {
            while (isScale)
            {
                ScaleFunc();
                yield return new WaitForFixedUpdate();
                if (Input.GetMouseButtonUp(0))
                {
                    OnFunctionEnd();
                    transform.parent = tempTrans;
                    bottom.transform.parent = transform;
                    tempTrans = null;
                    isScale = false;
                    break;
                }
            }
        }
      
    }

}
