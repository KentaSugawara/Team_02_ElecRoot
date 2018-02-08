using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Main_TextViewer : MonoBehaviour
    {
        [SerializeField]
        private Text _TargetText;

        [SerializeField]
        private GameObject _Canvas;

        [SerializeField]
        private float _Speed;

        private void Start()
        {
            _Canvas.SetActive(false);
        }

        public void StartView(Asset_ViewText ViewText, System.Action callbackNextText = null)
        {
            _Canvas.SetActive(true);
            StartCoroutine(Routine_View(ViewText, callbackNextText));
        }

        private bool _isInput;
        public void InputNext()
        {
            _isInput = true;
        }

        private IEnumerator Routine_View(Asset_ViewText ViewText, System.Action callbackNextText)
        {
            foreach (var str in ViewText.Strings)
            {
                _TargetText.text = "";
                _isInput = false;
                //行ごとに分ける
                yield return Routine_ViewLine(str);
                _isInput = false;
                while (!_isInput) yield return null;
            }

            if (callbackNextText != null) callbackNextText();
        }

        private IEnumerator Routine_ViewLine(string str)
        {
            foreach (var c in str)
            {
                for (float t = 0.0f; t < _Speed; t += Time.unscaledDeltaTime)
                {
                    if (_isInput)
                    {
                        _TargetText.text = str;
                        yield break;
                    }
                    yield return null;
                }
                _TargetText.text += c;
            }
        }
    }

}