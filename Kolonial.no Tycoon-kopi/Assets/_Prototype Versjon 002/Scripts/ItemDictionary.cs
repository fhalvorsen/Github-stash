using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary {
    public Dictionary<ItemData, int> Items;

    public ItemDictionary(){
        Items = new Dictionary<ItemData, int>();
    }

    public void AddItem(ItemData itemData, int amount)
    {
        if(Items.ContainsKey (itemData))
        {
            Items[itemData] += amount;
        } 
        else
        {
            Items.Add(itemData, amount);
            Debug.Log("Added " + amount + " of " + itemData.name);
        }
    }

    public void AddItem(int id, int amount, List<ItemData> itemList)
    {
        foreach (ItemData item in itemList)
        {
            if (item.GetInstanceID() == id)
            {
                Items.Add(item, amount);
                Debug.Log("Added " + amount + " of " + item.name);
            }
        }
    }

    public void RemoveItem(ItemData itemData, int amount)
    {
        if (Items[itemData] >= amount)
        {
            Items[itemData] -= amount;
            Debug.Log("Removed " + amount + " of " + itemData.name); return;
        }
        else if(Items[itemData] < amount)
        {
            Debug.Log("Not enough items in ItemDictionary to remove " + amount + " items.");
            return;
        }
        else
        {
            Debug.Log("No item of this type in inventory");
            return;
        }
    }

    public bool IsItemInDictionary(ItemData item, int amount)
    {
        foreach (var it in Items)
        {
            if (item == it.Key) {
                if (it.Value >= amount) {
                    return true;
                }
            }
        }

        return false;
    }

    public Dictionary<ItemData, int> GetItemDictionary()
    {
        return Items;
    }
}