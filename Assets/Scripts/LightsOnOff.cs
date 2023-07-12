using UnityEngine;

public class LightsOnOff : MonoBehaviour
{
    private bool _lightState;
    [SerializeField] private Light[] _lights;
    [SerializeField] private Material _lightMaterial;

    void Start()
    {
        _lightState = false;
        _lightMaterial.SetFloat("_OnOff", _lightState ? 1 : 0);
        foreach (Light light in _lights)
        {
            light.gameObject.SetActive(_lightState);
        }
    }

    public void SwitchLight()
    {
        _lightState = !_lightState;
        _lightMaterial.SetFloat("_OnOff", _lightState ? 1 : 0);
        foreach(Light light in _lights)
        {
            light.gameObject.SetActive(_lightState);
        }
    }
}
