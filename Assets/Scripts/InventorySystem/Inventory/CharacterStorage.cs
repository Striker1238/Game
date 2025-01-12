using UnityEngine;
using UI.Inventory;

namespace Inventory
{
    [RequireComponent(typeof(InventoryUI))]
    public class CharacterStorage : MonoBehaviour
    {
        [Header("Storage Settings")]
        [SerializeField] private int maxInventorySlot;

        [SerializeField] protected internal InventoryStorage inventory;
        private InventoryUI inventoryUI;

        private void Start()
        {
            UIEvents.UpdateSlotsData += UpdateInventoryUI;

            inventory = new InventoryStorage(maxInventorySlot);
            inventoryUI = GetComponent<InventoryUI>();

            // Инициализируем наш инвентарь 
            inventoryUI.InitializeUI(maxInventorySlot, inventory.Slots);
        }


        // Обновление инвентаря
        public void UpdateInventoryUI() => inventoryUI.UpdateUI(inventory.Slots);
    }
}
