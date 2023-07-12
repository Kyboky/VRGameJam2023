using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CameraPicker : MonoBehaviour
{
    [SerializeField] TMP_Text cameraIndexText;
    [SerializeField] TMP_Text signalStrengthText;
    CameraControls cameraControlls;

    // Start is called before the first frame update
    void Start()
    {
        cameraControlls = GetComponent<CameraControls>();

        StartCoroutine("lateInit");
    }

    IEnumerator lateInit()
    {
        yield return new WaitForSeconds(0.1f);
        pickCamera(0.5132f);
    }

    public void pickCamera(float frequency)
    {
        if(frequency < 0.02f)
        {
            cameraIndexText.text = "1";
            signalStrengthText.text = "0.0";
            return;
        }
        else if (frequency > 0.98f)
        {
            cameraIndexText.text = cameraControlls.cameras.Length.ToString();
            signalStrengthText.text = "0.0";
            return;
        }
        else
        {
            float bandBetweenFrequencies = 1 / (float)cameraControlls.cameras.Length;
            float cameraIndex = Mathf.Floor(frequency / bandBetweenFrequencies);
            float signalStrength = 1-Mathf.Abs(((frequency / bandBetweenFrequencies - cameraIndex) - 0.5f) *2);
            cameraControlls.signalStrengthFrequency = signalStrength;
            cameraControlls.ChangeCamera((int)cameraIndex);
            cameraIndexText.text = (cameraIndex+1).ToString();
            signalStrengthText.text = signalStrength.ToString();
        }
        
    } 
}
