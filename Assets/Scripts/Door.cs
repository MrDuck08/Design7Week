using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    StartScrip startScrip;
    ComputerScript computerScript;

    public GameObject HealhtTimeVisuals;

    public float currentDoorhealth = 120f;
    float maxDoorHealth = 120f;

    float currentTimeUntilWin = 370;
    float maxTimeUntilWin = 370f;
    

    public bool isAmmobroken;
    public bool isComputerBroken;
    public bool isEnergyBroken;

    public Image DoorHealhtVisuals;
    public Image TimeLeftVisuals;

    public float y = 0;

    private void Start()
    {
        startScrip = FindObjectOfType<StartScrip>();
        computerScript = FindObjectOfType<ComputerScript>();
    }

    private void Update()
    {
        if(computerScript.tutorialActive == false)
        {
            if (HealhtTimeVisuals.transform.position.y <= 124)
            {
                HealhtTimeVisuals.transform.position += new Vector3(0, 15, 0);
            }
        }
        if (startScrip.winBool || startScrip.died)
        {
            HealhtTimeVisuals.SetActive(false);
        }

        if (isAmmobroken)
        {
            currentDoorhealth -= 0.005f;

            DoorHealhtVisuals.fillAmount = (float)currentDoorhealth / (float)maxDoorHealth;
        }

        if (isComputerBroken)
        {
            currentDoorhealth -= 0.005f;

            DoorHealhtVisuals.fillAmount = (float)currentDoorhealth / (float)maxDoorHealth;
        }

        if (isEnergyBroken)
        {
            currentDoorhealth -= 0.005f;

            DoorHealhtVisuals.fillAmount = (float)currentDoorhealth / (float)maxDoorHealth;
        }

        if (currentDoorhealth < 0)
        {
            Time.timeScale = 0;
            startScrip.Die();
        }
    }
    
    public IEnumerator TimeUntulWinRoutine()
    {
        while (true)
        {


            yield return new WaitForSeconds(1);

            currentTimeUntilWin--;

            TimeLeftVisuals.fillAmount = (float)currentTimeUntilWin / (float)maxTimeUntilWin;

            if (currentTimeUntilWin <= 0)
            {
                startScrip.Win();

                Time.timeScale = 0;

                break;
            }
        }
    }
}
