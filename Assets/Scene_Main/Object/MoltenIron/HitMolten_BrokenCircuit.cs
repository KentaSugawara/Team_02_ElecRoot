using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class HitMolten_BrokenCircuit : Main_HitMoltenIron
    {
        [SerializeField]
        private Renderer _Renderer; //テスト用

        [SerializeField]
        private Color _AfterColor; //テスト用

        private bool _AlreadyHit = false;

        public override void HitMoltenIron()
        {
            if (_AlreadyHit == false)
            {
                //鉄液がヒットしたとき
                _Renderer.material.color = _AfterColor;
                _AlreadyHit = true;
            }
        }
    }
}
