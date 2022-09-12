using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGame_Cutlery : MonoBehaviour
{
    public bool isSorted;
    public GameObject goalSide;
    public Vector2 startPosition;

    private void Start()
    {
        startPosition = gameObject.GetComponent<Transform>().position;
    }

    private void Update()
    {
        if (isSorted)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
    }
    public void Restart()
    {
        isSorted = false;
        gameObject.transform.position = startPosition;
    }
}
