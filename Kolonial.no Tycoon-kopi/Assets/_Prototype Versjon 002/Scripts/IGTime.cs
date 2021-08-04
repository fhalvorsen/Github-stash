using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGTime : MonoBehaviour
{
	private int _hours;
    public float ScaleFactor = 1.0f;
    private float _time;
    private bool _isPaused = false;

	// Use this for initialization
	void Awake () 
    {
        DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        //hour updates once every 10 sec by default
        //ScaleFactor scales this so that ex. ScaleFactor = 2 will update hour once every 5 sec
        _time += (Time.deltaTime * 6) * ScaleFactor;
		if(_time >= 60)
        {
            _hours++;
            _time = 0;
        }
	}

    public void TogglePause()
    {
        if(_isPaused == false) { Time.timeScale = 0; }
        else if(_isPaused == true) { Time.timeScale = 1; }
    }

	public int GetTime()
	{
		return _hours;
	}

    public void SetTime(int i){
         _hours = i;
    }
}
