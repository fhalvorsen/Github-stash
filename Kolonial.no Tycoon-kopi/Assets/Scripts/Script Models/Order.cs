using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;

public class Order_old
{
	private static Dictionary<ItemX, int> _order;
	private static float _timeOfOrder;
	private InteractableCustomer _customer;

	public Order_old()	{
		_order = new Dictionary<ItemX, int>();
	}
	
	public void AddToOrder(ItemX item, int itemAmount) {
		
		// Checking if items exists in Inventory
		if (!Inventory_old.IsItemInInventory(item, itemAmount)){ Debug.Log("Items not in inventory"); return;}
		
		// Checking if item already exists in order. If so: iterate.
		if (_order.ContainsKey(item)) {
			Debug.Log("Item already in order, iterating by " + itemAmount);
			_order[item] += itemAmount;
			Debug.Log("There are now " + _order[item] + " items in this order.");
		}
		else {
			_order.Add(item, itemAmount);
			Debug.Log("Added " + itemAmount + " of " + item.GetItemName() + " to order.");
            
		}
	}

	public static void RemoveFromOrder(ItemX item, int itemAmount) {
		
		// Checking if items exists in Inventory
		if (!Inventory_old.IsItemInInventory(item, itemAmount)){ Debug.Log("Items not in inventory"); return;}
		
		// Checking if item already exists in order. If so: iterate.
		if (_order.ContainsKey(item) && _order[item] >= itemAmount) {
			_order[item] -= itemAmount;
			Debug.Log("Removed " + itemAmount + " from order");
		}
		else {
			Debug.Log("The order contains none or not enough of this item to remove");
		}
	}
	
	public int GetItemAmount() {
		return _order.Count;
	}
	
	public Dictionary<ItemX, int> GetOrderItems () {
		return _order;
	}
	
    public string GetCustomerName()
    {
        return _customer.name;
    }

}
