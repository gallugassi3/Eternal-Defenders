using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildButtons : MonoBehaviour
{
    private UI_Animator uiAnimator;


    [SerializeField] private float yPositionOffset;

    private bool isBuildMenuActive;

    private void Awake()
    {
        uiAnimator = GetComponentInParent<UI_Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ShowBuildButtons();
    }

    public void ShowBuildButtons()
    {
        isBuildMenuActive = !isBuildMenuActive;

        float yOffset = isBuildMenuActive ? yPositionOffset : -yPositionOffset;
        //float methodDelay = isBuildMenuActive ? openAnimationDuration : 0;

        Vector3 offset = new Vector3(0, yOffset);

        uiAnimator.ChangePosition(transform, offset);
        //Invoke(nameof(ToggleButtonMovement), methodDelay);
    }

}
