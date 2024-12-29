namespace Inventory
{
    public interface ISlot
    {
        int Id { get; }
        SlotType Type { get; }
        int CountItem { get; }
        void AddItem(Item item, int count);
        void SubtractionItem(int count);
    }
}