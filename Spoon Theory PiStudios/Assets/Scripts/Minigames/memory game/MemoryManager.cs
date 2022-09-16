using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager instance;

    public Sprite[] flippedLibrary = new Sprite[5];
    public GameObject[] flipPlates = new GameObject[10];
    private GameObject tempFp;
    public GameObject firstClickObj, repplayButton;
    private int[] numbers = new int[10];
    public Sprite clickOne = null, clickTwo = null;
    public int wins;
    public Text textObj;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            numbers[i - 1] = Mathf.CeilToInt((float)i / 2);
        }

        Shuffle();

        for (int i = 0; i < 10; i++)
        {
            flipPlates[i].GetComponent<ClickMemory>().images[1] = flippedLibrary[numbers[i]-1];
        }
    }

    private void Update()
    {
        if (wins == 5)
        {
            //What happens when you win goes here!!
            textObj.text = "You Won!!!!";
            repplayButton.SetActive(true);
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < flipPlates.Length; i++)
        {
            int rando = Random.Range(i, flipPlates.Length);
            tempFp = flipPlates[rando];
            flipPlates[rando] = flipPlates[i];
            flipPlates[i] = tempFp;
        }
    }
}
