using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Main_StageClearEffect : MonoBehaviour
    {
        [SerializeField]
        private List<Image> _Images;

        [SerializeField]
        private float _Delay;

        [SerializeField]
        private float _NeedTimePerOne;

        [SerializeField]
        private Color _StartColor;

        [SerializeField]
        private Color _EndColor;

        [SerializeField]
        private Color _Color_Bezier;

        [SerializeField]
        private Vector3 _StartScale;

        [SerializeField]
        private Vector3 _EndScale;

        [SerializeField]
        private Vector3 _Scale_Bezier;

        public void StartEffect(System.Action callback)
        {
            StartCoroutine(Routine_Effect(callback));
        }

        private IEnumerator Routine_Effect(System.Action callback)
        {
            int cnt = _Images.Count;
            foreach (var image in _Images)
            {
                StartCoroutine(Routine_OneOfEffect(image, () => --cnt));
                yield return new WaitForSecondsRealtime(_Delay);
            }

            while (cnt > 0) yield return null;
            callback();
        }

        private IEnumerator Routine_OneOfEffect(Image image, System.Action callback)
        {
            image.gameObject.SetActive(true);
            Color cb1, cb2;
            Vector3 lb1, lb2;
            float e;
            for (float t = 0.0f; t < _NeedTimePerOne; t += Time.unscaledDeltaTime)
            {
                e = t / _NeedTimePerOne;
                cb1 = Color.Lerp(_StartColor, _Color_Bezier, e);
                cb2 = Color.Lerp(_Color_Bezier, _EndColor, e);
                image.color = Color.Lerp(cb1, cb2, e);
                lb1 = Vector3.Lerp(_StartScale, _Scale_Bezier, e);
                lb2 = Vector3.Lerp(_Scale_Bezier, _EndScale, e);
                image.transform.localScale = Vector3.Lerp(lb1, lb2, e);
                yield return null;
            }

            image.color = _EndColor;
            image.transform.localScale = _EndScale;

            callback();
        }
    }
}
