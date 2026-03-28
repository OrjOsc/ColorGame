using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;    // "f" stands for float, only used for float variables
    public float jumpHeight = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;   // "public" = anything can see it, even external libraries

    private Rigidbody2D body;         // "private" = only visible within the current code unit
    private bool isGrounded;        // bool = true or false ; In this case: "Is the player grounded or not?"

    private Animator animator;

    public int extraJumpsValue = 1;
    private int extraJumps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();   // We assign the "body" variable to the "Rigidbody 2D" component in "Player"
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");  // "Horizontal" = left & right arrows (also A & D) ; "Vertical" = up & down arrows (also W & S)
        body.linearVelocity = new Vector2(moveInput * moveSpeed, body.linearVelocity.y);    // "Vector2" = 2D direction

        if(isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space))     // "GetKeyDown(...)" = Keyboard key pressed
        {
            if(isGrounded)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight); // NEW Vector2(), don't forget!
            }
            else if(extraJumps > 0)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight);
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
            if(body.linearVelocityY > 0)  // 
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
