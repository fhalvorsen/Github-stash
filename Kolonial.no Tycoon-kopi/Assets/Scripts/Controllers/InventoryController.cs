using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

	private static ItemX _item;

	public static ItemX Item {
		get { return _item; }
		private set { _item = value; }
	}

	// Use this for initialization
	void Start () {
		
		Item = ItemController.CreateItem();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyUp(KeyCode.Q)) {
			Inventory_old.AddToInventory(_item, 1);
		}
		
		if (Input.GetKeyUp(KeyCode.C)) {
			Debug.Log(Inventory_old.GetInventoryAmount());
		}
		
		if (Input.GetKeyUp(KeyCode.R)) {
			Inventory_old.RemoveFromInventory(_item, 2);
		}
	}

	public static void TakeFromInventory(Dictionary<ItemX, int> order) {
		foreach (var item in order.Keys) {
			Inventory_old.RemoveFromInventory(item, order[item]);

		}
	}

	

}
