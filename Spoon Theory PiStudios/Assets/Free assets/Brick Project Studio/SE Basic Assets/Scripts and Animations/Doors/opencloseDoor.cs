using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opencloseDoor : MonoBehaviour
{

	public Animator openandclose;
	public bool open;
	
	public enum SceneToGo { MemoryGame, SortingGame, Menu}
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
		openandclose.Play("Opening");
		AudioManager.GetInstance().Play("Door", 1);
		open = true;
		yield return new WaitForSeconds(.5f);
		GoToScene();
	}

	IEnumerator closing()
	{
		print("you are closing the door");
		openandclose.Play("Closing");
		open = false;
		yield return new WaitForSeconds(.5f);
	}

	void GoToScene()
    {
		switch(sceneToGo)
        {
			case SceneToGo.MemoryGame:
				GameManager.Instance.MemoryGameScene();
				break;

			case SceneToGo.SortingGame:
				GameManager.Instance.WorkScene();
				break;

			case SceneToGo.Menu:
				GameManager.Instance.MenuScene();
				break;
        }
    }

}
