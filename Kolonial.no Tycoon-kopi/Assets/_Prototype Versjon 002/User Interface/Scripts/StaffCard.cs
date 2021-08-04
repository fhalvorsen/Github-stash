using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StaffCard : MonoBehaviour 
{
	[SerializeField]
	private TextMeshProUGUI staffName, designStat, qualityStat, motivationStat, speedStat;
	[SerializeField]
	private Image portrait;
	private  CharacterData characterData;
	public Color emptyColor;
	public GameObject HireButton;

	public void Setup (CharacterData data)
	{
		foreach (Transform child in transform.GetComponentsInChildren<Transform>())
		{
			child.gameObject.SetActive(true);
		}
		GetComponent<Image>().color = Color.white;
		HireButton.SetActive(false);
		characterData = data;
		staffName.text = characterData.name;
		portrait.sprite = characterData.portrait;
		transform.localScale = Vector3.one;
		designStat.text = characterData.designStat.ToString();
		qualityStat.text = characterData.qualityStat.ToString();
		motivationStat.text = characterData.motivationStat.ToString();
		speedStat.text = characterData.speedStat.ToString();
		transform.localScale = Vector3.one;
	}

	public void SetupStaffSlot()
	{
		foreach (Transform child in transform.GetComponentsInChildren<Transform>())
		{
			child.gameObject.SetActive(false);
		}
		gameObject.SetActive(true);
		HireButton.SetActive(true);
		GetComponent<Image>().color = emptyColor;
		transform.localScale = Vector3.one;
	}

	public void UnassignStaff()
	{
		characterData.AssignedJob = CharacterData.Job.Unassigned;
	}

	public void DeleteStaff()
	{
		GameManager.Instance.Staff.Remove(characterData);
	}
}
