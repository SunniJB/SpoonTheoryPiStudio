using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText, spoonsText;
    [SerializeField] private Image textPanel;

    bool isDisplayed;
    // Start is called before the first frame update
    void Start()
    {
        textPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }
    }

    public void SetUpText(string text)
    {
        promptText.text = text;
        textPanel.gameObject.SetActive(true);
        isDisplayed = true;
        Invoke("Close", 3);
    }

    public void Close()
    {
        textPanel.gameObject.SetActive(false);
        isDisplayed = false;
    }

    public void DisplaySpoons(int spoonNumber)
    {
        spoonsText.text = spoonNumber.ToString();
    }
}
