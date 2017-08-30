using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CameraRayCast : MonoBehaviour
{
    public Sprite sucessText;
    public Sprite failText;
    public Sprite sucessBack;
    public Sprite failBack;
    public SpriteRenderer backSR;
    public SpriteRenderer textSR;
    public GameObject resultButton;
    public GameObject VReye;
    private bool isOver = false;

    private void InitSR()
    {
        GameObject go = Instantiate(resultButton, new Vector3(2.5f, 1.2f, 2), Quaternion.identity * new Quaternion(0, 180, 0, 0)) as GameObject;
        go.transform.LookAt(VReye.transform);
        go.transform.Rotate(new Vector3(0, 1, 0), 180);
        backSR = go.GetComponent<SpriteRenderer>();
        textSR = go.transform.FindChild("ButtonTitle").GetComponent<SpriteRenderer>();
    }
    public void ShowSucess()
    {
        isOver = true;
        InitSR();
        backSR.sprite = sucessBack;
        textSR.sprite = sucessText;
    }

    public void ShowFail()
    {
        isOver = true;
        InitSR();
        backSR.sprite = failBack;
        textSR.sprite = failText;
    }

    public Camera leftCamera;
    public float waitTime = 2;
    public bool isShowWaitEffect = false;
    public Image imageWait;
    private RaycastHit hitInfo;
    private Ray ray;
    private string lastGoName = "";
    private string currentGoName = "";
    private float countTime = 0;
    private IButton currentButton = null;
    private void Start()
    {
        ray = leftCamera.ScreenPointToRay(new Vector2(Screen.width / 4, Screen.height / 2));
    }


    private void HitButton(bool isH)
    {
        if (isH)
        {
            //Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.name.Equals(lastGoName)) //注视的是同一个对象
            {
                Vector3 p = currentButton.gameObject.transform.InverseTransformPoint(hitInfo.point);
                if (currentButton != null)
                {
                    currentButton.OnButtonSlider(p);
                }
            }
            else //第一次注视到按钮对象
            {
                lastGoName = hitInfo.collider.name;
                if (currentButton != null)
                {
                    currentButton.OnButtonLeave();
                }
                currentButton = hitInfo.collider.GetComponent<IButton>();
                if (currentButton)
                {
                    currentButton.OnButtonEnter();
                }
            }
        }
        else
        {
            lastGoName = "";
            if (currentButton != null)
            {
                currentButton.OnButtonLeave();
            }
        }
    }
    private void HitCube(bool isH)
    {
        if (isH)
        {
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.name.Equals(lastGoName)) //注视的是同一个对象
            {
                if (isShowWaitEffect)
                {
                    countTime += Time.deltaTime; //时间累加，计时
                    if (countTime >= waitTime)
                    {
                        bool isSingle = hitInfo.collider.gameObject.GetComponent<CubeColor>().attr.isSingle;
                        if (!isOver)
                        {
                            if (isSingle)
                            {
                                ShowSucess();
                            }
                            else
                            {
                                ShowFail();
                            }
                        }
                        countTime = 0;
                    }
                }
            }
            else //第一次注视到按钮对象
            {
                lastGoName = hitInfo.collider.name;
                countTime = 0;
            }
        }
        else
        {
            lastGoName = "";
            countTime = 0;
        }
        imageWait.fillAmount = countTime / waitTime;
    }
    // Update is called once per frame
    void Update()
    {
        ray = leftCamera.ScreenPointToRay(new Vector2(Screen.width / 4, Screen.height / 2));
        bool isHit = Physics.Raycast(ray, out hitInfo);
        string tag = hitInfo.collider.tag;
        if (tag.Equals("Cube"))
        {
            HitCube(isHit);
        }
        else if (tag.Equals("Button"))
        {
            HitButton(isHit);
        }
    }
}
