using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "StatsData", menuName = "Stats/NewStats")]
public class Stats : ScriptableObject, IAttributeHolder, ILevelable, IName
{
    [Header("Attributes")]
    [SerializeField] private int strength = 8;
    [SerializeField] private int agility = 8;
    [SerializeField] private int constitution = 8;
    [SerializeField] private int intelligence = 8;
    [SerializeField] private int wisdom = 8;
    [SerializeField] private int charisma = 8;

    [Header("Character Info")]
    [SerializeField] private string name;
    [Min(1)]
    [SerializeField] private int level = 1;

    public int Strength { get => strength; private set => strength = value; }
    public int Agility { get => agility; private set => agility = value; }
    public int Constitution { get => constitution; private set => constitution = value; }
    public int Intelligence { get => intelligence; private set => intelligence = value; }
    public int Wisdom { get => wisdom; private set => wisdom = value; }
    public int Charisma { get => charisma; private set => charisma = value; }



    public int StrengthModifier { get => GetModifier(Strength); }
    public int AgilityModifier { get => GetModifier(Agility); }
    public int ConstitutionModifier { get => GetModifier(Constitution); }
    public int IntelligenceModifier { get => GetModifier(Intelligence); }
    public int WisdomModifier { get => GetModifier(Wisdom); }
    public int CharismaModifier { get => GetModifier(Charisma); }
    public string Name
    {
        get => name;
        set => name = value;
    }
    public int Level 
    {
        get => level;
        set => level = Mathf.Max(1, value);
    }
    public int GetModifier(int attributeValue) => (attributeValue / 2) - 5;
    public int GetModifier(string statName) 
    {
        switch (statName.ToLower())
        {
            case "strength": return StrengthModifier;
            case "agility": return AgilityModifier;
            case "constitution": return ConstitutionModifier;
            case "intelligence": return IntelligenceModifier;
            case "wisdom": return WisdomModifier;
            case "charisma": return CharismaModifier;
            default:
                Debug.Log($"����������� ��������������: {statName}");
                return 0;
        }
    }
    public int GetStatValue(string statName)
    {
        switch (statName.ToLower())
        {
            case "strength": return Strength;
            case "agility": return Agility;
            case "constitution": return Constitution;
            case "intelligence": return Intelligence;
            case "wisdom": return Wisdom;
            case "charisma": return Charisma;
            default:
                Debug.Log($"����������� ��������������: {statName}");
                return 0;
        }
    }
}