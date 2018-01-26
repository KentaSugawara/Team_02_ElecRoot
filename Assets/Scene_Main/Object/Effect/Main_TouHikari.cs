using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_TouHikari : MonoBehaviour
    {
        [SerializeField]
        private Animation _Animation;

        [SerializeField]
        private SpriteRenderer _Renderer;

        [SerializeField]
        private Main_UI_EventCameraTarget _CameraTarget;
        public Main_UI_EventCameraTarget CameraTarget
        {
            get { return _CameraTarget; }
        }

        private void Start()
        {
            _Renderer.enabled = false;
        }

        public void StartClearFlash(System.Action EndCallBack)
        {
            _Renderer.enabled = true;
            _Animation.Play();
            StartCoroutine(Routine_ClearFlash(EndCallBack));
        }

        private IEnumerator Routine_ClearFlash(System.Action EndCallBack)
        {
            while (_Animation.isPlaying) yield return null;
            EndCallBack();

        }
    }
}
