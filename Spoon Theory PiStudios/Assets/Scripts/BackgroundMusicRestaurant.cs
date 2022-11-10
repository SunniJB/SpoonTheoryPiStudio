using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicRestaurant : PlayBackgroundMusic
{
    protected override void Start()
    {
        if (!GameManager.GetInstance().workedAlready) base.Start();
        else GameManager.GetInstance().workedAlready = true;
    }
}
