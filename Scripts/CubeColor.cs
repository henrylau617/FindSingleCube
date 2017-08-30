using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum cubeColor
{
    black,
    blue,
    green,
    orange,
    purple,
    red,
    white,
    yellow,
    n,
}

public class CubeColor : MonoBehaviour
{

    public CubeBaseAttr attr = null;
    public cubeColor color = cubeColor.n;
    protected bool isPaint = false;
    protected Renderer renderer;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        attr = new AttrFactory().GetAttr();
    }

    // Update is called once per frame
    void Update()
    {
        if (color != cubeColor.n && isPaint == false)
        {
            isPaint = true;
            UpdateColor(color);
        }
    }

    private void UpdateColor(cubeColor c)
    {
        switch (c)
        {
            case cubeColor.black:
                renderer.material = attr.allColors[0];
                break;
            case cubeColor.blue:
                renderer.material = attr.allColors[1];
                break;
            case cubeColor.green:
                renderer.material = attr.allColors[2];
                break;
            case cubeColor.orange:
                renderer.material = attr.allColors[3];
                break;
            case cubeColor.purple:
                renderer.material = attr.allColors[4];
                break;
            case cubeColor.red:
                renderer.material = attr.allColors[5];
                break;
            case cubeColor.white:
                renderer.material = attr.allColors[6];
                break;
            case cubeColor.yellow:
                renderer.material = attr.allColors[7];
                break;
        }
    }
}
