using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMemory : MonoBehaviour
{
    public Sprite[] images = new Sprite[2];
    private int arrayPos = 1; 

    public void RevertToStartImage()
    {
        GetComponent<Image>().overrideSprite = images[0];
    }

    public void OnClick()
    {
        GetComponent<Image>().overrideSprite = images[arrayPos];
        if (arrayPos == 1)
            arrayPos = 0;
        else
            arrayPos = 1;
    }
}
