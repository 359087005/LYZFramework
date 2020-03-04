using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>/// 获取切割材质/// </summary>
public class ShaderGraphPoQie: MonoBehaviour 
{
    public Transform UpPlane;

    private Vector3 pos;
    private Vector3 nor;
    public List<Material> Materias = new List<Material>();
    public List<Transform> trans = new List<Transform>();
    void Start() 
    {
    }

    [ContextMenu("OnEnable")]
    void OnEnable() 
    {
        Materias = new List<Material>();
        trans = new List<Transform>();
        MeshRenderer[] rends = this.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < rends.Length; i++)
        {
            for (int j = 0; j < rends[i].materials.Length; j++)
            {
                Materias.Add(rends[i].materials[j]);
                trans.Add(rends[i].transform);
            }
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        pos = UpPlane.position;
        nor = UpPlane.up;
        for (int i = 0; i < Materias.Count; i++)
        {
            Materias[i].SetVector("Vector3_3A974511", pos);
            Materias[i].SetVector("Vector3_ED34274A", nor);
            Materias[i].SetVector("Vector3_C4532279", trans[i].position);
            Materias[i].SetVector("Vector3_1DD7A5BA", trans[i].localScale);
        }
    }
}
