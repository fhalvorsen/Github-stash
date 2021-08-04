using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildDropdownItem : MonoBehaviour 
{
	[HideInInspector]
	public Service ResearchedService;
	[SerializeField]
	private Toggle toggle1, toggle2, toggle3;

	public void Setup(Service service)
	{
		ResearchedService = service;
	}

	public void HandleSelect()
	{
		if(ResearchedService.Level == 3)
		{
			toggle1.interactable = true;
			toggle2.interactable = true;
			toggle3.interactable = true;
		}
		else if (ResearchedService.Level ==2)
		{
			toggle1.interactable = true;
			toggle2.interactable = true;
			toggle3.interactable = false;
		}
		else if (ResearchedService.Level == 1)
		{
			toggle1.interactable = true;
			toggle2.interactable = false;
			toggle3.interactable = false;
		}
	}
	
}
