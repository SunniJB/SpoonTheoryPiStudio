using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    public string audioName;

    void Start()
    {
        AudioManager.GetInstance().Play(audioName, 1f);
    }

}
