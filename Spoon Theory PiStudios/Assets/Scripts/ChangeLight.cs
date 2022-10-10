using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    public GameManager GameManager;
    private void Start()
    {
        GameManager = GameManager.GetInstance();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayTime == GameManager.DayTime.Morning)
        {
            gameObject.GetComponent<Light>().color = new Color(0.971011f, 0.9716981f, 0.7104397f);
        } else
        {
            gameObject.GetComponent<Light>().color = new Color(0.511f, 0.1527415f, 0.135947f);
        }
    }
}
