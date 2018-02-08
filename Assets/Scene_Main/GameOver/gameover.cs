using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{

    public class gameover : MonoBehaviour
    {
        [SerializeField]
        private Main_StageOverEffect _StageClear;

        public void Start_GameOver()
        {
                StartCoroutine(Routine_GameOver());
        }

        private IEnumerator Routine_GameOver()
        {

            //GameOverImage
            {
                bool isRunning = true;
                _StageClear.StartEffect(() => isRunning = false);
                while (isRunning) yield return null;
            }
            yield return new WaitForSecondsRealtime(1.0f);

        }

    }
}