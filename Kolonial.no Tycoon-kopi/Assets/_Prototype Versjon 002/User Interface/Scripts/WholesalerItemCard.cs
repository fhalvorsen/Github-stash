using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WholesalerItemCard : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI itemName, price, priceRating, qualityRating, InputText;
	[SerializeField]
	private Image icon;
	private ItemData itemData;
	public int count;

	public void Setup (ItemData data)
	{
		itemData = data;
		itemName.text = itemData.name;
		icon.sprite = itemData.icon;
		transform.localScale = Vector3.one;
		priceRating.text = itemData.priceRating.ToString();
		qualityRating.text = itemData.quality.ToString();
		price.text = itemData.wholesalerPrice.ToString();
		InputText.text = "0";
		count = 0;
	}

	public void UpdateAmount(string amount)
	{
		count = int.Parse(amount);
		UserInterfaceController.Instance.UpdateOrder(itemData, count);
	}
}
