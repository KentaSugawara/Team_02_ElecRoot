using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    [CreateAssetMenu(menuName = "StageChipSet")]
    public class Main_StageChipSet : ScriptableObject
    {
        public List<GameObject> Objects = new List<GameObject>();
    }
}
