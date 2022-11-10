using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEnvironment : MonoBehaviour
{
    [SerializeField] string minigame;

    public void GoToScene()
    {
        SceneManager.LoadScene(minigame);
        Cursor.lockState = CursorLockMode.None;
    }
}
