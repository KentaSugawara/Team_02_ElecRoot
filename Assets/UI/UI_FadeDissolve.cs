using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeDissolve : MonoBehaviour {
    [SerializeField]
    private Image _Image;

    [SerializeField]
    private bool isFadeOuted;

    [SerializeField]
    private float _FadeIn_NeedTime;

    [SerializeField]
    private float _FadeOut_NeedTime;

    private bool isRunning = false;

    private void Start()
    {
        if (isFadeOuted)
        {
            _Image.material.SetFloat("Threshold", 2.0f);
            _Image.gameObject.SetActive(true);
        }
        else
        {
            _Image.material.SetFloat("Threshold", -1.0f);
            _Image.gameObject.SetActive(false);
        }
    }

    public void StartFade(System.Action callback)
    {
        if (!isRunning)
        {
            if (isFadeOuted)
            {
                //FadeIn
                
                StartCoroutine(Routine_FadeIn(callback));
            }
            else
            {
                //FadeOut
                _Image.gameObject.SetActive(true);
                StartCoroutine(Routine_FadeOut(callback));
            }
        }
    }

    private IEnumerator Routine_FadeIn(System.Action callback)
    {
        isRunning = true;

        for (float t = 0.0f; t < _FadeIn_NeedTime; t += Time.unscaledDeltaTime)
        {
            _Image.material.SetFloat("_Threshold", t / _FadeIn_NeedTime);
            yield return null;
        }
        _Image.material.SetFloat("_Threshold", 2.0f);

        isRunning = false;
        isFadeOuted = false;
        _Image.gameObject.SetActive(false);
        callback();
    }

    private IEnumerator Routine_FadeOut(System.Action callback)
    {
        isRunning = true;

        for (float t = 0.0f; t < _FadeOut_NeedTime; t += Time.unscaledDeltaTime)
        {
            _Image.material.SetFloat("_Threshold", 1.0f - t / _FadeOut_NeedTime);
            yield return null;
        }
        _Image.material.SetFloat("_Threshold", -1.0f);

        isRunning = false;
        isFadeOuted = false;
        callback();
    }
}
