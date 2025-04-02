using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] private Image fadeImageUI;
    [SerializeField] private GameObject[] uiElements;

    private UI_Settings settingsUI;
    private UI_MainMenu mainMenuUI;

    public UI_InGame inGameUI { get; private set; }

    public UI_Animator uiAnimator { get; private set; }
    public UI_BuildButtonsHolder buildButtonsUI { get; private set; }

    [Header("UI SFX")]
    public AudioSource onHoverSfx;
    public AudioSource onClickSfx;


    private void Awake()
    {
        buildButtonsUI = GetComponentInChildren<UI_BuildButtonsHolder>(true);
        settingsUI = GetComponentInChildren<UI_Settings>(true);
        mainMenuUI = GetComponentInChildren<UI_MainMenu>(true);
        inGameUI = GetComponentInChildren<UI_InGame>(true);
        uiAnimator = GetComponent<UI_Animator>();

        //ActivateFadeEffect(true);

        SwitchTo(settingsUI.gameObject);
        SwitchTo(mainMenuUI.gameObject);
        //SwitchTo(inGameUI.gameObject);

    }

    public void SwitchTo(GameObject uiToEnable)
    {
        foreach (GameObject ui in uiElements)
        {
            ui.SetActive(false);
        }

        if (uiToEnable != null)
        {
            uiToEnable.SetActive(true);
        }
    }

    public void EnableInGameUI(bool enable)
    {
        if (enable)
            SwitchTo(inGameUI.gameObject);
        else
        {
            inGameUI.SnapTimerToDefaultPosition();
            SwitchTo(null);
        }
    }

    public void EnableMainMenuUI(bool enable)
    {
        if (enable)
            SwitchTo(mainMenuUI.gameObject);
        else
            SwitchTo(null);
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
