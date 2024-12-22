using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Weapon")]
    public class Weapon : Item
    {
        [SerializeField]protected int _level = 0;
        protected int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }
        [SerializeField]protected int _damage = 0;
        protected int Damage
        {
            get
            {
                return _damage;
            }
            set
            {
                _damage = value;
            }
        }
    }
}
