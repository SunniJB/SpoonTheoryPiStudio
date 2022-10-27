using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    public int animationsPlayed;

    [SerializeField] Animator characterUIAnimator;
    CharacterMovement1stPerson player;
    private void OnTriggerEnter(Collider other)
    {
        NextAnimation();
        player = other.GetComponent<CharacterMovement1stPerson>();

        if (other == null) return;

        player.canMove = false;
        player.moving = false;

        gameObject.GetComponent<Collider>().enabled = false;
    }
    public void NextAnimation()
    {
        animationsPlayed += 1;
        characterUIAnimator.SetTrigger("nextAnimation");
        if (animationsPlayed > 3)
        {
            player.canMove = true;
        }
    }

}
