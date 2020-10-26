using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldSettings {

	private static Vector3Int worldSize = new Vector3Int(20, 20, 20);
	private static bool wallCollisionMode = true;

	public static Vector3Int GetWorldSize() {
		return worldSize;
	}

	public static bool GetWallCollisionMode() {
		return wallCollisionMode;
	}

	public static void SetWallCollisionMode(bool _b) {
		wallCollisionMode = _b;
	}
}
