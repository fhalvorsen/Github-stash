using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{

	// Lister over alle scriptable objects i spillet
	public List<CharacterData> AvailableStaff;
	public CharacterData player;
	public List<CompanyData> Companies;
	public CompanyData playerCompany;
    public List<CompanyData> Competitors;
	public List<CustomerData> Customers;
	public List<ItemData> Items;
	public List<WholesalerData> Wholesalers;

	public GameData () 
	{
		LoadAllScriptableObjects();
        AddItemsToCompetitors();
	}

	// Loader alle scriptable objects fra Resources mappen inn i listene
	void LoadAllScriptableObjects()
	{
		Object[] objects;

		// Loadler alle scriptable objects for characters inn i en liste
		objects = Resources.LoadAll("Character objects", typeof(CharacterData));
		AvailableStaff = new List<CharacterData>();

		foreach (var i in objects)
		{
			AvailableStaff.Add((CharacterData)i);
		}

		objects = null;

		// Loader alle scriptable objects for companies inn i en liste
		objects = Resources.LoadAll("Company objects", typeof(CompanyData));
		Companies = new List<CompanyData>();

		foreach (var i in objects)
		{
			Companies.Add((CompanyData)i);
		}

		objects = null;

        // Loader alle competitors i en egen liste
        objects = Resources.LoadAll("Company objects", typeof(CompanyData));
        Competitors = new List<CompanyData>();

        foreach (CompanyData i in Companies)
        {
            if (i.GetInstanceID() != 0)
            {
                Competitors.Add(i);
            }
        }

        objects = null;

        // Loader alle scriptable objects for customers inn i en liste
        objects = Resources.LoadAll("Customer objects", typeof(CustomerData));
		Customers = new List<CustomerData>();

		foreach (var i in objects)
		{
			Customers.Add((CustomerData)i);
		}

		objects = null;

		// Loader alle scriptable objects for items inn i en liste
		objects = Resources.LoadAll("Item objects", typeof(ItemData));
		Items = new List<ItemData>();

		foreach (var i in objects)
		{
			Items.Add((ItemData)i);
		}

		objects = null;

		//Loader alle scriptable objects for wholesalers inn i en liste
		objects = Resources.LoadAll("Wholesaler objects", typeof (WholesalerData));
		Wholesalers = new  List<WholesalerData>();

		foreach (var i in objects)
		{
			Wholesalers.Add((WholesalerData)i);
		}
	}

    // Legger til statiske items inn i inventory for competitors
    public void AddItemsToCompetitors()
    {
        foreach (CompanyData competitor in GetCompetitors())
        {
            competitor.inventory.AddItem(0, 1, GetAllItems());
            competitor.inventory.AddItem(1, 1, GetAllItems());
            competitor.inventory.AddItem(2, 1, GetAllItems());
        }
    }

	public CompanyData GetCompany(string name) {
		foreach (CompanyData company in GetAllCompanies()) {
			if (company.name == name) {
				return company;
			}
		}

		return null;
	}

	public List<CharacterData> GetAllCharacters()
	{
		return AvailableStaff;
	}

	public List<CompanyData> GetAllCompanies()
	{
		return Companies;
	}

    public List<CompanyData> GetCompetitors()
    {
        return Competitors;
    }

    public List<CustomerData> GetAllCustomers()
	{
		return Customers;
	}

	public List<ItemData> GetAllItems()
	{
		return Items;
	}

	public List<WholesalerData> GetAllWholesalers()
	{
		return Wholesalers;
	}
}
