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
    }

    public IEnumerator StartAnimation(float spoonsLost, float moneyWon)
    {
        spoonsAnim.SetTrigger("Play");

        yield return new WaitForSeconds(spoonsAnim.GetCurrentAnimatorStateInfo(0).length);

        for (int i = 0; i < spoonsLost; i++)
        {
            spoonSlider.value--;
            yield return new WaitForSeconds(.3f);
        }

        moneyText.text = moneyWon.ToString();
        moneyAnim.SetTrigger("Play");

        yield return new WaitForSeconds(moneyAnim.GetCurrentAnimatorStateInfo(0).length);

        //Show win panel (either call a function where it is or have a reference here)
    }
}
