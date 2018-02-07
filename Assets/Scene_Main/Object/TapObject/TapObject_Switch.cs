using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class TapObject_Switch : Main_TapObject
    {
        [SerializeField]
        private GameObject BoxView; //テスト用

        [SerializeField]
        private GameObject _Sprite;

        [SerializeField]
        private GameObject _Obj_On;

        [SerializeField]
        private GameObject _Obj_Off;

        [SerializeField]
        private Main_UI_EventCameraTarget _Event_On;

        [SerializeField]
        private Main_UI_EventCameraTarget _Event_Off;

        [SerializeField]
        private bool canOff;

        private bool isOn = false;

        private void Start()
        {
            _Sprite.gameObject.SetActive(false);
            if (_Obj_Off) _Obj_Off.SetActive(true);
            if (_Obj_On) _Obj_On.SetActive(false);
            isOn = false;
        }

        public override bool Tap(Main_PlayerCharacter chara)
        {
            if (_Character == chara)
            {
                base.Tap(chara);

                if (isOn)
                {
                    if (canOff)
                    {
                        if (_Event_Off) _Event_Off.StartEvent();
                        if (_Obj_Off) _Obj_Off.SetActive(true);
                        if (_Obj_On) _Obj_On.SetActive(false);
                        isOn = false;
                    }
                }
                else
                {
                    if (_Event_On) _Event_On.StartEvent();
                    if (_Obj_Off) _Obj_Off.SetActive(false);
                    if (_Obj_On) _Obj_On.SetActive(true);
                    isOn = true;
                    if (!canOff) _Sprite.gameObject.SetActive(false);
                }
            }

            return true;
        }

        private Main_PlayerCharacter _Character = null;
        private void OnTriggerEnter(Collider other)
        {
            if (isOn && !canOff) return;

            if (other.gameObject.layer == (int)Layers.Character)
            {
                var chara = other.GetComponent<Main_PlayerCharacter>();
                //Debug.Log(chara);
                if (chara != null)
                {
                    _Sprite.gameObject.SetActive(true);
                    _Character = chara;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == (int)Layers.Character)
            {
                var chara = other.GetComponent<Main_PlayerCharacter>();
                if (chara == _Character)
                {
                    _Sprite.gameObject.SetActive(false);
                    _Character = null;
                }
            }
        }
    }
}
