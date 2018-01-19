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
        private List<GameObject> _Targets;

        [SerializeField]
        private GameObject _Sprite;

        [SerializeField]
        private SpriteRenderer _Sprite_On;

        [SerializeField]
        private SpriteRenderer _Sprite_Off;

        private bool isOn = false;

        private void Start()
        {
            _Sprite.gameObject.SetActive(false);
            _Sprite_Off.enabled = true;
            _Sprite_On.enabled = false;
            isOn = false;
        }

        public override bool Tap(Main_PlayerCharacter chara)
        {
            if (_Character == chara)
            {
                base.Tap(chara);

                if (isOn)
                {
                    foreach (var obj in _Targets)
                    {
                        obj.SetActive(true);
                    }
                    _Sprite_Off.enabled = true;
                    _Sprite_On.enabled = false;
                    isOn = false;
                }
                else
                {
                    foreach (var obj in _Targets)
                    {
                        obj.SetActive(false);
                    }
                    _Sprite_Off.enabled = false;
                    _Sprite_On.enabled = true;
                    isOn = true;
                }


            }

            return true;
        }

        private Main_PlayerCharacter _Character = null;
        private void OnTriggerEnter(Collider other)
        {
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
