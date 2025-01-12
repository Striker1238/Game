using Inventory;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    [RequireComponent(typeof(UIAnimationHandler))]
    public class InventoryUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject inventoryParent;
        private UIAnimationHandler animationHandler;

        private List<GameObject> slotObjects = new List<GameObject>();


        public void Awake()
        {
            animationHandler = GetComponent<UIAnimationHandler>();
        }

        // Инициализирует инвентарь создавая объекты слотов
        protected internal void InitializeUI(int maxSlots, List<Slot> slots)
        {
            var inventory = GetComponent<CharacterStorage>().inventory;
            for (int i = 0; i < maxSlots; i++)
            {
                var slotObject = Instantiate(slotPrefab, inventoryParent.transform);
                var slot = slotObject.GetComponent<Slot>();
                // Добавляем слот в наш лист
                inventory.Slots.Add(slot);
                slot.GetType().GetProperty("Id")?.SetValue(slot, i);
                // Добавляем созданный объект слота в лист
                slotObjects.Add(slotObject);
            }
            // Обновляем UI инвентаря
            UpdateUI(slots);
        }

        // Обновление UI 
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

        // Открытие инвентаря
        public void InventoryOpen()
        {
            animationHandler.TogglePanel(inventoryParent);
            if (!inventoryParent.activeSelf) UIEvents.OnUpdateSlots();
        }
    }

}
