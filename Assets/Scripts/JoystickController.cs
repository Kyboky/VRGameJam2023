using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private Transform _handle;
    [SerializeField] private TMP_Text _xAxis;
    [SerializeField] private TMP_Text _zAxis;
    [SerializeField] private float _minDeadzone;
    
    public float XVal, ZVal;
    public bool Activate;

    public UnityEvent<Vector2> OnValueChange;

    private void Start()
    {
        Activation(false);
    }
    public void Activation(bool act)
    {
        if (!act)
        {
            XVal = 0;
            ZVal = 0;
            try
            {
                _xAxis.text = " 0.0000";
                _zAxis.text = " 0.0000";
            }
            catch { }
            OnValueChange?.Invoke(new Vector2(0,0));
        }
        Activate = act;
    }

    void Update()
    {
        if (Activate)
        {
            Vector3 capPosition = _handle.up;
            XVal = DeadzoneCalculator (Vector3.Dot(capPosition, -this.transform.right) * 2.366f);
            ZVal = DeadzoneCalculator(Vector3.Dot(capPosition, -this.transform.forward) * 2.366f);
            try
            {
                if (XVal >= 0)
                {
                    _xAxis.text = " " + XVal.ToString("0.0000");
                }
                else
                {
                    _xAxis.text = XVal.ToString("0.0000");
                }
                if (ZVal >= 0)
                {
                    _zAxis.text = " " + ZVal.ToString("0.0000");
                }
                else
                {
                    _zAxis.text = ZVal.ToString("0.0000");
                }
            }
            catch { }
           
            OnValueChange?.Invoke(new Vector2(XVal, ZVal));
        }   
    }

    float DeadzoneCalculator(float value)
    {
        float sign = Mathf.Sign(value);
        return sign * Mathf.InverseLerp(_minDeadzone, 1, Mathf.Abs(value));
    }

}
