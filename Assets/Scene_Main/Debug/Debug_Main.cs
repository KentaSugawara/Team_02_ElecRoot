using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Main : MonoBehaviour {
    public void GameClear()
    {
        Main.Main_GameManager.GameClear();
    }

    public void GameOver()
    {
        Main.Main_GameManager.GameOver();
    }
}
