using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Main_UIManager : MonoBehaviour
    {
        [SerializeField]
        private Main_PlayerCharacter _PlayerCharacter;
        public Main_PlayerCharacter PlayerCharacter
        {
            get { return _PlayerCharacter; }
        }

        [SerializeField]
        private Main_UI_LifeViewer _LifeViewer;
        public Main_UI_LifeViewer LifeViewer
        {
            get { return _LifeViewer; }
        }

        [SerializeField]
        private Main_UI_BarViewer _BarViewer;
        public Main_UI_BarViewer BarViewer
        {
            get { return _BarViewer; }
        }
    }
}
