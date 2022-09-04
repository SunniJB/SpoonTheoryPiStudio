using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Click_Spoons : MonoBehaviour
{
    public int spoonsNumber;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = spoonsNumber.ToString();
    }
}
