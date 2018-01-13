using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_Camera : MonoBehaviour
    {
        [SerializeField]
        private Transform _Target;

        [SerializeField]
        private float _Distance;

        [SerializeField]
        private float _Speed;

        private void Start()
        {
            StartCoroutine(Routine_Main());
        }

        private IEnumerator Routine_Main()
        {
            while (true)
            {
                
                transform.position = Vector3.Lerp(transform.position, _Target.position + Vector3.up * _Distance, _Speed * Time.deltaTime);
                transform.LookAt(transform.position + Vector3.down);

                yield return null;
            }
        }

    }
}
