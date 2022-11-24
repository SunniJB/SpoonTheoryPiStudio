using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opencloseDoor : MonoBehaviour
{

	public Animator animator;
	public bool open;
	public LevelManager lvlManager;

	public enum SceneToGo { TutorialScene, ChloesApartment, RestaurantScene, IsaacsApartment, IsaacWorkScene}
	public SceneToGo sceneToGo;

	void Start()
	{
		open = false;
	}

	public void OpenCloseDoor(CharacterInteractor interactor)
    {
		if (open == false) StartCoroutine(Opening(interactor));
		else StartCoroutine(Closing());
	}

	IEnumerator Opening(CharacterInteractor interactor)
	{
		animator.Play("Opening");
		AudioManager.GetInstance().Play("Door", 1);
		open = true;
		yield return new WaitForSeconds(.5f);
		if (GameManager.GetInstance().tutorialFinished)
		{
			if (GameManager.GetInstance().ActualScene() == "RestaurantScene")
			{
				GameManager.GetInstance().SetTimeAfternoon();
				interactor.RefreshStatsFromManager();
			}
			else
            {
				interactor.UpdateGameManagerStats();
            }
			
			GameManager.GetInstance().LoadScene(sceneToGo.ToString());
		}
		else
		{
			if (TutorialManager.GetInstance() != null) TutorialManager.GetInstance().finishFinished = true;
		}
	}

	IEnumerator Closing()
	{
		print("you are closing the door");
		animator.Play("Closing");
		open = false;
		yield return new WaitForSeconds(.5f);
	}
}
