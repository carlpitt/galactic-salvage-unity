using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float jumpPower;
	[SerializeField] private LayerMask groundLayer;
	private Rigidbody2D body;
	private BoxCollider2D boxCollider;
	private float horizontalInput;

	private void Awake() {
		body = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	private void Update() {
		horizontalInput = Input.GetAxis("Horizontal");

		body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

		// flip player when moving left-right
		if (horizontalInput > 0.01F) {
			transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);
		} else if (horizontalInput < -0.01F) {
			transform.localScale = new Vector3(-0.25F, 0.25F, 0.25F);

		}

		body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

		body.gravityScale = 7;

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			Jump();
		}
	}

	private void Jump() {
		if (isGrounded()) {
			body.velocity = new Vector2(body.velocity.x, jumpPower);
		}
	}

	private bool isGrounded() {
		RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1F, groundLayer);
		return raycastHit.collider != null;
	}

	public bool canAttack() {
		// return horizontalInput == 0 && isGrounded();
		return true;
	}
}
