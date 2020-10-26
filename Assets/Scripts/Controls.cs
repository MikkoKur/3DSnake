using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public GameState gameState;
	private Vector3 currentDirection = Vector3.up;
	private Vector3 changedDirection = Vector3.up;
	private SnakeHead snakeHead;
	private Transform camPointFront;
	private Transform camPointTop;
	private Transform camPointRight;
	private bool init = false;

	private enum LookDirection {Front, Right, Top};
	private LookDirection lookDir;

	private void Start() {
		lookDir = LookDirection.Front;
		camPointFront = GameObject.Find("Grid2020/CameraPoints/CamPointFront").transform;
		camPointTop = GameObject.Find("Grid2020/CameraPoints/CamPointTop").transform;
		camPointRight = GameObject.Find("Grid2020/CameraPoints/CamPointRight").transform;
	}

	public void Init(SnakeHead _snakeHead) {
		snakeHead = _snakeHead;
		init = true;
		StartCoroutine(SnakeUpdate());
	}

	private void Update() {
		if(gameState.currentGameState == GameState.State.Game && init) {
			KeyPresses();
		}
	}

	private void KeyPresses() {
		//change snake direction
		if(Input.GetKeyDown(KeyCode.W)) {
			if(lookDir == LookDirection.Front || lookDir == LookDirection.Right) {
				changedDirection = Vector3.up;
			} else if(lookDir == LookDirection.Top) {
				changedDirection = Vector3.forward;
			}
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			if(lookDir == LookDirection.Front || lookDir == LookDirection.Right) {
				changedDirection = Vector3.down;
			} else if(lookDir == LookDirection.Top) {
				changedDirection = Vector3.back;
			}
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			if(lookDir == LookDirection.Front || lookDir == LookDirection.Top) {
				changedDirection = Vector3.left;
			} else if(lookDir == LookDirection.Right) {
				changedDirection = Vector3.back;
			}
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			if(lookDir == LookDirection.Front || lookDir == LookDirection.Top) {
				changedDirection = Vector3.right;
			} else if(lookDir == LookDirection.Right) {
				changedDirection = Vector3.forward;
			}
		}

		//rotate view
		if(Input.GetKeyDown(KeyCode.UpArrow) && lookDir != LookDirection.Top) {
			StartCoroutine(MoveCamera(camPointTop));
			lookDir = LookDirection.Top;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow) && lookDir != LookDirection.Front) {
			StartCoroutine(MoveCamera(camPointFront));
			lookDir = LookDirection.Front;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow) && lookDir != LookDirection.Front) {
			StartCoroutine(MoveCamera(camPointFront));
			lookDir = LookDirection.Front;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow) && lookDir != LookDirection.Right) {
			StartCoroutine(MoveCamera(camPointRight));
			lookDir = LookDirection.Right;
		}
	}

	private IEnumerator MoveCamera(Transform _endTransform) {
		Quaternion startRotation = transform.rotation;
		Quaternion endRotation = _endTransform.rotation;
		Vector3 startPos = transform.position;
		Vector3 endPos = _endTransform.position;
		float speed = 3f;
		float movement = 0f;

		while(true) {
			movement += Time.deltaTime * speed;
			transform.rotation = Quaternion.Slerp(startRotation, endRotation, movement);
			transform.position = Vector3.Lerp(startPos, endPos, movement);

			if(transform.rotation == endRotation && transform.position == endPos) {
				break;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator SnakeUpdate() {
		while(true) {
			if(gameState.currentGameState == GameState.State.GameOver) { break; }

			//prevent opposite direction change
			if(changedDirection * -1 != currentDirection) {
				currentDirection = changedDirection;
			}

			snakeHead.Move(currentDirection);
			yield return new WaitForSeconds(1f / snakeHead.GetSpeed());
		}
	}
}
