using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (MovingAndInteracting))]
public class Vehicle : MonoBehaviour
{
    NavMeshAgent agent;
    public InteractableWarehouse warehouse;
    [HideInInspector]
    public InteractableVehicle warehouseVehicle;
    public Character driver;
    private MovingAndInteracting movement;
    public int speed;
    public int capacity;
    public int itemsInVehicle;
    //For each delivery
    //ItemsInVehicle -= _ordersToDeliver.Peek().OrderSize;
    [SerializeField]
    public Queue<Order> ordersToDeliver;
    public bool available = true;
    private bool nextOrder = true;

   private void Awake()
   {
       movement = GetComponent<MovingAndInteracting>();
       
      
       ordersToDeliver = new Queue<Order>();
        agent = GetComponent<NavMeshAgent>();   
        agent.speed = speed;  
   }
   
    void Start()
    {
              
    }
    
    public void StartDelivery(Character character)
    {    
        driver = character;

            
        driver.transform.position = transform.position;
       driver.transform.SetParent(gameObject.transform);
       movement.character = driver;
       
       driver.Invisible();
        //_assignedStaff = staff;
        StartCoroutine(DoDeliveries());
        available = false;
    }

    private IEnumerator DoDeliveries()
    {
        while (ordersToDeliver.Count > 0)
        {
            Debug.Log("Driving to " + ordersToDeliver.Peek().Customer.name);
            Building destination = ordersToDeliver.Peek().Customer.building;
            movement.SetInteractionTarget(destination);
            yield return new WaitUntil(()=> destination.HasInteractedWith(driver));
            StartCoroutine(DeliverOrder());
        }
        FinishDelivery();
    }

    public IEnumerator FinishDelivery()
    {
        movement.SetInteractionTarget(warehouse);

        yield return new WaitUntil(()=> warehouse.HasInteractedWith(driver));
        gameObject.SetActive(false);
        driver.Visible();
        driver.transform.position = warehouseVehicle.interactionTransform.position;
        driver.transform.rotation = warehouseVehicle.interactionTransform.rotation;
        driver.characterData.fatigue -= 110;
        driver = null;
    }

    public void AddOrderToVehicleDeliveryQueue(Order order)
    {
        ordersToDeliver.Enqueue(order);
    }


    private IEnumerator DeliverOrder()
    {
        nextOrder = false;

        // Customer is no longer ordering
        Debug.Log("Delivered order for: " + ordersToDeliver.Peek().Customer.name);
        yield return new WaitForSeconds(5);
        ordersToDeliver.Peek().Customer.isOrdering = false;
        itemsInVehicle -= ordersToDeliver.Peek().OrderSize;
        ordersToDeliver.Dequeue();

        nextOrder = true;
    }
}
