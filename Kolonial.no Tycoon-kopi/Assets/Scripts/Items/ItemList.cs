using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList {

	public List<Item> Items;
	public float _timeOfOrder;
	public Interactable _customer;

	public ItemList(List<Item> items, Interactable customer){
		Items = items;
		_timeOfOrder = Time.time;
		_customer = customer;
	}
}
