using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum DayTime {Morning, Noon, Afternoon, Evening, Night};
    public DayTime dayTime;

    [Header("Player stats")]
    public float money;
    public float spoons;
    public float hygiene;
    public float happiness;
    public float workPerformance;
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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        dayTime = DayTime.Morning;
        //if(SceneManager.GetActiveScene().name != "Menu") Cursor.lockState = CursorLockMode.Locked;
    }
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ApartmentScene()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void WorkScene()
    {
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.None;
    }

    public void MemoryGameScene()
    {
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
    }
}
