using UnityEngine;
using System.Collections;

public class AxelotelCharacterController : MonoBehaviour
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

    [SerializeField]
    private float wallJumpForce = 10f; // сила отскока от стены
    [SerializeField]
    private Transform wallCheck; // точка дл€ проверки стены
    [SerializeField]
    private float wallCheckDistance = 0.5f; // рассто€ние дл€ проверки стены
    [SerializeField]
    private LayerMask wallLayer; // слой стены
    private bool isTouchingWall; // персонаж касаетс€ стены

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.02f, groundLayer);
        
        WallJump();
        Dash();
        Jump();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Move()
    {
        rb.velocity = new Vector2(move * MaxSpeed, rb.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(JumpForce * -2f * Physics2D.gravity.y));
            jumpHoldTimer = JumpHoldTime;
            isJumping = true;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && jumpHoldTimer > 0 && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(JumpForce * -2f * Physics2D.gravity.y));
            jumpHoldTimer -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dashPosition = transform.position + (facingRight ? Vector3.right : Vector3.left) * DashDistance;
            rb.MovePosition(dashPosition);
        }
    }

    void WallJump()
    {
        // ѕровер€ем, касаетс€ ли персонаж стены
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wallLayer);

        // ≈сли персонаж касаетс€ стены и нажата кнопка прыжка, то выполн€ем отскок от стены
        if (isTouchingWall && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            rb.AddForce(new Vector2(wallJumpForce * -move, 0), ForceMode2D.Impulse);
        }
    }
}
