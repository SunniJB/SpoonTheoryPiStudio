using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Mirror : MonoBehaviour, Click_IInteractable
{
    public bool Interacted => false;
    public GameObject promptPanel;
    public float time;


    private void Start()
    {
        promptPanel = GameObject.Find("PromptPanel");
        promptPanel.SetActive(false);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 5f)
        {
            promptPanel.SetActive(false);
        }
    }

    public bool Interact(Click_CharacterInteractor interactor)
    {
        time = 0f;
        promptPanel.SetActive(true);
        GameObject.Find("SpoonsNumber").GetComponent<Click_Spoons>().spoonsNumber -= 2;
        Debug.Log("Its a mirror");
        return true;
    }
}
