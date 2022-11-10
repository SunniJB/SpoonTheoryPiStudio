using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    public string[] audioName;

    protected virtual void Start()
    {
        for (int i = 0; i < audioName.Length; i++)
        {
            AudioManager.GetInstance().Play(audioName[i], 1f);
        }
    }

}
