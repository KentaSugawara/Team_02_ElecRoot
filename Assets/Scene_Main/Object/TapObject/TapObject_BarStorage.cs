using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class TapObject_BarStorage : Main_TapObject
    {
        [SerializeField]
        private GameObject CircleView; //テスト用

        private void Start()
        {
            CircleView.transform.localScale = new Vector3
                (
                Main_GameManager.MainSettings.BarStorage_Radius * 2.0f,
                1.0f,
                Main_GameManager.MainSettings.BarStorage_Radius * 2.0f
                );
        }

        /// <summary>
        /// 操作中のプレイヤーキャラの鉄棒を補充する
        /// </summary>
        /// <returns></returns>
        public override bool Tap(Main_PlayerCharacter chara)
        {
            float r = Main_GameManager.MainSettings.BarStorage_Radius;
            //設定の距離よりも近ければ
            var pos1 = new Vector2(chara.transform.position.x, chara.transform.position.z);
            var pos2 = new Vector2(transform.position.x, transform.position.z);
            Debug.Log(Vector3.SqrMagnitude(pos1 - pos2));
            if (Vector3.SqrMagnitude(pos1 - pos2) <= r * r)
            {
                return chara.ReplenishmentBar();
            }

            return false;
        }
    }
}

