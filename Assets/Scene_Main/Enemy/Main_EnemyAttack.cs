using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_EnemyAttack : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Prefab_Bite;

        [SerializeField]
        private float _ToActiveSeconds;

        [SerializeField]
        private float _ToDamageSeconds;

        [SerializeField]
        private Main_Enemy _Enemy;
        public Main_Enemy Enemy
        {
            set { _Enemy = value; }
        }

        private void Start()
        {
            StartCoroutine(Routine_Main());
        }

        private IEnumerator Routine_Main()
        {
            yield return new WaitForSeconds(_ToActiveSeconds);
            Instantiate(_Prefab_Bite, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_ToDamageSeconds);
            if (!_Enemy.isDead) Damage();
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }

        public void Damage()
        {
            if (_Player != null)
            {
                _Player.Damage();
            }
        }

        private Main_PlayerCharacter _Player;
        private void OnTriggerEnter(Collider other)
        {
            var component = other.GetComponent<Main_PlayerCharacter>();
            if (component != null)
            {
                _Player = component;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var component = other.GetComponent<Main_PlayerCharacter>();
            if (component == _Player)
            {
                _Player = null;
            }
        }
    }
}
