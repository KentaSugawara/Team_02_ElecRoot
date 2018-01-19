using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croutine_Test : MonoBehaviour {

    [SerializeField]
    private int _Frame;

    private void Start()
    {
        StartCoroutine(RoutineAsync(_Frame));
    }

    private IEnumerator RoutineAsync(int frame)
    {
        for (int i = 0; i < frame; ++i)
        {
            transform.Rotate(0, 1, 0);
            yield return null;
        }
    }
}
