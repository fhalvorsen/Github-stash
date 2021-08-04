using System.Collections.Generic;
using UnityEngine;

public class Warehouse_old : MonoBehaviour {

    public List<Item> AvailableItems = new List<Item>();
	public bool WebsiteExists = false;

	public int Capacity;
    public Dictionary<Item, int> Inventory = new Dictionary<Item, int>();

    Queue<ItemList> orderQueue = new Queue<ItemList>();
	Queue<ItemList> readyToDeliver = new Queue<ItemList> ();

#region Singleton

    public static Warehouse_old instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one isntance of Warehouse found");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onItemChangedCallback;

    // Use this for initialization
    void Start () {
		RestockInventory (GenerateTestList());
        
	}

    Dictionary<Item, int> GenerateTestList()
    {
        Dictionary<Item, int> items = new Dictionary<Item, int>();

        foreach (Item item in AvailableItems)
        {
            items.Add(item, 10);
        }
        return items;
    }

	public void RestockInventory(Dictionary<Item, int> itemsToAdd)
    {
        foreach (Item key in itemsToAdd.Keys)
        {
            if (Inventory.ContainsKey(key))
            {
                Inventory[key] += itemsToAdd[key];
            }
            else
            {
                Inventory.Add(key, itemsToAdd[key]);
            }
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    

	public void SubtractFromInventory(Dictionary<Item, int> itemsToSubtract)
    {


        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

	public bool Order (ItemList groceries)
	{
		int length = groceries.Items.Count;
		Dictionary<Item, int> tempInventory = Inventory;

		for (int i = 0; i < length; i++) 
		{
			if(tempInventory.ContainsKey(groceries.Items[i]))
			{
				if (tempInventory [groceries.Items [i]] <= 0) 
				{
					return false;
				} 
				else 
				{
					tempInventory [groceries.Items [i]]--;
				}
			}

		}

		Inventory = tempInventory;
		orderQueue.Enqueue (groceries);
		Debug.Log ("orderQeue contains " + orderQueue.Count + " orders");

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		
		return true;
	}

	public void PrepareOrder()
	{
		ItemList itemsToPrep = orderQueue.Dequeue ();
		Debug.Log ("orderQeue contains" + orderQueue.Count + " orders");

		readyToDeliver.Enqueue (itemsToPrep);
		Debug.Log (readyToDeliver.Count + " orders are ready to deliver");

	}

	public Queue<ItemList> GetDeliveries(){
		Queue<ItemList> temp = new Queue<ItemList>();

		while(readyToDeliver.Count > 0){
			temp.Enqueue(readyToDeliver.Dequeue());
		}

		return temp;
	}

	public void MakeWebsite(){
        if (!WebsiteExists)
        {
            WebsiteExists = true;
            Debug.Log("Website has been created -- taking orders");
            
        }
    }

    public Queue<ItemList> GetQueue
    {
        get { return orderQueue; }
    }

    public Queue<ItemList> GetDeliveryOrders
    {
        get { return readyToDeliver; }
    }
}
