using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScript : MonoBehaviour
{
    [Header("Computer")]

    public GameObject chipBrokenVisuals;
    public GameObject TutorialComputerClipboard;

    public bool tutorialActive = true;
    public bool computerBroken;
    public bool computerTutorial = false;
    public bool ComputerTutorialActive = false;

    bool countDownComputerStart;
    bool chipEffectPause;

    float timeUntilComputerBroken = 110f;


    Door door;
    Lights lightScript;
    BrokenEnergyScript brokenEnergyScript;
    AmmoScript ammoScript;
    BigEnergyScript bigEnergyScript;
    StartScrip startScrip;

    public GameObject brokenSound;


    private void Start()
    {
        door = FindObjectOfType<Door>();
        lightScript = FindObjectOfType<Lights>();
        brokenEnergyScript = FindObjectOfType<BrokenEnergyScript>();
        ammoScript = FindObjectOfType<AmmoScript>();
        bigEnergyScript = FindObjectOfType<BigEnergyScript>();
        startScrip = FindObjectOfType<StartScrip>();

        brokenSound.SetActive(false);
    }

    private void Update()
    {
        if(startScrip.startBool == false)
        {
            ComputerBasic();
        }
    }

    void ComputerBasic()
    {
        if (computerTutorial)
        {
            if (TutorialComputerClipboard.transform.position.y <= 250)
            {
                TutorialComputerClipboard.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (TutorialComputerClipboard.transform.position.y >= -340)
            {
                TutorialComputerClipboard.transform.position -= new Vector3(0, 15, 0);
            }
        }

        if (!countDownComputerStart && !tutorialActive)
        {
            StartCoroutine(UntilComputerBroken());
        }

        if (computerBroken && !tutorialActive)
        {
            door.isComputerBroken = true;
            if (chipEffectPause)
            {
                return;
            }
            StartCoroutine(ChipFlashEffect());
        }
    }

    IEnumerator ChipFlashEffect()
    {

        while (true)
        {

            lightScript.computerMalfunction = true;

            chipEffectPause = true;

            chipBrokenVisuals.SetActive(false);

            Material mymat = GetComponent<Renderer>().material;
            mymat.SetColor("_EmissionColor", Color.black);


            foreach (Light AllLights in lightScript.allLightObjects)
            {
                AllLights.intensity = 0;
            }
            foreach (Light AllDoorLights in lightScript.doorLights)
            {
                AllDoorLights.intensity = 0;
            }



            yield return new WaitForSeconds(0.5f);



            mymat.SetColor("_EmissionColor", Color.white);

            foreach (Light AllLights in lightScript.allLightObjects)
            {
                AllLights.intensity = lightScript.currentLightStatus;
            }
            foreach (Light AllDoorLights in lightScript.doorLights)
            {
                AllDoorLights.intensity = lightScript.doorMaxStatus;
            }

            chipBrokenVisuals.SetActive(true);



            yield return new WaitForSeconds(0.5f);



            if (!computerBroken)
            {
                lightScript.computerMalfunction = false;

                chipBrokenVisuals.SetActive(false);

                break;
            }
        }
    }

    IEnumerator UntilComputerBroken()
    {
        countDownComputerStart = true;

        Debug.Log("countDownComp");

        yield return new WaitForSeconds(timeUntilComputerBroken);

        Debug.Log("ComputerBroken");

        computerBroken = true;

        brokenSound.SetActive(true);
    }

    public void ComputerFix()
    {
        countDownComputerStart = false;
        ComputerTutorialActive = false;

        chipEffectPause = false;

        computerBroken = false;
        door.isComputerBroken = false;
        lightScript.tutortialActive = false;


        ammoScript.tutorialActive = false;
        
        bigEnergyScript.tutorialActive = false;
        tutorialActive = false;
        computerTutorial = false;

        brokenSound.SetActive(false);

        StartCoroutine(door.TimeUntulWinRoutine());
    }
}
