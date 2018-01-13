using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_Bullet : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _Velocity;

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
                //壁のとの接触
                Destroy(gameObject);
            }
            else if(other.gameObject.layer == (int)Layers.HitMoltenIron)
            {
                //鉄液のヒット対象との接触
                var c = other.gameObject.GetComponent<Main_HitMoltenIron>();
                if (c != null) c.HitMoltenIron();
            }
        }
    }
}
