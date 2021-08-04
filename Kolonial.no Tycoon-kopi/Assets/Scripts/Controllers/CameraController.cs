using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script kilde: http://www.studica.com/blog/isometric-camera-unity

public class CameraController : MonoBehaviour {

	// moveSpeed er hvor raskt kameraet skall scrolle på skjermen
	public float moveSpeed = 50f;
	public float maxZoom = 5f;
	public float minZoom = 20f;
	private const int LeftMouse = 0;
	private const int RightMouse = 1;
	private const int MiddleMouse = 2;
	Vector3 forward, right;

	public Camera mainCamera;

	// Setter opp variablene
	void Start () {
		forward = mainCamera.transform.forward;
		forward.y = 0;
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}

	// Sjekker om en key er tastet ned for å sikre at kameraet ikke beveger seg tilfeldig
	void Update () {

		UpdateMouseMovement ();

		UpdateCameraZoom ();

		if (Input.anyKey) {
			UpdateCameraMovement ();
		}
	}

	// Oppdaterer kamera movement
	void UpdateCameraMovement () {
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

		transform.position -= rightMovement;
		transform.position -= upMovement;
	}

	void UpdateMouseMovement () {
		if (!Input.GetMouseButton(RightMouse) && !Input.GetMouseButton(MiddleMouse)) return;
		mainCamera.transform.position -= transform.right * Input.GetAxis("Mouse X");
		mainCamera.transform.position += transform.forward * Input.GetAxis("Mouse X");
		mainCamera.transform.position -= transform.up * Input.GetAxis("Mouse Y") * 2 ;
	}

	void UpdateCameraZoom () {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f && mainCamera.orthographicSize > maxZoom) {
			mainCamera.orthographicSize--;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0f && mainCamera.orthographicSize < minZoom) {
			mainCamera.orthographicSize++;
		}
	}
}