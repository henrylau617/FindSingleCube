using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBaseAttr
{
    public Material[] allColors = new Material[8];
    public bool isSingle = false;
}

public class AttrFactory
{
    private const string materialPath = "Materials/Cube/";
    private string[] matName = new string[8] { "Black", "Blue", "Green", "Orange", "Purple", "Red", "White", "Yellow" };
    //private const string
    public CubeBaseAttr GetAttr()
    {
        CubeBaseAttr attr = new CubeBaseAttr();
        int index = 0;
        foreach (var name in matName)
        {
            Object o = LoadAsset(materialPath + name);
            attr.allColors[index] = o as Material;
            index++;
        }
        return attr;
    }

    public UnityEngine.Object LoadAsset(string path)
    {
        UnityEngine.Object o = Resources.Load(path);
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径：" + path);
            return null;
        }
        return o;
    }
}
