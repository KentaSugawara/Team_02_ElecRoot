using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_transition : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}
