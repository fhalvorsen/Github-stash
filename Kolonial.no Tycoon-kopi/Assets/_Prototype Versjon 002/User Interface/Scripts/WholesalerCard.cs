using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WholesalerCard : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI negotiationLevel, qualityRating, nameLabel;
	[SerializeField]
	private Image icon;
	private WholesalerData Wholesaler;

	public void Setup (WholesalerData data)
	{
		Wholesaler = data;
		negotiationLevel.text = Wholesaler.negotiationLevel.ToString();
		nameLabel.text = Wholesaler.name;
		transform.localScale = Vector3.one;
		icon.sprite = Wholesaler.logo;
	}

	public void DisplayInventory()
	{
		UserInterfaceController.Instance.DisplayWholesalerInventoryMenu(Wholesaler);
	}
}
