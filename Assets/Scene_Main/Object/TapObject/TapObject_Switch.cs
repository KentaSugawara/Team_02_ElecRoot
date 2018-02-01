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
        private SpriteRenderer _Sprite_On;

        [SerializeField]
        private SpriteRenderer _Sprite_Off;

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
            if (_Event_Off) _Sprite_Off.enabled = true;
            if (_Sprite_On) _Sprite_On.enabled = false;
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
                        if (_Sprite_Off) _Sprite_Off.enabled = true;
                        if (_Sprite_On) _Sprite_On.enabled = false;
                        isOn = false;
                    }
                }
                else
                {
                    if (_Event_On) _Event_On.StartEvent();
                    if (_Sprite_Off) _Sprite_Off.enabled = false;
                    if (_Sprite_On) _Sprite_On.enabled = true;
                    isOn = true;
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
            if (isOn && !canOff) return;

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
