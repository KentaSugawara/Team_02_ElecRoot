using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Event_ObjectChange : Main_UI_EventCameraTarget
    {
        [SerializeField]
        private List<GameObject> _ExistObjects = new List<GameObject>();

        [SerializeField]
        private List<GameObject> _ChangeObjects = new List<GameObject>();

        [SerializeField]
        private bool _CameraMove = true;

        public override void StartEvent(Action callback = null)
        {
            base.StartEvent(callback);
            StartCoroutine(Routine_Event());
        }

        private IEnumerator Routine_Event()
        {
            if (_CameraMove)
            {
                Time.timeScale = 0.0f;

                var UIManager = Main_GameManager.UIManager;
                var CameraComponent = Main_GameManager.MainCamera.GetComponent<Main_Camera>();

                //FadeOut
                if (!UIManager.Fade_Normal.isFadeOuted)
                {
                    bool running = true;
                    UIManager.Fade_Normal.StartFade(() => running = false);
                    while (running) yield return null;
                }

                //カメラ移動
                {
                    CameraComponent.StartEventCamera(this);
                }

                //FadeIn
                {
                    bool running = true;
                    UIManager.Fade_Normal.StartFade(() => running = false);
                    while (running) yield return null;
                }

                //Change
                {
                    yield return new WaitForSecondsRealtime(1.0f);
                    ChangeObject();
                    yield return new WaitForSecondsRealtime(1.0f);
                }

                //FadeOut
                if (!UIManager.Fade_Normal.isFadeOuted)
                {
                    bool running = true;
                    UIManager.Fade_Normal.StartFade(() => running = false);
                    while (running) yield return null;
                }

                //カメラ戻す
                {
                    CameraComponent.EndEventCamera();
                }

                //FadeIn
                {
                    bool running = true;
                    UIManager.Fade_Normal.StartFade(() => running = false);
                    while (running) yield return null;
                }

                Time.timeScale = 1.0f;
            }
            else
            {
                ChangeObject();
            }

        }

        private void ChangeObject()
        {
            foreach(var obj in _ExistObjects)
            {
                if (obj) obj.SetActive(false);
            }

            foreach (var obj in _ChangeObjects)
            {
                if (obj) obj.SetActive(true);
            }
        }
    }
}
