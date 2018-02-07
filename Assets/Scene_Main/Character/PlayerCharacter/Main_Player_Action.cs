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
        private Vector3 _HandAttackOffset;

        [SerializeField]
        private Main_Player_Attack _BarAttack;

        [SerializeField]
        private float _BarAttackLength;


        [SerializeField]
        private Vector3 _BarAttackOffset;

        [SerializeField]
        private float _ShotOffsetLength;

        [SerializeField]
        private Vector3 _ShotOffset;

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
            _HandAttack.AttackOneShot(_PlayerCharacter.calcShotVector(), _HandAttackLength, _HandAttackOffset);
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
            _BarAttack.AttackOneShot(_PlayerCharacter.calcShotVector(), _BarAttackLength, _BarAttackOffset);
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

            Vector3 v = _PlayerCharacter.calcShotVector();
            yield return new WaitForSeconds(0.0f + 10.0f / 30.0f);

            inAction = true;

            float offset;
            if (_PlayerCharacter.isLeft) offset = _ShotOffsetLength;
            else offset = -_ShotOffsetLength;
            var obj = Instantiate(_Prefab_ShotObj, _PlayerCharacter.transform.position + Vector3.left * offset + _ShotOffset, Quaternion.identity);
            obj.transform.LookAt(obj.transform.position + v);
            var bullet = obj.GetComponent<Main_Bullet>();
            bullet.StartMove(v * _BulletSpeed);
            yield return new WaitForSeconds(0.0f + 20.0f / 30.0f);
            inAction = false;
            _PlayerCharacter.ChangeState(CharaState.Wait);
        }

        public void Damage()
        {
            StartCoroutine(Routine_Damage());
        }

        private IEnumerator Routine_Damage()
        {
            _PlayerCharacter.ChangeState(CharaState.Damage);
            yield return new WaitForSeconds(0.5f);
            _PlayerCharacter.ChangeState(CharaState.Wait);
        }

        public void Down()
        {
            _PlayerCharacter.ChangeState(CharaState.Down);
        }
    }
}
