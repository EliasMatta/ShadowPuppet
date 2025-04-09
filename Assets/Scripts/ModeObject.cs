using System.Collections;
using UnityEngine;

public class ModeObject : MonoBehaviour
{
    public bool isShadow; // Set to true for shadow objects, false for normal objects
    private SpriteRenderer spriteRenderer;
    private Collider2D objectCollider;
    public float fadeDuration = 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
        // Initially, normal objects are visible, shadow objects are hidden
        bool initialVisible = !isShadow;
        SetVisibility(initialVisible, instant: true);
    }

    public void ToggleVisibility(bool isInShadowMode)
    {
        // Visible if the object's mode matches the current mode
        bool shouldBeVisible = (isShadow == isInShadowMode);
        StartCoroutine(FadeObject(shouldBeVisible));
        objectCollider.enabled = shouldBeVisible;
    }

    private IEnumerator FadeObject(bool fadeIn)
    {
        float startAlpha = spriteRenderer.color.a;
        float targetAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    private void SetVisibility(bool isVisible, bool instant = false)
    {
        if (instant)
        {
            SetAlpha(isVisible ? 1f : 0f);
        }
        else
        {
            StartCoroutine(FadeObject(isVisible));
        }
        objectCollider.enabled = isVisible;
    }
}