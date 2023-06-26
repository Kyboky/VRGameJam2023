using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour
{
    public List<WheelInfo> wheels; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel


    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float leftTorque = motor;
        float rightTorque = motor;

        foreach (WheelInfo wheel in wheels)
        {
            if (wheel.isLeft)
            {
                wheel.wheel.motorTorque = leftTorque;
            }
            else
            {
                wheel.wheel.motorTorque = rightTorque;
            }
        }
    }
}

[System.Serializable]
public class WheelInfo
{
    public bool isLeft;
    public WheelCollider wheel;
}