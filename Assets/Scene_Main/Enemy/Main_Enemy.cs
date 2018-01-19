using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Enemy : MonoBehaviour
{
    [SerializeField]
    private int _HP;

    [SerializeField]
    private float DeadNeedTime;

    [SerializeField]
    private Vector3 _CenterOffset;
    public Vector3 CenterOffset
    {
        get{ return _CenterOffset; }
    }

    public bool isDead { get; private set; }

    public void Damage(int value)
    {
        if (isDead) return;

        _HP -= value;
        if (_HP <= 0)
        {
            isDead = true;
            StartCoroutine(Routine_Dead());
        }
    }

    private IEnumerator Routine_Dead()
    {
        Vector3 s = transform.localScale;
        Vector3 e = Vector3.zero;
        for (float t = 0.0f; t < DeadNeedTime; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(s, e, t / DeadNeedTime); 
            yield return null;
        }
    }
}
