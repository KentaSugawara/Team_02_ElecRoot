using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_WorldShadow : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _Sprite;

        private void Start()
        {
            _Sprite.enabled = true;
        }
    }
}
