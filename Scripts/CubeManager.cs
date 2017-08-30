using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private GameObject[] cubes = new GameObject[99];
    private int count;
    private int randNum = -1;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(InitCube());
    }

    IEnumerator InitCube()
    {
        yield return new WaitForSeconds(1);
        count = 0;
        string obName = "Cube" + count;
        cubes[count] = GameObject.Find(obName);
        while (cubes[count] != null)
        {
            count++;
            string obName2 = "Cube" + count;
            cubes[count] = GameObject.Find(obName2);
        }
        randNum = Random.Range(0, count);//单身块索引值
        Debug.Log("Count的值：" + count);
        Debug.Log("随机出来的单身块索引值是:" + randNum);
        //开始进行随机Cube染色
        PaintCube();
    }

    private void PaintCube()
    {
        List<int> indexs = new List<int>();//存储染色过的cube索引值
        int paintTimes = (count / 2);//染色材质球数量
        int paintIndex = Random.Range(0, paintTimes );//单身块染色材质球索引
        Debug.Log("单身块染色材质球数量" + paintTimes);
        //单身块染色
        Debug.Log("随机出来的单身块染色材质球索引值是:" + paintIndex);
        DoPaint(randNum, paintIndex);
        indexs.Add(randNum);
        cubes[randNum].GetComponent<CubeColor>().attr.isSingle = true;//设置单身块属性为true
        int cubeNum = 0;
        bool isPainted = false;
        for (int i = 0; i < paintTimes; i++)
        {
            if (i == paintIndex)
            {
                continue;
            }
            //每次染色染了两个cube才退出内循环
            while (cubeNum < 2)
            {
                int rNum = Random.Range(0, count);//随机一个cube索引值
                foreach (var cIndex in indexs)
                {
                    if (rNum == cIndex)
                    {
                        isPainted = true;
                        break;
                    }
                    else
                    {
                        isPainted = false;
                    }
                }
                if (isPainted == false)
                {
                    DoPaint(rNum, i);
                    Debug.Log("染色方块索引" + rNum + "+++染色材质球索引" + i);
                    indexs.Add(rNum);
                    cubeNum++;
                }
            }
            cubeNum = 0;
        }
    }


    public void DoPaint(int cubeIndex, int index)
    {
        CubeColor cc = cubes[cubeIndex].GetComponent<CubeColor>();
        switch (index)
        {
            case 0:
                cc.color = cubeColor.black;
                break;
            case 1:
                cc.color = cubeColor.blue;
                break;
            case 2:
                cc.color = cubeColor.green;
                break;
            case 3:
                cc.color = cubeColor.orange;
                break;
            case 4:
                cc.color = cubeColor.purple;
                break;
            case 5:
                cc.color = cubeColor.red;
                break;
            case 6:
                cc.color = cubeColor.white;
                break;
            case 7:
                cc.color = cubeColor.yellow;
                break;
        }

    }
}

