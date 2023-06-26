using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationRayActivation : MonoBehaviour
{
    [SerializeField] GameObject leftRay;
    [SerializeField] GameObject rightRay;

    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rightActivate;

    // Update is called once per frame
    void Update()
    {
        leftRay.SetActive(leftActivate.action.ReadValue<float>() > 0.1f);
        rightRay.SetActive(rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
