using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_StageColliders : MonoBehaviour
    {
        private void Awake()
        {
            var renderers = transform.GetComponentsInChildren<MeshRenderer>();
            foreach(var r in renderers)
            {
                r.enabled = false;
            }
        }
    }
}