using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_old{
   

    private static Dictionary<ItemX, int> _inventory = new Dictionary<ItemX, int>();
    private int _capacity;
    
    public Inventory_old() {
        
    }

    public static void AddToInventory(ItemX item, int itemAmount) {

        if (_inventory.ContainsKey(item)) {
            Debug.Log(item.GetItemName() + " is already in inventory, iterating by " + itemAmount);
            _inventory[item] += itemAmount;
            Debug.Log("There are now " + _inventory[item] + " of " + item.GetItemName() + " in inventory.");
        }
        else {
            _inventory.Add(item, itemAmount);
            Debug.Log("Added " + itemAmount + " of " + " " + item.GetItemName() + " to inventory.");
            
        }
    }

    public static bool IsItemInInventory(ItemX item, int itemAmount) {
        return _inventory.ContainsKey(item) && _inventory[item] >= itemAmount;
    }

    public static void RemoveFromInventory(ItemX item, int itemAmount) {
        if (_inventory.ContainsKey(item) && _inventory[item] >= itemAmount) {
            _inventory[item] -= itemAmount;
            Debug.Log("Removed " + itemAmount + " " + item.GetItemName() + " from inventory");
        }
        else {
            Debug.Log("The inventory contains none or not enough of " + item.GetItemName() + " to remove");
        }
    }

    public static void TakeFromInventory(ItemX item, int itemAmount) {
        if (_inventory.ContainsKey(item) && _inventory[item] >= itemAmount) {
            _inventory[item] -= itemAmount;
            Debug.Log("Took " + itemAmount + " " + item.GetItemName() + " from inventory");
        }
        else {
            Debug.Log("The inventory contains none or not enough of " + item.GetItemName() + " to remove");
        }
    }

    public static int GetInventoryAmount() {
        return _inventory.Count;
    }

    public static Dictionary<ItemX, int> GetInventory() {
        return _inventory;
    }
}
