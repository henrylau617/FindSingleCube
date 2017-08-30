using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomButton : IButton
{

    public override void OnButtonConfirm()
    {
        SceneManager.LoadScene("SceneGame");
    }
}
