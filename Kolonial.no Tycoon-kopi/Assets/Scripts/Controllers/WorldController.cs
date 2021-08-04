using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
//using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour {
	public static WorldController Instance { get; protected set; }

	public Sprite GroundSprite;
	public Sprite RoadSprite;
	public Mesh CubeMesh;
	public Material Material;

	private Dictionary<Tile, GameObject> _tileGameObjectMap;
	private Dictionary<InstalledObject, GameObject> _installedObjectGameObectMap;

	 public World World { get; protected set; }
	
	void Start () {
		if (Instance != null) {
			Debug.LogError("There should not be two world controllers");
		}
		Instance = this;
		
		// Create a world with Empty tiles
		World = new World();
		
		World.RegisterInstalledObjectCreated(OnInstalledObjectCreated);
		
		_tileGameObjectMap = new Dictionary<Tile, GameObject>();
		_installedObjectGameObectMap = new Dictionary<InstalledObject, GameObject>();
		
		// Create a GameObject for each tile.
		for (var x = 0; x < World.Width; x++) {
			for (var z = 0; z < World.Height; z++) {
				var tileData = World.GetTileAt(x, z);
				var tileGo = new GameObject {name = "Tile_" + x + "_" + "0" + "_" + z};
				tileGo.transform.position = new Vector3(tileData.X, 0, tileData.Z);
				tileGo.transform.Rotate(90, 0, 0);
				tileGo.transform.SetParent(transform, true);
				
				// Add a sprite renderer
				tileGo.AddComponent<SpriteRenderer>();		
				
				tileData.RegisterTileTypeChangedCallback( tile => {OnTileTypeChanged(tile, tileGo);} );
		
			}
		}
		World.RandomizeTiles();
	}

	void OnTileTypeChanged(Tile tileData, GameObject tileGo) {
		switch (tileData.Type) {
			case Tile.TileType.Ground:
				tileGo.GetComponent<SpriteRenderer>().sprite = GroundSprite;
				break;	
				
			case Tile.TileType.Road:
				tileGo.GetComponent<SpriteRenderer>().sprite = RoadSprite;
				break;
				
			case Tile.TileType.Empty:
				tileGo.GetComponent<SpriteRenderer>().sprite = null;
				break;
				
			default:
				Debug.LogError("OnTileTypeChanged - Unrecognized tyle type.");
				break;
		}
	}
	
	public static Tile GetTileAtWorldCoord(Vector3 coord){
		var x = Mathf.FloorToInt(coord.x);
		var z = Mathf.FloorToInt(coord.z);
		
		return Instance.World.GetTileAt(x, z);
	}

	public void OnInstalledObjectCreated(InstalledObject obj) {
		Debug.Log("OnInstalledObjectCreated");
		
		var objGo = new GameObject();
		
		_installedObjectGameObectMap.Add(obj, objGo);
		objGo.name = obj.ObjectType + "_" + obj.Tile.X + "_" + obj.Tile.Z;
		objGo.transform.position = new Vector3(obj.Tile.X, 1, obj.Tile.Z);
		objGo.transform.localScale = new Vector3(1, 2, 1);
		objGo.transform.SetParent(transform, true);

		objGo.AddComponent<MeshFilter>().mesh = CubeMesh;
		objGo.AddComponent<MeshRenderer>().material = Material;
	
		
		obj.RegisterOnChangedCallback(OnInstalledObjectChanged);
	}

	void OnInstalledObjectChanged(InstalledObject obj) {
		Debug.LogError("OnInstalledObjectChanged -- not implemented");
	}
}
