using UnityEngine;
using System.Collections.Generic;




[CreateAssetMenu(fileName = "New Customer", menuName = "Scriptable Object/New Customer object")]
public class CustomerData : ScriptableObject 
{
    [System.Serializable]
    public class ItemInfo
    {
        [SerializeField] public ItemData item;
        [SerializeField] public int amount;
    }

    public List<ItemInfo> itemList = new List<ItemInfo>();
    public ItemDictionary ShoppingList = new ItemDictionary();
    new public string name = "New customer";
    public int age = 30;
    public string MessageForPlayer;
    public ServiceType OrderPrefference;
    [Range(0, 100)]
    public int websitePrefScore, appPrefScore, physOrderPrefScore;
    public ServiceType DeliveryPrefference;
    [Range(0, 100)]
    public int carPrefScore, physDeliveryPrefScore;

    public CompanyData PreferedCompany;

    public Quality qualityPref = Quality.Okay;
    public PriceRating priceRatingPref = PriceRating.Normal;

    public float orderInterval = 60f;
    public bool isOrdering = false;

    public Building building;

    void OnEnable()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            ShoppingList.AddItem(itemList[i].item, itemList[i].amount);
        }
    }
}
