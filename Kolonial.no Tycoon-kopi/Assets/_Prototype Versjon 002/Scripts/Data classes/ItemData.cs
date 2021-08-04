using UnityEngine;

public enum Quality
{
    Poor,
    Okay,
    Great
}
public enum PriceRating
{
    Exclusive,
    Normal,
    Budget
}

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/New Item object")]
public class ItemData : ScriptableObject 
{
    new public string name = "New item";
    public Sprite icon;
    public float basePrice, wholesalerPrice, retailPrice;
    public Quality quality = Quality.Okay;
    public PriceRating priceRating = PriceRating.Normal;
}
