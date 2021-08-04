using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour {

	// Forhindrer at objektet forsvinner når man bytter scener og passer på at det ikke blir duplisert
	void Awake () {
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		}
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			SceneManager.LoadScene ("Test 1");
		} else if (Input.GetKeyDown (KeyCode.Y)) {
			SceneManager.LoadScene ("Test 2");
		}
	}
}
