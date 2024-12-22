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
        public int ID;
        public SlotType Type;
        public Item StorageItem;
        public Slot() : this(SlotType.Backpack) { }
        public Slot( SlotType type) : this(type,null) { }
        public Slot( SlotType type, Item? item)
        {
            Type = type;
            StorageItem = item;
        }

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

