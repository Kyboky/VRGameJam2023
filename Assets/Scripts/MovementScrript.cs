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


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        Vector3 force = forceVector.normalized * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if (rigidbody.velocity.magnitude < maxSpeed)
            rigidbody.AddForce(force);
        rigidbody.AddTorque(Vector3.Cross(Vector3.up, force) * torqueCompesation);

        float sign;
        if (Input.GetAxis("Vertical") >= -0.01f) sign = 1;
        else sign = Mathf.Sign(Vector3.Dot(rigidbody.velocity,this.transform.forward));

        
        Debug.DrawLine(this.transform.position + this.transform.up, this.transform.position + this.transform.up + force * torqueCompesation);
        Vector3 newAngularVelocity = new Vector3(rigidbody.angularVelocity.x, Input.GetAxis("Horizontal") * sign * angularSpeed, rigidbody.angularVelocity.z);
        rigidbody.angularVelocity = newAngularVelocity;
    }
}
