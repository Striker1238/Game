using TMPro;
using UnityEngine;
namespace UI
{
    [System.Serializable]
    public class StatTextBlock
    {
        public string TextValue;
        public TextMeshProUGUI PointText;
        public TextMeshProUGUI ModificationText;
        public GameObject UpButton;
        public GameObject DownButton;
        public void UpdateText(Stats stats)
        {
            int value = stats.GetStatValue(TextValue);
            int modifier = stats.GetModifier(TextValue);
            PointText.text = value.ToString();
            ModificationText.text = modifier.ToString();
        }
    }
}
