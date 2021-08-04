using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class MouseController : MonoBehaviour {

	public GameObject CircleCursorPrefab;

	private bool _buildModeIsObjects = false;
	
	private Tile.TileType _buildModeTile;
	private string _buildModeObjectType;

	private const int LeftMouse = 0;
	private const int RightMouse = 1;
	private const int MiddleMouse = 2;

	private Vector3 _dragStartPosition;
	private List<GameObject> _dragPreviewGameObjects;
	
	// Use this for initialization
	void Start () {
		//Cursor.visible = false;
		_dragPreviewGameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
		// Find cursor position using raycast to plane matching tile-grid
		var currMousePosition = MousePosition();

		if (Input.GetKey(KeyCode.C)) {
			Debug.Log(Mathf.FloorToInt(currMousePosition.x) + "," + Mathf.FloorToInt(currMousePosition.z));
		}

		//UpdateCursor(currMousePosition);
		UpdateCameraMovement();
		UpdateZoom();
		UpdateDragSelection(currMousePosition);
		
	}

	private static Vector3 MousePosition() {
		var plane = new Plane(Vector3.up, 0);
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float dist;
		plane.Raycast(ray, out dist);
		var currMousePosition = ray.GetPoint(dist);

		return currMousePosition;
	}

	/*private void UpdateCursor(Vector3 currMousePosition) {
		var tileUnderMouse = WorldController.GetTileAtWorldCoord(currMousePosition);
		if (tileUnderMouse != null) {
			CircleCursor.SetActive(true);
			var cursorPosition = new Vector3(tileUnderMouse.X, 0, tileUnderMouse.Z);
			CircleCursor.transform.position = cursorPosition;
		}
		else {
			CircleCursor.SetActive(false);
		}
	}*/

	private void UpdateCameraMovement() {
		if (!Input.GetMouseButton(RightMouse) && !Input.GetMouseButton(MiddleMouse)) return;
		Camera.main.transform.position -= transform.right * Input.GetAxis("Mouse X");
		Camera.main.transform.position += transform.forward * Input.GetAxis("Mouse X");
		Camera.main.transform.position -= transform.up * Input.GetAxis("Mouse Y") * 2 ;
	}

	private static void UpdateZoom() {
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 5)
		{
			Camera.main.orthographicSize--;
		}	
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 30)
		{
			Camera.main.orthographicSize++;
		}
	}

	private void UpdateDragSelection(Vector3 currMousePosition) {
		// Check if over UI
		if (EventSystem.current.IsPointerOverGameObject()) { return; }
		
		
		// Start drag
		if (Input.GetMouseButtonDown(LeftMouse)) {
			_dragStartPosition = currMousePosition;
		}
		
		var startX = Mathf.FloorToInt(_dragStartPosition.x);
		var endX = Mathf.FloorToInt(currMousePosition.x);

		if (endX < startX) {
			var tmp = endX;
			endX = startX;
			startX = tmp;
		}

		var startZ = Mathf.FloorToInt(_dragStartPosition.z);
		var endZ = Mathf.FloorToInt(currMousePosition.z);
		if (endZ < startZ) {
			var tmp = endZ;
			endZ = startZ;
			startZ = tmp;
		}
		
		// Clean dragpreviews
		while (_dragPreviewGameObjects.Count > 0) {
			var go = _dragPreviewGameObjects[0];
			_dragPreviewGameObjects.RemoveAt(0);
			SimplePool.Despawn(go);
		}

		if (Input.GetMouseButton(LeftMouse)) {
			for (var x = startX; x <= endX; x++) {
				for (var z = startZ; z <= endZ; z++) {
					var t = WorldController.Instance.World.GetTileAt(x, z);
					if (t != null) {
						var go = SimplePool.Spawn(CircleCursorPrefab, new Vector3(x, 0, z), Quaternion.Euler(90,0,0));
						go.transform.SetParent(transform, true);
						_dragPreviewGameObjects.Add(go);
					}
				}
			}
			
		}

		if (Input.GetMouseButtonUp(LeftMouse)) {
			
			
			for (var x = startX; x <= endX; x++) {
				for (var z = startZ; z <= endZ; z++) {
					var t = WorldController.Instance.World.GetTileAt(x, z);
					if (t != null) {
						if (_buildModeIsObjects == true) {
							WorldController.Instance.World.PlaceInstalledObject(_buildModeObjectType, t);
						}
						else {
							t.Type = _buildModeTile;	
						}
					}
				}
			}
		}
	}

	public void SetMode_Build() {
		_buildModeIsObjects = false;
		_buildModeTile = Tile.TileType.Ground;
	}
	
	public void SetMode_Bulldoze() {
		_buildModeIsObjects = false;
		_buildModeTile = Tile.TileType.Empty;
	}

	public void SetMode_BuildInstalledObject(string objectType) {
		_buildModeIsObjects = true;
		_buildModeObjectType = objectType;
	}
}

/*if (_tileDragStart == null) return;
				if (tileUnderMouse == null) return;
				switch (tileUnderMouse.Type) {
					case Tile.TileType.Empty:
						tileUnderMouse.Type = Tile.TileType.Ground;
						break;
					case Tile.TileType.Ground:
						tileUnderMouse.Type = Tile.TileType.Road;
						break;
					case Tile.TileType.Road:
						tileUnderMouse.Type = Tile.TileType.Empty;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}*/
