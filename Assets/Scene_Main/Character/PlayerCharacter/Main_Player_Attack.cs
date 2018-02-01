using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_Player_Attack : MonoBehaviour
    {
        [SerializeField]
        private Collider _Collider;

        [SerializeField]
        private int _Power;

        [SerializeField]
        private GameObject _Prefab_HitEffect;

        [SerializeField]
        private float _AttackNeedTime;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Attack(float Time, Vector2 Direction, float Length)
        {
            //HitEnemys.Clear();
            gameObject.SetActive(true);
            StopAllCoroutines();
            transform.localPosition = new Vector3(Direction.x, 0.0f, Direction.y) * Length;
            StartCoroutine(Routine_Time(Time));
        }

        public void AttackOneShot(Vector2 Direction, float Length, Vector3 Offset)
        {
            //HitEnemys.Clear();
            gameObject.SetActive(true);
            StartCoroutine(Routine_AttackOneShot(Direction, Length, Offset));
        }

        private IEnumerator Routine_AttackOneShot(Vector2 Direction, float Length, Vector3 Offset)
        {
            yield return new WaitForSeconds(_AttackNeedTime);
            transform.localPosition = new Vector3(Direction.x, 0.0f, Direction.y) * Length + Offset;
            yield return null;
            Damage();
            gameObject.SetActive(false);
        }

        //private List<Main_Enemy> HitEnemys = new List<Main_Enemy>();
        private Main_Enemy _TargetEnemy;
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Main_Enemy>();
            if (enemy != null)
            {
                _TargetEnemy = enemy;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var enemy = other.GetComponent<Main_Enemy>();
            if (enemy != null && _TargetEnemy == enemy)
            {
                _TargetEnemy = null;
            }
        }

        private IEnumerator Routine_Time(float Time)
        {
            yield return new WaitForSeconds(Time);
            gameObject.SetActive(false);
        }

        private void Damage()
        {
            if (_TargetEnemy)
            {
                _TargetEnemy.Damage(_Power);

                if (_Prefab_HitEffect != null)
                {
                    var obj = Instantiate(_Prefab_HitEffect);
                    obj.transform.position = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0.0f, Random.Range(-0.1f, 0.1f));
                }
            }
        }
    }

}
