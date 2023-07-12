using System.Collections;
using UnityEngine;
using TMPro;

public class CameraPicker : MonoBehaviour
{
    [SerializeField] private TMP_Text _cameraIndexText;
    [SerializeField] private TMP_Text _signalStrengthText;
    private CameraControls _cameraControlls;

    void Start()
    {
        _cameraControlls = GetComponent<CameraControls>();
        StartCoroutine("LateInit");
    }

    IEnumerator LateInit()
    {
        yield return new WaitForSeconds(0.1f);
        PickCamera(0.5132f);
    }

    public void PickCamera(float frequency)
    {
        if(frequency < 0.02f)
        {
            _cameraIndexText.text = "1";
            _signalStrengthText.text = "0.0";
            return;
        }
        else if (frequency > 0.98f)
        {
            _cameraIndexText.text = _cameraControlls.Cameras.Length.ToString();
            _signalStrengthText.text = "0.0";
            return;
        }
        else
        {
            float bandBetweenFrequencies = 1 / (float)_cameraControlls.Cameras.Length;
            float cameraIndex = Mathf.Floor(frequency / bandBetweenFrequencies);
            float signalStrength = 1-Mathf.Abs(((frequency / bandBetweenFrequencies - cameraIndex) - 0.5f) *2);
            _cameraControlls.SignalStrengthFrequency = signalStrength;
            _cameraControlls.ChangeCamera((int)cameraIndex);
            _cameraIndexText.text = (cameraIndex+1).ToString();
            _signalStrengthText.text = signalStrength.ToString();
        }
        
    } 
}
