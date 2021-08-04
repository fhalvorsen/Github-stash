using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Wholesaler", menuName = "Scriptable Object/New Wholesaler object")]
public class WholesalerData : ScriptableObject
{
    public List<ItemData> Inventory;
    [SerializeField]
    public Sprite logo;
    public enum NegotiationLevel
    {
        Default,
        Better,
        Best
    }
    
    public NegotiationLevel negotiationLevel = NegotiationLevel.Default;
    public WholesalerData()
    {
        
     
    }

    public void SetWholesalerPrices(NegotiationLevel level)
    {
        foreach (var item in Inventory)
        {
            switch (level)
            {
                case NegotiationLevel.Default:
                    item.wholesalerPrice = item.basePrice * 1.20f;
                    break;
                case NegotiationLevel.Better:
                    item.wholesalerPrice = item.basePrice * 1.15f;
                    break;
                case NegotiationLevel.Best:
                    item.wholesalerPrice = item.basePrice * 1.10f;
                    break;
            }
        }
    }
}
