using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Main_UI_BarViewer : MonoBehaviour
    {
        [SerializeField]
        private Main_UIManager _UIManager;

        [SerializeField]
        private List<Image> Numbers = new List<Image>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            UpdateBarView();
        }

        public void UpdateBarView()
        {
            Debug.Log("Update");
            for (int i = 0; i < Numbers.Count; ++i)
            {
                Numbers[i].gameObject.SetActive(false);
            }

            if (_UIManager.PlayerCharacter.NumOfBar >= Numbers.Count)
            {
                Numbers[Numbers.Count - 1].gameObject.SetActive(true);
            }
            else
            {
                Numbers[_UIManager.PlayerCharacter.NumOfBar].gameObject.SetActive(true);
            }
        }
    }
}
