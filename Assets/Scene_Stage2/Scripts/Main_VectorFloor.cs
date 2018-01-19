using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_VectorFloor : MonoBehaviour {
    [SerializeField]
    private GameObject _SpriteObject;
    private GameObject _SpriteObject_Copy;

    [SerializeField]
    private Vector2 _Vector;

    [SerializeField]
    private float _Speed;

    [SerializeField]
    private float _Length;

    [SerializeField]
    private bool _Reset;

    [SerializeField]
    private bool _Stop;

    private float Offset;
    private float Offset2;

    // Use this for initialization
    void Start () {
        _SpriteObject_Copy = Instantiate(_SpriteObject);
        _SpriteObject_Copy.transform.SetParent(transform, false);
        Offset2 = -_Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (_Reset)
        {
            _Reset = false;
            Offset = 0.0f;
            Offset2 = -_Length;
            _Vector = Vector2.zero;
        }
        if (!_Stop)
        {
            float DTime = Time.deltaTime;
            Offset += DTime;
            Offset2 += DTime;
            if (Offset > _Length)
            {
                Offset -= _Length * 2.0f;
            }
            if (Offset2 > _Length)
            {
                Offset2 -= _Length * 2.0f;
            }

            _SpriteObject.transform.localPosition = new Vector3(_Vector.x * Offset, _Vector.y * Offset, 0.0f);
            _SpriteObject_Copy.transform.localPosition = new Vector3(_Vector.x * Offset2, _Vector.y * Offset2, 0.0f);
        }
	}
}
