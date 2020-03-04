using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>/// 获取切割材质/// </summary>
public class GetSlicingMaterial : MonoBehaviour 
{
    public Transform UpPlane;

    private Vector3 pos;
    private Vector3 nor;
    public List<Material> Materias = new List<Material>();
    void Start() 
    {

    }

    public void PlaneMove(Vector3 pos,Vector3 rot,float time=0,UnityAction action=null) 
    {
        UpPlane.DOLocalMove(pos, time);
        UpPlane.DOLocalRotate(rot, time);
    }

    [ContextMenu("OnEnable")]
    void OnEnable() 
    {
        Materias = new List<Material>();
        MeshRenderer[] rends = this.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < rends.Length; i++)
        {
            for (int j = 0; j < rends[i].materials.Length; j++)
            {
                Materias.Add(rends[i].materials[j]);
            }
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        UpPlane.position += new Vector3(0,0,0.01f);
        pos = UpPlane.position;
        nor = UpPlane.up;
        for (int i = 0; i < Materias.Count; i++)
        {
            Materias[i].SetVector("_cPos", pos);
            Materias[i].SetVector("_cNormal", nor);
        }
    }
}
