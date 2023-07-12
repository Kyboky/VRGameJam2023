using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SliderController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _interactable;
    [SerializeField] private ConfigurableJoint _joint;
    private bool _isActive;

    public UnityEvent<float> OnValueChange;

    void Start()
    {
        _isActive = false;
        _interactable.selectEntered.AddListener(Activate);
        _interactable.selectExited.AddListener(Deactivate);
    }
    public void Activate(SelectEnterEventArgs args)
    {
        _isActive = true;
    }
    public void Deactivate(SelectExitEventArgs args)
    {
        _isActive = false;
    }

    private void Update()
    {
        if (_isActive)
        {
            Change();
        }
    }
    public void Change()
    {
        float x = GetValue();
        OnValueChange?.Invoke(GetValue());
    }

    private float GetValue()
    {
        float value = Vector3.Dot(_joint.transform.position - this.transform.position, -this.transform.right) / 0.112f / 2 + 0.5f;
        return value;
    }
}
