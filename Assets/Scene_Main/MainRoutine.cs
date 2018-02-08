using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class MainRoutine : MonoBehaviour
    {
        [SerializeField]
        Asset_MainSettings _MainSettings;

        [SerializeField]
        private Main_PlayerCharacter _PlayerCharacter;

        [SerializeField]
        private int _StartHP;

        [SerializeField]
        private int _StartNumOfBar;

        [SerializeField]
        private Camera _MainCamera;

        [SerializeField]
        private Main_InputManager _InputManager;

        [SerializeField]
        private Main_UIManager _UIManager;

        [SerializeField]
        private Main_SceneManager _SceneManager;

        [SerializeField]
        private int _NumObBrokenCircuit;

        [SerializeField]
        private List<Main_UI_EventCameraTarget> _Events = new List<Main_UI_EventCameraTarget>();

        private void Awake()
        {
            Main_GameManager.Init(_MainSettings, _MainCamera, _InputManager, _UIManager, _SceneManager);

            Main_GameManager.InitGame(_NumObBrokenCircuit);

            _PlayerCharacter.Init(_StartHP, _StartNumOfBar);
        }

        private void Start()
        {
            StartCoroutine(Routine_Start());
        }

        private IEnumerator Routine_Start()
        {
            Time.timeScale = 0.0f;
            //FadeIn
            if(_UIManager.Fade_Dissolve != null) {
                bool running = true;
                _UIManager.Fade_Dissolve.StartFade(() => running = false);
                while (running) yield return null;
            }
            Time.timeScale = 1.0f;

            foreach (var e in _Events)
            {
                e.StartEvent();
            }
        }
    }
}