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

        public void AttackOnShot(Vector2 Direction, float Length)
        {
            //HitEnemys.Clear();
            gameObject.SetActive(true);
            StopAllCoroutines();
            transform.localPosition = new Vector3(Direction.x, 0.0f, Direction.y) * Length;
            StartCoroutine(Routine_OneShot());
        }

        //private List<Main_Enemy> HitEnemys = new List<Main_Enemy>();

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Main_Enemy>();
            if (enemy != null)
            {
                enemy.Damage(_Power);

                if (_Prefab_HitEffect != null)
                {
                    var obj = Instantiate(_Prefab_HitEffect);
                    obj.transform.position = other.transform.position + enemy.CenterOffset;
                }

                gameObject.SetActive(false);
                StopAllCoroutines();
            }
        }

        private IEnumerator Routine_Time(float Time)
        {
            yield return new WaitForSeconds(Time);
            gameObject.SetActive(false);
        }

        private IEnumerator Routine_OneShot()
        {
            yield return null;
            gameObject.SetActive(false);
        }
    }

}
