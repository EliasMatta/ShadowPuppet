using System.Collections;
using UnityEngine;

public class ShadowMode : MonoBehaviour
{
    public bool isInShadowMode = false;
    public float shadowOpacity = 0.5f;  // Player opacity in shadow mode
    private SpriteRenderer spriteRenderer;
    private bool canToggleShadowMode = true;
    private float toggleCooldown = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canToggleShadowMode)
        {
            Debug.Log("Toggle Shadow Mode");
            ToggleShadowMode();
        }
    }

    private void ToggleShadowMode()
    {
        if (!canToggleShadowMode) return;

        isInShadowMode = !isInShadowMode;
        StartCoroutine(FadePlayerShadow(isInShadowMode ? shadowOpacity : 1f));

        // Switch backgrounds
        BackgroundManager backgroundManager = FindObjectOfType<BackgroundManager>();
        if (backgroundManager != null)
        {
            backgroundManager.SwitchBackground(isInShadowMode);
        }
        else
        {
            Debug.LogWarning("BackgroundManager not found in the scene!");
        }

        // Toggle all mode objects
        ModeObject[] modeObjects = FindObjectsOfType<ModeObject>();
        foreach (var obj in modeObjects)
        {
            obj.ToggleVisibility(isInShadowMode);
        }

        StartCoroutine(ToggleCooldown());
    }

    private IEnumerator ToggleCooldown()
    {
        canToggleShadowMode = false;
        yield return new WaitForSeconds(toggleCooldown);
        canToggleShadowMode = true;
    }

    private IEnumerator FadePlayerShadow(float targetAlpha)
    {
        float startAlpha = spriteRenderer.color.a;
        float fadeDuration = 0.5f;
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
        Color newColor = spriteRenderer.color;
        newColor.a = alpha;
        spriteRenderer.color = newColor;
    }
}