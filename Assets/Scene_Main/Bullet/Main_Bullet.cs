using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_Bullet : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _Velocity;

        [SerializeField]
        private GameObject _Prefab_Effect;

        public void StartMove(Vector3 Velocity)
        {
            Debug.Log("Shot : " + Velocity);
            _Velocity = Velocity;
            StartCoroutine(Routine_Move());
        }

        private IEnumerator Routine_Move()
        {
            while(true)
            {
                transform.position += _Velocity * Time.deltaTime;
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == (int)Layers.Wall)
            {
                //壁との接触
                Instantiate(_Prefab_Effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if(other.gameObject.layer == (int)Layers.HitMoltenIron)
            {
                //鉄液のヒット対象との接触
                var c = other.gameObject.GetComponent<Main_HitMoltenIron>();
                if (c != null) c.HitMoltenIron();
            }
            else if(other.gameObject.layer == (int)Layers.Enemy)
            {
                //敵との接触
                Instantiate(_Prefab_Effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
