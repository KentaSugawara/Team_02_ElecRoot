using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_DestroyObject : MonoBehaviour
    {
        [SerializeField]
        private float _Time;

        private void Start()
        {
            StartCoroutine(Routine_Destroy());
        }

        private IEnumerator Routine_Destroy()
        {
            yield return new WaitForSeconds(_Time);
            Destroy(gameObject);
        }
    }
}