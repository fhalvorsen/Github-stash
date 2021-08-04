using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemCard : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI itemName, countLabel, priceRating, qualityRating;
	[SerializeField]
	private Image icon;
	private ItemData itemData;

	public void Setup (ItemData data, int count)
	{
		itemData = data;
		itemName.text = itemData.name;
		icon.sprite = itemData.icon;
		transform.localScale = Vector3.one;
		countLabel.text = count.ToString();
		priceRating.text = itemData.priceRating.ToString();
		qualityRating.text = itemData.quality.ToString();
	}

	public void TrashItem()
	{
		GameManager.Instance.gameData.playerCompany.inventory.Items[itemData] = 0;
		countLabel.text = 0.ToString();
	}
}
