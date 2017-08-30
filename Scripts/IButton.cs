using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 所有可以交互的按钮的基类
/// </summary>
namespace Assets.Scripts
{
    public abstract class IButton : MonoBehaviour
    {
        protected float lastPercent;
        protected bool bHasConfirmedSend;
        protected Material mat;
        protected SpriteRenderer sr;
        public Sprite hintSprite;
        public float collderLength = 2.4f;
        void Start()
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
        }
        public void OnButtonEnter()
        {
            lastPercent = 0.3f;
            mat.SetFloat("_Percent", lastPercent);
            mat.SetFloat("_Borderp", 1);
        }
        public void OnButtonLeave()
        {
            bHasConfirmedSend = false;
            lastPercent = 0;
            mat.SetFloat("_Percent", lastPercent);
            mat.SetFloat("_Borderp", 0);
        }

        public virtual void OnButtonSlider(Vector3 p)
        {
            float perc = (p.x + collderLength / 2) / collderLength;
            if (perc < 0)
            {
                perc = 0;
            }
            else if (perc > 1)
            {
                perc = 1;
            }
            if (Mathf.Abs(lastPercent - perc) < 0.1f)
            {
                lastPercent = perc;
                mat.SetFloat("_Percent", perc);

                if (perc > 0.9f && !bHasConfirmedSend)
                {
                    bHasConfirmedSend = true;
                    OnButtonConfirm();
                }
            }
        }

        public abstract void OnButtonConfirm();
    }
}