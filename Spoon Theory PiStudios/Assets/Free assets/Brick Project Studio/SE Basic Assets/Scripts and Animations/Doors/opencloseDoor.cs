using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opencloseDoor : MonoBehaviour
{

	public Animator openandclose;
	public bool open;

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
		print("you are opening the door");
		openandclose.Play("Opening");
		open = true;
		yield return new WaitForSeconds(.5f);
		GameManager.Instance.WorkScene();
	}

	IEnumerator closing()
	{
		print("you are closing the door");
		openandclose.Play("Closing");
		open = false;
		yield return new WaitForSeconds(.5f);
	}


}
