using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public Sprite sucessText;
    public Sprite failText;
    public Sprite sucessBack;
    public Sprite failBack;
    public SpriteRenderer backSR;
    public SpriteRenderer textSR;
    public GameObject resultButton;
    public GameObject VReye;

    private void InitSR()
    {
        GameObject go = Instantiate(resultButton, new Vector3(2.5f, 1.2f, 2), Quaternion.identity) as GameObject;
        go.transform.LookAt(VReye.transform);
        backSR = go.GetComponent<SpriteRenderer>();
        textSR = go.transform.FindChild("ButtonTitle").GetComponent<SpriteRenderer>();
    }
    public void ShowSucess()
    {

        InitSR();
        backSR.sprite = sucessBack;
        textSR.sprite = sucessText;
    }

    public void ShowFail()
    {
        InitSR();
        backSR.sprite = failBack;
        textSR.sprite = failText;
    }
}
