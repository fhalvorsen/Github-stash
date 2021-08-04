using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//@author Tobben00
public class Money_Sys : MonoBehaviour 
{
	float kroner;
	int kunder;

	public int _cashFlowCase; // Forteller hvor i casen vi er

	public static int _boughtState = 1; // Kunde kjøpt vare
	public static int _soldState = 2; // Penger inn på konto
	public static int _restockState = 3; // Varer må oppdateres 

	#region Singleton

	public static Money_Sys instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one instance of Money_Sys found");
			return;
		}
		instance = this;
	}

	#endregion

	public delegate void OnCashChanged();
	public OnCashChanged onCashChangedCallback;

    /*
     	Planen med Money_Sys er å holde rede på alle intekter som eks kunder, frakt og
     	setter dette sammen i en routine for å simulere salg før man faktisk tjener penger.
		Etter hvor mange kunder man har blir totale inntekt endret.
	*/


	// Inint skjer her
	void Start () 
	{
		kroner = 35000;
		kunder = 22;
	}

	public void AddKroner(float amount)
	{
		kroner += amount;

		if (onCashChangedCallback != null)
			onCashChangedCallback.Invoke();
	}

	public void SubtractKroner(float amount)
	{
		kroner -= amount;

		if (onCashChangedCallback != null)
			onCashChangedCallback.Invoke();
	}

	public float GetKroner(){
		return kroner;
	}
		
	// Update skjer per frame
	void Update () 
	{
		//getBalance();
	}

	void cashFlow()
	{
		switch(_cashFlowCase)
		{
		case 1:
			Debug.Log ("Her selges varer men penger er enda ikke motatt");
			AddKroner(kroner * kunder / 100f);
			_cashFlowCase = 2;
			break;

		case 2:
			Debug.Log ("Her bekreftest salget og penger er satt inn på konto");
			_cashFlowCase = 3;
			break;

		case 3:
			Debug.Log ("Her må varene fylles på nytt");
			_cashFlowCase = 1;
			break;

		default:
			Debug.Log ("Her startet loopen DEFAULT");
			_cashFlowCase = 1;
			break;

		}
	}

	public float customerGenerateBalance()
	{
		float customerBalanceMath = kroner * kunder / 100;
		float retur = customerBalanceMath;
			 
		return retur;
	}

	IEnumerator idleTimer()
	{
		bool looptimer = true;
		while(looptimer)
		{
			looptimer = false;
			cashFlow();
			yield return new WaitForSeconds(2f);
			cashFlow();
			looptimer = true;
		}
	} 
}
