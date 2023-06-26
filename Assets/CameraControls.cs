using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] Camera[] cameras;
    [SerializeField] float maxDistance;
    [SerializeField] Material screenMaterial;

    int currentCamera = 0;

    void ChangeCamera()
    {
        cameras[currentCamera%cameras.Length].gameObject.SetActive(false);
        currentCamera++;
        cameras[currentCamera % cameras.Length].gameObject.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach (var camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeCamera();
        }
        float distance = Vector3.Distance(this.transform.position, cameras[currentCamera].transform.position);
        float signalStrength = 1 - Mathf.InverseLerp(0, maxDistance, distance);

        Debug.Log(signalStrength);
        screenMaterial.SetFloat("_SignalStrength", signalStrength);
    }
}
