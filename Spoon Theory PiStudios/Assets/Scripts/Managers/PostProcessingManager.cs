using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    private static PostProcessingManager instance;
    public static PostProcessingManager GetInstance() { return instance; }

    Volume volume;

    Vignette vignette;
    ChromaticAberration ca;

    private void Awake()
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
    }

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();

        Vignette _vignette;
        if (volume.profile.TryGet<Vignette>(out _vignette))
        {
            vignette = _vignette;
        }

        ChromaticAberration _ca;
        if (volume.profile.TryGet<ChromaticAberration>(out _ca))
        {
            ca = _ca;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VignetteValues(float intensity, Color color)
    {
        vignette.color.value = color;
        StartCoroutine(ChangeVignette(intensity));
    }

    IEnumerator ChangeVignette(float intensity)
    {
        bool add = intensity > vignette.intensity.value ? true : false;

        while(Mathf.Abs(intensity - vignette.intensity.value) >= 0.01f)
        {
            float x;
            if (add)
            {
                x = vignette.intensity.value + Time.deltaTime;
            }
            else
            {
                x = vignette.intensity.value - Time.deltaTime;
            }
            vignette.intensity.value = x;

            yield return null;
        }
    }

    public void ChromaticAberrationValues(float intensity)
    {
        StartCoroutine(ChangeChromaticAberration(intensity));
    }

    IEnumerator ChangeChromaticAberration(float intensity)
    {
        bool add = intensity > ca.intensity.value ? true : false;

        while (Mathf.Abs(intensity - ca.intensity.value) >= 0.01f)
        {
            float x;
            if (add)
            {
                x = ca.intensity.value + Time.deltaTime;
            }
            else
            {
                x = ca.intensity.value - Time.deltaTime;
            }
            ca.intensity.value = x;

            yield return null;
        }
    }
}
