using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuItemPlay : MainMenuItem
{
    public override void Activate()
    {
        SceneManager.LoadScene("Level - 001");
    }
}
