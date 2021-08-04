using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildServiceMenu : MonoBehaviour 
{
	[SerializeField]
	private TMP_Dropdown dropdown;
	[SerializeField]
	private Toggle toggle1, toggle2, toggle3;
	[SerializeField]
	private Button button;
	private int selectedOption, lvl;
	private List<Service> services;
	private string diasabled = "No researched services";


	private void OnEnable()
	{
		List<string> options = new List<string>();
		List<Service> s = GameManager.Instance.gameData.playerCompany.ResearchedServices;
		services = new List<Service>();

		dropdown.ClearOptions();

		for (int i = 0; i < s.Count; i++)
		{
			if(s[i].Level > 0)
			{
				services.Add(s[i]);
				options.Add(s[i].Type.ToString());
				Debug.Log(" number " + i);
			}
		}

		dropdown.AddOptions(options);
		Debug.Log(dropdown.value);
		HandleListChange(dropdown.value);

		if(options.Count > 0)
		{
			dropdown.interactable = true;
			button.interactable = true;
			// dropdown.AddOptions(new List<string>(new string[]{diasabled})));
		}
	}

	public void HandleButtonPress()
	{
		if(toggle1.isOn)
		{
			lvl = 1;
		}
		else if (toggle2.isOn)
		{
			lvl = 2;
		}
		else if (toggle3.isOn)
		{
			lvl = 3;
		}
		GameManager.Instance.StartBuildingNewService(services[selectedOption].Type, lvl);
	}

	public void HandleListChange(int option)
	{
		selectedOption = option;
		if(services.Count <= 0)
		{
			toggle1.isOn = false;
			toggle1.interactable = false;
			toggle2.interactable = false;
			toggle3.interactable = false;
		}
		else if(services[option].Level == 3)
		{
			toggle1.isOn = true;
			toggle1.interactable = true;
			toggle2.interactable = true;
			toggle3.interactable = true;
		}
		else if (services[option].Level ==2)
		{
			toggle1.isOn = true;
			toggle1.interactable = true;
			toggle2.interactable = true;
			toggle3.interactable = false;
		}
		else if (services[option].Level == 1)
		{
			toggle1.isOn = true;
			toggle1.interactable = true;
			toggle2.interactable = false;
			toggle3.interactable = false;
		}
	}
}
