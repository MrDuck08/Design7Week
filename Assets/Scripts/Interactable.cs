using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region Who Am I!
    [Header("Who Am I?")]

    public bool amIBigEnergy;
    public bool amIComputer;
    public bool amIAmmo;

    #endregion

    #region Energy
    [Header("Energy")]

    public GameObject[] SmallEnergyObject;

    public bool energyBroken;
    bool countDownEnergyStart;

    float timeUntilEnergyBroken = 10f;

    public float emissionrate = 1.8f;

    #endregion

    #region Computer/Chip
    [Header("Computer")]

    public GameObject chipBrokenVisuals;

    public bool computerBroken;
    bool countDownComputerStart;
    bool chipEffectPause;

    float timeUntilComputerBroken = 110f;

    #endregion

    #region Ammo
    [Header("Ammo")]

    float timeUntilAmmoBroken = 30f;

    public AudioSource noAmmoSound;
    public AudioSource ammoExistSound;

    public bool ammoFix;

    bool noAmmo;
    bool countdownAmmoStart;

    #endregion

    Door door;
    Lights lightScript;

    private void Start()
    {
        door = FindAnyObjectByType<Door>();
        lightScript = FindObjectOfType<Lights>();

        countDownEnergyStart = false;
        countdownAmmoStart = false;
        countDownComputerStart = false;

        if (amIBigEnergy)
        {
            Material mymat = GetComponentInChildren<Renderer>().material;
            mymat.SetColor("_EmissionColor", new Color(0, 191, 191, 0) * 0.018f);
        }
    }

    void Update()
    {
        if (amIBigEnergy)
        {
            EnergyBasic();
        }

        if (amIComputer)
        {
            ComputerBasic();
        }

        if(amIAmmo)
        {
            AmmoBasic();
        }
    }

    #region Ammo

    void AmmoBasic()
    {
        if(!countdownAmmoStart) 
        {
           StartCoroutine(UntilNoAmmo());
        } 

        if(noAmmo)
        {
            door.isAmmobroken = true;

            if(noAmmoSound != null)
            {
                noAmmoSound.Play();
            }
        }
        else
        {
            if(ammoExistSound != null)
            {
                ammoExistSound.Play();
            }
        }
    }

    IEnumerator UntilNoAmmo()
    {
        countdownAmmoStart = true;

        Debug.Log("countDownAmmo");

        yield return new WaitForSeconds(timeUntilAmmoBroken);

        Debug.Log("NO ammo");

        noAmmo = true;
    }

    public void AmmoFix()
    {
        noAmmo = false;
        countdownAmmoStart = false;

        door.isAmmobroken = false;
    }

    #endregion

    #region Computer/Chip

    void ComputerBasic()
    {
        if (!countDownComputerStart)
        {
            StartCoroutine(UntilComputerBroken());
        }

        if (computerBroken)
        {
            door.isComputerBroken = true;
            if(chipEffectPause)
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

            chipEffectPause = true;

            chipBrokenVisuals.SetActive(false);

            Material mymat = GetComponent<Renderer>().material;
            mymat.SetColor("_EmissionColor", Color.black);


            foreach (Light AllLights in lightScript.allLightsObject)
            {
                AllLights.intensity = 0;
            }
            foreach (Light AllDoorLights in lightScript.doorLights)
            {
                AllDoorLights.intensity = 0;
            }



            yield return new WaitForSeconds(0.5f);



            mymat.SetColor("_EmissionColor", Color.white);

            foreach (Light AllLights in lightScript.allLightsObject)
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
    }

    public void ComputerFix()
    {
        countDownComputerStart = false;

        chipEffectPause = false;

        computerBroken = false;
        door.isComputerBroken = false;
    }

    #endregion

    #region Energy

    void EnergyBasic()
    {
        if (!countDownEnergyStart)
        {
            StartCoroutine(UntilEnergyBroken());
        }

        if (energyBroken)
        {
            energyBroken = false;
            door.isEnergyBroken = true;

            int WhatToBreak = Random.Range(0, 9);

            SmallEnergyObject[WhatToBreak].gameObject.SetActive(false);
        }
    }

    IEnumerator UntilEnergyBroken()
    {
        countDownEnergyStart = true;

        Debug.Log("countDownEnergy");

        yield return new WaitForSeconds(timeUntilEnergyBroken);

        Debug.Log("NO Energy");

        Material mymat = GetComponentInChildren<Renderer>().material;

        mymat.SetColor("_EmissionColor", Color.red * 2);

        energyBroken = true;
    }

    public void EnergyFix()
    {
        energyBroken = false;
        countDownEnergyStart = false;
        

        Material mymat = GetComponentInChildren<Renderer>().material;
        mymat.SetColor("_EmissionColor", new Color(0, 191, 191, 0) * 0.018f);


        door.isEnergyBroken = false;
    }

    #endregion
}
