using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Main_UI_LifeViewer : MonoBehaviour
    {
        [SerializeField]
        private Main_UIManager _UIManager;

        [SerializeField]
        private List<Image> Lifes = new List<Image>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            UpdateLife();
        }

        public void UpdateLife()
        {
            for (int i = 0; i < Lifes.Count; ++i)
            {
                Lifes[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < _UIManager.PlayerCharacter.HP && i < Lifes.Count; ++i)
            {
                Lifes[i].gameObject.SetActive(true);
            }
        }
    }
}
