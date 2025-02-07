using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushForce = 10f; // How hard the player can push

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 pushDirection = new Vector2(collision.transform.localScale.x, 0); 
            rb.linearVelocity = pushDirection * pushForce * Time.deltaTime;
        }
    }
}
