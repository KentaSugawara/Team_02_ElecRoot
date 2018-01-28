using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Event_Rift : Main_UI_EventCameraTarget
    {
        [SerializeField]
        private Main_EventPlayerCharacter _EventPlayerChara;

        [SerializeField]
        private float _MoveTime;

        [SerializeField]
        private Vector3 _EndPosition;


        public override void StartEvent(System.Action callback = null)
        {
            base.StartEvent(callback);
            StartCoroutine(Routine_Event(callback));
        }

        private IEnumerator Routine_Event(System.Action callback = null)
        {
            Time.timeScale = 0.0f;

            var UIManager = Main_GameManager.UIManager;
            var CameraComponent = Main_GameManager.MainCamera.GetComponent<Main_Camera>();
            var PlayerComponent = UIManager.PlayerCharacter;

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

            //プレイヤー登場
            {
                PlayerComponent.Live2dTexture.SetActive(false);
                _EventPlayerChara.gameObject.SetActive(true);
                _EventPlayerChara.State = CharaState.Wait;

                Debug.Log(new Vector4(
                    _EndPosition.x,
                    0.0f,
                    _EndPosition.z
                    ));
                PlayerComponent.transform.position = transform.localToWorldMatrix * new Vector4(
                    _EndPosition.x,
                    0.0f,
                    _EndPosition.z
                    ) + new Vector4(0.0f, PlayerComponent.transform.position.y, 0.0f, 1.0f);
            }

            //ここでSE
            yield return new WaitForSecondsRealtime(1.0f);

            //FadeIn
            {
                bool running = true;
                UIManager.Fade_Normal.StartFade(() => running = false);
                while (running) yield return null;
            }

            yield return new WaitForSecondsRealtime(1.0f);

            _EventPlayerChara.State = CharaState.Walk;
            //プレイヤー移動
            {
                bool running = true;
                _EventPlayerChara.MoveLocalPosition(_MoveTime, _EventPlayerChara.transform.localPosition, _EndPosition, () => running = false);
                while (running) yield return null;
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

            //プレイヤー戻す
            {
                PlayerComponent.Live2dTexture.SetActive(true);
                _EventPlayerChara.gameObject.SetActive(false);
                _EventPlayerChara.State = CharaState.Wait;
            }

            //FadeIn
            {
                bool running = true;
                UIManager.Fade_Normal.StartFade(() => running = false);
                while (running) yield return null;
            }

            Time.timeScale = 1.0f;
            if (callback != null) callback();
        }
    }
}
