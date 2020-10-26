using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {

	public Transform tailPart;
	private List<Transform> tail;
	private AppleSpawner appleSpawner;
	private float speed = 5f;
	private readonly float speedIncreaseAfterApple = 0.35f;
	private AudioPlayer aPlayer;
	private GameState gameState;

	private void Awake() {
		aPlayer = GameObject.Find("Audio").GetComponent<AudioPlayer>();
		appleSpawner = GameObject.Find("AppleSpawner").GetComponent<AppleSpawner>();
		gameState = GameObject.Find("GameState").GetComponent<GameState>();
		tail = new List<Transform>();
	}

	public float GetSpeed() {
		return speed;
	}

	public void Move(Vector3 _direction) {
		Vector3 prevPosition = transform.position;
		transform.position += _direction;

		bool wallCollision = WallCheck();
		if(wallCollision) {
			transform.position = prevPosition;
			gameState.GameOver();
			return;
		}

		//check apple hit
		Transform apple = appleSpawner.GetCurrentApple();
		if(apple != null) {
			if(apple.transform.position == transform.position) {
				EatApple();
			}
		}

		//last index of tail goes to previous position of head
		if(tail.Count > 0) {
			Transform lastTailPart = tail[tail.Count - 1];
			lastTailPart.transform.position = prevPosition;
			for(int i = tail.Count - 1; i > 0; i--) {
				tail[i] = tail[i - 1];
			}
			tail[0] = lastTailPart;
		}

		if(HitTail()) {
			gameState.GameOver();
		}
	}

	private void EatApple() {
		aPlayer.PlayAppleEatSound();
		appleSpawner.SpawnApple();
		speed += speedIncreaseAfterApple;
		ScoreCounter.AddScore();
		//add one part to tail
		Transform tailPartCopy = Instantiate(tailPart, transform.position, Quaternion.identity);
		tail.Add(tailPartCopy);
	}

	private bool WallCheck() {
		Vector3 pos = transform.position;
		Vector3Int wSize = WorldSettings.GetWorldSize();
		bool wColMode = WorldSettings.GetWallCollisionMode();

		if(pos.x == -1) {
			if(wColMode) { return true; }
			transform.position = new Vector3(wSize.x - 1, pos.y, pos.z);
		} else if(pos.x == wSize.x) {
			if(wColMode) { return true; }
			transform.position = new Vector3(0f, pos.y, pos.z);
		} else if(pos.y == -1) {
			if(wColMode) { return true; }
			transform.position = new Vector3(pos.x,	wSize.y - 1, pos.z);
		} else if(pos.y == wSize.y) {
			if(wColMode) { return true; }
			transform.position = new Vector3(pos.x,	0f,	pos.z);
		} else if(pos.z == -1) {
			if(wColMode) { return true; }
			transform.position = new Vector3(pos.x,	pos.y, wSize.z - 1);
		} else if(pos.z == wSize.z) {
			if(wColMode) { return true; }
			transform.position = new Vector3(pos.x,	pos.y, 0f);
		}

		return false;
	}

	private bool HitTail() {
		for(int i = 0; i < tail.Count; i++) {
			if(transform.position == tail[i].transform.position) {
				return true;
			}
		}

		return false;
	}
}
