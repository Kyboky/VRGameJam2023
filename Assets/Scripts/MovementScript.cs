using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _angularSpeed = 1.5f;
    [SerializeField] private float _speed = 1000;
    [SerializeField] private float _maxSpeed = 0.8f;
    [SerializeField] private float _torqueCompesation = -0.1f;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform[] _tracks;

    public Vector2 ControllerValues;

    public void ChangeControllerValues(Vector2 vec)
    {
        ControllerValues = vec;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 forceVector = Vector3.zero;
        RaycastHit hit;
        foreach (var track in _tracks)
        {
            if (Physics.Raycast(track.position, -track.up, out hit, 0.2f, _mask))
            {
                Vector3 forceDir = Vector3.Cross(hit.normal, -this.transform.right);
                forceVector += forceDir;
            }
        }

        Vector3 force = forceVector.normalized * ControllerValues.y * _speed * Time.deltaTime;
        if (_rigidbody.velocity.magnitude < _maxSpeed)
            _rigidbody.AddForce(force);
        _rigidbody.AddTorque(Vector3.Cross(Vector3.up, force) * _torqueCompesation);

        Vector3 newAngularVelocity = new Vector3(_rigidbody.angularVelocity.x, ControllerValues.x * _angularSpeed, _rigidbody.angularVelocity.z);
        _rigidbody.angularVelocity = newAngularVelocity;
    }
}
