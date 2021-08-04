using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageTurner : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI bodyText;
	[SerializeField]
	private GameObject panel;


	public void TurnPageOrContinue()
	{
		if (bodyText.pageToDisplay < bodyText.textInfo.pageCount)
		{
			bodyText.pageToDisplay ++;
		} 
		else 
		{
			panel.SetActive(false);
		}
	}

}
