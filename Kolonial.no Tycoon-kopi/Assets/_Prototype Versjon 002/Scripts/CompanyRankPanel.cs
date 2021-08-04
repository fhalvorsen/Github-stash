using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CompanyRankPanel : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI firstplace, secondplace, thirdplace, fourthplace, playerRank;
	
	[SerializeField]
	private Image Image;
	[SerializeField]
	private GameObject child1, child2;
	

	void Update () 
	{
		if(GameManager.Instance.gameData.playerCompany.customerCount < 1)
		{
			 Image.enabled = false;
			 child1.SetActive(false);
			 child2.SetActive(false);
			 return;
		}
		else
		{
			Image.enabled = true;
			child1.SetActive (true);
			child2.SetActive(true);
		}
		
		var companiesDecending = from element in GameManager.Instance.gameData.Companies
						orderby element.customerCount descending
						select element;

	
		firstplace.text = companiesDecending.ElementAt(0).name;	
		firstplace.color = companiesDecending.ElementAt(0).companyColor;
		
		secondplace.text = companiesDecending.ElementAt(1).name;
		secondplace.color = companiesDecending.ElementAt(1).companyColor;
		
		thirdplace.text = companiesDecending.ElementAt(2).name;
		thirdplace.color = companiesDecending.ElementAt(2).companyColor;


		if(companiesDecending.ElementAt(0) == GameManager.Instance.gameData.playerCompany 
		|| companiesDecending.ElementAt(1) == GameManager.Instance.gameData.playerCompany
		|| companiesDecending.ElementAt(2) == GameManager.Instance.gameData.playerCompany
		|| companiesDecending.ElementAt(3) == GameManager.Instance.gameData.playerCompany)
		{
			fourthplace.text = companiesDecending.ElementAt(3).name;
			fourthplace.color = companiesDecending.ElementAt(3).companyColor;
			playerRank.text = "4.";
		} 
		else 
		{
			for (int i = 0; i < companiesDecending.Count(); i++)
			{
				if(companiesDecending.ElementAt(i) == GameManager.Instance.gameData.playerCompany)
				{
					playerRank.text = i.ToString() + ".";
					fourthplace.color = GameManager.Instance.gameData.playerCompany.companyColor;
					fourthplace.text = GameManager.Instance.gameData.playerCompany.name;	
				}
			}
		}
			
			
			
		
	}
}
