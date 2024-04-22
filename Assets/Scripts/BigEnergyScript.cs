using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnergyScript : MonoBehaviour
{

    [Header("Energy")]

    public GameObject[] SmallEnergyObject;

    public GameObject TutorialEnergyClipboard;

    public GameObject TestSoundBroken;
    public GameObject TestSoundWorking;

    public bool energyBroken;
    public bool energyTutorial = false;
    public bool energyTutorialActive = false;

    bool countDownEnergyStart;

    float timeUntilEnergyBroken = 55f;

    public float emissionrate = 1.8f;

    public AudioSource EnergyBrokenSound;
    public AudioSource EnergyWorkingSound;

    public bool tutorialActive = true;

    Door door;
    ComputerScript computerScript;
    AmmoScript ammoScript;
    PlayerInteract PlayerInteract;
    StartScrip startScrip;

    private void Start()
    {
        door = FindObjectOfType<Door>();
        computerScript = FindObjectOfType<ComputerScript>();
        ammoScript = FindObjectOfType<AmmoScript>();
        PlayerInteract = FindObjectOfType<PlayerInteract>();
        startScrip = FindObjectOfType<StartScrip>();

        Material mymat = GetComponentInChildren<Renderer>().material;
        mymat.SetColor("_EmissionColor", new Color(0, 191, 191, 0) * 0);

        int WhatToBreak = Random.Range(0, 9);

        SmallEnergyObject[WhatToBreak].gameObject.SetActive(false);

        TestSoundBroken.SetActive(false);
        TestSoundWorking.SetActive(false);

        //EnergyWorkingSound.Stop();
        //EnergyBrokenSound.Stop();
    }

    private void Update()
    {
        if(startScrip.startBool == false)
        {
            EnergyBasic();
        }
    }

    #region Energy

    void EnergyBasic()
    {
        if (energyTutorial)
        {
            if (TutorialEnergyClipboard.transform.position.y <= 250)
            {
                TutorialEnergyClipboard.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (TutorialEnergyClipboard.transform.position.y >= -340)
            {
                TutorialEnergyClipboard.transform.position -= new Vector3(0, 15, 0);
            }
        }

        if (!countDownEnergyStart && !tutorialActive)
        {
            StartCoroutine(UntilEnergyBroken());
        }

        if (energyBroken && !tutorialActive)
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

        TestSoundWorking.SetActive(false);
        TestSoundBroken.SetActive(true);

        //EnergyBrokenSound.Play();
        //EnergyWorkingSound.Stop();
    }

    public void EnergyFix()
    {
        energyBroken = false;
        countDownEnergyStart = false;

        TestSoundWorking.SetActive(true);
        TestSoundBroken.SetActive(false);

        //EnergyWorkingSound.Play();
        //EnergyBrokenSound.Stop();


        Material mymat = GetComponentInChildren<Renderer>().material;
        mymat.SetColor("_EmissionColor", new Color(0, 191, 191, 0) * 0.018f);


        door.isEnergyBroken = false;
    }

    public void EnergyTutorial()
    {
        energyTutorialActive = false;
        energyTutorial = false;

        Debug.Log("WORK");

        ammoScript.ammoTutorialActive = true;
        ammoScript.ammoTutoral = true;

        TestSoundWorking.SetActive(true);

        //EnergyWorkingSound.Play();

        Material mymat = GetComponentInChildren<Renderer>().material;
        mymat.SetColor("_EmissionColor", new Color(0, 191, 191, 0) * 0.018f);
    }

    #endregion
}
