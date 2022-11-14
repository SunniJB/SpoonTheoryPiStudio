using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLibrary : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, mainPage, lupusMainpage, msMainpage, lupusWhatIsIt, msWhatIsIt, lupusSymptoms, msSymptoms, lupusLife, msLife;
    public bool currentlyOnLupus, currentlyOnMS, currentlyOnNothing;
    // Start is called before the first frame update
    private void Update()
    {
        if (lupusWhatIsIt.activeInHierarchy || lupusSymptoms.activeInHierarchy || lupusLife.activeInHierarchy)
        {
            currentlyOnLupus = true;
            currentlyOnNothing = false;
        } else {currentlyOnLupus = false;}

        if (msWhatIsIt.activeInHierarchy || msSymptoms.activeInHierarchy || msLife.activeInHierarchy)
        {
            currentlyOnMS = true;
            currentlyOnNothing = false;
        } else { currentlyOnMS = false; }
    }
    public void GoBack()
    {
        if (currentlyOnLupus)
        {
            lupusWhatIsIt.SetActive(false);
            lupusSymptoms.SetActive(false);
            lupusLife.SetActive(false);
            lupusMainpage.SetActive(true);
        }

        if (currentlyOnMS)
        {
            msWhatIsIt.SetActive(false);
            msSymptoms.SetActive(false);
            msLife.SetActive(false);
            msMainpage.SetActive(true);
        }

        if (!currentlyOnLupus && !currentlyOnMS)
        {
            msMainpage.SetActive(false);
            lupusMainpage.SetActive(false);
            mainPage.SetActive(true);

            StartCoroutine(ExecuteAfterTime(0.5f)); //Makes you wait a second before returning to main menu
            Debug.Log("Started waiting");
        }

        if (currentlyOnNothing)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ExecuteAfterTime(float time) //Makes you wait a second before returning to main menu
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Finished waiting");
        currentlyOnNothing = true;
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
        currentlyOnNothing = false;
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
        currentlyOnNothing = false;
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
