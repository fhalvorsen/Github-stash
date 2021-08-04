using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Interactable {
	public CustomerData[] ResidentData;
    private List<Customer> Residents;
    

	void Start () 
    {
        Residents = new List<Customer>();
        for (int i = 0; i < ResidentData.Length; i++) 
        {
            GameObject go = new GameObject(ResidentData[i].name);
            go.transform.SetParent(gameObject.transform);
            Customer customer = go.AddComponent<Customer>();
            
            customer.building = this;
            customer.CustomerData = ResidentData[i];
            customer.CustomerData.isOrdering = false;
            customer.StartShopping();
            Residents.Add(customer);
            Debug.Log("Added" + customer.CustomerData.name + " to " + gameObject.name);
        }
	}
}
