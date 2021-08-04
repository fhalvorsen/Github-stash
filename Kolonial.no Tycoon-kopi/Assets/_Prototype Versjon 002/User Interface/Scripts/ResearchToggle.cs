using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResearchToggle : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI label;
	[HideInInspector]
	public ServiceType Type;
	[HideInInspector]
	public int Lvl;

	public void Setup(ServiceType type, int lvl)
	{
		Type = type;
		Lvl = lvl;
		label.text = Type.ToString() + " lvl " + Lvl.ToString();
		transform.localScale = Vector3.one;
	}
}
