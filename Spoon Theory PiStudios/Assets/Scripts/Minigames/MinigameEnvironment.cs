using UnityEngine;

public class MinigameEnvironment : MonoBehaviour
{
    [SerializeField] string minigame;

    public void GoToScene()
    {
        GameManager.GetInstance().LoadScene(minigame, false);
        Cursor.lockState = CursorLockMode.None;
    }
}
