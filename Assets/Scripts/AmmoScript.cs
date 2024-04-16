using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{


    [Header("Ammo")]

    public GameObject TutorialAmmoClipboard;

    public GameObject ObjectNoAmmoSound;
    public GameObject ObjectAmmoSound;

    float timeUntilAmmoBroken = 40f;

    public AudioSource noAmmoSound;
    public AudioSource ammoExistSound;


    public bool ammoFix;
    public bool ammoTutoral = false;
    public bool ammoTutorialActive = false;
    public bool tutorialActive = true;
    bool playSound = true;

    bool noAmmo = false;
    bool countdownAmmoStart;
    bool firstBroken = true;


    Door door;
    ComputerScript computerScript;
    BigEnergyScript bigEnergyScript;
    PlayerInteract playerInteract;
    StartScrip startScrip;


    private void Start()
    {
        door = FindObjectOfType<Door>();
        bigEnergyScript = FindObjectOfType<BigEnergyScript>();
        computerScript = FindObjectOfType<ComputerScript>();
        playerInteract = FindObjectOfType<PlayerInteract>();
        startScrip = FindObjectOfType<StartScrip>();

        countdownAmmoStart = false;
        
        ObjectAmmoSound.SetActive(false);
        ObjectNoAmmoSound.SetActive(false);        
    }

    void Update()
    {
        if(startScrip.startBool == false)
        {
            AmmoBasic();
        }
        
    }


    void AmmoBasic()
    {
        if (ammoTutoral)
        {
            if (TutorialAmmoClipboard.transform.position.y <= 250)
            {
                TutorialAmmoClipboard.transform.position += new Vector3(0, 15, 0);
            }
            playerInteract.ammoDoors.SetActive(false);
        }
        else
        {
            if (TutorialAmmoClipboard.transform.position.y >= -340)
            {
                TutorialAmmoClipboard.transform.position -= new Vector3(0, 15, 0);
            }
        }

        if (!countdownAmmoStart && !tutorialActive) 
        {
           StartCoroutine(UntilNoAmmo());
        }
        
        if(!tutorialActive)
        {
            if (noAmmo)
            {

                ObjectAmmoSound.SetActive(false);
                ObjectNoAmmoSound.SetActive(true);

            }
            else
            {
                ObjectAmmoSound.SetActive(true);
                ObjectNoAmmoSound.SetActive(false);
            }
        }        
    }

    IEnumerator UntilNoAmmo()
    {
        countdownAmmoStart = true;

        Debug.Log("countDownAmmo");

        if (firstBroken)
        {
            timeUntilAmmoBroken = 15;
        }

        yield return new WaitForSeconds(timeUntilAmmoBroken);

        Debug.Log("NO ammo");

        timeUntilAmmoBroken = 40;

        noAmmo = true;
        door.isAmmobroken = true;

        if (firstBroken)
        {
            timeUntilAmmoBroken = 40f;
            firstBroken = false;
        }
    }

    public void AmmoFix()
    {
        noAmmo = false;
        countdownAmmoStart = false;
        ammoTutorialActive = false;

        door.isAmmobroken = false;
    }

    public void AmmoTutorialFix()
    {
        noAmmo = false;
        ammoTutoral = false;
        ammoTutorialActive = false;
        computerScript.ComputerTutorialActive = true;

        ObjectAmmoSound.SetActive(true);
        //ammoExistSoundTutorial.Play();

        computerScript.computerTutorial = true;
    }
}
