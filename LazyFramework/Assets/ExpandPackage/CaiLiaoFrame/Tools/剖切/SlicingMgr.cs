using UnityEngine;
using System.Collections;

/// <summary>/// 切割/// </summary>
public class SlicingMgr : MonoBehaviour 
{

    public Transform UpPlane;
    
    private Vector3 pos;
    private Vector3 nor;

    public GetSlicingMaterial GetMaters;
    // Use this for initialization
    void Start()
    {
        //plane = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = UpPlane.position;
        nor = UpPlane.up;
        for (int i = 0; i < GetMaters.Materias.Count; i++)
        {
            GetMaters.Materias[i].SetVector("_cPos", pos);
            GetMaters.Materias[i].SetVector("_cNormal", nor);
        }
    }
}
