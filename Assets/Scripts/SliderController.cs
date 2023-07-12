using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SliderController : MonoBehaviour
{
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] XRGrabInteractable interactable;
    [SerializeField] ConfigurableJoint joint;

    Vector3 startPosition;
    bool isActive;

    public UnityEvent<float> onValueChange;
    // Start is called before the first frame update
    void Start()
    {

        isActive = false;
        startPosition = transform.position;
        interactable.selectEntered.AddListener(Activate);
        interactable.selectExited.AddListener(Deactivate);
    }
    public void Activate(SelectEnterEventArgs args)
    {
        isActive = true;
    }
    public void Deactivate(SelectExitEventArgs args)
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            Change();
        }
    }
    public void Change()
    {
        float x = GetValue();
        onValueChange.Invoke(GetValue());
    }

    private float GetValue()
    {
        float value = Vector3.Dot(joint.transform.position - this.transform.position, -this.transform.right) / 0.112f / 2 + 0.5f;
        
        return value;
    }

}
