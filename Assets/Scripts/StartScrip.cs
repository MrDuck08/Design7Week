using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScrip : MonoBehaviour
{
    public GameObject Player;
    public GameObject MenuHandeler;
    public GameObject Prefabs;
    public GameObject MainMenuCanvas;
    public GameObject StartCamera;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    public GameObject DeathByFallCanvas;

    public bool startBool = true;
    public bool winBool = false;
    public bool died = false;
    bool onlyOnce = true;

    Door door;

    private void Start()
    {
        door = FindObjectOfType<Door>();

        if (startBool)
        {
            Player.SetActive(false);
            Prefabs.SetActive(false);
            MenuHandeler.SetActive(false);
            WinCanvas.SetActive(false);

            MainMenuCanvas.SetActive(true);
            StartCamera.SetActive(true);
        }
    }

    private void Update()
    {
        if (onlyOnce)
        {
            if (startBool)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Player.SetActive(true);
                Prefabs.SetActive(true);
                MenuHandeler.SetActive(true);

                MainMenuCanvas.SetActive(false);
                StartCamera.SetActive(false);

                onlyOnce = false;
            }
        }
    }

    public void Win()
    {
        door.HealhtTimeVisuals.SetActive(false);

        Player.SetActive(false);
        Prefabs.SetActive(false);
        MenuHandeler.SetActive(false);

        WinCanvas.SetActive(true);
        StartCamera.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {
        startBool = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayAgain()
    {
        winBool = false;
        startBool = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;

        Player.SetActive(true);
        Prefabs.SetActive(true);
        MenuHandeler.SetActive(true);

        WinCanvas.SetActive(false);
        StartCamera.SetActive(false);
    }

    public void Die()
    {
        door.HealhtTimeVisuals.SetActive(false);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        died = true;

        Player.SetActive(false);
        Prefabs.SetActive(false);
        MenuHandeler.SetActive(false);

        LoseCanvas.SetActive(true);
        StartCamera.SetActive(true);
    }

    public void DieByFall()
    {
        door.HealhtTimeVisuals.SetActive(false);

        Cursor.lockState = CursorLockMode.None;

        died = true;

        Player.SetActive(false);
        Prefabs.SetActive(false);
        MenuHandeler.SetActive(false);

        DeathByFallCanvas.SetActive(true);
        StartCamera.SetActive(true);
    }
}
