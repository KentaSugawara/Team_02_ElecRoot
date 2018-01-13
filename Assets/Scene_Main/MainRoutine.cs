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

        private void Awake()
        {
            Main_GameManager.Init(_MainSettings, _MainCamera, _InputManager, _UIManager);

            _PlayerCharacter.Init(_StartHP, _StartNumOfBar);
        }
    }
}