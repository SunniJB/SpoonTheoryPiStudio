using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image avatarImg, pausePanel;
    [SerializeField] GameObject characterUIPanel;

    [SerializeField] bool pause;

    [SerializeField] F_CharacterInteractor characterInteractor;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.gameObject.SetActive(false);
        pause = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            pausePanel.gameObject.SetActive(pause);
            characterUIPanel    .SetActive(!pause);

            if (pause)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }

        if (!pause && Input.GetMouseButton(0) && Cursor.lockState != CursorLockMode.Locked && !characterInteractor.taskCanvasEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void GoToMenu()
    {
        GameManager.Instance.MenuScene();
    }
}
