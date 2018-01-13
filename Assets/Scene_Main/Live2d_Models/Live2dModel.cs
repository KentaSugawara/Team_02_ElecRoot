using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live2dModel : MonoBehaviour
{

    [SerializeField]
    private GameObject pad;
    [SerializeField]
    private Main.Main_PlayerCharacter Player;



    void Start()
    {
    }

    void Update()
    {

        if (Player.isLeft == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Player.isLeft == true)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }


    }
}
