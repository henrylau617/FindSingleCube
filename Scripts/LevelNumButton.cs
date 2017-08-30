using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
public class LevelNumButton : IButton
{
    private LevelNum levelNum;
    public enum OpType
    {
        Add,
        Dec,
    }

    public OpType op;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = sr.GetComponent<Renderer>().material;
        if (hintSprite)
        {
            var baser = sr.sprite.rect;
            var herer = hintSprite.rect;
            var fw = herer.width / baser.width;
            var fh = herer.height / baser.height;
            mat.SetFloat("_CursorWid", fw);
            mat.SetFloat("_CursorHei", fw);
            mat.SetFloat("_CursorOff", Random.Range(0.0f, 1f));
            mat.SetTexture("_CursorTex", hintSprite.texture);
        }
        else
        {
            mat.SetFloat("_CursorWid", 0);
            mat.SetFloat("_CursorHei", 0);
        }
        StartCoroutine(DelayCacheNumObj());
    }

    IEnumerator DelayCacheNumObj()
    {
        yield return new WaitForSeconds(1.5f);
        levelNum = GameObject.FindObjectOfType<LevelNum>();
    }
    public override void OnButtonConfirm()
    {
        switch (op)
        {
            case OpType.Add:
                if (levelNum)
                {
                    levelNum.AddLevel();
                    Debug.Log("Add");
                }
                break;
            case OpType.Dec:
                if (levelNum)
                {
                    levelNum.DecLevel();
                    Debug.Log("Add");
                }
                break;
        }
    }
}
