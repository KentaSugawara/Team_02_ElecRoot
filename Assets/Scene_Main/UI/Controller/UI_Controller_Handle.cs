using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main
{
    public class UI_Controller_Handle : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _Field;

        [SerializeField]
        private RectTransform _Handle;

        [SerializeField]
        private float _Field_Length;

        [SerializeField]
        private float _Handle_LocalStartY;

        [SerializeField]
        private float _Handle_LocalEndY;

        private Vector3 HandlePosition;

        [SerializeField]
        private float HandleStartY;
        [SerializeField]
        private float HandleEndY;
        //private float RailLength;

        [SerializeField]
        private Image _FieldImage;

        private bool isMove = true;

        private void Start()
        {
            //HandleStartY = _Field.position.y + _Field_Length / 2.0f * _Field.lossyScale.y;
            //HandleEndY = _Field.position.y - _Field_Length / 2.0f * _Field.lossyScale.y;
            //RailLength = _Field.sizeDelta.y;
            _Handle.transform.localPosition
                = new Vector3(
                    _Handle.transform.localPosition.x,
                    _Handle_LocalStartY,
                    _Handle.transform.localPosition.z
                );
            HandleStartY = _Handle.transform.position.y;

            _Handle.transform.localPosition
                = new Vector3(
                    _Handle.transform.localPosition.x,
                    _Handle_LocalEndY,
                    _Handle.transform.localPosition.z
                );
            HandleEndY = _Handle.transform.position.y;

            StartCoroutine(Routine_Main());
        }

        public void PointerDown(BaseEventData data)
        {
            Vector3 point = ((PointerEventData)data).position;

            //var rect = _Handle.rect;
            //rect.size *= 3.0f;

            //if (rect.Contains(point - _Handle.position))
            //{
            //    PositionUpdate(point.y);
            //    isMove = false;
            //    _InputSuccess = false;
            //}
            PositionUpdate(point.y);
            isMove = false;
            _InputSuccess = false;
        }

        public void PointerDrag(BaseEventData data)
        {
            if (isMove == false)
            {
                Vector3 point = ((PointerEventData)data).position;
                PositionUpdate(point.y);
            }
        }

        public void PointerUp(BaseEventData data)
        {
            if (isMove == false)
            {
                if (_InputSuccess)
                {
                    Main_GameManager.InputManager.Chara_Shot();
                    Main_GameManager.InputManager.Chara_StopTargetFind();
                    _InputSuccess = false;
                }
                isMove = true;
            }
            _FieldImage.color = Color.white;
        }

        private bool _InputSuccess = false;
        public bool InputSuccess { get { return _InputSuccess; } }
        private void PositionUpdate(float posY)
        {
            _InputSuccess = false;
            if (posY > HandleStartY)
            {
                posY = HandleStartY;
            }
            else if (posY < HandleEndY)
            {
                posY = HandleEndY;
                _InputSuccess = true;
                _FieldImage.color = Color.red;
                Main_GameManager.InputManager.Chara_StartTargetFind();
            }
            else
            {
                _InputSuccess = false;
                _FieldImage.color = Color.white;
                Main_GameManager.InputManager.Chara_StopTargetFind();
            }

            Vector3 pos = _Handle.position;
            pos.y = posY;
            _Handle.position = pos;
        }

        private float MoveTargetY = 0.0f;
        private IEnumerator Routine_Main()
        {
            while (true)
            {
                if (isMove)
                {
                    Vector3 pos = _Handle.position;
                    pos.y = Mathf.Lerp(pos.y, HandleStartY, 0.9f);
                    _Handle.position = pos;
                }
                yield return null;
            }
        }
    }
}
