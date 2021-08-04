using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    //public TextMeshProUGUI inventoryStats;
    //public TextMeshProUGUI totalOrders;
    public Text TotalOrders;
    Warehouse_old warehouse;


	// Use this for initialization
	void OnEnable ()
    {
        //inventoryStats = GetComponentInChildren<TextMeshProUGUI>();
        //totalOrders = GetComponent<TextMeshProUGUI>();

        warehouse = Warehouse_old.instance;
        warehouse.onItemChangedCallback += UpdateUI;

        UpdateUI();
    }

    void UpdateUI()
    {
        string textToDisplay = "";
        int totalInventory = 0;

        foreach (Item key in warehouse.Inventory.Keys)
        {
            textToDisplay += "\n" + key.name + " " + warehouse.Inventory[key];
            totalInventory += warehouse.Inventory[key];
        }
        textToDisplay += "\nCapacity: " + totalInventory + " / " + warehouse.Capacity;
         
            /*
        foreach (ItemX item in Inventory.GetInventory().Keys) {
            textToDisplay += "\n" + item.GetItemName() + " " + Inventory.GetInventory()[item];
        }*/

        //inventoryStats.text = textToDisplay;

        if (warehouse.GetQueue.Count > 0)
        {
            TotalOrders.text = "Orders waiting to be packed: " + warehouse.GetQueue.Count.ToString();
        }
        else
        {
            TotalOrders.text = "No orders ready for packing";
        }
    }
}
