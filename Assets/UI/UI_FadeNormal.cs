using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeNormal : MonoBehaviour {
    [SerializeField]
    private Image _Image;

    [SerializeField]
    private bool _isFadeOuted;
    public bool isFadeOuted
    {
        get { return _isFadeOuted; }
    }

    [SerializeField]
    private float _FadeIn_NeedTime;

    [SerializeField]
    private float _FadeOut_NeedTime;

    private bool isRunning = false;

    private void Start()
    {
        if (_isFadeOuted)
            _Image.gameObject.SetActive(true);
        else
            _Image.gameObject.SetActive(false);
    }

    public void StartFade(System.Action callback)
    {
        if (!isRunning)
        {
            if (_isFadeOuted)
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

        Color s = _Image.color;
        s.a = 1.0f;
        Color e = _Image.color;
        e.a = 0.0f;

        for (float t = 0.0f; t < _FadeIn_NeedTime; t += Time.unscaledDeltaTime)
        {
            _Image.color = Color.Lerp(s, e, t / _FadeIn_NeedTime);
            yield return null;
        }
        _Image.color = e;

        isRunning = false;
        _isFadeOuted = false;
        _Image.gameObject.SetActive(false);
        callback();
    }

    private IEnumerator Routine_FadeOut(System.Action callback)
    {
        isRunning = true;

        Color s = _Image.color;
        s.a = 0.0f;
        Color e = _Image.color;
        e.a = 1.0f;

        for (float t = 0.0f; t < _FadeOut_NeedTime; t += Time.unscaledDeltaTime)
        {
            _Image.color = Color.Lerp(s, e, t / _FadeOut_NeedTime);
            yield return null;
        }
        _Image.color = e;

        isRunning = false;
        _isFadeOuted = true;
        callback();
    }
}
