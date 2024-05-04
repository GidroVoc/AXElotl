using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour
{
	public float maxSpeed = 10f;
	public float jumpForce = 7f;
	bool facingRight = true;
	public float move;
	[SerializeField]
	private bool isGrounded;
	[SerializeField]
	private GameObject groundCheck;
	[SerializeField]
	private LayerMask groundLayer;

	// Use this for initialization
	void Start()
	{
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		Jump();
		Move();
	}
	void Update()
	{
		move = Input.GetAxis("Horizontal");
		isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.02f, groundLayer);
		
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
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight)
			Flip();
		else if (move < 0 && facingRight)
			Flip();
	}

	void Jump()
    {
		

		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded)
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
	}
}	