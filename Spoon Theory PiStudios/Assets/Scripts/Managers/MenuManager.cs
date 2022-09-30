using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void Begin()
    {
        GameManager.GetInstance().ApartmentScene();
    }

    public void Levels()
    {

    }

    public void Library()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
