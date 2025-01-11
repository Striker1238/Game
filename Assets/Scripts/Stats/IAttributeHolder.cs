using UnityEngine;

public interface IAttributeHolder
{
    [Header("Meaning of Characteristic")]
    int Strength { get; }
    int Agility { get; }
    int Constitution { get; }
    int Intelligence { get; }
    int Wisdom { get; }
    int Charisma { get; }


    [Header("Characteristic modifier")]
    int StrengthModifier { get; }
    int AgilityModifier { get; }
    int ConstitutionModifier { get; }
    int IntelligenceModifier { get; }
    int WisdomModifier { get; }
    int CharismaModifier { get; }
    int GetModifier(int attributeValue);
    int GetStatValue(string statName);
}
