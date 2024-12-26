using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyStats: Stats 
    {

        [SerializeField] protected internal List<Item> ItemDrop = new List<Item>();
        protected internal SpriteRenderer CharacterSprite;
        public override int HealthPoint
        {
            get
            {   
                return _healthPoint;
            }
            set
            {
                _healthPoint = value;
                if (_healthPoint > MaxHealthPoint) _healthPoint = MaxHealthPoint;
                if (_healthPoint <= 0)
                {
                    _healthPoint = 0;
                    GetComponent<EnemyAI>().Died();
                }
            }
        }


        public void Start()
        {
            CharacterSprite = GetComponent<SpriteRenderer>();
        }
    }
}
