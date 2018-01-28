using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SceneManager : MonoBehaviour {
    [SerializeField]
    private UI_FadeNormal _FadeNormal;
    public UI_FadeNormal FadeNormal
    {
        get { return _FadeNormal; }
    }

    [SerializeField]
    private UI_FadeDissolve _FadeDissolve;
    public UI_FadeDissolve FadeDissolve
    {
        get { return _FadeDissolve; }
    }

    [SerializeField]
    private bool _isRunning;
    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
}
