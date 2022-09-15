using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum DayTime {Morning, Noon, Afternoon, Evening, Night};
    public DayTime dayTime;

    public float money, spoons;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself. 
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        dayTime = DayTime.Morning;
        if(SceneManager.GetActiveScene().name != "Menu")
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayScene()
    {
        Debug.Log("Change Scene");
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
