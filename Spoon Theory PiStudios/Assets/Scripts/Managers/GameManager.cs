using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager GetInstance() { return instance; }

    public bool tutorialFinished;

    public enum DayTime {Morning, Noon, Afternoon, Evening, Night};
    public DayTime dayTime;

    [Header("Player stats")]
    public float money;
    public float hunger;
    public int spoons;
    public float hygiene;
    public float happiness;
    public float workPerformance;
    private void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //tutorialFinished = false;
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
        if (!tutorialFinished) SceneManager.LoadScene(1); //Go to tutorial
        else SceneManager.LoadScene(1); // Go to apartment
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

    public void UpdateGameManagerStats(float _money, float _hunger, int _spoons, float _hygiene, float _happines, float _workPerformance)
    {
        money = _money;
        hunger = _hunger;
        spoons = _spoons;
        hygiene = _hygiene;
        happiness = _happines;
        workPerformance = _workPerformance;
    }
}
