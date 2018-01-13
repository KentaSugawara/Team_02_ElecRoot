using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    [CreateAssetMenu(fileName = "MainSettings", menuName = "MainSettings")]
    public class Asset_MainSettings : ScriptableObject
    {
        [Header("鉄棒の最大所持数")]
        //鉄棒の最大所持数
        public int Chara_MaxNumOfBar;

        [Header("BarStorageの獲得可能半径")]
        //BarStorageの獲得可能半径
        public float BarStorage_Radius;
    }
}
