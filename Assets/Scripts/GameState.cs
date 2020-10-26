using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

	public Controls controls;
	public AppleSpawner appleSpawner;
	public GameEffects gEffects;
	public GameObject snakeHead;
	public UISystem uiSys;
	public AudioPlayer aPlayer;
	private Vector3 snakeStartPos = new Vector3(5f, 3f, 5f);

	public enum State {
		Menu,
		Game,
		GameOver
	}

	public State currentGameState = State.Menu;

	public void GameOver() {
		currentGameState = State.GameOver;
		aPlayer.PlayTailCollideSound();
		gEffects.GameOver();
		uiSys.EnableScoreBoard(true);
	}

	public void StartGame(bool _wallCollisionMode) {
		currentGameState = State.Game;
		WorldSettings.SetWallCollisionMode(_wallCollisionMode);
		GameObject snakeCopy = Instantiate(snakeHead, snakeStartPos, Quaternion.identity);
		appleSpawner.SpawnApple();
		controls.Init(snakeCopy.GetComponent<SnakeHead>());
	}

	public void RestartGame() {
		StartCoroutine(RestartDelayed());
	}

	private IEnumerator RestartDelayed() {
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
