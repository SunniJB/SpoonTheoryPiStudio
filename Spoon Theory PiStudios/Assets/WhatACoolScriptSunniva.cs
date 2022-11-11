using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatACoolScriptSunniva : MonoBehaviour
{
    [SerializeField] GameObject mainPage, lupusMainpage, msMainpage, lupusWhatIsIt, msWhatIsIt, lupusSymptoms, msSymptoms, lupusLife, msLife;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GoBack()
    {

    }

    public void GoToLupusMain()
    {
        lupusMainpage.SetActive(true);
        mainPage.SetActive(false);
    }

    public void GoToMSMain()
    {

    }
}
