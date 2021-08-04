using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class World {
	private Tile[,] tiles;

	private Dictionary<string, InstalledObject> _installedObjectPrototypes;
	
	private int _width;
	private int _height;

	public int Width {
		get { return _width; }
	}
	
	public int Height {
		get { return _height; }
	}

	private Action<InstalledObject> _cbInstalledObjectCreated;

	public World (int width = 100, int height = 100) {
		_width = width;
		_height = height;

		tiles = new Tile[width, height];

		for (var x = 0; x < width; x++) {
			for (var z = 0; z < height; z++) {
				tiles[x, z] = new Tile(this, x, z);
			}
		}
		
		Debug.Log("World created with " + width*height + " tiles.");
		
		CreateInstalledObjectPrototypes();
	}

	private void CreateInstalledObjectPrototypes() {
		
		_installedObjectPrototypes = new Dictionary<string, InstalledObject>();

		_installedObjectPrototypes.Add(
				"Building", 
				InstalledObject.CreatePrototype("Building")
			);
	}

	InstalledObject CreateInstalledObjectPrototype() {
		return null;
	}

	public void RandomizeTiles() {
		Debug.Log("RandomizeTiles");
		for (var x = 0; x < _width; x++) {
			for (var z = 0; z < _height; z++) {
				switch (Random.Range(0, 2)) {
						case 0: tiles[x, z].Type = Tile.TileType.Ground;
							break;
							
						case 1: tiles[x, z].Type = Tile.TileType.Road;
							break;
							
						default: tiles[x, z].Type = Tile.TileType.Empty;
							break;
				}
			}
		}
	}

	public Tile GetTileAt(int x, int z) {
		if (x <= _width && x >= 0 && z <= _height && z >= 0) return tiles[x, z];
		Debug.LogError("Tile ("+x+","+z+") is out of range");
		return null;
	}

	public void PlaceInstalledObject(string objectType, Tile t) {
		Debug.Log("PlaceInstalledObject");
		if (_installedObjectPrototypes.ContainsKey(objectType) == false) {
			Debug.LogError("installedObjectPrototypes doesn't contain a proto for key: " + objectType);
			return;
		}

		var obj = InstalledObject.PlaceInstance(_installedObjectPrototypes[objectType], t);

		if (_cbInstalledObjectCreated != null) {
			_cbInstalledObjectCreated(obj);
		}
	}

	public void RegisterInstalledObjectCreated(Action<InstalledObject> cb) {
     		_cbInstalledObjectCreated += cb;
     	}
	public void UnRegisterInstalledObjectCreated(Action<InstalledObject> cb) {
		_cbInstalledObjectCreated -= cb;
	}
}
