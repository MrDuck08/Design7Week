using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    public TMP_InputField sensitivityInputField;

    FirstPersonAIO firstPersonAIO;

    void Start()
    {
        firstPersonAIO = FindObjectOfType<FirstPersonAIO>();
    }

    public void ChangeSensetivity()
    {
        int inputFieldValue;
        bool successfulValue = int.TryParse(sensitivityInputField.text, out inputFieldValue);

        if (successfulValue)
        {
            firstPersonAIO.mouseSensitivity = inputFieldValue;
        }
    }
}
