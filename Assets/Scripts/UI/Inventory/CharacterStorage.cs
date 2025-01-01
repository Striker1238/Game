using UnityEngine;

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

            inventoryUI.InitializeUI(maxInventorySlot, inventory.Slots);
        }

        public void UpdateInventoryUI()
        {
            inventoryUI.UpdateUI(inventory.Slots);
        }
    }
}
