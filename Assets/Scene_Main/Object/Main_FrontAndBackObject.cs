using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_FrontAndBackObject : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _FrontLocalPosition;

        [SerializeField]
        private Vector3 _BackLocalPosition;

        private void Start()
        {
            StartCoroutine(Routine_Main());
        }

        private IEnumerator Routine_Main()
        {
            while(true)
            {
                if (Main_GameManager.UIManager.PlayerCharacter.transform.position.z > transform.position.z)
                {
                    transform.localPosition = _FrontLocalPosition;
                }
                else
                {
                    transform.localPosition = _BackLocalPosition;
                }

                yield return null;
            }
        }
    }
}
