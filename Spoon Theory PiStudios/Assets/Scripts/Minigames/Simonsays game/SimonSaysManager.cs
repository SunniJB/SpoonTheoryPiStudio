using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonSaysManager : MonoBehaviour
{
    int value, count, currentLength, maxStrikes, currentStrikes, timesSequencePlayed;

    [SerializeField] List<int> currentSequence = new List<int>();

    [SerializeField] int initialLength, repeatTimes;

    [SerializeField] float initialTimeBetweenColors, resetColorTime, beginSequenceDelay;
    float currentTimeBetweenColors;

    [SerializeField] Image red, green, blue, yellow, winPanel, losePanel;
    [SerializeField] Image[] strikes;
    [SerializeField] Image[] popUps;

    [SerializeField] Color blueCol, greenCol, yellowCol, redCol, whiteCol;

    public TMP_Text strikeText, PerformanceText, moneyText;
    public MinigameManager minigameManager;
    
    public void OnStartClick()
    {
        count = 0;
        currentStrikes = 0;
        timesSequencePlayed = 0;
        maxStrikes = 3;
        currentLength = initialLength;
        currentTimeBetweenColors = initialTimeBetweenColors;

        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);

        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].gameObject.SetActive(false);
        }

        DeactivateClick();

        CreateSequence();
    }

    void Update()
    {
        if (currentStrikes >= maxStrikes) { DeactivateClick(); return; }

        if (count > 0 && count >= currentSequence.Count)
        {
            if (timesSequencePlayed < repeatTimes)
                NextSequence();
            else Win();
        }
    }

    #region Click

    void DeactivateClick()
    {
        red.raycastTarget = false;
        yellow.raycastTarget = false;
        blue.raycastTarget = false;
        green.raycastTarget = false;
    }

    void ActivateClick()
    {
        red.raycastTarget = true;
        yellow.raycastTarget = true;
        blue.raycastTarget = true;
        green.raycastTarget = true;
    }

    public void RedClick()
    {
        value = 1;
        Debug.Log(value);
        ChangeColor(red, redCol);
        CheckIfCorrect(value);
        count++;
    }

    public void YellowClick()
    {
        value = 2;
        Debug.Log(value);
        ChangeColor(yellow, yellowCol);
        CheckIfCorrect(value);
        count++;
    }

    public void BlueClick()
    {
        value = 0;
        Debug.Log(value);
        ChangeColor(blue, blueCol);
        CheckIfCorrect(value);
        count++;
    }
    public void GreenClick()
    {
        value = 3;
        Debug.Log(value);
        ChangeColor(green, greenCol);
        CheckIfCorrect(value);
        count++;
    }

    void ChangeColor(Image img, Color color)
    {
        img.color = color;
        StartCoroutine(ResetColor(img));
    }

    IEnumerator ResetColor(Image img)
    {
        yield return new WaitForSeconds(resetColorTime);
        img.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
    }

    #endregion

    void ShowPopUp(int num)
    {
        popUps[num].gameObject.SetActive(true);
    }

    IEnumerator QuitPopUp(int num)
    {
        yield return new WaitForSeconds(0.2f);
        popUps[num].gameObject.SetActive(false);
    }

    void CreateSequence()
    {
        for (int i = 0; i < currentLength; i++)
        {
            value = Random.Range(0, 4);
            currentSequence.Add(value);
        }
        currentLength++;

        StartCoroutine(ShowSequence());
    }

    void ResetSequence()
    {
        timesSequencePlayed++;
        count = 0;
        currentSequence.Clear();
    }

    IEnumerator ShowSequence()
    {
        yield return new WaitForSeconds(beginSequenceDelay);
        int i = 0;
        while (i < currentSequence.Count)
        {
            ShowPopUp(currentSequence[i]);
            StartCoroutine(QuitPopUp(currentSequence[i]));
            //switch (currentSequence[i])
            //{
            //    case 0:
            //        ChangeColor(red, redCol);
            //        break;

            //    case 1:
            //        ChangeColor(yellow, yellowCol);
            //        break;

            //    case 2:
            //        ChangeColor(blue, blueCol);
            //        break;

            //    case 3:
            //        ChangeColor(green, greenCol);
            //        break;
            //}
            i++;
            if (i == currentSequence.Count) ActivateClick();
            yield return new WaitForSeconds(currentTimeBetweenColors);
        }
    }

    void NextSequence()
    {
        DeactivateClick();

        currentTimeBetweenColors -= 0.1f;

        ResetSequence();
        CreateSequence();
    }

    void CheckIfCorrect(int value)
    {
        if (value != currentSequence[count])
        {
            currentStrikes++;
            strikes[currentStrikes - 1].gameObject.SetActive(true);
            NextSequence();
        }

        if (currentStrikes >= maxStrikes) Lose();
    }

    void Lose()
    {
        Win();
        /*
        minigameManager.Complete(3, currentStrikes * 10, 50);

        strikeText.text = "You got " + currentStrikes + " orders wrong!";
        PerformanceText.text = "Performance Review: " + minigameManager.GetWorkPerform().ToString("00") + "/50";
        moneyText.text = "You earned: £" + minigameManager.GetMoney().ToString("00");
        losePanel.gameObject.SetActive(true); */
    }

    public void Win()
    {
        if (currentStrikes == 0)
            minigameManager.Complete(3, 5, 50);
        else
            minigameManager.Complete(3, currentStrikes * 10, 50);

        strikeText.text = "You got " + currentStrikes +" orders wrong!";
        PerformanceText.text = "Performance Review: " + minigameManager.GetWorkPerform().ToString("00") + "/50";
        moneyText.text = "You earned: £" + minigameManager.GetMoney().ToString("00");
        winPanel.gameObject.SetActive(true);
    }

    public void LeaveAndGoHome()
    {
        GameManager.GetInstance().SetTimeAfternoon();
        GameManager.GetInstance().ApartmentScene();
    }
}
