using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    private UI_Animator uiAnimator;


    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI healthPointsText;
    [SerializeField] private TextMeshProUGUI waveTimerText;

    [SerializeField] private float waveTimerOffset;


    private void Awake()
    {
        uiAnimator = GetComponentInParent<UI_Animator>();
    }

    public void UpdateHealthPointsUI(int value, int maxValue)
    {
        int newValue = maxValue - value;
        healthPointsText.text = "Threat : " + newValue + "/" + maxValue;
    }

    public void UpdateCurrencyUI(int value)
    {
        currencyText.text = "resources : " + value;
    }

    public void UpdateWaveTimerUI(float value) => waveTimerText.text = "seconds : " + value.ToString("00");

    public void EnableWaveTimer(bool enable)
    {
        Transform waveTimerTransform = waveTimerText.transform.parent;

        float yOffset = enable ? -waveTimerOffset : waveTimerOffset;

        Vector3 offset = new Vector3(0, yOffset);


        uiAnimator.ChangePosition(waveTimerTransform, offset);
        //waveTimerTextBlinkEffect.EnableBlink(enable);
    }

    public void ForceWaveButton()
    {
        WaveManager waveManager = FindAnyObjectByType<WaveManager>();
        waveManager.ForceNextWave();
    }
}
