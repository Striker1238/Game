using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    public enum ItemRarity
    {
        Common,
        Rare,
        Mystical,
        Legendary,
        Artifact
    }
    [CreateAssetMenu(fileName ="ItemData", menuName ="Inventory/Item")]
    public class Item : ScriptableObject
    {
        protected int ID { get; set; }
        [SerializeField] protected string Name;
        [TextArea(3,20)]
        [SerializeField] protected string Description;
        [SerializeField] protected Sprite Image;
        [SerializeField] protected ItemRarity Rarity;
        [SerializeField] protected SlotType Type;
        //[SerializeField] protected object? ItemScript;
    }
}

