using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_UI_EventCameraTarget : MonoBehaviour
    {
        [SerializeField]
        private Vector3 Offset;

        [SerializeField]
        private float _OrthoSize = 5.0f;
        public float OrthoSize
        {
            get { return _OrthoSize; }
        }

        public virtual void StartEvent(System.Action callback = null)
        {

        }

        public virtual Vector3 getPosition()
        {
            return transform.position + Offset;
        }
    }
}
