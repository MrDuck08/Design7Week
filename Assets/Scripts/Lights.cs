using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public float currentLightStatus = 5.11f;
    public float minLightStatus = 2.5f;
    public float maxLightStatus = 5.11f;
    float lessLightTimer = 0.5f;

    bool pauseInLessLight;
    bool stopFlicker;

    public Light[] allLightsObject;
    public Light[] doorLights;

    public float doorMaxStatus = 2;

    // Update is called once per frame
    void Update()
    {
        LightBasics();
    }

    private void LightBasics()
    {
        if (currentLightStatus >= minLightStatus)
        {
            if (!stopFlicker)
            {
                StartCoroutine(LighFlickerRoutine());
            }

            foreach (Light AllLights in allLightsObject)
            {
                AllLights.intensity = currentLightStatus;
            }

            if (!pauseInLessLight)
            {
                StartCoroutine(LessLightRoutine());
            }

            if (currentLightStatus >= maxLightStatus)
            {
                currentLightStatus = maxLightStatus;
            }
        }
    }

    IEnumerator LessLightRoutine()
    {
        pauseInLessLight = true;

        yield return new WaitForSeconds(lessLightTimer);

        currentLightStatus -= 0.01f;

        pauseInLessLight = false;
    }

    IEnumerator LighFlickerRoutine()
    {
        stopFlicker = true;

        yield return new WaitForSeconds(5);

        int whatLightToFlicker = Random.Range(0, 53);

        allLightsObject[whatLightToFlicker].intensity = 0;

        stopFlicker = false;
    }

    public void RestoreLight()
    {
        currentLightStatus += 0.5f;
    }
}
