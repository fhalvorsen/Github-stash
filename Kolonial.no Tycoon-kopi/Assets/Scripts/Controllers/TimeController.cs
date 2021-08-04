using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

	public Text TimeDisplay;
	public float TimeMinutes { get; private set; }
	public int TimeHours { get; private set; }
	public int TimeDays{ get; private set; }
	public int TimeWeeks { get; private set; }
	//public int ScaleFactor = 1000;

	
	private enum TimeWeekdays {
		Mandag,
		Tirsdag,
		Onsdag,
		Torsdag,
		Fredag,
		Lørdag,
		Søndag
	}
	
	private int _weekEnd = (int) TimeWeekdays.Søndag;

	private bool isPaused = false;
	
	// Use this for initialization

	private void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	private void Start () {
		TimeWeeks = 1;
	}
	
	// Update is called once per frame
	public void Update () {
		var day = (TimeWeekdays) TimeDays;
		TimeMinutes += Time.deltaTime*6;
		if (TimeMinutes >= 60) {
			TimeMinutes = 0;
			TimeHours++;
			if (TimeHours >= 24) {
				TimeHours = 0;
				TimeDays++;
				if (TimeDays > _weekEnd) {
					TimeDays = 0;
					TimeWeeks++;
				}
			}
			
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			PauseGame ();
		}
	}

	public string GetTimeToString () {
		var day = (TimeWeekdays) TimeDays;
		string timeText = "" + day + ", Uke: " + TimeWeeks + ", " + TimeHours.ToString("00") + ":" + TimeMinutes.ToString("00");
		return timeText;
	}

	public void PauseGame () {
		//Time.timeScale = Time.timeScale != 0f ? 0f : 1f;
		if (isPaused == true) {
			Time.timeScale = 1;
			isPaused = false;
		} else {
			Time.timeScale = 0;
			isPaused = true;
		}
	}
}
