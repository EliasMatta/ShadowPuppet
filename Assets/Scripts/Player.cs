using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

   [SerializeField] private float walkSpeed = 1f;


    private float xAxis;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private float groundCheckX = 0.5f;

    private void Awake()
    {
        
            rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        
        GetInputs();
        Move();
        Jump();

    }


    void GetInputs()
    {

        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {

        rb.linearVelocity = new Vector2(walkSpeed * xAxis, rb.linearVelocity.y);

    }

    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position,Vector2.down, groundCheckY, groundlayer)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, groundlayer)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, groundlayer))
        {

            return true;

        }
        else
        {
            return false;

        }

    }



    void Jump()
    {


        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y >0)
        {


            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);


        }



        if (Input.GetButtonDown("Jump") && Grounded())
        {


            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce);


        }



    }


}