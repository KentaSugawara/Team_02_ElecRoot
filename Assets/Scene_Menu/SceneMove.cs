using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour {
    [SerializeField]
    private string _SceneName;

    [SerializeField]
    private Menu_SceneManager _SceneManager;

    private bool _isRunning = false;

    public void MoveScene()
    {
        if (!_isRunning && !_SceneManager.isRunning)
            StartCoroutine(Routine_MoveScene());
    }

    private IEnumerator Routine_MoveScene()
    {
        _isRunning = true;
        _SceneManager.isRunning = true;
        {
            bool running = true;
            _SceneManager.FadeDissolve.StartFade(() => running = false);
            while (running) yield return null;
        }

        SceneManager.LoadScene(_SceneName);
    }
}
