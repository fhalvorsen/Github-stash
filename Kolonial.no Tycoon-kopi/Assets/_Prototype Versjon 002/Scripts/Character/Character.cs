using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingAndInteracting))]
public class Character : MonoBehaviour
{
	[SerializeField]
	private ProgressBarPro progressBar, fatigueBar;
	[SerializeField]
	private int progressInterval = 1;
	private bool resting;
	public CharacterData characterData;
	public MovingAndInteracting movement;
	protected Interactable interactable;


	private void Awake()
	{
		movement = GetComponent<MovingAndInteracting>();
		movement.character = this;
		resting = false;
	}

	public void Setup(CharacterData data)
	{
		characterData = data;
		Debug.Log("Setup " + gameObject.name);
		AssignJob(characterData.AssignedJob);
	}
	protected virtual void Update()
	{
		
		CheckFatigue();

		fatigueBar.SetValue(characterData.fatigue, 999);

		if(characterData.progress == 0)
			progressBar.gameObject.SetActive(false);
		else
			progressBar.gameObject.SetActive(true);
			
		progressBar.SetValue(characterData.progress, 999);
	}

	private void CheckFatigue()
	{
		
		if (characterData.fatigue < 1 && resting == false)
		{
			
			Debug.Log(resting);			
			StartCoroutine(Rest());
		}
	}

	public int GetFatigue()
	{
		return characterData.fatigue;
	}

	public IEnumerator Rest()
	{
		resting = true;
		movement.MoveToPoint(GameManager.Instance.characterSpawn.transform.position);
		
		yield return new WaitUntil(()=> Vector3.Distance(gameObject.transform.position, GameManager.Instance.characterSpawn.position) < 0.3f);

		Invisible();
		int previousInterval = 0;

		while(characterData.fatigue < 999)
		{
			characterData.fatigue += 110;
			previousInterval = GameManager.Instance.time.GetTime();
			yield return new WaitUntil(()=> GameManager.Instance.time.GetTime() - previousInterval > 1);
		}

		Visible();
		resting = false;
		AssignJob(characterData.AssignedJob);
	}

	public void AssignJob(CharacterData.Job job)
	{
		characterData.AssignedJob = job;
		characterData.progress = 0;
		StopAllCoroutines();
		movement.animator.SetBool("Working", false);

		switch (characterData.AssignedJob)
		{
			case CharacterData.Job.Unassigned:
				return;
			case CharacterData.Job.WarehouseWorker:
				StartCoroutine(NextOrder());
				break;
			case CharacterData.Job.Driver:
				StartCoroutine(StartDelivering());
				break;
		}
	}

	public IEnumerator StartDelivering()
	{
		if(GameManager.Instance.Warehouse.availableVehicles.Count > 0)
		{
			if(GameManager.Instance.Warehouse.deliveryQueue.Count > 0)
			{
				InteractableVehicle vehicle = GameManager.Instance.Warehouse.availableVehicles[0];
				movement.SetInteractionTarget(vehicle);

				yield return new WaitUntil(()=> vehicle.HasInteractedWith(this));
				
				vehicle.drivingVehicle.AddOrderToVehicleDeliveryQueue(GameManager.Instance.Warehouse.TakeOrderFromDeliveryQueue());
				//load the car with groceries
				int lastInterval = 0;
				Debug.Log(vehicle.drivingVehicle.capacity);
				Debug.Log(vehicle.drivingVehicle.ordersToDeliver.Count);
				Debug.Log(GameManager.Instance.Warehouse.orderQueue.Count);
				while(vehicle.drivingVehicle.capacity > vehicle.drivingVehicle.ordersToDeliver.Count
				   && GameManager.Instance.Warehouse.orderQueue.Count > 0)
				{
					if(characterData.progress < 999)
					{
						characterData.progress += 111;
						lastInterval = GameManager.Instance.time.GetTime();
						yield return new WaitUntil(()=> GameManager.Instance.time.GetTime() - lastInterval > 0);
					}
					else
					{				
						characterData.progress = 0;	
						vehicle.drivingVehicle.AddOrderToVehicleDeliveryQueue(GameManager.Instance.Warehouse.TakeOrderFromOrderQueue());
					}
				}
				vehicle.drivingVehicle.gameObject.SetActive(true);
				vehicle.drivingVehicle.warehouseVehicle = vehicle;
				vehicle.drivingVehicle.StartDelivery(this);
				vehicle.gameObject.SetActive(false);
			} 
			else
			{
				Debug.Log(characterData.name + " waiting to drive");
				yield return new WaitForSeconds(1);
				StartCoroutine(StartDelivering());
			} 
		} else 
		{
			Debug.Log("No available vehicles for " + characterData.name);
		}
		
	}	

	public IEnumerator NextOrder()
	{
		if (GameManager.Instance.Warehouse.orderQueue.Count <= 0)
		{
			Debug.Log(characterData.name + " waiting to prepare orders");
			yield return new WaitForSeconds(1);
			StartCoroutine(NextOrder());
		} 
		else 
		{
			Debug.Log(gameObject.name + " Next order");
			int random = Mathf.Clamp(Random.Range(0, GameManager.Instance.AvailableWarehousePoints.Count -1), 0, GameManager.Instance.AvailableWarehousePoints.Count);
			Debug.Log("random: " + random);
			movement.SetInteractionTarget(GameManager.Instance.AvailableWarehousePoints[random]);

			yield return new WaitUntil(()=> GameManager.Instance.AvailableWarehousePoints[random].HasInteractedWith(this));
			StartCoroutine(PrepareOrder());
		}
	}

	public void Invisible()
	{
		gameObject.GetComponent<CapsuleCollider>().enabled = false;
		movement.agent.enabled = false;
		foreach (Transform trans in transform)
		{
			trans.gameObject.SetActive(false);
			
		}
	}

	public void Visible()
	{
		gameObject.GetComponent<CapsuleCollider>().enabled = false;
		movement.agent.enabled = true;
		foreach(Transform trans in transform)
		{
			trans.gameObject.SetActive(true);
		}
	}

	public void RaiseMotivation(int amount)
	{
		characterData.motivationStat = amount;
		
	}

	public void StartPrepareOrder()
	{
		StartCoroutine(PrepareOrder());
	}

	public IEnumerator PrepareOrder()
	{
		
		int lastInterval = 0;
		movement.animator.SetBool("Working", true);
		while(characterData.progress < 999)
		{
			characterData.progress += 100;
			lastInterval = GameManager.Instance.time.GetTime();
			yield return new WaitUntil(()=> GameManager.Instance.time.GetTime() - lastInterval > progressInterval);
		}
		movement.animator.SetBool("Working", false);
		characterData.progress = 0;
		Order order = GameManager.Instance.Warehouse.TakeOrderFromOrderQueue();
		GameManager.Instance.Warehouse.AddOrderToDeliveryQueue(order);
		movement.RemoveInteractionTarget();
		characterData.fatigue -= 54;
		StartCoroutine(NextOrder());
		
	}
}
