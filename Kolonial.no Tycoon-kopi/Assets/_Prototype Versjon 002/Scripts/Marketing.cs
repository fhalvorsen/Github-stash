using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marketing : MonoBehaviour
{
	public int campaignPrice;

	public enum PrefToChange
	{
		websitePref,
		appPref,
	}

	public void CampaignByAge(int min, int max, int amount, PrefToChange pref)
	{
		foreach (var customer in GameManager.Instance.gameData.Customers)
		{
			if (customer.age >= min && customer.age <= max)
			{		
				ChangeCustomerPreferences(customer, amount, pref);
			}
		}
	}

	private void ChangeCustomerPreferences(CustomerData customer, int amount, PrefToChange pref)
	{
		switch (pref)
		{
			case PrefToChange.websitePref:
			customer.websitePrefScore += amount;
				break;
			
			case PrefToChange.appPref:
				customer.appPrefScore += amount;
				break;
		}
	}
}
