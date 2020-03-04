using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    public class Assemble3DContainer : AssembleContainerBase
    {
        protected override void Start()
        {
            base.Start();
            if(GetComponent<Collider>()==null)
            {
                Debug.LogError("请为容器<"+gameObject.name+">添加碰撞器");
            }
            else
            {
                GetComponent<Collider>().isTrigger = true;
            }
            if(GetComponent<Rigidbody>()==null)
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
            }
        }
    }
}
