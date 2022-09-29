using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameSides : MonoBehaviour
{
    public float sortedCuterly, maxCutlery;
    public bool full = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SortingGame_Cutlery cutlery = collision.gameObject.GetComponent<SortingGame_Cutlery>();

        if (cutlery != null)
        {
            if (cutlery.goalSide == gameObject)
            {
                cutlery.isSorted = true;
                sortedCuterly += 1;

                if (sortedCuterly == maxCutlery)
                {
                    full = true;
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
