using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;    // "f" stands for float, only used for float variables
    public float jumpForce = 10f;   // "public" = anything can see it, even external libraries
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;         // "rb" stands for RigidBody
    private bool isGrounded;        // bool = true or false ; In this case. 3Is the player grounded or not?3

    private Animator animator;      // "private" = only visible within the current code unit

    public int extraJumpsValue = 1;
    private int extraJumps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // We assign the "rb" variable to the "Rigidbody 2D" component in "Player"
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");  // "Horizontal" = left & right arrows (also A & D) ; "Vertical" = up & down arrows (also W & S)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);    // "Vector2" = 2D direction

        if(isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space))     // "GetKeyDown(...)" = Keyboard key pressed
        {
            if(isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // NEW Vector2(), don't forget!
            }
            else if(extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }

        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if(isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if(rb.linearVelocityY > 0)  // 
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }
        }
    }
}
