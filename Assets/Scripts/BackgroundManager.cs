using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Image normalBackground;    // Assign the normal mode background Image
    public Image shadowBackground;    // Assign the shadow mode background Image
    public float fadeDuration = 0.5f; // Duration of the fade transition

    private void Start()
    {
        // Initially, show normal background fully and hide shadow background
        SetAlpha(normalBackground, 1f);
        SetAlpha(shadowBackground, 0f);
    }

    public void SwitchBackground(bool isShadowMode)
    {
        StartCoroutine(FadeBackgrounds(isShadowMode));
    }

    private IEnumerator FadeBackgrounds(bool isShadowMode)
    {
        Image from = isShadowMode ? normalBackground : shadowBackground;
        Image to = isShadowMode ? shadowBackground : normalBackground;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeDuration;
            SetAlpha(from, 1f - alpha); // Fade out current background
            SetAlpha(to, alpha);        // Fade in new background
            yield return null;
        }
        SetAlpha(from, 0f);
        SetAlpha(to, 1f);
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}