using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class JoystickController : MonoBehaviour
{
    [SerializeField] Transform handle;

    [SerializeField] TMP_Text xAxis;
    [SerializeField] TMP_Text zAxis;

    [SerializeField] float minDeadzone;
    public float xVal, zVal;

    public bool activate;

    public UnityEvent<Vector2> OnValueChange;

    private void Start()
    {
        Activation(false);
    }
    public void Activation(bool act)
    {
        if (!act)
        {
            xVal = 0;
            zVal = 0;
            try
            {
                xAxis.text = " 0.0000";
                zAxis.text = " 0.0000";
            }
            catch { }
            OnValueChange.Invoke(new Vector2(0,0));
        }
        activate = act;
    }
    // Update is called once per frame
    void Update()
    {

        if (activate)
        {
            Vector3 capPosition = handle.up;
            xVal = DeadzoneCalculator (Vector3.Dot(capPosition, -this.transform.right) * 2.366f);
            zVal = DeadzoneCalculator(Vector3.Dot(capPosition, -this.transform.forward) * 2.366f);
            try
            {
                if (xVal >= 0)
                {
                    xAxis.text = " " + xVal.ToString("0.0000");
                }
                else
                {
                    xAxis.text = xVal.ToString("0.0000");
                }
                if (zVal >= 0)
                {
                    zAxis.text = " " + zVal.ToString("0.0000");
                }
                else
                {
                    zAxis.text = zVal.ToString("0.0000");
                }
            }
            catch { }
           
            OnValueChange.Invoke(new Vector2(xVal, zVal));
        }
        
    }

    float DeadzoneCalculator(float value)
    {
        float sign = Mathf.Sign(value);
        return sign * Mathf.InverseLerp(minDeadzone, 1, Mathf.Abs(value));
    }

}
