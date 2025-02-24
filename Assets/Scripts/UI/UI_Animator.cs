using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Animator : MonoBehaviour
{
    public void ChangePosition(Transform transform, Vector3 offset, float duration = .1f)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();

        StartCoroutine(ChangePositionCo(rectTransform, offset, duration));
    }


    public IEnumerator ChangePositionCo(RectTransform rectTransform, Vector3 offset, float duration)
    {
        float time = 0;

        Vector3 initialPosition = rectTransform.anchoredPosition;
        Vector3 targetPosition = initialPosition + offset;

        while (time < duration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(initialPosition, targetPosition, time / duration);
            time += Time.deltaTime;

            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }


    public void ChangeScale(Transform transform, float targetScale, float duration = .25f)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        StartCoroutine(ChangeScaleCo(rectTransform, targetScale, duration));
    }

    public IEnumerator ChangeScaleCo(RectTransform rectTransform, float newScale, float duration = .25f)
    {
        float time = 0;
        Vector3 initialScale = rectTransform.localScale;
        Vector3 targetScale = new Vector3(newScale, newScale, newScale);

        while (time < duration)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    public void ChangeColor(Image image, float targetAlpha, float duration)
    {
        StartCoroutine(ChangeColorCo(image, targetAlpha, duration));
    }

    private IEnumerator ChangeColorCo(Image image, float targetAlpha, float duration)
    {
        float time = 0; // Tracks elapsed time
        Color currentColor = image.color; // Store the image's original color
        float startAlpha = currentColor.a; // Store the initial alpha value

        while (time < duration) // Loop until the duration is reached
        {
            // Calculate the new alpha value based on elapsed time
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            // Update the image color with the new alpha value (red type , green type ,blue type , alpha type)
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            time += Time.deltaTime; // Increment time by the time passed since the last frame
            yield return null; // Wait for the next frame before continuing the loop
        }

        // Ensure the final alpha is set precisely to the target value
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

}
