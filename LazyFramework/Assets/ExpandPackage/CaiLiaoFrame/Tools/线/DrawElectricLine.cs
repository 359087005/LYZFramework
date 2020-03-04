using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 绘制电线
/// </summary>
public class DrawElectricLine : MonoBehaviour {

    /// <summary>
    /// 是否为固定线
    /// </summary>
    public bool isStatic;
    /// <summary>
    /// 是否会移动（实时）
    /// </summary>
    public bool isNeedMove;
    /// <summary>
    /// 自动生成导线所需时间
    /// </summary>
    public float timer = 1;

    LineRenderer render;

    /// <summary>
    /// 顺序目标点
    /// </summary>
    public Transform[] tranNodes = new Transform[5];
    Vector3[] tempVecs;

    void Start()
    {
        render = GetComponent<LineRenderer>();
        if (isStatic)
            DrawLine();
        if (isNeedMove)
            DrawMoveLine();
    }
    
    /// <summary>
    /// 绘制固定不动的线
    /// </summary>
    void DrawLine()
    {
        GetPoints();
        render.positionCount = tempVecs.Length;
        //render.SetVertexCount(tempVecs.Length);
        render.SetPositions(tempVecs);
        render.SetPosition(0, tranNodes[0].position);
        render.SetPosition(tempVecs.Length - 1, tranNodes[tranNodes.Length-1].position);
    }
    /// <summary>
    /// 某些需要移动的线，实时绘制
    /// </summary>
    void Update()
    {
    }
    /// <summary>
    /// 根据Transform获取目标路径点
    /// </summary>
    void GetPoints()
    {
        Vector3[] vecs = new Vector3[tranNodes.Length];
        for (int i = 0; i < vecs.Length; i++)
        {
            vecs[i] = tranNodes[i].position;
        }

        tempVecs = DrawPathHelper(vecs);
    }
    /// <summary>
    /// 绘制移动线
    /// </summary>
    public void DrawMoveLine()
    {
        GetPoints();
        StartCoroutine("MoveLine");
    }
    /// <summary>
    /// 自动线条生成
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveLine ()
    {
        WaitForSeconds wait = new WaitForSeconds(1 / tempVecs.Length);
        for (int i = 0; i < tempVecs.Length; i++)
        {
            render.positionCount = i + 1;
            //render.SetVertexCount(i+1);
            render.SetPosition(i, tempVecs[i]);
            yield return wait;
        }
    }

    /// <summary>
    /// 根据给定路线点绘制路线
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private Vector3[] DrawPathHelper(Vector3[] path)
    {
        Vector3[] vector3s = PathControlPointGenerator(path);

        //Line Draw:
        Vector3 prevPt = Interp(vector3s, 0);
        int SmoothAmount = path.Length * 20;
        Vector3[] retValue = new Vector3[SmoothAmount + 1];
        retValue[0] = prevPt;

        for (int i = 1; i <= SmoothAmount; i++)
        {
            float pm = (float)i / SmoothAmount;
            Vector3 currPt = Interp(vector3s, pm);
            retValue[i] = currPt;
        }
        return retValue;
    }

    /// <summary>
    /// 路线控制点生成
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private Vector3[] PathControlPointGenerator(Vector3[] path)
    {
        Vector3[] suppliedPath;
        Vector3[] vector3s;
        
        suppliedPath = path;
        
        int offset = 2;
        vector3s = new Vector3[suppliedPath.Length + offset];
        Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);

        vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
        vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);
        
        if (vector3s[1] == vector3s[vector3s.Length - 2])
        {
            Vector3[] tmpLoopSpline = new Vector3[vector3s.Length];
            Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
            tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
            tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
            vector3s = new Vector3[tmpLoopSpline.Length];
            Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
        }

        return (vector3s);
    }

    /// <summary>
    /// 中间处理
    /// </summary>
    /// <param name="pts"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private Vector3 Interp(Vector3[] pts, float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
        float u = t * (float)numSections - (float)currPt;

        Vector3 a = pts[currPt];
        Vector3 b = pts[currPt + 1];
        Vector3 c = pts[currPt + 2];
        Vector3 d = pts[currPt + 3];

        return .5f * (
            (-a + 3f * b - 3f * c + d) * (u * u * u)
            + (2f * a - 5f * b + 4f * c - d) * (u * u)
            + (-a + c) * u
            + 2f * b
        );
    }
}
