using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

	private Camera WarehouseCamera;
	
	void Start()
	{
		WarehouseCamera = Camera.main;
	}
	
	void Update () 
	{
		transform.LookAt(transform.position + WarehouseCamera.transform.rotation * Vector3.forward, WarehouseCamera.transform.rotation * Vector3.up);
	}
}
