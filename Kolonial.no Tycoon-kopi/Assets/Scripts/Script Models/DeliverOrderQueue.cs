using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverOrderQueue {

	private static readonly Queue<Order_old> DeliveryQueue = new Queue<Order_old>();

	public DeliverOrderQueue() {
		
	}
	
	public static void QueueReservedOrder(Order_old order) {
		DeliveryQueue.Enqueue(order);
	}

	public static Order_old DequeueOrder() {

		// Checking if queue contains any orders
		if (DeliveryQueue.Count <= 0) {
			Debug.LogError("No orders in queue.");
			return null;
		}

		var order = DeliveryQueue.Peek().GetOrderItems();

		foreach (var item in order.Keys) {
			Inventory_old.RemoveFromInventory(item, order[item]);

		}
		return null;
	}
}
