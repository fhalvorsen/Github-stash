using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	public enum TileType {
		Empty,
		Ground,
		Road
	}

	private TileType _type = TileType.Empty;

	private Action<Tile> _cbTileTypeChanged;

	public TileType Type {
		get { return _type; }
		set {
			var oldType = _type;
			_type = value;
			
			if(_cbTileTypeChanged != null && oldType != _type)
			_cbTileTypeChanged(this);
		}
	}

	private InstalledObject installedObject;

	World World;


	public int X { get; private set; }
	public int Z { get; private set; }

	public Tile(World world, int x, int z) {
		World = world;
		X = x;
		Z = z;

	}

	public void RegisterTileTypeChangedCallback(Action<Tile> cb) {
		_cbTileTypeChanged += cb;
	}
	
	public void UnRegisterTileTypeChangedCallback(Action<Tile> cb) {
		_cbTileTypeChanged -= cb;
	}

	public bool PlaceObject(InstalledObject objInstance) {
		if (objInstance == null) {
			installedObject = null;
			return true;
		}
		if (installedObject != null) {
			Debug.LogError("Trying to assign an installed object to a tile that already has one!");
			return false;
		}
		installedObject = objInstance;
		return true;
	}


}
