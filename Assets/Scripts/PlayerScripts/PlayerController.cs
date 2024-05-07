using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 13f;
    public float JumpForce = 2f;
    public float DashDistance = 5f;
    public float JumpHoldTime = 0.2f;

    private bool facingRight = true;
    private float move;
    private bool isGrounded;
    private float jumpHoldTimer;
    [SerializeField]
    private GameObject groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isJumping;
    private const float GroundCheckRadius = 0.02f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        CheckIfCharacterIsGrounded();
        PerformDash();
        PerformJump();
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void MoveCharacter()
    {
        rb.velocity = new Vector2(move * MaxSpeed, rb.velocity.y);
        if (move > 0 && !facingRight)
            FlipCharacter();
        else if (move < 0 && facingRight)
            FlipCharacter();
    }

    void CheckIfCharacterIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, GroundCheckRadius, groundLayer);
    }

    void PerformJump()
    {
        if (ShouldStartJump())
            StartJump();

        if (ShouldContinueJump())
            ContinueJump();

        if (ShouldStopJump())
            StopJump();
    }

    bool ShouldStartJump()
    {
        return (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isJumping;
    }

    void StartJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(JumpForce * -2f * Physics2D.gravity.y));
        jumpHoldTimer = JumpHoldTime;
        isJumping = true;
    }

    bool ShouldContinueJump()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && jumpHoldTimer > 0 && isJumping;
    }

    void ContinueJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(JumpForce * -2f * Physics2D.gravity.y));
        jumpHoldTimer -= Time.deltaTime;
    }

    bool ShouldStopJump()
    {
        return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    }

    void StopJump()
    {
        isJumping = false;
    }

    void PerformDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dashPosition = transform.position + (facingRight ? Vector3.right : Vector3.left) * DashDistance;
            rb.MovePosition(dashPosition);
        }
    }
}
