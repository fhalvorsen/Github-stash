using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenus : MonoBehaviour 
{
	[SerializeField]
	private TMP_InputField  companyNameInputField, playerNameInputField;

	public void GetCompanyName(string name)
	{
		Debug.Log(name);
	}

	public void GetPlayerName(string name)
	{
		Debug.Log(name);
	}
}
