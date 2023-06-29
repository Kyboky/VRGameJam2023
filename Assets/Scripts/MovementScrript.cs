using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class MovementScrript : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float angularSpeed = 1.5f;
    [SerializeField] float speed = 1000;
    [SerializeField] float maxSpeed = 0.8f;
    [SerializeField] float torqueCompesation;

    [SerializeField] LayerMask mask;

    [SerializeField] Transform[] tracks;

    public Vector2 controllerValues;

    public void ChangeControllerValues(Vector2 vec)
    {
        controllerValues = vec;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    

    private void FixedUpdate()
    {
        //Vector3 forwardVector = Vector3.zero;
        //Vector3 backwardVector = Vector3.zero;
        Vector3 forceVector = Vector3.zero;
        Vector3 finalTorque = Vector3.zero;
        RaycastHit hit;
        foreach (var track in tracks)
        {
            if (Physics.Raycast(track.position, -track.up, out hit, 0.2f, mask))
            {
                Vector3 forceDir = Vector3.Cross(hit.normal, -this.transform.right);
                forceVector += forceDir;
            }
        }

        Vector3 force = forceVector.normalized * controllerValues.y * speed * Time.deltaTime;
        if (rigidbody.velocity.magnitude < maxSpeed)
            rigidbody.AddForce(force);
        rigidbody.AddTorque(Vector3.Cross(Vector3.up, force) * torqueCompesation);

        Vector3 newAngularVelocity = new Vector3(rigidbody.angularVelocity.x, controllerValues.x * angularSpeed, rigidbody.angularVelocity.z);
        rigidbody.angularVelocity = newAngularVelocity;
    }
}
