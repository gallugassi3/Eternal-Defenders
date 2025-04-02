using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    private CameraController camController;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25;


    [Header("SFX Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParameter;
    [SerializeField] private TextMeshProUGUI sfxSliderText;

    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private string bgmParameter;
    [SerializeField] private TextMeshProUGUI bgmSliderText;

    [Header("Keyboard Sensitivity")]
    [SerializeField] private Slider keyboardSensSlider;
    [SerializeField] private string keyboardSensParameter = "KeyboardSens";
    [SerializeField] private TextMeshProUGUI keyboardSensText;

    [SerializeField] private float minKeyboardSens = 60;
    [SerializeField] private float maxKeyboardSens = 240;

    [Header("Mouse Sensitivity")]
    [SerializeField] private Slider mouseSensSlider;
    [SerializeField] private string mouseSensParameter = "MouseSens";
    [SerializeField] private TextMeshProUGUI mouseSensText;

    [SerializeField] private float minMouseSens = 1;
    [SerializeField] private float maxMouseSens = 10; 


    private void Awake()
    {
        camController = FindFirstObjectByType<CameraController>();
    }

    public void SFXSliderValue(float value)
    {
        float newValue = MathF.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(sfxParameter, newValue);

        sfxSliderText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void BGMSliderValue(float value)
    {
        float newValue = MathF.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParameter, newValue);

        bgmSliderText.text = Mathf.Round(value * 100) + "%";
    }

    public void KeyboardSensitivity(float value)
    {
        float newSensitivity = Mathf.Lerp(minKeyboardSens, maxKeyboardSens, value);
        camController.AdjustKeyboardSensitivity(newSensitivity);

        keyboardSensText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void MouseSensitivity(float value)
    {
        float newSensitivity = Mathf.Lerp(minMouseSens, maxMouseSens, value);
        camController.AdjustMouseSensitivity(newSensitivity);

        mouseSensText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(keyboardSensParameter, keyboardSensSlider.value);
        PlayerPrefs.SetFloat(mouseSensParameter, mouseSensSlider.value);
        PlayerPrefs.SetFloat(sfxParameter, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParameter, bgmSlider.value);
    }

    private void OnEnable()
    {
       keyboardSensSlider.value =  PlayerPrefs.GetFloat(keyboardSensParameter , .5f);
       mouseSensSlider.value = PlayerPrefs.GetFloat(mouseSensParameter, .6f);
       sfxSlider.value = PlayerPrefs.GetFloat(sfxParameter, .6f);
       bgmSlider.value = PlayerPrefs.GetFloat(bgmParameter, .6f);
    }
}
