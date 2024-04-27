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
    public GameObject SettingCanvas;
    public GameObject Manuel1;
    public GameObject Manuel2;
    public GameObject Manuel3;
    public GameObject Manuel4;
    public GameObject BackOnManuel;


    bool menuIsActive;
    bool controllerCanvasActive;
    bool manualCanvasActive;
    bool settingCanvasActive;

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
            if (!menuIsActive && settingCanvasActive)
            {
                SettingCanvas.SetActive(false);
                Menubasic.SetActive(true);

                settingCanvasActive = false;
                menuIsActive = true;

                return;
            }

            if (!menuIsActive && controllerCanvasActive)
            {
                controllerCanvas.SetActive(false);
                Menubasic.SetActive(true);

                controllerCanvasActive = false;
                menuIsActive = true;

                return;
            }

            if (!menuIsActive && manualCanvasActive)
            {
                Menubasic.SetActive(true);
                ManualsCanvas.SetActive(false);

                menuIsActive = true;
                manualCanvasActive = false;

                return;
            }


            if (!menuIsActive)
            {
                Menubasic.SetActive(true);
                Menu.SetActive(true);

                menuIsActive = true;

                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;

                return;
            }

            if (menuIsActive)
            {
                Menubasic.SetActive(false);
                Menu.SetActive(false);

                menuIsActive = false;

                Time.timeScale = 1f;

                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void FromMenuToManual()
    {

        Menubasic.SetActive(false);
        ManualsCanvas.SetActive(true);

        menuIsActive = false;
        manualCanvasActive = true;

    }

    public void FromMenuToControls()
    {

        Menubasic.SetActive(false);
        controllerCanvas.SetActive(true);

        menuIsActive = false;
        controllerCanvasActive = true;

    }

    public void FromMenuToSettings()
    {

        Menubasic.SetActive(false);
        SettingCanvas.SetActive(true);

        menuIsActive = false;
        settingCanvasActive = true;
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
        SettingCanvas.SetActive(false);

        menuIsActive = true;
        manualCanvasActive = false;
        controllerCanvasActive = false;
        settingCanvasActive = false;
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
