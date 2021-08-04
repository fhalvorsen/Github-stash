using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderQueue
{
    private static Queue<Order_old> _orderQueue = new Queue<Order_old>();

    public static void QueueOrder(Order_old order) {
        _orderQueue.Enqueue(order);

        foreach (var item in order.GetOrderItems()) {
            if (Inventory_old.IsItemInInventory(item.Key, item.Value)) {
                Inventory_old.RemoveFromInventory(item.Key, item.Value);
                
            }
        }
    }

    public static Order_old DequeueOrder() {

        // Checking if queue contains any orders
        if (_orderQueue.Count <= 0) {
            Debug.LogError("No orders in queue.");
            return null;
        }

        var order = _orderQueue.Peek();
        ReservedOrderQueue.ReserveOrder(order);

                ReservedOrderQueue.ReserveOrder(order);
                _orderQueue.Dequeue();
                Debug.Log(
                    "Dequeued order from orderqueue and put order in reserved queue");
                
            

            ReservedOrderQueue.ReserveOrder(order);

            return order;
        
    }
}