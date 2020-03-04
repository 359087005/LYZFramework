using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRecPlayer : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.GetComponent<ImagePlayer>().LoadFunc(ContinuousCapture.imageInfos);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.GetComponent<ImagePlayer>().Play();
        }
    }
}
