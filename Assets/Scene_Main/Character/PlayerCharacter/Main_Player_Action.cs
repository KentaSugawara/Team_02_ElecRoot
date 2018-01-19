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

        [SerializeField]
        private Main_Player_Attack _HandAttack;

        [SerializeField]
        private float _HandAttackLength;

        [SerializeField]
        private Main_Player_Attack _BarAttack;

        [SerializeField]
        private float _BarAttackLength;

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
            _HandAttack.AttackOnShot(_PlayerCharacter.calcShotVector(), _HandAttackLength);
            yield return new WaitForSeconds(0.0f + 20.0f / 30.0f);
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
            if (_PlayerCharacter.NumOfBar <= 0)
            {
                Debug.Log("鉄棒が足りない");
                yield break;
            }

            inAction = true;
            _PlayerCharacter.ChangeState(CharaState.BarAttack);
            _BarAttack.AttackOnShot(_PlayerCharacter.calcShotVector(), _BarAttackLength);
            yield return new WaitForSeconds(0.0f + 20.0f / 30.0f);
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

            StartCoroutine(Routine_Shot());
        }

        private IEnumerator Routine_Shot()
        {
            _PlayerCharacter.ChangeState(CharaState.Shot);
            inAction = true;
            Vector3 v = _PlayerCharacter.calcShotVector();

            var obj = Instantiate(_Prefab_ShotObj, _PlayerCharacter.transform.position, Quaternion.identity);
            var bullet = obj.GetComponent<Main_Bullet>();
            bullet.StartMove(v * _BulletSpeed);
            yield return new WaitForSeconds(3.0f + 20.0f / 30.0f);
            inAction = false;
            _PlayerCharacter.ChangeState(CharaState.Wait);
        }

        public void Damage()
        {
            StartCoroutine(Routine_Damage());
        }

        private IEnumerator Routine_Damage()
        {
            Debug.Log("Damage");
            _PlayerCharacter.ChangeState(CharaState.Damage);
            yield return new WaitForSeconds(1.0f);
            _PlayerCharacter.ChangeState(CharaState.Wait);
        }

        public void Down()
        {
            _PlayerCharacter.ChangeState(CharaState.Down);
        }
    }
}
