using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    public int animationsPlayed;
    Collider playerCollider;
    private void OnTriggerEnter(Collider other)
    {
        NextAnimation();
        playerCollider = other;
        other.gameObject.GetComponent<CharacterMovement1stPerson>().canMove = false;
        other.gameObject.GetComponent<CharacterMovement1stPerson>().moving = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
    public void NextAnimation()
    {
        animationsPlayed += 1;
        GameObject.Find("Character UI").GetComponent<Animator>().SetTrigger("nextAnimation");
        if (animationsPlayed > 3)
        {
            playerCollider.gameObject.GetComponent<CharacterMovement1stPerson>().canMove = true;
        }
    }

}
