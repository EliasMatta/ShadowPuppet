using System.Collections;
using UnityEngine;

public class PassableObject : MonoBehaviour
{


    private Collider2D objectCollider;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public float fadeDuration = 0.5f;

    private void Awake()
    {
        
        objectCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        
        GameObject player  = GameObject.FindWithTag("Player");
        if (player != null)
        {


            ShadowMode shadowMode = player.GetComponent<ShadowMode>();
           if(shadowMode != null)
            {


                bool isInShadowMode = shadowMode.isInShadowMode;
                objectCollider.enabled = !isInShadowMode;
                rb.constraints = isInShadowMode ? RigidbodyConstraints2D.FreezePositionY : RigidbodyConstraints2D.FreezeRotation;

                StopAllCoroutines();
                StartCoroutine(FadeTo(isInShadowMode ? 0.5f : 1f, fadeDuration));



            }


        }




    }




    private IEnumerator FadeTo(float targetAlpha, float duration)
    {


        Color currentColor = spriteRenderer.color;
        float startAlpha = currentColor.a;
        float time = 0;

        while(time < duration)
        {


            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha,targetAlpha, time / duration);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;

        }


        spriteRenderer.color = new Color (currentColor.r, currentColor.g, currentColor.b, targetAlpha);



    }



}
