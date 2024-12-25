using TMPro;

[System.Serializable]
public class StatTextBlock
{
    public TextMeshProUGUI PointText;
    public TextMeshProUGUI ModificationText;
    public string TextValue;

    public StatTextBlock(TextMeshProUGUI PointTMP, TextMeshProUGUI ModificationTMP, string textValue)
    {
        PointText = PointTMP;
        ModificationText = ModificationTMP;
        TextValue = textValue;
    }
}
