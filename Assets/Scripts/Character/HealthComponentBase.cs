using TMPro;
using UnityEngine;

public abstract class HealthComponentBase : MonoBehaviour, IHealthPoint, IDied
{
    [Header("HealthPoint")]
    [SerializeField] private int maxHealthPoint;
    public int MaxHealthPoint { get => maxHealthPoint; protected set => maxHealthPoint = value; }
    public int HealthPoint { get; protected set; }

    protected virtual void Awake()
    {
        HealthPoint = MaxHealthPoint;
    }

    public virtual void RestoringHealth(int healthPoint)
    {
        HealthPoint = Mathf.Min(HealthPoint + healthPoint, MaxHealthPoint);
    }

    public virtual void DecreaseHealth(int damage)
    {
        GameObject _hitText = Instantiate(GameManager.Instance.PrefabHitText, gameObject.transform);
        _hitText.GetComponent<TextMeshPro>().text = $"-{damage}";
        HealthPoint -= damage;

        Debug.Log($"Name: {gameObject.name} hp: {HealthPoint}");

        if (HealthPoint <= 0)
        {
            Died();
        }
    }

    public abstract void Died();
}
