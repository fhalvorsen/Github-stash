using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterMotor))]
public class VehicleController : MonoBehaviour {

    public Interactable focus;
	public Interactable Home;

	public Item testItem;
	public Interactable TestCustomer;

    CharacterMotor motor;
	Warehouse_old warehouse;

	public Camera cityCam;

	Queue<ItemList> deliveryList;
    

	// Use this for initialization
	void Start () {
        motor = GetComponent<CharacterMotor>();
		warehouse = Warehouse_old.instance;
	}
	


    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                // focus.OnDefocused();

            focus = newFocus;
            // focus.OnDefocused();
            motor.MoveToPoint(newFocus.interactionTransform.position);
        }

        // newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            // focus.OnDefocused();

        focus = null;
    }

	public void DoDelivery() {
		StartCoroutine("Delivery");
	}

	public IEnumerator Delivery()
	{
		yield return new WaitForSeconds (2);

		if (deliveryList.Count == 0) {
			SetFocus (Home);
		} else {
			SetFocus (deliveryList.Dequeue()._customer);
		}
		Debug.Log ("delivering to" + focus);
	}
		

	public void StartDelivery()
	{

//		deliveryList = GenerateTest ();

		deliveryList = warehouse.GetDeliveries ();
		Debug.Log ("Im going to deliver " + deliveryList.Count + " orders");

		foreach (var item in deliveryList) {
			Debug.Log("One order en route to " + item._customer);
		}
		


		StartCoroutine ("Delivery");

		cityCam.gameObject.SetActive (true);
		GameObject.Find ("WarehouseStage01").gameObject.transform.GetChild (1).gameObject.SetActive (false);

	}


	List<ItemList> GenerateTest()
	{
		List<Item> itemList = new List<Item> ();
		itemList.Add (testItem);
		itemList.Add (testItem);
		itemList.Add (testItem);
		itemList.Add (testItem);
		ItemList testList = new ItemList (itemList, TestCustomer);
		List<ItemList> testDeliveries = new List<ItemList>();
		testDeliveries.Add(testList);
		return testDeliveries;
	}
}
