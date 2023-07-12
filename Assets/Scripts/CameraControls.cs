using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Camera[] Cameras;
    public GameObject ActiveCamera;
    public float SignalStrengthFrequency;

    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _range;
    [SerializeField] private Material _screenMaterial;
    [SerializeField] private bool _isFrequencyStrength;

    private CameraMovement _cameraMovement;
    private int _currentCamera = 0;

    public void NextCamera()
    {
        Cameras[_currentCamera % Cameras.Length].gameObject.SetActive(false);
        _currentCamera++;
        Cameras[_currentCamera % Cameras.Length].gameObject.SetActive(true);
        ActiveCamera = Cameras[_currentCamera % Cameras.Length].gameObject;
    }

    public void ChangeCamera(int cameraIndex)
    {
        if (cameraIndex == _currentCamera) return;
        Cameras[_currentCamera].gameObject.SetActive(false);
        _currentCamera = cameraIndex;
        Cameras[cameraIndex].gameObject.SetActive(true);
        ActiveCamera = Cameras[cameraIndex].gameObject;
        _cameraMovement = ActiveCamera.transform.parent.GetComponent<CameraMovement>();
    }

    public void MoveCamera(Vector2 move)
    {
        if (_cameraMovement == null) return;
        _cameraMovement.MoveCamera(move);
    }

    void Start()
    {
        SignalStrengthFrequency = 1;
        foreach (var camera in Cameras)
        {
            camera.gameObject.SetActive(false);
        }
        Cameras[0].gameObject.SetActive(true);
        ActiveCamera = Cameras[0].gameObject;
    }

    void Update()
    {
        float signalStrength;
        float distance = Vector3.Distance(this.transform.position, Cameras[_currentCamera % Cameras.Length].transform.position);
        if (_isFrequencyStrength)
        {
            signalStrength = SignalStrengthFrequency;
        }
        else
        {
            signalStrength = 1 - Mathf.InverseLerp(_range, _maxDistance, Mathf.Clamp(distance, _range, _maxDistance));
        }
        _screenMaterial.SetFloat("_SignalStrength", signalStrength);
    }
}
