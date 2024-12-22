using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    [Header("MainStats")]
    [SerializeField] private protected int MaxHealthPoint;
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
    [SerializeField] private protected int MaxManaPool;
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
    public int Strength 
    { 
        get => _strength; 
        set 
        {
            _strength = value;
            StrengthModifier = (_strength / 2) - 5;
        } 
    }
    [SerializeField] private protected int _agility;
    public int Agility 
    { 
        get => _agility;
        set 
        { 
            _agility = value;
            AgilityModifier = (_agility / 2) - 5;
        }
    }
    [SerializeField] private protected int _constitution;
    public int Constitution 
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
    public int Intelligence { 
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
    public int Wisdom 
    {
        get => _wisdom;
        set
        {
            _wisdom = value;
            WisdomModifier = (_wisdom / 2) - 5;
        }
    }
    [SerializeField] private protected int _charisma;
    public int Charisma 
    {
        get => _charisma;
        set
        {
            _charisma = value;
            CharismaModifier = (_charisma / 2) - 5;
        }
    }


    [Header("Characteristic modifier")]
    private protected int StrengthModifier { get; set; }
    private protected int AgilityModifier { get; set; }
    private protected int ConstitutionModifier { get; set; }
    private protected int IntelligenceModifier { get; set; }
    private protected int WisdomModifier { get; set; }
    private protected int CharismaModifier { get; set; }



    [Header("Attack Settings")]
    [SerializeField] private protected int _damage;
    public virtual int Damage { get => _damage; set => _damage = value; }
    [SerializeField] private protected float AttackRange = 0.5f;
    [SerializeField] private protected Transform AttackTriggerPosition;

}