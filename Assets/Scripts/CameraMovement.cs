using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _cameraRotationSpeed;
    [SerializeField] private Vector2 _xLimit;
    [SerializeField] private Vector2 _yLimit;

    public void MoveCamera(Vector2 control)
    {
        if (control.Equals(Vector2.zero)) return;
        Vector3 rotAxis = Vector3.Cross(this.transform.forward,this.transform.right * control.x + this.transform.up * control.y);
        this.transform.RotateAround(this.transform.position, rotAxis,  control.magnitude * _cameraRotationSpeed);
        this.transform.localEulerAngles = new Vector3(Mathf.Clamp(this.transform.localEulerAngles.x, _xLimit.x, _xLimit.y), Mathf.Clamp(this.transform.localEulerAngles.y, _yLimit.x, _yLimit.y), 0);
    }
}
