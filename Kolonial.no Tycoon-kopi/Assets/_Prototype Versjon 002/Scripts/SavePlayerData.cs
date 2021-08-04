using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerData : MonoBehaviour {

    GameManager manager;

    public List<CharacterData> charList;
    public List<CustomerData> customerList;
    public List<CompanyData> companyList;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        charList = manager.gameData.GetAllCharacters();
        customerList = manager.gameData.GetAllCustomers();
        companyList = manager.gameData.GetAllCompanies();

    }

    public void SavePosition()
	{
        //Save Player Position
		PlayerPrefs.SetFloat("PlayerX", transform.position.x);
		PlayerPrefs.SetFloat("PlayerY", transform.position.y);
		PlayerPrefs.SetFloat("PlayerZ", transform.position.z);

        //Save Character Stats
        foreach (CharacterData character in manager.gameData.GetAllCharacters()) {
            
            PlayerPrefs.SetInt("fatigue" + character.GetInstanceID().ToString(), character.fatigue);
            PlayerPrefs.SetInt("motivation" + character.GetInstanceID().ToString(), character.motivationStat);
            PlayerPrefs.SetInt("quality" + character.GetInstanceID().ToString(), character.qualityStat);
            PlayerPrefs.SetInt("speed" + character.GetInstanceID().ToString(), character.speedStat);
            PlayerPrefs.SetInt("design" + character.GetInstanceID().ToString(), character.designStat);
            PlayerPrefs.SetInt("research" + character.GetInstanceID().ToString(), character.researchStat);
        }

        //Save Money And Time
        PlayerPrefs.SetFloat("money", manager.playerBalance);
        PlayerPrefs.SetInt("time", manager.time.GetTime());

        //Save Company Stats
        /*
        foreach (CompanyData company in manager.gameData.GetAllCompanies()) {

            PlayerPrefs.SetInt("websiteScore" + company.GetInstanceID().ToString(), company.websiteScore);
            PlayerPrefs.SetInt("appScore" + company.GetInstanceID().ToString(), company.appScore);
            PlayerPrefs.SetInt("carScore" + company.GetInstanceID().ToString(), company.carScore);
            PlayerPrefs.SetInt("pupScore" + company.GetInstanceID().ToString(), company.PuPScore);
            PlayerPrefs.SetInt("droneScore" + company.GetInstanceID().ToString(), company.droneScore);
            PlayerPrefs.SetInt("physScore" + company.GetInstanceID().ToString(), company.physicalScore);
        } */

        //Save Customer Stats
        foreach (CustomerData customer in manager.gameData.GetAllCustomers()) {
            PlayerPrefs.SetInt("orderPref" + customer.GetInstanceID().ToString(), (int)customer.OrderPrefference);
            PlayerPrefs.SetInt("websitePref" + customer.GetInstanceID().ToString(), customer.websitePrefScore);
            PlayerPrefs.SetInt("appPref" + customer.GetInstanceID().ToString(), customer.appPrefScore);
            PlayerPrefs.SetInt("physPref" + customer.GetInstanceID().ToString(), customer.physOrderPrefScore);
            PlayerPrefs.SetInt("deliveryPref" + customer.GetInstanceID().ToString(), (int)customer.DeliveryPrefference);
            PlayerPrefs.SetInt("carPref" + customer.GetInstanceID().ToString(), customer.carPrefScore);
            PlayerPrefs.SetInt("physDeliveryPref" + customer.GetInstanceID().ToString(), customer.physDeliveryPrefScore);
            PlayerPrefs.SetInt("qualityPref" + customer.GetInstanceID().ToString(), (int)customer.qualityPref);
            PlayerPrefs.SetInt("priceRatingPref" + customer.GetInstanceID().ToString(), (int)customer.priceRatingPref);
            PlayerPrefs.SetFloat("orderInt" + customer.GetInstanceID().ToString(), customer.orderInterval);

        }

        //Save Game Confirmation
		Debug.Log("Game Successfully Saved.");
	}

	public void LoadPosition()
	{
        //Load Player Position
		float x = PlayerPrefs.GetFloat("PlayerX");
		float y = PlayerPrefs.GetFloat("PlayerY");
		float z = PlayerPrefs.GetFloat("PlayerZ");

        //Load Character Stats
        foreach (CharacterData character in manager.gameData.GetAllCharacters())
        {
            character.fatigue = PlayerPrefs.GetInt("fatigue" + character.GetInstanceID().ToString());
            character.motivationStat = PlayerPrefs.GetInt("motivation" + character.GetInstanceID().ToString());
            character.qualityStat = PlayerPrefs.GetInt("quality" + character.GetInstanceID().ToString());
            character.speedStat = PlayerPrefs.GetInt("speed" + character.GetInstanceID().ToString());
            character.designStat = PlayerPrefs.GetInt("design" + character.GetInstanceID().ToString());
            character.researchStat = PlayerPrefs.GetInt("research" + character.GetInstanceID().ToString());

        }

        //Load Money And Time
        manager.playerBalance = PlayerPrefs.GetFloat("penger");
        manager.time.SetTime(PlayerPrefs.GetInt("time"));

        //Load Company Stats
        /*
        foreach (CompanyData company in manager.gameData.GetAllCompanies()) {
            company.websiteScore = PlayerPrefs.GetInt("websiteScore" + company.GetInstanceID().ToString());
            company.appScore = PlayerPrefs.GetInt("appScore" + company.GetInstanceID().ToString());
            company.carScore = PlayerPrefs.GetInt("carScore" + company.GetInstanceID().ToString());
            company.PuPScore = PlayerPrefs.GetInt("pupScore" + company.GetInstanceID().ToString());
            company.droneScore = PlayerPrefs.GetInt("droneScore" + company.GetInstanceID().ToString());
            company.physicalScore = PlayerPrefs.GetInt("physScore" + company.GetInstanceID().ToString());
        } */

        //Load Customer Stats
        foreach (CustomerData customer in manager.gameData.GetAllCustomers()) {
            customer.OrderPrefference = (ServiceType)PlayerPrefs.GetInt("orderPref" + customer.GetInstanceID().ToString());
            customer.websitePrefScore = PlayerPrefs.GetInt("websitePref" + customer.GetInstanceID().ToString());
            customer.appPrefScore = PlayerPrefs.GetInt("appPref" + customer.GetInstanceID().ToString());
            customer.physOrderPrefScore = PlayerPrefs.GetInt("physPref" + customer.GetInstanceID().ToString());
            customer.DeliveryPrefference = (ServiceType) PlayerPrefs.GetInt("deliveryPref" + customer.GetInstanceID().ToString());
            customer.carPrefScore = PlayerPrefs.GetInt("carPref" + customer.GetInstanceID().ToString());
            customer.physDeliveryPrefScore = PlayerPrefs.GetInt("physDeliveryPref" + customer.GetInstanceID().ToString());
            customer.qualityPref = (Quality) PlayerPrefs.GetInt("qualityPref" + customer.GetInstanceID().ToString());
            customer.priceRatingPref = (PriceRating) PlayerPrefs.GetInt("priceRatingPref" + customer.GetInstanceID().ToString());
            customer.orderInterval = PlayerPrefs.GetFloat("orderInt" + customer.GetInstanceID().ToString());
        }

        //Move player to saved position
		transform.position = new Vector3(x, y, z);

        //Load Game Confirmation
		Debug.Log("Game Successfully Loaded.");

	}
}
