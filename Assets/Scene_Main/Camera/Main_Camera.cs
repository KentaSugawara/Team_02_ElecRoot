﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_Camera : MonoBehaviour
    {
        [SerializeField]
        private Transform _Target;

        [SerializeField]
        private float _Distance;

        [SerializeField]
        private float _Speed;

        private bool isEventCamera = false;
        private float _StartOrthoSize;
        private Camera _MyCamera;

        private void Start()
        {
            transform.position = _Target.position + Vector3.up * _Distance;
            _MyCamera = GetComponent<Camera>();
            _StartOrthoSize = _MyCamera.orthographicSize;
            StartCoroutine(Routine_Main());
        }

        public void StartEventCamera(Main_UI_EventCameraTarget EventTarget)
        {
            transform.position = EventTarget.getPosition() + Vector3.up * _Distance;
            _MyCamera.orthographicSize = EventTarget.OrthoSize;
            isEventCamera = true;
        }

        public void EndEventCamera()
        {
            transform.position = _Target.position + Vector3.up * _Distance;
            _MyCamera.orthographicSize = _StartOrthoSize;
            isEventCamera = false;
        }

        private IEnumerator Routine_Main()
        {
            while (true)
            {
                if (!isEventCamera)
                {
                    transform.position = Vector3.Lerp(transform.position, _Target.position + Vector3.up * _Distance, _Speed * Time.deltaTime);
                    transform.LookAt(transform.position + Vector3.down);
                }

                yield return null;
            }
        }
    }
}
