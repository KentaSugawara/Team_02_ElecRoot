using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_FrontObject : MonoBehaviour
    {
        [SerializeField]
        private List<SpriteRenderer> _TargetSprites;

        [SerializeField]
        private List<GameObject> _HideObjects;

        [SerializeField]
        private float _NormalAlpha = 1.0f;

        [SerializeField]
        private float _FrontAlpha = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<Main_PlayerCharacter>();
            if (target != null)
            {
                SetTransparent(_FrontAlpha);
                SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<Main_PlayerCharacter>();
            if (target != null)
            {
                SetTransparent(_NormalAlpha);
                SetActive(true);
            }
        }
        
        private void SetTransparent(float a)
        {
            foreach (var sprite in _TargetSprites)
            {
                Color c = sprite.color;
                c.a = a;
                sprite.color = c;
            }
        }

        private void SetActive(bool value)
        {
            foreach (var obj in _HideObjects)
            {
                obj.SetActive(value);
            }
        }
    }
}
