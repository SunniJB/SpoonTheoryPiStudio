using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opencloseDoor : MonoBehaviour
{

	public Animator animator;
	public bool open;
	public LevelManager lvlManager;

	public enum SceneToGo { TutorialScene, ApartmentScene, RestaurantScene}
	public SceneToGo sceneToGo;

	void Start()
	{
		open = false;
	}

	public void OpenCloseDoor()
    {
		if (open == false) StartCoroutine(opening());
		else StartCoroutine(closing());
	}

	IEnumerator opening()
	{
		animator.Play("Opening");
		AudioManager.GetInstance().Play("Door", 1);
		open = true;
		yield return new WaitForSeconds(.5f);
<<<<<<< Updated upstream
        if (GameManager.GetInstance().tutorialFinished) GoToWork();
        else
        {
            if (TutorialManager.GetInstance() != null) TutorialManager.GetInstance().finishFinished = true;
        }
        //if (SceneManager.GetActiveScene().name != "Restaurant Scene")
        //{

        //      } else
        //{
        //	GameManager.GetInstance().ApartmentScene();
        //      }

    }
=======
		//if (GameManager.GetInstance().tutorialFinished) GoToWork();
		//else
		//{
		//	if (TutorialManager.GetInstance() != null) TutorialManager.GetInstance().finishFinished = true;
		//}
		GameManager.GetInstance().LoadScene(sceneToGo.ToString());
	}
>>>>>>> Stashed changes

	IEnumerator closing()
	{
		print("you are closing the door");
		animator.Play("Closing");
		open = false;
		yield return new WaitForSeconds(.5f);
	}

	void GoToWork()
    {
		lvlManager.GoToWork();
    }

}
