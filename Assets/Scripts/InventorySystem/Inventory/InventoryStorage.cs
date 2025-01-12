using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    // Реализация инвентаря
    public class InventoryStorage : IStorage
    {
        public int MaxSlots { get; private set; }
        public List<Slot> Slots { get; set; } = new List<Slot>();

        public InventoryStorage(int maxSlots)   
        {
            MaxSlots = maxSlots;
            Slots = new List<Slot>(maxSlots);
        }
        public bool AddItem(Item item, int count)
        {
            //1 - ищем похожие слоты
            var SuitableSlot = Slots.Find(slot => slot.StorageItem?.ID == item.ID);
            if (SuitableSlot != null) 
            {
                Debug.Log($"Добавил предмет такой же слот");
                SuitableSlot.AddItem(item, count);
                return true;
            }

            //2 - ищем пустые слоты
            SuitableSlot = Slots.Find(slot => slot?.StorageItem == null);
            if (SuitableSlot != null)
            {
                Debug.Log($"Добавил предмет в пустой слот");
                SuitableSlot.AddItem(item, count);
                return true;
            }
            //3 - выводим ошибку что нет слотов свободных
            //и выбрасываем предметы обратно на землю
            Debug.Log("Нет свободных слотов");
            return false;
        }
        public void AddItem(int idSlot, Item item, int count) => Slots.Find(slot => slot.Id == idSlot).AddItem(item, count);
        public void AddItem(Slot slot, Item item, int count) => slot.AddItem(item, count);

        public void SubtractionItem(int idSlot, int count) => Slots.Find(slot => slot.Id == idSlot).SubtractionItem(count);
        public void SubtractionItem(Slot slot, int count) => slot.SubtractionItem(count);
    }

}
