using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureColorchange : MonoBehaviour
{
    public int y;
    public Texture2D texture;
    public Color brush;
    public List<Vector2> vector2s = new List<Vector2>();
    public int range;
    
    private void Start()
    {
        texture = (Texture2D) this.GetComponent<RawImage>().texture;
    }
    private List<Vector2> AddRangePos(Vector2 point)
    {
        List<Vector2> temp = new List<Vector2>();
        for (int i = 0; i < range; i++)
        {
            for (int j = 0; j < range; j++)
            {
                temp.Add(new Vector2(point.x - (int)j + range / 2, point.y - ((int)i) + range / 2));
            }
        }
        return temp;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Deb(AddRangePos(Input.mousePosition));
        }
    }
    public void Deb(List<Vector2> _vector2s)
    {
        for (int i = 0; i < _vector2s.Count; i++)
        {
            texture.SetPixel((int)_vector2s[i].x, (int)_vector2s[i].y, brush);
        }
        texture.Apply();
    }
}
