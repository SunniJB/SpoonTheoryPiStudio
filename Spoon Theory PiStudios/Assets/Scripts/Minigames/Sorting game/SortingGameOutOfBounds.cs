using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameOutOfBounds : MonoBehaviour
{
    public GameObject returnPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SortingGame_Cutlery cutlery = collision.gameObject.GetComponent<SortingGame_Cutlery>();
        DragManager drag = collision.gameObject.GetComponent<DragManager>();

        if (cutlery != null)
        {
            Debug.Log(collision);
            collision.transform.position = cutlery.startPosition;
            drag.lastClickedPos.y = 0;
        }
    }
}
