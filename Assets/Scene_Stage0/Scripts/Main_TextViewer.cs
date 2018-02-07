using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Main_TextViewer : MonoBehaviour
    {
        [SerializeField, Multiline(3)]
        private List<string> _Strings = new List<string>();

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

        public void StartView(System.Action callback)
        {
            _Canvas.SetActive(true);
            StartCoroutine(Routine_View(callback));
        }

        private bool _isInput;
        public void InputNext()
        {
            _isInput = true;
        }

        private IEnumerator Routine_View(System.Action callback)
        {
            foreach (var str in _Strings)
            {
                _TargetText.text = "";
                _isInput = false;
                //行ごとに分ける
                yield return Routine_ViewLine(str);
                _isInput = false;
                while (!_isInput) yield return null;
            }

            callback();
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
                        break;
                    }
                    yield return null;
                }
                _TargetText.text += c;
            }
        }
    }

}