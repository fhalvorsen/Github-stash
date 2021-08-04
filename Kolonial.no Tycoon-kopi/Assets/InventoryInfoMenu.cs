using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryInfoMenu : MonoBehaviour 
{
	private SimpleObjectPool inventoryItemCardPool;
	[SerializeField]
	private GameObject inventoryCardGrid;
	[SerializeField]
	private TextMeshProUGUI inventoryCapacity;
	private ItemDictionary inventory;
	private List<InventoryItemCard> itemCards = new List<InventoryItemCard>();

	private void Update()
	{
		int inventoryCount = 0;
		foreach (ItemData item in inventory.Items.Keys)
		{
			inventoryCount += inventory.Items[item];
		}
		inventoryCapacity.text =  "Capacity: " + inventoryCount + " / " + GameManager.Instance.Warehouse.Capacity.ToString();
	}

	private void OnEnable()
	{
		while(itemCards.Count > 0)
		{
			inventoryItemCardPool.ReturnObject(itemCards[0].gameObject);
			itemCards.RemoveAt(0);
		}
		inventoryItemCardPool = GetComponent<SimpleObjectPool>();
		inventory = GameManager.Instance.gameData.playerCompany.inventory;
		foreach (ItemData item in inventory.Items.Keys)
		{
			GameObject itemCard = inventoryItemCardPool.GetObject();
			itemCard.transform.SetParent(inventoryCardGrid.transform);
			InventoryItemCard cardScript = itemCard.GetComponent<InventoryItemCard>();
			itemCards.Add(cardScript);
			cardScript.Setup(item, inventory.Items[item]);
		}
		
	}
}
