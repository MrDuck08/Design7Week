using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;


// DO NOT EDIT, will be updated by me (Josef) in the future. Write your own script!
public class Interaction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText = null;
    [SerializeField] TextMeshProUGUI interactText = null;
    [SerializeField] TextMeshProUGUI winText = null;
    [SerializeField] float interactionDistance = 3.0f;
    [SerializeField] AudioSource interactSound = null;

    int finishedObjectives = 0;
    List<GameObject> objectivesTurnedOff;

    void Start()
    {
        objectivesTurnedOff = new List<GameObject>();
        interactText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        objectiveText.text = finishedObjectives.ToString() + "/5 Objectives";
    }

    void Update()
    {
        InteractWithObjective();
    }

    void InteractWithObjective()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {

            if (hit.transform.gameObject.CompareTag("Objective"))
            {

                if (!objectivesTurnedOff.Contains(hit.transform.gameObject))
                {
                    interactText.gameObject.SetActive(true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        TurnOffObjective(hit.transform.gameObject);
                    }
                }
                else
                {
                    interactText.gameObject.SetActive(false);
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void TurnOffObjective(GameObject objective)
    {
        finishedObjectives++;
        objectivesTurnedOff.Add(objective);
        objectiveText.text = finishedObjectives.ToString() + "/5 Objectives";
        
        if (interactSound != null)
        {
            interactSound.Play();
        }

        if (finishedObjectives == 5)
        {
            winText.gameObject.SetActive(true);
        }
    }
}