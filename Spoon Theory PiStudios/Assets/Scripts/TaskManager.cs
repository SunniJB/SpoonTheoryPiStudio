using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    Task[] morningTasks, eveningTasks;

    Toggle[] checkbox;

    [SerializeField] int maxTasksPinned = 5, currentNumberofTasksPinned = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
