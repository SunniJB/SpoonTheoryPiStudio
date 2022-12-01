using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishMinigame : MonoBehaviour
{
    [SerializeField] Animator spoonsAnim, moneyAnim;
    [SerializeField] Slider spoonSlider;
    [SerializeField] TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        spoonSlider.maxValue = 25; //example value, idk what to put here
        spoonSlider.value = GameManager.GetInstance().spoons;

        GetComponent<Image>().enabled = false;
        spoonSlider.gameObject.SetActive(false);
        moneyAnim.gameObject.SetActive(false);
    }
    public void StartAnimationVoid(float spoonsLost, float moneyWon, MinigameManager minigameManager)
    {
        StartCoroutine(StartAnimation(spoonsLost, moneyWon, minigameManager));
    }

    IEnumerator StartAnimation(float spoonsLost, float moneyWon, MinigameManager minigameManager)
    {
        GetComponent<Image>().enabled = true;
        spoonSlider.gameObject.SetActive(true);
        moneyAnim.gameObject.SetActive(true);

        spoonsAnim.SetTrigger("Play");

        yield return new WaitForSeconds(spoonsAnim.runtimeAnimatorController.animationClips[0].length);

        for (int i = 0; i < spoonsLost; i++)
        {
            spoonSlider.value--;
            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(0.5f);

        moneyText.text = moneyWon.ToString();
        moneyAnim.SetTrigger("Play");

        yield return new WaitForSeconds(moneyAnim.runtimeAnimatorController.animationClips[0].length);

        yield return new WaitForSeconds(.5f);

        spoonsAnim.SetTrigger("Corner");
        moneyAnim.SetTrigger("Corner");

        GetComponent<Image>().enabled = false;
        minigameManager.Finish();
    }
}
