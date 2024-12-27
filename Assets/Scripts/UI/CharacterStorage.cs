using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    internal class CharacterStorage : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField] protected internal int MaxInventorySlot;
        [SerializeField] protected List<Slot> Inventory;

        [Header("Equipment")]
        [SerializeField] protected List<Slot> Equipment;
        [SerializeField] private List<GameObject> SlotObjects = new List<GameObject>();

        public void Start()
        {
            UIEvents.UpdateSlotsData += UpdateInventory;
            Equipment = new List<Slot>(4);
            Initialization();
        }
        private void Initialization()
        {
            //Создаем указанное количество слотов(префабов) на объекте родителя.
            for (int i = 0; i < MaxInventorySlot; i++)
            {
                SlotObjects.Add(Instantiate(UIManager.Instance.SlotPrefab, UIManager.Instance.InventoryPerent.transform));
                SlotObjects[i].GetComponent<Slot>().ID = i;
                Inventory.Add(SlotObjects[i].GetComponent<Slot>());
            }
            
            UpdateInventory();
        }
        
        private void UpdateInventory()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i] == null) continue;
                
                //Пишем количество предметов в слоте
                SlotObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = ((Inventory[i].Count() > 1) ? Inventory[i].Count().ToString() : "");

                //Устанавливаем изображение на слоты
                var SlotImage = SlotObjects[i].GetComponentsInChildren<Image>().First(x => x.name == "ItemImage");

                SlotImage.sprite = (Inventory[i].StorageItem != null) ? Inventory[i].StorageItem.Image : null;
                SlotImage.color = (Inventory[i].StorageItem != null) ? Color.white : Color.clear;
            }
        }
    }
}
