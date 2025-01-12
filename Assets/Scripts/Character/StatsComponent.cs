using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField] private Stats attributeHolder;
    public Stats AttributeHolder => attributeHolder;
    [SerializeField] private int points = 20;
    public int Points => points;
    private void Start()
    {
        if (AttributeHolder == null)
        {
            Debug.LogError("AttributeHolder is not assigned in the inspector.");
        }
        else
        {
            Debug.Log($"AttributeHolder: {AttributeHolder}");
        }
    }
}