using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerInteract : MonoBehaviour
{
    #region Masks

    [Header("mask")]

    public LayerMask LightButtonMask;
    public LayerMask AmmoBoxMask;
    public LayerMask AmmoCartMask;
    public LayerMask ChipMask;
    public LayerMask ComputerMask;
    public LayerMask EnergyWorkingMask;
    public LayerMask EnergyBrokenMask;
    public LayerMask BigEnergyMask;
    public LayerMask FallMask;

    public LayerMask InteractibleMask;

    #endregion

    #region Energy

    [Header("Energy")]

    public GameObject EnergyWorkingObject;

    bool isHoldingEnergy;

    public GameObject ClipboardEnergy;
    public GameObject clipboardBigEnergy;


    #endregion

    #region Ammo Box

    [Header("Ammo")]

    public GameObject ammoBoxObject;
    public GameObject ammoDoors;
    public GameObject ammoCartBox;

    bool isHoldingAmmoBox;

    public GameObject clipboardAmmo;

    #endregion

    #region Light

    [Header("Light")]

    public GameObject ClipboardLight;

    #endregion

    #region Chip/Computer

    [Header("Chip")]

    public GameObject holdingChipObject;

    public GameObject clioboardChip;
    public GameObject clipboardComputer;

    bool isHoldingChip;

    #endregion

    #region Genreral

    float interactRange = 3;

    bool isHoldingAnything;
    bool controllsTutorialActive = true;

    Door door;
    ComputerScript computerScript;
    AmmoScript ammoScript;
    BigEnergyScript bigEnergyScript;
    StartScrip startScrip;
    Lights LightsScript;

    public GameObject ControllsTutorial;

    #endregion

    private void Start()
    {
        door = FindObjectOfType<Door>();
        computerScript = FindObjectOfType<ComputerScript>();
        ammoScript = FindObjectOfType<AmmoScript>();
        bigEnergyScript = FindObjectOfType<BigEnergyScript>();
        startScrip = FindObjectOfType<StartScrip>();
        LightsScript = FindObjectOfType<Lights>();
    }

    void Update()
    {
        Interact();

        if (door.isAmmobroken)
        {
            ammoDoors.SetActive(false);
        }

        if (startScrip.died)
        {
            ammoScript.TutorialAmmoClipboard.SetActive(false);
            bigEnergyScript.TutorialEnergyClipboard.SetActive(false);
            computerScript.TutorialComputerClipboard.SetActive(false);
        }

        if(controllsTutorialActive)
        {
            if (ControllsTutorial.transform.position.y <= 250)
            {
                ControllsTutorial.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (ControllsTutorial.transform.position.y >= -340)
            {
                ControllsTutorial.transform.position -= new Vector3(0, 15, 0);
            }
        }
    }

    void Interact()
    {
        Ray WeaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();

        #region Ammo
        if (Physics.Raycast(WeaponRay, out hit, interactRange, AmmoBoxMask) && !isHoldingAmmoBox && !isHoldingAnything)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ammoBoxObject.SetActive(true);

                isHoldingAmmoBox = true;
                isHoldingAnything = true;
            }

            if (clipboardAmmo.transform.position.y <= 250)
            {
                clipboardAmmo.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (clipboardAmmo.transform.position.y >= -340)
            {
                clipboardAmmo.transform.position -= new Vector3(0, 15, 0);
            }
        }

        if (Physics.Raycast(WeaponRay, out hit, interactRange, AmmoCartMask) && isHoldingAmmoBox)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ammoBoxObject.SetActive(false);
                ammoCartBox.SetActive(true);

                isHoldingAmmoBox = false;
                isHoldingAnything = false;

                StartCoroutine(ammoDoorCloseRoutine());
            }
        }
        #endregion

        #region Light

        if (Physics.Raycast(WeaponRay, out hit, interactRange, LightButtonMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (LightsScript.tutortialActive)
                {
                    hit.collider.GetComponent<Lights>().RestoreLightTutorial();
                }
                else
                {
                    hit.collider.GetComponent<Lights>().RestoreLight();
                }

            }

            if (ClipboardLight.transform.position.y <= 250)
            {
                ClipboardLight.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (ClipboardLight.transform.position.y >= -340)
            {
                ClipboardLight.transform.position -= new Vector3(0, 15, 0);
            }
        }

        #endregion

        #region Energy

        if (Physics.Raycast(WeaponRay, out hit, interactRange, EnergyWorkingMask) && !isHoldingEnergy && !isHoldingAnything)
        {
            if (Input.GetMouseButtonDown(0))
            {
                EnergyWorkingObject.SetActive(true);

                isHoldingAnything = true;
                isHoldingEnergy = true;
            }

            if (ClipboardEnergy.transform.position.y <= 250)
            {
                ClipboardEnergy.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (ClipboardEnergy.transform.position.y >= -340)
            {
                ClipboardEnergy.transform.position -= new Vector3(0, 15, 0);
            }
        }


        if (Physics.Raycast(WeaponRay, out hit, interactRange))
        {
            if(hit.collider.tag == "EnergyBroken" && isHoldingEnergy)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    EnergyWorkingObject.SetActive(false);

                    hit.collider.gameObject.transform.GetChild(4).gameObject.SetActive(true);

                    isHoldingAnything = false;
                    isHoldingEnergy = false;


                    door.isEnergyBroken = false;

                    if (bigEnergyScript.energyTutorialActive)
                    {
                        bigEnergyScript.EnergyTutorial();
                    }
                    else
                    {
                        bigEnergyScript.EnergyFix();
                    }                    
                }
            }
        }

        if(Physics.Raycast(WeaponRay, out hit, interactRange, BigEnergyMask) && !isHoldingAnything)
        {

            if (clipboardBigEnergy.transform.position.y <= 250)
            {
                clipboardBigEnergy.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (clipboardBigEnergy.transform.position.y >= -340)
            {
                clipboardBigEnergy.transform.position -= new Vector3(0, 15, 0);
            }
        }

        #endregion

        #region Chip/Computer

        if (Physics.Raycast(WeaponRay, out hit, interactRange, ChipMask) && !isHoldingChip && !isHoldingAnything)
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingChipObject.SetActive(true);

                isHoldingAnything = true;
                isHoldingChip = true;
            }

            if (clioboardChip.transform.position.y <= 250)
            {
                clioboardChip.transform.position += new Vector3(0, 15, 0);
            }
        }
        else
        {
            if (clioboardChip.transform.position.y >= -340)
            {
                clioboardChip.transform.position -= new Vector3(0, 15, 0);
            }
        }

        if (Physics.Raycast(WeaponRay, out hit, interactRange, ComputerMask))
        {
            if(isHoldingChip)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    holdingChipObject.SetActive(false);

                    door.isComputerBroken = false;

                    computerScript.ComputerFix();

                    isHoldingChip = false;
                    isHoldingAnything = false;
                }
            }

            if (!isHoldingAnything)
            {
                if (clipboardComputer.transform.position.y <= 250)
                {
                    clipboardComputer.transform.position += new Vector3(0, 15, 0);
                }
            }
        }
        else
        {
            if(!isHoldingChip)
            {
                if (clipboardComputer.transform.position.y >= -340)
                {
                    clipboardComputer.transform.position -= new Vector3(0, 15, 0);
                }
            }
        }

        #endregion

        #region DropItem

        if (Input.GetMouseButtonDown(1))
        {
            if (isHoldingAmmoBox)
            {
                ammoBoxObject.SetActive(false);
                ammoDoors.SetActive(true);               

                isHoldingAmmoBox = false;
                isHoldingAnything = false;

                if (computerScript.ComputerTutorialActive && computerScript.tutorialActive)
                {
                    computerScript.computerTutorial = true;
                }
                if (bigEnergyScript.energyTutorialActive && bigEnergyScript.tutorialActive)
                {
                    bigEnergyScript.energyTutorial = true;
                }
                if (ammoScript.ammoTutorialActive && ammoScript.tutorialActive)
                {
                    ammoScript.ammoTutoral = true;
                }
            }

            if (isHoldingEnergy)
            {
                EnergyWorkingObject.SetActive(false);

                isHoldingEnergy = false;
                isHoldingAnything = false;

                if (computerScript.ComputerTutorialActive && computerScript.tutorialActive)
                {
                    computerScript.computerTutorial = true;
                }
                if (bigEnergyScript.energyTutorialActive && bigEnergyScript.tutorialActive)
                {
                    bigEnergyScript.energyTutorial = true;
                }
                if (ammoScript.ammoTutorialActive && ammoScript.tutorialActive)
                {
                    ammoScript.ammoTutoral = true;
                }
            }

            if (isHoldingChip)
            {
                holdingChipObject.SetActive(false);

                isHoldingChip = false;
                isHoldingAnything = false;

                if (computerScript.ComputerTutorialActive && computerScript.tutorialActive)
                {
                    computerScript.computerTutorial = true;
                }
                if (bigEnergyScript.energyTutorialActive && bigEnergyScript.tutorialActive)
                {
                    bigEnergyScript.energyTutorial = true;
                }
                if (ammoScript.ammoTutorialActive && ammoScript.tutorialActive)
                {
                    ammoScript.ammoTutoral = true;
                }
            }
        }

        #endregion

        if(Physics.Raycast(WeaponRay, out hit, interactRange, InteractibleMask) && !isHoldingAnything)
        {
            if (computerScript.computerTutorial)
            {
                computerScript.computerTutorial = false;
            }
            if (bigEnergyScript.energyTutorial)
            {
                bigEnergyScript.energyTutorial = false;
            }
            if (ammoScript.ammoTutoral)
            {
                ammoScript.ammoTutoral = false;
            }
            if (LightsScript.lightTutorial)
            {
                LightsScript.lightTutorial = false;
            }
        }
        else
        {
            if (computerScript.ComputerTutorialActive && computerScript.tutorialActive && !isHoldingAnything)
            {
                computerScript.computerTutorial = true;
            }
            if (bigEnergyScript.energyTutorialActive && bigEnergyScript.tutorialActive && !isHoldingAnything)
            {
                bigEnergyScript.energyTutorial = true;
            }
            if (ammoScript.ammoTutorialActive && ammoScript.tutorialActive && !isHoldingAnything)
            {
                ammoScript.ammoTutoral = true;
            }
            if(LightsScript.lightTutorialActive && LightsScript.tutortialActive && !isHoldingAnything)
            {
                LightsScript.lightTutorial = true;
            }
        }
    }

    IEnumerator ammoDoorCloseRoutine()
    {
        yield return new WaitForSeconds(1f);

        ammoDoors.SetActive(true);
        ammoCartBox.SetActive(false);

        if (ammoScript.ammoTutoral)
        {
            ammoScript.AmmoTutorialFix();
        }
        else
        {
            ammoScript.AmmoFix();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 14)
        {
            Debug.Log("DIED");
            startScrip.DieByFall();
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 15)
        {
            controllsTutorialActive = false;

            LightsScript.lightTutorial = true;
            LightsScript.lightTutorialActive = true;
        }
    }
}
