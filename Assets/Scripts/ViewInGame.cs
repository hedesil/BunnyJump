using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour {
	public Text coinsLabel;
	public Text scoreLabel;

	// Update is called once per frame
	void Update() {
		if (GameManager.GMsharedInstance.currentGameState == GameState.InTheGame) {
			coinsLabel.text = GameManager.GMsharedInstance.collectedCoins.ToString();
			scoreLabel.text = PlayerController.PlayerSharedInstance.GetDistance().ToString("f0");
		}
	}
}