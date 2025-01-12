using UnityEngine;

namespace Inventory
{
    public enum SlotType
    {
        Backpack,
        Equipment,
        Weapon
    }

    public class Slot: MonoBehaviour, ISlot
    {
        public int Id { get; private set; }
        public SlotType Type { get; private set; } = SlotType.Backpack;
        [SerializeField]
        public Item? storageItem;
        public Item? StorageItem { get => storageItem; private set => storageItem = value; }
        public int CountItem { get; private set; } = 0;


        public Slot(int id) : this(id, SlotType.Backpack) { }
        public Slot(int id, SlotType type) : this(id, type, null, 0) { }
        public Slot(int id, SlotType type, Item? item, int count)
        {
            Id = id;
            Type = type;
            StorageItem = item;
            CountItem = count;
        }
        public void AddItem(Item item, int count)
        {
            StorageItem = item;
            CountItem += count;
        }

        public void SubtractionItem(int count)
        {
            CountItem -= count;
            if (CountItem <= 0)
            {
                StorageItem = null;
                CountItem = 0;
            }
        }
    }
}

