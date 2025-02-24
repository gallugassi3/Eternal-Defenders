using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] private Image fadeImageUI;
    [SerializeField] private GameObject[] uiElements;

    private UI_Animator uiAnimator;
    private UI_Settings settingsUI;
    private UI_MainMenu mainMenuUI;
    private UI_InGame inGameUI;


    private void Awake()
    {
        settingsUI = GetComponentInChildren<UI_Settings>(true);
        mainMenuUI = GetComponentInChildren<UI_MainMenu>(true);
        inGameUI = GetComponentInChildren<UI_InGame>(true);
        uiAnimator = GetComponent<UI_Animator>();

        //ActivateFadeEffect(true);

        SwitchTo(settingsUI.gameObject);
        //SwitchTo(mainMenuUI.gameObject);
        SwitchTo(inGameUI.gameObject);

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

    public void ActivateFadeEffect(bool fadeIn)
    {
        if (fadeIn)
        {
            uiAnimator.ChangeColor(fadeImageUI, 0, 2);
        }
        else
        {
            uiAnimator.ChangeColor(fadeImageUI, 1, 2);
        }
    }
}
