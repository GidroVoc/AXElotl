using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 13f;
    public float JumpForce = 2f;
    public float DashDistance = 5f;
    public float JumpHoldTime = 0.2f;
    public float DashCooldown = 2f; // Время задержки для dash в секундах

    private float lastDashTime; // Время последнего использования dash
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
        lastDashTime = Time.time - DashCooldown; // Инициализируем так, чтобы dash был доступен сразу при старте
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
        Drop();
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, GroundCheckRadius, groundLayer | 1 << LayerMask.NameToLayer("Platform"));
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + DashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        Vector3 dashPosition = transform.position + (facingRight ? Vector3.right : Vector3.left) * DashDistance;
        rb.MovePosition(dashPosition);
        lastDashTime = Time.time; // Обновляем время последнего использования dash
        yield return new WaitForSeconds(DashCooldown); // Ждем время задержки перед следующим использованием dash
    }

    void Drop()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            StartCoroutine(DropThroughPlatform());
    }

    IEnumerator DropThroughPlatform()
    {
        Debug.Log("DropThroughPlatform called");
        // Игнорировать столкновения между персонажем и платформами
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Platform"), true);
        yield return new WaitForSeconds(0.2f); // Время, в течение которого персонаж может проходить сквозь платформы
                                               // Включить столкновения между персонажем и платформами
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Platform"), false);
    }
}