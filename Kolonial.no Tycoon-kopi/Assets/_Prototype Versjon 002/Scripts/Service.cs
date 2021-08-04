using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServiceType
{
    Website,
    App,
    Car,
    PickUpPoint,
    Drone,
    PhysicalOrder,
    PhysicalDelivery
}

public class Service
{
    public int Score, Level;
    public ServiceType Type;

	public Service()
    {
        Score = 0;
        Level = 0;
    }

    // Lager en ny service med eget navn
    public Service(ServiceType type)
    {
        Score = 0;
        Level = 0;
        Type = type;
    }

    public Service(ServiceType type, int score, int level)
    {
        this.Score = score;
        this.Level = level;
        Type = type;
    }

    // Oppgraderer scoren til servicen og legger til gjeldende level
    public void UpgradeService(int newLevel)
    {
        if (newLevel == 1)
        {
            Score = Random.Range(0, 33);
            Level = newLevel;
        } else if (newLevel == 2 && Level == 1)
        {
            Score = Random.Range(34, 66);
            Level = newLevel;
        } else if (newLevel == 3 && Level == 2)
        {
            Score = Random.Range(67, 100);
            Level = newLevel;
        } else
        {
            Debug.Log("Invalid level or invalid upgrade. Must be an int between 1 and 3.");
        }
    }
}
