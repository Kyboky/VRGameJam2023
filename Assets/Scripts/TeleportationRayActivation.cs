using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationRayActivation : MonoBehaviour
{
    [SerializeField] private GameObject _leftRay;
    [SerializeField] private GameObject _rightRay;

    [SerializeField] private InputActionProperty _leftActivate;
    [SerializeField] private InputActionProperty _rightActivate;

    void Update()
    {
        _leftRay.SetActive(_leftActivate.action.ReadValue<float>() > 0.1f);
        _rightRay.SetActive(_rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
