using UnityEngine;

public class PlayerController : MonoBehaviour {
	public static PlayerController PlayerSharedInstance;
	public float jumpForce = 25.0f;
	public float runningSpeed = 3.0f;
	private Rigidbody2D rigidBody;
	public LayerMask groundLayerMask;
	public Animator animator;
	private Vector3 startPosition;

	private void Awake() {
		animator.SetBool("isAlive", true);
		PlayerSharedInstance = this;
		rigidBody = GetComponent<Rigidbody2D>();
		startPosition = this.transform.position;
	}

	// Start is called before the first frame update
	public void StartGame() {
		animator.SetBool("isAlive", true);
		transform.position = startPosition;
		rigidBody.velocity = new Vector2(0, 0);
	}

	// Update is called once per frame
	private void Update() {
		//TODO Refactorizar este if y el de fixedUpdate en un método centralizado
		if (GameManager.GMsharedInstance.currentGameState == GameState.InTheGame) {
			if (Input.GetMouseButtonDown (0)) {
				Jump ();
			}
			// El conejico esta en el suelo
			animator.SetBool("isGrounded", IsOnTheFloor());
		}
	}

	//Para añadir constantes a la fisica del juego
	private void FixedUpdate() {
		// Si el estado del juego es "jugando" entonces el moñeco se mueve solo!
		if (GameManager.GMsharedInstance.currentGameState == GameState.InTheGame) {
			//Si la velocidad del conejo en su eje X es menor que la runningSpeed entonces cambia la velocidad
			if (rigidBody.velocity.x < runningSpeed) {
				rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
			}
		}
	}

	private void Jump() {
		if (IsOnTheFloor()) {
			rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	private bool IsOnTheFloor() {
		// Desde donde está el conejo (param1) tira un rayo (param2) de longitud (param3) y mira si el vector toca el
		// suelo (groundLayerMask.value)
		if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.9f, groundLayerMask.value)) {
			return true;
		}
		else {
			return false;
		}
	}

	public void KillPlayer() {
		GameManager.GMsharedInstance.GameOver();
		animator.SetBool("isAlive", false);
	}

	public float GetDistance() {

		float distanceTravelled =
			Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(transform.position.x, 0));
		return distanceTravelled;

	}
}