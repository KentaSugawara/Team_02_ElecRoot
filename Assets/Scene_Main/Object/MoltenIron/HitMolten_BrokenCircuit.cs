using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class HitMolten_BrokenCircuit : Main_HitMoltenIron
    {
        [SerializeField]
        private SpriteRenderer _BrokenRenderer;

        [SerializeField]
        private GameObject _DenkiParticle;

        [SerializeField]
        private SpriteRenderer _LightTexture;

        [SerializeField]
        private Transform _MaskTransform;
        [SerializeField]
        private Vector3 _MaskStartLocalScale;
        [SerializeField]
        private Vector3 _MaskEndLocalScale;

        [SerializeField]
        private GameObject _LockOnObj;

        [SerializeField]
        private float _LightSpan = 1.0f;

        [SerializeField]
        private float _RepairSpeed = 1.0f;

        [SerializeField]
        private Main_UI_EventCameraTarget _EventTarget;

        private bool _AlreadyHit = false;

        private void Start()
        {
            _LockOnObj.SetActive(true);
            StartCoroutine(Routine_Light());
        }

        private IEnumerator Routine_Light()
        {
            yield return null;
            Color s = _LightTexture.material.color;
            Color e = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            while(true)
            {
                for (float t = 0; t < _LightSpan; t += Time.deltaTime)
                {
                    if (_MaskTransform) _MaskTransform.localScale = Vector3.Lerp(_MaskStartLocalScale, _MaskEndLocalScale, t / _LightSpan);
                    _LightTexture.material.color = Color.Lerp(s, e, t / _LightSpan);
                    yield return null;
                }
                _LightTexture.material.color = e;

                for (float t = 0; t < _LightSpan; t += Time.deltaTime)
                {
                    if (_MaskTransform) _MaskTransform.localScale = Vector3.Lerp(_MaskEndLocalScale, _MaskStartLocalScale, t / _LightSpan);
                    _LightTexture.material.color = Color.Lerp(e, s, t / _LightSpan);
                    yield return null;
                }
                _LightTexture.material.color = s;
            }
        }


        public override void HitMoltenIron()
        {
            if (_AlreadyHit == false)
            {
                //鉄液がヒットしたとき
                _DenkiParticle.SetActive(false);
                _LockOnObj.SetActive(false);
                Main_GameManager.UIManager.PlayerCharacter.stopLockOn();
                StopAllCoroutines();
                StartCoroutine(Routine_Hit());
                _AlreadyHit = true;
            }
        }

        private IEnumerator Routine_Hit()
        {
            Color s1 = _BrokenRenderer.material.color;
            Color s2 = _LightTexture.material.color;
            Color e = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            Vector3 ms = _MaskTransform.localScale;
            for (float t = 0; t < _RepairSpeed; t += Time.deltaTime)
            {
                float dt = t / _RepairSpeed;
                if (_MaskTransform)
                {
                    Vector3 p1 = Vector3.Lerp(ms, Vector3.zero, dt);
                    _MaskTransform.localScale = Vector3.Lerp(ms, p1, dt);
                }
                _BrokenRenderer.material.color = Color.Lerp(s1, e, dt);
                _LightTexture.material.color = Color.Lerp(s2, e, dt);
                yield return null;
            }
            _BrokenRenderer.material.color = e;
            _LightTexture.material.color = e;
            _MaskTransform.gameObject.SetActive(false);

            Main_GameManager.RepairCiruit();

            if (_EventTarget) _EventTarget.StartEvent();
        }
    }
}
