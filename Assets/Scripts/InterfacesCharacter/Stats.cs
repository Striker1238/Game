using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    [Header("MainStats")]
    [SerializeField] protected internal int MaxHealthPoint;
    [SerializeField] private protected int _healthPoint;
    public virtual int HealthPoint
    {
        get
        {
            return _healthPoint;
        }
        set
        {
            _healthPoint = value;
        }
    }
    [SerializeField] protected internal int MaxManaPool;
    [SerializeField] private protected int _manaPool;
    public virtual int ManaPool
    {
        get
        {
            return _manaPool;
        }
        set
        {
            _manaPool = value;
        }
    }
    [SerializeField] private protected float _dodge;
    public virtual float Dodge
    {
        get
        {
            return _dodge;
        }
        set
        {
            _dodge = value;
        }
    }
    [SerializeField] private protected string _name;
    public virtual string Name 
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    [Min(1)]
    [SerializeField] private protected int _level;
    public virtual int Level 
    {
        get => _level;
        set
        {
            _level = value;
        }
    }




    [Header("Meaning of Characteristic")]
    [SerializeField] private protected int _strength;
    protected internal int Strength 
    { 
        get => _strength; 
        set 
        {
            _strength = value;
            StrengthModifier = (_strength / 2) - 5;
        } 
    }
    [SerializeField] private protected int _agility;
    protected internal int Agility 
    { 
        get => _agility;
        set 
        { 
            _agility = value;
            AgilityModifier = (_agility / 2) - 5;
        }
    }
    [SerializeField] private protected int _constitution;
    protected internal int Constitution 
    { 
        get => _constitution;
        set
        {
            _constitution = value;
            MaxHealthPoint = 3 * value;
            _healthPoint = MaxHealthPoint;
            ConstitutionModifier = (_constitution / 2) - 5;
        }
    }
    [SerializeField] private protected int _intelligence;   
    protected internal int Intelligence { 
        get => _intelligence; 
        set 
        { 
            _intelligence = value;
            MaxManaPool = 15 * value;
            _manaPool = MaxManaPool;
            IntelligenceModifier = (_intelligence / 2) - 5;
        } 
    }
    [SerializeField] private protected int _wisdom;
    protected internal int Wisdom 
    {
        get => _wisdom;
        set
        {
            _wisdom = value;
            WisdomModifier = (_wisdom / 2) - 5;
        }
    }
    [SerializeField] private protected int _charisma;
    protected internal int Charisma 
    {
        get => _charisma;
        set
        {
            _charisma = value;
            CharismaModifier = (_charisma / 2) - 5;
        }
    }


    [Header("Characteristic modifier")]
    [SerializeField] protected internal int StrengthModifier;
    [SerializeField] protected internal int AgilityModifier;
    [SerializeField] protected internal int ConstitutionModifier;
    [SerializeField] protected internal int IntelligenceModifier;
    [SerializeField] protected internal int WisdomModifier;
    [SerializeField] protected internal int CharismaModifier;



    [Header("Attack Settings")]
    [SerializeField] private protected int _damage;
    public virtual int Damage { get => _damage; set => _damage = value; }
    [SerializeField] protected internal float AttackRange = 0.5f;
    [SerializeField] protected internal Transform AttackTriggerPosition;

}