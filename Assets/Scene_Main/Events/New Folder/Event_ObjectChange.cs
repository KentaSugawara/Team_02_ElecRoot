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

        [SerializeField]
        private GameObject _Lamp_OFF;

        [SerializeField]
        private GameObject _Lamp_ON;

        [SerializeField]
        private GameObject _Lamp_Light;

        [SerializeField]
        private Transform _MaskTransform;
        [SerializeField]
        private Vector3 _LightStartLocalScale;
        [SerializeField]
        private Vector3 _LightEndLocalScale;
        [SerializeField]
        private float _LightSpan = 1.0f;

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
                    if (_Lamp_ON != null)
                    {
                        yield return Routine_Lamp();
                        StartCoroutine(Routine_Light());
                    }
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
        
        [SerializeField]
        private float _Lamp_LightSize;

        [SerializeField]
        private float _Lamp_LightNeedTime;

        private IEnumerator Routine_Lamp()
        {
            _Lamp_Light.transform.localScale = _LightEndLocalScale;
            yield return new WaitForSecondsRealtime(1.0f);
            _Lamp_OFF.SetActive(false);
            _Lamp_ON.SetActive(true);
            _Lamp_Light.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
            _Lamp_OFF.SetActive(true);
            _Lamp_ON.SetActive(false);
            _Lamp_Light.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
            _Lamp_OFF.SetActive(false);
            _Lamp_ON.SetActive(true);
            _Lamp_Light.SetActive(true);
            //{
            //    Vector3 s = Vector3.zero;
            //    Vector3 e = new Vector3(_Lamp_LightSize, _Lamp_LightSize, 1.0f);
            //    for (float t = 0.0f; t < _Lamp_LightNeedTime; t += Time.unscaledDeltaTime)
            //    {
            //        _Lamp_Light.transform.localScale= Vector3.Lerp(s, e, t / _Lamp_LightNeedTime);
            //        yield return null;
            //    }
            //    _Lamp_Light.transform.localScale = e;
            //}
        }

        private IEnumerator Routine_Light()
        {
            var LightTexture = _Lamp_Light.GetComponent<SpriteRenderer>();
            Color s = LightTexture.material.color;
            Color e = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            while (true)
            {
                for (float t = 0; t < _LightSpan; t += Time.deltaTime)
                {
                    _Lamp_Light.transform.localScale = Vector3.Lerp(_LightStartLocalScale, _LightEndLocalScale, t / _LightSpan);
                    LightTexture.material.color = Color.Lerp(s, e, t / _LightSpan);
                    yield return null;
                }
                _Lamp_Light.transform.localScale = _LightEndLocalScale;
                LightTexture.material.color = e;

                for (float t = 0; t < _LightSpan; t += Time.deltaTime)
                {
                    _Lamp_Light.transform.localScale = Vector3.Lerp(_LightEndLocalScale, _LightStartLocalScale, t / _LightSpan);
                    LightTexture.material.color = Color.Lerp(e, s, t / _LightSpan);
                    yield return null;
                }
                _Lamp_Light.transform.localScale = _LightStartLocalScale;
                LightTexture.material.color = s;

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
