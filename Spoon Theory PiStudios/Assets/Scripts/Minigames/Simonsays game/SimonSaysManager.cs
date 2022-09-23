using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysManager : MonoBehaviour
{
    int value, count, currentLength, maxStrikes, currentStrikes, timesSequencePlayed;

    List<int> currentSequence = new List<int>();

    [SerializeField] int initialLength, repeatTimes;

    [SerializeField] float initialTimeBetweenColors, resetColorTime, beginSequenceDelay;
    float currentTimeBetweenColors;

    [SerializeField] Image red, green, blue, yellow, winPanel, losePanel;
    [SerializeField] Image[] strikes;

    [SerializeField] Color blueCol, greenCol, yellowCol, redCol, whiteCol;
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        currentStrikes = 0;
        timesSequencePlayed = 0;
        maxStrikes = 3;
        currentLength = initialLength;
        currentTimeBetweenColors = initialTimeBetweenColors;

        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);

        DeactivateClick();

        CreateSequence();
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("red");
        value = 0;
        ChangeColor(red, redCol);
        CheckIfCorrect(value);
        count++;
    }

    public void YellowClick()
    {
        Debug.Log("yellow");
        value = 1;
        ChangeColor(yellow, yellowCol);
        CheckIfCorrect(value);
        count++;
    }

    public void BlueClick()
    {
        Debug.Log("blue");
        value = 2;
        ChangeColor(blue, blueCol);
        CheckIfCorrect(value);
        count++;
    }
    public void GreenClick()
    {
        Debug.Log("green");
        value = 3;
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
        img.color = whiteCol;
    }

    #endregion

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
            switch (currentSequence[i])
            {
                case 0:
                    ChangeColor(red, redCol);
                    break;

                case 1:
                    ChangeColor(yellow, yellowCol);
                    break;

                case 2:
                    ChangeColor(blue, blueCol);
                    break;

                case 3:
                    ChangeColor(green, greenCol);
                    break;
            }
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
        losePanel.gameObject.SetActive(true);
    }

    void Win()
    {
        winPanel.gameObject.SetActive(true);
    }
}
