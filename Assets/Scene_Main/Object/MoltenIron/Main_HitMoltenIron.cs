using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_HitMoltenIron : MonoBehaviour
    {
        /// <summary>
        /// 鉄液がヒットしたときに鉄液側から呼び出される
        /// </summary>
        public virtual void HitMoltenIron()
        {
            Debug.Log("Hit");
        }

        public virtual void HitMoltenIron(Main_Bullet Bullet)
        {
            Debug.Log("Hit");
        }
    }
}
