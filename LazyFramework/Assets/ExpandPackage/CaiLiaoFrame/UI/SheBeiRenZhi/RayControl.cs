using UnityEngine;
using System.Collections;

public class RayControl : MonoBehaviour
{
    public static RayControl instance;
    void Awake()
    {
       instance = this;
    }
    [HideInInspector]
    public GameObject  hitObj;//碰撞信息
    [HideInInspector]
    public bool isHit = false;//是否碰撞到物体

    //public GameObject cube0, cube1, cube2;
    void Start()
    {
        instance = this;
        for (int i = 0; i < RenZhiControl.instance.RenZhiObj.Count; i++)
        {
            RenZhiControl.instance.RenZhiObj[i].AddHover(MouseHoved);
            RenZhiControl.instance.RenZhiObj[i].AddExit(MouseExit);
        }
    }
   public  void MouseHoved(GameObject go)
    {
        isHit = true;
        hitObj = go;
    }
  public   void MouseExit(GameObject go)
    {
        isHit = false;
        hitObj = null;
    }
}
