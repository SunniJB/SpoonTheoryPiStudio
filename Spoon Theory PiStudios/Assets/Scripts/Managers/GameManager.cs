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
    public int dayCount;
    public float moneyGoal;
    public int totalDaysBeforeLoss = 60;

    public bool isIsaac;
    public bool workedAlready;

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

        PlayerPrefs.DeleteAll();
        workedAlready = false;

        SetTimeMorning();
    }

    private void Start()
    {
        Debug.Log("Start function of game manager happened");
        SetTimeMorning();

        if (GameObject.Find("Directional light"))
        {
            dayLight = GameObject.Find("Directional light").GetComponent<Light>();
        }
    }
    public void LoadScene(int sceneNumber = -1, bool stopMusic = true)
    {
        if (stopMusic) AudioManager.GetInstance().StopAllSounds();

        if (sceneNumber != -1) SceneManager.LoadScene(sceneNumber);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName, bool stopMusic = true)
    {
        if(stopMusic) AudioManager.GetInstance().StopAllSounds();
        SceneManager.LoadScene(sceneName);
    }

    public void ResetScene()
    {
        AudioManager.GetInstance().StopAllSounds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public string ActualScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void MenuScene()
    {
        LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;
    }

    public void ApartmentScene()
    {
        if (!tutorialFinished)
        {
            LoadScene(1); //Go to tutorial
        }
        else 
        {
            //Go to its correspondant apartment if your isaac or chloe
            if (!isIsaac) LoadScene("ChloesApartment");
            else LoadScene("IsaacsApartment");

            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void WorkScene(bool stopMusic = true)
    {
        if (!isIsaac) LoadScene("RestaurantScene", stopMusic);
        else LoadScene("IsaacWorkScene", stopMusic);

        Cursor.lockState = CursorLockMode.None;
    }

    public void MemoryGameScene()
    {
        LoadScene(4);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SimonGameScene()
    {
        LoadScene(5);
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

    public void SetTutorialFinished()
    {
        tutorialFinished = !tutorialFinished;
    }
}
