using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{

    public CustomerData CustomerData;
    public Building building;


    public void StartShopping()
    {
        StartCoroutine(ShoppingCycle());
    }
    private IEnumerator ShoppingCycle()
    {
        Debug.Log("started shopping cycle");
        int lastInterval = 0;
        Debug.Log(CustomerData.isOrdering);
        while (!CustomerData.isOrdering)
        {
             Debug.Log("ordered");
            SetPreferredCompany();
           
            PlaceOrder();
            
            lastInterval = GameManager.Instance.time.GetTime();
            yield return new WaitUntil (()=> GameManager.Instance.time.GetTime() - lastInterval > CustomerData.orderInterval);
        }
    }

    public void PlaceOrder()
    {
        CustomerData.isOrdering = true;
        Order order = new Order(CustomerData);
        Debug.Log(CustomerData.name + " ordering " + order.OrderSize + " items");
        GameManager.Instance.Warehouse.AddOrderToOrderQueue(order);
    }

    //TODO create a function that sets customerData.OrderPrefference and customerData.DeliveryPrefference
    
    // Remove company from _potentionalCompanies if they do not have an item in their inventory that the customer wants to buy
    public List<CompanyData> CheckInventories()
    {
        if (CustomerData.ShoppingList == null){
            Debug.Log(CustomerData.name + "'s shopping list is empty, no order was made.");
            return null;
        }
        
        List<CompanyData> possibleCompanies = new List<CompanyData>();

        foreach (CompanyData company in GameManager.Instance.gameData.GetAllCompanies())
        {
            possibleCompanies.Add(company);
        }


        foreach (ItemData shoppingListItem in CustomerData.ShoppingList.Items.Keys)
        {
            List<CompanyData> companiesToRemove = new List<CompanyData>();
            foreach (CompanyData company in possibleCompanies)
            {
                if (!company.inventory.Items.ContainsKey(shoppingListItem)
                ||   company.inventory.Items[shoppingListItem] < CustomerData.ShoppingList.Items[shoppingListItem])
                   {
                    if(company.name == GameManager.Instance.gameData.playerCompany.name)
                    {
                        CustomerData.MessageForPlayer = "I would have shopped at " + GameManager.Instance.gameData.playerCompany.name  + ", but they did not have " + shoppingListItem;
                    }
                    companiesToRemove.Add(company);
                }
            }
            foreach (CompanyData company in companiesToRemove)
            {
                possibleCompanies.Remove(company);
            }
        }
        return possibleCompanies;
    }

    public void SetPreferredCompany() 
    {
        List<CompanyData> possibleCompanies = CheckInventories();

        if (possibleCompanies.Count() < 1) 
        {
            CustomerData.PreferedCompany = null;
            CustomerData.MessageForPlayer = "I'm fasting untill I find a grocery store that suits my particular needs.";
            return;
        } 
        else if (possibleCompanies.Count == 1)
        {
            CustomerData.PreferedCompany = possibleCompanies[0];
            CustomerData.MessageForPlayer = "I prefer " + possibleCompanies[0].name + " because they got the stuff I want";
            return;
        }

        if((int)CustomerData.qualityPref > (int)CustomerData.priceRatingPref)
        {
            foreach (CompanyData company in possibleCompanies)
            {
                if((int)company.QualityRating < (int)CustomerData.qualityPref)
                {
                    possibleCompanies.Remove(company);
                    if(company == GameManager.Instance.gameData.playerCompany)
                    {
                        CustomerData.MessageForPlayer = company.name + " is too expensive.";
                    }
                }
            }
        
        }
        else if ((int)CustomerData.priceRatingPref > (int)CustomerData.qualityPref)
        {
            foreach (CompanyData company in possibleCompanies)
            {
                if((int)company.PriceRating < (int)CustomerData.priceRatingPref)
                {
                    possibleCompanies.Remove(company);

                    if(company == GameManager.Instance.gameData.playerCompany)
                    {
                        CustomerData.MessageForPlayer = company.name + " does not have good enough quality products.";
                    }
                }
            }
        }

        int topScore = 0;

        foreach (CompanyData company in possibleCompanies)  
        {
            int companyScore = 0;

            foreach (Service service in company.OrderServices)
            {
                if( service.Type == CustomerData.OrderPrefference)
                {
                    companyScore += service.Score;
                }
                else if (service.Type == CustomerData.DeliveryPrefference)
                {
                    companyScore += service.Score;
                }
            }
            if (companyScore > topScore)
            {
                CustomerData.PreferedCompany = company;
                topScore = companyScore;
            }
        }
        if(CustomerData.PreferedCompany == GameManager.Instance.gameData.playerCompany)
        {
            CustomerData.MessageForPlayer = "I already shop at " + CustomerData.PreferedCompany.name;
        }
    }
}
