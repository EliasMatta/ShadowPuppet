using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] float weight = 1f;
    [SerializeField] float pushForce = 5f;
    [SerializeField] float deadZone = 0.1f; // Minimum input to register

    Rigidbody2D rb;
    Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = weight;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            float inputDirection = Input.GetAxisRaw("Horizontal");

            // Only process if input exceeds deadzone
            if (Mathf.Abs(inputDirection) < deadZone) return;

            // Calculate push direction based on player's relative position
            float playerToBlockDirection = Mathf.Sign(transform.position.x - playerTransform.position.x);

            // Only push if input matches direction towards the block
            if (Mathf.Sign(inputDirection) == playerToBlockDirection)
            {
                rb.AddForce(Vector2.right * inputDirection * pushForce * Time.deltaTime);
            }
        }
    }
}