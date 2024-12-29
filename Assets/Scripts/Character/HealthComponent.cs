using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealthPoint, IDied
{
    [Header("HealthPoint")]
    [SerializeField]private int maxHealthPoint;
    public int MaxHealthPoint { get => maxHealthPoint; private set => maxHealthPoint = value; }
    public int HealthPoint { get; private set; }

    public void Awake()
    {
        HealthPoint = MaxHealthPoint;
    }
    public void RestoringHealth(int healthPoint)
    {
        HealthPoint = Mathf.Min(HealthPoint + healthPoint, MaxHealthPoint); ;
    }
    public void DecreaseHealth(int damage)
    {
        GameObject _hitText = Instantiate(GameManager.Instance.PrefabHitText, gameObject.transform);
        _hitText.GetComponent<TextMeshPro>().text = $"-{damage}";
        HealthPoint -= damage;
        //PlayerEvents.OnHitHero(HealthPoint, MaxHealthPoint);
        Debug.Log($"Name: {gameObject.name} hp: {HealthPoint}");
        if (HealthPoint <= 0) Died();
    }
    public void Died()
    {
        HealthPoint = 0;
        GetComponent<EnemyAI>().Died();
        //PlayerEvents.OnHeroDied();
    }
}