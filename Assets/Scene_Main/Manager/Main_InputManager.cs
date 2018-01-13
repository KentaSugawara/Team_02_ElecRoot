using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class Main_InputManager : MonoBehaviour
    {
        [SerializeField]
        private UI_Controller_MovePad _MovePad;

        [SerializeField]
        private bool _LockOnActive;

        private void Start()
        {
            StartCoroutine(Routine_Main());
        }

        private IEnumerator Routine_Main()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                if (_MovePad.NowInput)
                {
                    var chara = Main_GameManager.UIManager.PlayerCharacter;
                    if (chara != null) chara.Move(_MovePad.InputVector, _MovePad.Magnitude);
                }
                else
                {
                    var chara = Main_GameManager.UIManager.PlayerCharacter;
                    if (chara != null) chara.MoveWait();
                }

                //yield return null;
            }
        }

        public void Chara_HandAttack()
        {
            var chara = Main_GameManager.UIManager.PlayerCharacter;
            if (chara != null) chara.Action.HandAttack();
        }

        public void Chara_BarAttack()
        {
            var chara = Main_GameManager.UIManager.PlayerCharacter;
            if (chara != null) chara.Action.BarAttack();
        }

        public void Chara_Shot()
        {
            var chara = Main_GameManager.UIManager.PlayerCharacter;
            if (chara != null) chara.Action.Shot();
        }

        private int Mask_TapScreen = 1 << 9 | 1 << 15; // LockOnObject | TapObject
        /// <summary>
        /// ScreenがタップされたときにRayを飛ばして対象を探す
        /// </summary>
        /// <param name="data"></param>
        public void TapScreen(BaseEventData data)
        {
            RaycastHit hit;

            var chara = Main_GameManager.UIManager.PlayerCharacter;
            if (chara == null) return;

            var point = ((PointerEventData)data).position;
            Ray ray = Main_GameManager.MainCamera.ScreenPointToRay(point);

            if (Physics.Raycast(ray, out hit, 1000.0f, Mask_TapScreen))
            {
                //BarStorage
                {
                    var target = hit.collider.GetComponent<Main_TapObject>();
                    if (target != null)
                    {
                        if (target.Tap(chara)) return;
                    }
                }

                if (_LockOnActive)
                {
                    //LockOn
                    {
                        var target = hit.collider.GetComponent<Main_LockOnObject>();
                        if (target != null)
                        {
                            target.LockOn(chara);
                            if (chara.LockOnTarget != target)
                                chara.setLockOn(target);
                            else
                                chara.stopLockOn();

                            return;
                        }
                    }
                }
            }
        }
    }
}