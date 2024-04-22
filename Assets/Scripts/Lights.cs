using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public float currentLightStatus = 5.11f;
    public float minLightStatus = 2.5f;
    public float maxLightStatus = 5.11f;
    public float doorMaxStatus = 2;

    float lessLightTimer = 0.5f;

    bool pauseInLessLight;
    bool stopFlicker = false;

    public bool computerMalfunction = false;
    public bool lightTutorial = true;
    public bool lightTutorialActive = true;

    public bool tutortialActive = true;


    public Light[] allLightsObject;
    public Light[] doorLights;

    public GameObject LightTutorialObject;

    StartScrip startScrip;
    BigEnergyScript bigEnergyScript;

    private void Start()
    {
        startScrip = FindObjectOfType<StartScrip>();
        bigEnergyScript = FindObjectOfType<BigEnergyScript>();

        if (lightTutorialActive)
        {
            currentLightStatus = 0;

            foreach (Light AllLights in allLightsObject)
            {
                AllLights.intensity = currentLightStatus;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lightTutorial)
        {
            if (LightTutorialObject.transform.position.y <= 250)
            {

                LightTutorialObject.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (LightTutorialObject.transform.position.y >= -340)
            {
                LightTutorialObject.transform.position -= new Vector3(0, 15, 0);
            }
        }

        if (startScrip.startBool == false && tutortialActive == false)
        {
            LightBasics();
        }
    }

    private void LightBasics()
    {
        if (currentLightStatus >= minLightStatus  && !computerMalfunction)
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

        int whatLightToFlicker = Random.Range(0, 54);

        allLightsObject[whatLightToFlicker].intensity = 0;

        stopFlicker = false;
    }

    public void RestoreLight()
    {
        currentLightStatus += 0.5f;
    }

    public void RestoreLightTutorial()
    {
        Debug.Log("More Light Tutorial");
        currentLightStatus += 0.5f;

        foreach (Light AllLights in allLightsObject)
        {
            AllLights.intensity = currentLightStatus;
        }

        if (currentLightStatus >= maxLightStatus)
        {
            lightTutorial = false;
            lightTutorialActive = false;

            bigEnergyScript.energyTutorial = true;
            bigEnergyScript.energyTutorialActive = true;

            currentLightStatus = maxLightStatus;
        }
    }
}
