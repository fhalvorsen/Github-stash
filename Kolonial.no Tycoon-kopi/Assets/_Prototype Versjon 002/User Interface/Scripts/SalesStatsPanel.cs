using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SalesStatsPanel : MonoBehaviour 
{	
	[SerializeField]
	private Color negativeColor, positiveColor;
	[SerializeField]
	private Image image;
	[SerializeField]
	private ProgressBarPro today, yesterday, twodays, threedays, fourdays, fivedays, sixdays;
	[SerializeField]
	private TextMeshProUGUI todayCount, yesterdayCount, twodaysCount, threedaysCount, fourdaysCount, fivedaysCount, sixdaysCount;
	private int dayLastFrame, yesterdaySales, twodaySales, threedaySales, fourdaySales, fivedaySales, sixdaySales, currentSaleCeiling;

	private void Update()
	{
		int salesCount = GameManager.Instance.gameData.playerCompany.salesCount;
		if(salesCount < 1)
		{
			image.enabled = false;
			today.gameObject.SetActive(false);
			yesterday.gameObject.SetActive(false);
			twodays.gameObject.SetActive(false);
			threedays.gameObject.SetActive(false);
			fourdays.gameObject.SetActive(false);
			fivedays.gameObject.SetActive(false);
			sixdays.gameObject.SetActive(false);
			return;
		}
		else
		{
			image.enabled = true;
			today.gameObject.SetActive(true);
			yesterday.gameObject.SetActive(true);
			twodays.gameObject.SetActive(true);
			threedays.gameObject.SetActive(true);
			fourdays.gameObject.SetActive(true);
			fivedays.gameObject.SetActive(true);
			sixdays.gameObject.SetActive(true);
		}

		int hour = GameManager.Instance.time.GetTime();
		int day = (int)Mathf.Floor(hour / 24);
		Debug.Log ("day: " + day);
		Debug.Log ("dayLastframe: " + dayLastFrame);
		Debug.Log("Ceilling: " + currentSaleCeiling);
		
		if(day == dayLastFrame)
		{
			int todaySales = salesCount - yesterdaySales;
			Debug.Log("todaySales: " + todaySales);
			if(todaySales > currentSaleCeiling)
			{
				currentSaleCeiling = todaySales;
			}
			today.SetValue(todaySales, currentSaleCeiling);
			todayCount.text = todaySales.ToString();
			Debug.Log("ready");
			dayLastFrame =  day;
			Debug.Log("set");
		}
		else 
		{
			sixdaySales = fivedaySales;
			fivedaySales = fourdaySales;
			fourdaySales = threedaySales;
			threedaySales = twodaySales;
			twodaySales = yesterdaySales;
			yesterdaySales = salesCount - yesterdaySales;
			Debug.Log("yesterdaySales: " + yesterdaySales);
			yesterday.SetValue(yesterdaySales, currentSaleCeiling);
			if(yesterdaySales < twodaySales) yesterday.SetBarColor (negativeColor); else yesterday.SetBarColor (positiveColor);
			yesterdayCount.text = yesterdaySales.ToString();

			twodays.SetValue(twodaySales, currentSaleCeiling);
			if(twodaySales < threedaySales) twodays.SetBarColor (negativeColor); else twodays.SetBarColor (positiveColor);
			twodaysCount.text = twodaySales.ToString();

			threedays.SetValue(threedaySales, currentSaleCeiling);
			if(threedaySales < fourdaySales) threedays.SetBarColor (negativeColor); else threedays.SetBarColor (positiveColor);
			threedaysCount.text = threedaySales.ToString();

			fourdays.SetValue(fourdaySales, currentSaleCeiling);
			if(fourdaySales < fivedaySales)fourdays.SetBarColor (negativeColor);  else fourdays.SetBarColor (positiveColor);
			fourdaysCount.text = fourdaySales.ToString();

			fivedays.SetValue(fivedaySales, currentSaleCeiling);
			if(fivedaySales < sixdaySales) fivedays.SetBarColor (negativeColor); else fivedays.SetBarColor (positiveColor);
			fivedaysCount.text = fivedaySales.ToString();

			sixdays.SetValue(twodaySales, currentSaleCeiling);
			sixdaysCount.text = sixdaySales.ToString();
			
			dayLastFrame = day;
		}

	}

}
