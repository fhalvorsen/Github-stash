﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{

	
	// Klasse som inneholder all informasjon til spillet
	public GameData gameData;
	public List<CharacterData> Staff;
    public Warehouse Warehouse;
	public Transform characterSpawn;
	public List<InteractableWarehouseStation> AvailableWarehousePoints = new List<InteractableWarehouseStation>(); 
    public float playerBalance;
    public int customerCount;
	[HideInInspector]
    public IGTime time;
    [HideInInspector]
    public Service currentProject;
    [HideInInspector]
    public int projectProgress, researchPoints, designPoints, qualityPoints, problemPoints;
    [HideInInspector]
    public bool projectDone = false;
	private SimpleObjectPool characterPool;
	[SerializeField]
	private GameObject playerObject;
    [HideInInspector]
    public PlayerCharacter PlayerScript;
	[SerializeField]
	private CharacterData playerData;
    [SerializeField]
    private CompanyData playerCompany;

    private bool gameOver;

    public static GameManager Instance {
		get {
			return ((GameManager)mInstance);
		} set {
			mInstance = value;
		}
	}

    void Awake()
    {    
		gameData = new GameData();
        time = GetComponent<IGTime>();
        Warehouse = new Warehouse();
		characterPool = GetComponent<SimpleObjectPool>();
        gameOver = false;

        gameData.playerCompany = playerCompany;

        // GenerateServicesWithScores();
    }

	void Start ()
    {
        InitializeStaff();
        // GenerateTestOrders();
        // StartBuildingNewService(ServiceType.Drone, 1);
    }

    private void Update()
    {
        
    }
    
    private void GenerateTestOrders()
    {
        foreach (CustomerData customer in gameData.GetAllCustomers())
        {
            Order order = new Order(customer);
            Warehouse.AddOrderToOrderQueue(order);
			
        }
    }

    // Test funksjon for å sjekke om orders funker ordentlig
    private void GenerateServicesWithScores()
    {
        foreach (CompanyData company in gameData.GetAllCompanies())
        {
            company.OrderServices.Clear();
            company.DeliveryServices.Clear();
        }

        foreach (CompanyData company in gameData.GetAllCompanies())
        {
            if (company.name == "Kolonial") {
                company.OrderServices.Add(new Service(ServiceType.App, 50, 1));
                company.OrderServices.Add(new Service(ServiceType.Website, 50, 1));
                company.OrderServices.Add(new Service(ServiceType.PhysicalOrder, 50, 1));

                company.DeliveryServices.Add(new Service(ServiceType.Car, 50, 1));
                company.DeliveryServices.Add(new Service(ServiceType.PhysicalDelivery, 50, 1));
            }

            if (company.name == "Keewee") {
                company.OrderServices.Add(new Service(ServiceType.App, 30, 1));
                company.OrderServices.Add(new Service(ServiceType.Website, 30, 1));
                company.OrderServices.Add(new Service(ServiceType.PhysicalOrder, 50, 1));

                company.DeliveryServices.Add(new Service(ServiceType.Car, 30, 1));
                company.DeliveryServices.Add(new Service(ServiceType.PhysicalDelivery, 30, 1));
            }
        }


    }

    private void InitializeStaff()
    {
        foreach (CharacterData data in Staff)
        {
            GameObject characterObject = characterPool.GetObject();
            characterObject.transform.position = characterSpawn.position;
            characterObject.transform.rotation = characterSpawn.rotation;
            Character characterScript = characterObject.GetComponent<Character>();
            characterScript.Setup(data);
        }
		
		GameObject player = Instantiate(playerObject);        
		
		player.transform.position = characterSpawn.position;
		player.transform.rotation = characterSpawn.rotation;
		PlayerScript = player.GetComponent<PlayerCharacter>();
		PlayerScript.Setup(playerData);
    }

	public void SpawnStaffCharacter()
	{
		
	}

    public void AssignPlayerWarehouse()
    {
        PlayerScript.AssignJob(CharacterData.Job.WarehouseWorker);
    }

    public void StartBuildingNewService(ServiceType type, int lvl)
    {
        currentProject = new Service(type, 0, lvl);
        projectProgress = 0;
        StartCoroutine(BuildingNewService());
    }

    private IEnumerator BuildingNewService()
    {
        int lastInterval = 0;
        
        // Building phases
        while (projectProgress < 100)
        {
            problemPoints += Random.Range(-1, 2);
            if(problemPoints < 0)
                problemPoints = 0;
            researchPoints += Mathf.Clamp( Random.Range(-1, 2), 0,1);
            designPoints += Random.Range(0,2);
            qualityPoints += Random.Range(0,2);
            projectProgress ++;
            lastInterval = time.GetTime();
            
            yield return new WaitUntil(()=> time.GetTime() - lastInterval > 1);
        }

        currentProject.Score = designPoints + qualityPoints - problemPoints;
       projectDone = true;

       // After the new service is done, solving problems. 
       while (problemPoints > 0)
       {
           problemPoints += Random.Range(-2, 1);
           researchPoints += Mathf.Clamp( Random.Range(-3, 2), 0,1);
           designPoints += Mathf.Clamp( Random.Range(-3, 2), 0,1);
           qualityPoints += Mathf.Clamp( Random.Range(-3, 2), 0,1);

           currentProject.Score = designPoints + qualityPoints - problemPoints;

           lastInterval = time.GetTime();

           yield return new WaitUntil(()=> time.GetTime() - lastInterval > 1);
       }

        // Add the new service to company data
       for (int i = 0; i < gameData.playerCompany.OrderServices.Count; i++)
       {
           if(gameData.playerCompany.OrderServices[i].Type == currentProject.Type)
            {
                gameData.playerCompany.OrderServices.RemoveAt(i);
                gameData.playerCompany.OrderServices.Add(currentProject);
            }
       }
       for (int i = 0; i < gameData.playerCompany.DeliveryServices.Count; i++)
       {
           if (gameData.playerCompany.DeliveryServices[i].Type == currentProject.Type)
           {
               gameData.playerCompany.DeliveryServices.RemoveAt(i);
               gameData.playerCompany.DeliveryServices.Add(currentProject);
           }
       }

    }
}
