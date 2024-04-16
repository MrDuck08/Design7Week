using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEnergyScript : MonoBehaviour
{
    BigEnergyScript bigEnergyScript;
    AmmoScript interactable;

    bool tutorial = true;


    #region Energy
    public void FixEnergyFromBrokenEnergy()
    {
        if (tutorial)
        {
            tutorial = false;

            Debug.Log("Work");

            bigEnergyScript = FindObjectOfType<BigEnergyScript>();

            bigEnergyScript.EnergyTutorial();
        }
        else
        {
            bigEnergyScript = FindObjectOfType<BigEnergyScript>();

            bigEnergyScript.EnergyFix();

            Debug.Log("No");
        }
    }

    public void FixEnergyTutorial()
    {
        tutorial = false;

        Debug.Log("Work");

        bigEnergyScript = FindObjectOfType<BigEnergyScript>();

        bigEnergyScript.EnergyTutorial();
    }

    #endregion

    #region Im Lazy

    public void AmmoTutorialTrue()
    {
        interactable = FindObjectOfType<AmmoScript>();

        interactable.ammoTutoral = true;

    }

    #endregion
}
