using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLibrary : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, mainPage, lupusMainpage, msMainpage, lupusWhatIsIt, msWhatIsIt, lupusSymptoms, msSymptoms, lupusLife, msLife;
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
    public void GoBackTomainMenu()
    {
        mainPage.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void GoTolupusMainpage()
    {
        lupusMainpage.SetActive(true);
        mainPage.SetActive(false);
    }
    public void GoTolupusWhatIsIt()
    {
        lupusMainpage.SetActive(false);
        lupusSymptoms.SetActive(false);
        lupusLife.SetActive(false);
        lupusWhatIsIt.SetActive(true);
    }
    public void GoTolupusSymptoms()
    {
        lupusMainpage.SetActive(false);
        lupusSymptoms.SetActive(true);
        lupusLife.SetActive(false);
        lupusWhatIsIt.SetActive(false);
    }
    public void GoTolupusLife()
    {
        lupusMainpage.SetActive(false);
        lupusSymptoms.SetActive(false);
        lupusLife.SetActive(true);
        lupusWhatIsIt.SetActive(false);
    }
    public void GoToMsMainpage()
    {
        msMainpage.SetActive(true);
        mainPage.SetActive(false);
    }
    public void GoToMsWhatIsIt()
    {
        msMainpage.SetActive(false);
        msSymptoms.SetActive(false);
        msLife.SetActive(false);
        msWhatIsIt.SetActive(true);
    }
    public void GoToMsSymptoms()
    {
        msMainpage.SetActive(false);
        msSymptoms.SetActive(true);
        msLife.SetActive(false);
        msWhatIsIt.SetActive(false);
    }
    public void GoToMsLife()
    {
        msMainpage.SetActive(false);
        msSymptoms.SetActive(false);
        msLife.SetActive(true);
        msWhatIsIt.SetActive(false);
    }
}
