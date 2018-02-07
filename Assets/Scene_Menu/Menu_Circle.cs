using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Circle : MonoBehaviour {
    [SerializeField]
    private RectTransform _RectTransform;

    [SerializeField]
    private float _RotateSpeed;

    private void Start()
    {
        StartCoroutine(Routine_Main());
    }

    private IEnumerator Routine_Main()
    {
        while (true)
        {
            _RectTransform.Rotate(Vector3.forward * _RotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
