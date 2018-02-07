using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Main
{
    public class Main_SceneManager : MonoBehaviour
    {
        private bool AlreadyRunning = false;

        [SerializeField]
        private Image _FadeImage;

        [SerializeField]
        private float _FadeNeedTime;

        [Space(5)]
        [SerializeField]
        private Main_TouHikari _Tou_Hikari;

        [Space(5)]
        [SerializeField]
        private Image _Image_GameOver;

        [SerializeField]
        private float _GameOverNeedTime;

        [SerializeField]
        private Main_StageClearEffect _StageClear;

        [SerializeField]
        private GameObject _Button_ToTitle;

        [SerializeField]
        private GameObject _Button_ToRetry;

        [SerializeField]
        private float _ButtonNeedTime;

        [SerializeField]
        private string _Title_SceneName;

        [SerializeField]
        private string _Retry_SceneName;

        private void Start()
        {
            _FadeImage.gameObject.SetActive(false);
            _Image_GameOver.gameObject.SetActive(false);
            _Button_ToTitle.SetActive(false);
            _Button_ToRetry.SetActive(false);
        }

        public void Start_GameClear()
        {
            StartCoroutine(Routine_GameClear());
        }

        private IEnumerator Routine_GameClear()
        {
            var UIManager = Main_GameManager.UIManager;
            var CameraComponent = Main_GameManager.MainCamera.GetComponent<Main_Camera>();

            //FadeOut
            {
                bool isRunning = true;
                Main_GameManager.UIManager.Fade_Normal.StartFade(() => isRunning = false);
                while (isRunning) yield return null;
            }
            
            //MoveCamera
            {
                CameraComponent.StartEventCamera(_Tou_Hikari.CameraTarget);
            }

            //FadeIn
            {
                bool isRunning = true;
                Main_GameManager.UIManager.Fade_Normal.StartFade(() => isRunning = false);
                while (isRunning) yield return null;
            }

            {
                bool isRunning = true;
                _Tou_Hikari.StartClearFlash(() => isRunning = false);
                while (isRunning) yield return null;
            }

            {
                bool isRunning = true;
                _StageClear.StartEffect(() => isRunning = false);
                while (isRunning) yield return null;
            }

            yield return new WaitForSecondsRealtime(1.0f);

            //FadeOut
            {
                bool isRunning = true;
                Main_GameManager.UIManager.Fade_Dissolve.StartFade(() => isRunning = false);
                while (isRunning) yield return null;
            }

            yield return new WaitForSecondsRealtime(1.0f);

            SceneManager.LoadScene(_Title_SceneName);
        }

        public void Start_GameOver()
        {
            if (!AlreadyRunning)
            {
                AlreadyRunning = true;
                StartCoroutine(Routine_GameOver());
            }
        }

        private IEnumerator Routine_GameOver()
        {
            //フェードアウト
            {
                _FadeImage.gameObject.SetActive(true);
                Color s = _FadeImage.color;
                s.a = 0.0f;
                Color e = _FadeImage.color;
                e.a = 1.0f;
                _FadeImage.color = s;

                for (float t = 0.0f; t < _FadeNeedTime; t += Time.deltaTime)
                {
                    _FadeImage.color = Color.Lerp(s, e, t / _FadeNeedTime);
                    yield return null;
                }
                _FadeImage.color = e;
            }

            //GameOverImage
            {
                _Image_GameOver.gameObject.SetActive(true);
                Color s = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                Color e = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                _Image_GameOver.color = s;

                for (float t = 0.0f; t < _FadeNeedTime; t += Time.deltaTime)
                {
                    _Image_GameOver.color = Color.Lerp(s, e, t / _FadeNeedTime);
                    yield return null;
                }
                _Image_GameOver.color = e;
            }

            //Button
            {
                _Button_ToTitle.SetActive(true);
                _Button_ToRetry.SetActive(true);
            }
        }

        public void ToTitle()
        {
            SceneManager.LoadScene(_Title_SceneName);
        }

        public void ToRetry()
        {
            SceneManager.LoadScene(_Retry_SceneName);
        }
    }
}
