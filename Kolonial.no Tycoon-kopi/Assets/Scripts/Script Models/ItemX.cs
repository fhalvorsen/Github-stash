using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class ItemX
{

    private string _itemName;
    private Sprite _icon = null;
    private float _purchasePrice;
    private float _retailPrice;
    

    public ItemX(string itemName, float purchasePrice, float retailPrice) {
        _itemName = itemName;
        _purchasePrice = purchasePrice;
        _retailPrice = retailPrice;

    }

    public ItemX() {
        _itemName = "Default_Item";
        _purchasePrice = 23.7f;
        _retailPrice = 25.5f;
    }

    public string GetItemName() {
        return _itemName;
    }

    public float GetPurchasePrice() {
        return _purchasePrice;
    }
    
    public float GetRetailPrice() {
        return _retailPrice;
    }
}
