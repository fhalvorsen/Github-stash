using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{

	private bool _websiteCreated = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void CreateWebsite() {
		_websiteCreated = true;
		Debug.Log("Website has been created -- taking orders");
	}

	public bool DoesWebsiteExist() {
		return _websiteCreated;
	}
}
