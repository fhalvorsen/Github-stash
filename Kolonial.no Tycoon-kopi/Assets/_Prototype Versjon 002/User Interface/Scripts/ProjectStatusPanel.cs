using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectStatusPanel : MonoBehaviour 
{
	private Service currentService;
	[SerializeField]
	private TextMeshProUGUI nameLabel, problemCount, designCount, qualityCount, researchCount;
	[SerializeField]
	private GameObject finishButton;
	[SerializeField]
	private Image image;
	[SerializeField]
	private ProgressBarPro progressBar;

	private void Update()
	{
		currentService = GameManager.Instance.currentProject;

		if(GameManager.Instance.researchPoints == 0)
		{
			researchCount.transform.parent.gameObject.SetActive(false);
		} 
		else
		{
			researchCount.transform.parent.gameObject.SetActive(true);
		}
		
		if(currentService == null)
		{
			image.enabled = false;
			problemCount.transform.parent.gameObject.SetActive(false);
			designCount.transform.parent.gameObject.SetActive(false);
			qualityCount.transform.parent.gameObject.SetActive(false);
			nameLabel.gameObject.SetActive(false);
			progressBar.gameObject.SetActive(false);
			finishButton.SetActive(false);
			return;
		} 
		else 
		{
			image.enabled = true;
			problemCount.transform.parent.gameObject.SetActive(true);
			designCount.transform.parent.gameObject.SetActive(true);
			qualityCount.transform.parent.gameObject.SetActive(true);
			nameLabel.gameObject.SetActive(true);
			progressBar.gameObject.SetActive(true);
			finishButton.SetActive(true);
		}


		nameLabel.text= currentService.Type.ToString() + " lvl " + currentService.Level.ToString();

		problemCount.text = GameManager.Instance.problemPoints.ToString();
		designCount.text = GameManager.Instance.designPoints.ToString();
		qualityCount.text = GameManager.Instance.qualityPoints.ToString();
		researchCount.text = GameManager.Instance.researchPoints.ToString();
		progressBar.SetValue(GameManager.Instance.projectProgress, 100);

		if (GameManager.Instance.projectDone)
		{
			finishButton.gameObject.SetActive(true);
		}
		else
		{
			finishButton.gameObject.SetActive(false);
		}

	}
}
