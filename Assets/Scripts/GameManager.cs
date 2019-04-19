using System;
using UnityEngine;

// 
public enum GameState {
	Menu,
	InTheGame,
	GameOver
}

public class GameManager : MonoBehaviour {
	public static GameManager GMsharedInstance;

	// Se define una variable de tipo GameState para definir el estado del juego (por defecto Menu)
	public GameState currentGameState = GameState.Menu;
	public Canvas menuCanvas;
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;
	public int collectedCoins = 0;

	private void Awake() {
		GMsharedInstance = this;
	}

	// Start is called before the first frame update
	private void Start() {
		// Me aseguro de que siempre que arranque el manager se encuentr en menu, ya que la variable currentGameState es
		// publica
		currentGameState = GameState.Menu;
		menuCanvas.enabled = true;
		gameCanvas.enabled = false;
		gameOverCanvas.enabled = false;
	}


	public void StartGame() {
		PlayerController.PlayerSharedInstance.StartGame();
		LevelGenerator.sharedInstance.GenerateInitialBlocks(3);
		ChangeGameState(GameState.InTheGame);
	}

	// Se llama cuando muere el jugador 
	public void GameOver() {
		LevelGenerator.sharedInstance.RemoveAllBlocks();
		ChangeGameState(GameState.GameOver);
	}

	// Metodo para volver al menu principal
	public void BackToMainMenu() {
		ChangeGameState(GameState.Menu);
	}

	// Metodo encargado de cambiar el estado del juego, acepta por parametro un objeto de tipo GameState
	private void ChangeGameState(GameState newGameState) {
		// Se revisa el valor de newGameState y se actua en consecuencia segun sus valores
		switch (newGameState) {
			// La escena de de Unity deberá mostrar el menú principal
			case GameState.Menu:
				// La escena de Unity deberá mostrar el menú principal
				menuCanvas.enabled = true;
				gameCanvas.enabled = false;
				gameOverCanvas.enabled = false;

				break;
			case GameState.InTheGame:
				// Logica pra cuando se esta jugando
				menuCanvas.enabled = false;
				gameCanvas.enabled = true;
				gameOverCanvas.enabled = false;
				break;
			case GameState.GameOver:
				// Logica para cuando se acaba el juego
				menuCanvas.enabled = false;
				gameCanvas.enabled = false;
				gameOverCanvas.enabled = true;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
		}

		// El valor actual del juego se sustituye por el nuevo valor otorgado por el metodo.
		currentGameState = newGameState;
	}

	public void CollectCoin() {
		collectedCoins++;
	}
}