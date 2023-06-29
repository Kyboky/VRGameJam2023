using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOnOff : MonoBehaviour
{
    bool lightState;
    [SerializeField] Light[] lights;
    [SerializeField] Material lightMaterial;
    // Start is called before the first frame update
    void Start()
    {
        lightState = false;
        lightMaterial.SetFloat("_OnOff", lightState ? 1 : 0);
        foreach (Light light in lights)
        {
            light.gameObject.SetActive(lightState);
        }
    }

    public void SwitchLight()
    {
        lightState = !lightState;
        lightMaterial.SetFloat("_OnOff", lightState ? 1 : 0);
        foreach(Light light in lights)
        {
            light.gameObject.SetActive(lightState);
        }
    }
}
