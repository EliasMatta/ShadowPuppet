using UnityEngine; 

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f; // How far in front of the player we check for grab objects
    public float grabRadius = 0.3f; // Radius of the overlap circle used to detect nearby objects

    GameObject box; // Variable to store the currently grabbed box
    bool isGrabbing = false; // Whether the player is currently grabbing a box or not

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E)) // If the player presses the E key this frame
        {
            if (!isGrabbing) // If we're not already grabbing something
            {
                // Calculate the position to check in front of the player
                Vector2 origin = (Vector2)transform.position + Vector2.right * transform.localScale.x * distance * 0.5f;

                // Check for a collider in a circle at the grab point
                Collider2D hit = Physics2D.OverlapCircle(origin, grabRadius);

                if (hit != null) // If we hit something
                {
                    box = hit.gameObject; // Store the object we hit as the box

                    // Enable the box's FixedJoint2D and connect it to the player
                    box.GetComponent<FixedJoint2D>().enabled = true;
                    box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();

                    isGrabbing = true; // Mark that we're now grabbing
                }
            }
            else // If we're already grabbing something
            {
                // Disable the joint and disconnect the box
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<FixedJoint2D>().connectedBody = null;

                box = null; // Clear the reference to the box
                isGrabbing = false; // Mark that we're no longer grabbing
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red; 

        // Calculate the center of the circle we’ll draw
        Vector3 center = transform.position + Vector3.right * transform.localScale.x * distance * 0.5f;

        Gizmos.DrawLine(transform.position, center); // Draw a line from the player to the grab point
        Gizmos.DrawWireSphere(center, grabRadius); // Draw the grab detection circle
    }
}
