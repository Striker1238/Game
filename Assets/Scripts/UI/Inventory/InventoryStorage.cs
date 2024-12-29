using System.Collections.Generic;

namespace Inventory
{
    // Реализация инвентаря
    public class InventoryStorage : IStorage
    {
        public int MaxSlots { get; private set; }
        public List<Slot> Slots { get; private set; }

        public InventoryStorage(int maxSlots)
        {
            MaxSlots = maxSlots;
            Slots = new List<Slot>(maxSlots);
            for (int i = 0; i < MaxSlots; i++)
            {
                Slots.Add(new Slot(i));
            }
        }

        //TODO: Slot переделать на Item
        public void AddItem(int idSlot, Item item, int count) => Slots.Find(slot => slot.Id == idSlot).AddItem(item, count);
        public void AddItem(Slot slot, Item item, int count) => slot.AddItem(item, count);


        //TODO: Slot переделать на Item
        public void SubtractionItem(int idSlot, int count) => Slots.Find(slot => slot.Id == idSlot).SubtractionItem(count);
        public void SubtractionItem(Slot slot, int count) => slot.SubtractionItem(count);
    }

}
