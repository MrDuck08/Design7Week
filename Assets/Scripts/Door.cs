using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    float doorHealth = 100f;

    public bool isAmmobroken;
    public bool isComputerBroken;
    public bool isEnergyBroken;

    private void Update()
    {

        if (isAmmobroken)
        {
            doorHealth -= 0.005f;
        }

        if (isComputerBroken)
        {
            doorHealth -= 0.005f;
        }

        if (isEnergyBroken)
        {
            doorHealth -= 0.005f;
        }

        if (doorHealth < 0)
        {
            Time.timeScale = 0;
            Debug.Log("Die");
        }
    }
}
