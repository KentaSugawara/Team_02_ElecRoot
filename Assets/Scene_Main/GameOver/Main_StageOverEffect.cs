using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Main_StageOverEffect : MonoBehaviour
    {
        [SerializeField]
        private List<Image> _Images;

        [SerializeField]
        private Image _LastImage;

        [SerializeField]
        private List<int> _Indexes;

        [SerializeField]
        private float _Delay;

        [SerializeField]
        private float _Delay1;

        [SerializeField]
        private float _Delay2;


        [SerializeField]
        private int _RotateInt;

        [SerializeField]
        private int _Rotate1Int;

        [SerializeField]
        private Vector3 _Rotation;

        [SerializeField]
        private Vector3 _NewRotation;

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

        [SerializeField]
        private Vector3 _StartOffset;

        [SerializeField]
        private Vector3 _Offset_Bezier;

        [SerializeField]
        private float minAngle = 0.0F;

        [SerializeField]
        private float maxAngle = 90.0F;


        public void StartEffect(System.Action callback)
        {
            _LastImage.transform.Rotate(Vector3.zero);
            StartCoroutine(Routine_Effect(callback));
        }

        private IEnumerator Routine_Effect(System.Action callback)
        {
            int cnt = _Images.Count;
            foreach (var index in _Indexes)
            {
                StartCoroutine(Routine_OneOfEffect(_Images[_Indexes[index]], () => --cnt));
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
            Vector3 ob1, ob2;
            Vector3 EndOffset = image.rectTransform.localPosition;
            Vector3 StartOffset = EndOffset + _StartOffset;
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
                ob1 = Vector3.Lerp(StartOffset, _Offset_Bezier + StartOffset, e);
                ob2 = Vector3.Lerp(_Offset_Bezier + StartOffset, EndOffset, e);
                image.rectTransform.localPosition = Vector3.Lerp(ob1, ob2, e);
                yield return null;
            }

            image.color = _EndColor;
            image.transform.localScale = _EndScale;
            image.rectTransform.localPosition = EndOffset;
            StartCoroutine(Routine_RotateEffect(callback));

            callback();
        }

        private IEnumerator Routine_RotateEffect(System.Action callback)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            {
                for (float t = 0.0f; t < _NeedTimePerOne; t += Time.unscaledDeltaTime)
                {
                    Vector3 rotation = Vector3.Lerp(_Rotation, _NewRotation, t * 2);
                    _LastImage.rectTransform.localEulerAngles = rotation;
                    yield return null;
                }
            }

            callback();
        }

    }
}
