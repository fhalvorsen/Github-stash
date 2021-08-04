using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class InstalledObject {
	
	public Tile Tile { get; protected set; }

	public string ObjectType { get; private set; }

	private int _width;
	private int _height;

	private Action<InstalledObject> _cbOnChanged;

	protected InstalledObject() {
		
	}


	public static InstalledObject CreatePrototype(string objectType, int width=1, int height=1 ) {
		var obj = new InstalledObject {
			ObjectType = objectType,
			_width = width,
			_height = height
		};
		return obj;
	}

	public static InstalledObject PlaceInstance(InstalledObject proto, Tile tile) {
		var obj = new InstalledObject {
			ObjectType = proto.ObjectType,
			_width = proto._width,
			_height = proto._height,
			Tile = tile
		};
		
		return tile.PlaceObject(obj) == false ? null : obj;
	}
	
	public void RegisterOnChangedCallback(Action<InstalledObject> cb) {
		_cbOnChanged += cb;
	}
	public void UnRegisterOnChangedCallback(Action<InstalledObject> cb) {
		_cbOnChanged -= cb;
	}
}
