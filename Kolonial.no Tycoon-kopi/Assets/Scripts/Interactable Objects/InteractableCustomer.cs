using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCustomer : Interactable {

	Warehouse_old warehouse;

	void Start(){
		warehouse = Warehouse_old.instance;
		StartCoroutine ("ShoppingCycle");
	}


		
	IEnumerator ShoppingCycle ()
	{
		if (warehouse.WebsiteExists) 
		{
			Shopping ();
		}
		yield return new WaitForSeconds (Random.Range (5, 100));
		StartCoroutine ("ShoppingCycle");
	}

	void Shopping(){
		 // add money to the player
		bool couldOrder;
	
		couldOrder = warehouse.Order (GenerateGroceryList());

		if (!couldOrder) {
			Debug.Log (gameObject.name + " Could not order and is very disapointed");
		} else {
			Debug.Log (gameObject.name + " Placed an order");
		}
	}

	ItemList GenerateGroceryList()
	{
		List<Item> groceries = new List<Item> ();
		foreach (Item item in warehouse.AvailableItems) 
		{
			int randomNumber = Random.Range (0, 3);
			for (int i = 0; i < randomNumber; i++) 
			{
				groceries.Add (item);
			}
		}
		ItemList groceryOrder = new ItemList (groceries, this);
		return groceryOrder;
	}



}
