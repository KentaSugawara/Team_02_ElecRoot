using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_EnemyTest : MonoBehaviour
    {
        [SerializeField]
        private Transform Target;

        private void Start()
        {
            StartCoroutine(Routine_Move());
        }

        private IEnumerator Routine_Move()
        {
            while(true)
            {
                transform.position += (Target.position - transform.position).normalized * 1.0f * Time.deltaTime;
                yield return null;
            }
        }
    }
}

