using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingGameSides : MonoBehaviour
{
    string dropAudio = "Drop cutlery";

    [SerializeField] Transform iniPos;
    [SerializeField] float xSpacing = 11, sortingSpeed;
    Transform lastParent;

    public int sortedCutlery, maxCutlery;
    public bool full = false;

    private void Start()
    {
        lastParent = transform;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SortingGame_Cutlery cutlery = collision.gameObject.GetComponent<SortingGame_Cutlery>();

        if (cutlery != null)
        {
            if (cutlery.goalSide == gameObject)
            {
                AudioManager.GetInstance().Play(dropAudio, Random.Range(0.8f, 2.5f));
                cutlery.isSorted = true;
                sortedCutlery++;
                StartCoroutine(MoveToRightPlace(cutlery.GetComponent<RectTransform>()));
                lastParent = cutlery.transform;

                if (sortedCutlery == maxCutlery)
                {
                    full = true;
                }
            }
        }
    }

    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    SortingGame_Cutlery cutlery = collision.gameObject.GetComponent<SortingGame_Cutlery>();

    //    if (cutlery != null)
    //    {
    //        if (cutlery.goalSide == gameObject)
    //        {
    //            cutlery.isSorted = false;
    //        }
    //    }
    //}

    IEnumerator MoveToRightPlace(RectTransform obj)
    {
        obj.SetParent(lastParent);
        obj.GetComponent<Image>().raycastTarget = false;

        Vector3 targetPos = new Vector3(iniPos.position.x + xSpacing * sortedCutlery, iniPos.position.y, 0);

        while(Vector3.Distance(obj.position, targetPos) >= 0.1f)
        {
            obj.position = Vector3.Lerp(obj.position, targetPos, sortingSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
