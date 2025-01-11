using Inventory;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item ItemData { get; private set; }

    public void Initialize(Item item)
    {
        ItemData = item;
    }
}
