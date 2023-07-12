using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private float _threshold = 0.1f;
    [SerializeField] private float _deadzone = 0.025f;
    [SerializeField] private ConfigurableJoint _joint;

    public UnityEvent OnPressed, OnReleased;

    private bool _isPressed = false;
    private Vector3 _startPos;
    
    void Start()
    {
        _startPos = _joint.transform.localPosition;
    }

    void Update()
    {
        if(!_isPressed && GetValue() + _threshold >=1) Pressed();
        if(_isPressed && GetValue() - _threshold <= 0) Released();
    }
    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, _joint.transform.localPosition) / _joint.linearLimit.limit;
        if (Mathf.Abs(value) < _deadzone)
            value = 0;
        return Mathf.Clamp(value, -1, 1);
    }

    private void Pressed()
    {
        _isPressed = true;
        OnPressed?.Invoke();
    }
    private void Released()
    {
        _isPressed = false;
        OnReleased?.Invoke();
    }
}
