using System.Collections.Generic;

namespace Inventory
{
    // Интерфейс для работы с хранилищами
    public interface IStorage
    {
        int MaxSlots { get; }
        List<Slot> Slots { get; }
        bool AddItem(Item item, int count);
        void SubtractionItem(int idSlot, int count);
    }

}
