using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Controller_MovePad : MonoBehaviour
{
    [SerializeField]
    private bool _isActive;

    [SerializeField]
    private RectTransform _FieldTransform;

    [SerializeField]
    private RectTransform _PadTransform;

    private bool _NowInput;
    public bool NowInput
    {
        get { return _NowInput; }
    }

    private Vector2 _InputVector;
    public Vector2 InputVector
    {
        get { return _InputVector; }
    }

    private float _Magnitude;
    public float Magnitude
    {
        get { return _Magnitude; }
    }

    [SerializeField]
    private float _CircleScale;

    private float CircleRadius;

    private void Start()
    {
        CircleRadius = _FieldTransform.lossyScale.x * 100.0f * 0.5f * _CircleScale;
    }

    public void PointerDown(BaseEventData data)
    {
        var pointdata = (PointerEventData)data;

        _NowInput = true;
        MovePad(pointdata.position - (Vector2)_FieldTransform.position);
    }

    public void PointerUp(BaseEventData data)
    {
        _NowInput = false;
        ResetPad();
    }

    public void PointerDrag(BaseEventData data)
    {
        if (_NowInput)
        {
            var pointdata = (PointerEventData)data;
            MovePad(pointdata.position - (Vector2)_FieldTransform.position);
        }
    }

    private void MovePad(Vector2 v)
    {
        if (_NowInput)
        {
            float mag = v.magnitude;
            if (mag > CircleRadius) mag = CircleRadius;
            _InputVector = Vector3.Normalize(v) * mag / CircleRadius;
            _Magnitude = mag / CircleRadius;
            _PadTransform.position = (Vector2)_FieldTransform.position + _InputVector * CircleRadius;
        }
    }

    private void ResetPad()
    {
        _InputVector = Vector2.zero;
        _PadTransform.position = _FieldTransform.position;
    }
}
