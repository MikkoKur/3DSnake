using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffects : MonoBehaviour {

	public Camera cam;
	public Material origGridMat;
	public Material redGridMat;
	public Light monitorLight;
	private Color monitorLightOrigColor;
	public GameObject[] gridQuads;

	private void Start() {
		monitorLightOrigColor = monitorLight.color;
	}

	public void GameOver() {
		StartCoroutine(GameOverEffects());
	}

	private IEnumerator GameOverEffects() {
		StartCoroutine(CameraShake());
		ChangeGridMaterial(redGridMat);
		monitorLight.color = new Color(1f, 0, 0);
		yield return new WaitForSeconds(0.5f);
		monitorLight.color = monitorLightOrigColor;
		ChangeGridMaterial(origGridMat);
		yield break;
	}

	private IEnumerator CameraShake() {
		Vector3 origRotation = cam.transform.eulerAngles;
		float rotationAmount = 5f;
		int duration = 12;
		int count = 0;

		while(true) {
			cam.transform.eulerAngles += new Vector3(
				Random.Range(-rotationAmount, rotationAmount),
				Random.Range(-rotationAmount, rotationAmount),
				Random.Range(-rotationAmount, rotationAmount)
			);
			yield return new WaitForSeconds(0.05f);
			cam.transform.eulerAngles = origRotation;
			count++;

			if(count > duration) {
				yield break;
			}
		}
	}

	private void ChangeGridMaterial(Material _m) {
		for(int i = 0; i < gridQuads.Length; i++) {
			gridQuads[i].GetComponent<Renderer>().material = _m;
		}
	}
}
