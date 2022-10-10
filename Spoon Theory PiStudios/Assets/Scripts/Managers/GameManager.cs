using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance() { return instance; }

    public bool tutorialFinished;

    public Light dayLight;

    public enum DayTime {Morning, Noon, Afternoon, Evening, Night};
    public DayTime dayTime;
    public int dayCount = 1;
    public float moneyGoal;

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
        SetTimeMorning();
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
        else SceneManager.LoadScene(2); // Go to apartment
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadScene(int sceneNumber = -1)
    {
        if(sceneNumber != -1) SceneManager.LoadScene(sceneNumber);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void WorkScene()
    {
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
    }

    public void MemoryGameScene()
    {
        SceneManager.LoadScene(4);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SimonGameScene()
    {
        SceneManager.LoadScene(5);
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

    public void SetTimeMorning()
    {
        dayTime = DayTime.Morning;
        //dayLight.color = new Color(1f, 0.9568627f, 0.8392157f);
    }

    public void SetTimeAfternoon()
    {
        dayTime = DayTime.Evening;
        //dayLight.color = new Color(0.511f, 0.1527415f, 0.135947f);
    }
}
