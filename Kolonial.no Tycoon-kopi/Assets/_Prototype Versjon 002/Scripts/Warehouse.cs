using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse
{
    public List<InteractableVehicle> availableVehicles = new List<InteractableVehicle>();
    public int Capacity, StaffSlots = 7;
    public ItemDictionary Inventory;

    public readonly Queue<Order> orderQueue = new Queue<Order>();
    public readonly Queue<Order> deliveryQueue = new Queue<Order>();


    public Warehouse()
    {
        Inventory = GameManager.Instance.gameData.Companies[0].inventory;

        foreach(InteractableVehicle vehicle in GameObject.FindObjectsOfType<InteractableVehicle>())
        {
            availableVehicles.Add(vehicle);
        }
    }

    private int GetAmountOfItemsInInventory()
    {
        int totalItems = 0;
        foreach (var item in Inventory.Items)
        {
            totalItems += item.Value;
        }

        return totalItems;
    }
    public void AddOrderToOrderQueue(Order order)
    {
        orderQueue.Enqueue(order);
        UserInterfaceController.Instance.AddOrderIndicator();
    }

    public Order TakeOrderFromOrderQueue()
    {
        if (orderQueue.Count > 0)
        {
            Debug.Log("Prepared order for" + orderQueue.Peek().Customer.name);
            UserInterfaceController.Instance.RemoveOrderIndicator();
            return orderQueue.Dequeue();
        }
        else
        {
            return null;
        }

    }

    public void AddOrderToDeliveryQueue(Order order)
    {
        deliveryQueue.Enqueue(order);
        UserInterfaceController.Instance.AddDeliveryIndicator();
    }

    public void AddToDeliveryFromOrderQueue() 
    {
        deliveryQueue.Enqueue(TakeOrderFromOrderQueue());
        UserInterfaceController.Instance.AddDeliveryIndicator();
        Debug.Log(deliveryQueue.Count + " items in delivery queue for warehouse.");
    }

    public Order TakeOrderFromDeliveryQueue()
    {
         if (deliveryQueue.Count > 0)
        {
            UserInterfaceController.Instance.RemoveDeliveryIndicator();
            return deliveryQueue.Dequeue();
        }
        Debug.Log("No items in warehouse delivery queue.");
        return null;
    }

    public void PutOrdersInVehicles()
    {
        // foreach (var vehicle in availableVehicles)
        // {
        //     if (vehicle.available)
        //     {       
        //         while (vehicle.itemsInVehicle <= vehicle.capacity)
        //         {
        //             if (deliveryQueue.Count != 0) {
        //                 if ((vehicle.itemsInVehicle + deliveryQueue.Peek().OrderSize) > vehicle.capacity) {
        //                     Debug.Log("Vehicle capacity is full! Next order is too big.");
        //                     break;
        //                 } else if (deliveryQueue.Count == 0) {
        //                     Debug.Log("No more items in delivery queue to deliver!");
        //                     return;
        //                 }

        //                 vehicle.itemsInVehicle += deliveryQueue.Peek().OrderSize;
        //                 vehicle.AddOrderToVehicleDeliveryQueue(TakeOrderFromDeliveryQueue());
        //                 Debug.Log("Added order to vehicle delivery queue.");
        //             }
        //         }
        //     } else {
        //         Debug.Log("This vehicle is not ready to deliver.");
        //     }
        // }
    }

    void DeliverOrder()
    {
        // foreach (var vehicle in availableVehicles)
        // {
        //     if (vehicle.available && vehicle.itemsInVehicle != 0)
        //     {
        //         // vehicle.StartDelivery();
        //     } 
        //     else if (vehicle.itemsInVehicle == 0) 
        //     {
        //         Debug.Log("This vehicle has no items to deliver.");
        //     } 
        //     else 
        //     {
        //         Debug.Log("This vehicle is not ready to deliver.");
        //     }
        // }
    }

    public void BuyFromWholesaler(ItemData item, int amount)
    {
        //Check if warehouse has space for items
        if (Capacity % GetAmountOfItemsInInventory() >= amount)
        {
            //Check if player has enough money for items
            if (GameManager.Instance.playerBalance >= item.wholesalerPrice * amount)
            {
                GameManager.Instance.playerBalance -= item.wholesalerPrice * amount;
                //sets retail price to 15% more than you bought it for;
                item.retailPrice = item.wholesalerPrice * 1.15f;
                Inventory.AddItem(item, amount);
                Debug.Log("Bought " + amount + " of item: '" + item.name + "' and added to inventory");
            }
            Debug.Log("Not enough money for these items");
        }
        Debug.Log("Not enough space in inventory for these items");
    }
}
