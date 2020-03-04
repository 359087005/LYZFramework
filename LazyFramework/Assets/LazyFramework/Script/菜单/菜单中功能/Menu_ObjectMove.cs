using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    public class Menu_ObjectMove : MenuFunctionBase
    {
        Ray ray;
        RaycastHit hit;
        public LayerMask mask;
        LayerMask myLayer;
        public float distance = 0;
        bool isDrag = false;
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
            funcName = "移动";
            myLayer = gameObject.layer;
        }
        protected override void StartFunc()
        {
            isDrag = true;
            
            tempTrans = transform.parent;
            bottom.transform.parent = tempTrans;
            transform.parent = bottom.transform;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            StartCoroutine(StartMove());
        }
        private void MoveFunc()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (isDrag && Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 500, mask))
                {
                    if (hit.transform.gameObject == gameObject) return;
                    bottom.position = hit.point + (Camera.main.transform.position - hit.point).normalized * distance;
                }
            }
        }
        IEnumerator StartMove()
        {
            while (isDrag)
            {
                MoveFunc();
                yield return new WaitForFixedUpdate();
                if(Input.GetMouseButtonUp(0))
                {
                    gameObject.layer = myLayer;
                    transform.parent = tempTrans;
                    bottom.transform.parent = transform;
                    tempTrans = null;
                    OnFunctionEnd();
                    isDrag = false;
                    break;
                }
            }
        }
    }
}
