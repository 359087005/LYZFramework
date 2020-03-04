using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    public class ValueContainer<T>
    {
        public int dataCapacity;
        public float actTime = 0.1f;
        private List<T> values = new List<T>();
        public T GetValues
        {
            get
            {
                return values[values.Count];
            }
        }

        private Action onDo;
        public Action OnDo
        {
            set
            {
                onDo = value;
            }
        }
        public void AddValue(T t)
        {
            values.Add(t);
        }
        public void BeginAction()
        {
            WaitToAction();
        }
        IEnumerator WaitToAction()
        {
            while (values.Count != 0)
            {
                if (onDo != null)
                {
                    onDo.Invoke();
                }
                yield return new WaitForSeconds(actTime);
            }
        }
    }
}