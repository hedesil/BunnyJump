using UnityEngine;

public class Collectable : MonoBehaviour {
	private bool isCollected = false;

	void ShowCoin() {
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<CircleCollider2D>().enabled = true;
		isCollected = false;
	}

	void HideCoin() {
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<CircleCollider2D>().enabled = false;
	}

	void CollectCoin() {
		isCollected = true;
		HideCoin();
		// Notificar al manager del juego que hemos recogido una moneda...
		GameManager.GMsharedInstance.CollectCoin();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			CollectCoin();
		}
	}
}