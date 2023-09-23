using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuItemQuit : MainMenuItem
{
    public override void Activate()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
