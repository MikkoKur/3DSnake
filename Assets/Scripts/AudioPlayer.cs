using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioClip appleEat;
	public AudioClip hurt;
	public AudioClip buttonSound;
	private AudioSource effectsSource;

	private void Awake() {
		effectsSource = transform.Find("Effects").GetComponent<AudioSource>();
	}

	public void PlayAppleEatSound() {
		effectsSource.PlayOneShot(appleEat, 0.7f);
	}

	public void PlayTailCollideSound() {
		effectsSource.PlayOneShot(hurt);
	}

	public void PlayButtonSound() {
		effectsSource.PlayOneShot(buttonSound, 0.7f);
	}
}
