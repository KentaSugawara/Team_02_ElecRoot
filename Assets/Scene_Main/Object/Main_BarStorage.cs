using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_BarStorage : MonoBehaviour
    {
        [SerializeField]
        private GameObject CircleView; //テスト用

        private void Start()
        {
            var collider = GetComponent<BoxCollider>();
            CircleView.transform.localScale = Vector3.Scale(transform.lossyScale, collider.size);
        }

        /// <summary>
        /// 操作中のプレイヤーキャラの鉄棒を補充する
        /// </summary>
        /// <returns></returns>
        /// 
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
            if (other.gameObject.layer == (int)Layers.Character)
            {
                Main_PlayerCharacter chara = other.GetComponent<Main_PlayerCharacter>();
                if (chara == null) return;

                float r = Main_GameManager.MainSettings.BarStorage_Radius;
                //設定の距離よりも近ければ
                var pos1 = new Vector2(chara.transform.position.x, chara.transform.position.z);
                var pos2 = new Vector2(transform.position.x, transform.position.z);
                chara.ReplenishmentBar();
            }
        }

        //public bool ReplenishmentBar(Main_PlayerCharacter chara)
        //{
        //    float r = Main_GameManager.MainSettings.BarStorage_Radius;
        //    //設定の距離よりも近ければ
        //    var pos1 = new Vector2(chara.transform.position.x, chara.transform.position.z);
        //    var pos2 = new Vector2(transform.position.x, transform.position.z);
        //    Debug.Log(Vector3.SqrMagnitude(pos1 - pos2));
        //    if (Vector3.SqrMagnitude(pos1 - pos2) <= r * r)
        //    {
        //        return chara.ReplenishmentBar();
        //    }

        //    return false;
        //}
    }
}
