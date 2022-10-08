using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public string audioName = "hover over button";
    public void MenuButtonON()
    {
        AudioManager.GetInstance().Play(audioName, 1f);
    }

    public void MenuButtonOFF()
    {
        AudioManager.GetInstance().Play(audioName, 1f);
    }
}
