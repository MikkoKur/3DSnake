using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour {

	public GameObject scoreBoard;
	public GameObject arrows;
	public GameObject mainMenu;
	public AudioPlayer audioPlayer;
	public TextMesh scoreText;
	public GameState gameState;
	private bool hardmode = true;
	private bool canRestart = false;

	private void Update() {
		if(gameState.currentGameState == GameState.State.Game) {
			return;
		}

		if(Input.GetKeyDown(KeyCode.Return)) {
			if(gameState.currentGameState == GameState.State.Menu) {
				StartGame(hardmode);
				audioPlayer.PlayButtonSound();
			} else if(gameState.currentGameState == GameState.State.GameOver) {
				if(canRestart) {
					gameState.RestartGame();
					audioPlayer.PlayButtonSound();
					canRestart = false;
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.W) ||
			Input.GetKeyDown(KeyCode.UpArrow) ||
			Input.GetKeyDown(KeyCode.S) ||
			Input.GetKeyDown(KeyCode.DownArrow)) {

			audioPlayer.PlayButtonSound();
			ChangeGameMode();
		}
	}

	private void ChangeGameMode() {
		if(hardmode) {
			hardmode = false;
			arrows.transform.localPosition = new Vector3(0f, -0.26f, 0f);
		} else {
			hardmode = true;
			arrows.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
	}

	public void EnableScoreBoard(bool _b) {
		StartCoroutine(DelayedEnableScoreBoard(_b));
	}

	public void StartGame(bool _hardmode) {
		mainMenu.SetActive(false);
		gameState.StartGame(_hardmode);
	}

	private IEnumerator DelayedEnableScoreBoard(bool _b) {
		yield return new WaitForSeconds(1.5f);
		scoreBoard.SetActive(_b);
		scoreText.text = "SCORE: " + ScoreCounter.GetScore().ToString();
		ScoreCounter.ResetScore();
		canRestart = true;
	}
}
