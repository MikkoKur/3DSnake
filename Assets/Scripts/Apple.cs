using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {

	public void Destroy() {
		GetComponent<MeshRenderer>().enabled = false;
		GetComponentInChildren<ParticleSystem>().Play();

		//wait for particle system to finish
		Destroy(gameObject, 1f);
	}
}
