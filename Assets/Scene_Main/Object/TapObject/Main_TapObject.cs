using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_TapObject : MonoBehaviour
    {
        /// <summary>
        /// 鉄液がヒットしたときに鉄液側から呼び出される
        /// </summary>
        public virtual bool Tap(Main_PlayerCharacter chara)
        {
            Debug.Log("Hit");
            return false;
        }
    }
}