using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour {

	Money_Sys money;
	MovingAndInteracting player;
	public GameObject Car;

	// Use this for initialization
	void Start () {
		money = Money_Sys.instance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartDelivery(){
		StartCoroutine ("DeliveryRun");
	}

	IEnumerator DeliveryRun()
	{
		Debug.Log ("Starting a delivery");
		player.gameObject.SetActive (false);
		Car.SetActive (false);

		yield return new WaitForSeconds (5f); //lol im driving

		money.AddKroner(40f);
		player.gameObject.SetActive (true);
		Car.SetActive (true);
		Debug.Log ("Done with delivery");
	}
}
