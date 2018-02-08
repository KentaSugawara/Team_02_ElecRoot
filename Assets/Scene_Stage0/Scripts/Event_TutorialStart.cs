using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Event_TutorialStart : Main_UI_EventCameraTarget
    {
        [SerializeField]
        private Main_TextViewer _TextViewer;

        [SerializeField]
        private Asset_ViewText _ViewText;

        [SerializeField]
        private Main_UI_EventCameraTarget _EnemyCameraTarget;

        public override void StartEvent(System.Action callback = null)
        {
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

            Main_GameManager.UIManager.HideUI();

            //カメラ移動
            {
                CameraComponent.StartEventCamera(_EnemyCameraTarget);
            }

            ////ここでSE
            //yield return new WaitForSecondsRealtime(1.0f);

            //FadeIn
            {
                bool running = true;
                UIManager.Fade_Normal.StartFade(() => running = false);
                while (running) yield return null;
            }

            _TextViewer.StartView(_ViewText);

            //yield return new WaitForSecondsRealtime(1.0f);

            //_EventPlayerChara.State = CharaState.Walk;
            ////プレイヤー移動
            //{
            //    bool running = true;
            //    _EventPlayerChara.MoveLocalPosition(_MoveTime, _EventPlayerChara.transform.localPosition, _EndPosition, () => running = false);
            //    while (running) yield return null;
            //}

            ////FadeOut
            //if (!UIManager.Fade_Normal.isFadeOuted)
            //{
            //    bool running = true;
            //    UIManager.Fade_Normal.StartFade(() => running = false);
            //    while (running) yield return null;
            //}

            ////カメラ戻す
            //{
            //    CameraComponent.EndEventCamera();
            //}

            ////プレイヤー戻す
            //{
            //    PlayerComponent.Live2dTexture.SetActive(true);
            //    _EventPlayerChara.gameObject.SetActive(false);
            //    _EventPlayerChara.State = CharaState.Wait;
            //}

            ////FadeIn
            //{
            //    bool running = true;
            //    UIManager.Fade_Normal.StartFade(() => running = false);
            //    while (running) yield return null;
            //}

            //Time.timeScale = 1.0f;
            //if (callback != null) callback();
            yield break;
        }
    }
}

