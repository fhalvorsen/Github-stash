using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour 
{
	public void LoadMainScene()
	{
		SceneManager.LoadScene("Start");
		SceneManager.LoadScene("City", LoadSceneMode.Additive);
	}
}
