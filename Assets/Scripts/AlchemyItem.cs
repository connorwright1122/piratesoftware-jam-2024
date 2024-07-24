using UnityEngine;

[CreateAssetMenu(fileName = "NewAlchemyItem", menuName = "Inventory/AlchemyItem")]
public class AlchemyItem : ScriptableObject
{
    public string itemName;
    public Sprite icon; // You can use this to show the item in the UI
    // Add other properties as needed
}
