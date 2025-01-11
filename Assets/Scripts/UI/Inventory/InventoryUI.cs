using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform inventoryParent;

        private List<GameObject> slotObjects = new List<GameObject>();

        protected internal void InitializeUI(int maxSlots, List<Slot> slots)
        {
            var inventory = GetComponent<CharacterStorage>().inventory;
            for (int i = 0; i < maxSlots; i++)
            {
                var slotObject = Instantiate(slotPrefab, inventoryParent);
                var slot = slotObject.GetComponent<Slot>();
                inventory.Slots.Add(slot);
                slot.GetType().GetProperty("Id")?.SetValue(slot, i);
                slotObjects.Add(slotObject);
            }

            UpdateUI(slots);
        }

        protected internal void UpdateUI(List<Slot> slots)
        {
            
            for (int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                var slotObject = slotObjects[i];

                if (slot == null) continue;
                // Обновление текста и изображения
                var text = slotObject.GetComponentInChildren<TextMeshProUGUI>();
                text.text = (slot.CountItem > 1) ? slot.CountItem.ToString() : "";

                var image = slotObject.GetComponentsInChildren<Image>().First(x => x.name == "ItemImage");
                image.sprite = (slot.StorageItem != null) ? slot.StorageItem.Image : null;
                image.color = (slot.StorageItem != null) ? Color.white : Color.clear;
            }
        }
    }

}
