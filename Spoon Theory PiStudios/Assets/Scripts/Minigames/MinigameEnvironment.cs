using UnityEngine;

public class MinigameEnvironment : MonoBehaviour
{
    [SerializeField] string minigame;
    public PromptUI promptUI;

    public void GoToScene()
    {
        if (GameManager.GetInstance().spoons > 3)
        {
            GameManager.GetInstance().LoadScene(minigame, false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
            promptUI.SetUpText("I'm too tired to work more today");
    }
}
