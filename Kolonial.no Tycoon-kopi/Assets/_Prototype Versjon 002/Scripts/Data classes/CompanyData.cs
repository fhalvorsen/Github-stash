using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Company", menuName = "Scriptable Object/New Company object")]
public class CompanyData : ScriptableObject
{
    [System.Serializable]
    public class ItemInfo
    {
        [SerializeField] public ItemData item;
        [SerializeField] public int amount;
    }

    public List<ItemInfo> inventoryList = new List<ItemInfo>();
    [SerializeField]
    public ItemDictionary inventory = new ItemDictionary();
    public Quality QualityRating = Quality.Okay;
    public PriceRating PriceRating = PriceRating.Normal;
    new public string name = "New company";
    [SerializeField][Range(0,100)]
    private int websitScore, appScore, carScore, PuPScore, droneScore, physicalOrderScore, physicalDeliveryScore;
    [SerializeField][Range(0,3)]
    private int websiteLvl, appLvl, carLvl, PuPLvl, droneLvl, physicalOrderLvl, phycialDeliveryLvl,
                websitResearchLvl, appResearchLvl, carResearchLvl, PuPResearchLvl, droneResearchLvl;
    public Color companyColor;
    public List<Service> OrderServices = new List<Service>();
    public List<Service> DeliveryServices = new List<Service>();
    public List<Service> ResearchedServices = new List<Service>();
    public int customerCount, salesCount;

    private void OnEnable()
    {
       OrderServices.Add(new Service(ServiceType.App, appScore, appLvl));
       OrderServices.Add(new Service(ServiceType.Website, websitScore, websiteLvl));
       OrderServices.Add(new Service(ServiceType.PhysicalOrder, physicalOrderScore, phycialDeliveryLvl));

       DeliveryServices.Add(new Service(ServiceType.Drone, droneScore, droneLvl));
       DeliveryServices.Add(new Service(ServiceType.Car, carScore, carLvl));
       DeliveryServices.Add(new Service(ServiceType.PickUpPoint, PuPScore, PuPLvl));
       DeliveryServices.Add(new Service(ServiceType.PhysicalDelivery, physicalDeliveryScore, phycialDeliveryLvl));

       ResearchedServices.Add(new Service(ServiceType.App, 0, appResearchLvl));
       ResearchedServices.Add(new Service(ServiceType.Website, 0, websitResearchLvl));
       ResearchedServices.Add(new Service(ServiceType.Drone, 0, droneResearchLvl));
       ResearchedServices.Add(new Service(ServiceType.Car, 0, carResearchLvl));
       ResearchedServices.Add(new Service(ServiceType.PickUpPoint, 0, PuPResearchLvl));

        for (int i = 0; i < inventoryList.Count; i++)
        {
            inventory.AddItem(inventoryList[i].item, inventoryList[i].amount);
        }
    }
}
