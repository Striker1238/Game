using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
    public enum SlotType
    {
        Backpack,
        Equipment,
        Weapon
    }

    public class Slot : MonoBehaviour, ISlot
    {
        protected internal int ID;
        public SlotType Type;
        public Item StorageItem;
        private int CountItem;


        public Slot() : this(SlotType.Backpack) { }
        public Slot( SlotType type) : this(type,null,0) { }
        public Slot( SlotType type, Item? item, int count)
        {
            Type = type;
            StorageItem = item;
            CountItem = count;
        }

        public int Count() => CountItem;
        public void Move()
        {
            throw new NotImplementedException();
        }
        public void Select()
        {
            throw new NotImplementedException();
        }
    }
}

