using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTaskScale : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
