using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_RotateObject : MonoBehaviour
    {
        [SerializeField]
        private Vector3 D;

        [SerializeField]
        private float t;

        private void Start()
        {
            StartCoroutine(Routine_Main());
        }

        private IEnumerator Routine_Main()
        {
            while (true)
            {
                transform.Rotate(D);
                yield return new WaitForSeconds(t);
            }
        }
    }
}