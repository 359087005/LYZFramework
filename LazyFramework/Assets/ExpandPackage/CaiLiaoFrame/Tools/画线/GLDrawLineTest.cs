using UnityEngine;
using System.Collections;

public class GLDrawLineTest : MonoBehaviour {

    public bool isDrawLine = true;
    public Material lineMaterial;
    private float[] height;
	// Use this for initialization
    private int count;
    Vector3 start;
    Vector3 end;
    private float offsetX;
    private float offsetY;
    private float scaleX;
    private float scaleY;
    private int hNum;
	void Start () {

        if (GetComponent<Camera>() == null)
            Debug.LogError("GLDrawLineTest需要挂在Camera对象上");
        scaleX = 2.0f;
        scaleY = 0.3f;
        offsetX = 0.215f;
        offsetY = 0.5f*(1-scaleY)-0.026f;
        count = 0;
        hNum = (int)(400 / scaleX);
        height = new float[hNum];
        for (int i = 0; i < height.Length; i++)
        {
            height[i] = scaleY*Random.value;
        }

	}
	
	// Update is called once per frame

	void Update () {
        
	}

    public void OnPostRender()
    {
        if (isDrawLine) 
        { 
            if (!lineMaterial)
            {
                Debug.LogError("material == null");
                return;
            }

            if (count == height.Length)
            {
                count = height.Length;
            }
            else
            {
                count++;
            }

            GL.PushMatrix();
            //材质通道，0为默认。  
            lineMaterial.SetPass(0);
            //绘制2D图像  
            GL.LoadOrtho();
            //得到鼠标点信息总数量  
            GL.Begin(GL.LINES);
            GL.Color(Color.red);
            for (int i = 0; i < count - 1; i++)
            {
                start = new Vector3(i*scaleX / 1000f+offsetX, height[i]+offsetY, 0);
                end = new Vector3((i + 1)*scaleX / 1000f+offsetX, height[i + 1]+offsetY, 0);
                GL.Vertex(start);
                GL.Vertex(end);
            }
            //结束绘制  
            GL.End();
            GL.PopMatrix();
        }
        else
        {
            count = 0;
        }
    }





}
