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
    [CreateAssetMenu(fileName ="ItemData", menuName ="Inventory/New Item")]
    [SerializeField]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id;
        public int ID => _id;

        [SerializeField] private string _name;
        public string Name => _name;

        [TextArea(3, 20)]
        [SerializeField] private string _description;
        public string Description => _description;

        [SerializeField] private Sprite _image;
        public Sprite Image => _image;

        [SerializeField] private ItemRarity _rarity;
        public ItemRarity Rarity => _rarity;

        [SerializeField] private SlotType _type;
        public SlotType Type => _type;
    }
}

