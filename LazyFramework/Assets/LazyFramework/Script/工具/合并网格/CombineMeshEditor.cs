/*******************************************************************************
* 版本声明：v1.0.0
* 类 名 称： CombineMeshEditor
* 创建日期：2019-1-2 15:12:32
* 作者名称：白煜
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：合并网格
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditor(typeof(CombieMesh))]
public class CombineMeshEditor : Editor
{

    public CombieMesh mesh;

    private void OnEnable()
    {
        mesh = (CombieMesh)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("合并同材质网格"))
        {
            CombineSameMesh(mesh.transform);
        }
        if (GUILayout.Button("合并不同材质网格"))
        {
            CombineDifferentMesh(mesh.transform);
        }
        EditorGUILayout.EndVertical();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ts"></param>
    void CombineSameMesh(Transform ts)
    {
        MeshFilter[] meshFilters = ts.GetComponentsInChildren<MeshFilter>();    //获取自身和所有子物体中所有MeshFilter组件
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];     //新建CombineInstance数组

        MeshRenderer[] Renderer = ts.GetComponentsInChildren<MeshRenderer>();  //获取自身和所有子物体中所有MeshRenderer组件
        Material[] mats = new Material[Renderer.Length];                    //新建材质球数组

        for (int i = 0; i < meshFilters.Length; i++)
        {
            mats[i] = Renderer[i].sharedMaterial;                           //获取材质球列表

            combine[i].mesh = meshFilters[i].sharedMesh;
            //矩阵(Matrix)自身空间坐标的点转换成世界空间坐标的点   
            //combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //变换矩阵的问题，要保持相对位置不变，要转换为父节点的本地坐标，
            combine[i].transform = ts.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;

            if (meshFilters[i].gameObject.name != mesh.name)      //除了根物体，其他物体统统销毁
            {
                meshFilters[i].gameObject.SetActive(false);
            }
        }
        if (!ts.GetComponent<MeshFilter>())
        {
            ts.gameObject.AddComponent<MeshFilter>();
        }
        ts.GetComponent<MeshFilter>().mesh = new Mesh();
        ts.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, true);/*为mesh.CombineMeshes添加一个 false 参数，表示并不是合并为一个网格，而是一个子网格列表，可以让拥有多个材质球，如果要合并的网格
        用的是同一材质，false改为true，同时将上面的获取Material的代码去掉*/
        if (!ts.GetComponent<MeshRenderer>())
        {
            ts.gameObject.AddComponent<MeshRenderer>();
        }
        ts.GetComponent<MeshRenderer>().sharedMaterial = mats[0];          //为合并后的GameObject指定材质球数组
        ts.GetComponent<MeshRenderer>().receiveShadows = false;
        ts.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
    }
    void CombineDifferentMesh(Transform ts)
    {
        MeshFilter[] meshFilters = ts.GetComponentsInChildren<MeshFilter>();    //获取自身和所有子物体中所有MeshFilter组件
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];     //新建CombineInstance数组

        MeshRenderer[] Renderer = ts.GetComponentsInChildren<MeshRenderer>();  //获取自身和所有子物体中所有MeshRenderer组件
        Material[] mats = new Material[Renderer.Length];                    //新建材质球数组

        for (int i = 0; i < meshFilters.Length; i++)
        {
            mats[i] = Renderer[i].sharedMaterial;                           //获取材质球列表

            combine[i].mesh = meshFilters[i].sharedMesh;
            //矩阵(Matrix)自身空间坐标的点转换成世界空间坐标的点   
            //combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //变换矩阵的问题，要保持相对位置不变，要转换为父节点的本地坐标，
            combine[i].transform = ts.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;

            if (meshFilters[i].gameObject.name != mesh.name)      //除了根物体，其他物体统统销毁
            {
                //DestroyImmediate(meshFilters[i].gameObject);
                meshFilters[i].gameObject.SetActive(false);
            }
        }
        if (!ts.GetComponent<MeshFilter>())
        {
            ts.gameObject.AddComponent<MeshFilter>();
        }
        ts.GetComponent<MeshFilter>().mesh = new Mesh();
        ts.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, false);/*为mesh.CombineMeshes添加一个 false 参数，表示并不是合并为一个网格，而是一个子网格列表，可以让拥有多个材质球，如果要合并的网格
        用的是同一材质，false改为true，同时将上面的获取Material的代码去掉*/
        if (!ts.GetComponent<MeshRenderer>())
        {
            ts.gameObject.AddComponent<MeshRenderer>();
        }
        ts.GetComponent<MeshRenderer>().sharedMaterials = mats;          //为合并后的GameObject指定材质球数组
        ts.GetComponent<MeshRenderer>().receiveShadows = false;
        ts.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
    }
   
}
