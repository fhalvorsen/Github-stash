using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservedOrderQueue {
	
	private static readonly Queue<Dictionary<ItemX, int>> ReservedQueue = new Queue<Dictionary<ItemX, int>>();

	public ReservedOrderQueue() {
		
	}
	
	public static void ReserveOrder(Order_old order) {
		
		ReservedQueue.Enqueue(order.GetOrderItems());

		foreach (var item in order.GetOrderItems()) {
			Debug.Log(item + " has been added to the reserved queue order");
		}
	}

	public static Order_old DequeueOrder() {

		// Checking if queue contains any orders
		if (ReservedQueue.Count <= 0) {
			Debug.LogError("No orders in queue.");
			return null;
		}

		var order = ReservedQueue.Peek();

		foreach (var item in order.Keys) {
			Debug.Log(order[item] + " " + item + " has been added to reserved queue");

		}
		return null;
	}
	
}
