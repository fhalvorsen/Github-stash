using System.Collections.Generic;
using UnityEngine;

public class CustomerInfo : MonoBehaviour 
{

	public List <string> customerList = new List <string>();
	public string[] randomFirstNames;
	public string[] randomLastNames;

	// Use this for initialization
	void Start () 
	{

		string[] randomFirstNames = {"Halvaard", "Jhon", "Bob", "Anders", "Hasan", "Ando", "Alex", "Sultan", "Shalty", "Kristoffer", 
			"Adrian", "Alan", "Homer", "Anette", "Mellanie", "Jenny", "Michelle", "Amalie", "Eirin", "Pernille", "Bobby", "Gunnar",
		"Tron", "Jonas", "Petter", "Kari", "Pål", "Henriette", "Henrik", "Thomas", "Knut", "Pelle", "Bernard", "Kristian", "Sturle",
	"Bendikte", "Josef", "Arnold", "Benjamin", "Venessa", "Ali", "Atle"};

		string[] randomLastNames = {"Kupfeer", "Petty", "Mars", "Flanke", "Ballestaad", "Brown", "Ahmed", "Aas", "Amble", "Bjørkås", 
		"Bjergset", "breen", "Brubakken", "Olsen", "Opland", "Moe", "Monsen"};

		customerList.Add("Torvald");
		customerList.Add("Arnold");

		generateRandomCustomer();

	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	void generateRandomCustomer()
    	{
		// Generates a customer at random and place it in the list       
		int indexF = Random.Range (0, randomFirstNames.Length);
		int indexL = Random.Range (0, randomLastNames.Length);

		string firstName = randomFirstNames[indexF];
		string lastName = randomLastNames[indexL];

		Debug.Log(firstName + lastName);
	}

	void getAllCustomerInfo()
    	{
		//Get all customers from list and print on screen
		for (int i = 0; i < customerList.Count; i++) {
            //customerList.Push
            //customerList.Contains(i);
            for (int j = 0; j < customerList.Count; j++)
            {
                //customerList.Push
                //customerList.Contains(customerList[j]);
            }
		}
	}

	void removeCustomerInfo()
    	{
		//remove a customer from the list
	}
}

