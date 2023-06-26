using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float cameraRotationSpeed;
    [SerializeField] Vector2 xLimit;
    [SerializeField] Vector2 yLimit;

    public void MoveCamera(Vector2 control)
    {
        Vector3 rotAxis = Vector3.Cross(this.transform.forward,this.transform.right * control.x + this.transform.up * control.y);
        this.transform.RotateAround(this.transform.position, rotAxis,  control.magnitude * cameraRotationSpeed);
        this.transform.localEulerAngles = new Vector3(Mathf.Clamp(this.transform.localEulerAngles.x, xLimit.x, xLimit.y), Mathf.Clamp(this.transform.localEulerAngles.y, yLimit.x, yLimit.y), 0);
    }
}
