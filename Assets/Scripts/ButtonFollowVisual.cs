using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    public Transform VisualTarget;
    public Vector3 LocalAxis;
    public float ResetSpeed = 5;
    public float FollowAngleThreshold = 45;
   
    private bool _freeze = false;
    private Vector3 _offset;
    private Transform _pokeAttachTransform;
    private Vector3 _initialLocalPos;
    private XRBaseInteractable _interactable;
    private bool _isFollowing = false;

    void Start()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(Follow);
        _interactable.hoverExited.AddListener(Reset);
        _interactable.selectEntered.AddListener(Freeze);
        _initialLocalPos = VisualTarget.localPosition;
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            _freeze = false;
            _isFollowing = false;
        }
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            
            _pokeAttachTransform = interactor.attachTransform;
            _offset = VisualTarget.position -_pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(_offset, VisualTarget.TransformDirection(LocalAxis));
            if(pokeAngle < FollowAngleThreshold)
            {
                _freeze = false;
                _isFollowing = true;
            }

        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            _freeze = true;
        }
    }

    void Update()
    {
        if (_freeze) return;

        if(_isFollowing)
        {
            Vector3 localTargetPosition = VisualTarget.InverseTransformPoint(_pokeAttachTransform.position + _offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, LocalAxis);

            VisualTarget.position = VisualTarget.TransformPoint( constrainedLocalTargetPosition);
        }
        else
        {
            VisualTarget.localPosition = Vector3.Lerp(VisualTarget.localPosition, _initialLocalPos, Time.deltaTime * ResetSpeed);
        }
    }
}
