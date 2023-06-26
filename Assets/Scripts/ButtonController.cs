using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadzone = 0.025f;
    [SerializeField] private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;

    private bool _isPressed = false;
    private Vector3 _startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        _startPos = _joint.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isPressed && GetValue() + threshold >=1) Pressed();
        if(_isPressed && GetValue() - threshold <= 0) Released();
    }
    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, _joint.transform.localPosition) / _joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadzone)
            value = 0;
        return Mathf.Clamp(value, -1, 1);
    }

    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }
    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
