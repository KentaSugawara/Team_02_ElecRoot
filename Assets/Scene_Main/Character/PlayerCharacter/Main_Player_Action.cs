using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_Player_Action : MonoBehaviour
    {
        [SerializeField]
        private Main_PlayerCharacter _PlayerCharacter;

        [SerializeField]
        private GameObject _Prefab_ShotObj;

        [SerializeField]
        bool inAction = false;

        public void HandAttack()
        {
            if (!inAction)
            {
                StartCoroutine(Routine_HandAttack());
            }
        }

        private IEnumerator Routine_HandAttack()
        {
            inAction = true;
            _PlayerCharacter.ChangeState(CharaState.HandAttack);
            yield return new WaitForSeconds(1.0f + 8.0f / 30.0f);
            inAction = false;
            _PlayerCharacter.ChangeState(CharaState.Wait);
        }

        public void BarAttack()
        {
            if (!inAction)
            {
                StartCoroutine(Routine_BarAttack());
            }
        }

        private IEnumerator Routine_BarAttack()
        {
            inAction = true;
            _PlayerCharacter.ChangeState(CharaState.BarAttack);
            yield return new WaitForSeconds(1.0f + 8.0f / 30.0f);
            //yield return new WaitForSeconds(3.3333f);
            inAction = false;
            _PlayerCharacter.ChangeState(CharaState.Wait);
        }

        [SerializeField]
        private float _BulletSpeed;

        public void Shot()
        {
            if (!_PlayerCharacter.UseBar())
            {
                Debug.Log("鉄棒が足りない");
                return;
            }

            Vector3 v = _PlayerCharacter.calcShotVector();

            var obj = Instantiate(_Prefab_ShotObj, _PlayerCharacter.transform.position, Quaternion.identity);
            var bullet = obj.GetComponent<Main_Bullet>();
            bullet.StartMove(v * _BulletSpeed);
        }
    }
}
