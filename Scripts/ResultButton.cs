using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class ResultButton : IButton
{
    public override void OnButtonConfirm()
    {
        SceneManager.LoadScene("SceneMenu");
    }
}
