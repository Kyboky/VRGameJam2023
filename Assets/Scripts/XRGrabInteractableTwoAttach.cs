using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform LeftAttachTransform;
    public Transform RightAttachTransform;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = LeftAttachTransform;
        }
        else if(args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = RightAttachTransform;
        }

        base.OnHoverEntered(args);
    }
}
