using UnityEngine;

public class TankTracksController : MonoBehaviour
{
    [SerializeField] private Transform[] _rightWheels;
    [SerializeField] private Transform[] _leftWheels;
    [SerializeField] private Material _leftTrack;
    [SerializeField] private Material _rightTrack;
    [SerializeField] private float _rotSpeed = 278;
    [SerializeField] private float _trackSpeed = 18.7f;

    private float _leftTrackOffset = 0;
    private float _rightTrackOffset = 0;
    private Vector3 _lastPosition;
    private Vector3 _lastDirtection;

    void Start()
    {
        _lastPosition = this.transform.position;
        _lastDirtection = this.transform.forward;
    }

    void Update()
    {
        Vector3 rotDir = Vector3.Cross(_lastDirtection, this.transform.forward);
        float rotSign = rotDir.y < 0 ? -1 : 1;

        float rot = 0.395f * Mathf.Sin(rotDir.magnitude) * rotSign; //0.395 center to radius track
        float offset = Vector3.Dot(_lastDirtection,(this.transform.position - _lastPosition));
        float leftOffset = (rot + offset);
        float rightOffset = (-rot + offset);

        foreach (Transform wheel in _rightWheels)
        {
            wheel.Rotate(Vector3.right, _rotSpeed * rightOffset);
        }
        foreach (Transform wheel in _leftWheels)
        {
            wheel.Rotate(Vector3.right, _rotSpeed * leftOffset);
        }

        _leftTrackOffset += leftOffset * _trackSpeed;
        _rightTrackOffset += rightOffset * _trackSpeed;
        _leftTrack.SetFloat("_Offset", _leftTrackOffset);
        _rightTrack.SetFloat("_Offset", _rightTrackOffset);

        _lastPosition = this.transform.position;
        _lastDirtection = this.transform.forward;
    }
}
