using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ClickMemory : MonoBehaviour
{
    public Sprite[] images = new Sprite[2];
    private int arrayPos = 1;
    private static bool canClick = true;

    private void Start()
    {
        canClick = true;
    }
    public void OnClick()
    {
        if (canClick)
        {
            if (arrayPos == 1)
            {
                GetComponent<Image>().overrideSprite = images[arrayPos];

                    arrayPos = 0;
                
                if (MemoryManager.instance.clickOne == null)
                {
                    MemoryManager.instance.clickOne = images[1];
                    MemoryManager.instance.firstClickObj = this.gameObject;
                    canClick = false;
                    Invoke("CanClick", 0.3f);
                }
                else
                {
                    MemoryManager.instance.clickTwo = images[1];
                    if (MemoryManager.instance.clickOne == MemoryManager.instance.clickTwo)
                    {
                        Debug.Log("You Win Lmao"); // wincon!!!!!
                        MemoryManager.instance.clickOne = null;
                        MemoryManager.instance.clickTwo = null;
                        MemoryManager.instance.wins++;
                        canClick = false;
                        Invoke("CanClick", 0.5f);
                    }
                    else
                    {
                        MemoryManager.instance.clickOne = null;
                        MemoryManager.instance.clickTwo = null;
                        Invoke("Fail", 0.5f);
                        canClick = false;
                    }
                }
            }
            else
            {
                Debug.Log("You can't flip it back dumbo!");
            }
        }
    }

    public void Fail()
    {
        Debug.Log("no cigar");
        MemoryManager.instance.clickOne = null;
        MemoryManager.instance.clickTwo = null;
        GetComponent<Image>().overrideSprite = images[0];
        MemoryManager.instance.firstClickObj.GetComponent<Image>().overrideSprite = images[0];
        arrayPos = 1;
        MemoryManager.instance.firstClickObj.GetComponent<ClickMemory>().arrayPos = 1;
        CanClick();
    }

    private void CanClick()
    {
        canClick = true;
    }
}
