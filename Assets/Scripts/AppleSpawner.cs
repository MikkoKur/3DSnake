using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour {

	public Transform apple;
	private Transform currentApple;

	public void SpawnApple() {
		//destroy current apple if there is one
		if(currentApple != null) {
			currentApple.GetComponent<Apple>().Destroy();
		}

		float randX = Random.Range(0, WorldSettings.GetWorldSize().x);
		float randY = Random.Range(0, WorldSettings.GetWorldSize().y);
		float randZ = Random.Range(0, WorldSettings.GetWorldSize().z);
		Vector3 randomPos = new Vector3(randX, randY, randZ);
		currentApple = Instantiate(apple, randomPos, Quaternion.identity);
	}

	public Transform GetCurrentApple() {
		if(currentApple != null) {
			return currentApple;
		}

		return null;
	}
}
