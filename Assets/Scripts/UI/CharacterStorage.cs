using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    internal class CharacterStorage : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField] protected internal int MaxInventorySlot;
        [SerializeField] protected List<Slot> Inventory;

        [Header("Equipment")]
        [SerializeField] protected List<Slot> Equipment;

        public void Start()
        {
            EventBus.UpdateSlotsData += UpdateInventory;
            Equipment = new List<Slot>(4);
            Initialization();
        }
        private void Initialization()
        {
            //Создаем указанное количество слотов(префабов) на объекте родителя.
            for (int i = 0; i < MaxInventorySlot; i++)
            {
                GameObject _slot = Instantiate(UIManager.Instance.SlotPrefab, UIManager.Instance.InventoryPerent.transform);
                _slot.GetComponent<Slot>().ID = i;
                Inventory.Add(_slot.GetComponent<Slot>());
            }
        }
        
        private void UpdateInventory()
        {
            for (int i = 0; i < MaxInventorySlot; i++)
            {
            }
        }
    }
}
