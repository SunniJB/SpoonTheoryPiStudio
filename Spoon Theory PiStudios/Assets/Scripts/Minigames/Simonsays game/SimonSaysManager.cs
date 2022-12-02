using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonSaysManager : MonoBehaviour
{
    int value, count, currentLength, maxStrikes, currentStrikes;
    [SerializeField] int timesSequencePlayed = 0;

    [SerializeField] List<int> currentSequence = new();

    [SerializeField] int initialLength, repeatTimes;

    [SerializeField] float initialTimeBetweenColors, minTimeBetweenColors, resetColorTime, beginSequenceDelay;
    float currentTimeBetweenColors;

    [SerializeField] Image red, green, blue, yellow, winPanel, losePanel;
    [SerializeField] Image[] strikes;
    [SerializeField] Image[] popUps;

    [SerializeField] Color blueCol, greenCol, yellowCol, redCol, whiteCol;

    public TMP_Text strikeText, PerformanceText, moneyText;
    public MinigameManager minigameManager;
    bool gotpaid;

    bool canClick, failed;
    
    public void OnStartClick()
    {
        Time.timeScale = 1;
        count = 0;
        currentStrikes = 0;
        timesSequencePlayed = 1;
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

        if (failed) return;

        if (count > 0 && count >= currentSequence.Count)
        {
            if (timesSequencePlayed < repeatTimes)
                NextSequence();
            else Complete();
        }
    }

    #region Click

    void DeactivateClick()
    {
        canClick = false;

        red.raycastTarget = false;
        yellow.raycastTarget = false;
        blue.raycastTarget = false;
        green.raycastTarget = false;
    }

    void ActivateClick()
    {
        canClick = true;

        red.raycastTarget = true;
        yellow.raycastTarget = true;
        blue.raycastTarget = true;
        green.raycastTarget = true;
    }

    public void RedClick()
    {
        value = 1;
        ChangeColor(red, redCol);
        CheckIfCorrect(value);
    }

    public void YellowClick()
    {
        value = 2;
        ChangeColor(yellow, yellowCol);
        CheckIfCorrect(value);
    }

    public void BlueClick()
    {
        value = 0;
        ChangeColor(blue, blueCol);
        CheckIfCorrect(value);
    }
    public void GreenClick()
    {
        value = 3;
        ChangeColor(green, greenCol);
        CheckIfCorrect(value);
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
        yield return new WaitForSeconds(currentTimeBetweenColors - 0.3f);
        popUps[num].gameObject.SetActive(false);
    }

    void CreateSequence()
    {
        failed = false;

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

        for (int i = 0; i < currentSequence.Count; i++)
        {
            ShowPopUp(currentSequence[i]);
            StartCoroutine(QuitPopUp(currentSequence[i]));

            if (i == currentSequence.Count - 1) ActivateClick();

            yield return new WaitForSeconds(currentTimeBetweenColors);
        }
    }

    void NextSequence()
    {
        DeactivateClick();

        currentTimeBetweenColors -= 0.05f;
        if (currentTimeBetweenColors < minTimeBetweenColors) currentTimeBetweenColors += .1f;

        ResetSequence();
        CreateSequence();
    }

    void CheckIfCorrect(int value)
    {
        if (!canClick) return;

        Debug.Log("clicked value: " + value + "\tsequence value: " + currentSequence[count]);
        bool check = value != currentSequence[count];
        count++;

        if (check)
        {
            failed = true;

            DeactivateClick();

            StopCoroutine(ShowSequence());
            ResetSequence();
            currentStrikes++;
            strikes[currentStrikes - 1].gameObject.SetActive(true);
            AudioManager.GetInstance().Play("wrongSimon");

            currentTimeBetweenColors -= 0.1f;
            if (currentTimeBetweenColors < minTimeBetweenColors) currentTimeBetweenColors += .1f;

            if (currentStrikes >= maxStrikes) Complete();
            else Invoke(nameof(CreateSequence), 1);
        }
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
    public void Complete()
    {
        canClick = false;
        StopAllCoroutines();
        ResetSequence();

        if (!gotpaid)
        {
            if (currentStrikes == 0)
                minigameManager.Complete(3, 5, 50);
            else
                minigameManager.Complete(3, currentStrikes * 10, 50);

            gotpaid = true;
        }
    }

    public void Win()
    {
        strikeText.text = "You got " + currentStrikes +" orders wrong!";
         if (currentStrikes == 3)
        {
            PerformanceText.text = "You didn't do great. There is always tomorrow.";
        }
        if (currentStrikes == 2)
        {
            PerformanceText.text = "You did some good work today.";
        }
        if (currentStrikes <= 1)
        {
            PerformanceText.text = "Wow, you did great!";
        }
        moneyText.text = "You earned: £" + minigameManager.GetMoney().ToString("00");
        winPanel.gameObject.SetActive(true);
    }

    public void LeaveAndGoHome()
    {
        GameManager.GetInstance().SetTimeAfternoon();
        GameManager.GetInstance().ApartmentScene();
    }
}
