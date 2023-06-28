using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    public Camera[] cameras;
    [SerializeField] float maxDistance;
    [SerializeField] float range;
    [SerializeField] Material screenMaterial;
    [SerializeField] bool isFrequencyStrength;

    CameraMovement cameraMovement;

    public GameObject activeCamera;

    public float signalStrengthFrequency;



    int currentCamera = 0;

    public void NextCamera()
    {
        cameras[currentCamera % cameras.Length].gameObject.SetActive(false);
        currentCamera++;
        cameras[currentCamera % cameras.Length].gameObject.SetActive(true);
        activeCamera = cameras[currentCamera % cameras.Length].gameObject;
    }

    public void ChangeCamera(int cameraIndex)
    {
        if (cameraIndex == currentCamera) return;
        cameras[currentCamera].gameObject.SetActive(false);
        currentCamera = cameraIndex;
        cameras[cameraIndex].gameObject.SetActive(true);
        activeCamera = cameras[cameraIndex].gameObject;
        cameraMovement = activeCamera.transform.parent.GetComponent<CameraMovement>();
    }

    public void MoveCamera(Vector2 move)
    {
        if (cameraMovement == null) return;
        cameraMovement.MoveCamera(move);
    }

    // Start is called before the first frame update
    void Start()
    {
        signalStrengthFrequency = 1;
        int currentCamera = 0;
        foreach (var camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);
        activeCamera = cameras[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ChangeCamera();
        //}

        float signalStrength;
        float distance = Vector3.Distance(this.transform.position, cameras[currentCamera % cameras.Length].transform.position);
        if (isFrequencyStrength)
        {
            signalStrength = signalStrengthFrequency;
        }
        else
        {
            signalStrength = 1 - Mathf.InverseLerp(range, maxDistance, Mathf.Clamp(distance, range, maxDistance));
        }
        screenMaterial.SetFloat("_SignalStrength", signalStrength);
    }
}
