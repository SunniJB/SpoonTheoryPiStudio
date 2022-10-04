using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public static AudioManager GetInstance() { return instance; }

    void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if (sounds.Length != 0)
        {
            foreach (Sound s in sounds)
            {
                //s.source.clip = s.clip;

                //s.source.volume = s.volume;
                //s.source.pitch = s.pitch;
                //s.source.loop = s.music;
            }
        }


        // Initial values for the music and sound sliders
        AudioVolume(PlayerPrefs.GetFloat("SoundVolume", 5), false);
        AudioVolume(PlayerPrefs.GetFloat("MusicVolume", 5), true);

        //ChangeBackgroundMusic(SceneManager.GetActiveScene().name);
    }

    public void ChangeBackgroundMusic(string sceneName) // Changes the backgorund music depending on the scene 
    {
        switch (sceneName)
        {
            case "MainMenu_Scene":
                Stop("GameMusic");
                Play("MainMenu_Music", 1);
                break;
            default:
                Stop("MainMenu_Music");
                Play("GameMusic", 1);
                break;
        }
    }

    public void Play(string name, float pitch, AudioSource source = null)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //gets object's audio source if it has one, else creates one
        if(source != null) s.source = source;
        else s.source = gameObject.AddComponent<AudioSource>();

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.music;

        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    public void Resume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public bool CheckPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        return s.source.isPlaying;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void AudioVolume(float volume, bool isMusic) // It is called everytime sound or music is modified
    {
        // Store values in PlayerPref
        if (isMusic)  // Volumen Musica
            PlayerPrefs.SetFloat("MusicVolume", volume);

        if (!isMusic) // Sound volume
            PlayerPrefs.SetFloat("SoundVolume", volume);

        // Convert value to 0-1
        volume = volume / 10;

        AudioSource[] AllaudioSources = GetComponents<AudioSource>();

        for (int i = 0; i < AllaudioSources.Length; i++)
        {
            if (sounds[i].music == isMusic)
                AllaudioSources[i].volume = volume * sounds[i].maxVolume; // Change volume to all music/sound depending on "maxVolume"
        }
    }
}
