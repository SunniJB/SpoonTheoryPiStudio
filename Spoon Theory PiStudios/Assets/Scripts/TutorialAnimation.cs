using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAnimation : MonoBehaviour
{
    int animationsPlayed;

    [SerializeField] Animator characterUIAnimator;
    [SerializeField] Button nextButton;
    CharacterMovement1stPerson player;
    private void Start()
    {
        nextButton.enabled = false;
        animationsPlayed = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        nextButton.enabled = true;
        NextAnimation();
        player = other.GetComponent<CharacterMovement1stPerson>();

        if (other == null) return;

        player.canMove = false;
        player.moving = false;

        GetComponent<Collider>().enabled = false;
    }
    public void NextAnimation()
    {
        animationsPlayed++;

        if (animationsPlayed <= 5)
        {
            characterUIAnimator.SetTrigger("nextAnimation");
        }
        else
        {
            player.canMove = true;
        }
    }

}
