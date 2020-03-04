using UnityEngine;
using System.Collections;

public class GLDrawLine : MonoBehaviour
{
    public static GLDrawLine instance;
    public bool isCanDraw = false;
    public Material lineMaterial;
    public Vector3[] posArr = new Vector3[4];
    // Use this for initialization
    void Start()
    {
        if (GetComponent<Camera>() == null)
            Debug.LogError("GLDrawLine需要挂在Camera对象上");
        instance = this;

        isCanDraw = true;
    }
    void SetPos()
    {
        posArr[1].x = Screen.width;
        posArr[1].y = Screen.height;
    }

    public void OnPostRender()
    {
        if (!isCanDraw)
            return;

        SetPos();
        if (!lineMaterial)
        {
            //Debug.LogError("material == null");
            return;
        }

        GL.PushMatrix();

        //材质通道，0为默认。  
        lineMaterial.SetPass(0);
        //绘制2D图像  
        GL.LoadOrtho();
        //得到鼠标点信息总数量  
        GL.Begin(GL.LINES);

        GL.Color(Color.red);

        //GL.Color(new Color(218f / 255f, 1, 0, 2f / 3f)); 

        for (int i = 0; i < posArr.Length - 1; i++)
        {
            Vector3 start = posArr[i];
            Vector3 end = posArr[i + 1];
            //	Debug.Log ("start.x="+start.x+"start.y="+start.y);
            GL.Vertex(new Vector3(start.x / Screen.width, start.y / Screen.height, 0));
            GL.Vertex(new Vector3(end.x / Screen.width, end.y / Screen.height, 0));
        }

        //结束绘制  
        GL.End();

        GL.PopMatrix();
    }


}

































































































