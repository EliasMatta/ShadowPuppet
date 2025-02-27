using System.Collections;
using UnityEngine;

public class ShadowObject : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Collider2D objectCollider;
    private float fadeDuration = 0.5f;



    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();

        SetVisibilityInstant(false);



    }



    public void ToggleVisibility(bool isVisible)
    {


        StopAllCoroutines();
        StartCoroutine(FadeVisibility(isVisible));



    }


    private IEnumerator FadeVisibility(bool isVisible)
    {


        float startAlpha = spriteRenderer.color.a;
        float targetAlpha = isVisible ? 1f : 0f;
        float elapsedTime = 0f;

        while(elapsedTime < fadeDuration)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;



        }


        SetAlpha(targetAlpha);
        objectCollider.enabled = isVisible;


    }


    private void SetAlpha(float alpha)
    {
        Color newColor = spriteRenderer.color;
        newColor.a = alpha;
        spriteRenderer.color = newColor;
    }


    private void SetVisibilityInstant(bool isVisible)
    {

        SetAlpha(isVisible ? 1f : 0f);
        objectCollider.enabled = isVisible;


    }




}
