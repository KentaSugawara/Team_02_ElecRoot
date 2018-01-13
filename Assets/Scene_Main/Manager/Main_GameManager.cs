using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public enum Layers
    {
        Character = 13,
        Wall = 19,
        HitMoltenIron = 25
    }

    public static class Main_GameManager
    {
        public static Camera MainCamera { get; private set; }
        public static Main_InputManager InputManager { get; private set; }
        public static Main_UIManager UIManager { get; private set; }

        public static Asset_MainSettings MainSettings { get; private set; }

        public static void Init(Asset_MainSettings MainSettings, Camera MainCamera, Main_InputManager InputManager, Main_UIManager UIManager)
        {
            Main_GameManager.MainSettings = MainSettings;

            Main_GameManager.MainCamera = MainCamera;
            Main_GameManager.InputManager = InputManager;
            Main_GameManager.UIManager = UIManager;
        }
    }
}
