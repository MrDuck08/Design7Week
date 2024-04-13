using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEnergyScript : MonoBehaviour
{
    Interactable interactable;

    public void FixEnergyFromBrokenEnergy()
    {
        interactable = FindObjectOfType<Interactable>();

        interactable.EnergyFix();
    }
}
