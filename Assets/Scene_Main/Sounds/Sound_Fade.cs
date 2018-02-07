using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Fade : MonoBehaviour {
    [SerializeField]
    private AudioSource _Source;

    [SerializeField]
    private float _FadeTime;

    [SerializeField]
    private bool _PlayOnAwake;

    private void Start()
    {
        if (_PlayOnAwake)
        {
            FadeIn();
        }
    }

    private bool isPlaying = false;
    private bool isFading = false;

    public void FadeIn()
    {
        //StartCoroutine()
    }

    private IEnumerator Routine_FadeIn()
    {
        isFading = true;
        isPlaying = true;
        for (float t = 0.0f; t < _FadeTime; t += Time.unscaledDeltaTime)
        {

            yield return null;
        }
        isFading = false;
    }

    public void FadeOut()
    {
        ;
    }

    private IEnumerator Routine_FadeOut()
    {
        isFading = true;
        for (float t = 0.0f; t < _FadeTime; t += Time.unscaledDeltaTime)
        {

            yield return null;
        }
        isPlaying = false;
        isFading = false;
    }

}
