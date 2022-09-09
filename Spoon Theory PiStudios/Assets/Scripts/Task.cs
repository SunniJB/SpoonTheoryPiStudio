using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] string taskName = "Task name", description = "Task description";
    bool pinned, completed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight()
    {
        Debug.Log("Task highlighted: "+transform.name);
    }

    public void Unhiglight()
    {
        Debug.Log("Task unhighlighted: " + transform.name);
    }
}
