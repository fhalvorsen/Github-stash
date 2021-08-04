using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Order
{
    private int timeOfOrder;
    public int OrderSize;
    public CustomerData Customer;
    
    public Order(CustomerData customer)
    {
        Debug.Log(customer.name);
        timeOfOrder = GameManager.Instance.time.GetTime();
        Customer = customer;
        CountOrderSize(customer.ShoppingList);
    }
    private void CountOrderSize(ItemDictionary ItemsToOrder)
    {
        foreach (var item in ItemsToOrder.Items)
        {
            OrderSize += item.Value;
        }
    }
   
}
