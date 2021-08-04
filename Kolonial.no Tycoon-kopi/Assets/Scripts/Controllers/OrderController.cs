using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrderController : MonoBehaviour
{

	private Order_old _order;

	private TimeController _timeController;
	private ComputerController _computerController;

	private int _hour;
	
	private ItemX _item;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		_item = ItemController.CreateItem();
	}
	
	// Update is called once per frame
	void Update () {

		/*if (_computerController.DoesWebsiteExist()) {
			if (_timeController.TimeHours == _hour) {
				_order = new Order(Random.Range(1,20));
				Debug.Log("Created order with " + order.GetItemAmount() + " items.");
				QueueOrder(order);
				_hour++;
				if (_hour == 24) {
					_hour = 0;
				}

			}
			
			if (_hour == 12 || _hour == 19 ) {
				DequeueOrder();
				if (_hour == 24) {
					_hour = 0;
				}

			}
				
		}*/
		
		if (Input.GetKeyUp(KeyCode.A)){
			_order = new Order_old();
			_order.AddToOrder(InventoryController.Item, 1);

			foreach (var item in _order.GetOrderItems()) {
				if (Inventory_old.IsItemInInventory(item.Key, item.Value)) {
					Debug.Log("Queued order with " + _order.GetItemAmount() + " items.");
					OrderQueue.QueueOrder(_order);
				}
				else {
					Debug.Log("Not enough of this item in inventory.");
				}
			}
			/*	
			if (Inventory.IsItemInInventory()) {
				Debug.Log("Created order with " + _order.GetItemAmount() + " items.");
				OrderQueue.QueueOrder(_order);
			}
			else {
				Debug.Log("Not enough of this item in inventory.");
			}
			*/
		}
		
		if (Input.GetKeyUp(KeyCode.S)) {
			OrderQueue.DequeueOrder();

		}
		
		if (Input.GetKeyUp(KeyCode.D)){
			Debug.Log("Items in inventory: " + Inventory_old.GetInventoryAmount());
		}
		
	}
	
	
	//TODO: function for peeking order
}