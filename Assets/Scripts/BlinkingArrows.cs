using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingArrows : MonoBehaviour {

	private GameObject[] arrows;

	private void Start() {
		arrows = new GameObject[2];
		arrows[0] = transform.GetChild(0).gameObject;
		arrows[1] = transform.GetChild(1).gameObject;
		StartCoroutine(BlinkingArrowsAnim());
	}

	private IEnumerator BlinkingArrowsAnim() {
		while(true) {
			arrows[0].SetActive(false);
			arrows[1].SetActive(false);
			yield return new WaitForSeconds(0.3f);
			arrows[0].SetActive(true);
			arrows[1].SetActive(true); 
			yield return new WaitForSeconds(0.3f);
		}
	}
}
