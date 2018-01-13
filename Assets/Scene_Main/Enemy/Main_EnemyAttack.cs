using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_EnemyAttack : MonoBehaviour
    {
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
