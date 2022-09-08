using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameSides : MonoBehaviour
{
    public float sortedCuterly, maxCutlery;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SortingGame_Cutlery>() != null)
        {
            if (collision.gameObject.GetComponent<SortingGame_Cutlery>().goalSide == gameObject)
            {
                collision.gameObject.GetComponent<SortingGame_Cutlery>().isSorted = true;
                sortedCuterly += 1;

                if (sortedCuterly == maxCutlery)
                {
                    Debug.Log(gameObject + " is full!");
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SortingGame_Cutlery>() != null)
        {
            if (collision.gameObject.GetComponent<SortingGame_Cutlery>().goalSide == gameObject)
            {
                collision.gameObject.GetComponent<SortingGame_Cutlery>().isSorted = false;
                sortedCuterly -= 1;
            }
        }
    }
}
