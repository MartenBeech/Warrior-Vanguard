using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlefield : MonoBehaviour {
	public GameObject prefab;
	public Transform parent;

    private void Start() {
		CreateBattlefield(2, 8);
    }

    public void CreateBattlefield(int rows, int columns) {
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
				Vector3 pos = new Vector3(x * 125, y * 125);
				Quaternion rot = Quaternion.identity;

				Instantiate(prefab, pos, rot, parent);
			}
        }
        
	}
}
