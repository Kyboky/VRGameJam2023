using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTracksController : MonoBehaviour
{
    [SerializeField] Transform[] rightWheels;
    [SerializeField] Transform[] leftWheels;

    [SerializeField] Material leftTrack;
    [SerializeField] Material rightTrack;

    float leftTrackOffset = 0;
    float rightTrackOffset = 0;

    [SerializeField] float rotSpeed;
    [SerializeField] float trackSpeed;


    Vector3 lastPosition;
    Vector3 lastDirtection;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = this.transform.position;
        lastDirtection = this.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotDir = Vector3.Cross(lastDirtection, this.transform.forward);
        float rotSign = rotDir.y < 0 ? -1 : 1;

        float rot = 0.395f * Mathf.Sin(rotDir.magnitude) * rotSign; //0.395 center to radius track
        float offset = Vector3.Dot(lastDirtection,(this.transform.position - lastPosition));
        float leftOffset = (rot + offset);
        float rightOffset = (-rot + offset);

        foreach (Transform wheel in rightWheels)
        {
            wheel.Rotate(Vector3.right, rotSpeed * rightOffset);
        }
        foreach (Transform wheel in leftWheels)
        {
            wheel.Rotate(Vector3.right, rotSpeed * leftOffset);
        }

        leftTrackOffset += leftOffset * trackSpeed;
        rightTrackOffset += rightOffset * trackSpeed;
        leftTrack.SetFloat("_Offset", leftTrackOffset);
        rightTrack.SetFloat("_Offset", rightTrackOffset);

        lastPosition = this.transform.position;
        lastDirtection = this.transform.forward;
    }
}
