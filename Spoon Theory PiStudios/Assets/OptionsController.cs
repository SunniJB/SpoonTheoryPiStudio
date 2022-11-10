using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider sfx, music;

    private void Start()
    {
        sfx.value = AudioManager.GetInstance().startVolume;
        music.value = AudioManager.GetInstance().startVolume;
    }
    public void SFXSlider(float value)
    {
        AudioManager.GetInstance().AudioVolume(value, false);
    }
    public void MusicSlider(float value)
    {
        AudioManager.GetInstance().AudioVolume(value, true);
    }
}
