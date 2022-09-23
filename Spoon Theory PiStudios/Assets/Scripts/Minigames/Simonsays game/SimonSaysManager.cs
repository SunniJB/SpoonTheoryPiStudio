using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysManager : MonoBehaviour
{
    int value, count, currentLength;

    List<int> currentSequence = new List<int>();

    [SerializeField] int initialLength, repeatTimes;

    [SerializeField] float initialTimeBetweenColors;
    float currentTimeBetweenColors;

    [SerializeField] Image red, green, blue, yellow;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        currentLength = initialLength;
        currentTimeBetweenColors = initialTimeBetweenColors;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void RedClick()
    {
        Debug.Log("red");
        value = 0;
        count++;
    }

    public void YellowClick()
    {
        Debug.Log("yellow");
        value = 1;
        count++;
    }

    public void BlueClick()
    {
        Debug.Log("blue");
        value = 2;
        count++;
    }
    public void GreenClick()
    {
        Debug.Log("green");
        value = 3;
        count++;
    }

    void CreateSequence()
    {
        for (int i = 0; i < currentLength; i++)
        {
            value = Random.Range(0, 4);
            currentSequence.Add(value);
        }
        currentLength++;
    }

    void ResetSequence()
    {
        currentSequence.Clear();
    }

    IEnumerator ShowSequence()
    {
        yield return new WaitForSeconds(currentTimeBetweenColors);
    }

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
}
