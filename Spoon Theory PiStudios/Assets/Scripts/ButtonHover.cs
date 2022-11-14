using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void ResetScene()
    {
        GameManager.GetInstance().ResetScene();
    }

    public void ClickingSound()
    {
        AudioManager.GetInstance().Play("left click", 1f);
    }

    public void HoverOnImage()
    {
        gameObject.GetComponent<Image>().color = new Color(0.754717f, 0.754717f, 0.754717f);
        AudioManager.GetInstance().Play(audioName, 1f);
    }
    public void ExitHoverOnImage()
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
        AudioManager.GetInstance().Play(audioName, 1f);
    }
}
