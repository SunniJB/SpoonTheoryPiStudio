using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLibrary : MonoBehaviour
{
    [SerializeField] GameObject mainPage, lupusMainpage, msMainpage, lupusWhatIsIt, msWhatIsIt, lupusSymptoms, msSymptoms, lupusLife, msLife;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GoBack()
    {
        lupusMainpage.SetActive(false);
        msMainpage.SetActive(false);
        mainPage.SetActive(true);
    }
    public void GoTolupusMainpage()
    {
        lupusMainpage.SetActive(true);
        mainPage.SetActive(false);
    }
    public void GoToMsMainpage()
    {
        msMainpage.SetActive(true);
        mainPage.SetActive(false);
    }

}
