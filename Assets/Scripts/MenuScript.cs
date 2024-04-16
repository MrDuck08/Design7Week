using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Menu;

    public GameObject Menubasic;
    public GameObject controllerCanvas;
    public GameObject ManualsCanvas;
    public GameObject Manuel1;
    public GameObject Manuel2;
    public GameObject Manuel3;
    public GameObject Manuel4;
    public GameObject BackOnManuel;

    bool MenuIsActive;
    bool controllerCanvasActive;
    bool ManualCanvasActive;

    private void Start()
    {
        if(Menu != null && Menubasic != null && ManualsCanvas != null && controllerCanvas != null)
        {
            Menu.SetActive(false);
            Menubasic.SetActive(false);
            ManualsCanvas.SetActive(false);
            controllerCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        MenuAnywhere();
    }

    #region Menus

    void MenuAnywhere()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!MenuIsActive)
            {
                Menubasic.SetActive(true);
                Menu.SetActive(true);

                MenuIsActive = true;

                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;

                return;
            }

            if (MenuIsActive)
            {
                Menubasic.SetActive(false);
                Menu.SetActive(false);

                MenuIsActive = false;

                Time.timeScale = 1f;

                Cursor.lockState = CursorLockMode.Locked;
            }

            if (!MenuIsActive && controllerCanvasActive)
            {
                controllerCanvas.SetActive(false);
                Menubasic.SetActive(true);

                controllerCanvasActive = false;
                MenuIsActive = true;
            }

            if(MenuIsActive && ManualCanvasActive)
            {
                Menubasic.SetActive(true);
                ManualsCanvas.SetActive(false);

                MenuIsActive = true;
                ManualCanvasActive = false;
            }
        }
    }

    public void FromMenuToManual()
    {

        Menubasic.SetActive(false);
        ManualsCanvas.SetActive(true);

        MenuIsActive = false;
        ManualCanvasActive = true;

    }

    public void FromMenuToControls()
    {

        Menubasic.SetActive(false);
        controllerCanvas.SetActive(true);

        MenuIsActive = false;
        controllerCanvasActive = true;

    }

    public void WhatManualToShow(GameObject WhatToShow)
    {

        Manuel1.SetActive(false);
        Manuel2.SetActive(false);
        Manuel3.SetActive(false);
        Manuel4.SetActive(false);
        BackOnManuel.SetActive(false);

        WhatToShow.SetActive(true);

    }

    public void GoBackToManuals(GameObject WhatToStopShowing)
    {

        Manuel1.SetActive(true);
        Manuel2.SetActive(true);
        Manuel3.SetActive(true);
        Manuel4.SetActive(true);
        BackOnManuel.SetActive(true);

        WhatToStopShowing.SetActive(false);
    }

    public void startAgain()
    {
        Menubasic.SetActive(false);
        Time.timeScale = 1.0f;
        Menu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenu()
    {
        Menubasic.SetActive(true);
        controllerCanvas.SetActive(false);
        ManualsCanvas.SetActive(false);

        MenuIsActive = true;
        ManualCanvasActive = false;
        controllerCanvasActive = false;
    }

    #endregion

    #region SceneLoader

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void LoadScene(int whatScene)
    {
        SceneManager.LoadScene(whatScene);
    }

    #endregion
}
