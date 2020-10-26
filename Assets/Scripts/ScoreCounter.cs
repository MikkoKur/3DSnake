using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreCounter {

	private static int score;

	public static int GetScore() {
		return score;
	}

	public static void AddScore() {
		score += 1;
	}

	public static void ResetScore() {
		score = 0;
	}
}
