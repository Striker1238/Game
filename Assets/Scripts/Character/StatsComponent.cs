using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField] private Stats attributeHolder;
    public Stats AttributeHolder => attributeHolder;
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