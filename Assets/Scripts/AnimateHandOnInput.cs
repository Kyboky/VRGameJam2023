using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty PinchAnnimationAction;
    public InputActionProperty GripAnimationAction;
    public Animator HandAnimator;

    void Update()
    {
        float triggerValue = PinchAnnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat("Trigger", triggerValue);
        float gripValue = GripAnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat("Grip", gripValue);
    }
}
