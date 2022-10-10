using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public string audioName = "hover over button";
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }
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
        gameManager.ResetScene();
    }

    public void ClickingSound()
    {
        AudioManager.GetInstance().Play("left click", 1f);
    }
}
