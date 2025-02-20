using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;

    private UI_Settings uiSettings;
    private UI_MainMenu uiMainMenu;


    private void Awake()
    {
        uiSettings = GetComponentInChildren<UI_Settings>(true);
        uiMainMenu = GetComponentInChildren<UI_MainMenu>(true);

        SwitchTo(uiSettings.gameObject);
        SwitchTo(uiMainMenu.gameObject);
    }

    public void SwitchTo(GameObject uiToEnable)
    {
        foreach (GameObject ui in uiElements)
        {
            ui.SetActive(false);
        }

        uiToEnable.SetActive(true);
    }

    public void QuitButton()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
        
    }
}
